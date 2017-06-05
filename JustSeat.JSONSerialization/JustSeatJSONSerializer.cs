using JustSeat.Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustSeat.Serialization;
using Newtonsoft.Json;

namespace JustSeat.JSONSerialization
{
    public class JustSeatJSONSerializer : IJustSeatDataImporter, IJustSeatDataExporter
    {
        public string Export(Serialization.JustSeat justSeatModel)
        {
            var json = JsonConvert.SerializeObject(justSeatModel);

            return json;
        }

        public Serialization.JustSeat Import(string importText)
        {
            var model = JsonConvert.DeserializeObject<Serialization.JustSeat>(importText);

            return model;
        }
    }
}
