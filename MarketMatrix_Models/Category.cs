using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MarketMatrix_Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		[MaxLength(50)]
		[DisplayName("Category Name")]
		public string Name { get; set; }
		[DisplayName("Section Name")]
		[MaxLength(30)]
		public string SectionName { get; set; }

		[ValidateNever]
		public ICollection<Products> Products { get; set; }

	}
}
