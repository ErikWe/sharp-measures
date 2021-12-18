using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Acceleration3 : IEquatable<Acceleration3>, IQuantity3
    {
        public Acceleration X { get; }
        public Acceleration Y { get; }
        public Acceleration Z { get; }

        Scalar IQuantity3.X => X.Magnitude;
        Scalar IQuantity3.Y => Y.Magnitude;
        Scalar IQuantity3.Z => Z.Magnitude;

        public Acceleration3(Scalar3 components)
        {
            X = new Acceleration(components.X);
            Y = new Acceleration(components.Y);
            Z = new Acceleration(components.Z);
        }

        public Acceleration3(Scalar3 components, UnitOfLength lengthUnit, UnitOfTime timeUnit)
        {
            X = new Acceleration(components.X, lengthUnit, timeUnit);
            Y = new Acceleration(components.Y, lengthUnit, timeUnit);
            Z = new Acceleration(components.Z, lengthUnit, timeUnit);
        }

        public Acceleration3(Scalar3 components, (UnitOfLength unit, MetricPrefix prefix) length, (UnitOfTime unit, MetricPrefix prefix) time)
        {
            X = new Acceleration(components.X, length, time);
            Y = new Acceleration(components.Y, length, time);
            Z = new Acceleration(components.Z, length, time);
        }

        public Acceleration3(Acceleration x, Acceleration y, Acceleration z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Acceleration3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Acceleration Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Acceleration3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(Acceleration3 other) => Cross(this, other);
        public Acceleration3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(Acceleration3 a, Acceleration3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(Acceleration3 a, Acceleration3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Acceleration3 Transform(Acceleration3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Acceleration(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Acceleration(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Acceleration(transform.M43)
            );
        }

        public bool Equals(Acceleration3 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

        public override bool Equals(object? obj)
        {
            if (obj is Acceleration3 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.MetresPerSecondSquared}, {Y.MetresPerSecondSquared}, {Z.MetresPerSecondSquared}) [m/(s^2)]";

        public static bool operator ==(Acceleration3? a, Acceleration3? b)
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

        public static bool operator !=(Acceleration3? a, Acceleration3? b) => !(a == b);

        public static Acceleration3 operator +(Acceleration3 a) => a;
        public static Acceleration3 operator -(Acceleration3 a) => new(-a.X, -a.Y, -a.Z);
        public static Acceleration3 operator +(Acceleration3 a, Acceleration3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Acceleration3 operator -(Acceleration3 a, Acceleration3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Acceleration3 operator %(Acceleration3 a, Acceleration3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(Acceleration3 a, Acceleration3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Acceleration3 a, Acceleration3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Acceleration3 operator *(Acceleration3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Acceleration3 operator *(Scalar3 a, Acceleration3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Acceleration3 operator /(Acceleration3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Acceleration3 a, IQuantity3 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude, (a.Z * b.Z).Magnitude);
        public static UnhandledQuantity3 operator /(Acceleration3 a, IQuantity3 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude, (a.Z / b.Z).Magnitude);

        public static Velocity3 operator *(Acceleration3 a, Time b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Velocity3 operator *(Time a, Acceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Jerk3 operator *(Acceleration3 a, Frequency b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Jerk3 operator *(Frequency a, Acceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Velocity3 operator /(Acceleration3 a, Frequency b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Jerk3 operator /(Acceleration3 a, Time b) => new(a.X / b, a.Y / b, a.Z / b);

        public static Scalar3 operator /(Acceleration3 a, Acceleration b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Acceleration a, Acceleration3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Acceleration3 operator *(Acceleration3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Acceleration3 operator *(Scalar a, Acceleration3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Acceleration3 operator /(Acceleration3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Acceleration3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Acceleration3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Acceleration3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Acceleration3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
