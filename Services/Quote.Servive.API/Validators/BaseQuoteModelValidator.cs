namespace Quote.Service.API.Validators
{
    using FluentValidation;
    using Quote.Service.API.Infrastructure.Helpers;
    using Quote.Service.API.Models.ResquestModels;
    using System;

    public class BaseQuoteModelValidator<T> : AbstractValidator<T> where T : BaseQuoteModel
    {
        public BaseQuoteModelValidator()
        {
            RuleFor(x => x.ClientName)
                 .NotNull()
                 .WithMessage(AlertMessages.ClientNameNull)
                 .MaximumLength(30)
                 .WithMessage(AlertMessages.ClientNameMaximumLength);

            RuleFor(x => x.RetirementAge)
                 .NotEmpty()
                 .WithMessage(AlertMessages.RetirementAgeNull)
                 .InclusiveBetween(60, 75)
                 .WithMessage(AlertMessages.RetirementAgeInclusiveBetween);

            RuleFor(x => x.MobileNumber)
                 .NotNull()
                 .WithMessage(AlertMessages.MobileNumberNull)
                 .Matches("^\\d{10}$")
                 .WithMessage(AlertMessages.MobileNumberLength);

            RuleFor(x => x.InvestmentAmount)
                .GreaterThan(0)
                .WithMessage(AlertMessages.InvestmentAmountGreaterThan);

            RuleFor(x => x.Email)
                 .NotNull()
                 .WithMessage(AlertMessages.EmailNull)
                 .EmailAddress()
                 .WithMessage(AlertMessages.EmailInvalid);

            RuleFor(x => x.DateofBirth)
                 .NotEmpty()
                 .Must(ValidatorExtension.BeAValidDate)
                 .WithMessage(AlertMessages.DateofBirthNull)
                 .InclusiveBetween(DateTime.Now.AddYears(-150).Date, DateTime.UtcNow)
                 .WithMessage(AlertMessages.DateofBirthInclusiveBetween);

            RuleFor(x => x.InvestmentAmount).Must(ValidatorExtension.ValidateInvestmentAmount)
                .WithMessage(y => ValidatorExtension.WithMessageInvestmentAmount(y.PensionPlan));

            RuleFor(x => x.ClientSex).IsInEnum();

            RuleFor(x => x.PensionPlan).IsInEnum();
        }
    }
}