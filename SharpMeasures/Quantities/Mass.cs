using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Mass : IEquatable<Mass>, IComparable<Mass>, IQuantity
    {
        public static readonly Mass Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, UnitOfMass unit, MetricPrefix prefix) => prefix * magnitude * unit.Scale;

        public Scalar Magnitude { get; }

        public Mass(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Mass(Scalar magnitude, UnitOfMass unit)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, MetricPrefix.Identity);
        }

        public Mass(Scalar magnitude, UnitOfMass unit, MetricPrefix prefix)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, prefix);
        }

        public Scalar InUnit(UnitOfMass unit) => InUnit(unit, MetricPrefix.Identity);
        public Scalar InUnit(UnitOfMass unit, MetricPrefix prefix) => Magnitude / (prefix * unit.Scale);

        public Scalar Milligrams => InUnit(UnitOfMass.Gram, MetricPrefix.Milli);
        public Scalar Grams => InUnit(UnitOfMass.Gram);
        public Scalar Kilograms => InUnit(UnitOfMass.Gram, MetricPrefix.Kilo);
        public Scalar Tonnes => InUnit(UnitOfMass.Tonne);

        public Scalar Ounces => InUnit(UnitOfMass.Ounce);
        public Scalar Pounds => InUnit(UnitOfMass.Pound);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Mass Abs() => new(Magnitude.Abs());
        public Mass Floor() => new(Magnitude.Floor());
        public Mass Ceiling() => new(Magnitude.Ceiling());
        public Mass Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Mass other) => Magnitude.Equals(other.Magnitude);
        public override bool Equals(object? obj) => obj is Mass other && Equals(other);
        public int CompareTo(Mass other) => Magnitude.CompareTo(other.Magnitude);

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{Kilograms} [kg]";

        public static bool operator ==(Mass? x, Mass? y) => x?.Equals(y) ?? y is null;
        public static bool operator !=(Mass? x, Mass? y) => !(x == y);

        public static Mass operator +(Mass x) => x;
        public static Mass operator -(Mass x) => new(-x.Magnitude);
        public static Mass operator +(Mass x, Mass y) => new(x.Magnitude + y.Magnitude);
        public static Mass operator -(Mass x, Mass y) => new(x.Magnitude - y.Magnitude);
        public static Mass operator %(Mass x, Mass y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Mass x, Mass y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Mass x, Mass y) => new(x.Magnitude / y.Magnitude);

        public static SurfaceDensity operator /(Mass x, Area y) => new(x.Magnitude / y.Magnitude);
        public static Density operator /(Mass x, Volume y) => new(x.Magnitude / y.Magnitude);

        public static Mass operator *(Mass x, Scalar y) => new(x.Magnitude * y);
        public static Mass operator *(Scalar x, Mass y) => new(x * y.Magnitude);
        public static Mass operator /(Mass x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(Mass x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Mass x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Mass x, Mass y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Mass x, Mass y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Mass x, Mass y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Mass x, Mass y) => x.Magnitude >= y.Magnitude;
    }
}
