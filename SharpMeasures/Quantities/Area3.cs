using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Area3 : IEquatable<Area3>, IQuantity3<Area>
    {
        public Area X { get; }
        public Area Y { get; }
        public Area Z { get; }

        Scalar IQuantity3.XMagnitude => X.Magnitude;
        Scalar IQuantity3.YMagnitude => Y.Magnitude;
        Scalar IQuantity3.ZMagnitude => Z.Magnitude;

        public Area3(Scalar3 components)
        {
            X = new Area(components.X);
            Y = new Area(components.Y);
            Z = new Area(components.Z);
        }

        public Area3(Scalar3 components, UnitOfArea unit)
        {
            X = new Area(components.X, unit);
            Y = new Area(components.Y, unit);
            Z = new Area(components.Z, unit);
        }

        public Area3(Scalar3 components, UnitOfArea unit, MetricPrefix prefix)
        {
            X = new Area(components.X, unit, prefix);
            Y = new Area(components.Y, unit, prefix);
            Z = new Area(components.Z, unit, prefix);
        }

        public Area3(Scalar3 components, UnitOfLength unit)
        {
            X = new Area(components.X, unit);
            Y = new Area(components.Y, unit);
            Z = new Area(components.Z, unit);
        }

        public Area3(Scalar3 components, UnitOfLength unit, MetricPrefix prefix)
        {
            X = new Area(components.X, unit, prefix);
            Y = new Area(components.Y, unit, prefix);
            Z = new Area(components.Z, unit, prefix);
        }

        public Area3(Area x, Area y, Area z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Area3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Area Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Area3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(Area3 other) => Cross(this, other);
        public Area3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(Area3 a, Area3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(Area3 a, Area3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Area3 Transform(Area3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Area(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Area(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Area(transform.M43)
            );
        }

        public bool Equals(Area3? other) => X.Equals(other?.X) && Y.Equals(other?.Y) && Z.Equals(other?.Z);
        public override bool Equals(object? obj) => Equals(obj as Area3);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.SquareMetres}, {Y.SquareMetres}, {Z.SquareMetres}) [m^2]";

        public static bool operator ==(Area3? a, Area3? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Area3? a, Area3? b) => !(a == b);

        public static Area3 operator +(Area3 a) => a;
        public static Area3 operator -(Area3 a) => new(-a.X, -a.Y, -a.Z);
        public static Area3 operator +(Area3 a, Area3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Area3 operator -(Area3 a, Area3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Area3 operator %(Area3 a, Area3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(Area3 a, Area3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Area3 a, Area3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Length3 operator /(Area3 a, Length3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Area3 operator *(Area3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Area3 operator *(Scalar3 a, Area3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Area3 operator /(Area3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Area3 a, IQuantity3 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude, a.Z * b.ZMagnitude);
        public static UnhandledQuantity3 operator /(Area3 a, IQuantity3 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude, a.Z / b.ZMagnitude);

        public static Scalar3 operator /(Area3 a, Area b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Area a, Area3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Length3 operator /(Area3 a, Length b) => new(a.X / b, a.Y / b, a.Z / b);

        public static Area3 operator *(Area3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Area3 operator *(Scalar a, Area3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Area3 operator /(Area3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Area3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Area3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Area3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Area3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
