using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Time2 : IEquatable<Time2>, IQuantity2
    {
        public Time X { get; }
        public Time Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public Time2(Scalar2 components)
        {
            X = new Time(components.X);
            Y = new Time(components.Y);
        }

        public Time2(Scalar2 components, UnitOfTime unit)
        {
            X = new Time(components.X, unit);
            Y = new Time(components.Y, unit);
        }

        public Time2(Scalar2 components, UnitOfTime unit, MetricPrefix prefix)
        {
            X = new Time(components.X, unit, prefix);
            Y = new Time(components.Y, unit, prefix);
        }

        public Time2(Time x, Time y)
        {
            X = x;
            Y = y;
        }

        public Time2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Time Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Time2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(Time2 a, Time2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Time2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is Time2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.Seconds}, {Y.Seconds}) [s]";

        public static bool operator ==(Time2? a, Time2? b)
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

        public static bool operator !=(Time2? a, Time2? b) => !(a == b);

        public static Time2 operator +(Time2 a) => a;
        public static Time2 operator -(Time2 a) => new(-a.X, -a.Y);
        public static Time2 operator +(Time2 a, Time2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Time2 operator -(Time2 a, Time2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Time2 operator %(Time2 a, Time2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(Time2 a, Time2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Time2 a, Time2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Time2 operator *(Time2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Time2 operator *(Scalar2 a, Time2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Time2 operator /(Time2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Time2 a, IQuantity2 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude);
        public static UnhandledQuantity2 operator /(Time2 a, IQuantity2 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude);

        public static Length2 operator *(Time2 a, Velocity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Velocity2 operator *(Time2 a, Acceleration2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Acceleration2 operator *(Time2 a, Jerk2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Scalar2 operator /(Time2 a, Time b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Time a, Time2 b) => new(a / b.X, a / b.Y);

        public static Time2 operator *(Time2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Time2 operator *(Scalar a, Time2 b) => new(a * b.X, a * b.Y);
        public static Time2 operator /(Time2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Time2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, Time2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(Time2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, Time2 b) => new(b.X / a, b.Y / a);
    }
}
