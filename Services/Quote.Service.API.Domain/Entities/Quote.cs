namespace Quote.Service.API.Domain.Entities
{
    using System;

    public partial class Quote
    {
        public Guid Id { get; set; }

        public string ClientName { get; set; }

        public int ClientSex { get; set; }

        public DateTime? DateofBirth { get; set; }

        public DateTime? QuoteDate { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public decimal InvestmentAmount { get; set; }

        public int RetirementAge { get; set; }

        public int PensionPlan { get; set; }

        public decimal MaturityAmount { get; set; }
    }
}
