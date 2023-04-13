namespace SharpMeasures.Generators.Parsing.Units.BiasedUnitInstanceAttribute;

using OneOf;

using SharpMeasures;
using SharpMeasures.Generators.Parsing.Units.Common;

/// <inheritdoc cref="IRawBiasedUnitInstance"/>
internal sealed record class RawBiasedUnitInstance : ARawModifiedUnitInstance<IBiasedUnitInstanceSyntax>, IRawBiasedUnitInstance
{
    private OneOf<double, string?> Bias { get; }

    /// <summary>Instantiates a <see cref="RawBiasedUnitInstance"/>, representing a parsed <see cref="BiasedUnitInstanceAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IRawUnitInstance.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IRawUnitInstance.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IRawModifiedUnitInstance.OriginalUnitInstance" path="/summary"/></param>
    /// <param name="bias"><inheritdoc cref="IRawBiasedUnitInstance.Bias" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawBiasedUnitInstance.Syntax" path="/summary"/></param>
    public RawBiasedUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, OneOf<double, string?> bias, IBiasedUnitInstanceSyntax? syntax) : base(name, pluralForm, originalUnitInstance, syntax)
    {
        Bias = bias;
    }

    OneOf<double, string?> IRawBiasedUnitInstance.Bias => Bias;

    IBiasedUnitInstanceSyntax? IRawBiasedUnitInstance.Syntax => Syntax;
}
