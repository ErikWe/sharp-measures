namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class DivideBy_TScalar
{
    private static Unhandled3 Target<TScalar>(Unhandled3 vector, TScalar divisor) where TScalar : IScalarQuantity => vector.DivideBy(divisor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled3();
        var divisor = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(vector, divisor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidScalar))]
    public void Valid_MatchComponentsDivideBy(Unhandled3 vector, Scalar divisor)
    {
        var expected = vector.Components.DivideBy(divisor);

        var actual = Target(vector, divisor).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled3 vector, TScalar divisor) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(vector, divisor));

        Assert.IsType<TException>(exception);
    }
}
