// advisors.service.ts
import { Inject, Injectable, InjectionToken } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class AdvisorsService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject(API_BASE_URL) baseUrl?: string) {
    this.baseUrl = baseUrl ?? "";
  }

  getAdvisorById(id: number): Observable<AdvisorDto> {
    const url = `${this.baseUrl}/api/v1/Advisors/GetById/${id}`;
    return this.http.get<any>(url).pipe(map(event => {
      if (!event.success) {
        console.error("getAdvisorById", event)
      }
      return event.result;
    }));
  }

  getAllAdvisors(filter: string | undefined, page: number = 0, pageSize: number = 10): Observable<AdvisorDtoPagedResult> {
    const url = `${this.baseUrl}/api/v1/Advisors/GetAll/${page}/${pageSize}?filter=${filter}`;
    return this.http.get<any>(url).pipe(map(event => {
      if (!event.success) {
        console.error("getAllAdvisors", event)
      }
      return event.result;
    }));
  }

  updateAdvisor(id: number, body: UpdateAdvisorDto | undefined): Observable<void> {
    const url = `${this.baseUrl}/api/v1/Advisors/Update/${id}`;
    return this.http.put<any>(url, body).pipe(map(event => {
      if (!event.success) {
        console.error("updateAdvisor", event)
      }
      return event.result;
    }));
  }


  deleteAdvisor(id: number): Observable<void> {
    const url = `${this.baseUrl}/api/v1/Advisors/Delete/${id}`;
    return this.http.delete<any>(url).pipe(map(event => {
      if (!event.success) {
        console.error("updateAdvisor", event)
      }
      return event.result;
    }));
  }
}


export interface AdvisorDtoPagedResult {
  items: AdvisorDto[] | null;
  totalItems: number;
  pageNumber: number;
  pageSize: number;
}
export interface AdvisorDto {
  id: number;
  fullName: string | null;
  sin: string | null;
  phoneNumber: string | null;
  address: string | null;
  healthStatus: number;
}
export interface UpdateAdvisorDto {
  fullName: string;
  address: string | null;
}