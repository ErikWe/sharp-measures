using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Time : IEquatable<Time>, IComparable<Time>, IQuantity
    {
        public static readonly Time Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, UnitOfTime unit, MetricPrefix prefix) => prefix * magnitude * unit.Scale;

        public Scalar Magnitude { get; }

        public Time(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Time(Scalar magnitude, UnitOfTime unit)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, MetricPrefix.Identity);
        }

        public Time(Scalar magnitude, UnitOfTime unit, MetricPrefix prefix)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, prefix);
        }

        public Scalar InUnit(UnitOfTime unit) => InUnit(unit, MetricPrefix.Identity);
        public Scalar InUnit(UnitOfTime unit, MetricPrefix prefix) => Magnitude / (prefix * unit.Scale);

        public Scalar Femtoseconds => InUnit(UnitOfTime.Second, MetricPrefix.Femto);
        public Scalar Picoseconds => InUnit(UnitOfTime.Second, MetricPrefix.Pico);
        public Scalar Nanoseconds => InUnit(UnitOfTime.Second, MetricPrefix.Nano);
        public Scalar Microseconds => InUnit(UnitOfTime.Second, MetricPrefix.Micro);
        public Scalar Milliseconds => InUnit(UnitOfTime.Second, MetricPrefix.Milli);
        public Scalar Seconds => InUnit(UnitOfTime.Second);

        public Scalar Minutes => InUnit(UnitOfTime.Minute);
        public Scalar Hours => InUnit(UnitOfTime.Hour);
        public Scalar Days => InUnit(UnitOfTime.Day);
        public Scalar Weeks => InUnit(UnitOfTime.Week);
        public Scalar CommonYears => InUnit(UnitOfTime.CommonYear);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Time Abs() => new(Magnitude.Abs());
        public Time Floor() => new(Magnitude.Floor());
        public Time Ceiling() => new(Magnitude.Ceiling());
        public Time Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Time other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is Time other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{Seconds} [s]";

        public int CompareTo(Time other)
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

        public static bool operator ==(Time? x, Time? y)
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

        public static bool operator !=(Time? x, Time? y) => !(x == y);

        public static Time operator +(Time x) => x;
        public static Time operator -(Time x) => new(-x.Magnitude);
        public static Time operator +(Time x, Time y) => new(x.Magnitude + y.Magnitude);
        public static Time operator -(Time x, Time y) => new(x.Magnitude - y.Magnitude);
        public static Time operator %(Time x, Time y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Time x, Time y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Time x, Time y) => new(x.Magnitude / y.Magnitude);

        public static Length operator *(Time x, Velocity y) => new(x.Magnitude * y.Magnitude);
        public static Velocity operator *(Time x, Acceleration y) => new(x.Magnitude * y.Magnitude);
        public static Acceleration operator *(Time x, Jerk y) => new(x.Magnitude * y.Magnitude);

        public static Time operator *(Time x, Scalar y) => new(x.Magnitude * y);
        public static Time operator *(Scalar x, Time y) => new(x * y.Magnitude);
        public static Time operator /(Time x, Scalar y) => new(x.Magnitude / y);
        public static Frequency operator /(Scalar x, Time y) => new(x / y.Magnitude);

        public static UnhandledQuantity operator *(Time x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Time x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Time x, Time y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Time x, Time y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Time x, Time y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Time x, Time y) => x.Magnitude >= y.Magnitude;
    }
}
