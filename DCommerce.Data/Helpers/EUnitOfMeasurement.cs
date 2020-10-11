using System;
using System.ComponentModel;

namespace DCommerce.Data.Helpers
{
    public enum EUnitOfMeasurement : byte
    {
        [Description("UN")]
        Unity = 1,

        [Description("MG")]
        Milligram = 2,

        [Description("G")]
        Gram = 3,

        [Description("KG")]
        Kilogram = 4,

        [Description("L")]
        Liter = 5,

        [Description("U")]
        Unknown = 6,
    }
}
