using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestHarness.helpers
{
    public static class jsonSerializeExtension
    {

        public static string jsonSerializeToString(this object objectInstance)
        {
            string output = JsonConvert.SerializeObject(objectInstance, Formatting.Indented);

            return output;
        }

        public static T jsonDeserializeFromString<T>(this string objectData)
        {
            return JsonConvert.DeserializeObject<T>(objectData);
        }

    }
}
