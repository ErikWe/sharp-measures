using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct AngularVelocity2 : IEquatable<AngularVelocity2>, IQuantity2
    {
        public AngularVelocity X { get; }
        public AngularVelocity Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public AngularVelocity2(Scalar2 components)
        {
            X = new AngularVelocity(components.X);
            Y = new AngularVelocity(components.Y);
        }

        public AngularVelocity2(Scalar2 components, UnitOfAngle angleUnit, UnitOfTime timeUnit)
        {
            X = new AngularVelocity(components.X, angleUnit, timeUnit);
            Y = new AngularVelocity(components.Y, angleUnit, timeUnit);
        }

        public AngularVelocity2(Scalar2 components, (UnitOfAngle unit, MetricPrefix prefix) angle, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            X = new AngularVelocity(components.X, angle, time);
            Y = new AngularVelocity(components.Y, angle, time);
        }

        public AngularVelocity2(AngularVelocity x, AngularVelocity y)
        {
            X = x;
            Y = y;
        }

        public AngularVelocity2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public AngularVelocity Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(AngularVelocity2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(AngularVelocity2 a, AngularVelocity2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(AngularVelocity2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is AngularVelocity2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.RadiansPerSecond}, {Y.RadiansPerSecond}) [rad/s]";

        public static bool operator ==(AngularVelocity2? a, AngularVelocity2? b)
        {
            if (a is null)
            {
                return b is null;
            }
            else
            {
                return a.Equals(b);
            }
        }

        public static bool operator !=(AngularVelocity2? a, AngularVelocity2? b) => !(a == b);

        public static AngularVelocity2 operator +(AngularVelocity2 a) => a;
        public static AngularVelocity2 operator -(AngularVelocity2 a) => new(-a.X, -a.Y);
        public static AngularVelocity2 operator +(AngularVelocity2 a, AngularVelocity2 b) => new(a.X + b.X, a.Y + b.Y);
        public static AngularVelocity2 operator -(AngularVelocity2 a, AngularVelocity2 b) => new(a.X - b.X, a.Y - b.Y);
        public static AngularVelocity2 operator %(AngularVelocity2 a, AngularVelocity2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(AngularVelocity2 a, AngularVelocity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(AngularVelocity2 a, AngularVelocity2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Angle2 operator *(AngularVelocity2 a, Time2 b) => new(a.X * b.X, a.Y * b.Y);
        public static AngularAcceleration2 operator *(AngularVelocity2 a, Frequency2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Angle2 operator /(AngularVelocity2 a, Frequency2 b) => new(a.X / b.X, a.Y / b.Y);
        public static AngularAcceleration2 operator /(AngularVelocity2 a, Time2 b) => new(a.X / b.X, a.Y / b.Y);

        public static AngularVelocity2 operator *(AngularVelocity2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static AngularVelocity2 operator *(Scalar2 a, AngularVelocity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static AngularVelocity2 operator /(AngularVelocity2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(AngularVelocity2 a, IQuantity2 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude);
        public static UnhandledQuantity2 operator /(AngularVelocity2 a, IQuantity2 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude);

        public static Scalar2 operator /(AngularVelocity2 a, AngularVelocity b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(AngularVelocity a, AngularVelocity2 b) => new(a / b.X, a / b.Y);

        public static AngularVelocity2 operator *(AngularVelocity2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static AngularVelocity2 operator *(Scalar a, AngularVelocity2 b) => new(a * b.X, a * b.Y);
        public static AngularVelocity2 operator /(AngularVelocity2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(AngularVelocity2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator /(AngularVelocity2 a, IQuantity b) => new(a.X / b, a.Y / b);
    }
}
