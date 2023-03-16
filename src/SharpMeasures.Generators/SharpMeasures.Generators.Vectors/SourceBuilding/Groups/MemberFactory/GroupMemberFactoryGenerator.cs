namespace SharpMeasures.Generators.Vectors.SourceBuilding.Groups.MemberFactory;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Threading;

internal static class GroupMemberFactoryGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<GroupDataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<GroupDataModel> model, CancellationToken _)
    {
        if (model.HasValue is false)
        {
            return new Optional<DataModel>();
        }

        var unitParameterName = SourceBuildingUtility.ToParameterName(model.Value.Group.Unit.Name);

        return new DataModel(model.Value.Group.Type, model.Value.Group.Unit, unitParameterName, model.Value.Group.Scalar, model.Value.Group.MembersByDimension, model.Value.SourceBuildingContext);
    }
}
