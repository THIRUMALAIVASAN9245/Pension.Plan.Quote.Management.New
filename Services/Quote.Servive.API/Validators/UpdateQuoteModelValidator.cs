namespace Quote.Service.API.Validators
{
    using FluentValidation;
    using Quote.Service.API.Infrastructure.Helpers;
    using Quote.Service.API.Models.ResquestModels;

    public class UpdateQuoteModelValidator : BaseQuoteModelValidator<UpdateQuoteModel>
    {
        public UpdateQuoteModelValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty()
                 .WithMessage(AlertMessages.ClientIdEmpty);
        }
    }
}