export interface RevenueReport {
  period: string; // "2024-01", "2024-Q1", "2024"
  totalRevenue: number;
  totalBookings: number;
  totalRoomsBooked: number;
  averageRevenuePerBooking: number;
  dailyBreakdown?: DailyRevenue[];
  monthlyBreakdown?: MonthlyRevenue[];
}

export interface DailyRevenue {
  date: string; // ISO date string
  revenue: number;
  bookings: number;
}

export interface MonthlyRevenue {
  month: number;
  monthName: string;
  revenue: number;
  bookings: number;
}

export interface RevenueRequest {
  periodType: 'month' | 'quarter' | 'year';
  year?: number;
  month?: number; // 1-12
  quarter?: number; // 1-4
}


