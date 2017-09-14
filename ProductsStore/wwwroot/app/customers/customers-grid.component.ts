import { Component, OnInit, Input, ChangeDetectionStrategy } from '@angular/core';

import { ICustomer } from '../shared/interfaces';
import { Sorter } from '../core/sorter';

@Component({
    selector: 'customers-grid',
    templateUrl: './customers-grid.component.html',
    styleUrls: ['./customers-grid.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CustomersGridComponent implements OnInit {

    @Input() customers: ICustomer[] = [];

    constructor(private sorter: Sorter) { }

    ngOnInit(): void { }

    sort(prop: string) {
        this.sorter.sort(this.customers, prop);
    }
}