namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MassFlowRateTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(MassTimeEquivalence))]
    public void MassTime_ShouldBeEquivalent(Mass mass, Time time, MassFlowRate expected)
    {
        IEnumerable<MassFlowRate> actual = new MassFlowRate[]
        {
            MassFlowRate.From(mass, time),
            mass.Divide(time),
            mass / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> MassTimeEquivalence()
    {
        yield return new object[] { 3 * Mass.OneGram, 9 * Time.OneSecond, 0.003 / 9 * MassFlowRate.OneKilogramPerSecond };
    }
}
