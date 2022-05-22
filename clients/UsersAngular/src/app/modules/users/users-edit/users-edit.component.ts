import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { User } from 'src/app/shared/models/user';
import { UserResponse } from 'src/app/shared/models/userresponse';
import { UserService } from 'src/app/shared/services/userservice';

@Component({
  selector: 'app-users-edit',
  templateUrl: './users-edit.component.html',
  styleUrls: ['./users-edit.component.css']
})
export class UsersEditComponent implements OnInit {

  user!: UserResponse;
  private routeSub: Subscription;

  constructor(private route: ActivatedRoute, private userService: UserService) {
    this.routeSub = this.route.params.subscribe((params: Params): void => {
      const id = params['id'];
      console.log(id);
      this.getUser(id);
    });
  }

  ngOnInit(): void {
  }

  private getUser(id: string) {
    this.userService.Get(id).subscribe({
      next: (res: any) => {
        const data = res;
        if (data) {
          this.user = data;
        }
      },
      error: (err) => {
        console.log('Error al recuperar el usuario', err);
      }
    })
  }

}
