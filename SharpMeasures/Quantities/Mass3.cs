using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Mass3 : IEquatable<Mass3>, IQuantity3<Mass>
    {
        public Mass X { get; }
        public Mass Y { get; }
        public Mass Z { get; }

        Scalar IQuantity3.XMagnitude => X.Magnitude;
        Scalar IQuantity3.YMagnitude => Y.Magnitude;
        Scalar IQuantity3.ZMagnitude => Z.Magnitude;

        public Mass3(Scalar3 components)
        {
            X = new Mass(components.X);
            Y = new Mass(components.Y);
            Z = new Mass(components.Z);
        }

        public Mass3(Scalar3 components, UnitOfMass unit)
        {
            X = new Mass(components.X, unit);
            Y = new Mass(components.Y, unit);
            Z = new Mass(components.Z, unit);
        }

        public Mass3(Scalar3 components, UnitOfMass unit, MetricPrefix prefix)
        {
            X = new Mass(components.X, unit, prefix);
            Y = new Mass(components.Y, unit, prefix);
            Z = new Mass(components.Z, unit, prefix);
        }

        public Mass3(Mass x, Mass y, Mass z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Mass3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Mass Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Mass3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(Mass3 other) => Cross(this, other);
        public Mass3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(Mass3 a, Mass3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(Mass3 a, Mass3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Mass3 Transform(Mass3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Mass(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Mass(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Mass(transform.M43)
            );
        }

        public bool Equals(Mass3? other) => X.Equals(other?.X) && Y.Equals(other?.Y) && Z.Equals(other?.Z);
        public override bool Equals(object? obj) => Equals(obj as Mass3);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.Kilograms}, {Y.Kilograms}, {Z.Kilograms}) [kg]";

        public static bool operator ==(Mass3? a, Mass3? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Mass3? a, Mass3? b) => !(a == b);

        public static Mass3 operator +(Mass3 a) => a;
        public static Mass3 operator -(Mass3 a) => new(-a.X, -a.Y, -a.Z);
        public static Mass3 operator +(Mass3 a, Mass3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Mass3 operator -(Mass3 a, Mass3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Mass3 operator %(Mass3 a, Mass3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(Mass3 a, Mass3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Mass3 a, Mass3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static SurfaceDensity3 operator /(Mass3 a, Area3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        public static Density3 operator /(Mass3 a, Volume3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Mass3 operator *(Mass3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Mass3 operator *(Scalar3 a, Mass3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Mass3 operator /(Mass3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Mass3 a, IQuantity3 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude, a.Z * b.ZMagnitude);
        public static UnhandledQuantity3 operator /(Mass3 a, IQuantity3 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude, a.Z / b.ZMagnitude);

        public static SurfaceDensity3 operator /(Mass3 a, Area b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Density3 operator /(Mass3 a, Volume b) => new(a.X / b, a.Y / b, a.Z / b);

        public static Scalar3 operator /(Mass3 a, Mass b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Mass a, Mass3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Mass3 operator *(Mass3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Mass3 operator *(Scalar a, Mass3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Mass3 operator /(Mass3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Mass3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Mass3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Mass3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Mass3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
