using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarketMatrix_Models
{
	public class Products
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[DisplayName("Product Name")]
		public string Name { get; set; }

		[Required]
		[Range(1,100000)]
		[DisplayName("Product Price")]
		public int Price { get; set; }

		[Required]
		[Range(1, 1000)]
		[DisplayName("Product Quantity")]
		public int Quantity { get; set; }

		[Required]
		[DisplayName("Product Color")]
		public string Color { get; set; }

		[Required]
		[DisplayName("Product Description")]
		[MaxLength(300)]
		public string Description { get; set; }

		[DisplayName("Product Image")]
		[ValidateNever]
		public string ImageUrl { get; set; }

		[DisplayName("Category")]
		[ValidateNever]
		public int CategoryId { get; set; }
		[ValidateNever]
		[ForeignKey("CategoryId")]
		[JsonIgnore]
		public Category Category { get; set; }

		[ValidateNever]
		[JsonIgnore]
		public MyCart MyCart { get; set; }

	}
}
