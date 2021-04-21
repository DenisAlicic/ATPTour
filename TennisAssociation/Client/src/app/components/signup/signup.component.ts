import { UserService } from '../../services/user.service';
import { AlertService } from '../../services/alert.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { passwordsMustMatch } from './password.validator';
import { Router } from '@angular/router';
import { alphaPattern, maxNameLength, maxPasswordLength, minPasswordLength } from 'src/app/shared/constants';
import { Pages } from 'src/app/shared/pages';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent {
  Pages = Pages;
  
  signupForm: FormGroup;
  minPasswordLength = minPasswordLength;

  constructor(
    private formBuilder: FormBuilder, 
    private alertService: AlertService,
    private userService: UserService,
    private router: Router
  ) { 
    this.signupForm = this.formBuilder.group({
      firstname: [null, [Validators.required, Validators.pattern(alphaPattern), Validators.maxLength(maxNameLength)]],
      lastname: [null, [Validators.required, Validators.pattern(alphaPattern), Validators.maxLength(maxNameLength)]],
      username: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(minPasswordLength), Validators.maxLength(maxPasswordLength)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(minPasswordLength), Validators.maxLength(maxPasswordLength)]],
    }, {
      validator: passwordsMustMatch('password', 'confirmPassword')
    }
    );
  }

  get f() { return this.signupForm.controls; }

  onSubmit() {
    
    this.userService.signup(this.signupForm.value)
      .subscribe(
        _ => {
          this.alertService.success('Registration successful', true);
          this.router.navigate(['/']);
        },
        error => {
          this.alertService.error(error);
      });
  }

}
