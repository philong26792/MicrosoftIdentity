import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterModel } from '../../_core/_models/register-model';
import { AccountService } from '../../_core/_services/account.service';
import { AlertifyService } from '../../_core/_services/alertify.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'register.component.html'
})
export class RegisterComponent {
  registerModel: RegisterModel = {
    userName : '',
    email: '',
    password: '',
    confirmPassword: ''
  };
  constructor(private accountService: AccountService,
              private alertify: AlertifyService,
              private router: Router) { }
  register() {
    this.accountService.register(this.registerModel).subscribe(res => {
      if(res.data === 'exist') {
        this.alertify.error('Tài khoản này đã tồn tại');
      } else if(res.data === 'ok') {
        this.alertify.success('Đăng ký thành công! Mời đăng nhập');
        this.router.navigate(['/login']);
      } else {
        this.alertify.error('Error');
      }
    });
  }
}
