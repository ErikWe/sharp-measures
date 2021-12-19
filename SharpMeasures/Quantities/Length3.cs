using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Length3 : IEquatable<Length3>, IQuantity3<Length>
    {
        public Length X { get; }
        public Length Y { get; }
        public Length Z { get; }

        Scalar IQuantity3.XMagnitude => X.Magnitude;
        Scalar IQuantity3.YMagnitude => Y.Magnitude;
        Scalar IQuantity3.ZMagnitude => Z.Magnitude;

        public Length3(Scalar3 components)
        {
            X = new Length(components.X);
            Y = new Length(components.Y);
            Z = new Length(components.Z);
        }

        public Length3(Scalar3 components, UnitOfLength unit)
        {
            X = new Length(components.X, unit);
            Y = new Length(components.Y, unit);
            Z = new Length(components.Z, unit);
        }

        public Length3(Scalar3 components, UnitOfLength unit, MetricPrefix prefix)
        {
            X = new Length(components.X, unit, prefix);
            Y = new Length(components.Y, unit, prefix);
            Z = new Length(components.Z, unit, prefix);
        }

        public Length3(Length x, Length y, Length z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Length3 Normalize() => this / Magnitude().Magnitude;
        public Area SquaredMagnitude() => Dot(this);
        public Length Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public Area Dot(Length3 other) => Dot(this, other);
        public Area3 Cross(Length3 other) => Cross(this, other);
        public Length3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static Area Dot(Length3 a, Length3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static Area3 Cross(Length3 a, Length3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Length3 Transform(Length3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Length(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Length(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Length(transform.M43)
            );
        }

        public bool Equals(Length3? other) => X.Equals(other?.X) && Y.Equals(other?.Y) && Z.Equals(other?.Z);
        public override bool Equals(object? obj) => Equals(obj as Length3);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.Metres}, {Y.Metres}, {Z.Metres}) [m]";

        public static bool operator ==(Length3? a, Length3? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Length3? a, Length3? b) => !(a == b);

        public static Length3 operator +(Length3 a) => a;
        public static Length3 operator -(Length3 a) => new(-a.X, -a.Y, -a.Z);
        public static Length3 operator +(Length3 a, Length3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Length3 operator -(Length3 a, Length3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Length3 operator %(Length3 a, Length3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static Area3 operator *(Length3 a, Length3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Length3 a, Length3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Length3 operator *(Length3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Length3 operator *(Scalar3 a, Length3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Length3 operator /(Length3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Length3 a, IQuantity3 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude, a.Z * b.ZMagnitude);
        public static UnhandledQuantity3 operator /(Length3 a, IQuantity3 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude, a.Z / b.ZMagnitude);

        public static Velocity3 operator /(Length3 a, Time b) => new(a.X / b, a.Y / b, a.Z / b);

        public static Area3 operator *(Length3 a, Length b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Area3 operator *(Length a, Length3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Scalar3 operator /(Length3 a, Length b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Length a, Length3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Length3 operator *(Length3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Length3 operator *(Scalar a, Length3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Length3 operator /(Length3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Length3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Length3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Length3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Length3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
