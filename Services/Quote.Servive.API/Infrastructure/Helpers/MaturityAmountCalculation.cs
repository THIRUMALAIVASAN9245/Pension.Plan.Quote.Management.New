namespace Quote.Service.API.Infrastructure.Helpers
{
    using Quote.Service.API.Models.Enum;
    using System;

    public static class MaturityAmountCalculation
    {
        public static decimal MaturityAmountCalc(PensionPlan pensionPlan, decimal investmentAmount, DateTime dateofBirth, int retirementAge)
        {
            var factor = GetFactor(pensionPlan);
            var currentAge = GetCurrentAge(dateofBirth);

            var investmentAmt = investmentAmount * (1 + factor);
            var age = retirementAge - currentAge;

            var maturityAmount = (investmentAmt * age) / 100;
            return maturityAmount;
        }

        private static decimal GetFactor(PensionPlan pensionPlan)
        {
            double factorValue = 0;
            switch (pensionPlan)
            {
                case PensionPlan.PensionSilver:
                    factorValue = AlertMessages.PensionSilverFactor;
                    break;
                case PensionPlan.PensionGold:
                    factorValue = AlertMessages.PensionGoldFactor;
                    break;
                case PensionPlan.PensionPlatinum:
                    factorValue = AlertMessages.PensionPlatinumFactor;
                    break;
            }

            return (decimal)factorValue;
        }

        private static int GetCurrentAge(DateTime dateofBirth)
        {
            DateTime reference = DateTime.Today;
            int age = reference.Year - dateofBirth.Year;
            if (reference < dateofBirth.AddYears(age)) age--;

            return age;
        }
    }
}