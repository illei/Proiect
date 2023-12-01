using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Mark
    {
        public int Value { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Subject Subject { get; set; }

        public int StudentId { get; set; }
       public int SubjectId { get; set; }
    }
}
