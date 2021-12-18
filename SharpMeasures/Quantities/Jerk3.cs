using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Jerk3 : IEquatable<Jerk3>, IQuantity3
    {
        public Jerk X { get; }
        public Jerk Y { get; }
        public Jerk Z { get; }

        Scalar IQuantity3.X => X.Magnitude;
        Scalar IQuantity3.Y => Y.Magnitude;
        Scalar IQuantity3.Z => Z.Magnitude;

        public Jerk3(Scalar3 components)
        {
            X = new Jerk(components.X);
            Y = new Jerk(components.Y);
            Z = new Jerk(components.Z);
        }

        public Jerk3(Scalar3 components, UnitOfLength lengthUnit, UnitOfTime timeUnit)
        {
            X = new Jerk(components.X, lengthUnit, timeUnit);
            Y = new Jerk(components.Y, lengthUnit, timeUnit);
            Z = new Jerk(components.Z, lengthUnit, timeUnit);
        }

        public Jerk3(Scalar3 components, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            X = new Jerk(components.X, length, time);
            Y = new Jerk(components.Y, length, time);
            Z = new Jerk(components.Z, length, time);
        }

        public Jerk3(Jerk x, Jerk y, Jerk z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Jerk3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Jerk Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Jerk3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(Jerk3 other) => Cross(this, other);
        public Jerk3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(Jerk3 a, Jerk3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(Jerk3 a, Jerk3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Jerk3 Transform(Jerk3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Jerk(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Jerk(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Jerk(transform.M43)
            );
        }

        public bool Equals(Jerk3 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

        public override bool Equals(object? obj)
        {
            if (obj is Jerk3 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.MetresPerCubicSecond}, {Y.MetresPerCubicSecond}, {Z.MetresPerCubicSecond}) [m/(s^3)]";

        public static bool operator ==(Jerk3? a, Jerk3? b)
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

        public static bool operator !=(Jerk3? a, Jerk3? b) => !(a == b);

        public static Jerk3 operator +(Jerk3 a) => a;
        public static Jerk3 operator -(Jerk3 a) => new(-a.X, -a.Y, -a.Z);
        public static Jerk3 operator +(Jerk3 a, Jerk3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Jerk3 operator -(Jerk3 a, Jerk3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Jerk3 operator %(Jerk3 a, Jerk3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(Jerk3 a, Jerk3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Jerk3 a, Jerk3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Jerk3 operator *(Jerk3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Jerk3 operator *(Scalar3 a, Jerk3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Jerk3 operator /(Jerk3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Jerk3 a, IQuantity3 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude, (a.Z * b.Z).Magnitude);
        public static UnhandledQuantity3 operator /(Jerk3 a, IQuantity3 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude, (a.Z / b.Z).Magnitude);

        public static Acceleration3 operator *(Jerk3 a, Time b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Acceleration3 operator *(Time a, Jerk3 b) => new(a * b.X, a * b.Y, a * b.Z);

        public static Scalar3 operator /(Jerk3 a, Jerk b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Jerk a, Jerk3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Jerk3 operator *(Jerk3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Jerk3 operator *(Scalar a, Jerk3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Jerk3 operator /(Jerk3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Jerk3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Jerk3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Jerk3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Jerk3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
