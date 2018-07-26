import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-conference',
  templateUrl: './conference.component.html',
  styleUrls: ['./conference.component.css']
})
export class ConferenceComponent implements OnInit {
  conferences: any = [];
  active = 0;
  conferenceModel: any = {};
  currentConference: any = null;
  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.fetchConferences();
  }

  createConference() {
    const token = localStorage.getItem('token');
    this.http
      .post('http://localhost:5000/api/conference', this.conferenceModel, {
        headers: new HttpHeaders().set('Authorization', 'Bearer ' + token)
      })
      .subscribe(
        repsonse => {
          this.fetchConferences();
          this.setActive(0);
        },
        error => {
          console.log(error);
        }
      );
  }

  setActive(index: number) {
    this.active = index;
  }

  fetchConferences() {
    this.http.get('http://localhost:5000/api/conference').subscribe(
      response => {
        this.conferences = response;
      },
      error => {
        console.log(error);
      }
    );
  }

  checkLogin() {
    return localStorage.getItem('token') !== null;
  }

  showConference(conference: any) {
    this.http.get('http://localhost:5000/api/conference/' + conference.conferenceId).subscribe(
      response => {
        this.currentConference = response;
      }, error => {
        console.log(error);
      }
    );
  }
}
