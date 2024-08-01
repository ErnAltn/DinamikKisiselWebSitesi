import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../components/admin/login/services/auth.service";


@Injectable()
export class LoginRouting implements CanActivate {

    constructor(private router: Router, private authService: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        return this.checkClaimForRoute(state.url);

    }

    async checkClaimForRoute(routePath: string): Promise<boolean> {
       
        if (this.authService.loggedIn()) {
            this.router.navigate(["dashboard"]);
            return true;
        }
        return true;
    }





}