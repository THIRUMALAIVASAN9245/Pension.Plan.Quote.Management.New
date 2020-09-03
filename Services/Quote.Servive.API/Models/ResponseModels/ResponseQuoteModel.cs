namespace Quote.Service.API.Models.ResponseModels
{
    using Quote.Service.API.Models.Enum;
    using System;

    public class ResponseQuoteModel
    {
        public Guid Id { get; set; }

        public string ClientName { get; set; }

        public ClientSex ClientSex { get; set; }

        public DateTime? DateofBirth { get; set; }

        public DateTime? QuoteDate { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public decimal InvestmentAmount { get; set; }

        public int RetirementAge { get; set; }

        public PensionPlan PensionPlan { get; set; }

        public decimal MaturityAmount { get; set; }
    }
}
