namespace SharpMeasures;

/// <summary>Represents the name of a metric prefix.</summary>
public enum MetricPrefixName
{
    /// <summary>The <see cref="MetricPrefixName"/> is unknown.</summary>
    Unknown,
    /// <summary>Represents a factor of one nonillion { 10 ^ 30 = 1 000 ^ 10 }. Usually denoted by { Q }.</summary>
    Quetta,
    /// <summary>Represents a factor of one octillion { 10 ^ 27 = 1 000 ^ 9 }. Usually denoted by { R }.</summary>
    Ronna,
    /// <summary>Represents a factor of one septillion { 10 ^ 24 = 1 000 ^ 8 }. Usually denoted by { Y }.</summary>
    Yotta,
    /// <summary>Represents a factor of one sextillion { 10 ^ 21 = 1 000 ^ 7 }. Usually denoted by { Z }.</summary>
    Zetta,
    /// <summary>Represents a factor of one quintillion { 10 ^ 18 = 1 000 ^ 6 }. Usually denoted by { E }.</summary>
    Exa,
    /// <summary>Represents a factor of one quadrillion { 10 ^ 15 = 1 000 ^ 5 }. Usually denoted by { P }.</summary>
    Peta,
    /// <summary>Represents a factor of one trillion { 10 ^ 12 = 1 000 ^ 4 }. Usually denoted by { T }.</summary>
    Tera,
    /// <summary>Represents a factor of one billion { 10 ^ 9 = 1 000 ^ 3 = 1 000 000 000 }. Usually denoted by { G }.</summary>
    Giga,
    /// <summary>Represents a factor of one million { 10 ^ 6 = 1 000 ^ 2 = 1 000 000 }. Usually denoted by { M }.</summary>
    Mega,
    /// <summary>Represents a factor of one thousand { 10 ^ 3 = 1 000 }. Usually denoted by { k }.</summary>
    Kilo,
    /// <summary>Represents a factor of one hundred { 10 ^ 2 = 100 }. Usually denoted by { h }.</summary>
    Hecto,
    /// <summary>Represents a factor of ten { 10 ^ 1 = 10 }. Usually denoted by { da }.</summary>
    Deca,
    /// <summary>Represents a factor of one tenth { 10 ^ -1 = 0.1 }. Usually denoted by { d }.</summary>
    Deci,
    /// <summary>Represents a factor of one hundreth { 10 ^ -2 = 0.01 }. Usually denoted by { c }.</summary>
    Centi,
    /// <summary>Represents a factor of one thousandth { 10 ^ -3 = 1 000 ^ -1 = 0.001 }. Usually denoted by { m }.</summary>
    Milli,
    /// <summary>Represents a factor of one millionth { 10 ^ -6 = 1 000 ^ -2 = 0.000 001 }. Usually denoted by { μ }.</summary>
    Micro,
    /// <summary>Represents a factor of one billionth { 10 ^ -9 = 1 000 ^ -3 = 0.000 000 001 }. Usually denoted by { n }.</summary>
    Nano,
    /// <summary>Represents a factor of one trillionth { 10 ^ -12 = 1 000 ^ -4 }. Usually denoted by { p }.</summary>
    Pico,
    /// <summary>Represents a factor of one quadrillionth { 10 ^ -15 = 1 000 ^ -5 }. Usually denoted by { f }.</summary>
    Femto,
    /// <summary>Represents a factor of one quintillionth { 10 ^ -18 = 1 000 ^ -6 }. Usually denoted by { a }.</summary>
    Atto,
    /// <summary>Represents a factor of one sextillionth { 10 ^ -21 = 1 000 ^ -7 }. Usually denoted by { z }.</summary>
    Zepto,
    /// <summary>Represents a factor of one septillionth { 10 ^ -24 = 1 000 ^ -8 }. Usually denoted by { y }.</summary>
    Yocto,
    /// <summary>Represents a factor of one octillionth { 10 ^ -27 = 1 000 ^ -9 }. Usually denoted by { r }.</summary>
    Ronto,
    /// <summary>Represents a factor of one nonillionth { 10 ^ -30 = 1 000 ^ -10 }. Usually denoted by { q }.</summary>
    Quecto
}
