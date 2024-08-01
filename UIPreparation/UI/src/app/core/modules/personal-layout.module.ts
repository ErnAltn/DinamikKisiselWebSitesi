import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from '../components/app/layouts/admin-layout/admin-layout.routing';
import { DashboardComponent } from '../components/app/dashboard/dashboard.component';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslationService } from 'app/core/services/translation.service';
import { LanguageComponent } from '../components/admin/language/language.component';
import { TranslateComponent } from '../components/admin/translate/translate.component';
import { OperationClaimComponent } from '../components/admin/operationclaim/operationClaim.component';
import { LogDtoComponent } from '../components/admin/log/logDto.component';
import { MatSortModule } from '@angular/material/sort';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { AboutComponent } from '../components/personal-app/about/about.component';
import { HomePageComponent } from '../components/admin/homePage/homePage.component';
import { MeetingComponent } from '../components/admin/meeting/meeting.component';
import { ServiceComponent } from '../components/personal-app/service/service.component';
import { ContactComponent } from '../components/personal-app/contact/contact.component';
import { ArticleComponent } from '../components/admin/article/article.component';
import { PersonalLayoutRoutes } from '../components/app/layouts/personal-layout/personal-layout.routing';
import { FooterComponent } from '../components/personal-app/footer/footer.component';
import { NavbarComponent } from '../components/personal-app/navbar/navbar.component';
import { HomeComponent } from '../components/personal-app/home/home.component';
import { ServicesComponent } from '../components/personal-app/services/services.component';
import { MeetingPageComponent } from '../components/personal-app/meeting-page/meeting-page.component';
import { ArticlesPageComponent } from '../components/personal-app/articles-page/articles-page.component';
import { ArticlePageComponent } from '../components/personal-app/article-page/article-page.component';


// export function layoutHttpLoaderFactory(http: HttpClient) {
// 
//   return new TranslateHttpLoader(http,'../../../../../../assets/i18n/','.json');
// }

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(PersonalLayoutRoutes),
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatRippleModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatTooltipModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatCheckboxModule,
        NgbModule,
        NgMultiSelectDropDownModule,
        SweetAlert2Module,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                //useFactory:layoutHttpLoaderFactory,
                useClass: TranslationService,
                deps: [HttpClient]
            }
        })
    ],
    declarations: [
      //kullanıcının göreceği componentlar 
        HomeComponent,
        AboutComponent,
        ServicesComponent,
        ServiceComponent,
        ContactComponent,
        MeetingPageComponent,
        ArticlesPageComponent,
        ArticlePageComponent
    ]
})

export class PersonalLayoutModule { }
