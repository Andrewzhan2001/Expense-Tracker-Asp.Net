using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseRecorder.Models
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Note { get; set; } // this string is nullable

        public DateTime Date { get; set; } = DateTime.Now;

        [Range(1,int.MaxValue, ErrorMessage ="Please select a category for this transaction")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual CategoryModel? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0")]
        public int Amount { get; set; }

        [NotMapped]
        public string? IconedTitle
        {
            get
            {
                return Category == null  ?  "" : Category.Icon + " " + Category.Title;
            }
        }

        [NotMapped]
        public string? FormatAmount
        {
            get
            {
                // format to c0
                return ((Category == null || Category.TransactionType == "Expense") ? "- " : "+ ") + Amount.ToString("C0");
            }
        }
    }
}
