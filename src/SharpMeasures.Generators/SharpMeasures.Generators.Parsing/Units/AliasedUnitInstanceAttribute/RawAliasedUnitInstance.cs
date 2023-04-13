namespace SharpMeasures.Generators.Parsing.Units.AliasedUnitInstanceAttribute;

using SharpMeasures.Generators.Parsing.Units.Common;

/// <inheritdoc cref="IRawAliasedUnitInstance"/>
internal sealed record class RawAliasedUnitInstance : ARawModifiedUnitInstance<IAliasedUnitInstanceSyntax>, IRawAliasedUnitInstance
{
    /// <summary>Instantiates a <see cref="RawAliasedUnitInstance"/>, representing a parsed <see cref="UnitInstanceAliasAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IRawUnitInstance.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IRawUnitInstance.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IRawModifiedUnitInstance.OriginalUnitInstance" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawAliasedUnitInstance.Syntax" path="/summary"/></param>
    public RawAliasedUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, IAliasedUnitInstanceSyntax? syntax) : base(name, pluralForm, originalUnitInstance, syntax) { }

    IAliasedUnitInstanceSyntax? IRawAliasedUnitInstance.Syntax => Syntax;
}
