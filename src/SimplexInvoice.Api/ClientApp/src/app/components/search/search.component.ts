import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, QueryList, Renderer2 } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatChipListboxChange, MatChipOption, MatChipsModule } from '@angular/material/chips';
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
export class SearchComponent implements OnInit, AfterViewInit {
  @Output() onCloseEvent = new EventEmitter();
  @Output() onSelectedItemEvent = new EventEmitter<SearchItem>();
  @Output() onChangesInputSearchEvent = new EventEmitter<SearchInput>();
  @Input() listIcon: string = '';
  @Input() searchInput: SearchInput[] = [];
  @Input() searchList: SearchItem[] = [];

  public selectedSearch: SearchInput = new SearchInput();
  public inputSearch: string = '';

  constructor(private renderer: Renderer2) { }

  ngOnInit() {
    this.selectedSearch = this.searchInput[0];
  }

  ngAfterViewInit(): void {
    this.renderer.selectRootElement('#inputsearch').focus();
  }

  toggleChip(matChipListboxChange: MatChipListboxChange) {
    if (matChipListboxChange.value === undefined) {
      this.selectChipByValue(matChipListboxChange.source._chips, this.selectedSearch.chipText);
      return;
    }

    let selectOption = this.searchInput.find(c => c.chipText === matChipListboxChange.value);
    if (selectOption === undefined) {
      selectOption = this.searchInput[0];
      this.selectFirstChip(matChipListboxChange.source._chips);
    }
    this.selectedSearch = selectOption;
    this.inputSearch = '';
    this.searchList = [];
  }

  selectFirstChip(chips: QueryList<MatChipOption>) {
    if (chips === undefined) {
      return;
    }
    chips.first.toggleSelected();
  }

  selectChipByValue(chips: QueryList<MatChipOption>, value: string) {
    if (chips === undefined) {
      return;
    }
    const chip = chips.find(c => c.value === value);
    if (chip === undefined) {
      chips.first.select;
    }

    chip?.toggleSelected();
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


