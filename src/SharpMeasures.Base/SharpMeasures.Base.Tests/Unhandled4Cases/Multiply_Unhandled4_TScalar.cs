namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class Multiply_Unhandled4_TScalar
{
    private static Unhandled4 Target<TScalar>(Unhandled4 a, TScalar b) where TScalar : IScalarQuantity => Unhandled4.Multiply(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled4();
        var b = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidScalar))]
    public void Valid_MatchInstanceMethod(Unhandled4 a, Scalar b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled4 a, TScalar b) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
