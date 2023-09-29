import { CommonModule, Location } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Input, OnChanges, OnInit, Output, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { TableColumn } from 'src/app/shared/interfaces/tablecolumn';

@Component({
  selector: 'app-base-grid',
  templateUrl: './base-grid.component.html',
  styleUrls: ['./base-grid.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatPaginatorModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatToolbarModule,
    MatTooltipModule,
  ]
})
export class BaseGridComponent implements OnInit, OnChanges, AfterViewInit {

  @Input() tableColumns: TableColumn[] = [];
  @Input() gridData: any[] = [];
  @Output() onRowClick: EventEmitter<any> = new EventEmitter();
  @Output() onAddButtonClick: EventEmitter<any> = new EventEmitter();
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  public dataSource = new MatTableDataSource();
  public displayedColumns: string[] = [];

  constructor(private location: Location) {
  }

  ngOnInit(): void {
    this.displayedColumns = this.tableColumns.map((tableColumn: TableColumn) => tableColumn.header);
    this.updateDataSource();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnChanges() : void {
    this.updateDataSource();
  }

  updateDataSource() {
    this.dataSource = new MatTableDataSource(this.gridData);    
    this.dataSource.sortingDataAccessor = (item, property) => this.getProperty(item, property);
    this.dataSource.sort = this.sort;
  }

  getProperty(item: any, property: string) : any {
    const field: string | undefined = this.tableColumns.find(c => c.header === property)?.field;
    if (field === undefined)
      return;
    
    const data = item[field];
    return data;
  }

  getRecord(row: any) {
    this.onRowClick.emit(row);
  }

  addButtonClick() {
    this.onAddButtonClick.emit();
  }

  backButtonClick() {
    console.log('Back button.');
    this.location.back();
  }

}
