using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class SurfaceDensity3 : IEquatable<SurfaceDensity3>, IQuantity3
    {
        public SurfaceDensity X { get; }
        public SurfaceDensity Y { get; }
        public SurfaceDensity Z { get; }

        Scalar IQuantity3.X => X.Magnitude;
        Scalar IQuantity3.Y => Y.Magnitude;
        Scalar IQuantity3.Z => Z.Magnitude;

        public SurfaceDensity3(Scalar3 components)
        {
            X = new SurfaceDensity(components.X);
            Y = new SurfaceDensity(components.Y);
            Z = new SurfaceDensity(components.Z);
        }

        public SurfaceDensity3(Scalar3 components, UnitOfMass massUnit, UnitOfLength lengthUnit)
        {
            X = new SurfaceDensity(components.X, massUnit, lengthUnit);
            Y = new SurfaceDensity(components.Y, massUnit, lengthUnit);
            Z = new SurfaceDensity(components.Z, massUnit, lengthUnit);
        }

        public SurfaceDensity3(Scalar3 components, (UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfLength unit, MetricPrefix prefix) length)
        {
            X = new SurfaceDensity(components.X, mass, length);
            Y = new SurfaceDensity(components.Y, mass, length);
            Z = new SurfaceDensity(components.Z, mass, length);
        }

        public SurfaceDensity3(SurfaceDensity x, SurfaceDensity y, SurfaceDensity z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public SurfaceDensity3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public SurfaceDensity Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(SurfaceDensity3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(SurfaceDensity3 other) => Cross(this, other);
        public SurfaceDensity3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(SurfaceDensity3 a, SurfaceDensity3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(SurfaceDensity3 a, SurfaceDensity3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static SurfaceDensity3 Transform(SurfaceDensity3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new SurfaceDensity(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new SurfaceDensity(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new SurfaceDensity(transform.M43)
            );
        }

        public bool Equals(SurfaceDensity3 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

        public override bool Equals(object? obj)
        {
            if (obj is SurfaceDensity3 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.KilogramsPerSquareMetre}, {Y.KilogramsPerSquareMetre}, {Z.KilogramsPerSquareMetre}) [kg/(m^2)]";

        public static bool operator ==(SurfaceDensity3? a, SurfaceDensity3? b)
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

        public static bool operator !=(SurfaceDensity3? a, SurfaceDensity3? b) => !(a == b);

        public static SurfaceDensity3 operator +(SurfaceDensity3 a) => a;
        public static SurfaceDensity3 operator -(SurfaceDensity3 a) => new(-a.X, -a.Y, -a.Z);
        public static SurfaceDensity3 operator +(SurfaceDensity3 a, SurfaceDensity3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static SurfaceDensity3 operator -(SurfaceDensity3 a, SurfaceDensity3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static SurfaceDensity3 operator %(SurfaceDensity3 a, SurfaceDensity3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(SurfaceDensity3 a, SurfaceDensity3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(SurfaceDensity3 a, SurfaceDensity3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Mass3 operator *(SurfaceDensity3 a, Area3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Density3 operator /(SurfaceDensity3 a, Length3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static SurfaceDensity3 operator *(SurfaceDensity3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static SurfaceDensity3 operator *(Scalar3 a, SurfaceDensity3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static SurfaceDensity3 operator /(SurfaceDensity3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(SurfaceDensity3 a, IQuantity3 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude, (a.Z * b.Z).Magnitude);
        public static UnhandledQuantity3 operator /(SurfaceDensity3 a, IQuantity3 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude, (a.Z / b.Z).Magnitude);

        public static Mass3 operator *(SurfaceDensity3 a, Area b) => new(a.X * b, a.Y * b, a.Y * b);
        public static Density3 operator /(SurfaceDensity3 a, Length b) => new(a.X / b, a.Y / b, a.Z / b);

        public static Scalar3 operator /(SurfaceDensity3 a, SurfaceDensity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(SurfaceDensity a, SurfaceDensity3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static SurfaceDensity3 operator *(SurfaceDensity3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static SurfaceDensity3 operator *(Scalar a, SurfaceDensity3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static SurfaceDensity3 operator /(SurfaceDensity3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(SurfaceDensity3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, SurfaceDensity3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(SurfaceDensity3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, SurfaceDensity3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
