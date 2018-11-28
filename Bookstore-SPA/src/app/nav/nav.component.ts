import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  signIn() {
    this.authService.signIn(this.model).subscribe(next => {
      console.log('Sign In Successfully');
    }, error => {
      console.log('Failed to sign In');
    });
  }

}
