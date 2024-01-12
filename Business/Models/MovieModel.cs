#nullable disable

using DataAccess.Entities.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class MovieModel : Record
	{
		#region Entity Properties
		[Required(ErrorMessage = "{0} is required!")]
		[StringLength(150, ErrorMessage = "{0} must be maximum {1} characters!")]
		public string Name { get; set; }

		public short? Year { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        public double? Revenue { get; set; }

		[DisplayName("Director")]
		public int? DirectorId { get; set; }
		#endregion

		#region Other Properties
		[DisplayName("Revenue")]
		public string RevenueOutput { get; set; }

		[DisplayName("Director")]
		public string DirectorOutput { get; set; }

        [DisplayName("Genres")]
        public string GenresOutput { get; set; }

        [DisplayName("Genres")]
        public List<int> GenreIdsInput { get; set; }
        #endregion
    }
}
