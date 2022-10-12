namespace SharpMeasures;

using System;

/// <summary>Describes a binary prefix. Common <see cref="BinaryPrefix"/> exists as static properties.</summary>
public readonly record struct BinaryPrefix : IPrefix, IComparable<BinaryPrefix>
{
    /// <summary>Indicates that the value should be scaled by { 2 ^ 80 = 1 024 ^ 8 }. Usually denoted by [Yi].</summary>
    public static BinaryPrefix Yobi { get; } = ThousandTwentyFourToThePower(8);
    /// <summary>Indicates that the value should be scaled by { 2 ^ 70 = 1 024 ^ 7 }. Usually denoted by [Zi].</summary>
    public static BinaryPrefix Zebi { get; } = ThousandTwentyFourToThePower(7);
    /// <summary>Indicates that the value should be scaled by { 2 ^ 60 = 1 024 ^ 6 }. Usually denoted by [Ei].</summary>
    public static BinaryPrefix Exbi { get; } = ThousandTwentyFourToThePower(6);
    /// <summary>Indicates that the value should be scaled by { 2 ^ 50 = 1 024 ^ 5 }. Usually denoted by [Pi].</summary>
    public static BinaryPrefix Pebi { get; } = ThousandTwentyFourToThePower(5);
    /// <summary>Indicates that the value should be scaled by { 2 ^ 40 = 1 024 ^ 4 }. Usually denoted by [Ti].</summary>
    public static BinaryPrefix Tebi { get; } = ThousandTwentyFourToThePower(4);
    /// <summary>Indicates that the value should be scaled by { 2 ^ 30 = 1 024 ^ 3 }. Usually denoted by [Gi].</summary>
    public static BinaryPrefix Gibi { get; } = ThousandTwentyFourToThePower(3);
    /// <summary>Indicates that the value should be scaled by { 2 ^ 20 = 1 024 ^ 2 = 1 048 576 }. Usually denoted by [Mi].</summary>
    public static BinaryPrefix Mebi { get; } = ThousandTwentyFourToThePower(2);
    /// <summary>Indicates that the value should be scaled by { 2 ^ 10 = 1 024 }. Usually denoted by [Ki].</summary>
    public static BinaryPrefix Kibi { get; } = ThousandTwentyFourToThePower(1);
    /// <summary>Indicates that the value should not be scaled.</summary>
    public static BinaryPrefix Identity { get; } = new(1);

    /// <summary>Constructs a custom <see cref="BinaryPrefix"/>, describing a factor { 2 ^ <paramref name="exponent"/> }.</summary>
    /// <param name="exponent">The exponent of { 2 ^ <paramref name="exponent"/> }.</param>
    public static BinaryPrefix TwoToThePower(Scalar exponent) => new(Math.Pow(2, exponent));

    /// <summary>Constructs a custom <see cref="BinaryPrefix"/>, describing a factor { 1 024 ^ <paramref name="exponent"/> }.</summary>
    /// <param name="exponent">The exponent of { 1 024 ^ <paramref name="exponent"/> }.</param>
    public static BinaryPrefix ThousandTwentyFourToThePower(Scalar exponent) => new(Math.Pow(1024, exponent));

    /// <inheritdoc/>
    public Scalar Factor { get; }

    /// <summary>Constructs a <see cref="BinaryPrefix"/> representing { <paramref name="factor"/> }.</summary>
    /// <param name="factor">The scale-factor represented by the constructed <see cref="BinaryPrefix"/>.</param>
    public BinaryPrefix(Scalar factor)
    {
        Factor = factor;
    }

    /// <inheritdoc cref="MetricPrefix.CompareTo(MetricPrefix)"/>
    public int CompareTo(BinaryPrefix other) => Factor.CompareTo(other.Factor);
    /// <summary>Produces a description of <see langword="this"/> containing the represented <see cref="Factor"/>.</summary>
    public override string ToString() => $"{Factor}";

    /// <inheritdoc cref="MetricPrefix.operator &lt;"/>
    public static bool operator <(BinaryPrefix x, BinaryPrefix y) => x.Factor < y.Factor;
    /// <inheritdoc cref="MetricPrefix.operator &gt;"/>
    public static bool operator >(BinaryPrefix x, BinaryPrefix y) => x.Factor > y.Factor;
    /// <inheritdoc cref="MetricPrefix.operator &lt;="/>
    public static bool operator <=(BinaryPrefix x, BinaryPrefix y) => x.Factor <= y.Factor;
    /// <inheritdoc cref="MetricPrefix.operator &gt;="/>
    public static bool operator >=(BinaryPrefix x, BinaryPrefix y) => x.Factor >= y.Factor;
}
