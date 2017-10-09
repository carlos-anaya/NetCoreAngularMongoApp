import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { CustomerService } from '../core/services/customer.service';
import { DataFilterService } from '../core/services/data-filter.service';
import { ICustomer, IPagedResults } from '../shared/interfaces';

@Component({
    selector: 'customers',
    templateUrl: './customers.component.html'
})
export class CustomersComponent implements OnInit {
    title: string;
    customers: ICustomer[] = [];
    filteredCustomers: ICustomer[] = [];

    totalRecords: number = 0;
    pageSize: number = 10;

    constructor(private router: Router,
        private customerService: CustomerService,
        private dataFilter: DataFilterService) { }

    ngOnInit(): void {
        this.title = 'Customers';
        this.getCustomersPaged(1);
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

    pageChanged(page: number) {
        this.getCustomersPaged(page);
    }

    pageSizeChanged(pageSize: number) {
        this.pageSize = pageSize;
        this.getCustomersPaged(1);
    }

    getCustomersPaged(page: number) {
        const skip = (page - 1) * this.pageSize;
        this.customerService.getCustomersPaged(skip, this.pageSize).subscribe(
            (res: IPagedResults<ICustomer[]>) => {
                this.customers = this.filteredCustomers = res.results;
                this.totalRecords = res.totalRecords;
            },
            (err: any) => console.log(err),
            () => console.log('getCustomersPaged() executed succesfully')
        );
    }

    filterChanged(filterText: string) {
        if (filterText && this.customers) {
            let props = ['firstName', 'lastName', 'address', 'city', 'state.name', 'orderTotal'];
            this.filteredCustomers = this.dataFilter.filter(this.customers, props, filterText);
        } else
            this.filteredCustomers = this.customers;
    }

    addNewCustomer() {
        this.router.navigateByUrl('/customers/0');
    }
}