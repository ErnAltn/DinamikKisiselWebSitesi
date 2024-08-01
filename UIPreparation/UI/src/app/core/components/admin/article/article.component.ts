import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Article } from './models/Article';
import { ArticleService } from './services/Article.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-article',
	templateUrl: './article.component.html',
	styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','createdDate','updatedDate','deletedDate','header','imageUrl','description','subject', 'update','delete'];

	articleList:Article[];
	article:Article=new Article();

	articleAddForm: FormGroup;


	articleId:number;

	constructor(private articleService:ArticleService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getArticleList();
    }

	ngOnInit() {

		this.createArticleAddForm();
	}


	getArticleList() {
		this.articleService.getArticleList().subscribe(data => {
			this.articleList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.articleAddForm.valid) {
			this.article = Object.assign({}, this.articleAddForm.value)

			if (this.article.id == 0)
				this.addArticle();
			else
				this.updateArticle();
		}

	}

	addArticle(){

		this.articleService.addArticle(this.article).subscribe(data => {
			this.getArticleList();
			this.article = new Article();
			jQuery('#article').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.articleAddForm);

		})

	}

	updateArticle(){

		this.articleService.updateArticle(this.article).subscribe(data => {

			var index=this.articleList.findIndex(x=>x.id==this.article.id);
			this.articleList[index]=this.article;
			this.dataSource = new MatTableDataSource(this.articleList);
            this.configDataTable();
			this.article = new Article();
			jQuery('#article').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.articleAddForm);

		})

	}

	createArticleAddForm() {
		this.articleAddForm = this.formBuilder.group({		
			id : [0],
header : ["", Validators.required],
imageUrl : ["", Validators.required],
description : ["", Validators.required],
subject : ["", Validators.required]
		})
	}

	deleteArticle(articleId:number){
		this.articleService.deleteArticle(articleId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.articleList=this.articleList.filter(x=> x.id!=articleId);
			this.dataSource = new MatTableDataSource(this.articleList);
			this.configDataTable();
		})
	}

	getArticleById(articleId:number){
		this.clearFormGroup(this.articleAddForm);
		this.articleService.getArticleById(articleId).subscribe(data=>{
			this.article=data;
			this.articleAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
