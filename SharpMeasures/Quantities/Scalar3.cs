using ErikWe.SharpMeasures.Quantities.Definitions;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Scalar3 : IEquatable<Scalar3>, IQuantity3<Scalar>
    {
        public Scalar X { get; }
        public Scalar Y { get; }
        public Scalar Z { get; }

        Scalar IQuantity3.XMagnitude => X;
        Scalar IQuantity3.YMagnitude => Y;
        Scalar IQuantity3.ZMagnitude => Z;

        public Scalar3(Scalar x, Scalar y, Scalar z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Scalar3 Normalize() => this / Magnitude();
        public Scalar SquaredMagnitude() => Dot(this);
        public Scalar Magnitude() => SquaredMagnitude().Sqrt();
        public Scalar Dot(Scalar3 other) => Dot(this, other);
        public Scalar3 Cross(Scalar3 other) => Cross(this, other);
        public Scalar3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static Scalar Dot(Scalar3 a, Scalar3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static Scalar3 Cross(Scalar3 a, Scalar3 b)
        {
            return new Scalar3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Scalar3 Transform(Scalar3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + transform.M41,
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + transform.M42,
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + transform.M43
            );
        }

        public bool Equals(Scalar3? other) => X.Equals(other?.X) && Y.Equals(other?.Y) && Z.Equals(other?.Z);
        public override bool Equals(object? obj) => Equals(obj as Scalar3);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X}, {Y}, {Z})";

        public static bool operator ==(Scalar3? a, Scalar3? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Scalar3? a, Scalar3? b) => !(a == b);

        public static Scalar3 operator +(Scalar3 a) => a;
        public static Scalar3 operator -(Scalar3 a) => new(-a.X, -a.Y, -a.Z);
        public static Scalar3 operator +(Scalar3 a, Scalar3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Scalar3 operator -(Scalar3 a, Scalar3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Scalar3 operator %(Scalar3 a, Scalar3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static Scalar3 operator *(Scalar3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Scalar3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Scalar3 operator *(Scalar3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Scalar3 operator *(Scalar a, Scalar3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Scalar3 operator /(Scalar3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Scalar a, Scalar3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static implicit operator Scalar3((double x, double y, double z) a) => new(a.x, a.y, a.z);
    }
}
