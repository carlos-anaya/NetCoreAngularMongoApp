﻿import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { ICustomer, IState } from '../shared/interfaces';
import { CustomerService } from '../core/services/customer.service';
import { StateService } from '../core/services/state.service';

@Component({
    selector: 'customer-edit',
    templateUrl: './customer-edit.component.html'
})
export class CustomerEditComponent implements OnInit {
    states: IState[];
    customer: ICustomer = {
        firstName: '',
        lastName: '',
        gender: '',
        address: '',
        email: '',
        city: '',
        zip: 0
    };
    operationText: string = 'Insert';

    constructor(private customerService: CustomerService,
        private stateService: StateService, private router: Router,
        private route: ActivatedRoute) { }

    ngOnInit(): void {
        let id = this.route.snapshot.params['id'];
        if (id !== '0') {
            this.operationText = 'Update';
            this.getCustomer(id);
        }
        this.getStates();
    }

    getCustomer(id: string) {
        this.customerService.getCustomer(id).subscribe(
            (customer: ICustomer) => this.customer = customer,
            (err: any) => console.log(err)
        );
    }

    getStates() {
        this.stateService.getStates().subscribe(
            (states: IState[]) => this.states = states,
            (err: any) => console.log(err)
        );
    }

    submit() { }

    cancel(e: Event) {
        e.preventDefault();
        this.router.navigateByUrl('/customers');
    }
}