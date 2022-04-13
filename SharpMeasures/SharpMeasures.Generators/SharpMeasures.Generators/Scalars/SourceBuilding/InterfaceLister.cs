namespace SharpMeasures.Generators.Scalars.SourceBuilding;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Scalars.Pipeline;

using System;
using System.Collections.Generic;

internal static class InterfaceLister
{
    public static IEnumerable<string> GetAll(INamedTypeSymbol typeSymbol, PowerOperations powerOperations)
    {
        yield return $"System.IComparable<{typeSymbol.Name}>";
        yield return $"SharpMeasures.ScalarAbstractions.IMultiplicableScalarQuantity<{typeSymbol.Name}, Scalar>";
        yield return $"SharpMeasures.ScalarAbstractions.IMultiplicableScalarQuantity<Unhandled, Unhandled>";
        yield return $"SharpMeasures.ScalarAbstractions.IDivisibleScalarQuantity<{typeSymbol.Name}, Scalar>";
        yield return $"SharpMeasures.ScalarAbstractions.IDivisibleScalarQuantity<Unhandled, Unhandled>";
        yield return "SharpMeasures.ScalarAbstractions.IGenericallyMultiplicableScalarQuantity";
        yield return "SharpMeasures.ScalarAbstractions.IGenericallyDivisibleScalarQuantity";

        foreach (string invertible in GetInvertibleInterfaces(powerOperations))
        {
            yield return invertible;
        }

        foreach (string squarable in GetSquarableInterfaces(powerOperations))
        {
            yield return squarable;
        }

        foreach (string cubable in GetCubableInterfaces(powerOperations))
        {
            yield return cubable;
        }

        foreach (string squareRootable in GetSquareRootableInterfaces(powerOperations))
        {
            yield return squareRootable;
        }

        foreach (string cubeRootable in GetCubeRootableInterfaces(powerOperations))
        {
            yield return cubeRootable;
        }
    }

    private static IEnumerable<string> GetInvertibleInterfaces(PowerOperations powerOperations)
    {
        if (powerOperations.Invertible is InvertibleQuantityAttributeParameters invertibleParameters)
        {
            if (invertibleParameters.Quantity is Type primaryQuantity)
            {
                yield return text(primaryQuantity.FullName);
            }

            foreach (Type secondaryQuantity in invertibleParameters.SecondaryQuantities)
            {
                yield return text(secondaryQuantity.FullName);
            }
        }

        static string text(string type) => $"SharpMeasures.ScalarAbstractions.IInvertibleScalarQuantity<{type}>";
    }

    private static IEnumerable<string> GetSquarableInterfaces(PowerOperations powerOperations)
    {
        if (powerOperations.Squarable is SquarableQuantityAttributeParameters squarableParameters)
        {
            if (squarableParameters.Quantity is Type primaryQuantity)
            {
                yield return text(primaryQuantity.FullName);
            }

            foreach (Type secondaryQuantity in squarableParameters.SecondaryQuantities)
            {
                yield return text(secondaryQuantity.FullName);
            }
        }

        static string text(string type) => $"SharpMeasures.ScalarAbstractions.ISquarableScalarQuantity<{type}>";
    }

    private static IEnumerable<string> GetCubableInterfaces(PowerOperations powerOperations)
    {
        if (powerOperations.Cubable is CubableQuantityAttributeParameters cubableParameters)
        {
            if (cubableParameters.Quantity is Type primaryQuantity)
            {
                yield return text(primaryQuantity.FullName);
            }

            foreach (Type secondaryQuantity in cubableParameters.SecondaryQuantities)
            {
                yield return text(secondaryQuantity.FullName);
            }
        }

        static string text(string type) => $"SharpMeasures.ScalarAbstractions.ICubableScalarQuantity<{type}>";
    }

    private static IEnumerable<string> GetSquareRootableInterfaces(PowerOperations powerOperations)
    {
        if (powerOperations.SquareRootable is SquareRootableQuantityAttributeParameters squareRootableParameters)
        {
            if (squareRootableParameters.Quantity is Type primaryQuantity)
            {
                yield return text(primaryQuantity.FullName);
            }

            foreach (Type secondaryQuantity in squareRootableParameters.SecondaryQuantities)
            {
                yield return text(secondaryQuantity.FullName);
            }
        }

        static string text(string type) => $"SharpMeasures.ScalarAbstractions.ISquareRootableScalarQuantity<{type}>";
    }

    private static IEnumerable<string> GetCubeRootableInterfaces(PowerOperations powerOperations)
    {
        if (powerOperations.CubeRootable is CubeRootableQuantityAttributeParameters cubeRootableParameters)
        {
            if (cubeRootableParameters.Quantity is Type primaryQuantity)
            {
                yield return text(primaryQuantity.FullName);
            }

            foreach (Type secondaryQuantity in cubeRootableParameters.SecondaryQuantities)
            {
                yield return text(secondaryQuantity.FullName);
            }
        }

        static string text(string type) => $"SharpMeasures.ScalarAbstractions.ICubeRootableScalarQuantity<{type}>";
    }
}
