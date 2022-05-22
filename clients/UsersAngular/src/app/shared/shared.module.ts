import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { UserService } from './services/userservice';
import { JWTService } from './services/jwtservice';


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
    UserService,
    JWTService
  ]
})
export class SharedModule { }
