import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Room, CreateRoomRequest, UpdateRoomRequest, RoomListResponse } from '../models/room.models';
import { API_CONFIG } from '../config/api.config';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private apiUrl = API_CONFIG.baseUrl;

  constructor(private http: HttpClient) {}

  // Lấy tất cả phòng
  getAllRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(`${this.apiUrl}/rooms`);
  }

  // Lấy phòng theo trạng thái
  getRoomsByStatus(): Observable<RoomListResponse> {
    return this.http.get<RoomListResponse>(`${this.apiUrl}/rooms/by-status`);
  }

  // Lấy phòng trống
  getAvailableRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(`${this.apiUrl}/rooms/available`);
  }

  // Lấy phòng đã đặt
  getBookedRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(`${this.apiUrl}/rooms/booked`);
  }

  // Lấy phòng theo ID
  getRoomById(id: number): Observable<Room> {
    return this.http.get<Room>(`${this.apiUrl}/rooms/${id}`);
  }

  // Tạo phòng mới
  createRoom(room: CreateRoomRequest): Observable<Room> {
    return this.http.post<Room>(`${this.apiUrl}/rooms`, room);
  }

  // Cập nhật phòng
  updateRoom(id: number, room: UpdateRoomRequest): Observable<Room> {
    return this.http.put<Room>(`${this.apiUrl}/rooms/${id}`, room);
  }

  // Xóa phòng
  deleteRoom(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/rooms/${id}`);
  }
}

