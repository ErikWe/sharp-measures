namespace ErikWe.SharpMeasures.Quantities;

using System;

public readonly partial record struct Angle :
    IAddableScalarQuantity<Angle, Angle>,
    ISubtractableScalarQuantity<Angle, Angle>
{
    public double Sin() => Math.Sin(Magnitude);
    public double Cos() => Math.Cos(Magnitude);
    public double Tan() => Math.Tan(Magnitude);

    public double Sinh() => Math.Sinh(Magnitude);
    public double Cosh() => Math.Cosh(Magnitude);
    public double Tanh() => Math.Tanh(Magnitude);

    public static Angle Asin(double sine) => new(Math.Asin(sine));
    public static Angle Acos(double cosine) => new(Math.Acos(cosine));
    public static Angle Atan(double tangent) => new(Math.Atan(tangent));

    public static Angle Asinh(double hyperbolicSine) => new(Math.Asinh(hyperbolicSine));
    public static Angle Acosh(double hyperbolicCosine) => new(Math.Acosh(hyperbolicCosine));
    public static Angle Atanh(double hyperbolicTangent) => new(Math.Atanh(hyperbolicTangent));

    public static Angle Atan2(double y, double x) => new(Math.Atan2(y, x));
}
