let vectorQuantity : {
    type: "Vector",
    name: string,
    component: string,
    baseUnits: string,
    unit: string,
    unitBias: string | boolean,
    dimensionalities: number[],
    units: string | ({ 
        special: false | undefined,
        singular: string,
        plural: string,
        SI : boolean | undefined,
        symbol: string | undefined,
        base: true | false | undefined
    } | {
        special: true,
        separator: boolean | undefined
    }) [],
    convertible: string | string[] | false,
    symbol: string | undefined,
    bases: string | ({ 
        special: false | undefined,
        singular: string,
        plural: string,
        SI : boolean | undefined,
        symbol: string | undefined,
        base: true | false | undefined
    } | {
        special: true,
        separator: boolean | undefined
    })[]
}


export type VectorQuantity = typeof vectorQuantity