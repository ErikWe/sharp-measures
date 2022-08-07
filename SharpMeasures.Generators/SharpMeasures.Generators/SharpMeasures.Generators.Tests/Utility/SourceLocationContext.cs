namespace SharpMeasures.Generators.Tests.Utility;

internal record struct SourceSearchContext
{
    public string Target { get; }

    public string Prefix { get; }
    public string Postfix { get; }

    public SourceSearchContext(string target, string prefix = "", string postfix = "")
    {
        Target = target;

        Prefix = prefix;
        Postfix = postfix;
    }

    public SourceSearchContext(SourceSubtext subtext, string prefix = "", string postfix = "") : this(subtext.Target, $"{prefix}{subtext.Prefix}", $"{subtext.Postfix}{postfix}") { }

    public override string ToString() => $"{Prefix}{Target}{Postfix}";
}
