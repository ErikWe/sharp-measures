#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfTorqueTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTorqueDataset, UnitOfTorqueDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfTorque a, UnitOfTorque b)
    {
        if (a.Torque.CompareTo(b.Torque) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Torque.CompareTo(b.Torque) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTorqueDataset, UnitOfTorqueDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfTorque a, UnitOfTorque b)
    {
        if (a.Torque > b.Torque)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Torque < b.Torque)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Torque == b.Torque)
        {
            Assert.False(a > b);
            Assert.True(a >= b);
            Assert.True(a <= b);
            Assert.False(a < b);
        }
        else
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
    }
}
