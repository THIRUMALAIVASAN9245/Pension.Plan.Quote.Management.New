namespace Quote.Service.API.Infrastructure.Helpers
{
    public static class AlertMessages
    {
        public const string ClientIdEmpty = "The client id should not be empty";

        public const string ClientNameNull = "The client name should not be empty";

        public const string ClientNameMaximumLength = "The client name should be lessthan 30 character long";

        public const string RetirementAgeNull = "The retirement age should not be empty";

        public const string RetirementAgeInclusiveBetween = "The minimum age is 60 and the maximum age is 75 years";

        public const string MobileNumberNull = "The mobile number should not be empty";

        public const string MobileNumberLength = "The mobile number must be 10 digit in length";

        public const string InvestmentAmountGreaterThan = "The investment amount should not be empty";

        public const string EmailNull = "The email should not be empty";

        public const string EmailInvalid = "Invalid email address";

        public const string DateofBirthNull = "The date of birth should not be empty";

        public const string DateofBirthInclusiveBetween = "The birthday must not be longer ago than 150 years and can not be in the future";

        public const string InvestmentAmountPensionSilver = "Min investment amount required for pension silver : 100000 and retirement age required : 65";

        public const string InvestmentAmountPensionGold = "Min investment amount required for pension gold : 300000 and retirement age required : 63";

        public const string InvestmentAmountPensionPlatinum = "Min investment amount required for pension platinum: 500000 and retirement age required : 60";

        public const long PensionSilverMin = 100000;

        public const long PensionGoldMin = 300000;

        public const long PensionPlatinumMin = 500000;

        public const double PensionSilverFactor = 0.02;

        public const double PensionGoldFactor = 0.04;

        public const double PensionPlatinumFactor = 0.06;
    }
}
