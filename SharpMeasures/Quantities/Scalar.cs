namespace ErikWe.SharpMeasures.Quantities;

using System;

public readonly record struct Scalar(double Magnitude) :
    IComparable<Scalar>,
    IScalarQuantity<Scalar>,
    IInvertibleScalarQuantity<Scalar>,
    ISquarableScalarQuantity<Scalar>,
    ICubableScalarQuantity<Scalar>,
    ISquareRootableScalarQuantity<Scalar>,
    ICubeRootableScalarQuantity<Scalar>
{
    public static Scalar Zero { get; } = new(0);
    public static Scalar One { get; } = new(1);

    public bool IsNaN => double.IsNaN(Magnitude);
    public bool IsZero => Magnitude == 0;
    public bool IsPositive => Magnitude > 0;
    public bool IsNegative => double.IsNegative(Magnitude);
    public bool IsFinite => double.IsFinite(Magnitude);
    public bool IsInfinite => double.IsInfinity(Magnitude);
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    public Scalar Absolute() => new(Math.Abs(Magnitude));
    public Scalar Floor() => new(Math.Floor(Magnitude));
    public Scalar Ceiling() => new(Math.Ceiling(Magnitude));
    public Scalar Round() => new(Math.Round(Magnitude));

    public Scalar Power(double exponent) => new(Math.Pow(Magnitude, exponent));
    public Scalar Invert() => new(1 / Magnitude);
    public Scalar Square() => new(Magnitude * Magnitude);
    public Scalar Cube() => new(Magnitude * Magnitude * Magnitude);
    public Scalar SquareRoot() => new(Math.Sqrt(Magnitude));
    public Scalar CubeRoot() => new(Math.Cbrt(Magnitude));

    public int CompareTo(Scalar other) => Magnitude.CompareTo(other.Magnitude);
    public override string ToString() => $"{Magnitude}";

    public Scalar Plus() => this;
    public Scalar Negate() => new(-Magnitude);
    public static Scalar operator +(Scalar x) => x;
    public static Scalar operator -(Scalar x) => new(-x.Magnitude);

    public Scalar Add(double term) => new(Magnitude + term);
    public Scalar Subtract(double term) => new(Magnitude - term);
    public static Scalar operator +(Scalar x, double y) => new(x.Magnitude + y);
    public static Scalar operator +(double x, Scalar y) => new(x + y.Magnitude);
    public static Scalar operator -(Scalar x, double y) => new(x.Magnitude - y);
    public static Scalar operator -(double x, Scalar y) => new(x - y.Magnitude);

    public Scalar Add(Scalar term) => new(Magnitude + term.Magnitude);
    public Scalar Subtract(Scalar term) => new(Magnitude - term.Magnitude);
    public static Scalar operator +(Scalar x, Scalar y) => new(x.Magnitude + y.Magnitude);
    public static Scalar operator -(Scalar x, Scalar y) => new(x.Magnitude - y.Magnitude);

    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    public static Unhandled operator *(Scalar x, Unhandled y) => new(x.Magnitude * y.Magnitude);
    public static Unhandled operator /(Scalar x, Unhandled y) => new(x.Magnitude / y.Magnitude);

    public Scalar Remainder(double divisor) => new(Magnitude % divisor);
    public Scalar Multiply(double factor) => new(Magnitude * factor);
    public Scalar Divide(double divisor) => new(Magnitude / divisor);
    public static Scalar operator %(Scalar x, double y) => new(x.Magnitude % y);
    public static Scalar operator *(Scalar x, double y) => new(x.Magnitude * y);
    public static Scalar operator *(double x, Scalar y) => new(x * y.Magnitude);
    public static Scalar operator /(Scalar x, double y) => new(x.Magnitude / y);

    public Scalar Remainder(Scalar divisor) => new(Magnitude % divisor.Magnitude);
    public Scalar Multiply(Scalar factor) => new(Magnitude * factor.Magnitude);
    public Scalar Divide(Scalar divisor) => new(Magnitude / divisor.Magnitude);
    public static Scalar operator %(Scalar x, Scalar y) => new(x.Magnitude % y.Magnitude);
    public static Scalar operator *(Scalar x, Scalar y) => new(x.Magnitude * y.Magnitude);
    public static Scalar operator /(Scalar x, Scalar y) => new(x.Magnitude / y.Magnitude);

    public Unhandled Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity => new(Magnitude * factor.Magnitude);
    public Unhandled Divide<TScalar>(TScalar divisor) where TScalar : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    public static Unhandled operator *(Scalar x, IScalarQuantity y) => new(x.Magnitude * (y?.Magnitude ?? double.NaN));
    public static Unhandled operator /(Scalar x, IScalarQuantity y) => new(x.Magnitude / (y?.Magnitude ?? double.NaN));

    public static bool operator <(Scalar x, Scalar y) => x.Magnitude < y.Magnitude;
    public static bool operator >(Scalar x, Scalar y) => x.Magnitude > y.Magnitude;
    public static bool operator <=(Scalar x, Scalar y) => x.Magnitude <= y.Magnitude;
    public static bool operator >=(Scalar x, Scalar y) => x.Magnitude >= y.Magnitude;

    public double ToDouble() => Magnitude;
    public static implicit operator double(Scalar x) => x.Magnitude;

    public static Scalar FromDouble(double x) => new(x);
    public static explicit operator Scalar(double x) => new(x);
}