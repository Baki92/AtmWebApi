using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtmWebApi.Interfaces;
using AtmWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtmWebApi.Repositories
{
    public class AtmRepository:IAtmRepository
    {
        public Banknotes Withdrawal(int amount)
        {
            return new Banknotes();
        }
        public int Deposit(Banknotes bankNotes)
        {
            return 0;
        }
    }
}
