import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ICustomer, IState } from '../shared/interfaces';
import { CustomerService } from '../core/services/customer.service';
import { StateService } from '../core/services/state.service';

@Component({
    selector: 'customer-edit-reactive',
    templateUrl: './customer-edit-reactive.component.html'
})
export class CustomerEditComponent implements OnInit {
    customerForm: FormGroup;
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
    errorMessage: string;

    constructor(private customerService: CustomerService,
        private stateService: StateService, private router: Router,
        private route: ActivatedRoute, private formBuilder: FormBuilder) { }

    ngOnInit(): void {
        let id = this.route.snapshot.params['id'];
        if (id !== '0') {
            this.operationText = 'Update';
            this.getCustomer(id);
        }
        this.getStates();
        this.buildForm();
    }

    getCustomer(id: string) {
        this.customerService.getCustomer(id).subscribe(
            (customer: ICustomer) => {
                this.customer = customer;
                this.buildForm();
            },
            (err: any) => console.log(err)
        );
    }

    getStates() {
        this.stateService.getStates().subscribe(
            (states: IState[]) => this.states = states,
            (err: any) => console.log(err)
        );
    }

    buildForm() {
        this.customerForm = this.formBuilder.group({
            firstName: [this.customer.firstName, Validators.required],
            lastName: [this.customer.lastName, Validators.required],
            email: [this.customer.email, [Validators.required, Validators.email]],
            gender: [this.customer.gender, Validators.required],
            zip: [this.customer.zip, Validators.required],
            address: [this.customer.address, Validators.required],
            city: [this.customer.city, Validators.required],
            stateId: [this.customer.stateId, Validators.required]
        });
    }

    submit({ value, valid }: { value: ICustomer, valid: boolean }) {
        value.id = this.customer.id;
        value.zip = this.customer.zip || 0;

        if (value.id) {
            this.customerService.updateCustomer(value).subscribe(
                (customer: ICustomer) => {
                    if (customer)
                        this.router.navigateByUrl('/customers');
                    else
                        this.errorMessage = 'Customer could not be updated.';
                },
                (err: any) => console.log(err)
            );
        } else {
            this.customerService.insertCustomer(value).subscribe(
                (customer: ICustomer) => {
                    if (customer)
                        this.router.navigateByUrl('/customers');
                    else
                        this.errorMessage = 'Customer could not be added.';
                },
                (err: any) => console.log(err)
            );
        }
    }

    cancel(e: Event) {
        e.preventDefault();
        this.router.navigateByUrl('/customers');
    }
}