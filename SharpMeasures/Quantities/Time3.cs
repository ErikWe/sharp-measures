using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;
using System.Numerics;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Time3 : IEquatable<Time3>, IQuantity3<Time>
    {
        public Time X { get; }
        public Time Y { get; }
        public Time Z { get; }

        Scalar IQuantity3.XMagnitude => X.Magnitude;
        Scalar IQuantity3.YMagnitude => Y.Magnitude;
        Scalar IQuantity3.ZMagnitude => Z.Magnitude;

        public Time3(Scalar3 components)
        {
            X = new Time(components.X);
            Y = new Time(components.Y);
            Z = new Time(components.Z);
        }

        public Time3(Scalar3 components, UnitOfTime unit)
        {
            X = new Time(components.X, unit);
            Y = new Time(components.Y, unit);
            Z = new Time(components.Z, unit);
        }

        public Time3(Scalar3 components, UnitOfTime unit, MetricPrefix prefix)
        {
            X = new Time(components.X, unit, prefix);
            Y = new Time(components.Y, unit, prefix);
            Z = new Time(components.Z, unit, prefix);
        }

        public Time3(Time x, Time y, Time z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Time3 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Time Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Time3 other) => Dot(this, other);
        public UnhandledQuantity3 Cross(Time3 other) => Cross(this, other);
        public Time3 Transform(Matrix4x4 transform) => Transform(this, transform);

        public static UnhandledQuantity Dot(Time3 a, Time3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static UnhandledQuantity3 Cross(Time3 a, Time3 b)
        {
            return new(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static Time3 Transform(Time3 vector, Matrix4x4 transform)
        {
            return new(
                vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + new Time(transform.M41),
                vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + new Time(transform.M42),
                vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + new Time(transform.M43)
            );
        }

        public bool Equals(Time3? other) => X.Equals(other?.X) && Y.Equals(other?.Y) && Z.Equals(other?.Z);
        public override bool Equals(object? obj) => Equals(obj as Time3);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
        public override string ToString() => $"({X.Seconds}, {Y.Seconds}, {Z.Seconds}) [s]";

        public static bool operator ==(Time3? a, Time3? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Time3? a, Time3? b) => !(a == b);

        public static Time3 operator +(Time3 a) => a;
        public static Time3 operator -(Time3 a) => new(-a.X, -a.Y, -a.Z);
        public static Time3 operator +(Time3 a, Time3 b) => new(a.X + b.X, a.Y + b.Y, b.Z + b.Z);
        public static Time3 operator -(Time3 a, Time3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Time3 operator %(Time3 a, Time3 b) => new(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        public static UnhandledQuantity3 operator *(Time3 a, Time3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Scalar3 operator /(Time3 a, Time3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static Time3 operator *(Time3 a, Scalar3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Time3 operator *(Scalar3 a, Time3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Time3 operator /(Time3 a, Scalar3 b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static UnhandledQuantity3 operator *(Time3 a, IQuantity3 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude, a.Z * b.ZMagnitude);
        public static UnhandledQuantity3 operator /(Time3 a, IQuantity3 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude, a.Z / b.ZMagnitude);

        public static Length3 operator *(Time3 a, Velocity3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Velocity3 operator *(Time3 a, Acceleration3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Acceleration3 operator *(Time3 a, Jerk3 b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

        public static Scalar3 operator /(Time3 a, Time b) => new(a.X / b, a.Y / b, a.Z / b);
        public static Scalar3 operator /(Time a, Time3 b) => new(a / b.X, a / b.Y, a / b.Z);

        public static Time3 operator *(Time3 a, Scalar b) => new(a.X * b, a.Y * b, a.Z * b);
        public static Time3 operator *(Scalar a, Time3 b) => new(a * b.X, a * b.Y, a * b.Z);
        public static Time3 operator /(Time3 a, Scalar b) => new(a.X / b, a.Y / b, a.Z / b);

        public static UnhandledQuantity3 operator *(Time3 a, IQuantity b) => new(a.X * b, a.Y * b, a.Z * b);
        public static UnhandledQuantity3 operator *(IQuantity a, Time3 b) => new(b.X * a, b.Y * a, b.Z * a);
        public static UnhandledQuantity3 operator /(Time3 a, IQuantity b) => new(a.X / b, a.Y / b, a.Z / b);
        public static UnhandledQuantity3 operator /(IQuantity a, Time3 b) => new(b.X / a, b.Y / a, b.Z / a);
    }
}
