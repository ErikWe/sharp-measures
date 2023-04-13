namespace SharpMeasures.Generators.Parsing.Units.DerivedUnitInstanceAttribute;

using SharpMeasures;
using SharpMeasures.Generators.Parsing.Units.Common;

using System.Collections.Generic;

/// <inheritdoc cref="IRawDerivedUnitInstance"/>
internal sealed record class RawDerivedUnitInstance : ARawUnitInstance<IDerivedUnitInstanceSyntax>, IRawDerivedUnitInstance
{
    private string? DerivationID { get; }
    private IReadOnlyList<string?>? Units { get; }

    /// <summary>Instantiates a <see cref="RawDerivedUnitInstance"/>, representing a parsed <see cref="DerivedUnitInstanceAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IRawUnitInstance.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IRawUnitInstance.PluralForm" path="/summary"/></param>
    /// <param name="derivationID"><inheritdoc cref="IRawDerivedUnitInstance.DerivationID" path="/summary"/></param>
    /// <param name="units"><inheritdoc cref="IRawDerivedUnitInstance.Units" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawDerivedUnitInstance.Syntax" path="/summary"/></param>
    public RawDerivedUnitInstance(string? name, string? pluralForm, string? derivationID, IReadOnlyList<string?>? units, IDerivedUnitInstanceSyntax? syntax) : base(name, pluralForm, syntax)
    {
        DerivationID = derivationID;
        Units = units;
    }

    string? IRawDerivedUnitInstance.DerivationID => DerivationID;
    IReadOnlyList<string?>? IRawDerivedUnitInstance.Units => Units;

    IDerivedUnitInstanceSyntax? IRawDerivedUnitInstance.Syntax => Syntax;
}
