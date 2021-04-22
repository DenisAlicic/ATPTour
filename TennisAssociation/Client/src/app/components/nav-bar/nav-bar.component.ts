import { Router } from '@angular/router';
import { Pages } from './../../shared/pages';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  Pages = Pages;
  isLogged$ = new BehaviorSubject(false);
 
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    if(this.authService.isLogged()) {
      this.isLogged$.next(true);
    }
  } 

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/', Pages.Login]);
  }

  addMobileTheme() {
    var topnav = document.getElementById("myTopnav");
    if (topnav.className === "topnav") {
      topnav.className += " responsive";
    } else {
      topnav.className = "topnav";
    }
  }
} 