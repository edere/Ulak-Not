using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.BusinessLayer.Results
{
    public class ErrorResult<T> where T : class
    {
        public List<string> Error { get; set; }
        public T Result { get; set; }

        public ErrorResult()
        {
            Error = new List<string>();
        }
    }
}