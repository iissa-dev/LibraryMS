import { useForm } from "react-hook-form";
import type { RegisterDto } from "../../../types/index";

type FormValue = RegisterDto;
const useRegisterAccountForm = () => {
  const { register, handleSubmit, reset } = useForm<FormValue>({
    defaultValues: {
      userName: "",
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      address: "",
    },
  });

  return { register, handleSubmit, reset };
};

export default useRegisterAccountForm;
