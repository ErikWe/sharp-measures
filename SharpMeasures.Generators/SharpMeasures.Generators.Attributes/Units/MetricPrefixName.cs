namespace SharpMeasures.Generators.Units;

/// <summary>Represents the names of available metric prefixes.</summary>
public enum MetricPrefixName
{
    /// <summary>Represents a factor of one septillion [10^24 = (1 000)^8]. Usually written as [Y].</summary>
    Yotta,
    /// <summary>Represents a factor of one sextillion [10^21 = (1 000)^7]. Usually written as [Z].</summary>
    Zetta,
    /// <summary>Represents a factor of one quintillion [10^18 = (1 000)^6]. Usually written as [E].</summary>
    Exa,
    /// <summary>Represents a factor of one quadrillion [10^15 = (1 000)^5]. Usually written as [P].</summary>
    Peta,
    /// <summary>Represents a factor of one trillion [10^12 = (1 000)^4]. Usually written as [T].</summary>
    Tera,
    /// <summary>Represents a factor of one billion [10^9 = (1 000)^3 = 1 000 000 000]. Usually written as [G].</summary>
    Giga,
    /// <summary>Represents a factor of one million [10^6 = (1 000)^2 = 1 000 000]. Usually written as [M].</summary>
    Mega,
    /// <summary>Represents a factor of one thousand [10^3 = 1 000]. Usually written as [k].</summary>
    Kilo,
    /// <summary>Represents a factor of one hundred [10^2 = 100]. Usually written as [h].</summary>
    Hecto,
    /// <summary>Represents a factor of ten [10^1 = 10]. Usually written as [da].</summary>
    Deca,
    /// <summary>Represents a factor of one [10^0 = 1].</summary>
    Identity,
    /// <summary>Represents a factor of one tenth [10^(-1) = 0.1]. Usually written as [d].</summary>
    Deci,
    /// <summary>Represents a factor of one hundreth [10^(-2) = 0.01]. Usually written as [c].</summary>
    Centi,
    /// <summary>Represents a factor of one thousandth [10^(-3) = (1 000)^(-1) = 0.001]. Usually written as [m].</summary>
    Milli,
    /// <summary>Represents a factor of one millionth [10^(-6) = (1 000)^(-2) = 0.000 001]. Usually written as [μ].</summary>
    Micro,
    /// <summary>Represents a factor of one billionth [10^(-9) = (1 000)^(-3) = 0.000 000 001]. Usually written as [n].</summary>
    Nano,
    /// <summary>Represents a factor of one trillionth [10^(-12) = (1 000)^(-4)]. Usually written as [p].</summary>
    Pico,
    /// <summary>Represents a factor of one quadrillionth [10^(-15) = (1 000)^(-5)]. Usually written as [f].</summary>
    Femto,
    /// <summary>Represents a factor of one quintillionth [10^(-18) = (1 000)^(-6)]. Usually written as [a].</summary>
    Atto,
    /// <summary>Represents a factor of one sextillionth [10^(-21) = (1 000)^(-7)]. Usually written as [z].</summary>
    Zepto,
    /// <summary>Represents a factor of one septillionth [10^(-24) = (1 000)^(-8)]. Usually written as [y].</summary>
    Yocto
}
