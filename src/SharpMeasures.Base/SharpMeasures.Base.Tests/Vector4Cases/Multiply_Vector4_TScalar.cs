namespace SharpMeasures.Tests.Vector4Cases;

using System;

using Xunit;

public class Multiply_Vector4_TScalar
{
    private static (TScalar X, TScalar Y, TScalar Z, TScalar W) Target<TScalar>(Vector4 a, TScalar b) where TScalar : IScalarQuantity<TScalar> => Vector4.Multiply(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidVector4();
        var b = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void Valid_MatchInstanceMethod(Vector4 a, Scalar b)
    {
        var expected = a.Multiply<Scalar>(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Vector4 a, TScalar b) where TException : Exception where TScalar : IScalarQuantity<TScalar>
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
