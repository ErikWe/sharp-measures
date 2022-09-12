namespace SharpMeasures.Generators.Quantities;

/// <summary>Describes how a derivation is implemented through operators.</summary>
public enum DerivationOperatorImplementation
{
    /// <summary>No operators are implemented.</summary>
    None,
    /// <summary>Only the exact provided derivation is implemented thorugh an operator.</summary>
    /// <remarks>For example; for a derived quantity { <i>Speed</i> = <i>Length</i> / <i>Time</i> }, <i>Length</i> would implement the operator { / }, with
    /// arguments (<i>Length</i>, <i>Time</i>), resulting in <i>Speed</i>.</remarks>
    Exact,
    /// <summary>All algebraically equivalent derivations are implemented through operators.</summary>
    /// <remarks>For example; for a derived quantity { <i>Speed</i> = <i>Length</i> / <i>Time</i> }, the following operations would be implemented:
    /// <list type="bullet">
    /// <item>
    /// <i>Length</i> implements the operator { / } with arguments { <i>Length</i>, <i>Time</i> }, resulting in <i>Speed</i>.
    /// </item>
    /// <item>
    /// <i>Length</i> implements the operator { / } with arguments { <i>Length</i>, <i>Speed</i> }, resulting in <i>Time</i>.
    /// </item>
    /// <item>
    /// <i>Speed</i> implements the operator { * } with arguments { <i>Speed</i>, <i>Time</i> }, resulting in <i>Length</i>.
    /// </item>
    /// <item>
    /// <i>Time</i> implements the operator { * } with arguments { <i>Time</i>, <i>Speed</i> }, resulting in <i>Length</i>.
    /// </item>
    /// </list>
    /// </remarks>
    AlgebraicallyEquivalent
}
