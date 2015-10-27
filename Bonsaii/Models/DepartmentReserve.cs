namespace Bonsaii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepartmentReserves")]
    public partial class DepartmentReserve
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "字段名")]
        public string RecordName { get; set; }
      
        [StringLength(50)]
        [Display(Name = "内容标识")]
        public int ContextName { get; set; }

       // [Required]
        [StringLength(50)]
        [Display(Name = "内容")]
        public string Context { get; set; }
    }
}
