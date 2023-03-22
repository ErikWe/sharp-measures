namespace SharpMeasures.Tests.Vector2Cases;

using System;

using Xunit;

public class Multiply_Vector2_TScalar
{
    private static (TScalar X, TScalar Y) Target<TScalar>(Vector2 a, TScalar b) where TScalar : IScalarQuantity<TScalar> => Vector2.Multiply(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidVector2();
        var b = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidScalar))]
    public void Valid_MatchInstanceMethod(Vector2 a, Scalar b)
    {
        var expected = a.Multiply<Scalar>(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Vector2 a, TScalar b) where TException : Exception where TScalar : IScalarQuantity<TScalar>
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
