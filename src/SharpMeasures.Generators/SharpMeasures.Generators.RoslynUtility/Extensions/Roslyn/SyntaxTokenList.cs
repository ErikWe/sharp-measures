namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public static partial class Extensions
{
    public static bool ContainsTokenOfKind(this SyntaxTokenList tokenList, SyntaxKind token)
    {
        foreach (var candidateToken in tokenList)
        {
            if (candidateToken.IsKind(token))
            {
                return true;
            }
        }

        return false;
    }
}
