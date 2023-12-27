export interface Pagination{
    itemsPerPage: number;
    currentPage: number;
    totalPages: number;
    totalItems: number;
    income: number;
    expense: number;
}

export class PaginatedResult<T>{
    result: T;
    pagination: Pagination
}
