using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct AngularVelocity3 : IEquatable<AngularVelocity3>, IQuantity3
    {
        public AngularVelocity X { get; }
        public AngularVelocity Y { get; }
        public AngularVelocity Z { get; }

        Scalar IQuantity3.X => X.Magnitude;
        Scalar IQuantity3.Y => Y.Magnitude;
        Scalar IQuantity3.Z => Z.Magnitude;

        public AngularVelocity3(Scalar3 components)
        {
            X = new AngularVelocity(components.X);
            Y = new AngularVelocity(components.Y);
            Z = new AngularVelocity(components.Z);
        }

        public AngularVelocity3(Scalar3 components, UnitOfAngle angleUnit, UnitOfTime timeUnit)
        {
            X = new AngularVelocity(components.X, angleUnit, timeUnit);
            Y = new AngularVelocity(components.Y, angleUnit, timeUnit);
            Z = new AngularVelocity(components.Z, angleUnit, timeUnit);
        }

        public AngularVelocity3(Scalar3 components, (UnitOfAngle unit, MetricPrefix prefix) angle, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            X = new AngularVelocity(components.X, angle, time);
            Y = new AngularVelocity(components.Y, angle, time);
            Z = new AngularVelocity(components.Z, angle, time);
        }

        public AngularVelocity3(AngularVelocity x, AngularVelocity y, AngularVelocity z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public AngularVelocity3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public AngularVelocity Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(AngularVelocity3 other) => Dot(this, other);

        public static UnhandledQuantity Dot(AngularVelocity3 a, AngularVelocity3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public bool Equals(AngularVelocity3 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

        public override bool Equals(object? obj)
        {
            if (obj is AngularVelocity3 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.RadiansPerSecond}, {Y.RadiansPerSecond}, {Z.RadiansPerSecond}) [rad/s]";

        public static bool operator ==(AngularVelocity3? a, AngularVelocity3? b)
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

        public static bool operator !=(AngularVelocity3? a, AngularVelocity3? b) => !(a == b);

        public static AngularVelocity3 operator +(AngularVelocity3 a) => a;
        public static AngularVelocity3 operator -(AngularVelocity3 a) => new(-a.X, -a.Y, -a.Z);
        public static AngularVelocity3 operator +(AngularVelocity3 a, AngularVelocity3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static AngularVelocity3 operator -(AngularVelocity3 a, AngularVelocity3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static AngularVelocity3 operator %(AngularVelocity3 a, AngularVelocity3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(AngularVelocity3 a, AngularVelocity3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(AngularVelocity3 a, AngularVelocity3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Angle3 operator *(AngularVelocity3 a, Time3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static AngularAcceleration3 operator *(AngularVelocity3 a, Frequency3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Angle3 operator /(AngularVelocity3 a, Frequency3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        public static AngularAcceleration3 operator /(AngularVelocity3 a, Time3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static AngularVelocity3 operator *(AngularVelocity3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static AngularVelocity3 operator *(Scalar3 a, AngularVelocity3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static AngularVelocity3 operator /(AngularVelocity3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(AngularVelocity3 a, IQuantity3 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude, (a.Z * b.Z).Magnitude);
        public static UnhandledQuantity3 operator /(AngularVelocity3 a, IQuantity3 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude, (a.Z / b.Z).Magnitude);

        public static Scalar3 operator /(AngularVelocity3 a, AngularVelocity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(AngularVelocity a, AngularVelocity3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static AngularVelocity3 operator *(AngularVelocity3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static AngularVelocity3 operator *(Scalar a, AngularVelocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static AngularVelocity3 operator /(AngularVelocity3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(AngularVelocity3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator /(AngularVelocity3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
    }
}
