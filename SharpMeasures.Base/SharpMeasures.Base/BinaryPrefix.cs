namespace SharpMeasures;

using System;

/// <summary>Describes a binary prefix. Common <see cref="BinaryPrefix"/> exists as static properties.</summary>
public readonly record struct BinaryPrefix : IPrefix, IComparable<BinaryPrefix>
{
    /// <summary>Denotes that the value should be scaled by [2^80 = (1 024)^8]. Usually written as [Yi].</summary>
    public static BinaryPrefix Yobi { get; } = ThousandTwentyFourToThePower(8);
    /// <summary>Denotes that the value should be scaled by [2^70 = (1 024)^7]. Usually written as [Zi].</summary>
    public static BinaryPrefix Zebi { get; } = ThousandTwentyFourToThePower(7);
    /// <summary>Denotes that the value should be scaled by [2^60 = (1 024)^6]. Usually written as [Ei].</summary>
    public static BinaryPrefix Exbi { get; } = ThousandTwentyFourToThePower(6);
    /// <summary>Denotes that the value should be scaled by [2^50 = (1 024)^5]. Usually written as [Pi].</summary>
    public static BinaryPrefix Pebi { get; } = ThousandTwentyFourToThePower(5);
    /// <summary>Denotes that the value should be scaled by [2^40 = (1 024)^4]. Usually written as [Ti].</summary>
    public static BinaryPrefix Tebi { get; } = ThousandTwentyFourToThePower(4);
    /// <summary>Denotes that the value should be scaled by [2^30 = (1 024)^3]. Usually written as [Gi].</summary>
    public static BinaryPrefix Gibi { get; } = ThousandTwentyFourToThePower(3);
    /// <summary>Denotes that the value should be scaled by [2^20 = (1 024)^2 = 1 048 576]. Usually written as [Mi].</summary>
    public static BinaryPrefix Mebi { get; } = ThousandTwentyFourToThePower(2);
    /// <summary>Denotes that the value should be scaled by [2^10 = 1 024]. Usually written as [Ki].</summary>
    public static BinaryPrefix Kibi { get; } = ThousandTwentyFourToThePower(1);
    /// <summary>Denotes that the value should not be scaled, or scaled by one.</summary>
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
    /// <inheritdoc cref="MetricPrefix.ToString"/>
    public override string ToString() => $"{Factor}x";

    /// <inheritdoc cref="MetricPrefix.operator &lt;"/>
    public static bool operator <(BinaryPrefix x, BinaryPrefix y) => x.Factor < y.Factor;
    /// <inheritdoc cref="MetricPrefix.operator &gt;"/>
    public static bool operator >(BinaryPrefix x, BinaryPrefix y) => x.Factor > y.Factor;
    /// <inheritdoc cref="MetricPrefix.operator &lt;="/>
    public static bool operator <=(BinaryPrefix x, BinaryPrefix y) => x.Factor <= y.Factor;
    /// <inheritdoc cref="MetricPrefix.operator &gt;="/>
    public static bool operator >=(BinaryPrefix x, BinaryPrefix y) => x.Factor >= y.Factor;
}
