using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Acceleration2 : IEquatable<Acceleration2>, IQuantity2
    {
        public Acceleration X { get; }
        public Acceleration Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public Acceleration2(Scalar2 components)
        {
            X = new Acceleration(components.X);
            Y = new Acceleration(components.Y);
        }

        public Acceleration2(Scalar2 components, UnitOfLength lengthUnit, UnitOfTime timeUnit)
        {
            X = new Acceleration(components.X, lengthUnit, timeUnit);
            Y = new Acceleration(components.Y, lengthUnit, timeUnit);
        }

        public Acceleration2(Scalar2 components, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            X = new Acceleration(components.X, length, time);
            Y = new Acceleration(components.Y, length, time);
        }

        public Acceleration2(Acceleration x, Acceleration y)
        {
            X = x;
            Y = y;
        }

        public Acceleration2 Normalize() => this / Magnitude().Magnitude;

        public UnhandledQuantity SquaredMagnitude() => Dot(this);

        public Acceleration Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);

        public UnhandledQuantity Dot(Acceleration2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(Acceleration2 a, Acceleration2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Acceleration2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is Acceleration2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.MetresPerSecondSquared}, {Y.MetresPerSecondSquared}) [m/(s^2)]";

        public static bool operator ==(Acceleration2? a, Acceleration2? b)
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

        public static bool operator !=(Acceleration2? a, Acceleration2? b) => !(a == b);

        public static Acceleration2 operator +(Acceleration2 a) => a;

        public static Acceleration2 operator -(Acceleration2 a) => new(-a.X, -a.Y);

        public static Acceleration2 operator +(Acceleration2 a, Acceleration2 b) => new(a.X + b.X, a.Y + b.Y);

        public static Acceleration2 operator -(Acceleration2 a, Acceleration2 b) => new(a.X - b.X, a.Y - b.Y);

        public static Acceleration2 operator %(Acceleration2 a, Acceleration2 b) => new(a.X % b.X, a.Y % b.Y);

        public static UnhandledQuantity2 operator *(Acceleration2 a, Acceleration2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Scalar2 operator /(Acceleration2 a, Acceleration2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Acceleration2 operator *(Acceleration2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Acceleration2 operator *(Scalar2 a, Acceleration2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Acceleration2 operator /(Acceleration2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Acceleration2 a, IQuantity2 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude);

        public static UnhandledQuantity2 operator /(Acceleration2 a, IQuantity2 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude);

        public static Velocity2 operator *(Acceleration2 a, Time b) => new(a.X * b, a.Y * b);

        public static Velocity2 operator *(Time a, Acceleration2 b) => new(a * b.X, a * b.Y);

        public static Jerk2 operator *(Acceleration2 a, Frequency b) => new(a.X * b, a.Y * b);

        public static Jerk2 operator *(Frequency a, Acceleration2 b) => new(a * b.X, a * b.Y);

        public static Velocity2 operator /(Acceleration2 a, Frequency b) => new(a.X / b, a.Y / b);

        public static Jerk2 operator /(Acceleration2 a, Time b) => new(a.X / b, a.Y / b);

        public static Scalar2 operator /(Acceleration2 a, Acceleration b) => new(a.X / b, a.Y / b);

        public static Scalar2 operator /(Acceleration a, Acceleration2 b) => new(a / b.X, a / b.Y);

        public static Acceleration2 operator *(Acceleration2 a, Scalar b) => new(a.X * b, a.Y * b);

        public static Acceleration2 operator *(Scalar a, Acceleration2 b) => new(a * b.X, a * b.Y);

        public static Acceleration2 operator /(Acceleration2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Acceleration2 a, IQuantity b) => new(a.X * b, a.Y * b);

        public static UnhandledQuantity2 operator *(IQuantity a, Acceleration2 b) => new(b.X * a, b.Y * a);

        public static UnhandledQuantity2 operator /(Acceleration2 a, IQuantity b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator /(IQuantity a, Acceleration2 b) => new(b.X / a, b.Y / a);
    }
}
