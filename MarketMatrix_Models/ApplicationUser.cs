using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MarketMatrix_Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? Name { get; set; }

		[AllowNull]
		public int? AddressId { get; set; }

		[ForeignKey("AddressId")]
		public Address? Address { get; set; }
	}
}
