namespace ErikWe.SharpMeasures.Quantities;

using ErikWe.SharpMeasures.Units;

using System;

public readonly partial record struct Angle
{
    /// <summary>Computes <see cref="Angle"/> according to { <paramref name="angularSpeed"/> ∙ <paramref name="time"/> },
    /// where <paramref name="angularSpeed"/> is the average <see cref="AngularSpeed"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Angle From(AngularSpeed angularSpeed, Time time) => new(angularSpeed.Magnitude * time.Magnitude);

    /// <summary>Computes average <see cref="AngularSpeed"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public AngularSpeed Divide(Time time) => AngularSpeed.From(this, time);
    /// <summary>Computes average <see cref="AngularSpeed"/> according to { <paramref name="angle"/> / <paramref name="time"/> }.</summary>
    public static AngularSpeed operator /(Angle angle, Time time) => angle.Divide(time);

    /// <summary>Computes the sine of the <see cref="Angle"/>.</summary>
    public Scalar Sin() => new(Math.Sin(Magnitude));
    /// <summary>Computes the cosine of the <see cref="Angle"/>.</summary>
    public Scalar Cos() => new(Math.Cos(Magnitude));
    /// <summary>Computes the tangent of the <see cref="Angle"/>.</summary>
    public Scalar Tan() => new(Math.Tan(Magnitude));

    /// <summary>Computes the hyperbolic sine of the <see cref="Angle"/>.</summary>
    public Scalar Sinh() => new(Math.Sinh(Magnitude));
    /// <summary>Computes the hyperbolic cosine of the <see cref="Angle"/>.</summary>
    public Scalar Cosh() => new(Math.Cosh(Magnitude));
    /// <summary>Computes the hyperbolic tangent of the <see cref="Angle"/>.</summary>
    public Scalar Tanh() => new(Math.Tanh(Magnitude));

    /// <summary>Computes the <see cref="Angle"/> for which the sine is <paramref name="sine"/>, in the range [-π/2, π/2] <see cref="UnitOfAngle.Radian"/>.</summary>
    public static Angle Asin(Scalar sine) => Asin(sine.Magnitude);
    /// <summary>Computes the <see cref="Angle"/> for which the sine is <paramref name="sine"/>, in the range [-π/2, π/2] <see cref="UnitOfAngle.Radian"/>.</summary>
    public static Angle Asin(double sine) => new(Math.Asin(sine));
    /// <summary>Computes the <see cref="Angle"/> for which the cosine is <paramref name="cosine"/>, in the range [0, π] <see cref="UnitOfAngle.Radian"/>.</summary>
    public static Angle Acos(Scalar cosine) => Acos(cosine.Magnitude);
    /// <summary>Computes the <see cref="Angle"/> for which the cosine is <paramref name="cosine"/>, in the range [0, π] <see cref="UnitOfAngle.Radian"/>.</summary>
    public static Angle Acos(double cosine) => new(Math.Acos(cosine));
    /// <summary>Computes the <see cref="Angle"/> for which the tangent is <paramref name="tangent"/>, in the range [-π/2, π/2] <see cref="UnitOfAngle.Radian"/>.</summary>
    /// <remarks><see cref="Atan2(Scalar, Scalar)"/> is similar, but computes an <see cref="Angle"/> in the range [-π, π] <see cref="UnitOfAngle.Radian"/>.</remarks>
    public static Angle Atan(Scalar tangent) => Atan(tangent.Magnitude);
    /// <summary>Computes the <see cref="Angle"/> for which the tangent is <paramref name="tangent"/>, in the range [-π/2, π/2] <see cref="UnitOfAngle.Radian"/>.</summary>
    /// <remarks><see cref="Atan2(Scalar, Scalar)"/> is similar, but computes an <see cref="Angle"/> in the range [-π, π] <see cref="UnitOfAngle.Radian"/>.</remarks>
    public static Angle Atan(double tangent) => new(Math.Atan(tangent));
    /// <summary>Computes the <see cref="Angle"/> of the vector (x, y) to the positive x-axis, in the range [-π, π] <see cref="UnitOfAngle.Radian"/>.</summary>
    /// <remarks>Note the 'reversed' order of <paramref name="y"/> and <paramref name="x"/>.</remarks>
    public static Angle Atan2(Scalar y, Scalar x) => Atan2(y.Magnitude, x.Magnitude);
    /// <summary>Computes the <see cref="Angle"/> of the vector (x, y) to the positive x-axis, in the range [-π, π] <see cref="UnitOfAngle.Radian"/>.</summary>
    /// <remarks>Note the 'reversed' order of <paramref name="y"/> and <paramref name="x"/>.</remarks>
    public static Angle Atan2(double y, double x) => new(Math.Atan2(y, x));

    /// <summary>Computes the <see cref="Angle"/> for which the hyperbolic sine is <paramref name="hyperbolicSine"/>.</summary>
    public static Angle Asinh(Scalar hyperbolicSine) => Asinh(hyperbolicSine.Magnitude);
    /// <summary>Computes the <see cref="Angle"/> for which the hyperbolic sine is <paramref name="hyperbolicSine"/>.</summary>
    public static Angle Asinh(double hyperbolicSine) => new(Math.Asinh(hyperbolicSine));
    /// <summary>Computes the <see cref="Angle"/> for which the hyperbolic cosine is <paramref name="hyperbolicCosine"/>.</summary>
    public static Angle Acosh(Scalar hyperbolicCosine) => Acosh(hyperbolicCosine.Magnitude);
    /// <summary>Computes the <see cref="Angle"/> for which the hyperbolic cosine is <paramref name="hyperbolicCosine"/>.</summary>
    public static Angle Acosh(double hyperbolicCosine) => new(Math.Acosh(hyperbolicCosine));
    /// <summary>Computes the <see cref="Angle"/> for which the hyperbolic tangent is <paramref name="hyperbolicTangent"/>.</summary>
    public static Angle Atanh(Scalar hyperbolicTangent) => Atanh(hyperbolicTangent.Magnitude);
    /// <summary>Computes the <see cref="Angle"/> for which the hyperbolic tangent is <paramref name="hyperbolicTangent"/>.</summary>
    public static Angle Atanh(double hyperbolicTangent) => new(Math.Atanh(hyperbolicTangent));
}
