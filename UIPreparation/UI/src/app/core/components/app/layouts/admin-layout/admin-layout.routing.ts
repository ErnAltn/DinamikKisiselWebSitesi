import { Routes } from '@angular/router';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { LanguageComponent } from 'app/core/components/admin/language/language.component';
import { LogDtoComponent } from 'app/core/components/admin/log/logDto.component';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { OperationClaimComponent } from 'app/core/components/admin/operationclaim/operationClaim.component';
import { TranslateComponent } from 'app/core/components/admin/translate/translate.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { LoginGuard } from 'app/core/guards/login-guard';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { AboutComponent } from 'app/core/components/admin/about/about.component';
import { ContactComponent } from 'app/core/components/admin/contact/contact.component';
import { ArticleComponent } from 'app/core/components/admin/article/article.component';
import { MeetingComponent } from 'app/core/components/admin/meeting/meeting.component';
import { HomePageComponent } from 'app/core/components/admin/homePage/homePage.component';
import { ServiceComponent } from 'app/core/components/admin/service/service.component';
import { LoginRouting } from 'app/core/guards/login-routing';





export const AdminLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent,canActivate:[LoginGuard] }, 
    { path: 'user',           component: UserComponent, canActivate:[LoginGuard] },
    { path: 'group',          component: GroupComponent, canActivate:[LoginGuard] },
    { path: 'login',          component: LoginComponent , canActivate:[LoginRouting]},
    { path: 'language',       component: LanguageComponent,canActivate:[LoginGuard]},
    { path: 'translate',      component: TranslateComponent,canActivate:[LoginGuard]},
    { path: 'operationclaim', component: OperationClaimComponent,canActivate:[LoginGuard]},
    { path: 'log',            component: LogDtoComponent,canActivate:[LoginGuard]},
    { path: 'about',            component: AboutComponent,canActivate:[LoginGuard]},
    { path: 'contact',            component: ContactComponent,canActivate:[LoginGuard]},
    { path: 'article',            component: ArticleComponent,canActivate:[LoginGuard]},
    { path: 'meeting',            component: MeetingComponent,canActivate:[LoginGuard]},
    { path: 'home-page',            component: HomePageComponent,canActivate:[LoginGuard]},
    { path: 'service',            component: ServiceComponent,canActivate:[LoginGuard]}
    
];
