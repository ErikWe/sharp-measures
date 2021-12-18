using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Density : IEquatable<Density>, IComparable<Density>, IQuantity
    {
        public static readonly Density Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, (UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfLength unit, MetricPrefix prefix) length)
            => mass.prefix / Math.Pow(length.prefix, 3) * magnitude * mass.unit.Scale / Math.Pow(length.unit.Scale, 3);

        public Scalar Magnitude { get; }

        public Density(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Density(Scalar magnitude, UnitOfMass massUnit, UnitOfLength lengthUnit)
        {
            Magnitude = ComputeMagnitude(magnitude, (massUnit, MetricPrefix.Identity), (lengthUnit, MetricPrefix.Identity));
        }

        public Density(Scalar magnitude, (UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfLength unit, MetricPrefix prefix) length)
        {
            Magnitude = ComputeMagnitude(magnitude, mass, length);
        }

        public Scalar InUnit(UnitOfMass massUnit, UnitOfLength lengthUnit) => InUnit((massUnit, MetricPrefix.Identity), (lengthUnit, MetricPrefix.Identity));
        public Scalar InUnit((UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfLength unit, MetricPrefix prefix) length)
            => Magnitude / (mass.prefix / Math.Pow(length.prefix, 3) * mass.unit.Scale / Math.Pow(length.unit.Scale, 3));
        public Scalar InUnit(UnitOfMass massUnit, UnitOfVolume volumeUnit) => InUnit((massUnit, MetricPrefix.Identity), (volumeUnit, MetricPrefix.Identity));
        public Scalar InUnit((UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfVolume unit, MetricPrefix prefix) volume)
            => Magnitude / (mass.prefix / volume.prefix * mass.unit.Scale / volume.unit.Scale);

        public Scalar GramsPerCubicCentimetre => InUnit((UnitOfMass.Gram, MetricPrefix.Identity), (UnitOfLength.Metre, MetricPrefix.Centi));
        public Scalar GramsPerCubicMetre => InUnit(UnitOfMass.Gram, UnitOfLength.Metre);
        public Scalar GramsPerMillilitre => InUnit((UnitOfMass.Gram, MetricPrefix.Identity), (UnitOfVolume.Litre, MetricPrefix.Milli));
        public Scalar GramsPerLitre => InUnit(UnitOfMass.Gram, UnitOfVolume.Litre);
        public Scalar KilogramsPerLitre => InUnit((UnitOfMass.Gram, MetricPrefix.Kilo), (UnitOfVolume.Litre, MetricPrefix.Identity));
        public Scalar KilogramsPerCubicMetre => InUnit((UnitOfMass.Gram, MetricPrefix.Kilo), (UnitOfLength.Metre, MetricPrefix.Identity));
        public Scalar TonnesPerCubicMetres => InUnit(UnitOfMass.Tonne, UnitOfLength.Metre);

        public Scalar OuncesPerCubicInch => InUnit(UnitOfMass.Ounce, UnitOfLength.Inch);
        public Scalar PoundsPerCubicInch => InUnit(UnitOfMass.Pound, UnitOfLength.Inch);
        public Scalar PoundsPerCubicFoot => InUnit(UnitOfMass.Pound, UnitOfLength.Foot);
        public Scalar PoundsPerCubicYard => InUnit(UnitOfMass.Pound, UnitOfLength.Yard);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Density Abs() => new(Magnitude.Abs());
        public Density Floor() => new(Magnitude.Floor());
        public Density Ceiling() => new(Magnitude.Ceiling());
        public Density Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Density other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is Density other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{KilogramsPerCubicMetre} [kg/(m^3)]";

        public int CompareTo(Density other)
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

        public static bool operator ==(Density? x, Density? y)
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

        public static bool operator !=(Density? x, Density? y) => !(x == y);

        public static Density operator +(Density x) => x;
        public static Density operator -(Density x) => new(-x.Magnitude);
        public static Density operator +(Density x, Density y) => new(x.Magnitude + y.Magnitude);
        public static Density operator -(Density x, Density y) => new(x.Magnitude - y.Magnitude);
        public static Density operator %(Density x, Density y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Density x, Density y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Density x, Density y) => new(x.Magnitude / y.Magnitude);

        public static SurfaceDensity operator *(Density x, Length y) => new(x.Magnitude * y.Magnitude);
        public static Mass operator *(Density x, Volume y) => new(x.Magnitude * y.Magnitude);

        public static Density operator *(Density x, Scalar y) => new(x.Magnitude * y);
        public static Density operator *(Scalar x, Density y) => new(x * y.Magnitude);
        public static Density operator /(Density x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(Density x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Density x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Density x, Density y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Density x, Density y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Density x, Density y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Density x, Density y) => x.Magnitude >= y.Magnitude;
    }
}