using Microsoft.EntityFrameworkCore;

namespace ExpenseRecorder.Models
{
    public class WebDbContext:DbContext
    {
        public WebDbContext(DbContextOptions options):base(options)
        {

        }
        // those model will be conver to db tables with that name
        public DbSet<TransactionModel> NetTransactions { get; set; }
        public DbSet<CategoryModel> NetCategories { get; set; }
    }
}
