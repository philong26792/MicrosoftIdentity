import { INavData } from "@coreui/angular";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root", // <- ADD THIS
})
export class NavItem {
  navItems: INavData[] = [];
  constructor() {}
  getNav() {
    this.navItems = [];
    const roles: string[] = JSON.parse(localStorage.getItem("roles"));
    let navItemMain = {
      name: "Dashboard",
      url: "/dashboard",
      icon: "icon-speedometer",
      badge: {
        variant: "info",
        text: "NEW"
      }
    };
    this.navItems.push(navItemMain);

    if (roles.includes("fullRole")) {
      let navItemRole1 = {
        name: "Roles",
        url: "/roles",
        icon: "icon-settings",
      };
      this.navItems.push(navItemRole1);

      let navItemRole2 = {
        name: "Users",
        url: "/users",
        icon: "icon-user",
      };
      this.navItems.push(navItemRole2);
    }

    if (roles.includes("admin")) {
      let navItemAdmin = {
        name: "Users",
        url: "/users",
        icon: "icon-user",
      };
      this.navItems.push(navItemAdmin);
    }

    let navItemUserInformation = {
      name: "User Information",
      url: "/users/user-information",
      icon: "icon-user"
    }
    this.navItems.push(navItemUserInformation);
    
    let navItemChangePass = {
      name: "Change Password",
      url: "/users/change-pass",
      icon: "icon-user"
    }
    this.navItems.push(navItemChangePass);

    return this.navItems;
  }
  
}
