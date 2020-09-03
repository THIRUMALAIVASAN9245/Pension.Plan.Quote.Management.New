namespace Quote.Service.API.Models.ResquestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UpdateQuoteModel : BaseQuoteModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}