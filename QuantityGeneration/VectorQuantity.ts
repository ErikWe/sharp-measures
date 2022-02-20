let vectorQuantity : {
    type: "Vector",
    name: string,
    component: string,
    dimensionalities: number[],
    unit: string,
    unitBias: string | boolean,
    defaultUnit: string | undefined,
    includeUnits: string[] | undefined,
    excludeUnits: string[] | undefined,
    includeBases: string[] | undefined,
    excludeBases: string[] | undefined,
    includeConstants: string[] | undefined,
    excludeConstants: string[] | undefined,
    convertible: string[] | undefined,
}


export type VectorQuantity = typeof vectorQuantity