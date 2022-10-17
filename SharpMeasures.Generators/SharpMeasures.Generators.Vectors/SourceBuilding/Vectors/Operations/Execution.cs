namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Operations;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.SourceBuilding;

using System;
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

        context.AddSource($"{data.Value.Vector.QualifiedName}.Operations.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private InterfaceCollector InterfaceCollector { get; }

        private HashSet<(NamedType, OperatorType, NamedType)> ImplementedOperators { get; } = new();
        private HashSet<(string, NamedType)> ImplementedMethods { get; } = new();

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);

            InterfaceCollector = InterfaceCollector.Delayed(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContentLevel);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            Builder.Append(Data.Vector.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
        }

        private string Retrieve()
        {
            if (ImplementedMethods.Count is 0)
            {
                return string.Empty;
            }

            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            foreach (var operation in Data.Operations)
            {
                if (operation.Implementation.HasFlag(QuantityOperationImplementation.Method))
                {
                    AppendMethodOperation(indentation, operation);
                }
            }

            foreach (var operation in Data.VectorOperations)
            {
                AppendMethodVectorOperation(indentation, operation);
            }

            foreach (var operation in Data.Operations)
            {
                if (operation.Implementation.HasFlag(QuantityOperationImplementation.Operator))
                {
                    AppendOperatorOperation(indentation, operation);
                }
            }
        }

        private void AppendMethodOperation(Indentation indentation, IQuantityOperation operation)
        {
            if (GetQuantityCombination(operation.Result, operation.Other) is not (NamedType result, NamedType other))
            {
                return;
            }

            var operatorSymbol = GetOperatorSymbol(operation.OperatorType);
            var parameterName = SourceBuildingUtility.ToParameterName(other.Name);

            if (ImplementedMethods.Add((operation.MethodName, other)))
            {
                SeparationHandler.AddIfNecessary();

                var methodNameAndModifiers = $"public {result.FullyQualifiedName} {operation.MethodName}";

                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.OperationMethod(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, fullExpression(), new[] { (other, parameterName) });
            }

            if ((operation.MirrorMethod && operation.OperatorType is OperatorType.Subtraction or OperatorType.Division) && ImplementedMethods.Add((operation.MirroredMethodName, other)))
            {
                SeparationHandler.AddIfNecessary();

                var mirroredMethodNameAndModifiers = $"public {result.FullyQualifiedName} {operation.MirroredMethodName}";

                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MirroredOperationMethod(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, mirroredMethodNameAndModifiers, mirroredExpression(), new[] { (other, parameterName) });
            }

            string fullExpression()
            {
                if (operation.Position is OperatorPosition.Left)
                {
                    return $"new(Components {operatorSymbol} {otherExpression()})";
                }

                return $"new({otherExpression()} {operatorSymbol} Components)";
            }

            string mirroredExpression()
            {
                if (operation.Position is OperatorPosition.Right)
                {
                    return $"new(Components {operatorSymbol} {otherExpression()})";
                }

                return $"new({otherExpression()} {operatorSymbol} Components)";
            }

            string otherExpression()
            {
                if (other.FullyQualifiedName is "global::SharpMeasures.Scalar" || other.FullyQualifiedName.StartsWith("global::SharpMeasures.Vector", StringComparison.InvariantCulture))
                {
                    return $"{parameterName}";
                }

                if (Data.ScalarPopulation.Scalars.ContainsKey(other))
                {
                    return $"{parameterName}.Magnitude";
                }

                return $"{parameterName}.Components";
            }
        }

        private void AppendMethodVectorOperation(Indentation indentation, IVectorOperation operation)
        {
            if (operation.OperatorType is VectorOperatorType.Cross && Data.Dimension is not 3)
            {
                return;
            }

            if (GetQuantityCombination(operation.Result, operation.Other) is not (NamedType result, NamedType other))
            {
                return;
            }

            var parameterName = SourceBuildingUtility.ToParameterName(other.Name);

            if (ImplementedMethods.Add((operation.Name, other)))
            {
                SeparationHandler.AddIfNecessary();

                var methodNameAndModifiers = $"public {result.FullyQualifiedName} {operation.Name}";

                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.VectorOperationMethod(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, fullExpression(), new[] { (other, parameterName) });
            }

            if ((operation.Mirror && operation.OperatorType is VectorOperatorType.Cross) && ImplementedMethods.Add((operation.MirroredName, other)))
            {
                SeparationHandler.AddIfNecessary();

                var mirroredMethodNameAndModifiers = $"public {result.FullyQualifiedName} {operation.MirroredName}";

                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MirroredVectorOperationMethod(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, mirroredMethodNameAndModifiers, mirroredExpression(), new[] { (other, parameterName) });
            }

            string fullExpression()
            {
                if (operation.OperatorType is VectorOperatorType.Dot)
                {
                    return $"new(Components.Dot({otherExpression()}))";
                }

                if (operation.Position is OperatorPosition.Left)
                {
                    return $"new(Components.Cross({otherExpression()}))";
                }

                return $"new({otherExpression()}.Cross(Components))";
            }

            string mirroredExpression()
            {
                if (operation.OperatorType is VectorOperatorType.Dot)
                {
                    return $"new(Components.Dot({otherExpression()}))";
                }

                if (operation.Position is OperatorPosition.Right)
                {
                    return $"new(Components.Cross({otherExpression()}))";
                }

                return $"new({otherExpression()}.Cross(Components))";
            }

            string otherExpression()
            {
                if (other.FullyQualifiedName is "global::SharpMeasures.Scalar" || other.FullyQualifiedName.StartsWith("global::SharpMeasures.Vector", StringComparison.InvariantCulture))
                {
                    return $"{parameterName}";
                }

                if (Data.ScalarPopulation.Scalars.ContainsKey(other))
                {
                    return $"{parameterName}.Magnitude";
                }

                return $"{parameterName}.Components";
            }
        }

        private void AppendOperatorOperation(Indentation indentation, IQuantityOperation operation)
        {
            if (GetQuantityCombination(operation.Result, operation.Other) is not (NamedType result, NamedType other))
            {
                return;
            }

            var operatorSymbol = GetOperatorSymbol(operation.OperatorType);

            var methodNameAndModifiers = $"public static {result.FullyQualifiedName} operator {operatorSymbol}";

            if ((operation.Position is OperatorPosition.Left || operation.Mirror) && ImplementedOperators.Add((Data.Vector.AsNamedType(), operation.OperatorType, other)))
            {
                SeparationHandler.AddIfNecessary();

                var expression = operation.Position is OperatorPosition.Left ? fullExpression() : mirroredExpression();
                var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (other, "b") };

                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.OperationOperator(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
            }

            if ((operation.Position is OperatorPosition.Right || operation.Mirror) && ImplementedOperators.Add((other, operation.OperatorType, Data.Vector.AsNamedType())))
            {
                SeparationHandler.AddIfNecessary();

                var expression = operation.Position is OperatorPosition.Right ? fullExpression() : mirroredExpression();
                var parameters = new[] { (other, "a"), (Data.Vector.AsNamedType(), "b") };

                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MirroredOperationOperator(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
            }

            string fullExpression()
            {
                if (operation.Position is OperatorPosition.Left)
                {
                    return $"new(a.Components {operatorSymbol} {otherExpression("b")})";
                }

                return $"new({otherExpression("a")} {operatorSymbol} b.Components)";
            }

            string mirroredExpression()
            {
                if (operation.Position is OperatorPosition.Right)
                {
                    return $"new(a.Components {operatorSymbol} {otherExpression("b")})";
                }

                return $"new({otherExpression("a")} {operatorSymbol} b.Components)";
            }

            string otherExpression(string parameterName)
            {
                if (other.FullyQualifiedName is "global::SharpMeasures.Scalar" || other.FullyQualifiedName.StartsWith("global::SharpMeasures.Vector", StringComparison.InvariantCulture))
                {
                    return $"{parameterName}";
                }

                if (Data.ScalarPopulation.Scalars.ContainsKey(other))
                {
                    return $"{parameterName}.Magnitude";
                }

                return $"{parameterName}.Components";
            }
        }

        private static string GetOperatorSymbol(OperatorType operatorType) => operatorType switch
        {
            OperatorType.Addition => "+",
            OperatorType.Subtraction => "-",
            OperatorType.Multiplication => "*",
            OperatorType.Division => "/",
            _ => throw new NotSupportedException($"Invalid {typeof(OperatorType).Name}: {operatorType}")
        };

        private (NamedType Result, NamedType Other)? GetQuantityCombination(NamedType result, NamedType other)
        {
            if (Data.ScalarPopulation.Scalars.ContainsKey(result))
            {
                return (result, other);
            }

            if (Data.VectorPopulation.Vectors.ContainsKey(result))
            {
                if (Data.VectorPopulation.Vectors.ContainsKey(other))
                {
                    return (result, other);
                }

                if (Data.VectorPopulation.Groups.TryGetValue(other, out var otherGroup) && otherGroup.MembersByDimension.TryGetValue(Data.Dimension, out var otherMember))
                {
                    return (result, otherMember);
                }
            }

            if (Data.VectorPopulation.Groups.TryGetValue(result, out var resultGroup) && resultGroup.MembersByDimension.TryGetValue(Data.Dimension, out var resultMember))
            {
                if (Data.VectorPopulation.Vectors.ContainsKey(other))
                {
                    return (resultMember, other);
                }

                if (Data.VectorPopulation.Groups.TryGetValue(other, out var otherGroup) && otherGroup.MembersByDimension.TryGetValue(Data.Dimension, out var otherMember))
                {
                    return (resultMember, otherMember);
                }
            }

            return null;
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
