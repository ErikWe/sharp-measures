using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Temperature : IEquatable<Temperature>, IComparable<Temperature>, IQuantity
    {
        public static readonly Temperature Zero = new(0);

        private static Scalar ComputePureMagnitude(Scalar magnitude, UnitOfTemperature unit, MetricPrefix prefix) => (prefix * magnitude + unit.Offset) * unit.Scale;

        public Scalar Magnitude { get; }

        public Temperature(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Temperature(Scalar magnitude, UnitOfTemperature unit)
        {
            Magnitude = ComputePureMagnitude(magnitude, unit, MetricPrefix.Identity);
        }

        public Temperature(Scalar magnitude, UnitOfTemperature unit, MetricPrefix prefix)
        {
            Magnitude = ComputePureMagnitude(magnitude, unit, prefix);
        }

        public Scalar InUnit(UnitOfTemperature unit) => InUnit(unit, MetricPrefix.Identity);
        public Scalar InUnit(UnitOfTemperature unit, MetricPrefix prefix) => (Magnitude / unit.Scale - unit.Offset) / prefix;

        public Scalar Kelvin => InUnit(UnitOfTemperature.Kelvin);
        public Scalar Celsius => InUnit(UnitOfTemperature.Celcius);
        public Scalar Rankine => InUnit(UnitOfTemperature.Rankine);
        public Scalar Fahrenheit => InUnit(UnitOfTemperature.Fahrenheit);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Temperature Abs() => new(Magnitude.Abs());
        public Temperature Floor() => new(Magnitude.Floor());
        public Temperature Ceiling() => new(Magnitude.Ceiling());
        public Temperature Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Temperature other) => Magnitude.Equals(other.Magnitude);
        public override bool Equals(object? obj) => obj is Temperature other && Equals(other);
        public int CompareTo(Temperature other) => Magnitude.CompareTo(other.Magnitude);

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{Kelvin} [K]";

        public static bool operator ==(Temperature? x, Temperature? y) => x?.Equals(y) ?? y is null;
        public static bool operator !=(Temperature? x, Temperature? y) => !(x == y);

        public static Temperature operator +(Temperature x) => x;
        public static Temperature operator -(Temperature x) => new(-x.Magnitude);
        public static Temperature operator +(Temperature x, Temperature y) => new(x.Magnitude + y.Magnitude);
        public static Temperature operator -(Temperature x, Temperature y) => new(x.Magnitude - y.Magnitude);
        public static Temperature operator %(Temperature x, Temperature y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Temperature x, Temperature y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Temperature x, Temperature y) => new(x.Magnitude / y.Magnitude);

        public static Temperature operator *(Temperature x, Scalar y) => new(x.Magnitude * y);
        public static Temperature operator *(Scalar x, Temperature y) => new(x * y.Magnitude);
        public static Temperature operator /(Temperature x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(Temperature x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Temperature x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Temperature x, Temperature y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Temperature x, Temperature y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Temperature x, Temperature y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Temperature x, Temperature y) => x.Magnitude >= y.Magnitude;
    }
}
