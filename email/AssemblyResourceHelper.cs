using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace WpfTestHarness.email
{
    public class AssemblyResourceHelper
    {
        private const string GenericNotificationTemplate = "Wpf.email.GenericEmailTemplate.eml";

        public static string GenericNoticeEmailTemplate
        {
            get
            {
                string resourceData = string.Empty;

                try
                {
                    Assembly assembly = typeof(AssemblyResourceHelper).Assembly;
                    Stream resourceStream = assembly.GetManifestResourceStream(GenericNotificationTemplate);
                    resourceData = new StreamReader(resourceStream).ReadToEnd();
                }
                catch (Exception ex)
                {
                    resourceData = ex.Message;
                }
                finally
                {

                }

                return resourceData;
            }
        }
    }
}
