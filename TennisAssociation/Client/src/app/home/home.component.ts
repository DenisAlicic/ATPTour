import { minPasswordLength, maxPasswordLength } from './../constants';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  public loginForm: FormGroup;
  public isError: boolean;
  minPasswordLength = minPasswordLength;
  
  constructor(private formBuilder: FormBuilder) { 
    this.loginForm = this.formBuilder.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(minPasswordLength), Validators.maxLength(maxPasswordLength)]]
    });
  }
  
  get f() { return this.loginForm.controls; }

  onSubmit() {
    console.log(this.loginForm.value);
  }
}
