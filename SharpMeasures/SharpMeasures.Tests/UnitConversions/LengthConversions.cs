namespace SharpMeasures.Tests.UnitConversions;

using SharpMeasures;

using System.Collections.Generic;

using Xunit;

public class LengthConversions
{
    [Theory]
    [MemberData(nameof(EquivalentLengths))]
    public void FromNanometre(double nanometre, double metre, double yard, double lightYear)
    {
        var length = nanometre * Length.OneNanometre;

        Assert.Equal(nanometre, length.Nanometres, 10);
        Assert.Equal(metre, length.Metres, 10);
        Assert.Equal(yard, length.Yards, 10);
        Assert.Equal(lightYear, length.LightYears, 10);
    }

    [Theory]
    [MemberData(nameof(EquivalentLengths))]
    public void FromLightYear(double nanometre, double metre, double yard, double lightYear)
    {
        var length = lightYear * Length.OneLightYear;

        Assert.Equal(nanometre, length.Nanometres, 10);
        Assert.Equal(metre, length.Metres, 10);
        Assert.Equal(yard, length.Yards, 10);
        Assert.Equal(lightYear, length.LightYears, 10);
    }

    public static IEnumerable<object[]> EquivalentLengths() => new object[][]
    {
        new object[] { 0, 0, 0, 0 },
        new object[] { 16780387654867.19, 16780.38765486719, 16780.38765486719 / 0.9144, 16780.38765486719 / 9460730472580800 },
        new object[] { 9460730472580800 * 4.2 * 1E9, 9460730472580800 * 4.2, 9460730472580800 * 4.2 / 0.9144, 4.2 }
    };
}
