export const CopyStatus = [
  { name: "Available", value: 1 },
  { name: "Borrowed", value: 2 },
  { name: "Reserved", value: 3 },
  { name: "InMaintenance", value: 4 },
  { name: "Lost", value: 5 },
  { name: "Damaged", value: 6 },
  { name: "Restricted", value: 7 },
  { name: "Archived", value: 8 },
] as const;

export const getCopyStatusValue = (name: string) => {
  return CopyStatus.find((c) => c.name === name)?.value ?? 1;
};
export const getCopyStatusName = (value: number) => {
  return CopyStatus.find((c) => c.value === value)?.name ?? "Available";
};
