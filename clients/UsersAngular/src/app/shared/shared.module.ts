import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { Userservice } from './services/userservice';


@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    SharedRoutingModule
  ],
  exports:[    
  ],
  providers:[
    Userservice,
  ]
})
export class SharedModule { }
