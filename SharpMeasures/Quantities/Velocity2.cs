using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Velocity2 : IEquatable<Velocity2>, IQuantity2
    {
        public Velocity X { get; }
        public Velocity Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public Velocity2(Scalar2 components)
        {
            X = new Velocity(components.X);
            Y = new Velocity(components.Y);
        }

        public Velocity2(Scalar2 components, UnitOfLength lengthUnit, UnitOfTime timeUnit)
        {
            X = new Velocity(components.X, lengthUnit, timeUnit);
            Y = new Velocity(components.Y, lengthUnit, timeUnit);
        }

        public Velocity2(Scalar2 components, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            X = new Velocity(components.X, length, time);
            Y = new Velocity(components.Y, length, time);
        }

        public Velocity2(Velocity x, Velocity y)
        {
            X = x;
            Y = y;
        }

        public Velocity2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Velocity Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Velocity2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(Velocity2 a, Velocity2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Velocity2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is Velocity2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.MetresPerSecond}, {Y.MetresPerSecond}) [m/s]";

        public static bool operator ==(Velocity2? a, Velocity2? b)
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

        public static bool operator !=(Velocity2? a, Velocity2? b) => !(a == b);

        public static Velocity2 operator +(Velocity2 a) => a;
        public static Velocity2 operator -(Velocity2 a) => new(-a.X, -a.Y);
        public static Velocity2 operator +(Velocity2 a, Velocity2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Velocity2 operator -(Velocity2 a, Velocity2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Velocity2 operator %(Velocity2 a, Velocity2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(Velocity2 a, Velocity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Velocity2 a, Velocity2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Velocity2 operator *(Velocity2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Velocity2 operator *(Scalar2 a, Velocity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Velocity2 operator /(Velocity2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Velocity2 a, IQuantity2 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude);
        public static UnhandledQuantity2 operator /(Velocity2 a, IQuantity2 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude);

        public static Length2 operator *(Velocity2 a, Time b) => new(a.X * b, a.Y * b);
        public static Length2 operator *(Time a, Velocity2 b) => new(a * b.X, a * b.Y);
        public static Acceleration2 operator /(Velocity2 a, Time b) => new(a.X / b, a.Y / b);

        public static Scalar2 operator /(Velocity2 a, Velocity b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Velocity a, Velocity2 b) => new(a / b.X, a / b.Y);

        public static Velocity2 operator *(Velocity2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Velocity2 operator *(Scalar a, Velocity2 b) => new(a * b.X, a * b.Y);
        public static Velocity2 operator /(Velocity2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Velocity2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, Velocity2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(Velocity2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, Velocity2 b) => new(b.X / a, b.Y / a);
    }
}
