namespace Quote.Service.API.Models.Enum
{
    using System.ComponentModel;

    public enum PensionPlan
    {
        [Description("PensionSilver")]
        PensionSilver,

        [Description("PensionGold")]
        PensionGold,

        [Description("PensionPlatinum")]
        PensionPlatinum
    }
}
