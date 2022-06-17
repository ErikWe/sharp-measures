namespace SharpMeasures.Generators.Attributes.Parsing;

using SharpMeasures.Generators.Diagnostics;

public abstract class AProcesser<TContext, TDefinition, TProduct> : IProcesser<TContext, TDefinition, TProduct>
    where TContext : IProcessingContext
{
    public abstract IOptionalWithDiagnostics<TProduct> Process(TContext context, TDefinition definition);
}

public abstract class AActionableProcesser<TContext, TDefinition, TProduct> : AProcesser<TContext, TDefinition, TProduct>,
    IActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IProcessingContext
{
    public virtual void OnStartProcessing(TContext context, TDefinition definition) { }
    public virtual void OnSuccessfulProcess(TContext context, TDefinition definition, TProduct product) { }
    public virtual void OnUnsuccessfulProcess(TContext context, TDefinition definition) { }
}

public abstract class AReprocesser<TContext, TDefinition, TProduct> : IReprocesser<TContext, TDefinition, TProduct>
    where TContext : IProcessingContext
{
    public abstract IOptionalWithDiagnostics<TProduct> Reprocess(TContext context, TDefinition definition, TProduct product);
}

public abstract class AActionableReprocesser<TContext, TDefinition, TProduct> : AReprocesser<TContext, TDefinition, TProduct>,
    IActionableReprocesser<TContext, TDefinition, TProduct>
    where TContext : IProcessingContext
{
    public virtual void OnStartReprocessing(TContext context, TDefinition definition, TProduct product) { }
    public virtual void OnSuccessfulReprocess(TContext context, TDefinition definition, TProduct product) { }
    public virtual void OnUnsuccessfulReprocess(TContext context, TDefinition definition, TProduct product) { }
}