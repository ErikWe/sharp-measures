namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using System;
using System.Collections.Generic;

public class OperatorDerivationSearcher
{
    public static IReadOnlyList<IOperatorDerivation> GetDerivations(NamedType quantity, IDerivedQuantity derivation)
    {
        var searcher = new OperatorDerivationSearcher(quantity, derivation);

        return searcher.GetDerivations();
    }

    private NamedType Quantity { get; }

    private IDerivedQuantity Derivation { get; }

    private OperatorType OriginalOperatorType { get; }
    private OperatorType OppositeOperatorType { get; }

    private OperatorDerivationSearcher(NamedType quantity, IDerivedQuantity derivation)
    {
        Quantity = quantity;
        Derivation = derivation;

        if (derivation.OperatorImplementation is DerivationOperatorImplementation.None)
        {
            return;
        }

        OriginalOperatorType = GetOperatorType(derivation.Expression);

        if (Derivation.OperatorImplementation is DerivationOperatorImplementation.Exact)
        {
            return;
        }

        OppositeOperatorType = GetOppositeOperatorType(OriginalOperatorType);
    }

    public IReadOnlyList<IOperatorDerivation> GetDerivations()
    {
        if (Derivation.OperatorImplementation is DerivationOperatorImplementation.None)
        {
            return Array.Empty<IOperatorDerivation>();
        }

        bool orderedExpression = Derivation.Expression.IndexOf("0", StringComparison.InvariantCulture) < Derivation.Expression.IndexOf("1", StringComparison.InvariantCulture);

        List<IOperatorDerivation> derivations = new(Derivation.OperatorImplementation is DerivationOperatorImplementation.Exact ? 1 : 4);

        if (orderedExpression)
        {
            derivations.Add(new OperatorDerivation(Quantity, OriginalOperatorType, Derivation.Signature[0], Derivation.Signature[1]));
        }
        else
        {
            derivations.Add(new OperatorDerivation(Quantity, OriginalOperatorType, Derivation.Signature[1], Derivation.Signature[0]));
        }

        if (Derivation.OperatorImplementation is DerivationOperatorImplementation.Exact)
        {
            return derivations;
        }

        if (OriginalOperatorType is OperatorType.Addition or OperatorType.Multiplication)
        {
            if (orderedExpression)
            {
                derivations.Add(new OperatorDerivation(Quantity, OriginalOperatorType, Derivation.Signature[1], Derivation.Signature[0]));
                derivations.Add(new OperatorDerivation(Derivation.Signature[0], OppositeOperatorType, Quantity, Derivation.Signature[1]));
                derivations.Add(new OperatorDerivation(Derivation.Signature[1], OppositeOperatorType, Quantity, Derivation.Signature[0]));
            }
            else
            {
                derivations.Add(new OperatorDerivation(Quantity, OriginalOperatorType, Derivation.Signature[0], Derivation.Signature[1]));
                derivations.Add(new OperatorDerivation(Derivation.Signature[1], OppositeOperatorType, Quantity, Derivation.Signature[0]));
                derivations.Add(new OperatorDerivation(Derivation.Signature[0], OppositeOperatorType, Quantity, Derivation.Signature[1]));
            }
        }

        if (OriginalOperatorType is OperatorType.Subtraction or OperatorType.Division)
        {
            if (orderedExpression)
            {
                derivations.Add(new OperatorDerivation(Derivation.Signature[0], OppositeOperatorType, Quantity, Derivation.Signature[1]));
                derivations.Add(new OperatorDerivation(Derivation.Signature[0], OppositeOperatorType, Derivation.Signature[1], Quantity));
                derivations.Add(new OperatorDerivation(Derivation.Signature[1], OriginalOperatorType, Derivation.Signature[0], Quantity));
            }
            else
            {
                derivations.Add(new OperatorDerivation(Derivation.Signature[1], OppositeOperatorType, Quantity, Derivation.Signature[0]));
                derivations.Add(new OperatorDerivation(Derivation.Signature[1], OppositeOperatorType, Derivation.Signature[0], Quantity));
                derivations.Add(new OperatorDerivation(Derivation.Signature[0], OriginalOperatorType, Derivation.Signature[1], Quantity));
            }
        }

        return derivations;
    }

    private static OperatorType GetOperatorType(string expression)
    {
        if (expression.Contains("+"))
        {
            return OperatorType.Addition;
        }

        if (expression.Contains("-"))
        {
            return OperatorType.Subtraction;
        }

        if (expression.Contains("*"))
        {
            return OperatorType.Multiplication;
        }

        return OperatorType.Division;
    }

    private static OperatorType GetOppositeOperatorType(OperatorType originalOperatorType)
    {
        return originalOperatorType switch
        {
            OperatorType.Addition => OperatorType.Subtraction,
            OperatorType.Subtraction => OperatorType.Addition,
            OperatorType.Multiplication => OperatorType.Division,
            OperatorType.Division => OperatorType.Multiplication,
            _ => throw new NotSupportedException($"Unrecognized {typeof(OperatorType).Name}: {originalOperatorType}")
        };
    }
}
