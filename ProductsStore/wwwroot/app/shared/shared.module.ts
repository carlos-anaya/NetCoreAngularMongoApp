import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CapitalizePipe } from './pipes/capitalize.pipe';
import { TrimPipe } from './pipes/trim.pipe';

@NgModule({
    imports: [CommonModule, FormsModule, ReactiveFormsModule],
    declarations: [CapitalizePipe, TrimPipe],
    exports: [CommonModule, FormsModule, ReactiveFormsModule, CapitalizePipe, TrimPipe]
})
export class SharedModule { }