namespace SharpMeasures.Generators.Tests.Utility;

public record struct SourceSubtext
{
    public static SourceSubtext Typeof(string target, string prefix = "", string postfix = "") => new(target, prefix: $"{prefix}typeof(", postfix: $"){postfix}");

    public string Target { get; init; }

    public string Prefix { get; init; }
    public string Postfix { get; init; }

    public SourceSubtext(string target, string prefix = "", string postfix = "")
    {
        Target = target;
        Prefix = prefix;
        Postfix = postfix;
    }

    public override string ToString() => $"{Prefix}{Target}{Postfix}";
}
