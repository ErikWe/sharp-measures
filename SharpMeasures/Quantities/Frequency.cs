using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Frequency : IEquatable<Frequency>, IComparable<Frequency>, IQuantity
    {
        public static readonly Frequency Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, UnitOfFrequency unit, MetricPrefix prefix) => prefix * magnitude * unit.Scale;

        public Scalar Magnitude { get; }

        public Frequency(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Frequency(Scalar magnitude, UnitOfFrequency unit)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, MetricPrefix.Identity);
        }

        public Frequency(Scalar magnitude, UnitOfFrequency unit, MetricPrefix prefix)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, prefix);
        }

        public Scalar InUnit(UnitOfFrequency unit) => InUnit(unit, MetricPrefix.Identity);
        public Scalar InUnit(UnitOfFrequency unit, MetricPrefix prefix) => prefix * Magnitude * unit.Scale;

        public Scalar Hertz => InUnit(UnitOfFrequency.Hertz);
        public Scalar Kilohertz => InUnit(UnitOfFrequency.Hertz, MetricPrefix.Kilo);
        public Scalar Megahertz => InUnit(UnitOfFrequency.Hertz, MetricPrefix.Mega);
        public Scalar Gigahertz => InUnit(UnitOfFrequency.Hertz, MetricPrefix.Giga);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Frequency Abs() => new(Magnitude.Abs());
        public Frequency Floor() => new(Magnitude.Floor());
        public Frequency Ceiling() => new(Magnitude.Ceiling());
        public Frequency Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Frequency other) => Magnitude.Equals(other.Magnitude);
        public override bool Equals(object? obj) => obj is Frequency other && Equals(other);
        public int CompareTo(Frequency other) => Magnitude.CompareTo(other.Magnitude);

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{Hertz} [Hz]";

        public static bool operator ==(Frequency? x, Frequency? y) => x?.Equals(y) ?? y is null;
        public static bool operator !=(Frequency? x, Frequency? y) => !(x == y);

        public static Frequency operator +(Frequency x) => x;
        public static Frequency operator -(Frequency x) => new(-x.Magnitude);
        public static Frequency operator +(Frequency x, Frequency y) => new(x.Magnitude + y.Magnitude);
        public static Frequency operator -(Frequency x, Frequency y) => new(x.Magnitude - y.Magnitude);
        public static Frequency operator %(Frequency x, Frequency y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Frequency x, Frequency y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Frequency x, Frequency y) => new(x.Magnitude / y.Magnitude);

        public static Velocity operator *(Frequency x, Length y) => new(x.Magnitude * y.Magnitude);
        public static Acceleration operator *(Frequency x, Velocity y) => new(x.Magnitude * y.Magnitude);
        public static Jerk operator *(Frequency x, Acceleration y) => new(x.Magnitude * y.Magnitude);

        public static Frequency operator *(Frequency x, Scalar y) => new(x.Magnitude * y);
        public static Frequency operator *(Scalar x, Frequency y) => new(x * y.Magnitude);
        public static Frequency operator /(Frequency x, Scalar y) => new(x.Magnitude / y);
        public static Time operator /(Scalar x, Frequency y) => new(x / y.Magnitude);

        public static UnhandledQuantity operator *(Frequency x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Frequency x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Frequency x, Frequency y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Frequency x, Frequency y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Frequency x, Frequency y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Frequency x, Frequency y) => x.Magnitude >= y.Magnitude;
    }
}
