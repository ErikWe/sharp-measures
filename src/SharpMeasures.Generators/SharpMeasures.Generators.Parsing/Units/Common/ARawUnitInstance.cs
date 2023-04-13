namespace SharpMeasures.Generators.Parsing.Units.Common;

/// <summary>An abstract <see cref="IRawUnitInstance"/>.</summary>
/// <typeparam name="TSyntax">The type of the described <see cref="IUnitInstanceSyntax"/>.</typeparam>
internal abstract record class ARawUnitInstance<TSyntax> : IRawUnitInstance where TSyntax : IUnitInstanceSyntax
{
    private string? Name { get; }
    private string? PluralForm { get; }

    /// <summary>Provides syntactical information about the parsed attribute.</summary>
    protected TSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="ARawUnitInstance{TSyntax}"/>, representing a parsed attribute describing an instance of a unit.</summary>
    /// <param name="name"><inheritdoc cref="IRawUnitInstance.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IRawUnitInstance.PluralForm" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawUnitInstance.Syntax" path="/summary"/></param>
    protected ARawUnitInstance(string? name, string? pluralForm, TSyntax? syntax)
    {
        Name = name;
        PluralForm = pluralForm;

        Syntax = syntax;
    }

    string? IRawUnitInstance.Name => Name;
    string? IRawUnitInstance.PluralForm => PluralForm;

    IUnitInstanceSyntax? IRawUnitInstance.Syntax => Syntax;
}
