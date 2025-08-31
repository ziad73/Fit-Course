

using DAL.Enum.PaymentType;
using System.ComponentModel.DataAnnotations;

namespace Resturant_DAL.Entities
{
    public class Payment
    {
   
        [Key]
        public int PaymentID { get; private set; }
        public PaymentType PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    
    }
}
