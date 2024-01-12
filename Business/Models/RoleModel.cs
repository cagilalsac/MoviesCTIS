#nullable disable

using DataAccess.Entities.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class RoleModel : Record
    {
        #region Entity Properties
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(5, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Role Name")]
        public string Name { get; set; }
        #endregion

        #region Other Properties
        [DisplayName("Users")]
        public List<UserModel> UsersOutput { get; set; }
        #endregion
    }
}