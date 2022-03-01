namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.PowerTests;

using ErikWe.SharpMeasures.Quantities;

using System.Collections.Generic;

using Xunit;

public class DerivedTests
{
    [Theory]
    [MemberData(nameof(WorkTimeEquivalence))]
    public void WorkTime_ShouldBeEquivalent(Work work, Time time, Power expected)
    {
        IEnumerable<Power> actual = new Power[]
        {
            Power.From(work, time),
            work.Divide(time),
            work / time
        };

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    public static IEnumerable<object[]> WorkTimeEquivalence()
    {
        yield return new object[] { 15 * Work.OneKilojoule, 37 * Time.OneSecond, 15d / 37 * Power.OneKilowatt };
    }
}
