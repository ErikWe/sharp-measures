namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class AsIVectorQuantity_SquaredMagnitude
{
    private static Scalar Target(Unhandled4 vector)
    {
        return execute(vector);

        static Scalar execute(IVectorQuantity vector) => vector.SquaredMagnitude();
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchPureSquaredMagnitude(Unhandled4 vector)
    {
        var expected = vector.PureSquaredMagnitude();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
