using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTestHarness.helpers;
using WpfTestHarness.model;
using WpfTestHarness.viewmodel;
using System.Net;
using System.IO;

namespace WpfTestHarness.UserControls
{
    /// <summary>
    /// Interaction logic for TestHarness.xaml
    /// </summary>
    public partial class TestHarness : UserControl
    {
        private TestharnessViewModel _viewModel = null;
        public TestHarness()
        {
            InitializeComponent();
            _viewModel = new TestharnessViewModel();

            EndDateCaculation.Tag = new ApiTestMethodAccessor() { fInput = GetEndDateCaculationInput, fResult = GetEndDateCaculationResult };
            regexTest.Tag = new ApiTestMethodAccessor() { fInput = GetRegexTestInput, fResult = GetRegexTestResult };
            whiteSpaceDelimited.Tag = new ApiTestMethodAccessor() { fInput = GetWhiteSpaceDelimitedInput, fResult = GetWhiteSpaceDelimitedResult };
            totalTermCaculation.Tag = new ApiTestMethodAccessor() { fInput = GetTotalTermCaculationInput, fResult = GetTotalTermCaculationResult };
            httpGet.Tag = new ApiTestMethodAccessor() { fInput = GetHttpGetInput, fResult = GetHttpGetResult };
            mimeConverter.Tag = new ApiTestMethodAccessor() { fInput = GetMIMEConverterInput, fResult = GetMIMEConverterResult };
        }

        private void GetMIMEConverterResult()
        {
            //txtResult.Text = "fixed me";
            //email.MimeReader mr = new email.MimeReader();
            //byte[] data = Encoding.ASCII.GetBytes(email.AssemblyResourceHelper.GenericNoticeEmailTemplate);
            //MemoryStream ms = new MemoryStream(data);
            //email.RxMailMessage rm = mr.GetEmail(ms);

           
            Regex regex = new Regex(@"<html>(.|\n)*?<\/html>", RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(email.AssemblyResourceHelper.GenericNoticeEmailTemplate);

            string html = ExtractString(email.AssemblyResourceHelper.GenericNoticeEmailTemplate, "<html", "/html>");
            txtInput.Text = html;
            //string xx = email.QuotedPrintable.Decode(html);

            string test = @"xmlns:o=3D""urn: schemas - microsoft - com:office: office"" ";

            txtResult.Text = email.QuotedPrintable.Decode(test);
            
        }

        public static string ExtractString(string mimeMessage, string startTag, string endTag)
        {
            if (string.IsNullOrEmpty(mimeMessage)) { return mimeMessage; }

            int startIndex = mimeMessage.IndexOf(startTag, StringComparison.CurrentCultureIgnoreCase);
            startIndex = startIndex == -1 ? 0 : startIndex;
            int endIndex = mimeMessage.IndexOf(endTag, startIndex, StringComparison.CurrentCultureIgnoreCase);
            endIndex = endIndex > mimeMessage.Length || endIndex == -1 ? mimeMessage.Length : endIndex + endTag.Length;
            return mimeMessage.Substring(startIndex, endIndex - startIndex);
        }

        private object GetMIMEConverterInput()
        {
            return email.AssemblyResourceHelper.GenericNoticeEmailTemplate;
            
        }

        private void GetHttpGetResult()
        {
            string html = string.Empty;
            HttpGetInput httpUrl = txtInput.Text.jsonDeserializeFromString<HttpGetInput>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpUrl.Url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                html = ex.Message;
            }

            txtResult.Text = html;
        }

        private object GetHttpGetInput()
        {
            return new HttpGetInput { Url = "http://scdcictsweb51/ICMSACT_R7_ICMSServer/Status/Default.aspx" };
        }

        private void SampleInput_Click(object sender, RoutedEventArgs e)
        {
            ApiTestMethodAccessor api = GetMethodAccessor();
            object obj = api.fInput();
            if (obj == null)
            {
                txtInput.Text = "{}";
            }
            else
            {
                txtInput.Text = obj.jsonSerializeToString();
            }
        }

        private void ReDo_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = String.Empty;
            ApiTestMethodAccessor api = GetMethodAccessor();
            api.fResult();
        }

        private void tabTestName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtResult.Text = string.Empty;
            SampleInput_Click(null, null);
        }


        internal class ApiTestMethodAccessor
        {
            public Func<object> fInput;
            public Action fResult;
        }

        private ApiTestMethodAccessor GetMethodAccessor()
        {
            TabItem ti = tabTestName.SelectedItem as TabItem;
            return (ApiTestMethodAccessor)ti.Tag;
        }

        private void GetEndDateCaculationResult()
        {
            List<EndDateCaculationInput> tuples = txtInput.Text.jsonDeserializeFromString<List<EndDateCaculationInput>>();
            foreach (var item in tuples)
            {
                DateTime x = CaculateIndivisualSentenceEndDate(item.StartDate, item.Year, item.Month, item.Days);
                txtResult.Text += "Start Date:" + item.StartDate.ToString("yyyy-MM-dd") + " Years:" + item.Year + " Months:" + item.Month + " Days:" + item.Days + " End Date:" + x.ToString("yyyy-MM-dd") + Environment.NewLine;
            }

        }

        private DateTime CaculateIndivisualSentenceEndDate(DateTime startDate, int years, int months, double days)
        {
            DateTime retMinusOne = startDate.AddYears(years).AddMonths(months).AddDays(days).AddDays(-1);
            if (days != 0) // if days specified, do the calcualtion as previous
            {
                return retMinusOne;
            }
            DateTime tryDateNoMinus = startDate.AddYears(years).AddMonths(months);
            if (startDate.Day == 31 && tryDateNoMinus.Day == 31) { return retMinusOne; } //start 31 end at 30
            else if (startDate.Day == 31) { return tryDateNoMinus; }   //start 31 end at 30, 29 (Leep) or 28 No need to Add(-1)
            else if (startDate.Day == 30 && tryDateNoMinus.Day >= 30) { return retMinusOne; } //start 30 end at 29
            else if (startDate.Day == 30) { return tryDateNoMinus; } //start 30 end at 29 (Leep) or 28 No need to Add(-1)
            else if (startDate.Day == 29 && tryDateNoMinus.Day >= 29) { return retMinusOne; } //start 29 end at 28
            else if (startDate.Day == 29) { return tryDateNoMinus; } //start 29 end at 28 (noneLeep) No need to Add(-1)
            else if (startDate.Day <= 28) { return retMinusOne; }


            return retMinusOne; //should never be able to reach here.

        }


        private object GetEndDateCaculationInput()
        {
            return new List<EndDateCaculationInput>()
                        {
                            new EndDateCaculationInput(new DateTime(2021, 1, 29), 0, 1, 0),
                            new EndDateCaculationInput(new DateTime(2020, 2, 29), 1, 0, 0), //leap year
                            new EndDateCaculationInput(new DateTime(2021, 2, 28), 2, 0, 0),
                            new EndDateCaculationInput(new DateTime(2020, 3, 29), 0, 11, 0),
                            new EndDateCaculationInput(new DateTime(2019, 4, 29), 0, 10, 0), //leap year
                            new EndDateCaculationInput(new DateTime(2021, 1, 30), 0, 1, 0),
                            new EndDateCaculationInput(new DateTime(2021, 3, 30), 0, 11, 0),
                            new EndDateCaculationInput(new DateTime(2019, 4, 30), 0, 11, 0), //leap year
                            new EndDateCaculationInput(new DateTime(2021, 1, 31), 0, 1, 0),
                            new EndDateCaculationInput(new DateTime(2021, 3, 31), 0, 11, 0),
                            new EndDateCaculationInput(new DateTime(2019, 3, 31), 0, 11, 0), //leap year
                            new EndDateCaculationInput(new DateTime(2021, 1, 31), 0, 3, 0),
                            new EndDateCaculationInput(new DateTime(2021, 1, 31), 0, 5, 0),
                            new EndDateCaculationInput(new DateTime(2021, 1, 31), 0, 8, 0),
                            new EndDateCaculationInput(new DateTime(2021, 1, 31), 0, 10, 0),
                            new EndDateCaculationInput(new DateTime(2021, 3, 31), 0, 1, 0),
                            new EndDateCaculationInput(new DateTime(2021, 3, 31), 0, 3, 0),
                            new EndDateCaculationInput(new DateTime(2021, 3, 31), 0, 6, 0),
                            new EndDateCaculationInput(new DateTime(2021, 3, 31), 0, 8, 0),
                            new EndDateCaculationInput(new DateTime(2021, 5, 31), 0, 1, 0),
                            new EndDateCaculationInput(new DateTime(2021, 5, 31), 0, 4, 0),
                            new EndDateCaculationInput(new DateTime(2021, 5, 31), 0, 6, 0),
                            new EndDateCaculationInput(new DateTime(2021, 5, 31), 0, 11, 0),
                            new EndDateCaculationInput(new DateTime(2021, 7, 31), 0, 2, 0),
                            new EndDateCaculationInput(new DateTime(2021, 7, 31), 0, 4, 0),
                            new EndDateCaculationInput(new DateTime(2021, 7, 31), 0, 9, 0),
                            new EndDateCaculationInput(new DateTime(2021, 7, 31), 0, 11, 0),
                            new EndDateCaculationInput(new DateTime(2021, 8, 31), 0, 1, 0),
                            new EndDateCaculationInput(new DateTime(2021, 8, 31), 0, 3, 0),
                            new EndDateCaculationInput(new DateTime(2021, 8, 31), 0, 8, 0),
                            new EndDateCaculationInput(new DateTime(2021, 8, 31), 0, 10, 0),
                            new EndDateCaculationInput(new DateTime(2021, 10, 31), 0, 1, 0),
                            new EndDateCaculationInput(new DateTime(2021, 10, 31), 0, 6, 0),
                            new EndDateCaculationInput(new DateTime(2021, 10, 31), 0, 8, 0),
                            new EndDateCaculationInput(new DateTime(2021, 10, 31), 0, 11, 0),
                            new EndDateCaculationInput(new DateTime(2021, 12, 31), 0, 4, 0),
                            new EndDateCaculationInput(new DateTime(2021, 12, 31), 0, 6, 0),
                            new EndDateCaculationInput(new DateTime(2021, 12, 31), 0, 9, 0),
                            new EndDateCaculationInput(new DateTime(2021, 12, 31), 0, 11, 0),
                            new EndDateCaculationInput(new DateTime(2021, 9, 30), 0, 1, 0),
                            new EndDateCaculationInput(new DateTime(2021, 9, 30), 0, 2, 0),
                            new EndDateCaculationInput(new DateTime(2021, 9, 30), 0, 3, 0),
                            new EndDateCaculationInput(new DateTime(2021, 9, 30), 0, 4, 0),
                            new EndDateCaculationInput(new DateTime(2021, 08, 31), 0, 6, 0),
                            new EndDateCaculationInput(new DateTime(2021, 08, 31), 0, 6, 1),

                        };
        }


        private void GetTotalTermCaculationResult()
        {
            StringBuilder sb = new StringBuilder();
            List<TotalTermsCaculationInput> tuples = txtInput.Text.jsonDeserializeFromString<List<TotalTermsCaculationInput>>();

            foreach (var item in tuples)
            {
                //TimeDifference x = DeleteExtraDayRuleApplied(GetPeriodDifferenceBetweenDates(item.StartDate, item.EndDate, true), item.StartDate, item.EndDate);
                TimeDifference old = GetPeriodDifferenceBetweenDates(item.StartDate, item.EndDate, true);
                TimeDifference y = DeleteExtraDayRuleApplied(old, item.StartDate, item.EndDate);
                //Run runX = new Run("Start Date:" + item.StartDate.ToString("yyyy-MM-dd") + " End Date:" + item.EndDate.ToString("yyyy-MM-dd") + " New Way TotalTermsCaculation: Y:" + x.Years + " M:" + x.Months + " D:" + x.Days + Environment.NewLine);
                //runX.Foreground = Brushes.Red;

                Run runY = new Run("Start Date:" + item.StartDate.ToString("yyyy-MM-dd") + " End Date:" + item.EndDate.ToString("yyyy-MM-dd") + " TotalTermsCaculation: Y:" + y.Years + " M:" + y.Months + " D:" + y.Days + Environment.NewLine);
                runY.Foreground = Brushes.Black;
                runY.FontSize = 16;

                txtResult.Inlines.Add(runY);
                //txtResult.Inlines.Add(runX);
            }

            Clipboard.SetText(txtResult.Text);
        }

        public static bool IsLastDayOfMonth(DateTime dt)
        {
            return dt.Day == DateTime.DaysInMonth(dt.Year, dt.Month);
        }

        public static TimeDifference DeleteExtraDayRuleApplied(TimeDifference td, DateTime startDate, DateTime endDate)
        {
            int yearCount = td.Years;
            int monthCount = td.Months;
            int dayCount = td.Days;
            //delete extra day rule
            if (IsLastDayOfMonth(startDate) && IsLastDayOfMonth(endDate) && dayCount == 1 && (yearCount > 0 || monthCount > 0))
            {
                dayCount = 0;
            }

            return new TimeDifference(yearCount, monthCount, dayCount, 0, 0, 0, 0, 0);
        }

        public static TimeDifference GetPeriodDifferenceBetweenDates(DateTime startDate, DateTime endDate, bool includeEndDate = false)
        {


            if (includeEndDate)
            {
                endDate = endDate.AddDays(1);
            }

            if (startDate <= endDate)
            {
                DateTime monthEndDate = startDate;
                DateTime workingMonthEndDate = startDate;
                DateTime dayEndDate;
                int yearCount = 0;
                int monthCount = 0;
                int dayCount = 0;

                int totalDays = endDate.Subtract(startDate).Days;



                ////do years
                //DateTime countStartDate = startDate;
                //DateTime tmpYearDate = countStartDate;
                //while (countStartDate < endDate)
                //{
                //    tmpYearDate = tmpYearDate.AddYears(1);
                //    if (tmpYearDate <= endDate)
                //    {
                //        yearCount += 1;
                //        countStartDate = countStartDate.AddYears(1);
                //    }
                //}

                ////do months
                //DateTime afterYearsStartDate = startDate.AddDays(yearDays);
                //DateTime tmpMonthEnd = afterYearsStartDate;
                //while (afterYearsStartDate < endDate)
                //{
                //    tmpMonthEnd = tmpMonthEnd.AddMonths(1);
                //    if (tmpMonthEnd <= endDate)
                //    {
                //        monthCount += 1;
                //        afterYearsStartDate = startDate.AddDays(DateTime.DaysInMonth(tmpMonthEnd.Year, tmpMonthEnd.Month));
                //    }
                //}


                //Do the months
                DateTime tmpMonthEnd = startDate;
                while (tmpMonthEnd < endDate)
                {
                    tmpMonthEnd = tmpMonthEnd.AddMonths(1);

                    // only add a month is past the end date
                    if (tmpMonthEnd <= endDate)
                    {
                        monthCount += 1;
                        monthEndDate = monthEndDate.AddDays(DateTime.DaysInMonth(monthEndDate.Year, monthEndDate.Month));
                        tmpMonthEnd = monthEndDate;
                    }
                }

                //while (totalDays >= 0)
                //{
                //    tmpMonthEnd = tmpMonthEnd.AddMonths(1);
                //    if (tmpMonthEnd <= endDate)
                //    {
                //        monthCount += 1;
                //        totalDays -= DateTime.DaysInMonth(monthEndDate.Year, monthEndDate.Month);
                //    }

                //}

                // if the above counter has passed the end date, deduct a month from the count
                //if (monthEndDate > endDate)
                //    monthEndDate = monthEndDate.AddMonths(-1);


                // need to reset the day of the month end date to day of the endDate
                // passed into the function. Found that Feb can stuff up the .AddMonths
                // calculation. 
                workingMonthEndDate = startDate.AddMonths(monthCount + 1);

                //try last time to see if there is a month gap between
                if (workingMonthEndDate <= endDate)
                {
                    monthCount += 1;
                }

                    dayEndDate = startDate.AddMonths(monthCount);

                // Do the days
                while (dayEndDate  < endDate)
                {
                    dayEndDate = dayEndDate.AddDays(1);
                    dayCount += 1;
                }

                //potential it could end up with more than a month days.
                //if (dayCount >= DateTime.DaysInMonth(workingMonthEndDate.Year, workingMonthEndDate.Month))
                //{
                //    monthCount += 1;
                //    dayCount -= DateTime.DaysInMonth(workingMonthEndDate.Year, workingMonthEndDate.Month);
                //}

                yearCount = monthCount / 12;
                monthCount = monthCount % 12;


                return new TimeDifference(yearCount, monthCount, dayCount, 0, 0, 0, 0, 0);
            }
            else
                return new TimeDifference(0, 0, 0, 0, 0, 0, 0, 0);
        }

        private object GetTotalTermCaculationInput()
        {
            return new List<TotalTermsCaculationInput>()
                    {
                        new TotalTermsCaculationInput(new DateTime(2022, 4, 30),  new DateTime(2023, 5, 28)),
                        new TotalTermsCaculationInput(new DateTime(2021, 12, 31),  new DateTime(2023, 1, 28)),
                        new TotalTermsCaculationInput(new DateTime(2022, 3, 31),  new DateTime(2023, 4, 28)),
                        new TotalTermsCaculationInput(new DateTime(2021, 9, 30),  new DateTime(2022, 10, 27)),
                        new TotalTermsCaculationInput(new DateTime(2021, 12, 31),  new DateTime(2023, 1, 27)),
                        new TotalTermsCaculationInput(new DateTime(2021, 12, 31),  new DateTime(2023, 1, 26)),
                        new TotalTermsCaculationInput(new DateTime(2021, 12, 31),  new DateTime(2022, 1, 28)),
                        new TotalTermsCaculationInput(new DateTime(2021, 12, 31),  new DateTime(2022, 1, 27)),
                         new TotalTermsCaculationInput(new DateTime(2020, 1, 27),  new DateTime(2020, 2, 28)),
                         new TotalTermsCaculationInput(new DateTime(2020, 1, 28),  new DateTime(2020, 2, 28)),
                         new TotalTermsCaculationInput(new DateTime(2020, 1, 29),  new DateTime(2020, 2, 28)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 30),  new DateTime(2020, 2, 28)),
                         new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 2, 28)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 2, 1)),
                         new TotalTermsCaculationInput(new DateTime(2020, 1, 27),  new DateTime(2020, 3, 28)),
                         new TotalTermsCaculationInput(new DateTime(2020, 1, 28),  new DateTime(2020, 3, 28)),
                         new TotalTermsCaculationInput(new DateTime(2020, 1, 29),  new DateTime(2020, 3, 28)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 30),  new DateTime(2020, 3, 28)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 3, 28)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 26),  new DateTime(2020, 3, 28)),
                         new TotalTermsCaculationInput(new DateTime(2020, 1, 27),  new DateTime(2020, 3, 29)),
                         new TotalTermsCaculationInput(new DateTime(2020, 1, 28),  new DateTime(2020, 3, 30)),
                         new TotalTermsCaculationInput(new DateTime(2020, 1, 29),  new DateTime(2020, 3, 31)),
                        new TotalTermsCaculationInput(new DateTime(2020, 2, 29),  new DateTime(2020, 3, 27)),
                        new TotalTermsCaculationInput(new DateTime(2020, 2, 29),  new DateTime(2020, 3, 28)),
                          new TotalTermsCaculationInput(new DateTime(2020, 2, 29),  new DateTime(2020, 3, 29)),
                        new TotalTermsCaculationInput(new DateTime(2021, 2, 28),  new DateTime(2022, 5, 31)),
                        new TotalTermsCaculationInput(new DateTime(2021, 2, 28),  new DateTime(2022, 3, 27)),
                        new TotalTermsCaculationInput(new DateTime(2021, 2, 28),  new DateTime(2022, 3, 28)),
                          new TotalTermsCaculationInput(new DateTime(2021, 2, 28),  new DateTime(2022, 3, 29)),
                        new TotalTermsCaculationInput(new DateTime(2020, 2, 29),  new DateTime(2020, 5, 31)),
                        new TotalTermsCaculationInput(new DateTime(2020, 3, 8),  new DateTime(2028, 6, 30)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 7, 31)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 30),  new DateTime(2020, 3, 30)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 8, 31)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 9, 20)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 9, 30)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 10, 30)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 10, 31)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 11, 1)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 11, 30)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 12, 30)),
                        new TotalTermsCaculationInput(new DateTime(2020, 1, 31),  new DateTime(2020, 12, 31)),
                        new TotalTermsCaculationInput(new DateTime(2021, 1, 1),  new DateTime(2021, 2, 28)),
                        new TotalTermsCaculationInput(new DateTime(2021, 1, 31),  new DateTime(2021, 2, 28)),
                        new TotalTermsCaculationInput(new DateTime(2021, 1, 5),  new DateTime(2099, 3, 28)),
                        new TotalTermsCaculationInput(new DateTime(2021, 1, 5),  new DateTime(2021, 1, 5)),
                    };
        }

        private void GetWhiteSpaceDelimitedResult()
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options); //replace multiple space with one

            List<string> test = txtInput.Text.jsonDeserializeFromString<List<string>>();

            foreach (var item in test)
            {
                string x = item.Substring(43);
                string xx = regex.Replace(x, " ");

                string[] narratives = xx.Trim().Split(' ');

                try
                {
                    txtResult.Text += "Parse narrative:" + item + Environment.NewLine
                        + " As  Old: "
                        + "[ErrorText]:" + item.Substring(0, 21)
                        + "[DDRequestDate]:" + item.Substring(22, 6)
                        + "[LodgementReference]:" + item.Substring(29, 13)
                        + "[TTPID]:" + item.Substring(43, 7)
                        + "[ReceiptNumber]:" + item.Substring(52, 8)
                        + "[Answer]:" + item.Substring(61, 6)
                        + "[AnswerCode]:" + item.Substring(68, 1)
                        + Environment.NewLine
                        + " As New: "
                        + "[ErrorText]:" + item.Substring(0, 21)
                        + "[DDRequestDate]:" + item.Substring(22, 6)
                        + "[LodgementReference]:" + item.Substring(29, 13)
                        + "[TTPID]:" + narratives[0]
                        + "[ReceiptNumber]:" + narratives[1]
                        + "[Answer]:" + narratives[2]
                        + "[AnswerCode]:" + narratives[3]
                        + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    txtResult.Text += "Parse narrative:" + item + Environment.NewLine
                        + " As  Old: Oops Exceptions:"
                        + ex.Message
                        + Environment.NewLine
                        + " As New: "
                        + "[ErrorText]:" + item.Substring(0, 21)
                        + "[DDRequestDate]:" + item.Substring(22, 6)
                        + "[LodgementReference]:" + item.Substring(29, 13)
                        + "[TTPID]:" + narratives[0]
                        + "[ReceiptNumber]:" + narratives[1]
                        + "[Answer]:" + narratives[2]
                        + "[AnswerCode]:" + narratives[3]
                        + Environment.NewLine;
                }


            }


        }

        private object GetWhiteSpaceDelimitedInput()
        {
            return new List<string>()
                    {
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 1693639  ACT54392 ANSWER 2",
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 1682315  ACT54383 ANSWER 6",
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 386459  ACT54245 ANSWER 6",
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 1702486  ACT54333 ANSWER 6",
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 1  ACT54408 ANSWER 6",
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 12  ACT54408 ANSWER 6",
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 123  ACT54408 ANSWER 6",
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 1234  ACT54408 ANSWER 6",
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 12345  ACT54408 ANSWER 6",
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 123456  ACT54408 ANSWER 6",
                        "DIRECT DEBIT RETURNED 020420 LODGEMENT REF 1234567  ACT54408 ANSWER 6"
                    };
        }

        private void GetRegexTestResult()
        {
            string regexExp = "[^0-9.]";

            decimal result;

            List<string> test = txtInput.Text.jsonDeserializeFromString<List<string>>();

            foreach (var item in test)
            {
                var x = Regex.Replace(item.Trim().Replace(System.Environment.NewLine, string.Empty), regexExp, "");
                if (Decimal.TryParse(x, out result))
                {
                    //Console.WriteLine(result + Environment.NewLine);
                    txtResult.Text += "Success to parse:" + item + " To: " + result + Environment.NewLine;
                }
                else
                {
                    //Console.WriteLine("Failed to parse:" + item + Environment.NewLine);
                    txtResult.Text += "Failed to parse:" + item + Environment.NewLine;
                }
            }
        }

        private object GetRegexTestInput()
        {
            return new List<string>()
            {
                        "159.5",
                        "159.50",
                        "1559.50",
                        "1,559.50",
                        "1 559.50",
                        "$1559.50",
                        "$1,559.50",
                        "TT1559.50",
                        "T.T1559.50"
                    };
        }



    }
}
