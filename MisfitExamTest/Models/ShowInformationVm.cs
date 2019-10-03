using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MisfitExamTest.Models
{
    public class ShowInformationVm
    {
        public int InfoId { get; set; }
        [Column(TypeName = "bigint")]
        public int FirstNo { get; set; }
        [Column(TypeName = "bigint")]
        public int SecondNo { get; set; }
        [Column(TypeName = "bigint")]
        public int SumOfTwo { get; set; }
        public string CreateDate { get; set; }
        public string UserName { get; set; }
    }
}
