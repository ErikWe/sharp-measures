namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.VolumetricFlowRateTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(VolumeTimeEquivalence))]
    public void VolumeTime_ShouldBeEquivalent(Volume volume, Time time, VolumetricFlowRate expected)
    {
        IEnumerable<VolumetricFlowRate> actual = new VolumetricFlowRate[]
        {
            VolumetricFlowRate.From(volume, time),
            volume.Divide(time),
            volume / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> VolumeTimeEquivalence()
    {
        yield return new object[] { 3 * Volume.OneLitre, 9 * Time.OneMinute, 3 / (9 * 60) * VolumetricFlowRate.OneLitrePerSecond };
    }
}
