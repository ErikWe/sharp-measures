#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularAcceleration3Dataset))]
    public void ToTuple_ComponentsShouldMatch(OrbitalAngularAcceleration3 quantity)
    {
        (double x, double y, double z) result = quantity.ToValueTuple();

        Utility.AssertExtra.AssertEqualComponents(quantity, result);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularAcceleration3Dataset))]
    public void CastToTuple_ComponentsShouldMatch(OrbitalAngularAcceleration3 quantity)
    {
        (double x, double y, double z) result = ((double, double, double))quantity;

        Utility.AssertExtra.AssertEqualComponents(quantity, result);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularAcceleration3Dataset))]
    public void ToVector3_ComponentsShouldMatch(OrbitalAngularAcceleration3 quantity)
    {
        Vector3 result = quantity.ToVector3();

        Utility.AssertExtra.AssertEqualComponents(quantity, result.ToValueTuple());
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularAcceleration3Dataset))]
    public void CastToVector3_ComponentsShouldMatch(OrbitalAngularAcceleration3 quantity)
    {
        Vector3 result = (Vector3)quantity;

        Utility.AssertExtra.AssertEqualComponents(quantity, result.ToValueTuple());
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void FromTuple_ComponentsShouldMatch(double x, double y, double z)
    {
        OrbitalAngularAcceleration3 result = OrbitalAngularAcceleration3.FromValueTuple((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), result);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void CastFromTuple_ShouldMatchMagnitude(double x, double y, double z)
    {
        OrbitalAngularAcceleration3 result = (OrbitalAngularAcceleration3)(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), result);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void FromVector3_ShouldMatchMagnitude(Vector3 vector)
    {
        OrbitalAngularAcceleration3 result = OrbitalAngularAcceleration3.FromVector3(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), result);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void CastFromVector3_ShouldMatchMagnitude(Vector3 vector)
    {
        OrbitalAngularAcceleration3 result = (OrbitalAngularAcceleration3)vector;

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), result);
    }
}
