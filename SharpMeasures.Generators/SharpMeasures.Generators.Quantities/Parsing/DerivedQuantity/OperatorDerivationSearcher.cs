namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using System;
using System.Collections.Generic;

public class OperatorDerivationSearcher
{
    public static IReadOnlyList<OperatorDerivation> GetDerivations(NamedType quantity, IDerivedQuantity derivation)
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
        OppositeOperatorType = GetOppositeOperatorType(OriginalOperatorType);
    }

    private IReadOnlyList<OperatorDerivation> GetDerivations()
    {
        if (Derivation.OperatorImplementation is DerivationOperatorImplementation.None)
        {
            return Array.Empty<OperatorDerivation>();
        }

        if (Derivation.Signature.Count is 1)
        {
            return GetSingleQuantityDerivations();
        }

        return GetDoubleQuantityDerivations();
    }

    private IReadOnlyList<OperatorDerivation> GetSingleQuantityDerivations()
    {
        List<OperatorDerivation> derivations = new(3);

        NamedType pureScalar = new("Scalar", "SharpMeasures", "SharpMeasures.Base", true);

        if (Derivation.OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.LeftHandSide or DerivationOperatorImplementation.Suitable)
        {
            derivations.Add(new OperatorDerivation(pureScalar, OperatorType.Multiplication, Quantity, Derivation.Signature[0]));
        }

        if (Derivation.OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.RightHandSide
            || Derivation.OperatorImplementation is DerivationOperatorImplementation.Suitable && Derivation.Signature[0].Assembly != Quantity.Assembly)
        {
            derivations.Add(new OperatorDerivation(pureScalar, OperatorType.Multiplication, Derivation.Signature[0], Quantity));
        }

        derivations.Add(new OperatorDerivation(Derivation.Signature[0], OperatorType.Division, pureScalar, Quantity));

        return derivations;
    }

    private IReadOnlyList<OperatorDerivation> GetDoubleQuantityDerivations()
    {
        bool orderedExpression = Derivation.Expression.IndexOf("0", StringComparison.InvariantCulture) < Derivation.Expression.IndexOf("1", StringComparison.InvariantCulture);

        List<OperatorDerivation> derivations = new(3);

        if (OriginalOperatorType is OperatorType.Addition or OperatorType.Multiplication)
        {
            if (Derivation.OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.LeftHandSide or DerivationOperatorImplementation.Suitable)
            {
                derivations.Add(new OperatorDerivation(Derivation.Signature[0], OppositeOperatorType, Quantity, Derivation.Signature[1]));
            }

            if (Derivation.OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.RightHandSide
                || Derivation.OperatorImplementation is DerivationOperatorImplementation.Suitable && Derivation.Signature[0].Assembly != Quantity.Assembly)
            {
                derivations.Add(new OperatorDerivation(Derivation.Signature[1], OppositeOperatorType, Quantity, Derivation.Signature[0]));
            }
        }

        if (OriginalOperatorType is OperatorType.Subtraction or OperatorType.Division)
        {
            if (orderedExpression)
            {
                if (Derivation.OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.LeftHandSide or DerivationOperatorImplementation.Suitable)
                {
                    derivations.Add(new OperatorDerivation(Derivation.Signature[0], OppositeOperatorType, Quantity, Derivation.Signature[1]));
                }

                if (Derivation.OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.RightHandSide
                    || Derivation.OperatorImplementation is DerivationOperatorImplementation.Suitable && Derivation.Signature[1].Assembly != Quantity.Assembly)
                {
                    derivations.Add(new OperatorDerivation(Derivation.Signature[0], OppositeOperatorType, Derivation.Signature[1], Quantity));
                }

                if (Derivation.OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.RightHandSide
                    || Derivation.OperatorImplementation is DerivationOperatorImplementation.Suitable && Derivation.Signature[0].Assembly != Quantity.Assembly)
                {
                    derivations.Add(new OperatorDerivation(Derivation.Signature[1], OriginalOperatorType, Derivation.Signature[0], Quantity));
                }
            }
            else
            {
                if (Derivation.OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.LeftHandSide or DerivationOperatorImplementation.Suitable)
                {
                    derivations.Add(new OperatorDerivation(Derivation.Signature[1], OppositeOperatorType, Quantity, Derivation.Signature[0]));
                }

                if (Derivation.OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.RightHandSide
                    || Derivation.OperatorImplementation is DerivationOperatorImplementation.Suitable && Derivation.Signature[0].Assembly != Quantity.Assembly)
                {
                    derivations.Add(new OperatorDerivation(Derivation.Signature[1], OppositeOperatorType, Derivation.Signature[0], Quantity));
                }

                if (Derivation.OperatorImplementation is DerivationOperatorImplementation.All or DerivationOperatorImplementation.RightHandSide
                    || Derivation.OperatorImplementation is DerivationOperatorImplementation.Suitable && Derivation.Signature[1].Assembly != Quantity.Assembly)
                {
                    derivations.Add(new OperatorDerivation(Derivation.Signature[0], OriginalOperatorType, Derivation.Signature[1], Quantity));
                }
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
