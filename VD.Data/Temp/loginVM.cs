using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VD.Data.Base;
using VD.Data.DataTrans;
using VD.Data.Entity;

namespace VD.Data.Temp
{
    public class loginVM
    {
        public int ID { get; set; }
        //main
        [Required(ErrorMessage = "* {0}")]
        public string Username { get; set; }
        [Required(ErrorMessage = "* {0}")]
        public string Password { get; set; }

        public string FullName { get; set; }
        public string RoleName { get; set; }

        public bool RememberMe { get; set; }

        public bool IsVip1 { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
     
        //ne
      
        public int RoleId { get; set; }
        public string RoleIdStr
        {
            get
            {
                return this.RoleId.ToString();
            }
        }
     
 
        public int[] ne_QuyenCoSoArrInt { get; set; }
        public string[] ne_QuyenCoSoArrIntArrStr
        {
            get
            {
                return Array.ConvertAll(this.ne_QuyenCoSoArrInt, x => x.ToString());
            }
        }
      

        //
        public int[] ne_quyenInt { get; set; }
        public string[] ne_quyenIntArrStr
        {
            get
            {
                return Array.ConvertAll(this.ne_quyenInt, x => x.ToString());
            }
        }
      
      
        //directive
        public string ReturnUrl { get; set; }
        public string role { get; set; } //homepage or admin

  
        //default constructor
        public loginVM()
        {
            
        }
        public loginVM(User log)
        {
            this.ID = log.Id;
            this.Username = log.Username;
            this.RoleId = log.RoleId;
            this.RoleName = log.Role.Name;
            this.Address = log.Address;
            this.Phone = log.Phone;
            this.FullName = log.Fullname;
            this.ne_quyenInt = log.Role.Permissions.Select(s => s.Id).ToArray();
            this.IsVip1 = log.IsVip1;
        }
    }
    public class CustomerLoginVM
    {
        public int ID { get; set; }
        //main
        [Required(ErrorMessage = "* {0}")]
        public string Username { get; set; }
        [MuiMui("email")]
        [Required(ErrorMessage = "* {0}")]
        public string Password { get; set; }

        public string FullName { get; set; }
        public string RoleName { get; set; }
        public bool IsWalletNotification { get; set; }

        //ne
        /// <summary>
        /// 1: candidates
        /// 2: employee
        /// </summary>
        public int RoleId { get; set; }
        public bool IsUseHuntingService { get; set; }

        public bool ValidIsUseHuntingService
        {
            get
            {
                return RoleId == 2 && IsUseHuntingService;
            }
        }
        /// <summary>
        /// 1: candidates
        /// 2: employee
        /// </summary>
        public int TypeLogin { get; set; }

        //directive
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
        public int IDRememberMe { get; set; }
        public bool isLogged { get; set; }
        public CustomerLoginVM()
        {
        }
    
    }
}
