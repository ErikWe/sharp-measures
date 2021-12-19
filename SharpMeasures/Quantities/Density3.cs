using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Density3 : IEquatable<Density3>, IQuantity3<Density>
    {
        public Density X { get; }
        public Density Y { get; }
        public Density Z { get; }

        Scalar IQuantity3.XMagnitude => X.Magnitude;
        Scalar IQuantity3.YMagnitude => Y.Magnitude;
        Scalar IQuantity3.ZMagnitude => Z.Magnitude;

        public Density3(Scalar3 components)
        {
            X = new Density(components.X);
            Y = new Density(components.Y);
            Z = new Density(components.Z);
        }

        public Density3(Scalar3 components, UnitOfMass massUnit, UnitOfLength lengthUnit)
        {
            X = new Density(components.X, massUnit, lengthUnit);
            Y = new Density(components.Y, massUnit, lengthUnit);
            Z = new Density(components.Z, massUnit, lengthUnit);
        }

        public Density3(Scalar3 components, (UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfLength unit, MetricPrefix prefix) length)
        {
            X = new Density(components.X, mass, length);
            Y = new Density(components.Y, mass, length);
            Z = new Density(components.Z, mass, length);
        }

        public Density3(Density x, Density y, Density z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Density3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Density Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Density3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(Density3 other) => Cross(this, other);
        public Density3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(Density3 a, Density3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(Density3 a, Density3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Density3 Transform(Density3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Density(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Density(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Density(transform.M43)
            );
        }

        public bool Equals(Density3? other) => X.Equals(other?.X) && Y.Equals(other?.Y) && Z.Equals(other?.Z);
        public override bool Equals(object? obj) => Equals(obj as Density3);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.KilogramsPerCubicMetre}, {Y.KilogramsPerCubicMetre}, {Z.KilogramsPerCubicMetre}) [kg/(m^3)]";

        public static bool operator ==(Density3? a, Density3? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Density3? a, Density3? b) => !(a == b);

        public static Density3 operator +(Density3 a) => a;
        public static Density3 operator -(Density3 a) => new(-a.X, -a.Y, -a.Z);
        public static Density3 operator +(Density3 a, Density3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Density3 operator -(Density3 a, Density3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Density3 operator %(Density3 a, Density3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(Density3 a, Density3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Density3 a, Density3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static SurfaceDensity3 operator *(Density3 a, Length3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Mass3 operator *(Density3 a, Volume3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

        public static Density3 operator *(Density3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Density3 operator *(Scalar3 a, Density3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Density3 operator /(Density3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Density3 a, IQuantity3 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude, a.Z * b.ZMagnitude);
        public static UnhandledQuantity3 operator /(Density3 a, IQuantity3 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude, a.Z / b.ZMagnitude);

        public static SurfaceDensity3 operator *(Density3 a, Length b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Mass3 operator *(Density3 a, Volume b) => new(a.X * b, a.Y * b, a.Z * b);

        public static Scalar3 operator /(Density3 a, Density b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Density a, Density3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Density3 operator *(Density3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Density3 operator *(Scalar a, Density3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Density3 operator /(Density3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Density3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Density3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Density3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Density3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
