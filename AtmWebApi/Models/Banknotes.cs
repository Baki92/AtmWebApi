using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AtmWebApi.Models
{
    public class Banknotes
    {
        [JsonPropertyName("1000")]
        public int oneThousand { get; set; }
        [JsonPropertyName("2000")]
        public int twoThousand { get; set; }
        [JsonPropertyName("5000")]
        public int fiveThousand { get; set; }
        [JsonPropertyName("10000")]
        public int tenThousand { get; set; }
        [JsonPropertyName("20000")]
        public int twentyThousand { get; set; }

        public int getTotalAmount()
        {
            Dictionary<int, int> banknotesKeyValue = this.getInKeyValue();
            int totalAmount = 0;
            foreach (var item in banknotesKeyValue)
            {
                totalAmount += item.Key * item.Value;
            }
            return totalAmount;
        }
        public Dictionary<int,int> getInKeyValue()
        {
            return JsonSerializer.Deserialize<Dictionary<int, int>>(JsonSerializer.Serialize(this));
        }
        public Banknotes setFromKeyValue(Dictionary<int,int> values)
        {
            return JsonSerializer.Deserialize<Banknotes>(JsonSerializer.Serialize(values));
        }
        public bool validate()
        {
            if(this.oneThousand==0 && this.twoThousand==0 && this.fiveThousand == 0 && this.tenThousand==0 && this.twentyThousand==0)
            {
                return false;
            }
            else if(this.oneThousand < 0 || this.twoThousand < 0 || this.fiveThousand < 0 || this.tenThousand < 0 || this.twentyThousand < 0)
            {
                return false;
            }
            return true;
        }

    }
}
