namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(TemperatureTemperatureDifferenceEquivalence))]
    public void TemperatureTemperatureDifference_ShouldBeEquivalent(Temperature initialTemperature, TemperatureDifference temperatureDifference, Temperature expected)
    {
        IEnumerable<Temperature> actual = new Temperature[]
        {
            Temperature.From(initialTemperature, temperatureDifference),
            initialTemperature.Add(temperatureDifference),
            initialTemperature + temperatureDifference,
            temperatureDifference.Add(initialTemperature),
            temperatureDifference + initialTemperature
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> TemperatureTemperatureDifferenceEquivalence()
    {
        yield return new object[] { 15 * Temperature.OneKelvin, 37 * TemperatureDifference.OneCelsius, 52 * Temperature.OneKelvin };
    }
}
