import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RevenueReport, RevenueRequest } from '../models/revenue.models';
import { API_CONFIG } from '../config/api.config';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private apiUrl = API_CONFIG.baseUrl;

  constructor(private http: HttpClient) {}

  // Doanh thu theo tháng
  getRevenueByMonth(year: number, month: number): Observable<RevenueReport> {
    return this.http.get<RevenueReport>(
      `${this.apiUrl}/reports/revenue/month/${year}/${month}`
    );
  }

  // Doanh thu theo quý
  getRevenueByQuarter(year: number, quarter: number): Observable<RevenueReport> {
    return this.http.get<RevenueReport>(
      `${this.apiUrl}/reports/revenue/quarter/${year}/${quarter}`
    );
  }

  // Doanh thu theo năm
  getRevenueByYear(year: number): Observable<RevenueReport> {
    return this.http.get<RevenueReport>(
      `${this.apiUrl}/reports/revenue/year/${year}`
    );
  }

  // Doanh thu theo nhiều kỳ
  getRevenueByPeriod(request: RevenueRequest): Observable<RevenueReport[]> {
    return this.http.post<RevenueReport[]>(
      `${this.apiUrl}/reports/revenue`,
      request
    );
  }
}

