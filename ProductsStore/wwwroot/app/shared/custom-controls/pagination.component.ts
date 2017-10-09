import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core'
@Component({
    selector: 'pagination',
    templateUrl: './pagination.component.html',
    styleUrls: ['./pagination.component.css']
})

export class PaginationComponent implements OnInit {
    private pagerTotalRecords: number;
    private pagerPageSize: number;

    totalPages: number;
    pages: number[] = [];
    currentPage: number = 1;
    isVisible: boolean = false;
    isPreviousEnabled: boolean = false;
    isNextEnabled: boolean = true;

    @Input()
    get pageSize(): number {
        return this.pagerPageSize;
    }

    set pageSize(value: number) {
        this.pagerPageSize = value;
        this.update();
    }

    @Input()
    get totalRecords(): number {
        return this.pagerTotalRecords;
    }

    set totalRecords(value: number) {
        this.pagerTotalRecords = value;
        this.update();
    }

    @Output()
    pageChanged: EventEmitter<number> = new EventEmitter();

    @Output()
    pageSizeChanged: EventEmitter<number> = new EventEmitter();

    update() {
        if (this.pagerPageSize && this.pagerTotalRecords &&
            this.totalRecords >= this.pageSize) {
            this.totalPages = Math.ceil(this.pagerTotalRecords / this.pageSize);
            this.pages = [];
            this.isVisible = true;

            for (let i = 1; i < this.totalPages + 1; i++) {
                this.pages.push(i);
            }

            return;
        }
        this.isVisible = false;
    }

    previousPageClick(e?: MouseEvent) {
        let page = this.currentPage;
        if (page > 1)
            page--;
        this.pageChange(page, e);
    }

    nextPageClick(e?: MouseEvent) {
        let page = this.currentPage;
        if (page < this.totalPages)
            page++;
        this.pageChange(page, e);
    }

    pageChange(page: number, e?: MouseEvent) {
        if (e)
            e.preventDefault();

        if (this.currentPage === page)
            return;

        this.currentPage = page;
        this.isPreviousEnabled = this.currentPage > 1;
        this.isNextEnabled = this.currentPage < this.totalPages;
        this.pageChanged.emit(page);
    }

    pageSizeChange(pageSize: number, e?: MouseEvent) {
        if (e)
            e.preventDefault();

        if (this.pageSize === pageSize)
            return;

        this.currentPage = 1;
        this.pageSize = pageSize;
        this.pageSizeChanged.emit(pageSize);
    }

    ngOnInit(): void { }
}