using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Angle : IEquatable<Angle>, IComparable<Angle>, IQuantity
    {
        public static readonly Angle Zero = new(0, UnitOfAngle.Turn);
        public static readonly Angle Full = new(1, UnitOfAngle.Turn);
        public static readonly Angle Half = Full / 2;
        public static readonly Angle Quarter = Full / 4;

        private static Scalar ComputeMagnitude(Scalar magnitude, UnitOfAngle unit, MetricPrefix prefix) => prefix * magnitude * unit.Scale;

        public Scalar Magnitude { get; }

        public Angle(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Angle(Scalar magnitude, UnitOfAngle unit)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, MetricPrefix.Identity);
        }

        public Angle(Scalar magnitude, UnitOfAngle unit, MetricPrefix prefix)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, prefix);
        }

        public Scalar InUnit(UnitOfAngle unit) => InUnit(unit, MetricPrefix.Identity);
        public Scalar InUnit(UnitOfAngle unit, MetricPrefix prefix) => Magnitude / (prefix * unit.Scale);

        public Scalar Radians => InUnit(UnitOfAngle.Radian);
        public Scalar Degrees => InUnit(UnitOfAngle.Degree);
        public Scalar ArcMinutes => InUnit(UnitOfAngle.ArcMinute);
        public Scalar ArcSeconds => InUnit(UnitOfAngle.ArcSecond);
        public Scalar Turns => InUnit(UnitOfAngle.Turn);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Angle Abs() => new(Magnitude.Abs());
        public Angle Floor() => new(Magnitude.Floor());
        public Angle Ceiling() => new(Magnitude.Ceiling());
        public Angle Round() => new(Magnitude.Round());

        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public Scalar Sin() => Math.Sin(Radians);
        public Scalar Cos() => Math.Cos(Radians);
        public Scalar Tan() => Math.Tan(Radians);
        public Scalar Sinh() => Math.Sinh(Radians);
        public Scalar Cosh() => Math.Cosh(Radians);
        public Scalar Tanh() => Math.Tanh(Radians);

        public static Angle Asin(IQuantity quantity) => new(Math.Asin(quantity.Magnitude), UnitOfAngle.Radian);
        public static Angle Acos(IQuantity quantity) => new(Math.Acos(quantity.Magnitude), UnitOfAngle.Radian);
        public static Angle Atan(IQuantity quantity) => new(Math.Atan(quantity.Magnitude), UnitOfAngle.Radian);
        public static Angle Asinh(IQuantity quantity) => new(Math.Asinh(quantity.Magnitude), UnitOfAngle.Radian);
        public static Angle Acosh(IQuantity quantity) => new(Math.Acosh(quantity.Magnitude), UnitOfAngle.Radian);
        public static Angle Atanh(IQuantity quantity) => new(Math.Atanh(quantity.Magnitude), UnitOfAngle.Radian);

        public Angle Normalize()
        {
            Angle value = this % Full;
            if (value < Zero)
            {
                value += Full;
            }

            return value;
        }

        public Angle ShiftedNormalize()
        {
            Angle value = Normalize();
            if (value >= Half)
            {
                value -= Full;
            }

            return value;
        }

        public Angle DeltaAngle(Angle other) => (other - this).ShiftedNormalize();

        public Angle ClockwiseDeltaAngle(Angle other)
        {
            Angle value = DeltaAngle(other);
            if (value > Zero)
            {
                value = Full - value;
            }

            return value.Abs();
        }

        public Angle CounterClockwiseDeltaAngle(Angle other)
        {
            Angle value = Full - ClockwiseDeltaAngle(other);
            if (value == Full)
            {
                return Zero;
            }

            return value;
        }

        public bool Equals(Angle other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is Angle other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{Radians} [rad]";

        public int CompareTo(Angle other)
        {
            if (this > other)
            {
                return 1;
            }
            else if (this < other)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public static bool operator ==(Angle? x, Angle? y)
        {
            if (x is null)
            {
                return y is null;
            }
            else
            {
                return x.Equals(y);
            }
        }

        public static bool operator !=(Angle? x, Angle? y) => !(x == y);

        public static Angle operator +(Angle x) => x;
        public static Angle operator -(Angle x) => new(-x.Magnitude);
        public static Angle operator +(Angle x, Angle y) => new(x.Magnitude + y.Magnitude);
        public static Angle operator -(Angle x, Angle y) => new(x.Magnitude - y.Magnitude);
        public static Angle operator %(Angle x, Angle y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Angle x, Angle y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Angle x, Angle y) => new(x.Magnitude / y.Magnitude);

        public static AngularVelocity operator *(Angle x, Frequency y) => new(x.Magnitude * y.Magnitude);
        public static AngularVelocity operator /(Angle x, Time y) => new(x.Magnitude / y.Magnitude);

        public static Angle operator *(Angle x, Scalar y) => new(x.Magnitude * y);
        public static Angle operator *(Scalar x, Angle y) => new(x * y.Magnitude);
        public static Angle operator /(Angle x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(Angle x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Angle x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Angle x, Angle y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Angle x, Angle y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Angle x, Angle y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Angle x, Angle y) => x.Magnitude >= y.Magnitude;
    }
}
