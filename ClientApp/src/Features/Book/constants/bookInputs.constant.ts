export const INPUTS = [
  {
    id: "book-title",
    label: "Book Title",
    placeholder: "e.g. The Architecure of information",
    name: "title",
    gridOrder: "col-span-2",
    showOnlyInAddMode: false,
  },
  {
    id: "isbn-number",
    label: "ISBN Number",
    placeholder: "971-x-xxxx-xxxx-x",
    name: "isbn",
    gridOrder: "md:col-span-1 col-span-2",
    showOnlyInAddMode: false,
  },
  {
    id: "inital-copy",
    label: "Total Copies",
    placeholder: "1",
    name: "initalCopies",
    gridOrder: "md:col-span-1 col-span-2",
    showOnlyInAddMode: true,
  },
  {
    id: "publication-year",
    label: "Publication Year",
    placeholder: "YYYY",
    name: "publishDate",
    gridOrder: "md:col-span-1 col-span-2",
    showOnlyInAddMode: false,
  },
] as const;
