using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Velocity : IEquatable<Velocity>, IComparable<Velocity>, IQuantity
    {
        public static readonly Velocity Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
            => length.prefix / time.prefix * magnitude * length.unit.Scale / time.unit.Scale;

        public Scalar Magnitude { get; }

        public Velocity(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Velocity(Scalar magnitude, UnitOfLength lengthUnit, UnitOfTime timeUnit)
        {
            Magnitude = ComputeMagnitude(magnitude, (lengthUnit, MetricPrefix.Identity), (timeUnit, MetricPrefix.Identity));
        }

        public Velocity(Scalar magnitude, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            Magnitude = ComputeMagnitude(magnitude, length, time);
        }

        public Scalar InUnit(UnitOfLength lengthUnit, UnitOfTime timeUnit) => InUnit((lengthUnit, MetricPrefix.Identity), (timeUnit, MetricPrefix.Identity));
        public Scalar InUnit((UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
            => Magnitude / (length.prefix / time.prefix * length.unit.Scale / time.unit.Scale);

        public Scalar MetresPerSecond => InUnit(UnitOfLength.Metre, UnitOfTime.Second);
        public Scalar MetresPerHour => InUnit(UnitOfLength.Metre, UnitOfTime.Hour);
        public Scalar KilometresPerHour => InUnit((UnitOfLength.Metre, MetricPrefix.Kilo), (UnitOfTime.Hour, MetricPrefix.Identity));
        public Scalar MilesPerHour => InUnit(UnitOfLength.Mile, UnitOfTime.Hour);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Velocity Abs() => new(Magnitude.Abs());
        public Velocity Floor() => new(Magnitude.Floor());
        public Velocity Ceiling() => new(Magnitude.Ceiling());
        public Velocity Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Velocity other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is Velocity other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{MetresPerSecond} [m/s]";

        public int CompareTo(Velocity other)
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

        public static bool operator ==(Velocity? x, Velocity? y)
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

        public static bool operator !=(Velocity? x, Velocity? y) => !(x == y);

        public static Velocity operator +(Velocity x) => x;
        public static Velocity operator -(Velocity x) => new(-x.Magnitude);
        public static Velocity operator +(Velocity x, Velocity y) => new(x.Magnitude + y.Magnitude);
        public static Velocity operator -(Velocity x, Velocity y) => new(x.Magnitude - y.Magnitude);
        public static Velocity operator %(Velocity x, Velocity y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Velocity x, Velocity y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Velocity x, Velocity y) => new(x.Magnitude / y.Magnitude);

        public static Length operator *(Velocity x, Time y) => new(x.Magnitude * y.Magnitude);
        public static Acceleration operator *(Velocity x, Frequency y) => new(x.Magnitude * y.Magnitude);
        public static Length operator /(Velocity x, Frequency y) => new(x.Magnitude / y.Magnitude);
        public static Acceleration operator /(Velocity x, Time y) => new(x.Magnitude / y.Magnitude);
        public static Frequency operator /(Velocity x, Length y) => new(x.Magnitude / y.Magnitude);
        public static Time operator /(Velocity x, Acceleration y) => new(x.Magnitude / y.Magnitude);

        public static Velocity operator *(Velocity x, Scalar y) => new(x.Magnitude * y);
        public static Velocity operator *(Scalar x, Velocity y) => new(x * y.Magnitude);
        public static Velocity operator /(Velocity x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(Velocity x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Velocity x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Velocity x, Velocity y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Velocity x, Velocity y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Velocity x, Velocity y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Velocity x, Velocity y) => x.Magnitude >= y.Magnitude;
    }
}
