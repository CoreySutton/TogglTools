export interface Entry {
    id:                number;
    pid:               number;
    tid:               null;
    uid:               number;
    description:       string;
    start:             string;
    end:               string;
    updated:           string;
    dur:               number;
    user:              string;
    use_stop:          boolean;
    client:            string;
    project:           string;
    project_color:     string;
    project_hex_color: string;
    task:              null;
    billable:          null;
    is_billable:       boolean;
    cur:               null;
    tags:              any[];
}

export interface TotalCurrency {
    currency: null;
    amount:   null;
}
