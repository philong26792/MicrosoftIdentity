import {Component, OnInit} from '@angular/core';
import { NavItem } from '../../_nav';
import { AuthService } from '../../_core/_services/auth.service';
import { AlertifyService } from '../../_core/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent implements OnInit {
  public sidebarMinimized = false;
  public navItems = [];
  currentUser: any = JSON.parse(localStorage.getItem('user'));
  constructor(private authService: AuthService, 
              private alertify: AlertifyService,
              private nav: NavItem,
              private router: Router) {
  }

  ngOnInit() {
    this.navItems = this.nav.getNav();
  }
  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }
  logout() {
    this.authService.logout();
    this.alertify.message('Logged out');
  }
}
