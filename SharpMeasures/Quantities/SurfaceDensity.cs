using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct SurfaceDensity : IEquatable<SurfaceDensity>, IComparable<SurfaceDensity>, IQuantity
    {
        public static readonly SurfaceDensity Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, (UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfLength unit, MetricPrefix prefix) length)
            => mass.prefix / Math.Pow(length.prefix, 2) * magnitude * mass.unit.Scale / Math.Pow(length.unit.Scale, 2);

        public Scalar Magnitude { get; }

        public SurfaceDensity(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public SurfaceDensity(Scalar magnitude, UnitOfMass massUnit, UnitOfLength lengthUnit)
        {
            Magnitude = ComputeMagnitude(magnitude, (massUnit, MetricPrefix.Identity), (lengthUnit, MetricPrefix.Identity));
        }

        public SurfaceDensity(Scalar magnitude, (UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfLength unit, MetricPrefix prefix) length)
        {
            Magnitude = ComputeMagnitude(magnitude, mass, length);
        }

        public Scalar InUnit(UnitOfMass massUnit, UnitOfLength lengthUnit) => InUnit((massUnit, MetricPrefix.Identity), (lengthUnit, MetricPrefix.Identity));
        public Scalar InUnit((UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfLength unit, MetricPrefix prefix) length)
            => Magnitude / (mass.prefix / Math.Pow(length.prefix, 2) * mass.unit.Scale / Math.Pow(length.unit.Scale, 2));

        public Scalar GramsPerSquareMetre => InUnit(UnitOfMass.Gram, UnitOfLength.Metre);
        public Scalar KilogramsPerSquareMetre => InUnit((UnitOfMass.Gram, MetricPrefix.Kilo), (UnitOfLength.Metre, MetricPrefix.Identity));
        public Scalar OuncesPerSquareFoot => InUnit(UnitOfMass.Ounce, UnitOfLength.Foot);
        public Scalar PoundsPerSquareFoot => InUnit(UnitOfMass.Pound, UnitOfLength.Foot);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public SurfaceDensity Abs() => new(Magnitude.Abs());
        public SurfaceDensity Floor() => new(Magnitude.Floor());
        public SurfaceDensity Ceiling() => new(Magnitude.Ceiling());
        public SurfaceDensity Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(SurfaceDensity other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is SurfaceDensity other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{KilogramsPerSquareMetre} [kg/(m^2)]";

        public int CompareTo(SurfaceDensity other)
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

        public static bool operator ==(SurfaceDensity? x, SurfaceDensity? y)
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

        public static bool operator !=(SurfaceDensity? x, SurfaceDensity? y) => !(x == y);

        public static SurfaceDensity operator +(SurfaceDensity x) => x;
        public static SurfaceDensity operator -(SurfaceDensity x) => new(-x.Magnitude);
        public static SurfaceDensity operator +(SurfaceDensity x, SurfaceDensity y) => new(x.Magnitude + y.Magnitude);
        public static SurfaceDensity operator -(SurfaceDensity x, SurfaceDensity y) => new(x.Magnitude - y.Magnitude);
        public static SurfaceDensity operator %(SurfaceDensity x, SurfaceDensity y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(SurfaceDensity x, SurfaceDensity y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(SurfaceDensity x, SurfaceDensity y) => new(x.Magnitude / y.Magnitude);

        public static Mass operator *(SurfaceDensity x, Area y) => new(x.Magnitude * y.Magnitude);
        public static Density operator /(SurfaceDensity x, Length y) => new(x.Magnitude / y.Magnitude);

        public static SurfaceDensity operator *(SurfaceDensity x, Scalar y) => new(x.Magnitude * y);
        public static SurfaceDensity operator *(Scalar x, SurfaceDensity y) => new(x * y.Magnitude);
        public static SurfaceDensity operator /(SurfaceDensity x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(SurfaceDensity x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(SurfaceDensity x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(SurfaceDensity x, SurfaceDensity y) => x.Magnitude < y.Magnitude;
        public static bool operator >(SurfaceDensity x, SurfaceDensity y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(SurfaceDensity x, SurfaceDensity y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(SurfaceDensity x, SurfaceDensity y) => x.Magnitude >= y.Magnitude;
    }
}
