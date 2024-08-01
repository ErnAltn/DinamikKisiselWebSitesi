import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../../admin/article/services/Article.service';
import { Article } from '../../admin/article/models/Article';

@Component({
  selector: 'app-articles-page',
  templateUrl: './articles-page.component.html',
  styleUrls: ['./articles-page.component.css']
})
export class ArticlesPageComponent implements OnInit {
articleList:Article[]=[];
  constructor(private articleService:ArticleService) { }

  ngOnInit(): void {
    this.articleService.getArticleList().subscribe(data=>{
      this.articleList=data;
      console.log(this.articleList)
    })
  }

  

}
