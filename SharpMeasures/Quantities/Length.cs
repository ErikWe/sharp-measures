using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Length : IEquatable<Length>, IComparable<Length>, IQuantity
    {
        public static readonly Length Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, UnitOfLength unit, MetricPrefix prefix) => prefix * magnitude * unit.Scale;

        public Scalar Magnitude { get; }

        public Length(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Length(Scalar magnitude, UnitOfLength unit)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, MetricPrefix.Identity);
        }

        public Length(Scalar magnitude, UnitOfLength unit, MetricPrefix prefix)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, prefix);
        }

        public Scalar InUnit(UnitOfLength unit) => InUnit(unit, MetricPrefix.Identity);
        public Scalar InUnit(UnitOfLength unit, MetricPrefix prefix) => Magnitude / (prefix * unit.Scale);

        public Scalar Nanometres => InUnit(UnitOfLength.Metre, MetricPrefix.Nano);
        public Scalar Micrometres => InUnit(UnitOfLength.Metre, MetricPrefix.Micro);
        public Scalar Millimetres => InUnit(UnitOfLength.Metre, MetricPrefix.Milli);
        public Scalar Centimetres => InUnit(UnitOfLength.Metre, MetricPrefix.Centi);
        public Scalar Decimetres => InUnit(UnitOfLength.Metre, MetricPrefix.Deci);
        public Scalar Metres => InUnit(UnitOfLength.Metre);
        public Scalar Kilometres => InUnit(UnitOfLength.Metre, MetricPrefix.Kilo);

        public Scalar AstronomicalUnits => InUnit(UnitOfLength.AstronomicalUnit);
        public Scalar Lightyears => InUnit(UnitOfLength.Lightyear);
        public Scalar Parsecs => InUnit(UnitOfLength.Parsec);

        public Scalar Inches => InUnit(UnitOfLength.Inch);
        public Scalar Feet => InUnit(UnitOfLength.Foot);
        public Scalar Yards => InUnit(UnitOfLength.Yard);
        public Scalar Miles => InUnit(UnitOfLength.Mile);

        public Scalar NauticalMiles => InUnit(UnitOfLength.NauticalMile);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Length Abs() => new(Magnitude.Abs());
        public Length Floor() => new(Magnitude.Floor());
        public Length Ceiling() => new(Magnitude.Ceiling());
        public Length Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public Area Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Length other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is Length other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{Metres} [m]";

        public int CompareTo(Length other)
        {
            if (this > other)
            {
                return 1;
            }
            else if (this < other)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public static bool operator ==(Length? x, Length? y)
        {
            if (x is null)
            {
                return y is null;
            }
            else
            {
                return x.Equals(y);
            }
        }

        public static bool operator !=(Length? x, Length? y) => !(x == y);

        public static Length operator +(Length x) => x;
        public static Length operator -(Length x) => new(-x.Magnitude);
        public static Length operator +(Length x, Length y) => new(x.Magnitude + y.Magnitude);
        public static Length operator -(Length x, Length y) => new(x.Magnitude - y.Magnitude);
        public static Length operator %(Length x, Length y) => new(x.Magnitude % y.Magnitude);
        public static Area operator *(Length x, Length y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Length x, Length y) => new(x.Magnitude / y.Magnitude);

        public static Volume operator *(Length x, Area y) => new(x.Magnitude * y.Magnitude);
        public static Velocity operator *(Length x, Frequency y) => new(x.Magnitude * y.Magnitude);
        public static Velocity operator /(Length x, Time y) => new(x.Magnitude / y.Magnitude);
        public static Time operator /(Length x, Velocity y) => new(x.Magnitude / y.Magnitude);

        public static Length operator *(Length x, Scalar y) => new(x.Magnitude * y);
        public static Length operator *(Scalar x, Length y) => new(x * y.Magnitude);
        public static Length operator /(Length x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(Length x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Length x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Length x, Length y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Length x, Length y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Length x, Length y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Length x, Length y) => x.Magnitude >= y.Magnitude;
    }
}
