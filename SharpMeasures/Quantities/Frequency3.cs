using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Frequency3 : IEquatable<Frequency3>, IQuantity3<Frequency>
    {
        public Frequency X { get; }
        public Frequency Y { get; }
        public Frequency Z { get; }

        Scalar IQuantity3.XMagnitude => X.Magnitude;
        Scalar IQuantity3.YMagnitude => Y.Magnitude;
        Scalar IQuantity3.ZMagnitude => Z.Magnitude;

        public Frequency3(Scalar3 components)
        {
            X = new Frequency(components.X);
            Y = new Frequency(components.Y);
            Z = new Frequency(components.Z);
        }

        public Frequency3(Scalar3 components, UnitOfFrequency unit)
        {
            X = new Frequency(components.X, unit);
            Y = new Frequency(components.Y, unit);
            Z = new Frequency(components.Z, unit);
        }

        public Frequency3(Scalar3 components, UnitOfFrequency unit, MetricPrefix prefix)
        {
            X = new Frequency(components.X, unit, prefix);
            Y = new Frequency(components.Y, unit, prefix);
            Z = new Frequency(components.Z, unit, prefix);
        }

        public Frequency3(Frequency x, Frequency y, Frequency z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Frequency3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Frequency Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Frequency3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(Frequency3 other) => Cross(this, other);
        public Frequency3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(Frequency3 a, Frequency3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(Frequency3 a, Frequency3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Frequency3 Transform(Frequency3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Frequency(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Frequency(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Frequency(transform.M43)
            );
        }

        public bool Equals(Frequency3? other) => X.Equals(other?.X) && Y.Equals(other?.Y) && Z.Equals(other?.Z);
        public override bool Equals(object? obj) => Equals(obj as Frequency3);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.Hertz}, {Y.Hertz}, {Z.Hertz}) [Hz]";

        public static bool operator ==(Frequency3? a, Frequency3? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Frequency3? a, Frequency3? b) => !(a == b);

        public static Frequency3 operator +(Frequency3 a) => a;
        public static Frequency3 operator -(Frequency3 a) => new(-a.X, -a.Y, -a.Z);
        public static Frequency3 operator +(Frequency3 a, Frequency3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Frequency3 operator -(Frequency3 a, Frequency3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Frequency3 operator %(Frequency3 a, Frequency3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(Frequency3 a, Frequency3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Frequency3 a, Frequency3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Frequency3 operator *(Frequency3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Frequency3 operator *(Scalar3 a, Frequency3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Frequency3 operator /(Frequency3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Frequency3 a, IQuantity3 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude, a.Z * b.ZMagnitude);
        public static UnhandledQuantity3 operator /(Frequency3 a, IQuantity3 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude, a.Z / b.ZMagnitude);

        public static Velocity3 operator *(Frequency3 a, Length3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Acceleration3 operator *(Frequency3 a, Velocity3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Jerk3 operator *(Frequency3 a, Acceleration3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

        public static Scalar3 operator /(Frequency3 a, Frequency b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Frequency a, Frequency3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Frequency3 operator *(Frequency3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Frequency3 operator *(Scalar a, Frequency3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Frequency3 operator /(Frequency3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Frequency3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Frequency3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Frequency3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Frequency3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
