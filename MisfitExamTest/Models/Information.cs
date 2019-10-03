using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MisfitExamTest.Models
{
    [Table("Informations")]
    public class Information
    {
        [Key]
        public int InfoId { get; set; }
        [Column(TypeName = "bigint")]
        public int FirstNo { get; set; }
        [Column(TypeName = "bigint")]
        public int SecondNo { get; set; }
        [Column(TypeName = "bigint")]
        public int SumOfTwo { get; set; }
        public DateTime? CreateDate { get; set; }
        [ForeignKey("UserInfo")]
        public int UserRefId { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
