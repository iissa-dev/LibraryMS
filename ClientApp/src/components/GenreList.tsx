import { forwardRef } from "react";
import { GENRES } from "../constants/Genre";

type Params = {
  id?: string;
  readonly?: boolean;
  name?: string;
  value?: string | number;
  defaultValue?: string | number;
  onChange?: (e: React.ChangeEvent<HTMLSelectElement>) => void;
  onBlur?: (e: React.FocusEvent<HTMLSelectElement>) => void;
};

const GenreList = forwardRef<HTMLSelectElement, Params>(
  ({ id, readonly, value, defaultValue, ...rest }, ref) => {
    return (
      <div>
        <select
          className="search-input w-full"
          id={id || "category-filter"}
          ref={ref}
          disabled={readonly}
          {...rest}
          value={value}
          defaultValue={defaultValue}
        >
          <option value={""}>All Genres</option>
          {GENRES.map((genre) => (
            <option key={genre.value} value={genre.value}>
              {genre.name}
            </option>
          ))}
        </select>
      </div>
    );
  },
);

export default GenreList;
