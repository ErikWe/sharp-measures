using ErikWe.SharpMeasures.Quantities;

namespace ErikWe.SharpMeasures.Units
{
    public struct UnitOfMass
    {
        public static readonly UnitOfMass Gram = new(1 / 1000);

        public static readonly UnitOfMass Milligram = new(Gram.Scale * MetricPrefix.Milli);
        public static readonly UnitOfMass Kilogram = new(Gram.Scale * MetricPrefix.Kilo);
        public static readonly UnitOfMass Tonne = new(Gram.Scale * MetricPrefix.Mega);

        /// <summary>Avoirdupois ounce (US customary and British imperial), abbreviated (oz). Equivalent to 28.349523125 <see cref="Gram"/>.</summary>
        public static readonly UnitOfMass Ounce = new(Gram.Scale * 28.349523125);
        /// <summary>Avoirdupois pound (US customary and British imperial), abbreviated (lb). Equivalent to 16 <see cref="Ounce"/>.</summary>
        public static readonly UnitOfMass Pound = new(Ounce.Scale * 16);

        public Scalar Scale { get; }

        private UnitOfMass(Scalar scale)
        {
            Scale = scale;
        }
    }
}
