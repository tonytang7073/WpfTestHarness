using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace WpfTestHarness.email
{
    public class RxMailMessage : MailMessage
    {
        /// <summary>
        /// To whom the email was delivered to
        /// </summary>
        public MailAddress DeliveredTo;
        /// <summary>
        /// To whom the email was
        /// </summary>
        public MailAddress ReturnPath;
        /// <summary>
        /// 
        /// </summary>
        public DateTime DeliveryDate;
        /// <summary>
        /// Date when the email was received
        /// </summary>
        public string MessageId;
        /// <summary>
        /// probably '1,0'
        /// </summary>
        public string MimeVersion;
        /// <summary>
        /// It may be desirable to allow one body to make reference to another. Accordingly, 
        /// bodies may be labelled using the "Content-ID" header field.    
        /// </summary>
        public string ContentId;
        /// <summary>
        /// some descriptive information for body
        /// </summary>
        public string ContentDescription;
        /// <summary>
        /// ContentDisposition contains normally redundant information also stored in the 
        /// ContentType. Since ContentType is more detailed, it is enough to analyze ContentType
        /// 
        /// something like:
        /// inline
        /// inline; filename="image001.gif
        /// attachment; filename="image001.jpg"
        /// </summary>
        public ContentDisposition ContentDisposition;
        /// <summary>
        /// something like "7bit" / "8bit" / "binary" / "quoted-printable" / "base64"
        /// </summary>
        public string TransferType;
        /// <summary>
        /// similar as TransferType, but .NET supports only "7bit" / "quoted-printable"
        /// / "base64" here, "bit8" is marked as "bit7" (i.e. no transfer encoding needed), 
        /// "binary" is illegal in SMTP
        /// </summary>
        public TransferEncoding ContentTransferEncoding;
        /// <summary>
        /// The Content-Type field is used to specify the nature of the data in the body of a
        /// MIME entity, by giving media type and subtype identifiers, and by providing 
        /// auxiliary information that may be required for certain media types. Examples:
        /// text/plain;
        /// text/plain; charset=ISO-8859-1
        /// text/plain; charset=us-ascii
        /// text/plain; charset=utf-8
        /// text/html;
        /// text/html; charset=ISO-8859-1
        /// image/gif; name=image004.gif
        /// image/jpeg; name="image005.jpg"
        /// message/delivery-status
        /// message/rfc822
        /// multipart/alternative; boundary="----=_Part_4088_29304219.1115463798628"
        /// multipart/related; 	boundary="----=_Part_2067_9241611.1139322711488"
        /// multipart/mixed; 	boundary="----=_Part_3431_12384933.1139387792352"
        /// multipart/report; report-type=delivery-status; boundary="k04G6HJ9025016.1136391237/carbon.singnet.com.sg"
        /// </summary>
        public ContentType ContentType;
        /// <summary>
        /// .NET framework combines MediaType (text) with subtype (plain) in one property, but
        /// often one or the other is needed alone. MediaMainType in this example would be 'text'.
        /// </summary>
        public string MediaMainType;
        /// <summary>
        /// .NET framework combines MediaType (text) with subtype (plain) in one property, but
        /// often one or the other is needed alone. MediaSubType in this example would be 'plain'.
        /// </summary>
        public string MediaSubType;
        /// <summary>
        /// RxMessage can be used for any MIME entity, as a normal message body, an attachement or an alternative view. ContentStream
        /// provides the actual content of that MIME entity. It's mainly used internally and later mapped to the corresponding 
        /// .NET types.
        /// </summary>
        public Stream ContentStream;
        /// <summary>
        /// A MIME entity can contain several MIME entities. A MIME entity has the same structure
        /// like an email. 
        /// </summary>
        public List<RxMailMessage> Entities;
        /// <summary>
        /// This entity might be part of a parent entity
        /// </summary>
        public RxMailMessage Parent;
        /// <summary>
        /// The top most MIME entity this MIME entity belongs to (grand grand grand .... parent)
        /// </summary>
        public RxMailMessage TopParent;
        /// <summary>
        /// The complete entity in raw content. Since this might take up quiet some space, the raw content gets only stored if the
        /// Pop3MimeClient.isGetRawEmail is set.
        /// </summary>
        public string RawContent;
        /// <summary>
        /// Headerlines not interpretable by Pop3ClientEmail
        /// <example></example>
        /// </summary>
        public List<string> UnknowHeaderlines; //


        // Constructors
        // ------------
        /// <summary>
        /// default constructor
        /// </summary>
        public RxMailMessage()
        {
            //for the moment, we assume to be at the top
            //should this entity become a child, TopParent will be overwritten
            TopParent = this;
            Entities = new List<RxMailMessage>();
            UnknowHeaderlines = new List<string>();
        }

        /// <summary>
        /// Set all content disposition related fields
        /// </summary>
        public void SetContentDisposition(string headerLineContent)
        {
            // example Content-Disposition: inline; filename="PilotsEy.gif"; size=7242; creation-date="Thu, 13 Nov 2008 14:03:50 GMT"; modification-date="Thu, 13 Nov 2008 14:03:50 GMT"
            string[] saParms = headerLineContent.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (saParms.Length == 0)
            {
                this.ContentDisposition = new ContentDisposition("inline");
                return;
            }

            // do the type and create the object
            this.ContentDisposition = new ContentDisposition(saParms[0].Trim());

            // now for the parms (skip the first array value since the RFC says is has to be the type and is done already)
            for (int i = 1; i < saParms.Length; i++)
            {
                string[] saNameValue = saParms[i].Split(new char[] { '=' });
                if (saNameValue.Length != 2)
                    continue;   // shouldn't happen
                string sName = saNameValue[0].Trim().ToLower();
                string sValue = saNameValue[1].Trim();
                sValue = sValue.Replace("\"", "");
                switch (sName)
                {
                    case "filename":
                        this.ContentDisposition.FileName = sValue;
                        break;
                    case "size":
                        this.ContentDisposition.Size = long.Parse(sValue);
                        break;
                    case "creation-date":
                        this.ContentDisposition.CreationDate = DateTime.Parse(sValue);
                        break;
                    case "modification-date":
                        this.ContentDisposition.ModificationDate = DateTime.Parse(sValue);
                        break;
                    case "read-date":
                        this.ContentDisposition.ReadDate = DateTime.Parse(sValue);
                        break;
                }
            }
        }

        /// <summary>
        /// Set all content type related fields
        /// </summary>
        public void SetContentTypeFields(string contentTypeString)
        {
            contentTypeString = contentTypeString.Trim();
            //set content type
            if (contentTypeString == null || contentTypeString.Length < 1)
            {
                ContentType = new ContentType("text/plain; charset=us-ascii");
            }
            else
            {
                ContentType = new ContentType(contentTypeString);
            }

            //set encoding (character set)
            if (ContentType.CharSet == null)
            {
                BodyEncoding = Encoding.ASCII;
            }
            else
            {
                try
                {
                    BodyEncoding = Encoding.GetEncoding(ContentType.CharSet);
                }
                catch
                {
                    BodyEncoding = Encoding.ASCII;
                }
            }

            //set media main and sub type
            if (ContentType.MediaType == null || ContentType.MediaType.Length < 1)
            {
                //no mediatype found
                ContentType.MediaType = "text/plain";
            }
            else
            {
                string mediaTypeString = ContentType.MediaType.Trim().ToLowerInvariant();
                int slashPosition = ContentType.MediaType.IndexOf("/");
                if (slashPosition < 1)
                {
                    //only main media type found
                    MediaMainType = mediaTypeString;
                    System.Diagnostics.Debugger.Break(); //didn't have a sample email to test this
                    if (MediaMainType == "text")
                    {
                        MediaSubType = "plain";
                    }
                    else
                    {
                        MediaSubType = "";
                    }
                }
                else
                {
                    //also submedia found
                    MediaMainType = mediaTypeString.Substring(0, slashPosition);
                    if (mediaTypeString.Length > slashPosition)
                    {
                        MediaSubType = mediaTypeString.Substring(slashPosition + 1);
                    }
                    else
                    {
                        if (MediaMainType == "text")
                        {
                            MediaSubType = "plain";
                        }
                        else
                        {
                            MediaSubType = "";
                            System.Diagnostics.Debugger.Break(); //didn't have a sample email to test this
                        }
                    }
                }
            }

            IsBodyHtml = MediaSubType == "html";
        }


        /// <summary>
        /// Creates an empty child MIME entity from the parent MIME entity.
        /// 
        /// An email can consist of several MIME entities. A entity has the same structure
        /// like an email, that is header and body. The child inherits few properties 
        /// from the parent as default value.
        /// </summary>
        public RxMailMessage CreateChildEntity()
        {
            RxMailMessage child = new RxMailMessage();
            child.Parent = this;
            child.TopParent = this.TopParent;
            child.ContentTransferEncoding = this.ContentTransferEncoding;
            return child;
        }

        public static RxMailMessage CreateFromFile(string sEmlPath)
        {
            MimeReader mimeDecoder = new MimeReader();
            return mimeDecoder.GetEmail(sEmlPath);
        }

        public static RxMailMessage CreateFromFile(MimeReader mimeDecoder, string sEmlPath)
        {
            return mimeDecoder.GetEmail(sEmlPath);
        }

        public static RxMailMessage CreateFromStream(Stream EmailStream)
        {
            MimeReader mimeDecoder = new MimeReader();
            return mimeDecoder.GetEmail(EmailStream);
        }

        public static RxMailMessage CreateFromStream(MimeReader mimeDecoder, Stream EmailStream)
        {
            return mimeDecoder.GetEmail(EmailStream);
        }

    }
}
