namespace SharpMeasures.Generators.Attributes.Parsing;

using SharpMeasures.Generators.Diagnostics;

public interface IProcessingContext
{
    public abstract DefinedType Type { get; }
}

public interface IProcesser<in TContext, in TDefinition, TProduct>
    where TContext : IProcessingContext
{
    public abstract IOptionalWithDiagnostics<TProduct> Process(TContext context, TDefinition definition);
}

public interface IActionableProcesser<in TContext, in TDefinition, TProduct> : IProcesser<TContext, TDefinition, TProduct>
    where TContext : IProcessingContext
{
    public abstract void OnStartProcessing(TContext context, TDefinition definition);
    public abstract void OnSuccessfulProcess(TContext context, TDefinition definition, TProduct product);
    public abstract void OnUnsuccessfulProcess(TContext context, TDefinition definition);
}

public interface IReprocesser<in TContext, in TDefinition, TProduct>
    where TContext : IProcessingContext
{
    public abstract IOptionalWithDiagnostics<TProduct> Reprocess(TContext context, TDefinition definition, TProduct product);
}

public interface IActionableReprocesser<in TContext, in TDefinition, TProduct> : IReprocesser<TContext, TDefinition, TProduct>
    where TContext : IProcessingContext
{
    public abstract void OnStartReprocessing(TContext context, TDefinition definition, TProduct product);
    public abstract void OnSuccessfulReprocess(TContext context, TDefinition definition, TProduct product);
    public abstract void OnUnsuccessfulReprocess(TContext context, TDefinition definition, TProduct product);
}