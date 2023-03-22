namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Divide_Unhandled3_TScalar
{
    private static Unhandled3 Target<TScalar>(Unhandled3 a, TScalar b) where TScalar : IScalarQuantity => Unhandled3.Divide(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled3();
        var b = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidScalar))]
    public void Valid_MatchInstanceMethod(Unhandled3 a, Scalar b)
    {
        var expected = a.DivideBy(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled3 a, TScalar b) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
