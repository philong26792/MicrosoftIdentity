import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { interval } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { UserUpdateInformation } from '../../../_core/_models/user-update-information';
import { AccountService } from '../../../_core/_services/account.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { FunctionUtility } from '../../../_core/_utility/function-utility';

@Component({
  selector: 'app-user-information',
  templateUrl: './user-information.component.html',
  styleUrls: ['./user-information.component.scss']
})
export class UserInformationComponent implements OnInit {
  user: UserUpdateInformation;
  isDisable = true; 
  constructor(private accountService: AccountService,
              private alertify: AlertifyService,
              private spinner: NgxSpinnerService,
              private functionUtility: FunctionUtility) { }

  ngOnInit() {
    this.getUser();
  }
  getUser() {
    this.accountService.getInformationUser('currentUser').subscribe(res => {
      this.user = res.user; 
    })
  }
  updateUser() {
    if(this.functionUtility.checkEmpty(this.user.firstName) || 
      this.functionUtility.checkEmpty(this.user.lastName) ||
      this.functionUtility.checkEmpty(this.user.phoneNumber)) {
        return this.alertify.error('Please enter all Input!');
      }
      this.spinner.show();
      let param: UserUpdateInformation = {
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        age: this.user.age,
        phoneNumber: this.user.phoneNumber
      }
      this.accountService.updateInformationUser(param).subscribe(res => {
        if(res) {
          this.alertify.success('Update information success!');
          this.isDisable = true;
        } else {
          this.alertify.error('Error');
        }
        this.spinner.hide();
      });
  }
  resetUser() {
    this.getUser();
  }
}
