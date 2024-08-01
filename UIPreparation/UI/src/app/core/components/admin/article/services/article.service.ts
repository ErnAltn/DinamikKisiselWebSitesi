import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Article } from '../models/Article';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private httpClient: HttpClient) { }


  getArticleList(): Observable<Article[]> {

    return this.httpClient.get<Article[]>(environment.getApiUrl + '/articles/getall')
  }

  getArticleById(id: number): Observable<Article> {
    return this.httpClient.get<Article>(environment.getApiUrl + '/articles/getbyid?id='+id)
  }

  addArticle(article: Article): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/articles/', article, { responseType: 'text' });
  }

  updateArticle(article: Article): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/articles/', article, { responseType: 'text' });

  }

  deleteArticle(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/articles/', { body: { id: id } });
  }


}