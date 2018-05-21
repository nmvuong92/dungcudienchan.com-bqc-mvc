using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels.BaoKim
{
    public class NapTheVM
    {
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "{0} Minimum 5 characters required")] //Minimum of 5 characters
        [StringLength(50, ErrorMessage = "Maximum {2} characters exceeded")] //Maximum of 50 characters
        [Display(Name = "Số seri thẻ cào")]
        public string Seri { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "{0} Minimum 5 characters required")] //Minimum of 5 characters
        [StringLength(50, ErrorMessage = "Maximum {2} characters exceeded")] //Maximum of 50 characters

        [Display(Name = "Mã thẻ cào")]
        public string pin { get; set; }


        [Required]
        public string nhamang { get; set; }

         [Required(ErrorMessage = "Vui lòng nhập {0}")]
        public string Captcha { get; set; }
    }
}