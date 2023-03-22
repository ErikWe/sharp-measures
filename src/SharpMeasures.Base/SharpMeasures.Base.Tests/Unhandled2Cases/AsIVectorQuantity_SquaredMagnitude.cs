namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class AsIVectorQuantity_SquaredMagnitude
{
    private static Scalar Target(Unhandled2 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.SquaredMagnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchPureSquaredMagnitude(Unhandled2 vector)
    {
        var expected = vector.PureSquaredMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
