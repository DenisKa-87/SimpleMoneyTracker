export class UserParams {
    CategoryId: number = -1;
    RecordType: number = 0;
    MinDate: Date = new Date(new Date().getFullYear(), 0,1);
    MaxDate: Date = new Date();
    itemsPerPage: number = 20;
    pageNumber: number = 1;
    Order: string;
}