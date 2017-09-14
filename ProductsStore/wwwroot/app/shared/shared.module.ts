import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CapitalizePipe } from './pipes/capitalize.pipe';
import { TrimPipe } from './pipes/trim.pipe';
import { FilterTextboxComponent } from './custom-controls/filter-textbox.component';

@NgModule({
    imports: [CommonModule, FormsModule, ReactiveFormsModule],
    declarations: [CapitalizePipe, TrimPipe, FilterTextboxComponent],
    exports: [CommonModule, FormsModule, ReactiveFormsModule, CapitalizePipe, TrimPipe, FilterTextboxComponent]
})
export class SharedModule { }