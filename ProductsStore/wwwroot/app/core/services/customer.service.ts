import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { ICustomer } from '../../shared/interfaces';


@Injectable()
export class CustomerService {

    baseUrl: string = '/api/customers';

    constructor(private http: Http) { }

    getCustomers(): Observable<ICustomer[]> {
        return this.http.get(this.baseUrl)
            .map((res: Response) => {
                let customers = res.json();
                this.calculateCustomersOrderTotal(customers);
                return customers;
            })
            .catch(this.handleError);
    }

    getCustomer(id: string): Observable<ICustomer> {
        return this.http.get(this.baseUrl + '/' + id)
            .map((res: Response) => res.json())
            .catch(this.handleError);
    }

    insertCustomer(customer: ICustomer): Observable<ICustomer> {
        return this.http.post(this.baseUrl, customer)
            .map((res: Response) => {
                const data = res.json();
                console.log('Insert Customer status: ' + data.status);
                return data.customer;
            })
            .catch(this.handleError);
    }

    updateCustomer(customer: ICustomer): Observable<ICustomer> {
        return this.http.put(this.baseUrl + '/' + customer.id, customer)
            .map((res: Response) => {
                const data = res.json();
                console.log('Update Customer status:' + data.status);
                return data.customer;
            })
            .catch(this.handleError);
    }

    calculateCustomersOrderTotal(customers: ICustomer[]) {
        for (let customer of customers) {
            if (customer && customer.orders) {
                let total = 0;
                for (let order of customer.orders) {
                    total += (order.price * order.quantity);
                }
                customer.orderTotal = total;
            }
        }
    }

    private handleError(error: any) {
        console.error('server error', error);
        if (error instanceof Response) {
            let errorMessage = '';
            try {
                errorMessage = error.json().error;
            } catch (e) {
                errorMessage = error.statusText;
            }
            return Observable.throw(errorMessage);
        }
        return Observable.throw(error || '.NET Core server error');
    }
}