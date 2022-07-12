﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual CategoryModel Category { get; set; }

        public int Amount { get; set; }

    }
}
