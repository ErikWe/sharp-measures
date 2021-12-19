using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class AngularAcceleration3 : IEquatable<AngularAcceleration3>, IQuantity3<AngularAcceleration>
    {
        public AngularAcceleration X { get; }
        public AngularAcceleration Y { get; }
        public AngularAcceleration Z { get; }

        Scalar IQuantity3.XMagnitude => X.Magnitude;
        Scalar IQuantity3.YMagnitude => Y.Magnitude;
        Scalar IQuantity3.ZMagnitude => Z.Magnitude;

        public AngularAcceleration3(Scalar3 components)
        {
            X = new AngularAcceleration(components.X);
            Y = new AngularAcceleration(components.Y);
            Z = new AngularAcceleration(components.Z);
        }

        public AngularAcceleration3(Scalar3 components, UnitOfAngle angleUnit, UnitOfTime timeUnit)
        {
            X = new AngularAcceleration(components.X, angleUnit, timeUnit);
            Y = new AngularAcceleration(components.Y, angleUnit, timeUnit);
            Z = new AngularAcceleration(components.Z, angleUnit, timeUnit);
        }

        public AngularAcceleration3(Scalar3 components, (UnitOfAngle unit, MetricPrefix prefix) angle, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            X = new AngularAcceleration(components.X, angle, time);
            Y = new AngularAcceleration(components.Y, angle, time);
            Z = new AngularAcceleration(components.Z, angle, time);
        }

        public AngularAcceleration3(AngularAcceleration x, AngularAcceleration y, AngularAcceleration z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public AngularAcceleration3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public AngularAcceleration Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(AngularAcceleration3 other) => Dot(this, other);

        public static UnhandledQuantity Dot(AngularAcceleration3 a, AngularAcceleration3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public bool Equals(AngularAcceleration3? other) => X.Equals(other?.X) && Y.Equals(other?.Y) && Z.Equals(other?.Z);
        public override bool Equals(object? obj) => Equals(obj as AngularAcceleration3);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.RadiansPerSecondSquared}, {Y.RadiansPerSecondSquared}, {Z.RadiansPerSecondSquared}) [rad/s]";

        public static bool operator ==(AngularAcceleration3? a, AngularAcceleration3? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(AngularAcceleration3? a, AngularAcceleration3? b) => !(a == b);

        public static AngularAcceleration3 operator +(AngularAcceleration3 a) => a;
        public static AngularAcceleration3 operator -(AngularAcceleration3 a) => new(-a.X, -a.Y, -a.Z);
        public static AngularAcceleration3 operator +(AngularAcceleration3 a, AngularAcceleration3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static AngularAcceleration3 operator -(AngularAcceleration3 a, AngularAcceleration3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static AngularAcceleration3 operator %(AngularAcceleration3 a, AngularAcceleration3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(AngularAcceleration3 a, AngularAcceleration3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(AngularAcceleration3 a, AngularAcceleration3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static AngularVelocity3 operator *(AngularAcceleration3 a, Time3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static AngularVelocity3 operator /(AngularAcceleration3 a, Frequency3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static AngularAcceleration3 operator *(AngularAcceleration3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static AngularAcceleration3 operator *(Scalar3 a, AngularAcceleration3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static AngularAcceleration3 operator /(AngularAcceleration3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(AngularAcceleration3 a, IQuantity3 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude, a.Z * b.ZMagnitude);
        public static UnhandledQuantity3 operator /(AngularAcceleration3 a, IQuantity3 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude, a.Z / b.ZMagnitude);

        public static Scalar3 operator /(AngularAcceleration3 a, AngularAcceleration b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(AngularAcceleration a, AngularAcceleration3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static AngularAcceleration3 operator *(AngularAcceleration3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static AngularAcceleration3 operator *(Scalar a, AngularAcceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static AngularAcceleration3 operator /(AngularAcceleration3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(AngularAcceleration3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator /(AngularAcceleration3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
    }
}