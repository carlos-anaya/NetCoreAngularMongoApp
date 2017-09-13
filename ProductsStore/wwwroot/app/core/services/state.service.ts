import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { IState } from '../../shared/interfaces';

@Injectable()
export class StateService {

    baseUrl: string = '/api/states';

    constructor(private http: Http) { }

    getStates(): Observable<IState> {
        return this.http.get(this.baseUrl)
            .map((res: Response) => res.json())
            .catch(this.handleError);
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