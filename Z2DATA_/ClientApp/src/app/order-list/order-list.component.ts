import { Component, Inject, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { DataService } from '../shared/dataservice.service'

@Component({
  selector: 'order-list',
  templateUrl: './order-list.component.html'
})

export class OrderListComponent implements OnInit {

  public orders = [];

  constructor(public http: Http, private router: Router, private data: DataService) { }

  ngOnInit(): void {
    this.data.getOrders()
      .subscribe(success => {
        if (success) {
          this.orders = this.data.orders;
        }
      });
  }
}
