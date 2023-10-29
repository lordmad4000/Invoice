import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SearchInput } from 'src/app/shared/models/searchinput';
import { SearchItem } from 'src/app/shared/models/searchitem';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [
    CommonModule,
    DragDropModule,
    FormsModule,
    MatChipsModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatSelectModule,
    MatTooltipModule,
    ReactiveFormsModule,
    TranslateModule
  ],
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  @Output() onCloseEvent = new EventEmitter();
  @Output() onSelectedItemEvent = new EventEmitter<SearchItem>();
  @Output() onChangesInputSearchEvent = new EventEmitter<SearchInput>();
  @Input() listIcon: string = '';
  @Input() searchInput: SearchInput[] = [];
  @Input() searchList: SearchItem[] = [];

  public selectedSearch: SearchInput = new SearchInput();
  public inputSearch: string = '';

  constructor() { }

  ngOnInit() {
    this.selectedSearch = this.searchInput[0];
  }

  toggleChip(option: SearchInput) {
    this.selectedSearch = option;
    this.inputSearch = '';
    this.searchList = [];
  }

  onChangesInputSearch() {
    const searchInput: SearchInput = {
      id: this.selectedSearch.id,
      inputText: this.inputSearch,
      chipText: this.selectedSearch.chipText,
    };
    this.onChangesInputSearchEvent.emit(searchInput);
  }

  onSelectedItem(item: SearchItem) {
    item.searchId = this.selectedSearch.id;
    this.onSelectedItemEvent.emit(item);
  }

  onClose() {
    this.onCloseEvent.emit();
  }

}


