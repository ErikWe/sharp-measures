using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Jerk : IEquatable<Jerk>, IComparable<Jerk>, IQuantity
    {
        public static readonly Jerk Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
            => length.prefix / Math.Pow(time.prefix, 3) * magnitude * length.unit.Scale / Math.Pow(time.unit.Scale, 3);

        public Scalar Magnitude { get; }

        public Jerk(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Jerk(Scalar magnitude, UnitOfLength lengthUnit, UnitOfTime timeUnit)
        {
            Magnitude = ComputeMagnitude(magnitude, (lengthUnit, MetricPrefix.Identity), (timeUnit, MetricPrefix.Identity));
        }

        public Jerk(Scalar magnitude, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            Magnitude = ComputeMagnitude(magnitude, length, time);
        }

        public Scalar InUnit(UnitOfLength lengthUnit, UnitOfTime timeUnit) => InUnit((lengthUnit, MetricPrefix.Identity), (timeUnit, MetricPrefix.Identity));
        public Scalar InUnit((UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
            => Magnitude / (length.prefix / Math.Pow(time.prefix, 3) * length.unit.Scale / Math.Pow(time.unit.Scale, 3));

        public Scalar MetresPerCubicSecond => InUnit(UnitOfLength.Metre, UnitOfTime.Second);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Jerk Abs() => new(Magnitude.Abs());
        public Jerk Floor() => new(Magnitude.Floor());
        public Jerk Ceiling() => new(Magnitude.Ceiling());
        public Jerk Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Jerk other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is Jerk other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{MetresPerCubicSecond} [m/(s^3)]";

        public int CompareTo(Jerk other)
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

        public static bool operator ==(Jerk? x, Jerk? y)
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

        public static bool operator !=(Jerk? x, Jerk? y) => !(x == y);

        public static Jerk operator +(Jerk x) => x;
        public static Jerk operator -(Jerk x) => new(-x.Magnitude);
        public static Jerk operator +(Jerk x, Jerk y) => new(x.Magnitude + y.Magnitude);
        public static Jerk operator -(Jerk x, Jerk y) => new(x.Magnitude - y.Magnitude);
        public static Jerk operator %(Jerk x, Jerk y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Jerk x, Jerk y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Jerk x, Jerk y) => new(x.Magnitude / y.Magnitude);

        public static Acceleration operator *(Jerk x, Time y) => new(x.Magnitude * y.Magnitude);
        public static Acceleration operator /(Jerk x, Frequency y) => new(x.Magnitude / y.Magnitude);
        public static Frequency operator /(Jerk x, Acceleration y) => new(x.Magnitude / y.Magnitude);

        public static Jerk operator *(Jerk x, Scalar y) => new(x.Magnitude * y);
        public static Jerk operator *(Scalar x, Jerk y) => new(x * y.Magnitude);
        public static Jerk operator /(Jerk x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(Jerk x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Jerk x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Jerk x, Jerk y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Jerk x, Jerk y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Jerk x, Jerk y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Jerk x, Jerk y) => x.Magnitude >= y.Magnitude;
    }
}
