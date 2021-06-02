import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { UserInformationComponent } from './user-information/user-information.component';
import { UsersManagerComponent } from './users-manager/users-manager.component';


const routes: Routes = [
  {
    path: "",
    data: {title: "Main"},
    children: [
      {
        path: "",
        component: UsersManagerComponent,
        data: {title: "List"}
      },
      {
        path: "user-information",
        component: UserInformationComponent,
        data: {title: 'User-Information'}
      },
      {
        path: "change-pass",
        component: ChangePasswordComponent,
        data: {title: 'Change-Password'}
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
