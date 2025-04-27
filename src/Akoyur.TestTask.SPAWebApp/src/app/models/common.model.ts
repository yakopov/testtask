export interface CollectionResponse<T> {
  items: T[];
  hasMore: boolean;
  cursor: number | undefined;
}
