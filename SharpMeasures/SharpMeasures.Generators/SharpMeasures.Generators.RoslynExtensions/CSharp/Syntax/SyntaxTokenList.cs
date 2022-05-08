namespace Microsoft.CodeAnalysis.CSharp.Syntax;

public static partial class Extensions
{
    public static bool ContainsTokenOfKind(this SyntaxTokenList tokenList, SyntaxKind token)
    {
        foreach (SyntaxToken candidateToken in tokenList)
        {
            if (candidateToken.IsKind(token))
            {
                return true;
            }
        }

        return false;
    }
}
