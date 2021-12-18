using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct AngularVelocity : IEquatable<AngularVelocity>, IComparable<AngularVelocity>, IQuantity
    {
        public static readonly AngularVelocity Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, (UnitOfAngle unit, MetricPrefix prefix) angle, (UnitOfTime unit, MetricPrefix prefix) time)
            => angle.prefix / time.prefix * magnitude * angle.unit.Scale / time.unit.Scale;

        public Scalar Magnitude { get; }

        public AngularVelocity(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public AngularVelocity(Scalar magnitude, UnitOfAngle angleUnit, UnitOfTime timeUnit)
        {
            Magnitude = ComputeMagnitude(magnitude, (angleUnit, MetricPrefix.Identity), (timeUnit, MetricPrefix.Identity));
        }

        public AngularVelocity(Scalar magnitude, (UnitOfAngle unit, MetricPrefix prefix) angle, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            Magnitude = ComputeMagnitude(magnitude, angle, time);
        }

        public Scalar InUnit(UnitOfAngle angleUnit, UnitOfTime timeUnit) => InUnit((angleUnit, MetricPrefix.Identity), (timeUnit, MetricPrefix.Identity));
        public Scalar InUnit((UnitOfAngle unit, MetricPrefix prefix) angle, (UnitOfTime unit, MetricPrefix prefix) time)
            => Magnitude / (angle.prefix / time.prefix * angle.unit.Scale / time.unit.Scale);

        public Scalar RadiansPerSecond => InUnit(UnitOfAngle.Radian, UnitOfTime.Second);
        public Scalar DegreesPerSecond => InUnit(UnitOfAngle.Degree, UnitOfTime.Second);
        public Scalar ArcMinutesPerSecond => InUnit(UnitOfAngle.ArcMinute, UnitOfTime.Second);
        public Scalar ArcSecondsPerSecond => InUnit(UnitOfAngle.ArcSecond, UnitOfTime.Second);
        public Scalar TurnsPerSecond => InUnit(UnitOfAngle.Turn, UnitOfTime.Second);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public AngularVelocity Abs() => new(Magnitude.Abs());
        public AngularVelocity Floor() => new(Magnitude.Floor());
        public AngularVelocity Ceiling() => new(Magnitude.Ceiling());
        public AngularVelocity Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(AngularVelocity other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is AngularVelocity other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{RadiansPerSecond} [rad/s]";

        public int CompareTo(AngularVelocity other)
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

        public static bool operator ==(AngularVelocity? x, AngularVelocity? y)
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

        public static bool operator !=(AngularVelocity? x, AngularVelocity? y) => !(x == y);

        public static AngularVelocity operator +(AngularVelocity x) => x;
        public static AngularVelocity operator -(AngularVelocity x) => new(-x.Magnitude);
        public static AngularVelocity operator +(AngularVelocity x, AngularVelocity y) => new(x.Magnitude + y.Magnitude);
        public static AngularVelocity operator -(AngularVelocity x, AngularVelocity y) => new(x.Magnitude - y.Magnitude);
        public static AngularVelocity operator %(AngularVelocity x, AngularVelocity y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(AngularVelocity x, AngularVelocity y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(AngularVelocity x, AngularVelocity y) => new(x.Magnitude / y.Magnitude);

        public static Angle operator *(AngularVelocity x, Time y) => new(x.Magnitude * y.Magnitude);
        public static AngularAcceleration operator *(AngularVelocity x, Frequency y) => new(x.Magnitude * y.Magnitude);
        public static Angle operator /(AngularVelocity x, Frequency y) => new(x.Magnitude / y.Magnitude);
        public static AngularAcceleration operator /(AngularVelocity x, Time y) => new(x.Magnitude / y.Magnitude);

        public static AngularVelocity operator *(AngularVelocity x, Scalar y) => new(x.Magnitude * y);
        public static AngularVelocity operator *(Scalar x, AngularVelocity y) => new(x * y.Magnitude);
        public static AngularVelocity operator /(AngularVelocity x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(AngularVelocity x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(AngularVelocity x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(AngularVelocity x, AngularVelocity y) => x.Magnitude < y.Magnitude;
        public static bool operator >(AngularVelocity x, AngularVelocity y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(AngularVelocity x, AngularVelocity y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(AngularVelocity x, AngularVelocity y) => x.Magnitude >= y.Magnitude;
    }
}