using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SM.Radio.Entity
{
    public class BaseEntity
    {
        [Key]
        [Display(Name="ID")]
        public String ID { get; set; }
    }
}
