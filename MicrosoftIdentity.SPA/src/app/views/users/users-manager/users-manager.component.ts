import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AppUser } from '../../../_core/_models/app-user';
import { PaginatedResult, Pagination } from '../../../_core/_models/pagination';
import { AccountService } from '../../../_core/_services/account.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { RolesService } from '../../../_core/_services/roles.service';
import { FunctionUtility } from '../../../_core/_utility/function-utility';

@Component({
  selector: 'app-users-manager',
  templateUrl: './users-manager.component.html',
  styleUrls: ['./users-manager.component.scss']
})
export class UsersManagerComponent implements OnInit {
  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: 4,
    totalItems: 1,
    totalPages: 1,
  };
  users: AppUser[] = [];
  modalRef: BsModalRef | null;
  roles: any = [];
  userCurrentUpdate: string;
  role: string;
  roleCurrent: string;
  constructor(private accountService: AccountService,
              private roleService: RolesService,
              private alertify: AlertifyService,
              private modalService: BsModalService,
              private functionUtility: FunctionUtility) { }

  ngOnInit() {
    this.getAll();
    this.getRoles();
  }
  getAll() {
    this.accountService.getAll(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((res: PaginatedResult<any[]>) => {
        this.users = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      }); 
  }
  getRoles() {
    this.roleService.getAll().subscribe(res => {
      this.roles = res.filter(x => x.status);
    });
  }
  remove(users: string) {
    this.accountService.removeUser(users).subscribe(res => {
      if(res) {
        this.alertify.success('Delete user success');
        this.getAll();
      } else {
        this.alertify.error('Error');
      }
    })
  }
  openModalRole(template: TemplateRef<any>, userName: string) {
    this.modalRef = this.modalService.show(template, {class: 'modal-md'});
    this.userCurrentUpdate = userName;
    this.accountService.getInformationUser(userName).subscribe(res => {
      this.role = (res.role)[0].toString();
      this.roleCurrent = JSON.parse(JSON.stringify((res.role)[0].toString()));
    })
  }
  updateRole() {
    if(this.role === this.roleCurrent) {
      return this.alertify.error('Please choose another option');
    }
    this.accountService.updateUserRole(this.role, this.userCurrentUpdate).subscribe(res => {
      if(res) {
        this.alertify.success('Update Role successfuly!');
      } else {
        this.alertify.error('Error!');
      }
      this.modalRef.hide();
    });
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getAll();
  }
}
