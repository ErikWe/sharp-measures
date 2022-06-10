namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;

internal interface IDataModel<TSelf> where TSelf : IDataModel<TSelf>
{
    public abstract string Identifier { get; }
    public abstract Location? IdentifierLocation { get; }

    public abstract bool ExplicitlySetGenerateDocumentation { get; }
    public abstract bool GenerateDocumentation { get; }

    public abstract TSelf WithDocumentation(DocumentationFile documentation);
}
