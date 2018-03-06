using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.DataLayer.EntityFramework
{
    public class Singleton
    {
        protected static DatabaseContext context;
        protected static object _contextLock = new object();

        private static void CreateContext()
        {
            if (context == null)
            {
                lock (_contextLock)
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }
        }

        protected Singleton()
        {
            CreateContext();
        }
    }
}