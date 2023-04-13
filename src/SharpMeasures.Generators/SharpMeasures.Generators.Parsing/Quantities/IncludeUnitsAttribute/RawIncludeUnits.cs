namespace SharpMeasures.Generators.Parsing.Quantities.IncludeUnitsAttribute;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IRawIncludeUnits"/>
internal sealed record class RawIncludeUnits : IRawIncludeUnits
{
    private IReadOnlyList<string?>? IncludedUnits { get; }
    private FilterStackingMode? StackingMode { get; }

    private IIncludeUnitsSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawIncludeUnits"/>, representing a parsed <see cref="IncludeUnitsAttribute"/>.</summary>
    /// <param name="includedUnits"><inheritdoc cref="IRawIncludeUnits.IncludedUnits" path="/summary"/></param>
    /// <param name="stackingMode"><inheritdoc cref="IRawIncludeUnits.StackingMode" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawIncludeUnits.Syntax" path="/summary"/></param>
    public RawIncludeUnits(IReadOnlyList<string?>? includedUnits, FilterStackingMode? stackingMode, IIncludeUnitsSyntax? syntax)
    {
        IncludedUnits = includedUnits;
        StackingMode = stackingMode;

        Syntax = syntax;
    }

    IReadOnlyList<string?>? IRawIncludeUnits.IncludedUnits => IncludedUnits;
    FilterStackingMode? IRawIncludeUnits.StackingMode => StackingMode;

    IIncludeUnitsSyntax? IRawIncludeUnits.Syntax => Syntax;
}
