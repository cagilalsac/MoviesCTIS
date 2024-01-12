#nullable disable

using DataAccess.Entities.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class GenreModel : Record
    {
        #region Entity Properties
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(75, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }
        #endregion

        #region Other Properties
        [DisplayName("Movie Count")]
        public int MoviesCountOutput { get; set; }

        [DisplayName("Movies")]
        public string MoviesOutput { get; set; }
        #endregion
    }
}
