namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

internal static class SourceTexts
{
    public static string Alias(string name = "\"Meter\"", string plural = "\"Meters\"", string aliasOf = "\"Metre\"") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [UnitAlias({{name}}, {{plural}}, {{aliasOf}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    public static string Biased(string name = "\"Celsius\"", string plural = "\"Celsius\"", string from = "\"Kelvin\"", string bias = "-273.15") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnit("Kelvin", "Kelvin")]
        [BiasedUnit({{name}}, {{plural}}, {{from}}, {{bias}})]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    public static string DerivedWithID(string name = "\"MetrePerSecond\"", string plural = "\"MetresPerSecond\"", string derivationID = "\"1\"", string units = "\"Metre\", \"Second\"") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnit("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("1", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnit({{name}}, {{plural}}, {{derivationID}}, {{units}})]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    public static string DerivedWithoutID(string name = "\"MetrePerSecond\"", string plural = "\"MetresPerSecond\"", string units = "new[] { \"Metre\", \"Second\" }") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnit("Second", "Seconds")]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnit({{name}}, {{plural}}, {{units}})]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    public static string Fixed(string name = "\"Metre\"", string plural = "\"Metres\"") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit({{name}}, {{plural}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    public static string Prefixed(string name = "\"Kilometre\"", string plural = "\"Kilometres\"", string from = "\"Metre\"",
        string prefix = "MetricPrefixName.Kilo") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit({{name}}, {{plural}}, {{from}}, {{prefix}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    public static string Scaled(string name = "\"Kilometre\"", string plural = "\"Kilometres\"", string from = "\"Metre\"", string value = "1000") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [ScaledUnit({{name}}, {{plural}}, {{from}}, {{value}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
