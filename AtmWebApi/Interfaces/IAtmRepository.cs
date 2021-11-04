using AtmWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmWebApi.Interfaces
{
    interface IAtmRepository
    {
        public Banknotes withdrawal(int amount);
        public int deposit(Banknotes bankNotes);
    }
}
