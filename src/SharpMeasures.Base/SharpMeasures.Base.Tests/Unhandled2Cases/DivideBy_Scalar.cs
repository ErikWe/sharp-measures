namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class DivideBy_Scalar
{
    private static Unhandled2 Target(Unhandled2 vector, Scalar divisor) => vector.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidScalar))]
    public void MatchComponentsDivideBy(Unhandled2 vector, Scalar divisor)
    {
        var expected = vector.Components.DivideBy(divisor);

        var actual = Target(vector, divisor).Components;

        Assert.Equal(expected, actual);
    }
}
