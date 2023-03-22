namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Multiply_Scalar
{
    private static Unhandled3 Target(Unhandled3 vector, Scalar factor) => vector.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidScalar))]
    public void MatchComponentsMultiply(Unhandled3 vector, Scalar factor)
    {
        var expected = vector.Components.Multiply(factor);

        var actual = Target(vector, factor).Components;

        Assert.Equal(expected, actual);
    }
}
