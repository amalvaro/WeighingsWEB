export class VirtualTable {
    tableCode : TableCode;
    tableName : TableName;
    tableCaption: TableCaption;
    fields : Fields[];

}

export class Field {
    fieldCode: FieldCode;
    fieldName: FieldName;
    caption: Caption;
    allowDBNull: AllowDBNull;
    defaultValue: DefaultValue;
}


/* XML tag attributes */

export class Fields {
    field: Field;
}

export class TableCode {
    value: string;
}

export class TableName {
    value: string;
}

export class TableCaption {
    value: string;
}

export class FieldCode {
    value: string;
}

export class FieldName {
    value: string;
}
export class Caption {
    value: string;
}

export class AllowDBNull {
    value: string;
}

export class DefaultValue {
    value: string;
}
