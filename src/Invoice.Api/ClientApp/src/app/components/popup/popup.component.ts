import { CommonModule } from '@angular/common';
import { Component, OnInit, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';

@Component({
  standalone: true,
  imports: [
      CommonModule,
      MatButtonModule,
      MatDialogModule,
  ],
  selector: 'app-popup',
  templateUrl: './popup.component.html',
  styleUrls: ['./popup.component.css']
})
export class PopupComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<PopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) { 
    this.dialogRef.disableClose = true;
  }
  
  ngOnInit(): void {
  }

  closeModal(actionValue: any) {
    this.dialogRef.close(actionValue);
  }

}