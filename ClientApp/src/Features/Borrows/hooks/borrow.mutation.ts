import useGenericMutation from "../../../hooks/useGenericMutation";
import { borrowService, type borrowType } from "../services/borrowService";

type params = {
  onClose: () => void;
};
export const useBorrow = ({ onClose }: params) => {
  const mutation = useGenericMutation<borrowType, void>(
    (data) => borrowService.borrow(data),
    ["bookCopies"],
    "The loan process was successful.",
    onClose,
  );

  return mutation;
};
