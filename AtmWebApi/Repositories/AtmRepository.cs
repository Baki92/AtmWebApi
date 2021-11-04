using System;
using System.Collections.Generic;
using System.Linq;
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
        public AtmRepository()
        {
            this.dal = new Dal();
        }
        public Banknotes Withdrawal(int amount)
        {
            string sql = "Select * From  Banknote";
            return this.dal.get<Banknotes>(sql, null);
        }
        public int Deposit(Banknotes bankNotes)
        {
            return 0;
        }
    }
}
