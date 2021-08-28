using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTuts.Repositories
{
    public abstract class Repository
    {
        public Repository()
        {
            ConnectionStringSettingsCollection settings =
             ConfigurationManager.ConnectionStrings;
            connString = settings["MySql"].ConnectionString;

        }

        protected string connString;
    }
}
