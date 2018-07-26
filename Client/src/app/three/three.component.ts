import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-three',
  templateUrl: './three.component.html',
  styleUrls: ['./three.component.css']
})
export class ThreeComponent implements OnInit {
  active = 0;
  loginModel: any = {};
  registerModel: any = {};
  username = '';
  loginFailed = false;
  userExists = false;
  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  checkLogin() {
    const token = localStorage.getItem('token');
    if (token) {
      this.username = jwt_decode(token)['unique_name'];
    }
    return token !== null;
  }

  setActive(index: number) {
    this.active = index;
  }

  login() {
    this.http.post('http://localhost:5000/api/user/token', this.loginModel)
      .subscribe(response => {
        localStorage.setItem('token', response['token']);
        this.setActive(2);
        this.loginFailed = false;
      }, error => {
        this.loginFailed = true;
      });
  }

  register() {
    this.http.post('http://localhost:5000/api/user/register', this.registerModel)
      .subscribe(response => {
        this.setActive(0);
        this.userExists = false;
      }, error => {
        this.userExists = true;
      });
  }

  logout() {
    localStorage.removeItem('token');
  }
}
