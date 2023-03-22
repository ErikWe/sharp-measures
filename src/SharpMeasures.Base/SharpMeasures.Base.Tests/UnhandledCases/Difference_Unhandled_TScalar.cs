namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Difference_Unhandled_TScalar
{
    private static Unhandled Target<TScalar>(Unhandled x, TScalar y) where TScalar : IScalarQuantity => Unhandled.Difference(x, y);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var x = Datasets.GetValidUnhandled();
        var y = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchInstanceMethod(Unhandled x, Scalar y)
    {
        var expected = x.Difference(y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled x, TScalar y) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(x, y));

        Assert.IsType<TException>(exception);
    }
}
