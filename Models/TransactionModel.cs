using System.ComponentModel.DataAnnotations;

namespace ExpenseRecorder.Models
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }

        public string? Note { get; set; } // this string is nullable

        public DateTime Date { get; set; } = DateTime.Now;

    }
}
