import { UserService } from '../../services/user.service';
import { minPasswordLength, maxPasswordLength, minUsernameLength, maxUsernameLength } from '../../constants';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  public loginForm: FormGroup;
  public isError: boolean;
  minPasswordLength = minPasswordLength;
  returnUrl: string;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router
  ) { 
    this.loginForm = this.formBuilder.group({
      username: [null, [Validators.required, Validators.minLength(minUsernameLength), Validators.maxLength(maxUsernameLength)]],
      password: [null, [Validators.required, Validators.minLength(minPasswordLength), Validators.maxLength(maxPasswordLength)]]
    });
  }
  
  ngOnInit() {
    this.userService.logout();
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.userService.login(this.loginForm.value)
      .subscribe(
        dat => {
          this.returnUrl = '/users/' + this.f.username.value;
          this.router.navigate([this.returnUrl]);
        },
        error => {
          console.log('2 ' + error.status);
          this.isError = true;
          console.log('error is ', error);
          setTimeout(() => {
            this.isError = false;
          }, 3000);
        });  
  }


}
