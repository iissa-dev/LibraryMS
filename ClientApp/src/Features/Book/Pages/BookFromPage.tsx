import { ArrowBigLeft, UploadCloud, X } from "lucide-react";
import MainPageTitle from "../../../components/MainPageTitle";
import { useBookForm } from "../hooks/useBookForm";
import { useLocation, useParams } from "react-router-dom";
import { useEffect, useRef, useState } from "react";
import type { AuthorResponseDto, ResponseBookDto } from "../../../types";
import { useNavigate } from "react-router-dom";
import { INPUTS } from "../constants/bookInputs.constant";
import MultiAuthorSelect from "../components/MultiAuthorSelectProps";
import { Controller } from "react-hook-form";
import GenreList from "../../../components/GenreList";
import {
  useAddBook,
  useGetBookById,
  useUpdateBook,
} from "../hooks/book.mutation";
import { API_BASE_URL } from "../../../api/apiClient";
import { GenreMapping } from "../../../constants/Genre";

const BookFromPage = ({ readOnly = false }) => {
  const { bookId } = useParams<{ bookId: string }>();
  const location = useLocation();
  const navigation = useNavigate();
  const [isOpen, setIsOpen] = useState(true);

  let isUpdateMode = Boolean(bookId) && !readOnly;
  let isViewMode = Boolean(bookId) && readOnly;
  const mode = isUpdateMode ? "Edit" : isViewMode ? "View" : "Add";

  const handleClose = () => {
    setIsOpen(false);
    navigation("/bookManagement");
  };

  const addMutation = useAddBook(handleClose);
  const updateMutation = useUpdateBook(Number(bookId), handleClose);

  const needsFetch = (mode === "Edit" || mode === "View") && !location.state;

  const {
    data: fetchedData,
    isLoading,
    error,
  } = useGetBookById(Number(bookId), {
    enabled: needsFetch,
    retry: false,
  });

  const data: ResponseBookDto =
    location.state?.currentBook ?? fetchedData ?? {};

  const { register, control, handleSubmit, reset } = useBookForm({
    isOpen,
    mode,
    data,
  });

  const fileInputRef = useRef<HTMLInputElement>(null);
  const [imagePreview, setImagePreview] = useState<string | null>(
    isUpdateMode || isViewMode ? data.bookImageUrl : null,
  );

  useEffect(() => {
    if (needsFetch && !isLoading) {
      if (error || !fetchedData || Object.keys(fetchedData).length === 0) {
        navigation("/404-not-found", { replace: true });
      }
    }
  }, [fetchedData, isLoading, error, needsFetch, navigation]);

  useEffect(() => {
    if (fetchedData) {
      const { genre, ...restOfData } = fetchedData;
      reset({ genre: GenreMapping(genre.toString()), ...restOfData });
      if (fetchedData.bookImageUrl) {
        setImagePreview(fetchedData.bookImageUrl);
      }
    }
  }, [fetchedData, reset]);

  useEffect(() => {
    if (data.bookImageUrl) {
      setImagePreview(data.bookImageUrl);
    }
  }, [data.bookImageUrl]);
  // Handle image logic

  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (readOnly) return;
    const file = e.target.files?.[0];
    if (file) {
      const previewUrl = URL.createObjectURL(file);
      setImagePreview(previewUrl);
    }
  };
  const handleRemoveImage = () => {
    if (readOnly) return;
    setImagePreview(null);
    if (fileInputRef.current) {
      fileInputRef.current.value = ""; // clear the input
    }
  };

  // Submit logic
  const onSubmit = (formData: any) => {
    if (readOnly) return;

    if (mode === "Edit") {
      const { authors, genre, initalCopies, ...restOfFormData } = formData;
      const apiData = new FormData();

      apiData.append("id", restOfFormData.id);
      apiData.append("title", restOfFormData.title);
      apiData.append("isbn", restOfFormData.isbn);
      apiData.append("publishDate", restOfFormData.publishDate);
      apiData.append("genre", Number(genre).toString());
      apiData.append("additionalDetails", restOfFormData.additionalDetails);
      const authorIds = (authors || []).map(
        (author: AuthorResponseDto) => author.id,
      );
      authorIds.forEach((id: number) =>
        apiData.append("authorIds", id.toString()),
      );

      if (fileInputRef.current?.files?.[0]) {
        apiData.append("bookImageUrl", fileInputRef.current?.files?.[0]);
      } else if (!imagePreview) apiData.append("bookImageUrl", "");

      updateMutation.mutate(apiData);
    } else if (mode === "Add") {
      const { authors, genre, initalCopies, ...restOfFormData } = formData;
      const apiData = new FormData();

      apiData.append("Title", restOfFormData.title);
      apiData.append("PublishDate", restOfFormData.publishDate);
      apiData.append("InitialCopiesCount", initalCopies);
      apiData.append("Genre", Number(genre).toString());
      apiData.append("AdditionalDetails", restOfFormData.additionalDetails);
      const authorIds = (authors || []).map(
        (author: AuthorResponseDto) => author.id,
      );
      authorIds.forEach((id: number) =>
        apiData.append("AuthorIds", id.toString()),
      );

      if (fileInputRef.current?.files?.[0]) {
        apiData.append("BookImageUrl", fileInputRef.current?.files?.[0]);
      }

      addMutation.mutate(apiData);
    }
  };

  const isBlobUrl = imagePreview?.startsWith("blob:");
  const finalImageSrc = isBlobUrl
    ? imagePreview
    : `${API_BASE_URL}${imagePreview}`;

  if (needsFetch && isLoading) {
    return (
      <div className="flex justify-center items-center h-64 text-text-secondary font-bold">
        Loading Book Details...
      </div>
    );
  }
  return (
    <div>
      <span
        onClick={handleClose}
        className="flex gap-2 text-primary text-[12px] items-center font-bold cursor-pointer w-fit"
      >
        <ArrowBigLeft className="font-bold" size={15} /> Back to Books List
      </span>
      <MainPageTitle
        Title={
          isUpdateMode
            ? "Update Book Details"
            : !isViewMode
              ? "Create New Book"
              : "View Book Detail"
        }
        Description={
          isUpdateMode
            ? "Modify the bibliographic details of the selected volume."
            : "Enter the bibliographic details to register a new volume in the central repository."
        }
      />
      {/* form  */}
      <main className="main-card mt-6">
        <form
          onSubmit={handleSubmit(onSubmit)}
          className="grid grid-cols-1 md:grid-cols-2 gap-4"
        >
          {/* image picker */}

          <div className="col-span-2 md:col-span-1 flex flex-col items-center">
            <label className="text-[12px] text-text-secondary font-bold mb-2 self-start ml-2">
              Book Cover Image
            </label>

            <div
              onClick={() => {
                if (isViewMode) return;
                !imagePreview && fileInputRef.current?.click();
              }}
              className={`w-full h-64 border-2 border-dashed rounded-card flex flex-col items-center justify-center relative overflow-hidden transition-all duration-300 group
                ${
                  imagePreview
                    ? "border-border"
                    : "border-border/60 hover:border-primary/50 bg-text/5 hover:bg-text/10 cursor-pointer"
                }`}
            >
              <input
                {...register("bookImageUrl", { required: false })}
                type="file"
                id="book-image"
                ref={fileInputRef}
                onChange={handleImageChange}
                accept="image/*"
                className="hidden"
                readOnly={readOnly}
              />

              {/* Show preview image */}
              {imagePreview ? (
                <>
                  <img
                    src={finalImageSrc || ""}
                    alt="Book Cover Image"
                    className="w-full h-full object-cover"
                  />
                  {/* Remvoe Image Button */}

                  <button
                    type="button"
                    onClick={(e) => {
                      e.preventDefault();
                      handleRemoveImage();
                    }}
                    title="Remove Image"
                    className="absolute top-2 right-2 p-1.5 bg-neutral/80 hover:bg-red/20 text-text hover:text-red rounded-full transition-all cursor-pointer shadow-md"
                  >
                    <X size={14} />
                  </button>
                </>
              ) : (
                // No Image
                <div className="flex flex-col items-center text-center p-4">
                  <UploadCloud
                    size={32}
                    className="text-text-secondary group-hover:text-primary transition-colors mb-2"
                  />
                  <p className="text-sm font-medium text-text/80">
                    Click to upload
                  </p>
                  <p className="text-[11px] text-text-secondary/60 mt-1">
                    PNG, JPG or WEBP
                  </p>
                </div>
              )}
            </div>
          </div>

          {/* Inputs */}
          {INPUTS &&
            INPUTS.map((input, i) => {
              if (mode !== "Add" && input.showOnlyInAddMode) return;
              return (
                <div key={`input.id-${i}`} className={`${input.gridOrder}`}>
                  <label
                    className="text-[12px] text-text-secondary font-bold mb-2 ml-2"
                    htmlFor={input.id}
                  >
                    {input.label}
                  </label>
                  <input
                    {...register(input.name)}
                    type="text"
                    placeholder={input.placeholder}
                    id={input.id}
                    className="search-input"
                    readOnly={readOnly}
                  />
                </div>
              );
            })}
          {/* Genre Select */}
          <div className="col-span-1 h-fit">
            <label
              className="text-[12px] text-text-secondary font-bold mb-2 ml-2"
              htmlFor="genre"
            >
              Genre
            </label>
            <GenreList
              readonly={readOnly}
              id={"genre"}
              {...register("genre", {
                required: true,
              })}
            />
          </div>
          <div className="col-span-2">
            <textarea
              readOnly={readOnly}
              {...register("additionalDetails", { required: false })}
              className="search-input"
              placeholder="Additional Detail (Optional)"
              id="additional-detail"
            ></textarea>
          </div>
          <div className="col-span-2">
            <Controller
              name="authors"
              control={control}
              render={({ field: { value, onChange } }) => (
                <MultiAuthorSelect
                  selectedAuthors={value || []}
                  setSelectedAuthors={(newAuthors) => onChange(newAuthors)}
                  disabled={readOnly}
                />
              )}
            />
          </div>
          {!isViewMode && (
            <div className="col-span-2 flex justify-end gap-2">
              <button
                type="button"
                className="secondary-button"
                onClick={handleClose}
              >
                Cancel
              </button>
              <button type="submit" className="main-button">
                Save Book
              </button>
            </div>
          )}
        </form>
      </main>
    </div>
  );
};

export default BookFromPage;
