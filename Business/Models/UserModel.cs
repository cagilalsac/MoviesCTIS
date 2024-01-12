#nullable disable

using DataAccess.Entities.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserModel : Record
    {
        #region Entity Properties
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(10, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
		public string Password { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Role")]
        public int? RoleId { get; set; }
        #endregion

        #region Other Properties
        [DisplayName("Password")]
        public string PasswordOutput { get; set; }

        [DisplayName("Active")]
        public string IsActiveOutput { get; set; }

        [DisplayName("Role")]
        public RoleModel RoleOutput { get; set; }
        #endregion
    }
}
