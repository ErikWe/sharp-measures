using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities.Definitions
{
    public class UnhandledQuantity3 : IEquatable<UnhandledQuantity3>, IQuantity3<UnhandledQuantity>
    {
        public UnhandledQuantity X { get; }
        public UnhandledQuantity Y { get; }
        public UnhandledQuantity Z { get; }

        Scalar IQuantity3.XMagnitude => X.Magnitude;
        Scalar IQuantity3.YMagnitude => Y.Magnitude;
        Scalar IQuantity3.ZMagnitude => Z.Magnitude;

        public UnhandledQuantity3(Scalar3 components)
        {
            X = new UnhandledQuantity(components.X);
            Y = new UnhandledQuantity(components.Y);
            Z = new UnhandledQuantity(components.Z);
        }

        public UnhandledQuantity3(IQuantity3 components)
        {
            X = new UnhandledQuantity(components.XMagnitude);
            Y = new UnhandledQuantity(components.YMagnitude);
            Z = new UnhandledQuantity(components.ZMagnitude);
        }

        public UnhandledQuantity3(Scalar x, Scalar y, Scalar z)
        {
            X = new UnhandledQuantity(x);
            Y = new UnhandledQuantity(y);
            Z = new UnhandledQuantity(z);
        }

        public UnhandledQuantity3(IQuantity x, IQuantity y, IQuantity z)
        {
            X = new UnhandledQuantity(x.Magnitude);
            Y = new UnhandledQuantity(y.Magnitude);
            Z = new UnhandledQuantity(z.Magnitude);
        }

        public UnhandledQuantity3(UnhandledQuantity x, UnhandledQuantity y, UnhandledQuantity z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public UnhandledQuantity3 Normalize() => this / Magnitude();
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public UnhandledQuantity Magnitude() => SquaredMagnitude().Sqrt();
        public UnhandledQuantity Dot(UnhandledQuantity3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(UnhandledQuantity3 other) => Cross(this, other);
        public UnhandledQuantity3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(UnhandledQuantity3 a, UnhandledQuantity3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(UnhandledQuantity3 a, UnhandledQuantity3 b)
        {
            return new UnhandledQuantity3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static UnhandledQuantity3 Transform(UnhandledQuantity3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new UnhandledQuantity(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new UnhandledQuantity(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new UnhandledQuantity(transform.M43)
            );
        }

        public bool Equals(UnhandledQuantity3? other) => X.Equals(other?.X) && Y.Equals(other?.Y) && Z.Equals(other?.Z);
        public override bool Equals(object? obj) => Equals(obj as UnhandledQuantity3);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X}, {Y}, {Z}) [undef]";

        public static bool operator ==(UnhandledQuantity3? a, UnhandledQuantity3? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(UnhandledQuantity3? a, UnhandledQuantity3? b) => !(a == b);

        public static UnhandledQuantity3 operator +(UnhandledQuantity3 a) => a;
        public static UnhandledQuantity3 operator -(UnhandledQuantity3 a) => new(-a.X, -a.Y, -a.Z);
        public static UnhandledQuantity3 operator +(UnhandledQuantity3 a, UnhandledQuantity3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static UnhandledQuantity3 operator -(UnhandledQuantity3 a, UnhandledQuantity3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static UnhandledQuantity3 operator %(UnhandledQuantity3 a, UnhandledQuantity3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(UnhandledQuantity3 a, UnhandledQuantity3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static UnhandledQuantity3 operator /(UnhandledQuantity3 a, UnhandledQuantity3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(UnhandledQuantity3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static UnhandledQuantity3 operator *(Scalar3 a, UnhandledQuantity3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static UnhandledQuantity3 operator /(UnhandledQuantity3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        public static UnhandledQuantity3 operator /(Scalar3 a, UnhandledQuantity3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(UnhandledQuantity3 a, IQuantity3 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude, a.Z * b.ZMagnitude);
        public static UnhandledQuantity3 operator /(UnhandledQuantity3 a, IQuantity3 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude, a.Z / b.ZMagnitude);

        public static UnhandledQuantity3 operator *(UnhandledQuantity3 a, UnhandledQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(UnhandledQuantity a, UnhandledQuantity3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static UnhandledQuantity3 operator /(UnhandledQuantity3 a, UnhandledQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(UnhandledQuantity a, UnhandledQuantity3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static UnhandledQuantity3 operator *(UnhandledQuantity3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(Scalar a, UnhandledQuantity3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static UnhandledQuantity3 operator /(UnhandledQuantity3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(Scalar a, UnhandledQuantity3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static UnhandledQuantity3 operator *(UnhandledQuantity3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator /(UnhandledQuantity3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
    }
}
