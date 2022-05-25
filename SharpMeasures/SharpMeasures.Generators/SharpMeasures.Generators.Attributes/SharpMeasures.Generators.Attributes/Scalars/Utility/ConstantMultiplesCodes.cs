namespace SharpMeasures.Generators.Scalars.Utility;

/// <summary>Contains common notation for producing the name of the property used to retrieve the magnitude of the scalar in multiples of a given constant.</summary>
public static class ConstantMultiplesCodes
{
    /// <summary>Prepends "In" to the name of the constant.</summary>
    /// <remarks>For example; the name "</remarks>
    public const string InConstant = "In[*]";

    /// <summary>Prepends "In" and appends "Multiples" to the name of the constant.</summary>
    /// <remarks>For example; the name "StandardGravity" results in "InStandardGravityMultiples".</remarks>
    public const string InConstantMultiplies = "In[*]Multiples";

    /// <summary>Prepends "InMultiplesOf" to the name of the constant.</summary>
    /// <remarks>For example; the name "StandardGravity" results in "InMultiplesOfStandardGravity".</remarks>
    public const string InMultiplesOfConstant = "InMultiplesOf[*]";

    /// <summary>Prepends "In" and appends "s" to the name of the constant.</summary>
    /// <remarks>For example; the name "PlanckLength" results in "InPlanckLengths".</remarks>
    public const string InConstants = "In[*]s";

    /// <summary>Prepends "In" and appends "es" to the name of the constant.</summary>
    /// <remarks>For example; the name "PlanckMass" results in "InPlanckMasses".</remarks>
    public const string InConstantes = "In[*]es";
}
