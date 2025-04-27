import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, of, Subject } from 'rxjs';
import { catchError, filter, finalize, switchMap, take, takeUntil, tap } from 'rxjs/operators';
import { CollectionResponse } from '../../models/common.model';
import { Country, Province } from '../../models/country.model';
import { ApiService } from '../../services/api.service';
import { RegistrationStateService } from '../../services/registration-state.service';

@Component({
  standalone: false,
  selector: 'app-step2',
  templateUrl: './step2.component.html',
  styleUrls: ['./step2.component.css']
})

export class Step2Component implements OnInit {
  @Output() save = new EventEmitter<any>();
  form!: FormGroup;
  private destroy$ = new Subject<void>();
  countries$!: Observable<CollectionResponse<Country>>;
  provinces$!: Observable<CollectionResponse<Province>>;
  isSubmitEnabled = false;
  isCountriesLoading = true;
  isProvincesLoading = false;

  constructor(private fb: FormBuilder, private registrationStateService: RegistrationStateService) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      countryId: ['', Validators.required],
      provinceId: [{ value: '', disabled: true }, Validators.required]
    });

    this.initCountries();
    this.initProvinces();
  }

  initCountries(): void {
    const countryControl = this.form.get('countryId')!;
    const provinceControl = this.form.get('provinceId')!;
    countryControl.disable();

    const cachedCountries = this.registrationStateService.countries;
    if (cachedCountries.length > 0) {
      this.isCountriesLoading = false;
      countryControl.enable();
      this.countries$ = of({ items: cachedCountries } as CollectionResponse<Country>);

      this.countries$
        .pipe(
          filter(response => response.items.length > 0),
          take(1)
        ).subscribe(() => {
          const data = this.registrationStateService.formData;
          if (data && data.countryId) {
            this.form.patchValue({
              countryId: data.countryId
            });
          }
        });
    } else {
      this.isCountriesLoading = true;
      this.countries$ = ApiService.Country.getCountries()
        .pipe(
          tap(response => {
            this.registrationStateService.setCountries(response.items);
            this.isCountriesLoading = false;
            countryControl.enable();
          }),
          catchError(error => {
            this.isCountriesLoading = false;
            alert('Error loading countries. Try refreshing the page.');
            return of({ items: cachedCountries } as CollectionResponse<Country>);
          })
        );
    }

    countryControl.valueChanges.pipe(
      tap(countryId => {
        const data = { ...this.registrationStateService.formData, countryId };
        this.registrationStateService.setFormData(data);
        this.registrationStateService.setProvinces([]);
        if (!countryId) {
          provinceControl.disable();
          provinceControl.reset();
        }
        this.isProvincesLoading = !!countryId;
      }),
      filter(countryId => !!countryId),
      switchMap(countryId => ApiService.Country.getProvinces(countryId)
        .pipe(
          tap(response => {
            this.provinces$ = of(response);
            this.registrationStateService.setProvinces(response.items);
            provinceControl.enable();
          }),
          catchError(() => {
            alert('Error loading provinces. Try refreshing the page.');
            return of({ items: [], hasMore: false, cursor: undefined });
          }),
          finalize(() => {
            this.isProvincesLoading = false;
          })
        )
      ),
      takeUntil(this.destroy$)
    ).subscribe();
  }

  initProvinces(): void {
    const provinceControl = this.form.get('provinceId')!;

    const cachedProvinces = this.registrationStateService.provinces;
    if (cachedProvinces.length > 0) {
      provinceControl.enable();
      this.provinces$ = of({ items: cachedProvinces } as CollectionResponse<Province>)
        .pipe(
          tap(() => {
            var data = this.registrationStateService.formData;
            if (data && data.provinceId) {
              this.form.patchValue({
                provinceId: data.provinceId
              });
            }
          })
        );
      this.isProvincesLoading = false;
    }

    provinceControl.valueChanges
      .pipe(
        tap(provinceId => {
          const data = { ...this.registrationStateService.formData, provinceId };
          this.registrationStateService.setFormData(data);
        }),
        takeUntil(this.destroy$)
      ).subscribe();
  }

  onSave(): void {
    if (this.form.valid) {
      const { countryId, provinceId } = this.form.value;
      const data = { ...this.registrationStateService.formData, countryId, provinceId };
      this.registrationStateService.setFormData(data);
      this.save.emit();
    } else {
      this.form.markAllAsTouched();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
