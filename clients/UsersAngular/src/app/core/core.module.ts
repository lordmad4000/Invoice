import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { CoreComponent } from './core.component';
import { AuthGuard } from './guards/auth.guard';
import { NavmenuComponent } from './components/navmenu/navmenu.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';


@NgModule({
  declarations: [
    CoreComponent,
    NavmenuComponent
  ],
  imports: [
    CommonModule,
    CoreRoutingModule,    
    MatToolbarModule,
    MatSidenavModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
  ],
  exports: [
    NavmenuComponent,
  ],
  providers: [
    NavmenuComponent,
    AuthGuard,
  ],
})
export class CoreModule { }
