namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class Multiply_Unhandled2_TScalar
{
    private static Unhandled2 Target<TScalar>(Unhandled2 a, TScalar b) where TScalar : IScalarQuantity => Unhandled2.Multiply(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled2();
        var b = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidScalar))]
    public void Valid_MatchInstanceMethod(Unhandled2 a, Scalar b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled2 a, TScalar b) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
