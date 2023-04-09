namespace SharpMeasures;

/// <summary>Represents the name of a binary prefix.</summary>
public enum BinaryPrefixName
{
    /// <summary>The <see cref="BinaryPrefixName"/> is unknown.</summary>
    Unknown,
    /// <summary>Represents a factor of { 2 ^ 80 = 1 024 ^ 8 }. Usually denoted by { Yi }.</summary>
    Yobi,
    /// <summary>Represents a factor of { 2 ^ 70 = 1 024 ^ 7 }. Usually denoted by { Zi }.</summary>
    Zebi,
    /// <summary>Represents a factor of { 2 ^ 60 = 1 024 ^ 6 }. Usually denoted by { Ei }</summary>
    Exbi,
    /// <summary>Represents a factor of { 2 ^ 50 = 1 024 ^ 5 }. Usually denoted by { Pi }.</summary>
    Pebi,
    /// <summary>Represents a factor of { 2 ^ 40 = 1 024 ^ 4 }. Usually denoted by { Ti }.</summary>
    Tebi,
    /// <summary>Represents a factor of { 2 ^ 30 = 1 024 ^ 3 }. Usually denoted by { Gi }.</summary>
    Gibi,
    /// <summary>Represents a factor of { 2 ^ 20 = 1 024 ^ 2 = 1 048 576 }. Usually denoted by { Mi }.</summary>
    Mebi,
    /// <summary>Represents a factor of { 2 ^ 10 = 1 024 }. Usually denoted by { Ki }.</summary>
    Kibi
}
