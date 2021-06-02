import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserChangePass } from '../../../_core/_models/user-change-pass';
import { AccountService } from '../../../_core/_services/account.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { AuthService } from '../../../_core/_services/auth.service';
import { FunctionUtility } from '../../../_core/_utility/function-utility';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {
  currentPass: string;
  passNew: string;
  confirmPass: string;
  constructor(private accountService: AccountService,
              private authService: AuthService,
              private alertify: AlertifyService,
              private router: Router,
              private spiner: NgxSpinnerService,
              private functionUtility: FunctionUtility) { }

  ngOnInit() {
  }
  changePass() {
    if(this.functionUtility.checkEmpty(this.currentPass) || this.functionUtility.checkEmpty(this.passNew)) {
      return this.alertify.error('Please Enter Input Pass Old and Pass New');
    }
    if(this.passNew !== this.confirmPass) {
      return this.alertify.error('New password does not match!');
    }
    this.spiner.show();
    let param : UserChangePass =  {
      passwordNew: this.passNew,
      passwordOld: this.currentPass
    }
    this.accountService.changePass(param).subscribe(res => {
      if(res) {
        [this.currentPass, this.passNew, this.confirmPass] = ['', '', ''];
        this.alertify.success('Change password success, please login!');
        this.alertify.confirm('', 'Do you want to logout?', () => {
          this.authService.logout();
        });
      } else {
        this.alertify.success('An error occurred');
      }
      this.spiner.hide();
    });
  }
}
