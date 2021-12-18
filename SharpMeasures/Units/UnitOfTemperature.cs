using ErikWe.SharpMeasures.Quantities;

namespace ErikWe.SharpMeasures.Units
{
    public struct UnitOfTemperature
    {
        public static readonly UnitOfTemperature Kelvin = new(1, 0);
        public static readonly UnitOfTemperature Celcius = new(Kelvin.Scale, Kelvin.Offset + 273.15);

        public static readonly UnitOfTemperature Rankine = new(Kelvin.Scale * 5 / 9, Kelvin.Offset);
        public static readonly UnitOfTemperature Fahrenheit = new(Rankine.Scale, Rankine.Offset + 459.67);

        public Scalar Scale { get; }
        public Scalar Offset { get; }

        private UnitOfTemperature(Scalar scale, Scalar offset)
        {
            Scale = scale;
            Offset = offset;
        }
    }
}
