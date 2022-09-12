import { Component, OnInit, Optional, SkipSelf } from '@angular/core';
// import { CoreModule } from './core.module';

@Component({
  selector: 'app-core',
  templateUrl: './core.component.html',
  styleUrls: ['./core.component.css']
})
export class CoreComponent implements OnInit {

  constructor() {}

  // constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
  //   if (parentModule) {
  //     throw new Error(
  //       'CoreModule is already loaded. Import it in the AppModule only.');
  //   }
  // }

  ngOnInit(): void {
  }

}
