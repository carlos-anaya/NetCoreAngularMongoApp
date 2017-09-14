import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { CustomerService } from '../core/services/customer.service';
import { ICustomer } from '../shared/interfaces';

@Component({
    selector: 'customers',
    templateUrl: './customers.component.html'
})
export class CustomersComponent implements OnInit {
    title: string;
    customers: ICustomer[] = [];
    filteredCustomers: ICustomer[] = [];

    constructor(private router: Router,
        private customerService: CustomerService) { }

    ngOnInit(): void {
        this.title = 'Customers';
        this.getCustomers();
    }

    getCustomers(): any {
        this.customerService.getCustomers().subscribe(
            (customers: ICustomer[]) => {
                this.customers = this.filteredCustomers = customers;
            },
            (err: any) => console.log(err),
            () => console.log('getCustomers() executed succesfully')
        );
    }
}