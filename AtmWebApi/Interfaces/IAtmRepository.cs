using AtmWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmWebApi.Interfaces
{
    interface IAtmRepository
    {
        public Banknotes Withdrawal(int amount);
        public Banknotes Deposit(Banknotes bankNotes);
    }
}
