import { HttpClient, HttpParams } from "@angular/common/http";
import { PaginatedResult } from "../_models/Pagination"
import { map } from "rxjs";

export function getPaginatedResult<T>(url: string, params, http: HttpClient){
    const paginatedResult = new PaginatedResult<T>();
    return http.get<T>(url, 
        {observe: 'response', params}).pipe(
        map(response => {
            paginatedResult.result = response.body;
            var headers = response.headers.get('Pagination')
            if(headers !== null)
                paginatedResult.pagination = JSON.parse(headers)
            return paginatedResult;
        })
    )
}

export function getPaginationHeaders(pageNumber: number, pageSize: number){
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString())
    params = params.append('pageSize', pageSize.toString());
    return params;
}