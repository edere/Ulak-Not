using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlakNot.DataLayer.EntityFramework;
using UlakNot.Entity;

namespace UlakNot.BusinessLayer.Control
{
    public class CategoryManager
    {
        private Repository<UnCategories> repo_cat = new Repository<UnCategories>();

        public List<UnCategories> GetCategories()
        {
            return repo_cat.List();
        }
    }
}