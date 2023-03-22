namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class DivideBy_Scalar
{
    private static Unhandled3 Target(Unhandled3 vector, Scalar divisor) => vector.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidScalar))]
    public void MatchComponentsDivideBy(Unhandled3 vector, Scalar divisor)
    {
        var expected = vector.Components.DivideBy(divisor);

        var actual = Target(vector, divisor).Components;

        Assert.Equal(expected, actual);
    }
}
