namespace SharpMeasures.Generators.Scalars.SourceBuilding;

using SharpMeasures.Generators.Scalars.Pipeline;

using System;
using System.Collections.Generic;

internal static class InterfaceLister
{
    public static IEnumerable<string> GetAll(FifthStage.Result data)
    {
        yield return $"System.IComparable<{data.TypeDefinition.Name}>";
        yield return $"SharpMeasures.ScalarAbstractions.IMultiplicableScalarQuantity<{data.TypeDefinition.Name}, SharpMeasures.Scalar>";
        yield return $"SharpMeasures.ScalarAbstractions.IMultiplicableScalarQuantity<SharpMeasures.Unhandled, SharpMeasures.Unhandled>";
        yield return $"SharpMeasures.ScalarAbstractions.IDivisibleScalarQuantity<{data.TypeDefinition.Name}, SharpMeasures.Scalar>";
        yield return $"SharpMeasures.ScalarAbstractions.IDivisibleScalarQuantity<SharpMeasures.Unhandled, SharpMeasures.Unhandled>";
        yield return "SharpMeasures.ScalarAbstractions.IGenericallyMultiplicableScalarQuantity";
        yield return "SharpMeasures.ScalarAbstractions.IGenericallyDivisibleScalarQuantity";

        foreach (string invertible in GetInvertibleInterfaces(data.InvertibleOperations))
        {
            yield return invertible;
        }

        foreach (string squarable in GetSquarableInterfaces(data.SquarableOperations))
        {
            yield return squarable;
        }

        foreach (string cubable in GetCubableInterfaces(data.CubableOperations))
        {
            yield return cubable;
        }

        foreach (string squareRootable in GetSquareRootableInterfaces(data.SquareRootableOperations))
        {
            yield return squareRootable;
        }

        foreach (string cubeRootable in GetCubeRootableInterfaces(data.CubeRootableOperations))
        {
            yield return cubeRootable;
        }
    }

    public static IEnumerable<string> GetInterfaces(FifthStage.PowerData? operations, Func<string, string> toInterfaceDelegate)
    {
        if (operations is null)
        {
            yield break;
        }

        yield return toInterfaceDelegate(operations.Value.Quantity);

        foreach (string secondaryQuantity in operations.Value.SecondaryQuantities)
        {
            yield return toInterfaceDelegate(secondaryQuantity);
        }
    }

    private static IEnumerable<string> GetInvertibleInterfaces(FifthStage.PowerData? operations)
    {
        return GetInterfaces(operations, text);

        static string text(string type) => $"SharpMeasures.ScalarAbstractions.IInvertibleScalarQuantity<{type}>";
    }

    private static IEnumerable<string> GetSquarableInterfaces(FifthStage.PowerData? operations)
    {
        return GetInterfaces(operations, text);

        static string text(string type) => $"SharpMeasures.ScalarAbstractions.ISquarableScalarQuantity<{type}>";
    }

    private static IEnumerable<string> GetCubableInterfaces(FifthStage.PowerData? operations)
    {
        return GetInterfaces(operations, text);

        static string text(string type) => $"SharpMeasures.ScalarAbstractions.ICubableScalarQuantity<{type}>";
    }

    private static IEnumerable<string> GetSquareRootableInterfaces(FifthStage.PowerData? operations)
    {
        return GetInterfaces(operations, text);

        static string text(string type) => $"SharpMeasures.ScalarAbstractions.ISquareRootableScalarQuantity<{type}>";
    }

    private static IEnumerable<string> GetCubeRootableInterfaces(FifthStage.PowerData? operations)
    {
        return GetInterfaces(operations, text);

        static string text(string type) => $"SharpMeasures.ScalarAbstractions.ICubeRootableScalarQuantity<{type}>";
    }
}
