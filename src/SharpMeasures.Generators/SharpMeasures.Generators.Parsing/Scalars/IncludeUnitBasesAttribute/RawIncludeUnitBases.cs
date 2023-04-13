namespace SharpMeasures.Generators.Parsing.Scalars.IncludeUnitBasesAttribute;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IRawIncludeUnitBases"/>
internal sealed record class RawIncludeUnitBases : IRawIncludeUnitBases
{
    private IReadOnlyList<string?>? IncludedUnitBases { get; }
    private FilterStackingMode? StackingMode { get; }

    private IIncludeUnitBasesSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawIncludeUnitBases"/>, representing a parsed <see cref="IncludeUnitBasesAttribute"/>.</summary>
    /// <param name="includedUnitBases"><inheritdoc cref="IRawIncludeUnitBases.IncludedUnitBases" path="/summary"/></param>
    /// <param name="stackingMode"><inheritdoc cref="IRawIncludeUnitBases.StackingMode" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawIncludeUnitBases.Syntax" path="/summary"/></param>
    public RawIncludeUnitBases(IReadOnlyList<string?>? includedUnitBases, FilterStackingMode? stackingMode, IIncludeUnitBasesSyntax? syntax)
    {
        IncludedUnitBases = includedUnitBases;
        StackingMode = stackingMode;

        Syntax = syntax;
    }

    IReadOnlyList<string?>? IRawIncludeUnitBases.IncludedUnitBases => IncludedUnitBases;
    FilterStackingMode? IRawIncludeUnitBases.StackingMode => StackingMode;

    IIncludeUnitBasesSyntax? IRawIncludeUnitBases.Syntax => Syntax;
}
