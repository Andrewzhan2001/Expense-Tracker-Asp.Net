using ExpenseRecorder.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseRecorder.Models
{
    public class CategoryModel
    {
        [Key] // we want this to be the primary key
        public int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Icon { get; set; } = "";
        [Column(TypeName = "nvarchar(50)")]
        public string TransactionType { get; set; } = "Expense";


    }
}
