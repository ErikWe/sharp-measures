namespace SharpMeasures.Generators.Parsing.Quantities.ExcludeUnitsAttribute;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IRawExcludeUnits"/>
internal sealed record class RawExcludeUnits : IRawExcludeUnits
{
    private IReadOnlyList<string?>? ExcludedUnits { get; }
    private FilterStackingMode? StackingMode { get; }

    private IExcludeUnitsSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawExcludeUnits"/>, representing a parsed <see cref="ExcludeUnitsAttribute"/>.</summary>
    /// <param name="excludedUnits"><inheritdoc cref="IRawExcludeUnits.ExcludedUnits" path="/summary"/></param>
    /// <param name="stackingMode"><inheritdoc cref="IRawExcludeUnits.StackingMode" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawExcludeUnits.Syntax" path="/summary"/></param>
    public RawExcludeUnits(IReadOnlyList<string?>? excludedUnits, FilterStackingMode? stackingMode, IExcludeUnitsSyntax? syntax)
    {
        ExcludedUnits = excludedUnits;
        StackingMode = stackingMode;

        Syntax = syntax;
    }

    IReadOnlyList<string?>? IRawExcludeUnits.ExcludedUnits => ExcludedUnits;
    FilterStackingMode? IRawExcludeUnits.StackingMode => StackingMode;

    IExcludeUnitsSyntax? IRawExcludeUnits.Syntax => Syntax;
}
