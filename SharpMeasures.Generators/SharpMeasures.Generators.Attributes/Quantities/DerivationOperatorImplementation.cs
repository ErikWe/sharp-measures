namespace SharpMeasures.Generators.Quantities;

/// <summary>Describes how a derivation is implemented through operators.</summary>
public enum DerivationOperatorImplementation
{
    /// <summary>No operators are implemented.</summary>
    None,
    /// <summary>All operators involving the target type are implemented.</summary>
    /// <remarks>This can result in duplicate operator definitions if two types both define an algebraically equivalent derivation. In that case, consider preferring <see cref="Suitable"/>, <see cref="LeftHandSide"/>, or <see cref="RightHandSide"/>.</remarks>
    All,
    /// <summary>Only operators where the target type is on the left side of the operator are implemented.</summary>
    LeftHandSide,
    /// <summary>Only operators where the target type is on the right side of the operator are implemented.</summary>
    RightHandSide,
    /// <summary>Behaves identical to <see cref="LeftHandSide"/> in most cases, but identical to <see cref="All"/> if the other type involved in the derivation is defined in another assembly (as this means
    /// that the other type impossibly can implement the <see cref="RightHandSide"/> operator).</summary>
    Suitable
}
