import { minPasswordLength, alphaPattern, maxNameLength, maxPasswordLength } from './../constants';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Component } from '@angular/core';
import { passwordsMustMatch } from './password.validator';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent {

  public signupForm: FormGroup;
  minPasswordLength = minPasswordLength;

  constructor(private formBuilder: FormBuilder) { 
    this.signupForm = this.formBuilder.group({
      firstname: [null, [Validators.required, Validators.pattern(alphaPattern), Validators.maxLength(maxNameLength)]],
      lastname: [null, [Validators.required, Validators.pattern(alphaPattern), Validators.maxLength(maxNameLength)]],
      birthday: [null, [Validators.required]],
      gender: new FormControl(null, Validators.required),
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
    console.log(this.signupForm.value);
  }

}
