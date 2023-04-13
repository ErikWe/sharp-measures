namespace SharpMeasures.Generators.Parsing.Scalars.ExcludeUnitBasesAttribute;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IRawExcludeUnitBases"/>
internal sealed record class RawExcludeUnitBases : IRawExcludeUnitBases
{
    private IReadOnlyList<string?>? ExcludedUnitBases { get; }
    private FilterStackingMode? StackingMode { get; }

    private IExcludeUnitBasesSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawExcludeUnitBases"/>, representing a parsed <see cref="ExcludeUnitBasesAttribute"/>.</summary>
    /// <param name="excludedUnitBases"><inheritdoc cref="IRawExcludeUnitBases.ExcludedUnitBases" path="/summary"/></param>
    /// <param name="stackingMode"><inheritdoc cref="IRawExcludeUnitBases.StackingMode" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawExcludeUnitBases.Syntax" path="/summary"/></param>
    public RawExcludeUnitBases(IReadOnlyList<string?>? excludedUnitBases, FilterStackingMode? stackingMode, IExcludeUnitBasesSyntax? syntax)
    {
        ExcludedUnitBases = excludedUnitBases;
        StackingMode = stackingMode;

        Syntax = syntax;
    }

    IReadOnlyList<string?>? IRawExcludeUnitBases.ExcludedUnitBases => ExcludedUnitBases;
    FilterStackingMode? IRawExcludeUnitBases.StackingMode => StackingMode;

    IExcludeUnitBasesSyntax? IRawExcludeUnitBases.Syntax => Syntax;
}
