let unit : {
    type: 'Unit',
    name: string,
    quantity: string,
    bias: boolean | undefined,
    unbiasedQuantity: string | undefined,
    derived: { signature: string[], expression: string }[] | undefined,
    units: ({ 
        special: false | undefined,
        name: string,
        plural: string,
        symbol: string | undefined,
        SI: boolean | undefined,
        value: string | undefined,
        bias : string | undefined,
        alias: string | undefined,
        derived: string[] | undefined,
        scaled: { from: string, by: string } | undefined,
        prefix: { from: string, with: string } | undefined,
        offset: { from: string, by: string } | undefined
    } | {
        special: true,
        separator: boolean | undefined
    })[],
    constants: ({
        special: false | undefined,
        name: string,
        plural: string,
        symbol: string | undefined,
        scaled: { from: string, by: string },
        offset: { from: string, by: string } | undefined
    } | {
        special: true,
        separator: boolean | undefined
    })[] | undefined
}

export type Unit = typeof unit