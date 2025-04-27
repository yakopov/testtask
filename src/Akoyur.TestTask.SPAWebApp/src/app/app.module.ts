import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { API_BASE_URL } from './api.token';
import { AppComponent } from './app.component';
import { RegistrationWizardComponent } from './components/registration-wizard/registration-wizard.component';
import { Step1Component } from './components/registration-wizard/step1.component';
import { Step2Component } from './components/registration-wizard/step2.component';
import { Step3Component } from './components/registration-wizard/step3.component';

@NgModule({
  declarations: [
    AppComponent,
    RegistrationWizardComponent,
    Step1Component,
    Step2Component,
    Step3Component
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    { provide: API_BASE_URL, useValue: 'https://localhost:7191/api/v1' }
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }

