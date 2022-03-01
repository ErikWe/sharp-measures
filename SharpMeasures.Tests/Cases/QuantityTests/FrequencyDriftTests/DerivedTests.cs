namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.FrequencyDriftTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(FrequencyTimeEquivalence))]
    public void FrequencyTime_ShouldBeEquivalent(Frequency frequency, Time time, FrequencyDrift expected)
    {
        IEnumerable<FrequencyDrift> actual = new FrequencyDrift[]
        {
            FrequencyDrift.From(frequency, time),
            frequency.Divide(time),
            frequency / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> FrequencyTimeEquivalence()
    {
        yield return new object[] { 19 * Frequency.OneKilohertz, 3 * Time.OneSecond, 19000d / 3 * FrequencyDrift.OneHertzPerSecond };
    }
}
