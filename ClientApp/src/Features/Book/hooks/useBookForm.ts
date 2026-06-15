import { useForm } from "react-hook-form";
import type { AuthorResponseDto, ResponseBookDto } from "../../../types";
import { useEffect } from "react";
import dayjs from "dayjs";
import { GenreMapping } from "../../../constants/Genre";

type FormValue = {
  id?: number;
  title: string;
  isbn: string;
  initalCopies?: number;
  genre: number;
  authors: AuthorResponseDto[];
  publishDate: number;
  additionalDetails?: string;
  bookImageUrl?: string;
};

type Params = {
  isOpen: boolean;
  mode: "Add" | "Edit" | "View";
  data?: ResponseBookDto;
};

const initialDefaultValues: FormValue = {
  title: "",
  isbn: "",
  initalCopies: 0,
  genre: 1,
  authors: [],
  publishDate: Number(new Date(dayjs().year())),
  additionalDetails: "",
  bookImageUrl: "",
};

export const useBookForm = ({ isOpen, mode, data }: Params) => {
  const { register, handleSubmit, reset, control } = useForm<FormValue>({
    defaultValues: initialDefaultValues,
  });

  useEffect(() => {
    if (!isOpen) return;

    if ((mode === "Edit" || mode === "View") && data) {
      reset({
        ...data,
        initalCopies: undefined,
        genre: GenreMapping(data?.genre as unknown as string),
      });
    } else {
      // Add
      reset({
        ...initialDefaultValues,
      });
    }
  }, [isOpen, mode, reset]);

  return { register, handleSubmit, control, reset };
};
