namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class AsIVector3Quantity_Components
{
    private static Vector3 Target(Unhandled3 vector)
    {
        return execute(vector);

        static Vector3 execute(IVector3Quantity vector) => vector.Components;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchComponents(Unhandled3 vector)
    {
        var expected = vector.Components;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
