namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Multiply_Scalar
{
    private static Unhandled4 Target(Unhandled4 vector, Scalar factor) => vector.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidScalar))]
    public void MatchComponentsMultiply(Unhandled4 vector, Scalar factor)
    {
        var expected = vector.Components.Multiply(factor);

        var actual = Target(vector, factor).Components;

        Assert.Equal(expected, actual);
    }
}
