#nullable disable

using DataAccess.Entities.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class DirectorModel : Record
	{
		#region Entity Properties
		[Required(ErrorMessage = "{0} is required!")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
		public string Name { get; set; }

		[Required(ErrorMessage = "{0} is required!")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
		public string Surname { get; set; }

		[DisplayName("Birth Date")]
		public DateTime? BirthDate { get; set; }

		[DisplayName("Retired")]
		public bool IsRetired { get; set; }
		#endregion

		#region Other Properties
		[DisplayName("Full Name")]
        public string FullNameOutput { get; set; }

		[DisplayName("Birth Date")]
		public string BirthDateOutput { get; set; }

		[DisplayName("Retired")]
		public string IsRetiredOutput { get; set; }

        [DisplayName("Movies")]
        public List<MovieModel> MoviesOutput { get; set; }
        #endregion
    }
}
