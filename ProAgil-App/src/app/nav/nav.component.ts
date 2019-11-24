import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_Services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(
    public authService: AuthService,
    private toastr: ToastrService,
    public router: Router
  ) { }

  ngOnInit() {
    this.LoggedIn();
  }
  LoggedIn(){


    return this.authService.loggedIn();;
  }
  logout(){
    localStorage.removeItem('Token');
    this.toastr.show('Log Out');
    this.router.navigate(['/user/login']);
  }

  entrar(){
    this.router.navigate(['/user/login']);
  }

  userName(){
    return sessionStorage.getItem('username');
  }
}
