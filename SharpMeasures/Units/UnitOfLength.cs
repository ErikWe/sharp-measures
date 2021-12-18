using ErikWe.SharpMeasures.Quantities;

using System;

namespace ErikWe.SharpMeasures.Units
{
    public struct UnitOfLength
    {
        public static readonly UnitOfLength Metre = new(1);

        public static readonly UnitOfLength Nanometre = new(Metre.Scale * MetricPrefix.Nano);
        public static readonly UnitOfLength Micrometre = new(Metre.Scale * MetricPrefix.Micro);
        public static readonly UnitOfLength Millimetre = new(Metre.Scale * MetricPrefix.Milli);
        public static readonly UnitOfLength Centimetre = new(Metre.Scale * MetricPrefix.Centi);
        public static readonly UnitOfLength Decimetre = new(Metre.Scale * MetricPrefix.Deci);
        public static readonly UnitOfLength Kilometre = new(Metre.Scale * MetricPrefix.Kilo);

        public static readonly UnitOfLength AstronomicalUnit = new(Metre.Scale * 1.495978797 * Math.Pow(10, 11));
        public static readonly UnitOfLength Lightyear = new(Metre.Scale * 9460730472580800);
        public static readonly UnitOfLength Parsec = new(AstronomicalUnit.Scale * 648000 / Math.PI);

        public static readonly UnitOfLength Inch = new(Millimetre.Scale * 25.4);
        public static readonly UnitOfLength Foot = new(Inch.Scale * 12);
        public static readonly UnitOfLength Yard = new(Foot.Scale * 3);
        public static readonly UnitOfLength Mile = new(Yard.Scale * 1760);

        public static readonly UnitOfLength NauticalMile = new(Metre.Scale * 1852);

        public Scalar Scale { get; }

        private UnitOfLength(Scalar scale)
        {
            Scale = scale;
        }
    }
}
