import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { UserService } from './services/userservice';
import { JWTService } from './services/jwtservice';
import { ErrorService } from './services/errorservice';


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
    JWTService,
    ErrorService
  ]
})
export class SharedModule { }
