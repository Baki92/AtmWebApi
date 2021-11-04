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
            return this.banknotes;
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
