namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class AsIVectorQuantity_SquaredMagnitude
{
    private static Scalar Target(Unhandled3 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.SquaredMagnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchPureSquaredMagnitude(Unhandled3 vector)
    {
        var expected = vector.PureSquaredMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
