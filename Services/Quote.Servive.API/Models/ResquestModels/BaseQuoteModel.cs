namespace Quote.Service.API.Models.ResquestModels
{
    using Quote.Service.API.Infrastructure.Helpers;
    using Quote.Service.API.Models.Enum;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class BaseQuoteModel
    {
        [Required]
        public string ClientName { get; set; }

        [Required]
        public ClientSex ClientSex { get; set; }

        [Required]
        public DateTime DateofBirth { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public decimal InvestmentAmount { get; set; }

        [Required]
        public int RetirementAge { get; set; }

        [Required]
        public PensionPlan PensionPlan { get; set; }

        [JsonIgnore]
        public decimal MaturityAmount => MaturityAmountCalculation.MaturityAmountCalc(this.PensionPlan, this.InvestmentAmount, this.DateofBirth, this.RetirementAge);
    }
}
