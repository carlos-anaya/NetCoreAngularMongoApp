import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';

import { EnsureModuleLoadedOnceGuard } from './ensureModuleLoadedOnceGuard';
import { Sorter } from './sorter';
import { DataService } from './services/data.service';
import { DataFilterService } from './services/data-filter.service';
import { CustomerService } from './services/customer.service';
import { StateService } from './services/state.service';

@NgModule({
    imports: [CommonModule, HttpModule],
    exports: [HttpModule],
    providers: [CustomerService, StateService, DataService, DataFilterService, Sorter] // these should be singleton
})
export class CoreModule extends EnsureModuleLoadedOnceGuard {    //Ensure that CoreModule is only loaded into AppModule

    //Looks for the module in the parent injector to see if it's already been loaded (only want it loaded once)
    constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
        super(parentModule);
    }
}