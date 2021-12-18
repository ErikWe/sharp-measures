using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Scalar2 : IEquatable<Scalar2>, IQuantity2
    {
        public Scalar X { get; }
        public Scalar Y { get; }

        public Scalar2(Scalar x, Scalar y)
        {
            X = x;
            Y = y;
        }

        public Scalar2 Normalize() => this / Magnitude();
        public Scalar SquaredMagnitude() => Dot(this);
        public Scalar Magnitude() => SquaredMagnitude().Sqrt();
        public Scalar Dot(Scalar2 other) => Dot(this, other);

        public static Scalar Dot(Scalar2 a, Scalar2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Scalar2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is Scalar2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X}, {Y})";

        public static bool operator ==(Scalar2? a, Scalar2? b)
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

        public static bool operator !=(Scalar2? a, Scalar2? b) => !(a == b);

        public static Scalar2 operator +(Scalar2 a) => a;
        public static Scalar2 operator -(Scalar2 a) => new(-a.X, -a.Y);
        public static Scalar2 operator +(Scalar2 a, Scalar2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Scalar2 operator -(Scalar2 a, Scalar2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Scalar2 operator %(Scalar2 a, Scalar2 b) => new(a.X % b.X, a.Y % b.Y);
        public static Scalar2 operator *(Scalar2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Scalar2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Scalar2 operator *(Scalar2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Scalar2 operator *(Scalar a, Scalar2 b) => new(a * b.X, a * b.Y);
        public static Scalar2 operator /(Scalar2 a, Scalar b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Scalar a, Scalar2 b) => new(a / b.X, a / b.Y);

        public static implicit operator Scalar2((double x, double y) a) => new(a.x, a.y);
    }
}
