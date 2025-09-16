export interface Position {
  x: number;
  y: number;
  z: number;
}

export interface PositionsResponse {
  positions: Record<string, Position>;
}
