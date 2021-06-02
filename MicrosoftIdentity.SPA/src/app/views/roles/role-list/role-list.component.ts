import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { RolesService } from '../../../_core/_services/roles.service';
import { FunctionUtility } from '../../../_core/_utility/function-utility';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss']
})
export class RoleListComponent implements OnInit {
  roles: any = [];
  roleNameAdd: string;
  roleStatus: boolean = true;
  roleEditCurrent: any;
  modalRef: BsModalRef | null;
  constructor(private roleService: RolesService,
              private alertify: AlertifyService,
              private modalService: BsModalService,
              private functionUtility: FunctionUtility) { }

  ngOnInit() {
    this.getAll();
  }
  getAll() {
    this.roleService.getAll().subscribe(res => {
      this.roles = res;
    });
  }
  delete(roleName: string) {
    this.alertify.confirm('Delete Role!', 'Are you sure you want to delete "' + roleName + '" ?', () => {
      this.roleService.delete(roleName).subscribe(res => {
        if(res) {
          this.alertify.success('Delete success!');
          this.getAll();
        } else {
          this.alertify.success('Error!');
        }
      })
    });
  }
  addRole() {
    if(this.functionUtility.checkEmpty(this.roleNameAdd)) {
      return this.alertify.error('please enter role name');
    }
    let param = {
      name: this.roleNameAdd,
      status: this.roleStatus
    }
    this.roleService.create(param).subscribe(res => {
      if(res.data === 'exist') {
        this.alertify.error('This name already exists');
      } else if (res.data === 'ok') {
        this.alertify.success('Add success');
        this.modalRef.hide();
        this.getAll();
      } else {
        this.alertify.error('Error');
      }
    });
  }
  updateRole() {
    this.roleService.update(this.roleEditCurrent).subscribe(res => {
      if(res) {
        this.alertify.success('Update success');
        this.modalRef.hide();
      } else {
        this.alertify.success('Error');
      }
    });
  }
  openModalAdd(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template,  {class: 'modal-md'});
  }
  openModalUpdate(template: TemplateRef<any>, role: any) {
    this.modalRef = this.modalService.show(template, {class: 'modal-md'});
    this.roleEditCurrent = role;
  }
}
