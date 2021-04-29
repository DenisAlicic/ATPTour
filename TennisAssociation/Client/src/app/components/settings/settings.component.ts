import { UpdateService } from './../../services/update.service';
import { minPasswordLength, maxPasswordLength } from './../../shared/constants';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { BehaviorSubject } from 'rxjs';
import { Component, OnInit } from "@angular/core";
import { passwordsMustMatch } from '../signup/password.validator';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  isAdmin$ = new BehaviorSubject(false);

  changePassForm: FormGroup;
  minPasswordLength = minPasswordLength;

  isError$ = new BehaviorSubject(false);
  isPasswordChangedSuccessfully$ = new BehaviorSubject(false);
  isUpdateSuccessfull$ = new BehaviorSubject(false);

  constructor(
    private formBuilder: FormBuilder, 
    private authService: AuthService,
    private updateService: UpdateService
  ) {
    this.changePassForm = this.formBuilder.group({
      password: [null, [Validators.required, Validators.minLength(minPasswordLength), Validators.maxLength(maxPasswordLength)]],
      newPassword: [null, [Validators.required, Validators.minLength(minPasswordLength), Validators.maxLength(maxPasswordLength)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(minPasswordLength), Validators.maxLength(maxPasswordLength)]],
    }, {
      validator: passwordsMustMatch('newPassword', 'confirmPassword')
    }
    );
  }

  ngOnInit() {
    this.isAdmin$.next(this.authService.isAdmin());
  }

  get f() { return this.changePassForm.controls; }

  onError() {
    this.isError$.next(true);
    setTimeout(() => {
      this.isError$.next(false);
    }, 3000);
  }

  onUpdateSuccess() {
    this.isUpdateSuccessfull$.next(true);
    setTimeout(() => {
      this.isUpdateSuccessfull$.next(false);
    }, 2000);
  }

  onChangePassword() {
    this.authService.changePassword(this.changePassForm.value)
      .subscribe(
        _ => {
          this.isPasswordChangedSuccessfully$.next(true);
          setTimeout(() => {
            this.isPasswordChangedSuccessfully$.next(false);
          }, 3000);
          this.changePassForm.reset();
        },
        _ => {
          this.onError();
          this.changePassForm.reset();
        }
      );
  }

  onUpdatePlayersDb() {
    this.updateService.updatePlayersDb()
      .subscribe(
        _ => this.onUpdateSuccess(),
        _ => this.onError()
      );
  }

  onUpdateTournamentsDb() {
    this.updateService.updateTournamentsDb()
    .subscribe(
      _ => this.onUpdateSuccess(),
      _ => this.onError()
    );
  }

  onUpdateMatchesDb() {
    this.updateService.updateMatchesDb()
    .subscribe(
      _ => this.onUpdateSuccess(),
      _ => this.onError()
    );
  }

}