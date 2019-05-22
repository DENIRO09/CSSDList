using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSSDROPLIST.Models
{
    public class VM
    {
        [Key]
        public int W1 { get; set; }

        public int pID { get; set; }

        public int bID { get; set; }
    }
}