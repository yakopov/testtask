import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Country, Province } from '../models/country.model';

@Injectable({ providedIn: 'root' })

export class RegistrationStateService {
  private countriesSubject = new BehaviorSubject<Country[]>([]);
  private provincesSubject = new BehaviorSubject<Province[]>([]);
  private formDataSubject = new BehaviorSubject<RegistrationWizardData>({});

  countries$ = this.countriesSubject.asObservable();
  provinces$ = this.provincesSubject.asObservable();
  formData$ = this.formDataSubject.asObservable();

  setCountries(countries: Country[]): void {
    this.countriesSubject.next(countries);
  }

  setProvinces(provinces: Province[]): void {
    this.provincesSubject.next(provinces);
  }

  setFormData(formData: RegistrationWizardData): void {
    this.formDataSubject.next(formData);
  }

  get countries(): Country[] {
    return this.countriesSubject.getValue();
  }

  get provinces(): Province[] {
    return this.provincesSubject.getValue();
  }

  get formData(): RegistrationWizardData {
    return this.formDataSubject.getValue();
  }

  clear(): void {
    this.countriesSubject.next([]);
    this.provincesSubject.next([]);
    this.formDataSubject.next({});
  }
}

export interface RegistrationWizardData {
  email?: string;
  password?: string;
  termsAccepted?: boolean;
  countryId?: number;
  provinceId?: number;
}
