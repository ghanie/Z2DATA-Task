import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';


@Injectable(
   { 
   providedIn: 'root' 
   }
)


export class DataService {

  private AppUrl = '';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.AppUrl = baseUrl;
  }

  public orders = [];

  /** GET heroes from the server */
  getOrders() {
    return this.http.get(this.AppUrl + "/api/orders/index")
      .pipe(
        map((data: any[]) => {
          this.orders = data;
          return true;
        }));
  }



  /* GET heroes whose name contains search term */
  /*
    searchHeroes(term: string): Observable<Hero[]> {
      if (!term.trim()) {
        // if not search term, return empty hero array.
        return of([]);
      }
      return this.http.get<Hero[]>(`${this.AppUrl}/?name=${term}`).pipe(
        tap(x => x.length ?
           this.log(`found heroes matching "${term}"`) :
           this.log(`no heroes matching "${term}"`)),
        catchError(this.handleError<Hero[]>('searchHeroes', []))
      );
    }
  */

  //////// Save methods //////////

  /** POST: add a new hero to the server */
  /*
   addHero(hero: Hero): Observable<Hero> {
     return this.http.post<Hero>(this.AppUrl, hero, this.httpOptions).pipe(
       tap((newHero: Hero) => this.log(`added hero w/ id=${newHero.id}`)),
       catchError(this.handleError<Hero>('addHero'))
     );
   }
 */

  /** DELETE: delete the hero from the server */
  /*
    deleteHero(hero: Hero | number): Observable<Hero> {
      const id = typeof hero === 'number' ? hero : hero.id;
      const url = `${this.AppUrl}/${id}`;
  
      return this.http.delete<Hero>(url, this.httpOptions).pipe(
        tap(_ => this.log(`deleted hero id=${id}`)),
        catchError(this.handleError<Hero>('deleteHero'))
      );
    }
  */


  /** PUT: update the hero on the server */
  /*
    updateHero(hero: Hero): Observable<any> {
      return this.http.put(this.AppUrl, hero, this.httpOptions).pipe(
        tap(_ => this.log(`updated hero id=${hero.id}`)),
        catchError(this.handleError<any>('updateHero'))
      );
    }
  */


  /**
     * Handle Http operation that failed.
     * Let the app continue.
     * @param operation - name of the operation that failed
     * @param result - optional value to return as the observable result
     */

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
