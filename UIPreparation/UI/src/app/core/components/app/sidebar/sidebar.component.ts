import { HostListener } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../admin/login/services/auth.service';


declare const $: any;
declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
    claim:string;
}
export const ADMINROUTES: RouteInfo[] = [
  { path: '/user', title: 'Users', icon: 'how_to_reg', class: '', claim:"GetUsersQuery" },
  { path: '/language', title: 'Languages', icon:'language', class: '', claim:"GetLanguagesQuery" },
  { path: '/translate', title: 'TranslateWords', icon: 'translate', class: '', claim: "GetTranslatesQuery" },
  { path: '/article', title: 'Yazılar', icon:'article', class: '', claim:"GetUsersQuery"},
  { path: '/home-page', title: 'AnaSayfa', icon:'home', class: '', claim:"GetUsersQuery" },
  { path: '/about', title: 'Hakkımda', icon: 'manage_accounts', class: '', claim: "GetUsersQuery" },
  { path: '/contact', title: 'İletişim', icon: 'call', class: '', claim: "GetUsersQuery" },
  { path: '/service', title: 'Hizmetler', icon: 'support_agent', class: '', claim: "GetUsersQuery" },
  { path: '/meeting', title: 'Randevu', icon: 'meeting_room', class: '', claim: "GetUsersQuery" }
];

export const USERROUTES: RouteInfo[] = [ 
  //{ path: '/log', title: 'Logs', icon: 'update', class: '', claim: "GetLogDtoQuery" }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  adminMenuItems: any[];
  userMenuItems: any[];

  constructor(private router:Router, private authService:AuthService,public translateService:TranslateService) {
    
  }

  ngOnInit() {
  
    this.adminMenuItems = ADMINROUTES.filter(menuItem => menuItem);
    this.userMenuItems = USERROUTES.filter(menuItem => menuItem);

    var lang=localStorage.getItem('lang') || 'tr-TR'
    this.translateService.use(lang);
  }
  isMobileMenu() {
      if ($(window).width() > 991) {
          return false;
      }
      return true;
  };

  checkClaim(claim:string):boolean{
    return this.authService.claimGuard(claim)
  }
  ngOnDestroy() {
    if (!this.authService.loggedIn()) {
      this.authService.logOut();
      this.router.navigateByUrl("/login");
    }
  } 
 }

