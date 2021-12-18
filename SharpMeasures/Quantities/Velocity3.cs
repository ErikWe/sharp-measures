using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Velocity3 : IEquatable<Velocity3>, IQuantity3
    {
        public Velocity X { get; }
        public Velocity Y { get; }
        public Velocity Z { get; }

        Scalar IQuantity3.X => X.Magnitude;
        Scalar IQuantity3.Y => Y.Magnitude;
        Scalar IQuantity3.Z => Z.Magnitude;

        public Velocity3(Scalar3 components)
        {
            X = new Velocity(components.X);
            Y = new Velocity(components.Y);
            Z = new Velocity(components.Z);
        }

        public Velocity3(Scalar3 components, UnitOfLength lengthUnit, UnitOfTime timeUnit)
        {
            X = new Velocity(components.X, lengthUnit, timeUnit);
            Y = new Velocity(components.Y, lengthUnit, timeUnit);
            Z = new Velocity(components.Z, lengthUnit, timeUnit);
        }

        public Velocity3(Scalar3 components, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            X = new Velocity(components.X, length, time);
            Y = new Velocity(components.Y, length, time);
            Z = new Velocity(components.Z, length, time);
        }

        public Velocity3(Velocity x, Velocity y, Velocity z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Velocity3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Velocity Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Velocity3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(Velocity3 other) => Cross(this, other);
        public Velocity3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(Velocity3 a, Velocity3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(Velocity3 a, Velocity3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Velocity3 Transform(Velocity3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Velocity(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Velocity(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Velocity(transform.M43)
            );
        }

        public bool Equals(Velocity3 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

        public override bool Equals(object? obj)
        {
            if (obj is Velocity3 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.MetresPerSecond}, {Y.MetresPerSecond}, {Z.MetresPerSecond}) [m/s]";

        public static bool operator ==(Velocity3? a, Velocity3? b)
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

        public static bool operator !=(Velocity3? a, Velocity3? b) => !(a == b);

        public static Velocity3 operator +(Velocity3 a) => a;
        public static Velocity3 operator -(Velocity3 a) => new(-a.X, -a.Y, -a.Z);
        public static Velocity3 operator +(Velocity3 a, Velocity3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Velocity3 operator -(Velocity3 a, Velocity3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Velocity3 operator %(Velocity3 a, Velocity3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(Velocity3 a, Velocity3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Velocity3 a, Velocity3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Velocity3 operator *(Velocity3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Velocity3 operator *(Scalar3 a, Velocity3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Velocity3 operator /(Velocity3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Velocity3 a, IQuantity3 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude, (a.Z * b.Z).Magnitude);
        public static UnhandledQuantity3 operator /(Velocity3 a, IQuantity3 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude, (a.Z / b.Z).Magnitude);

        public static Length3 operator *(Velocity3 a, Time b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Length3 operator *(Time a, Velocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Acceleration3 operator /(Velocity3 a, Time b) => new(a.X / b, a.Y / b, a.Z / b);

        public static Scalar3 operator /(Velocity3 a, Velocity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Velocity a, Velocity3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Velocity3 operator *(Velocity3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Velocity3 operator *(Scalar a, Velocity3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Velocity3 operator /(Velocity3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Velocity3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Velocity3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Velocity3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Velocity3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
