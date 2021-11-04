using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AtmWebApi.DB;
using AtmWebApi.Interfaces;
using AtmWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtmWebApi.Repositories
{
    public class AtmRepository:IAtmRepository
    {
        private Dal dal = null;
        private Banknotes banknotes = null;
        public AtmRepository()
        {
            this.dal = new Dal();
            this.updateBanknotes();
        }
        public Banknotes withdrawal(int amount)
        {
            Banknotes bankNotesOut = new Banknotes();
            Dictionary<int, int> banknotesKeyValue = this.banknotes.getInKeyValue();
            Dictionary<int, int> banknotesKeyValueOut = bankNotesOut.getInKeyValue();
            int totalAmount = this.banknotes.getTotalAmount();
            if(totalAmount>=amount)
            {
                foreach (var item in banknotesKeyValue)
                {
                    while (item.Key <= amount && banknotesKeyValue[item.Key] > 0 && amount != 0)
                    {
                        amount -= item.Key;
                        banknotesKeyValueOut[item.Key]++;
                        banknotesKeyValue[item.Key]--;
                    }
                }
            }
            if(amount==0)
            {

                bankNotesOut.setFromKeyValue(banknotesKeyValueOut);
                this.banknotes.setFromKeyValue(banknotesKeyValue);
                string sql = "Update Banknote set oneThousand=@oneThousand," +
                                             "twoThousand=@twoThousand," +
                                              "fiveThousand=@fiveThousand," +
                                              "tenThousand=@tenThousand," +
                                              "twentyThousand=@twentyThousand";
                this.dal.update<Banknotes>(sql, this.banknotes);
                this.updateBanknotes();

                return bankNotesOut;
            }
            else
            {
                return null;
            }
          
        }
        public int deposit(Banknotes newBankNotes)
        {
            string sql = "Update Banknote set oneThousand=@oneThousand+oneThousand," +
                                              "twoThousand=@twoThousand+twoThousand," +
                                               "fiveThousand=@fiveThousand+fiveThousand," +
                                               "tenThousand=@tenThousand+tenThousand," +
                                               "twentyThousand=@twentyThousand+twentyThousand";
            this.dal.update<Banknotes>(sql, newBankNotes);
            this.updateBanknotes();

            return this.banknotes.getTotalAmount();
        }
        private void updateBanknotes()
        {
            string sql = "Select * From  Banknote";
            this.banknotes=this.dal.get<Banknotes>(sql, null);
        }
    }
}
