using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Volume : IEquatable<Volume>, IComparable<Volume>, IQuantity
    {
        public static readonly Volume Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, UnitOfVolume unit, MetricPrefix prefix) => prefix * magnitude * unit.Scale;

        private static Scalar ComputeMagnitude(Scalar magnitude, UnitOfLength unit, MetricPrefix prefix) => Math.Pow(prefix, 3) * magnitude * Math.Pow(unit.Scale, 3);

        public Scalar Magnitude { get; }

        public Volume(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Volume(Scalar magnitude, UnitOfVolume unit)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, MetricPrefix.Identity);
        }

        public Volume(Scalar magnitude, UnitOfVolume unit, MetricPrefix prefix)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, prefix);
        }

        public Volume(Scalar magnitude, UnitOfLength unit)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, MetricPrefix.Identity);
        }

        public Volume(Scalar magnitude, UnitOfLength unit, MetricPrefix prefix)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, prefix);
        }

        public Scalar InUnit(UnitOfVolume unit) => InUnit(unit, MetricPrefix.Identity);
        public Scalar InUnit(UnitOfVolume unit, MetricPrefix prefix) => Magnitude / (prefix * unit.Scale);
        public Scalar InUnit(UnitOfLength unit) => InUnit(unit, MetricPrefix.Identity);
        public Scalar InUnit(UnitOfLength unit, MetricPrefix prefix) => Magnitude / (Math.Pow(prefix, 3) * Math.Pow(unit.Scale, 3));

        public Scalar Millilitres => InUnit(UnitOfVolume.Litre, MetricPrefix.Milli);
        public Scalar Centilitres => InUnit(UnitOfVolume.Litre, MetricPrefix.Centi);
        public Scalar Decilitres => InUnit(UnitOfVolume.Litre, MetricPrefix.Deci);
        public Scalar Litres => InUnit(UnitOfVolume.Litre);

        public Scalar CubicMillimetres => InUnit(UnitOfLength.Metre, MetricPrefix.Milli);
        public Scalar CubicCentimetres => InUnit(UnitOfLength.Metre, MetricPrefix.Centi);
        public Scalar CubicDecimetres => InUnit(UnitOfLength.Metre, MetricPrefix.Deci);
        public Scalar CubicMetres => InUnit(UnitOfLength.Metre);
        public Scalar CubicKilometres => InUnit(UnitOfLength.Metre, MetricPrefix.Kilo);

        public Scalar CubicInches => InUnit(UnitOfLength.Inch);
        public Scalar CubicFeet => InUnit(UnitOfLength.Foot);
        public Scalar CubicYards => InUnit(UnitOfLength.Yard);
        public Scalar CubicMiles => InUnit(UnitOfLength.Mile);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Volume Abs() => new(Magnitude.Abs());
        public Volume Floor() => new(Magnitude.Floor());
        public Volume Ceiling() => new(Magnitude.Ceiling());
        public Volume Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Volume other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is Volume other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{CubicMetres} [m^3]";

        public int CompareTo(Volume other)
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

        public static bool operator ==(Volume? x, Volume? y)
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

        public static bool operator !=(Volume? x, Volume? y) => !(x == y);

        public static Volume operator +(Volume x) => x;
        public static Volume operator -(Volume x) => new(-x.Magnitude);
        public static Volume operator +(Volume x, Volume y) => new(x.Magnitude + y.Magnitude);
        public static Volume operator -(Volume x, Volume y) => new(x.Magnitude - y.Magnitude);
        public static Volume operator %(Volume x, Volume y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Volume x, Volume y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Volume x, Volume y) => new(x.Magnitude / y.Magnitude);

        public static Area operator /(Volume x, Length y) => new(x.Magnitude * y.Magnitude);
        public static Length operator /(Volume x, Area y) => new(x.Magnitude / y.Magnitude);

        public static Volume operator *(Volume x, Scalar y) => new(x.Magnitude * y);
        public static Volume operator *(Scalar x, Volume y) => new(x * y.Magnitude);
        public static Volume operator /(Volume x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(Volume x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Volume x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Volume x, Volume y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Volume x, Volume y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Volume x, Volume y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Volume x, Volume y) => x.Magnitude >= y.Magnitude;
    }
}
