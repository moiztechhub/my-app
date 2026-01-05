import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-employess',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employess.html',
  styleUrl: './employess.css',
})

export class Employees implements OnInit {

  api = 'https://example.com/api/employees';

  employees: any[] = [];

  form!: any;

  editingId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient
  ) {}

  ngOnInit() {

    this.form = this.fb.group({
      name: [''],
      position: ['']
    });

    this.getAll();
  }

  getAll() {
    this.http
    .get<any[]>(this.api)
      .subscribe(data => this.employees = data);
  }

  add() {
    this.http
    .post(this.api, this.form.value)
      .subscribe(() => {
        this.getAll();
        this.form.reset();
      });
  }

  edit(employee: any) {
    this.editingId = employee.id;
    this.form.setValue({
      name: employee.name,
      position: employee.position
    });
  }

  update() {
    this.http.put(`${this.api}/${this.editingId}`, this.form.value)
      .subscribe(() => {
        this.getAll();
        this.form.reset();
      });
  }

  remove(id: number) {
    this.http
    .delete(`${this.api}/${id}`)
      .subscribe(() => this.getAll());
  }
}
