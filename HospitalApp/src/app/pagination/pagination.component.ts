import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent {
  @Input() page: number;
  @Output() pageChange = new EventEmitter<number>();
  @Input() pages: number[];

  nextPage(): void {
    this.pageChange.emit(this.page + 1);
  }

  prevPage(): void {
    if (this.page > 1) {
      this.pageChange.emit(this.page - 1);
    }
  }

  onPageChange(newPage: number): void {
    if (newPage >= 1 && newPage <= this.totalPages) {
      this.page = newPage;
      this.pageChange.emit(newPage);
    }
  }

  get totalPages(): number {
    // Calculate the total pages based on your data
    // You may need to pass the totalItems as an Input to this component
    // and calculate totalPages accordingly
    return 10; // Replace with your actual total pages calculation
  }
   
}
