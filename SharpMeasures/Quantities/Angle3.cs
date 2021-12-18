using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Angle3 : IEquatable<Angle3>, IQuantity3
    {
        public Angle X { get; }
        public Angle Y { get; }
        public Angle Z { get; }

        Scalar IQuantity3.X => X.Magnitude;
        Scalar IQuantity3.Y => Y.Magnitude;
        Scalar IQuantity3.Z => Z.Magnitude;

        public Angle3(Scalar3 components)
        {
            X = new Angle(components.X);
            Y = new Angle(components.Y);
            Z = new Angle(components.Z);
        }

        public Angle3(Scalar3 components, UnitOfAngle unit)
        {
            X = new Angle(components.X, unit);
            Y = new Angle(components.Y, unit);
            Z = new Angle(components.Z, unit);
        }

        public Angle3(Scalar3 components, UnitOfAngle unit, MetricPrefix prefix)
        {
            X = new Angle(components.X, unit, prefix);
            Y = new Angle(components.Y, unit, prefix);
            Z = new Angle(components.Z, unit, prefix);
        }

        public Angle3(Angle x, Angle y, Angle z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Angle3 Normalize() => this / Magnitude().Magnitude;
        public Angle3 NormalizeComponents()
        {
            return new(
                X.Normalize(),
                Y.Normalize(),
                Z.Normalize()
            );
        }

        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Angle Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Angle3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(Angle3 other) => Cross(this, other);
        public Angle3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(Angle3 a, Angle3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(Angle3 a, Angle3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Angle3 Transform(Angle3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Angle(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Angle(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Angle(transform.M43)
            );
        }

        public bool Equals(Angle3 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

        public override bool Equals(object? obj)
        {
            if (obj is Angle3 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.Radians}, {Y.Radians}, {Z.Radians}) [rad]";

        public static bool operator ==(Angle3? a, Angle3? b)
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

        public static bool operator !=(Angle3? a, Angle3? b) => !(a == b);

        public static Angle3 operator +(Angle3 a) => a;
        public static Angle3 operator -(Angle3 a) => new(-a.X, -a.Y, -a.Z);
        public static Angle3 operator +(Angle3 a, Angle3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Angle3 operator -(Angle3 a, Angle3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Angle3 operator %(Angle3 a, Angle3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(Angle3 a, Angle3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Angle3 a, Angle3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Angle3 operator *(Angle3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Angle3 operator *(Scalar3 a, Angle3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Angle3 operator /(Angle3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Angle3 a, IQuantity3 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude, (a.Z * b.Z).Magnitude);
        public static UnhandledQuantity3 operator /(Angle3 a, IQuantity3 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude, (a.Z / b.Z).Magnitude);

        public static Scalar3 operator /(Angle3 a, Angle b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Angle a, Angle3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Angle3 operator *(Angle3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Angle3 operator *(Scalar a, Angle3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Angle3 operator /(Angle3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Angle3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Angle3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Angle3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Angle3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
