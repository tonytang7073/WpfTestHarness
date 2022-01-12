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
    public class TotalTermsCaculationInput
    {
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime StartDate { get; set; }

        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime EndDate { get; set; }

        public TotalTermsCaculationInput(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
