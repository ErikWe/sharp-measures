using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct AngularAcceleration : IEquatable<AngularAcceleration>, IComparable<AngularAcceleration>, IQuantity
    {
        public static readonly AngularAcceleration Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, (UnitOfAngle unit, MetricPrefix prefix) angle, (UnitOfTime unit, MetricPrefix prefix) time)
            => angle.prefix / Math.Pow(time.prefix, 2) * magnitude * angle.unit.Scale / Math.Pow(time.unit.Scale, 2);

        public Scalar Magnitude { get; }

        public AngularAcceleration(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public AngularAcceleration(Scalar magnitude, UnitOfAngle angleUnit, UnitOfTime timeUnit)
        {
            Magnitude = ComputeMagnitude(magnitude, (angleUnit, MetricPrefix.Identity), (timeUnit, MetricPrefix.Identity));
        }

        public AngularAcceleration(Scalar magnitude, (UnitOfAngle unit, MetricPrefix prefix) angle, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            Magnitude = ComputeMagnitude(magnitude, angle, time);
        }

        public Scalar InUnit(UnitOfAngle angleUnit, UnitOfTime timeUnit) => InUnit((angleUnit, MetricPrefix.Identity), (timeUnit, MetricPrefix.Identity));
        public Scalar InUnit((UnitOfAngle unit, MetricPrefix prefix) angle, (UnitOfTime unit, MetricPrefix prefix) time)
            => Magnitude / (angle.prefix / Math.Pow(time.prefix, 2) * angle.unit.Scale / Math.Pow(time.unit.Scale, 2));

        public Scalar RadiansPerSecondSquared => InUnit(UnitOfAngle.Radian, UnitOfTime.Second);
        public Scalar DegreesPerSecondSquared => InUnit(UnitOfAngle.Degree, UnitOfTime.Second);
        public Scalar ArcMinutesPerSecondSquared => InUnit(UnitOfAngle.ArcMinute, UnitOfTime.Second);
        public Scalar ArcSecondsPerSecondSquared => InUnit(UnitOfAngle.ArcSecond, UnitOfTime.Second);
        public Scalar TurnsPerSecondSquared => InUnit(UnitOfAngle.Turn, UnitOfTime.Second);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public AngularAcceleration Abs() => new(Magnitude.Abs());
        public AngularAcceleration Floor() => new(Magnitude.Floor());
        public AngularAcceleration Ceiling() => new(Magnitude.Ceiling());
        public AngularAcceleration Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(AngularAcceleration other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is AngularAcceleration other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{RadiansPerSecondSquared} [rad/(s^2)]";

        public int CompareTo(AngularAcceleration other)
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

        public static bool operator ==(AngularAcceleration? x, AngularAcceleration? y)
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

        public static bool operator !=(AngularAcceleration? x, AngularAcceleration? y) => !(x == y);

        public static AngularAcceleration operator +(AngularAcceleration x) => x;
        public static AngularAcceleration operator -(AngularAcceleration x) => new(-x.Magnitude);
        public static AngularAcceleration operator +(AngularAcceleration x, AngularAcceleration y) => new(x.Magnitude + y.Magnitude);
        public static AngularAcceleration operator -(AngularAcceleration x, AngularAcceleration y) => new(x.Magnitude - y.Magnitude);
        public static AngularAcceleration operator %(AngularAcceleration x, AngularAcceleration y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(AngularAcceleration x, AngularAcceleration y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(AngularAcceleration x, AngularAcceleration y) => new(x.Magnitude / y.Magnitude);

        public static AngularVelocity operator *(AngularAcceleration x, Time y) => new(x.Magnitude * y.Magnitude);
        public static AngularVelocity operator /(AngularAcceleration x, Frequency y) => new(x.Magnitude / y.Magnitude);

        public static AngularAcceleration operator *(AngularAcceleration x, Scalar y) => new(x.Magnitude * y);
        public static AngularAcceleration operator *(Scalar x, AngularAcceleration y) => new(x * y.Magnitude);
        public static AngularAcceleration operator /(AngularAcceleration x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(AngularAcceleration x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(AngularAcceleration x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(AngularAcceleration x, AngularAcceleration y) => x.Magnitude < y.Magnitude;
        public static bool operator >(AngularAcceleration x, AngularAcceleration y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(AngularAcceleration x, AngularAcceleration y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(AngularAcceleration x, AngularAcceleration y) => x.Magnitude >= y.Magnitude;
    }
}