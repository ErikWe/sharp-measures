namespace Microsoft.CodeAnalysis;

using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static partial class Extensions
{
    public static AttributeSyntax? GetSyntax(this AttributeData attributeData)
        => attributeData.ApplicationSyntaxReference?.GetSyntax() as AttributeSyntax;

    public static AttributeArgumentSyntax? GetArgumentSyntax(this AttributeData attributeData, int index)
    {
        if (index < 0 || attributeData.GetSyntax()?.ArgumentList is not AttributeArgumentListSyntax argumentList || index >= argumentList.Arguments.Count)
        {
            return null;
        }

        return argumentList.Arguments[index];
    }
}
