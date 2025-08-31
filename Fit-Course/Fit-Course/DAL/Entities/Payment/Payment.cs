
using DAL.Enum.PaymentType;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Payment
    {
   
        [Key]
        public int PaymentID { get; private set; }
        [Required(ErrorMessage ="The Payment Method is Required.")]
        public PaymentType PaymentMethod { get; set; }
        [Required(ErrorMessage = "The Amount is Required.")]
        [Range(0,double.MaxValue,ErrorMessage ="The Amount must be more than or equal 0.")]
        public double Amount { get; set; }
        [Required(ErrorMessage ="The Date is Required.")]
      
        public DateTime Date { get; set; }
        [Required(ErrorMessage ="The Status is Required.")]
        public string Status { get; set; }
    
    }
}
