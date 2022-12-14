namespace SharpMeasures.Generators.Vectors.SourceBuilding.Groups.MemberFactory;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, Optional<DataModel> data)
    {
        if (context.CancellationToken.IsCancellationRequested || data.HasValue is false)
        {
            return;
        }

        var source = Composer.Compose(data.Value);

        context.AddSource($"{data.Value.Group.QualifiedName}.MemberFactory.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private sealed class Composer
    {
        public static string Compose(DataModel data)
        {
            Composer composer = new(data);
            composer.Compose();
            return composer.Retrieve();
        }

        private StringBuilder Builder { get; } = new();
        private NewlineSeparationHandler SeparationHandler { get; }

        private DataModel Data { get; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContent);

            NamespaceBuilding.AppendNamespace(Builder, Data.Group.Namespace);

            AppendDocumentation(new Indentation(0), Data.SourceBuildingContext.Documentation.Header());
            Builder.Append(Data.Group.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve() => Builder.ToString();

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            foreach (var member in Data.MembersByDimension)
            {
                AppendScalarFactoryMethod(indentation, member.Value, member.Key);
                AppendVectorNFactoryMethod(indentation, member.Value, member.Key);
                AppendComponentsFactoryMethod(indentation, member.Value, member.Key);
            }
        }

        private void AppendScalarFactoryMethod(Indentation indentation, NamedType member, int dimension)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ScalarFactoryMethod(dimension));
            Builder.AppendLine($"{indentation}public static {member.FullyQualifiedName} Create({ConstantVectorTexts.Lower.Scalar(dimension)}, {Data.Unit.FullyQualifiedName} {Data.UnitParameterName}) " +
                $"=> new({ConstantVectorTexts.Lower.Name(dimension)}, {Data.UnitParameterName});");
        }

        private void AppendVectorNFactoryMethod(Indentation indentation, NamedType member, int dimension)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.VectorFactoryMethod(dimension));
            Builder.AppendLine($"{indentation}public static {member.FullyQualifiedName} Create(global::SharpMeasures.Vector{dimension} components, {Data.Unit.FullyQualifiedName} {Data.UnitParameterName}) => new(components, {Data.UnitParameterName});");
        }

        private void AppendComponentsFactoryMethod(Indentation indentation, NamedType member, int dimension)
        {
            if (Data.Scalar is null)
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ComponentsFactoryMethod(dimension));
            Builder.AppendLine($"{indentation}public static {member.FullyQualifiedName} Create({CommonTextBuilders.Lower.Component(Data.Scalar.Value.Name).GetText(dimension)}) => new({ConstantVectorTexts.Lower.Name(dimension)});");
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
