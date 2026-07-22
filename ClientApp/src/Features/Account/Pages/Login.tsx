import dayjs from "dayjs";
import { ArrowRight, BookOpenText, Eye, EyeClosed } from "lucide-react";
import LogInImage from "../../../assets/Images/LoginImage.png";
import { useState } from "react";
import { useAuth } from "../../../hooks/useAuth";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const currentYear = dayjs();
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const { login } = useAuth();
  const navigate = useNavigate();
  const [showPass, setShowPass] = useState(false);

  const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!userName || !password) return;
    if (isSubmitting) return;

    try {
      setIsSubmitting(true);
      const result = await login({ userName, password });
      if (result.isSuccess) {
        toast.success("Welcome Back!");
        navigate("/");
      } else {
        toast.error(result.message);
      }
    } catch {
      toast.error("Something went wrong. Please try again.");
    } finally {
      setIsSubmitting(false);
    }
  };
  return (
    <div className="flex min-h-screen justify-center items-center flex-col bg-background  md:mx-0 relative">
      <div className="bg-primary p-2 rounded-md shadow-[0_0_10px] shadow-slate-300">
        <BookOpenText className="text-white" size={30} />
      </div>
      <p className="font-bold text-xl mt-2 text-text">Lexicon Systems</p>

      <div className="main-card mt-5 bg-background-secondary z-10 mx-2">
        <h2 className="font-bold text-2xl mb-1 text-text">Welcome Back</h2>
        <p className="text-text-secondary text-[12px]">
          Please enter your details to access the librarian portal
        </p>

        <form className="mt-5" onSubmit={handleSubmit}>
          <div>
            <label
              className="text-[12px] font-bold select-none text-text"
              htmlFor="username"
            >
              USERNAME
            </label>
            <input
              type="text"
              id="username"
              className="search-input"
              placeholder="John. Doe"
              value={userName}
              onChange={(e) => setUserName(e.target.value)}
            />
          </div>
          <div className="mt-4">
            <label
              className="text-[12px] font-bold select-none text-text"
              htmlFor="password"
            >
              PASSWORD
            </label>
            <div className="relative">
              {showPass ? (
                <EyeClosed
                  className="absolute right-2 top-1/2 translate-y-[-50%] cursor-pointer text-text-secondary"
                  size={20}
                  onClick={() => setShowPass((prev) => !prev)}
                />
              ) : (
                <Eye
                  className="absolute right-2 top-1/2 translate-y-[-50%] cursor-pointer text-text-secondary"
                  size={20}
                  onClick={() => setShowPass((prev) => !prev)}
                />
              )}
              <input
                type={showPass ? "text" : "password"}
                id="password"
                className="search-input select-none"
                placeholder="********"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
          </div>
          <button
            className="main-button flex items-center gap-2 w-full mt-5 justify-center"
            type="submit"
          >
            <span className="text-sm font-semibold">Sign In</span>{" "}
            <ArrowRight size={18} />
          </button>

          <div className="mt-5 flex items-center justify-center md:flex-row flex-col text-[12px] gap-1 text-text-secondary">
            <p className="text-nowrap">Don't have an account?</p>
            <p
              onClick={() => {
                navigate("/registery");
              }}
              className="underline cursor-pointer"
            >
              Create Account
            </p>
          </div>

          <div className="mt-5 flex flex-col text-center">
            <div className="text-xs font-mono text-text-secondary/60 justify-center gap-1.5">
              &copy; {currentYear.year()} Lexicon Systems. All Rights Reserved.
            </div>
            <div className="text-xs font-mono text-text-secondary/60 justify-center gap-1.5">
              Crafted with{"  "}
              {
                <span className="text-red animate-pulse text-base">♥</span>
              } by{" "}
              <a
                href="https://iissa.dev"
                target="_blank"
                className="text-text-secondary hover:text-primary transition-colors"
              >
                IIssadev
              </a>
            </div>
          </div>
        </form>
      </div>

      <div className="absolute hidden md:block w-50 h-62.5 left-10 bottom-20 -rotate-6 transition-transform hover:rotate-0 duration-300">
        <div className="relative h-full w-full shadow-lg rounded-2xl overflow-hidden">
          <div className="bg-black/20 w-full h-full absolute top-0 left-0 z-10"></div>

          <img
            className="w-full h-full object-cover"
            src={LogInImage}
            alt="Login Image"
          />

          <p className="absolute bottom-6 left-4 text-white font-bold text-xl z-20 drop-shadow-md">
            Lexicon System
          </p>
        </div>
      </div>
    </div>
  );
};

export default Login;
