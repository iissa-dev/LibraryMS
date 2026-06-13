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
  isbn: string;
  publishDate: number;
  genre: number;
  additionalDetails?: string;
  bookImageUrl: string;
}

export interface ResponseBookDto extends BaseBookDto {
  id: number;
  authors: AuthorResponseDto[];
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
  IsFailure: boolean;
  error?: string;
}

export interface TokenResult {
  useId: number;
  accessToken: string;
  userName: string;
  role: "Admin" | "Employee" | "Client";
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
  errors: Record<string, string[]>;
}

// ======== Pagination Params ========
export type PaginationParams<T = {}> = {
  pageNumber: number;
  pageSize: number;
} & T; // to add more params
