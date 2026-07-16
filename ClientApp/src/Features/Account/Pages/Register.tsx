import dayjs from "dayjs";
import type { RegisterDto } from "../../../types";
import { useRegister } from "../../Client/hooks/client.mutation";
import { INPUTS } from "../constants/inputs.constants";
import useRegisterAccountForm from "../hooks/useRegisterAccountForm";
import { useNavigate } from "react-router-dom";

const Register = () => {
  const { register, handleSubmit } = useRegisterAccountForm();
  const navigate = useNavigate();
  const handleRedirect = () => {
    navigate("/login");
  };
  const registery = useRegister({ method: handleRedirect });
  const onSubmit = (data: RegisterDto) => {
    const finallData = {
      ...data,
      birthDate: dayjs(data.birthDate).format("YYYY-MM-DD"),
      countryId: 100,
    };
    registery.mutate(finallData);
  };
  return (
    <div className="flex min-h-screen justify-center items-center flex-col bg-background m-2 md:m-0 relative">
      <div className="main-card border-t-4 border-t-primary">
        <h2 className="font-bold md:text-2xl mb-1 text-text">Create Reader Account</h2>
        <p className="text-text-secondary text-sm md:text-[16px] text-wrap">
          Register to browse the catalog and manage your loans
        </p>

        <form
          className="mt-5 grid grid-cols-2 gap-4"
          action="POST"
          onSubmit={handleSubmit(onSubmit)}
        >
          {INPUTS.map((input) => (
            <div key={input.id} className={`${input.gridOrder}`}>
              <label
                className="text-[12px] font-bold select-none text-text"
                htmlFor={input.id}
              >
                {input.placeholder}
              </label>
              <input
                {...register(input.name, { required: true })}
                autoComplete="on"
                id={input.id}
                className="search-input"
                type={input.type}
                placeholder={input.placeholder}
              />
            </div>
          ))}
          <div className="col-span-2">
            <input
              className="main-button mt-4 w-full"
              type="submit"
              value="Register Account"
            />
          </div>
          <div className="border-t border-text py-5 col-span-2 flex items-center gap-2 justify-center">
            <p className="text-sm text-text">Already have an account?</p>{" "}
            <p
              onClick={() => {
                navigate("/login");
              }}
              className="text-primary text-sm underline cursor-pointer font-bold"
            >
              Log in
            </p>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Register;
