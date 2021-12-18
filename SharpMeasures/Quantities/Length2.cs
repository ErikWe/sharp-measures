using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Length2 : IEquatable<Length2>, IQuantity2
    {
        public Length X { get; }
        public Length Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public Length2(Scalar2 components)
        {
            X = new Length(components.X);
            Y = new Length(components.Y);
        }

        public Length2(Scalar2 components, UnitOfLength unit)
        {
            X = new Length(components.X, unit);
            Y = new Length(components.Y, unit);
        }

        public Length2(Scalar2 components, UnitOfLength unit, MetricPrefix prefix)
        {
            X = new Length(components.X, unit, prefix);
            Y = new Length(components.Y, unit, prefix);
        }

        public Length2(Length x, Length y)
        {
            X = x;
            Y = y;
        }

        public Length2 Normalize() => this / Magnitude().Magnitude;
        public Area SquaredMagnitude() => Dot(this);
        public Length Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public Area Dot(Length2 other) => Dot(this, other);

        public static Area Dot(Length2 a, Length2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Length2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is Length2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.Metres}, {Y.Metres}) [m]";

        public static bool operator ==(Length2? a, Length2? b)
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

        public static bool operator !=(Length2? a, Length2? b) => !(a == b);

        public static Length2 operator +(Length2 a) => a;
        public static Length2 operator -(Length2 a) => new(-a.X, -a.Y);
        public static Length2 operator +(Length2 a, Length2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Length2 operator -(Length2 a, Length2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Length2 operator %(Length2 a, Length2 b) => new(a.X % b.X, a.Y % b.Y);
        public static Area2 operator *(Length2 a, Length2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Length2 a, Length2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Length2 operator *(Length2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Length2 operator *(Scalar2 a, Length2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Length2 operator /(Length2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Length2 a, IQuantity2 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude);
        public static UnhandledQuantity2 operator /(Length2 a, IQuantity2 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude);

        public static Velocity2 operator /(Length2 a, Time b) => new(a.X / b, a.Y / b);

        public static Area2 operator *(Length2 a, Length b) => new(a.X * b, a.Y * b);
        public static Area2 operator *(Length a, Length2 b) => new(a * b.X, a * b.Y);
        public static Scalar2 operator /(Length2 a, Length b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Length a, Length2 b) => new(a / b.X, a / b.Y);

        public static Length2 operator *(Length2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Length2 operator *(Scalar a, Length2 b) => new(a * b.X, a * b.Y);
        public static Length2 operator /(Length2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Length2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, Length2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(Length2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, Length2 b) => new(b.X / a, b.Y / a);
    }
}
