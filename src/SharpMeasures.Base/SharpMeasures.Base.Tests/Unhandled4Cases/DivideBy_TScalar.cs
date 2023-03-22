namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class DivideBy_TScalar
{
    private static Unhandled4 Target<TScalar>(Unhandled4 vector, TScalar divisor) where TScalar : IScalarQuantity => vector.DivideBy(divisor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled4();
        var divisor = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(vector, divisor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidScalar))]
    public void Valid_MatchComponentsDivideBy(Unhandled4 vector, Scalar divisor)
    {
        var expected = vector.Components.DivideBy(divisor);

        var actual = Target(vector, divisor).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled4 vector, TScalar divisor) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(vector, divisor));

        Assert.IsType<TException>(exception);
    }
}
