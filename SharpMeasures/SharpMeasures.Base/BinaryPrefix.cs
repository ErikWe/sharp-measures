namespace SharpMeasures;

using System;

/// <summary>Describes a binary prefix. Common <see cref="BinaryPrefix"/> exists as static properties</summary>
public readonly record struct BinaryPrefix :
    IComparable<BinaryPrefix>
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

    /// <summary>Constructs a custom <see cref="BinaryPrefix"/>, with factor 2^(<paramref name="power"/>).</summary>
    /// <param name="power">The factor of the constructed <see cref="BinaryPrefix"/> will be 2 raised to this power.</param>
    public static BinaryPrefix TwoToThePower(double power) => new(Math.Pow(2, power));

    /// <summary>Constructs a custom <see cref="BinaryPrefix"/>, with factor (1 024)^(<paramref name="power"/>).</summary>
    /// <param name="power">The factor of the constructed <see cref="BinaryPrefix"/> will be [1 024] raised to this power.</param>
    public static BinaryPrefix ThousandTwentyFourToThePower(double power) => new(Math.Pow(1024, power));

    /// <summary>The amount that values should be scaled by.</summary>
    public Scalar Factor { get; init; }

    /// <summary>Constructs a new <see cref="BinaryPrefix"/> with <paramref name="factor"/>.</summary>
    /// <param name="factor">The amount that values should be scaled by.</param>
    public BinaryPrefix(Scalar factor)
    {
        Factor = factor;
    }

    /// <summary>Constructs a new <see cref="BinaryPrefix"/> with <paramref name="factor"/>.</summary>
    /// <param name="factor">The amount that values should be scaled by.</param>
    public BinaryPrefix(double factor) : this(Scalar.FromDouble(factor)) { }

    /// <inheritdoc/>
    public int CompareTo(BinaryPrefix other) => Factor.CompareTo(other.Factor);
    /// <summary>Produces a formatted string from the factor of the <see cref="BinaryPrefix"/> - followed by 'x', to indicate scaling.</summary>
    public override string ToString() => $"{Factor}x";

    /// <summary>Determines whether the factor of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the factor of this <see cref="BinaryPrefix"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the factor of <paramref name="x"/> is less than that of this <see cref="BinaryPrefix"/>.</param>
    public static bool operator <(BinaryPrefix x, BinaryPrefix y) => x.Factor < y.Factor;
    /// <summary>Determines whether the factor of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the factor of this <see cref="BinaryPrefix"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the factor of <paramref name="x"/> is greater than that of this <see cref="BinaryPrefix"/>.</param>
    public static bool operator >(BinaryPrefix x, BinaryPrefix y) => x.Factor > y.Factor;
    /// <summary>Determines whether the factor of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the factor of this <see cref="BinaryPrefix"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the factor of <paramref name="x"/> is less than or equal to that of this <see cref="BinaryPrefix"/>.</param>
    public static bool operator <=(BinaryPrefix x, BinaryPrefix y) => x.Factor <= y.Factor;
    /// <summary>Determines whether the factor of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the factor of this <see cref="BinaryPrefix"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the factor of <paramref name="x"/> is greater than or equal to that of this <see cref="BinaryPrefix"/>.</param>
    public static bool operator >=(BinaryPrefix x, BinaryPrefix y) => x.Factor >= y.Factor;
}