namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class AsIVector3Quantity_Y
{
    private static Scalar Target(Unhandled3 vector)
    {
        return execute(vector);

        static Scalar execute(IVector3Quantity vector) => vector.Y;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchYMagnitude(Unhandled3 vector)
    {
        var expected = vector.Y.Magnitude;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
