import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticleService } from '../../admin/article/services/Article.service';
import { Article } from '../../admin/article/models/Article';

@Component({
  selector: 'app-article-page',
  templateUrl: './article-page.component.html',
  styleUrls: ['./article-page.component.css']
})
export class ArticlePageComponent implements OnInit {
  articleId?:number;
  article?:Article;
  constructor(private activeRoute:ActivatedRoute,private articleService:ArticleService) { }

  ngOnInit(): void {
this.activeRoute.params.subscribe(params=>{
  this.articleId=params['articleId'];
  if(params['articleId'])
    this.getArticleById(this.articleId);
})
  }


getArticleById(id:number){
  this.articleService.getArticleById(id).subscribe(data=>{
    this.article=data;
    console.log(this.article)
  })
}


}
