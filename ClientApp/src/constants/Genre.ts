// Book Genre it is const
export const GENRES = [
  { name: "Other", value: 0 },
  { name: "ActionAndAdventure", value: 1 },
  { name: "Drama", value: 2 },
  { name: "Fantasy", value: 3 },
  { name: "ScienceFiction", value: 4 },
  { name: "MysteryAndThriller", value: 5 },
  { name: "Romance", value: 6 },
  { name: "Horror", value: 7 },
  { name: "SelfHelp", value: 8 },
  { name: "Biography", value: 9 },
  { name: "History", value: 10 },
  { name: "ScienceAndTech", value: 11 },
  { name: "BusinessAndFinance", value: 12 },
] as const;

export const GenreMapping = (name: string) => {
  return GENRES.find((g) => g.name === name)?.value ?? 0;
};

export const GenreReverseMapping = (value: number) => {
  return GENRES.find((g) => g.value === value)?.name ?? "Other";
};
