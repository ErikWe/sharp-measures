﻿namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Operations;

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
            StaticBuilding.AppendHeaderAndDirectives(Builder);

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
                if (operation.Implementation is QuantityOperationImplementation.OperatorAndMethod or QuantityOperationImplementation.Method)
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
                if (operation.Implementation is QuantityOperationImplementation.OperatorAndMethod or QuantityOperationImplementation.Operator)
                {
                    AppendOperatorOperation(indentation, operation);
                }
            }
        }

        private void AppendMethodOperation(Indentation indentation, IQuantityOperation operation)
        {
            if (GetQuantityCombination(operation) is not (NamedType result, NamedType other))
            {
                return;
            }

            var operatorSymbol = GetOperatorSymbol(operation.OperatorType);
            var parameterName = SourceBuildingUtility.ToParameterName(other.Name);

            if (ImplementedMethods.Add((operation.MethodName, other)))
            {
                SeparationHandler.AddIfNecessary();

                var methodNameAndModifiers = $"public {result.FullyQualifiedName} {operation.MethodName}";

                AppendDocumentation(indentation, Data.Documentation.OperationMethod(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, fullExpression(), new[] { (other, parameterName) });
            }

            if (operation.MirrorMethod && operation.OperatorType is OperatorType.Subtraction or OperatorType.Division && ImplementedMethods.Add((operation.MirroredMethodName, other)))
            {
                SeparationHandler.AddIfNecessary();

                var mirroredMethodNameAndModifiers = $"public {result.FullyQualifiedName} {operation.MirroredMethodName}";

                AppendDocumentation(indentation, Data.Documentation.MirroredOperationMethod(operation, other));
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
            if (GetVectorCombination(operation) is not (NamedType result, NamedType other))
            {
                return;
            }

            var parameterName = SourceBuildingUtility.ToParameterName(other.Name);

            if (ImplementedMethods.Add((operation.Name, other)))
            {
                SeparationHandler.AddIfNecessary();

                var methodNameAndModifiers = $"public {result.FullyQualifiedName} {operation.Name}";

                AppendDocumentation(indentation, Data.Documentation.VectorOperationMethod(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, fullExpression(), new[] { (other, parameterName) });
            }

            if (operation.Mirror && operation.OperatorType is VectorOperatorType.Cross && ImplementedMethods.Add((operation.MirroredName, other)))
            {
                SeparationHandler.AddIfNecessary();

                var mirroredMethodNameAndModifiers = $"public {result.FullyQualifiedName} {operation.MirroredName}";

                AppendDocumentation(indentation, Data.Documentation.MirroredVectorOperationMethod(operation, other));
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
            if (GetQuantityCombination(operation) is not (NamedType result, NamedType other))
            {
                return;
            }

            var operatorSymbol = GetOperatorSymbol(operation.OperatorType);

            var methodNameAndModifiers = $"public static {result.FullyQualifiedName} operator {operatorSymbol}";

            if (operation.Position is OperatorPosition.Left || operation.Mirror && ImplementedOperators.Add((Data.Vector.AsNamedType(), operation.OperatorType, other)))
            {
                SeparationHandler.AddIfNecessary();

                var expression = operation.Position is OperatorPosition.Left ? fullExpression() : mirroredExpression();
                var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (other, "b") };

                AppendDocumentation(indentation, Data.Documentation.OperationOperator(operation, other));
                StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
            }

            if (operation.Position is OperatorPosition.Right || operation.Mirror && ImplementedOperators.Add((other, operation.OperatorType, Data.Vector.AsNamedType())))
            {
                SeparationHandler.AddIfNecessary();

                var expression = operation.Position is OperatorPosition.Right ? fullExpression() : mirroredExpression();
                var parameters = new[] { (other, "a"), (Data.Vector.AsNamedType(), "b") };

                AppendDocumentation(indentation, Data.Documentation.MirroredOperationOperator(operation, other));
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
                if (other.FullyQualifiedName is "global::SharpMeasures.Scalar")
                {
                    return $"{parameterName}";
                }

                return $"{parameterName}.Magnitude";
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

        private (NamedType Result, NamedType Other)? GetQuantityCombination(IQuantityOperation operation)
        {
            if (Data.VectorPopulation.Vectors.ContainsKey(operation.Result))
            {
                return (operation.Result, operation.Other);
            }

            if (Data.VectorPopulation.Groups.TryGetValue(operation.Result, out var resultGroup))
            {
                if (resultGroup.MembersByDimension.TryGetValue(Data.Dimension, out var resultMember))
                {
                    return (resultMember, operation.Other);
                }
            }

            return null;
        }

        private (NamedType Result, NamedType Other)? GetVectorCombination(IVectorOperation operation)
        {
            if (Data.ScalarPopulation.Scalars.ContainsKey(operation.Result))
            {
                if (Data.VectorPopulation.Vectors.ContainsKey(operation.Other))
                {
                    return (operation.Result, operation.Other);
                }

                if (Data.VectorPopulation.Groups.TryGetValue(operation.Other, out var otherGroup))
                {
                    if (otherGroup.MembersByDimension.TryGetValue(Data.Dimension, out var otherMember))
                    {
                        return (operation.Result, otherMember);
                    }
                }

                return null;
            }

            if (Data.VectorPopulation.Vectors.ContainsKey(operation.Result))
            {
                if (Data.VectorPopulation.Vectors.ContainsKey(operation.Other))
                {
                    return (operation.Result, operation.Other);
                }

                if (Data.VectorPopulation.Groups.TryGetValue(operation.Other, out var otherGroup))
                {
                    if (otherGroup.MembersByDimension.TryGetValue(Data.Dimension, out var otherMember))
                    {
                        return (operation.Result, otherMember);
                    }
                }
            }

            if (Data.VectorPopulation.Groups.TryGetValue(operation.Result, out var resultGroup))
            {
                if (resultGroup.MembersByDimension.TryGetValue(Data.Dimension, out var resultMember) is false)
                {
                    return null;
                }

                if (Data.VectorPopulation.Vectors.ContainsKey(operation.Other))
                {
                    return (resultMember, operation.Other);
                }

                if (Data.VectorPopulation.Groups.TryGetValue(operation.Other, out var otherGroup))
                {
                    if (otherGroup.MembersByDimension.TryGetValue(Data.Dimension, out var otherMember))
                    {
                        return (resultMember, otherMember);
                    }
                }
            }

            return null;
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
