namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Multiply_Scalar
{
    private static Unhandled2 Target(Unhandled2 vector, Scalar factor) => vector.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidScalar))]
    public void MatchComponentsMultiply(Unhandled2 vector, Scalar factor)
    {
        var expected = vector.Components.Multiply(factor);

        var actual = Target(vector, factor).Components;

        Assert.Equal(expected, actual);
    }
}
