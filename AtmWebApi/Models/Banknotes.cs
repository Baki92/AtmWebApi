using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AtmWebApi.Models
{
    public class Banknotes
    {
        [JsonPropertyName("1000")]
        public int oneThousand { get; set; }
        [JsonPropertyName("2000")]
        public int twoThousand { get; set; }
        [JsonPropertyName("5000")]
        public int fiveThouand { get; set; }
        [JsonPropertyName("10000")]
        public int tenThousand { get; set; }
        [JsonPropertyName("20000")]
        public int twentyThousand { get; set; }
    }
}
