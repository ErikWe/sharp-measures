namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class DivideBy_Scalar
{
    private static Unhandled4 Target(Unhandled4 vector, Scalar divisor) => vector.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidScalar))]
    public void MatchComponentsDivideBy(Unhandled4 vector, Scalar divisor)
    {
        var expected = vector.Components.DivideBy(divisor);

        var actual = Target(vector, divisor).Components;

        Assert.Equal(expected, actual);
    }
}
