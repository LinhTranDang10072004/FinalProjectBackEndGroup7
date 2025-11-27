export interface Room {
  roomID: number;
  roomNumber: string;
  floor: number;
  status: string; // "Available", "Booked", "Occupied", "Maintenance"
  isActive: boolean;
  roomTypeID: number;
  roomTypeName?: string;
  basePrice?: number;
}

export interface CreateRoomRequest {
  roomNumber: string;
  floor: number;
  roomTypeID: number;
  isActive?: boolean; // default: true
}

export interface UpdateRoomRequest {
  roomNumber?: string;
  floor?: number;
  status?: string;
  roomTypeID?: number;
  isActive?: boolean;
}

export interface RoomListResponse {
  availableRooms: Room[];
  bookedRooms: Room[];
  totalAvailable: number;
  totalBooked: number;
}


