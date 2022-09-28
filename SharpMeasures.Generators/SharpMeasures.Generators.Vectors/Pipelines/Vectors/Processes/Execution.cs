namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Processes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, Optional<DataModel> data)
    {
        if (context.CancellationToken.IsCancellationRequested || data.HasValue is false)
        {
            return;
        }

        string source = Composer.Compose(data.Value);

        if (source.Length is 0)
        {
            return;
        }

        context.AddSource($"{data.Value.Vector.QualifiedName}.Processes.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private HashSet<string> ImplementedProcesses { get; } = new();

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            Builder.Append(Data.Vector.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            foreach (var process in Data.Processes)
            {
                if (ImplementedProcesses.Add(process.Name) is false)
                {
                    continue;
                }

                SeparationHandler.AddIfNecessary();

                AppendDocumentation(indentation, Data.Documentation.Process(process));

                if (process.ImplementAsProperty)
                {
                    ComposeProperty(indentation, process);

                    continue;
                }

                ComposeMethod(indentation, process);
            }
        }

        private void ComposeProperty(Indentation indentation, IProcessedQuantity process)
        {
            Builder.AppendLine($"{indentation}public {GetPotentialStaticKeyword(process)}{GetResultingType(process).FullyQualifiedName} {process.Name} => {process.Expression};");
        }

        private void ComposeMethod(Indentation indentation, IProcessedQuantity process)
        {
            var methodNameAndModifiers = $"public {GetPotentialStaticKeyword(process)}{GetResultingType(process).FullyQualifiedName} {process.Name}";

            (NamedType, string)[] parameters = new (NamedType, string)[process.ParameterTypes.Count];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = (process.ParameterTypes[i], process.ParameterNames[i]);
            }

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, process.Expression, parameters);
        }

        private static string GetPotentialStaticKeyword(IProcessedQuantity process)
        {
            if (process.ImplementStatically)
            {
                return "static ";
            }

            return string.Empty;
        }

        private NamedType GetResultingType(IProcessedQuantity process)
        {
            if (process.ResultsInCurrentType)
            {
                return Data.Vector.AsNamedType();
            }

            return process.Result!.Value;
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
