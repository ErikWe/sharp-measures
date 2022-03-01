namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureDifferenceTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(TemperatureTemperatureEquivalence))]
    public void TemperatureTemperature_ShouldBeEquivalent(Temperature initialTemperature, Temperature finalTemperature, TemperatureDifference expected)
    {
        IEnumerable<TemperatureDifference> actual = new TemperatureDifference[]
        {
            TemperatureDifference.From(initialTemperature, finalTemperature),
            finalTemperature.Subtract(initialTemperature),
            finalTemperature - initialTemperature
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> TemperatureTemperatureEquivalence()
    {
        yield return new object[] { 15 * Temperature.OneKelvin, 37 * Temperature.OneKelvin, 22 * TemperatureDifference.OneCelsius };
    }
}
