using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IudexBoost.Models.Classes
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }//Price
        public string PaymentMethod { get; set; }
        public string Status { get; set; }//ACTIVE,CANCELLED,COMPLETED
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
    }

}
