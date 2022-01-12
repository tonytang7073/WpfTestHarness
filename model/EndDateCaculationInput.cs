using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTestHarness.helpers;

namespace WpfTestHarness.model
{
    [Serializable]
    public class EndDateCaculationInput
    {

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime StartDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double Days { get; set; }

        public EndDateCaculationInput(DateTime startDate, int year, int month, double days)
        {
            StartDate = startDate;
            Year = year;
            Month = month;
            Days = days;
        }

    }
}
