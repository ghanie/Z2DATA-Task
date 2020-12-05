import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';


interface IProcess {
  id: number,
  customerName: string,
  goodsId: number,
  quantity: number;
}

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




  //////// Save methods //////////

  /** POST: add a new Order to the server */
  
  addOrder(process:IProcess) {
    return this.http.post(this.AppUrl + 'api/orders/Create', process)
      .pipe(
        map((response: Response) => response.json()))
  } 
 

  /** DELETE: delete the Order from the server */
  
  deleteOrder(id:number) {  
    return this.http.delete(this.AppUrl + "api/Orders/Delete/" + id)  
        .pipe(
          map((response: Response) => response.json()));
}  
  


updateOrder(process : IProcess) {  
  return this.http.put(this.AppUrl + "api/Orders/Edit/"+ process.id, process)  
      .pipe(
        map((response: Response) => response.json()));  
}  


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
