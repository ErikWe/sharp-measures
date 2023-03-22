namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Multiply_TScalar
{
    private static Unhandled3 Target<TScalar>(Unhandled3 vector, TScalar factor) where TScalar : IScalarQuantity => vector.Multiply(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled3();
        var factor = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(vector, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidScalar))]
    public void Valid_MatchComponentsMultiply(Unhandled3 vector, Scalar factor)
    {
        var expected = vector.Components.Multiply(factor);

        var actual = Target(vector, factor).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled3 vector, TScalar factor) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(vector, factor));

        Assert.IsType<TException>(exception);
    }
}
