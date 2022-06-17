namespace SharpMeasures.Generators.Units;

/// <summary>Represents the names of available binary prefixes.</summary>
public enum BinaryPrefixName
{
    /// <summary>Represents a factor of [2^80 = (1 024)^8]. Usually written as [Yi].</summary>
    Yobi,
    /// <summary>Represents a factor of [2^70 = (1 024)^7]. Usually written as [Zi].</summary>
    Zebi,
    /// <summary>Represents a factor of [2^60 = (1 024)^6]. Usually written as [Ei]</summary>
    Exbi,
    /// <summary>Represents a factor of [2^50 = (1 024)^5]. Usually written as [Pi].</summary>
    Pebi,
    /// <summary>Represents a factor of [2^40 = (1 024)^4]. Usually written as [Ti].</summary>
    Tebi,
    /// <summary>Represents a factor of [2^30 = (1 024)^3]. Usually written as [Gi].</summary>
    Gibi,
    /// <summary>Represents a factor of [2^20 = (1 024)^2 = 1 048 576]. Usually written as [Mi].</summary>
    Mebi,
    /// <summary>Represents a factor of [2^10 = 1 024]. Usually written as [Ki].</summary>
    Kibi,
    /// <summary>Represents a factor of one [2^0 = 1].</summary>
    Identity
}
