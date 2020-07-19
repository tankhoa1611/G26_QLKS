using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationQLKS.Models
{
    public class Parameter
    {
        [Key]
        [DisplayName("Mã tham số")]
        public int ParameterId { get; set; }
        [DisplayName("Tên tham số")]
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string ParameterName { get; set; }                
        [DisplayName("Giá trị")]
        public double ParameterValue { get; set; }
        [StringLength(500, ErrorMessage = "{0} tối đa {1} kí tự.")]
        [DisplayName("Ghi chú")]
        public string Note { get; set; }
    }
}