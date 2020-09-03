namespace Quote.Service.API.Data.Database
{
    using Microsoft.EntityFrameworkCore;
    using Quote.Service.API.Data.Database.Configuration;
    using Quote.Service.API.Domain.Entities;
    using System;

    ///<Summary>
    /// Quote Db Context
    ///</Summary>
    public class QuoteDbContext : DbContext
    {
        ///<Summary>
        /// QuoteDbContext constructor
        ///</Summary>
        public QuoteDbContext() { }

        ///<Summary>
        /// QuoteDbContext constructor with DbContextOptions
        ///</Summary>
        public QuoteDbContext(DbContextOptions<QuoteDbContext> contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }

        ///<Summary>
        /// Quote Entitiy
        ///</Summary>
        public DbSet<Quote> QuoteInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuoteConfiguration());

            modelBuilder.Entity<Quote>().HasData(
                new Quote
                {
                    Id = Guid.NewGuid(),
                    ClientName = "ClientName",
                    DateofBirth = new DateTime(1992, 09, 09),
                    ClientSex = 1,
                    Email = "Test@test.com",
                    MobileNumber = "1234567880",
                    QuoteDate = new DateTime(2020, 08, 08),
                    RetirementAge = 62,
                    InvestmentAmount = 100000,
                    MaturityAmount = 1500,
                    PensionPlan = 1
                }
            );
        }
    }
}

