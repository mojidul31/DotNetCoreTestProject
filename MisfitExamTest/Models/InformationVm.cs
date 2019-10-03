using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MisfitExamTest.Models
{
    public class InformationVm
    {
        [Column(TypeName = "bigint")]
        public int FirstNo { get; set; }
        [Column(TypeName = "bigint")]
        public int SecondNo { get; set; }
        public string UserName { get; set; }
    }
}
