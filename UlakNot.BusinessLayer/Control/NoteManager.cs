using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlakNot.DataLayer.EntityFramework;
using UlakNot.Entity;

namespace UlakNot.BusinessLayer.Control
{
    public class NoteManager
    {
        private Repository<UnNotes> repo_note = new Repository<UnNotes>();

        public List<UnNotes> GetNotes()
        {
            return repo_note.List();
        }
    }
}