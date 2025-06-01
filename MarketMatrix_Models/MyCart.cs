using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketMatrix_Models
{
    public class MyCart
    {
        [Key]
        public int Id { get; set; }

        [ValidateNever]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [ValidateNever]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Products Products { get; set; }

        public int Quantity { get; set; }

    }
}
