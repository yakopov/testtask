import { Component } from '@angular/core';
import { CreateUserRequest } from '../../models/user.model';
import { ApiService } from '../../services/api.service';
import { RegistrationStateService } from '../../services/registration-state.service';

@Component({
  standalone: false,
  selector: 'app-registration-wizard',
  templateUrl: './registration-wizard.component.html',
  styleUrls: ['./registration-wizard.component.css']
})

export class RegistrationWizardComponent {
  currentStep = 1;
  fieldErrors: Record<string, string> = {};
  userId: number = 0;

  constructor(private apiService: ApiService, private registrationStateService: RegistrationStateService) { }

  goToStep(step: number) {
    this.currentStep = step;
  }

  onStep1Complete() {
    this.fieldErrors = {};
    this.goToStep(2);
  }

  onStep2Complete() {
    this.toggleLoader(true);
    const data = this.registrationStateService.formData;
    ApiService.User.register({
      email: data.email,
      password: data.password,
      provinceId: data.provinceId
    } as CreateUserRequest).subscribe({
      next: (response) => {
        this.userId = response.userId;
        this.registrationStateService.clear();
        this.goToStep(3);
      },
      error: (response) => {
        if (response.status === 400) {
          this.fieldErrors = response.error.errors;
          this.goToStep(1);
        } else {
          alert('Error: ' + response.error.message);
        }
      }
    });
  }

  goBack() {
    if (this.currentStep > 1) {
      this.currentStep = 1;
    }
  }

  toggleLoader(show: boolean) {
    this.goToStep(show ? 0 : 1);
  }
}
