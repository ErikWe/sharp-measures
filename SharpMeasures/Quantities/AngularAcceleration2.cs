using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class AngularAcceleration2 : IEquatable<AngularAcceleration2>, IQuantity2<AngularAcceleration>
    {
        public AngularAcceleration X { get; }
        public AngularAcceleration Y { get; }

        Scalar IQuantity2.XMagnitude => X.Magnitude;
        Scalar IQuantity2.YMagnitude => Y.Magnitude;

        public AngularAcceleration2(Scalar2 components)
        {
            X = new AngularAcceleration(components.X);
            Y = new AngularAcceleration(components.Y);
        }

        public AngularAcceleration2(Scalar2 components, UnitOfAngle angleUnit, UnitOfTime timeUnit)
        {
            X = new AngularAcceleration(components.X, angleUnit, timeUnit);
            Y = new AngularAcceleration(components.Y, angleUnit, timeUnit);
        }

        public AngularAcceleration2(Scalar2 components, (UnitOfAngle unit, MetricPrefix prefix) angle, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            X = new AngularAcceleration(components.X, angle, time);
            Y = new AngularAcceleration(components.Y, angle, time);
        }

        public AngularAcceleration2(AngularAcceleration x, AngularAcceleration y)
        {
            X = x;
            Y = y;
        }

        public AngularAcceleration2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public AngularAcceleration Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(AngularAcceleration2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(AngularAcceleration2 a, AngularAcceleration2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(AngularAcceleration2? other) => X.Equals(other?.X) && Y.Equals(other?.Y);
        public override bool Equals(object? obj) => Equals(obj as AngularAcceleration2);

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.RadiansPerSecondSquared}, {Y.RadiansPerSecondSquared}) [rad/(s^2)]";

        public static bool operator ==(AngularAcceleration2? a, AngularAcceleration2? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(AngularAcceleration2? a, AngularAcceleration2? b) => !(a == b);

        public static AngularAcceleration2 operator +(AngularAcceleration2 a) => a;
        public static AngularAcceleration2 operator -(AngularAcceleration2 a) => new(-a.X, -a.Y);
        public static AngularAcceleration2 operator +(AngularAcceleration2 a, AngularAcceleration2 b) => new(a.X + b.X, a.Y + b.Y);
        public static AngularAcceleration2 operator -(AngularAcceleration2 a, AngularAcceleration2 b) => new(a.X - b.X, a.Y - b.Y);
        public static AngularAcceleration2 operator %(AngularAcceleration2 a, AngularAcceleration2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(AngularAcceleration2 a, AngularAcceleration2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(AngularAcceleration2 a, AngularAcceleration2 b) => new(a.X / b.X, a.Y / b.Y);

        public static AngularVelocity2 operator *(AngularAcceleration2 a, Time2 b) => new(a.X * b.X, a.Y * b.Y);
        public static AngularVelocity2 operator /(AngularAcceleration2 a, Frequency2 b) => new(a.X / b.X, a.Y / b.Y);

        public static AngularAcceleration2 operator *(AngularAcceleration2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static AngularAcceleration2 operator *(Scalar2 a, AngularAcceleration2 b) => new(a.X * b.X, a.Y * b.Y);
        public static AngularAcceleration2 operator /(AngularAcceleration2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(AngularAcceleration2 a, IQuantity2 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude);
        public static UnhandledQuantity2 operator /(AngularAcceleration2 a, IQuantity2 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude);

        public static Scalar2 operator /(AngularAcceleration2 a, AngularAcceleration b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(AngularAcceleration a, AngularAcceleration2 b) => new(a / b.X, a / b.Y);

        public static AngularAcceleration2 operator *(AngularAcceleration2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static AngularAcceleration2 operator *(Scalar a, AngularAcceleration2 b) => new(a * b.X, a * b.Y);
        public static AngularAcceleration2 operator /(AngularAcceleration2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(AngularAcceleration2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator /(AngularAcceleration2 a, IQuantity b) => new(a.X / b, a.Y / b);
    }
}
