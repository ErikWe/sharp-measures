using System;

namespace ErikWe.SharpMeasures.Quantities.Definitions
{
    public struct UnhandledQuantity2 : IEquatable<UnhandledQuantity2>, IQuantity2
    {
        public UnhandledQuantity X { get; }
        public UnhandledQuantity Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public UnhandledQuantity2(Scalar x, Scalar y)
        {
            X = new UnhandledQuantity(x);
            Y = new UnhandledQuantity(y);
        }

        public UnhandledQuantity2(IQuantity x, IQuantity y)
        {
            X = new UnhandledQuantity(x.Magnitude);
            Y = new UnhandledQuantity(y.Magnitude);
        }

        public UnhandledQuantity2(UnhandledQuantity x, UnhandledQuantity y)
        {
            X = x;
            Y = y;
        }

        public UnhandledQuantity2 Normalize() => this / Magnitude();
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public UnhandledQuantity Magnitude() => SquaredMagnitude().Sqrt();
        public UnhandledQuantity Dot(UnhandledQuantity2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(UnhandledQuantity2 a, UnhandledQuantity2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(UnhandledQuantity2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is UnhandledQuantity2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X}, {Y}) [undef]";

        public static bool operator ==(UnhandledQuantity2? a, UnhandledQuantity2? b)
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

        public static bool operator !=(UnhandledQuantity2? a, UnhandledQuantity2? b) => !(a == b);

        public static UnhandledQuantity2 operator +(UnhandledQuantity2 a) => a;
        public static UnhandledQuantity2 operator -(UnhandledQuantity2 a) => new(-a.X, -a.Y);
        public static UnhandledQuantity2 operator +(UnhandledQuantity2 a, UnhandledQuantity2 b) => new(a.X + b.X, a.Y + b.Y);
        public static UnhandledQuantity2 operator -(UnhandledQuantity2 a, UnhandledQuantity2 b) => new(a.X - b.X, a.Y - b.Y);
        public static UnhandledQuantity2 operator %(UnhandledQuantity2 a, UnhandledQuantity2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(UnhandledQuantity2 a, UnhandledQuantity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static UnhandledQuantity2 operator /(UnhandledQuantity2 a, UnhandledQuantity2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(UnhandledQuantity2 a, UnhandledQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(UnhandledQuantity a, UnhandledQuantity2 b) => new(a * b.X, a * b.Y);
        public static UnhandledQuantity2 operator /(UnhandledQuantity2 a, UnhandledQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(UnhandledQuantity a, UnhandledQuantity2 b) => new(a / b.X, a / b.Y);

        public static UnhandledQuantity2 operator *(UnhandledQuantity2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator /(UnhandledQuantity2 a, IQuantity b) => new(a.X / b, a.Y / b);
    }
}
