namespace Quote.Service.API.Infrastructure.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Quote.Service.API.Models.Enum;
    using Quote.Service.API.Models.ResquestModels;

    [ExcludeFromCodeCoverage]
    public static class ValidatorExtension
    {
        public static bool ValidateInvestmentAmount(BaseQuoteModel baseQuoteModel, decimal InvestmentAmount)
        {
            bool pensionPlanFlag = false;
            switch (baseQuoteModel.PensionPlan)
            {
                case PensionPlan.PensionSilver:
                    pensionPlanFlag = InvestmentAmount >= AlertMessages.PensionSilverMin && baseQuoteModel.RetirementAge >= 65;
                    break;
                case PensionPlan.PensionGold:
                    pensionPlanFlag = InvestmentAmount >= AlertMessages.PensionGoldMin && baseQuoteModel.RetirementAge >= 63;
                    break;
                case PensionPlan.PensionPlatinum:
                    pensionPlanFlag = InvestmentAmount >= AlertMessages.PensionPlatinumMin && baseQuoteModel.RetirementAge >= 60;
                    break;
            }

            return pensionPlanFlag;
        }

        public static string WithMessageInvestmentAmount(PensionPlan pensionPlan)
        {
            var pensionPlanMessage = string.Empty;
            switch (pensionPlan)
            {
                case PensionPlan.PensionSilver:
                    pensionPlanMessage = AlertMessages.InvestmentAmountPensionSilver;
                    break;
                case PensionPlan.PensionGold:
                    pensionPlanMessage = AlertMessages.InvestmentAmountPensionGold;
                    break;
                case PensionPlan.PensionPlatinum:
                    pensionPlanMessage = AlertMessages.InvestmentAmountPensionPlatinum;
                    break;
            }

            return pensionPlanMessage;
        }

        public static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
