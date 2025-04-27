import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { API_BASE_URL } from '../api.token';
import { CollectionResponse } from '../models/common.model';
import { Country, Province } from '../models/country.model';
import { CheckEmailResponse, CreateUserRequest, CreateUserResponse } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class ApiService {
  static http: HttpClient;
  static apiUrl: string;

  constructor(
    private _http: HttpClient,
    @Inject(API_BASE_URL) private _apiUrl: string) {
    ApiService.http = _http;
    ApiService.apiUrl = _apiUrl;
  }

  static User = class {
    static register(data: CreateUserRequest): Observable<CreateUserResponse> {
      return ApiService.http.post<CreateUserResponse>(`${ApiService.apiUrl}/users`, data).pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
    }

    static checkEmail(email: string): Observable<CheckEmailResponse> {
      return ApiService.http.get<CheckEmailResponse>(`${ApiService.apiUrl}/users/check-email?email=${encodeURIComponent(email)}`).pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
    }
  };

  static Country = class {
    static getCountries(): Observable<CollectionResponse<Country>> {
      return ApiService.http.get<CollectionResponse<Country>>(`${ApiService.apiUrl}/countries`).pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
    }

    static getProvinces(countryId: string): Observable<CollectionResponse<Province>> {
      return ApiService.http.get<CollectionResponse<Province>>(`${ApiService.apiUrl}/countries/${countryId}/provinces`).pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
    }
  };
}
