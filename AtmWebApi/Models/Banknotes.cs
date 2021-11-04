using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AtmWebApi.Models
{
    public struct BanknotesTypes
    {
        public const int oneThousand = 1000;
        public const int twoThousand = 2000;
        public const int fiveThousand = 5000;
        public const int tenThousand = 10000;
        public const int twentyThousand = 20000;
    }
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
            return (BanknotesTypes.oneThousand * this.oneThousand) +
                   (BanknotesTypes.twoThousand * this.twoThousand) +
                   (BanknotesTypes.fiveThousand * this.fiveThousand) +
                   (BanknotesTypes.tenThousand * this.tenThousand) +
                   (BanknotesTypes.twentyThousand * this.twentyThousand);
        }
        public Dictionary<int,int> getInKeyValueFormat()
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            result.Add(BanknotesTypes.twentyThousand, this.twentyThousand);
            result.Add(BanknotesTypes.tenThousand, this.tenThousand);
            result.Add(BanknotesTypes.fiveThousand, this.fiveThousand);
            result.Add(BanknotesTypes.twoThousand, this.twoThousand);
            result.Add(BanknotesTypes.oneThousand, this.oneThousand);

            return result;
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
