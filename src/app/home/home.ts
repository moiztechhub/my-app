import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit{

  

  myname: string = "khizer";



  users: any[] = [];

  constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.http
    .get<any[]>('https://jsonplaceholder.typicode.com/users')
    .subscribe(data=>{this.users=data})
  }

}
