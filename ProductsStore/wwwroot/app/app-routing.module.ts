import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { CustomersComponent } from './customers/customers.component';
import { CustomerEditComponent } from './customers/customer-edit.component';
import { CustomersGridComponent } from './customers/customers-grid.component';

const app_routes: Routes = [
    { path: 'customers', component: CustomersComponent },
    { path: 'customers/:id', component: CustomerEditComponent },
    { path: 'home', component: HomeComponent },
    { path: '', pathMatch: 'full', redirectTo: '/home' },
    { path: '**', pathMatch: 'full', redirectTo: '/home' } //catch any unfound routes and redirect to home page
];

@NgModule({
    imports: [RouterModule.forRoot(app_routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
    static components = [HomeComponent, CustomersComponent, CustomersGridComponent, CustomerEditComponent];
}