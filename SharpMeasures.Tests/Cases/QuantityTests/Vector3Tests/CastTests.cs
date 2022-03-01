namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Vector3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void ToTuple_ComponentsShouldMatch(Vector3 vector)
    {
        (double x, double y, double z) result = vector.ToValueTuple();

        Utility.AssertExtra.AssertEqualComponents(vector, result);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void CastToTuple_ComponentsShouldMatch(Vector3 vector)
    {
        (double x, double y, double z) result = ((double, double, double))vector;

        Utility.AssertExtra.AssertEqualComponents(vector, result);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void FromTuple_ComponentsShouldMatch(double x, double y, double z)
    {
        Vector3 result = Vector3.FromValueTuple((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), result);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void CastFromTuple_ShouldMatchMagnitude(double x, double y, double z)
    {
        Vector3 result = (Vector3)(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), result);
    }
}
