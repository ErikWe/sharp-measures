using System;

namespace ErikWe.SharpMeasures.Quantities.Definitions
{
    public class UnhandledQuantity2 : IEquatable<UnhandledQuantity2>, IQuantity2<UnhandledQuantity>
    {
        public UnhandledQuantity X { get; }
        public UnhandledQuantity Y { get; }

        Scalar IQuantity2.XMagnitude => X.Magnitude;
        Scalar IQuantity2.YMagnitude => Y.Magnitude;

        public UnhandledQuantity2(Scalar2 components)
        {
            X = new UnhandledQuantity(components.X);
            Y = new UnhandledQuantity(components.Y);
        }

        public UnhandledQuantity2(IQuantity2 components)
        {
            X = new UnhandledQuantity(components.XMagnitude);
            Y = new UnhandledQuantity(components.YMagnitude);
        }

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

        public bool Equals(UnhandledQuantity2? other) => X.Equals(other?.X) && Y.Equals(other?.Y);
        public override bool Equals(object? obj) => Equals(obj as UnhandledQuantity2);

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X}, {Y}) [undef]";

        public static bool operator ==(UnhandledQuantity2? a, UnhandledQuantity2? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(UnhandledQuantity2? a, UnhandledQuantity2? b) => !(a == b);

        public static UnhandledQuantity2 operator +(UnhandledQuantity2 a) => a;
        public static UnhandledQuantity2 operator -(UnhandledQuantity2 a) => new(-a.X, -a.Y);
        public static UnhandledQuantity2 operator +(UnhandledQuantity2 a, UnhandledQuantity2 b) => new(a.X + b.X, a.Y + b.Y);
        public static UnhandledQuantity2 operator -(UnhandledQuantity2 a, UnhandledQuantity2 b) => new(a.X - b.X, a.Y - b.Y);
        public static UnhandledQuantity2 operator %(UnhandledQuantity2 a, UnhandledQuantity2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(UnhandledQuantity2 a, UnhandledQuantity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static UnhandledQuantity2 operator /(UnhandledQuantity2 a, UnhandledQuantity2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(UnhandledQuantity2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static UnhandledQuantity2 operator *(Scalar2 a, UnhandledQuantity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static UnhandledQuantity2 operator /(UnhandledQuantity2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);
        public static UnhandledQuantity2 operator /(Scalar2 a, UnhandledQuantity2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(UnhandledQuantity2 a, IQuantity2 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude);
        public static UnhandledQuantity2 operator /(UnhandledQuantity2 a, IQuantity2 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude);

        public static UnhandledQuantity2 operator *(UnhandledQuantity2 a, UnhandledQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(UnhandledQuantity a, UnhandledQuantity2 b) => new(a * b.X, a * b.Y);
        public static UnhandledQuantity2 operator /(UnhandledQuantity2 a, UnhandledQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(UnhandledQuantity a, UnhandledQuantity2 b) => new(a / b.X, a / b.Y);

        public static UnhandledQuantity2 operator *(UnhandledQuantity2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(Scalar a, UnhandledQuantity2 b) => new(a * b.X, a * b.Y);
        public static UnhandledQuantity2 operator /(UnhandledQuantity2 a, Scalar b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(Scalar a, UnhandledQuantity2 b) => new(a / b.X, a / b.Y);

        public static UnhandledQuantity2 operator *(UnhandledQuantity2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator /(UnhandledQuantity2 a, IQuantity b) => new(a.X / b, a.Y / b);
    }
}
