// ======== AuthorDto DTOs ========
export interface AuthorResponseDto {
  id: number;
  fullName: string;
}

export interface AuthorDto {
  id: number;
  firstName: string;
  lastName: string;
  biography: string;
}

// ======== Book DTOs ========
export interface BaseBookDto {
  title: string;
  isbn?: string;
  publishDate: number;
  genre: number;
  additionalDetails?: string;
  bookImageUrl: string;
}

export interface ResponseBookDto extends BaseBookDto {
  id: number;
  authors: AuthorResponseDto[];
  isDeleted: boolean;
}

export interface CreateBookDto extends BaseBookDto {
  initalCopies: number;
  authorIds: number[];
}

export interface UpdateBookDto extends BaseBookDto {
  id: number;
}

// ======== Result DTOs ========
export interface Result<T = void> {
  data?: T;
  isSuccess: boolean;
  isFailure: boolean;
  error?: string;
}

export interface TokenResult {
  userId: number;
  accessToken: string;
  userName: string;
  role: "Admin" | "Employee" | "Client";
  clientId?: number;
  personId: number;
}

export interface PagedResult<T> {
  items: T;
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface ProblemDetails {
  title: string;
  detail: string;
  status: string;
  errors?: Record<string, string[]>;
}

// ======== Pagination Params ========
export type PaginationParams<T = {}> = {
  pageNumber: number;
  pageSize: number;
} & T; // to add more params

// ======== Login ========
export interface LoginDto {
  userName: string;
  password: string;
}

// ======== Register Dto ========

export interface RegisterDto {
  email: string;
  userName: string;
  password: string;
  phoneNumber?: string;
  address: string;
  firstName: string;
  lastName: string;
  birthDate: string;
  imageUrl: string;
  countryId: number;
}

// ======== BookCopies Dto ========
export interface ResponseBookCopiesDto {
  bookCopyId: number;
  bookId: number;
  title: string;
  isbn: string;
  serialNumber: string;
  status: string;
}
export interface BookSummaryDto {
  bookId: number;
  title: string;
  author: string[];
}
// ======== Client Dto ========
export interface ClientResponseDto {
  clientId: number;
  firstName: string;
  lastName: string;
  address: string;
  libraryCardNumber: string;
  createdOn: string;
  country: string;
}
export interface ClientSummaryDto {
  clientId: number;
  clientName: string;
  libraryCardNumber: string;
}
// ======== Borrow Dto ========
export interface BorrowDetails {
  borrowId: number;
  book: BookSummaryDto;
  borrower: ClientSummaryDto;
  borrowDate: Date;
  copyId: number;
  dueDate: Date;
  returnDate?: Date;
  status: string;
  fineAmount: number;
}

// ======== Reservation Dto ========
export interface ClientReservationDto {
  reservationId: number;
  bookId: number;
  bookTitle: string;
  reservationDate: Date;
  statusName: string;
  bookCopyId: number;
  queuePosition: number;
}

// ======== Fines Dto ========

export interface FineDetailes {
  fineId: number;
  borrowingDate: Date;
  returnDate: Date;
  paymentStatus: string;
  reason: string;
  fineAmount: number;
  borrower: ClientSummaryDto;
}
