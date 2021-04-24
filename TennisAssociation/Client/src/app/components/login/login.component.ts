import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { maxPasswordLength, maxUsernameLength, minPasswordLength, minUsernameLength } from 'src/app/shared/constants';
import { Pages } from 'src/app/shared/pages';
@Component({
  selector: 'app-home',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  Pages = Pages;

  loginForm: FormGroup;
  isError$ = new BehaviorSubject(false);
  minPasswordLength = minPasswordLength;
  returnUrl: string;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { 
    this.loginForm = this.formBuilder.group({
      username: [null, [Validators.required, Validators.minLength(minUsernameLength), Validators.maxLength(maxUsernameLength)]],
      password: [null, [Validators.required, Validators.minLength(minPasswordLength), Validators.maxLength(maxPasswordLength)]]
    });
  }
  
  ngOnInit() {
    this.authService.logout();
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.authService.login(this.loginForm.value)
      .subscribe(
        data => {
          console.log(data);
          this.router.navigate(['/', Pages.Players]);
        },
        error => {
          this.isError$.next(true);
          setTimeout(() => {
            this.isError$.next(false);
          }, 3000);
        });  
  }
}
