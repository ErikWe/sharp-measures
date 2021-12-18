using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Jerk2 : IEquatable<Jerk2>, IQuantity2
    {
        public Jerk X { get; }
        public Jerk Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public Jerk2(Scalar2 components)
        {
            X = new Jerk(components.X);
            Y = new Jerk(components.Y);
        }

        public Jerk2(Scalar2 components, UnitOfLength lengthUnit, UnitOfTime timeUnit)
        {
            X = new Jerk(components.X, lengthUnit, timeUnit);
            Y = new Jerk(components.Y, lengthUnit, timeUnit);
        }

        public Jerk2(Scalar2 components, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            X = new Jerk(components.X, length, time);
            Y = new Jerk(components.Y, length, time);
        }

        public Jerk2(Jerk x, Jerk y)
        {
            X = x;
            Y = y;
        }

        public Jerk2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Jerk Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Jerk2 other) => Dot(this, other);
        public static UnhandledQuantity Dot(Jerk2 a, Jerk2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Jerk2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is Jerk2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.MetresPerCubicSecond}, {Y.MetresPerCubicSecond}) [m/(s^2)]";

        public static bool operator ==(Jerk2? a, Jerk2? b)
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

        public static bool operator !=(Jerk2? a, Jerk2? b) => !(a == b);

        public static Jerk2 operator +(Jerk2 a) => a;
        public static Jerk2 operator -(Jerk2 a) => new(-a.X, -a.Y);
        public static Jerk2 operator +(Jerk2 a, Jerk2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Jerk2 operator -(Jerk2 a, Jerk2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Jerk2 operator %(Jerk2 a, Jerk2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(Jerk2 a, Jerk2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Jerk2 a, Jerk2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Jerk2 operator *(Jerk2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Jerk2 operator *(Scalar2 a, Jerk2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Jerk2 operator /(Jerk2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Jerk2 a, IQuantity2 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude);
        public static UnhandledQuantity2 operator /(Jerk2 a, IQuantity2 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude);

        public static Acceleration2 operator *(Jerk2 a, Time b) => new(a.X * b, a.Y * b);
        public static Acceleration2 operator *(Time a, Jerk2 b) => new(a * b.X, a * b.Y);

        public static Scalar2 operator /(Jerk2 a, Jerk b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Jerk a, Jerk2 b) => new(a / b.X, a / b.Y);

        public static Jerk2 operator *(Jerk2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Jerk2 operator *(Scalar a, Jerk2 b) => new(a * b.X, a * b.Y);
        public static Jerk2 operator /(Jerk2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Jerk2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, Jerk2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(Jerk2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, Jerk2 b) => new(b.X / a, b.Y / a);
    }
}
