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
import { HomePageComponent } from 'app/core/components/admin/homePage/homePage.component';
import { FooterComponent } from 'app/core/components/personal-app/footer/footer.component';
import { NavbarComponent } from 'app/core/components/personal-app/navbar/navbar.component';
import { HomeComponent } from 'app/core/components/personal-app/home/home.component';
import { AboutComponent } from 'app/core/components/personal-app/about/about.component';
import { ServicesComponent } from 'app/core/components/personal-app/services/services.component';
import { ArticlePageComponent } from 'app/core/components/personal-app/article-page/article-page.component';
import { ArticlesPageComponent } from 'app/core/components/personal-app/articles-page/articles-page.component';
import { ContactComponent } from 'app/core/components/personal-app/contact/contact.component';
import { MeetingPageComponent } from 'app/core/components/personal-app/meeting-page/meeting-page.component';
import { ServiceComponent } from 'app/core/components/personal-app/service/service.component';





export const PersonalLayoutRoutes: Routes = [

    // { path: 'dashboard',      component: DashboardComponent}, personal componentler about article vs   
    { path: '',  pathMatch:'full',    component: HomeComponent},
    { path: 'home',    component: HomeComponent},
    { path: 'pabout',      component: AboutComponent},
    { path: 'pservices',      component: ServicesComponent},
    { path: 'pservice',      component: ServiceComponent},
    { path: 'particles',      component: ArticlesPageComponent},
    { path: 'particle/:articleId',      component: ArticlePageComponent},
    { path: 'pservices/:serviceId',      component: ServiceComponent},
    { path: 'pcontact',      component: ContactComponent},
    { path: 'pmeeting',      component: MeetingPageComponent}
    
];
