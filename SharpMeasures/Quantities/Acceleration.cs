using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Acceleration : IEquatable<Acceleration>, IComparable<Acceleration>, IQuantity
    {
        public static readonly Acceleration Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
            => length.prefix / Math.Pow(time.prefix, 2) * magnitude * length.unit.Scale / Math.Pow(time.unit.Scale, 2);

        public Scalar Magnitude { get; }

        public Acceleration(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Acceleration(Scalar magnitude, UnitOfLength lengthUnit, UnitOfTime timeUnit)
        {
            Magnitude = ComputeMagnitude(magnitude, (lengthUnit, MetricPrefix.Identity), (timeUnit, MetricPrefix.Identity));
        }

        public Acceleration(Scalar magnitude, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            Magnitude = ComputeMagnitude(magnitude, length, time);
        }

        public Scalar InUnit(UnitOfLength lengthUnit, UnitOfTime timeUnit) => InUnit((lengthUnit, MetricPrefix.Identity), (timeUnit, MetricPrefix.Identity));
        public Scalar InUnit((UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
            => Magnitude / (length.prefix / Math.Pow(time.prefix, 2) * length.unit.Scale / Math.Pow(time.unit.Scale, 2));

        public Scalar MetresPerSecondSquared => InUnit(UnitOfLength.Metre, UnitOfTime.Second);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Acceleration Abs() => new(Magnitude.Abs());
        public Acceleration Floor() => new(Magnitude.Floor());
        public Acceleration Ceiling() => new(Magnitude.Ceiling());
        public Acceleration Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Acceleration other) => Magnitude.Equals(other.Magnitude);
        public override bool Equals(object? obj) => obj is Acceleration other && Equals(other);
        public int CompareTo(Acceleration other) => Magnitude.CompareTo(other.Magnitude);

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{MetresPerSecondSquared} [m/(s^2)]";

        public static bool operator ==(Acceleration? x, Acceleration? y) => x?.Equals(y) ?? y is null;
        public static bool operator !=(Acceleration? x, Acceleration? y) => !(x == y);

        public static Acceleration operator +(Acceleration x) => x;
        public static Acceleration operator -(Acceleration x) => new(-x.Magnitude);
        public static Acceleration operator +(Acceleration x, Acceleration y) => new(x.Magnitude + y.Magnitude);
        public static Acceleration operator -(Acceleration x, Acceleration y) => new(x.Magnitude - y.Magnitude);
        public static Acceleration operator %(Acceleration x, Acceleration y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Acceleration x, Acceleration y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Acceleration x, Acceleration y) => new(x.Magnitude / y.Magnitude);

        public static Velocity operator *(Acceleration x, Time y) => new(x.Magnitude * y.Magnitude);
        public static Jerk operator *(Acceleration x, Frequency y) => new(x.Magnitude * y.Magnitude);
        public static Velocity operator /(Acceleration x, Frequency y) => new(x.Magnitude / y.Magnitude);
        public static Jerk operator /(Acceleration x, Time y) => new(x.Magnitude / y.Magnitude);
        public static Frequency operator /(Acceleration x, Velocity y) => new(x.Magnitude / y.Magnitude);
        public static Time operator /(Acceleration x, Jerk y) => new(x.Magnitude / y.Magnitude);

        public static Acceleration operator *(Acceleration x, Scalar y) => new(x.Magnitude * y);
        public static Acceleration operator *(Scalar x, Acceleration y) => new(x * y.Magnitude);
        public static Acceleration operator /(Acceleration x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(Acceleration x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Acceleration x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Acceleration x, Acceleration y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Acceleration x, Acceleration y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Acceleration x, Acceleration y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Acceleration x, Acceleration y) => x.Magnitude >= y.Magnitude;
    }
}
