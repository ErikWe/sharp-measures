namespace SharpMeasures.Tests.Vector3Cases;

using System;

using Xunit;

public class Multiply_Vector3_TScalar
{
    private static (TScalar X, TScalar Y, TScalar Z) Target<TScalar>(Vector3 a, TScalar b) where TScalar : IScalarQuantity<TScalar> => Vector3.Multiply(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidVector3();
        var b = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void Valid_MatchInstanceMethod(Vector3 a, Scalar b)
    {
        var expected = a.Multiply<Scalar>(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Vector3 a, TScalar b) where TException : Exception where TScalar : IScalarQuantity<TScalar>
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
