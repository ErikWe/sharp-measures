using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Volume3 : IEquatable<Volume3>, IQuantity3<Volume>
    {
        public Volume X { get; }
        public Volume Y { get; }
        public Volume Z { get; }

        Scalar IQuantity3.XMagnitude => X.Magnitude;
        Scalar IQuantity3.YMagnitude => Y.Magnitude;
        Scalar IQuantity3.ZMagnitude => Z.Magnitude;

        public Volume3(Scalar3 components)
        {
            X = new Volume(components.X);
            Y = new Volume(components.Y);
            Z = new Volume(components.Z);
        }

        public Volume3(Scalar3 components, UnitOfVolume unit)
        {
            X = new Volume(components.X, unit);
            Y = new Volume(components.Y, unit);
            Z = new Volume(components.Z, unit);
        }

        public Volume3(Scalar3 components, UnitOfVolume unit, MetricPrefix prefix)
        {
            X = new Volume(components.X, unit, prefix);
            Y = new Volume(components.Y, unit, prefix);
            Z = new Volume(components.Z, unit, prefix);
        }

        public Volume3(Scalar3 components, UnitOfLength unit)
        {
            X = new Volume(components.X, unit);
            Y = new Volume(components.Y, unit);
            Z = new Volume(components.Z, unit);
        }

        public Volume3(Scalar3 components, UnitOfLength unit, MetricPrefix prefix)
        {
            X = new Volume(components.X, unit, prefix);
            Y = new Volume(components.Y, unit, prefix);
            Z = new Volume(components.Z, unit, prefix);
        }

        public Volume3(Volume x, Volume y, Volume z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Volume3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Volume Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Volume3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(Volume3 other) => Cross(this, other);
        public Volume3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(Volume3 a, Volume3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(Volume3 a, Volume3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Volume3 Transform(Volume3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Volume(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Volume(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Volume(transform.M43)
            );
        }

        public bool Equals(Volume3? other) => X.Equals(other?.X) && Y.Equals(other?.Y) && Z.Equals(other?.Z);
        public override bool Equals(object? obj) => Equals(obj as Volume3);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.CubicMetres}, {Y.CubicMetres}, {Z.CubicMetres}) [m^3]";

        public static bool operator ==(Volume3? a, Volume3? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Volume3? a, Volume3? b) => !(a == b);

        public static Volume3 operator +(Volume3 a) => a;
        public static Volume3 operator -(Volume3 a) => new(-a.X, -a.Y, -a.Z);
        public static Volume3 operator +(Volume3 a, Volume3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Volume3 operator -(Volume3 a, Volume3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Volume3 operator %(Volume3 a, Volume3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(Volume3 a, Volume3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Volume3 a, Volume3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Area3 operator /(Volume3 a, Length3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        public static Length3 operator /(Volume3 a, Area3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Volume3 operator *(Volume3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Volume3 operator *(Scalar3 a, Volume3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Volume3 operator /(Volume3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Volume3 a, IQuantity3 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude, a.Z * b.ZMagnitude);
        public static UnhandledQuantity3 operator /(Volume3 a, IQuantity3 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude, a.Z / b.ZMagnitude);

        public static Area3 operator /(Volume3 a, Length b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Length3 operator /(Volume3 a, Area b) => new(a.X / b, a.Y / b, a.Z / b);

        public static Scalar3 operator /(Volume3 a, Volume b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Volume a, Volume3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Volume3 operator *(Volume3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Volume3 operator *(Scalar a, Volume3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Volume3 operator /(Volume3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Volume3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Volume3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Volume3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Volume3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
