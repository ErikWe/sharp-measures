using ErikWe.SharpMeasures.Quantities;

using System;

namespace ErikWe.SharpMeasures.Units
{
    public struct UnitOfVolume
    {
        public static readonly UnitOfVolume Litre = new(FromUnitOfLength(UnitOfLength.Metre) * Math.Pow(10, -3));
        public static readonly UnitOfVolume Millilitre = new(Litre.Scale * MetricPrefix.Milli);
        public static readonly UnitOfVolume Centilitre = new(Litre.Scale * MetricPrefix.Centi);
        public static readonly UnitOfVolume Decilitre = new(Litre.Scale * MetricPrefix.Deci);

        public Scalar Scale { get; }

        private UnitOfVolume(Scalar scale)
        {
            Scale = scale;
        }

        private static Scalar FromUnitOfLength(UnitOfLength lengthUnit) => Math.Pow(lengthUnit.Scale, 3);
    }
}
