namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class DivideBy_TScalar
{
    private static Unhandled2 Target<TScalar>(Unhandled2 vector, TScalar divisor) where TScalar : IScalarQuantity => vector.DivideBy(divisor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled2();
        var divisor = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(vector, divisor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidScalar))]
    public void Valid_MatchComponentsDivideBy(Unhandled2 vector, Scalar divisor)
    {
        var expected = vector.Components.DivideBy(divisor);

        var actual = Target(vector, divisor).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled2 vector, TScalar divisor) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(vector, divisor));

        Assert.IsType<TException>(exception);
    }
}
