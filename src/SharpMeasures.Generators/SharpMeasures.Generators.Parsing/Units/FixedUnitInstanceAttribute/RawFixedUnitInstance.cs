namespace SharpMeasures.Generators.Parsing.Units.FixedUnitInstanceAttribute;

using SharpMeasures.Generators.Parsing.Units.Common;

/// <inheritdoc cref="IRawFixedUnitInstance"/>
internal sealed record class RawFixedUnitInstance : ARawUnitInstance<IFixedUnitInstanceSyntax>, IRawFixedUnitInstance
{
    /// <summary>Instantiates a <see cref="RawFixedUnitInstance"/>, representing a parsed <see cref="FixedUnitInstanceAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IRawUnitInstance.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IRawUnitInstance.PluralForm" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawFixedUnitInstance.Syntax" path="/summary"/></param>
    public RawFixedUnitInstance(string? name, string? pluralForm, IFixedUnitInstanceSyntax? syntax) : base(name, pluralForm, syntax) { }

    IFixedUnitInstanceSyntax? IRawFixedUnitInstance.Syntax => Syntax;
}
