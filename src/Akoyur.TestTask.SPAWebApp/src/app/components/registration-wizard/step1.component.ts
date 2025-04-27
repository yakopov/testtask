import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError, debounceTime, distinctUntilChanged, filter, of, Subscription, switchMap, tap } from 'rxjs';
import { CheckEmailResponse } from '../../models/user.model';
import { ApiService } from '../../services/api.service';
import { RegistrationStateService } from '../../services/registration-state.service';

@Component({
  standalone: false,
  selector: 'app-step1',
  templateUrl: './step1.component.html',
  styleUrls: ['./step1.component.css']
})
export class Step1Component implements OnInit {
  @Input() fieldErrors: Record<string, string> = {};
  @Output() next = new EventEmitter<any>();
  form: FormGroup;
  emailTaken: boolean = false;
  isCheckingEmail: boolean = false;
  private subscription!: Subscription;

  constructor(private fb: FormBuilder, private registrationStateService: RegistrationStateService) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email, Validators.pattern(/^.{3,400}$/)]],
      password: ['', [Validators.required, Validators.pattern(/^.{1,30}$/)]],
      confirmPassword: ['', Validators.required],
      termsAccepted: [false, Validators.requiredTrue]
    }, { validators: this.passwordsMatch });
  }

  ngOnInit(): void {
    var data = this.registrationStateService.formData;
    if (data) {
      this.form.patchValue({
        email: data.email,
        password: data.password,
        confirmPassword: data.password,
        termsAccepted: data.termsAccepted
      });
    }

    this.subscription = this.form.get('email')!.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      filter(() => !!this.form.get('email')?.valid),
      tap(() => this.isCheckingEmail = true),
      switchMap(email =>
        ApiService.User.checkEmail(email).pipe(
          catchError(() => of({ canBeRegistered: false } as CheckEmailResponse))
        )
      ),
      tap(() => this.isCheckingEmail = false)
    ).subscribe(response => {
      this.emailTaken = !response || !response.canBeRegistered;
      if (this.emailTaken) {
        this.form.get('email')?.setErrors({ emailTaken: true });
      } else {
        const errors = this.form.get('email')?.errors;
        if (errors) {
          delete errors['emailTaken'];
          if (Object.keys(errors).length === 0) {
            this.form.get('email')?.setErrors(null);
          } else {
            this.form.get('email')?.setErrors(errors);
          }
        }
      }
    });
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }

  passwordsMatch(group: FormGroup) {
    return group.get('password')?.value === group.get('confirmPassword')?.value
      ? null
      : { notMatching: true };
  }

  onNext() {
    if (this.form.valid && !this.emailTaken && !this.isCheckingEmail) {
      var data = this.registrationStateService.formData;
      const { email, password } = this.form.value;
      data.email = email;
      data.password = password;
      data.termsAccepted = true;
      this.registrationStateService.setFormData(data);
      this.next.emit();
    } else {
      this.form.markAllAsTouched();
    }
  }
}
