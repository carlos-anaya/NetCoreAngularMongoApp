import { Component, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'filter-textbox',
    templateUrl: './filter-textbox.component.html'
})
export class FilterTextboxComponent {
    model: { filter: string } = { filter: '' };

    @Output() changed: EventEmitter<string> = new EventEmitter<string>();

    filterChanged(event: any) {
        event.preventDefault();
        this.changed.emit(this.model.filter);
    }
}