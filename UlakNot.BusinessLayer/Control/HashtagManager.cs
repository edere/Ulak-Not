using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlakNot.DataLayer.EntityFramework;
using UlakNot.Entity;

namespace UlakNot.BusinessLayer.Control
{
    public class HashtagManager
    {
        private Repository<UnHashtags> repo_hashtags = new Repository<UnHashtags>();

        public List<UnHashtags> GetHashtags()
        {
            return repo_hashtags.List();
        }
    }
}