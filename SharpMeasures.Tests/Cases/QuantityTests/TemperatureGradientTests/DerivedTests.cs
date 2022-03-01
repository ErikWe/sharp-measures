namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureGradientTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(TemperatureDifferenceDistanceEquivalence))]
    public void TemperatureDifferenceDistance_ShouldBeEquivalent(TemperatureDifference temperatureDifference, Distance distance, TemperatureGradient expected)
    {
        IEnumerable<TemperatureGradient> actual = new TemperatureGradient[]
        {
            TemperatureGradient.From(temperatureDifference, distance),
            temperatureDifference.Divide(distance),
            temperatureDifference / distance
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> TemperatureDifferenceDistanceEquivalence()
    {
        yield return new object[] { 15 * TemperatureDifference.OneCelsius, 37 * Distance.OneCentimetre, 15 / 0.37 * TemperatureGradient.OneKelvinPerMetre };
    }
}
