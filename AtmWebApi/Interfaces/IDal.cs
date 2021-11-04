using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmWebApi.Interfaces
{
    interface IDal
    {
        public T get<T>(string sql, object param) where T : class;
        public bool insert<T>(string sql, object param) where T : class;
        public bool update<T>(string sql, object param) where T : class;
        public bool delete(string sql, object param);
    }
}
