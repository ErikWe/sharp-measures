namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

internal static class Common
{
    public static string Alias(string name = "\"Meter\"", string plural = "\"Meters\"", string aliasOf = "\"Metre\"") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres", 1)]
        [UnitAlias({{name}}, {{plural}}, {{aliasOf}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    public static string Biased(string name = "\"Celsius\"", string plural = "\"Celsius\"", string from = "\"Kelvin\"", string bias = "-273.15") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnit("Kelvin", "Kelvin", 1, 0)]
        [BiasedUnit({{name}}, {{plural}}, {{from}}, {{bias}})]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    public static string DerivedWithoutSignature(string name = "\"MetrePerSecond\"", string plural = "\"MetresPerSecond\"", string units = "\"Metre\", \"Second\"") =>
        DerivedWithSignature(name, plural, "null", $$"""new[] { {{units}} }""");

    public static string DerivedWithSignature(string name = "\"MetrePerSecond\"", string plural = "\"MetresPerSecond\"",
        string signatureID = "1", string units = "new[] { \"Metre\", \"Second\" }") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnit("Second", "Seconds", 1)]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("1", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnit({{name}}, {{plural}}, {{signatureID}}, {{units}})]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    public static string Fixed(string name = "\"Metre\"", string plural = "\"Metres\"", string value = "1", string bias = "0") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit({{name}}, {{plural}}, {{value}}, {{bias}})]
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

        [FixedUnit("Metre", "Metres", 1)]
        [PrefixedUnit({{name}}, {{plural}}, {{from}}, {{prefix}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    public static string Scaled(string name = "\"Kilometre\"", string plural = "\"Kilometres\"", string from = "\"Metre\"", string value = "1000") => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres", 1)]
        [ScaledUnit({{name}}, {{plural}}, {{from}}, {{value}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
