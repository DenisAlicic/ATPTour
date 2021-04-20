import { UserService } from '../../services/user.service';
import { minPasswordLength, maxPasswordLength, minUsernameLength, maxUsernameLength } from '../../constants';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  loginForm: FormGroup;
  isError$ = new BehaviorSubject(false);
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
    console.log(this.loginForm.value);
    
    this.userService.login(this.loginForm.value)
      .subscribe(
        data => {
          this.returnUrl = '/players';
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.isError$.next(true);
          setTimeout(() => {
            this.isError$.next(false);
          }, 3000);
        });  
  }
}
