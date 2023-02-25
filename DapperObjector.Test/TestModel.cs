using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperObjector
{
    [Table("States")]
    public class TestModel
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        //public string FloarsCount { get; set; }
        //public long Creator { get; set; }
        //public long HPF { get; set; }
        //public DateTime Created { get; set; }
        //public DateTime Modified { get; set; }
    }
}
