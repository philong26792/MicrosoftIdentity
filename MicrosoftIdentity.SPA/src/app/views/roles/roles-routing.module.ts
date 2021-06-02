import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoleListComponent } from './role-list/role-list.component';


const routes: Routes = [
  {
    path: "",
    data: {title: "Main"},
    children: [
      {
        path: "",
        component: RoleListComponent,
        data: {title: "List"}
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RolesRoutingModule { }
