using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MoneyBox.Api.Models;

namespace MoneyBox.Api.Services
{
    public class MoneyBoxContext  : DbContext
    {
        public MoneyBoxContext(): base("MoneyBoxApiTestContext")
        {
            
        }
            
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void Seed (MoneyBoxContext context)
        {

            new List<Transaction>
            {
                new Transaction(){TransactionId = 1, Merchant = "PayPal", TransactionAmount = 100, CurrencyCode = "EUR", Description = "Something to eat", TransactionDate = DateTime.Today, ModifiedDate = DateTime.Today, CreatedDate = DateTime.Today},
                new Transaction(){TransactionId = 2, Merchant = "MasterCard", TransactionAmount = 256, CurrencyCode = "GPB", Description = "A house", TransactionDate = DateTime.Today, ModifiedDate = DateTime.Today, CreatedDate = DateTime.Today}
            }.ForEach(i => context.Transactions.Add(i));
       

            // Normal seeding goes here
            context.SaveChanges ();
        }

        public class RecreateDatabase : DropCreateDatabaseAlways<MoneyBoxContext>
        {
            protected override void Seed(MoneyBoxContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        public static void MoneyBoxDatabaseInit()
        {
        
            Database.SetInitializer<MoneyBoxContext>(new RecreateDatabase());
       
        }
    }
}