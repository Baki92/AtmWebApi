using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AtmWebApi.DB;
using AtmWebApi.Interfaces;
using AtmWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
namespace AtmWebApi.Repositories
{
    public class AtmRepository:IAtmRepository
    {
        private Dal dal = null;
        private Banknotes banknotes = null;
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
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
            this.logger.Info("Find output banknotes");
            if (totalAmount>=amount)
            {
                foreach (var item in banknotesKeyValue.Reverse())
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
                this.logger.Info("Finded output banknotes");
                bankNotesOut =bankNotesOut.setFromKeyValue(banknotesKeyValueOut);
                this.banknotes=this.banknotes.setFromKeyValue(banknotesKeyValue);

                this.logger.Info("Update Banknote db table");
                string sql = "Update Banknote set oneThousand=@oneThousand," +
                                             "twoThousand=@twoThousand," +
                                              "fiveThousand=@fiveThousand," +
                                              "tenThousand=@tenThousand," +
                                              "twentyThousand=@twentyThousand";
                this.dal.update<Banknotes>(sql, this.banknotes);
                this.logger.Info("Successfully updated Banknote db table");
                this.updateBanknotes();

                this.logger.Info("Return output Banknotes");
                return bankNotesOut;
            }
            else
            {
                this.logger.Warn("Output Banknotes not found");
                return null;
            }
          
        }
        public int deposit(Banknotes newBankNotes)
        {
            this.logger.Info("Update Banknote db table");
            string sql = "Update Banknote set oneThousand=@oneThousand+oneThousand," +
                                              "twoThousand=@twoThousand+twoThousand," +
                                               "fiveThousand=@fiveThousand+fiveThousand," +
                                               "tenThousand=@tenThousand+tenThousand," +
                                               "twentyThousand=@twentyThousand+twentyThousand";
            this.dal.update<Banknotes>(sql, newBankNotes);
            this.logger.Info("Successfully updated Banknote db table");
            this.updateBanknotes();
            return this.banknotes.getTotalAmount();
        }
        private void updateBanknotes()
        {
            this.logger.Info("Update repository local banknote object");
            string sql = "Select * From  Banknote";
            this.banknotes=this.dal.get<Banknotes>(sql, null);
            if(this.banknotes==null)
            {
                this.banknotes = new Banknotes();
                sql = "Insert into Banknote(oneThousand,twoThousand,fiveThousand,tenThousand,twentyThousand) " +
                                           "Values(@oneThousand,@twoThousand,@fiveThousand,@tenThousand,@twentyThousand)";
                this.dal.insert<Banknotes>(sql, this.banknotes);
            }
            this.logger.Info("Successfully updated local banknote object");
        }
    }
}
