
import { Injectable } from "@angular/core";
import { AuthService } from "../_services/auth.service";
import { CanActivate, Router } from "@angular/router";

@Injectable({
    providedIn: "root"
})
export class RegisterGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }
    canActivate(): boolean {
        if (!this.authService.loggedIn()) {
            return true;
        }
        this.router.navigate(["/dashboard"]);
        return false;
    }
}
