#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TimeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTimeDataset>))]
    public void InUnit(Scalar expected, UnitOfTime unit)
    {
        Time quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSecond(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.Second);

        Scalar actual = quantity.Seconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMinute(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.Minute);

        Scalar actual = quantity.Minutes;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InHour(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.Hour);

        Scalar actual = quantity.Hours;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InDay(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.Day);

        Scalar actual = quantity.Days;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InWeek(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.Week);

        Scalar actual = quantity.Weeks;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCommonYear(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.CommonYear);

        Scalar actual = quantity.CommonYears;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InJulianYear(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.JulianYear);

        Scalar actual = quantity.JulianYears;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InFemtosecond(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.Femtosecond);

        Scalar actual = quantity.Femtoseconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPicosecond(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.Picosecond);

        Scalar actual = quantity.Picoseconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InNanosecond(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.Nanosecond);

        Scalar actual = quantity.Nanoseconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMicrosecond(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.Microsecond);

        Scalar actual = quantity.Microseconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMillisecond(Scalar expected)
    {
        Time quantity = new(expected, UnitOfTime.Millisecond);

        Scalar actual = quantity.Milliseconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
