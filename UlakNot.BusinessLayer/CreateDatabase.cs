using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.BusinessLayer
{
    public class CreateDatabase
    {
        public CreateDatabase()
        {
            DataLayer.DatabaseContext db= new DataLayer.DatabaseContext();
            db.Database.CreateIfNotExists();

        }
    }
}
