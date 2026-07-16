import {
  createContext,
  useCallback,
  useEffect,
  useState,
  type ReactNode,
} from "react";
import type { LoginDto, TokenResult } from "../types";
import { useQueryClient } from "@tanstack/react-query";
import { authService } from "../Features/Account/services/authService";
import apiClient from "../api/apiClient";
import { clearAuth, getStoredToken, saveToken } from "../api/auth";

interface User {
  userId: number;
  username: string;
  role: string;
}
interface AuthContextType {
  token: TokenResult | null;
  user: User | null;
  loading: boolean;
  login: (data: LoginDto) => Promise<{ message: string; isSuccess: boolean }>;
  logout: () => Promise<void>;
}

const AuthContext = createContext<AuthContextType | null>(null);
export default AuthContext;

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [token, setToken] = useState<TokenResult | null>(() =>
    getStoredToken(),
  );

  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);
  const queryClient = useQueryClient();

  // init auth
  useEffect(() => {
    const storedToken = getStoredToken();
    if (!storedToken) {
      setLoading(false);
      return;
    }
    const init = async () => {
      try {
        const res: User | any = await apiClient.get<User>("/Auth/me");
        setUser(res);
      } catch (error) {
        setUser(null);
        setToken(null);
        clearAuth();
      } finally {
        setLoading(false);
      }
    };
    init();
  }, []);

  // loading
  const login = useCallback(
    async (
      data: LoginDto,
    ): Promise<{ message: string; isSuccess: boolean }> => {
      try {
        setLoading(true);
        const res = await authService.login(data);

        if (res && res.accessToken) {
          setToken(res);
          saveToken(res);

          const me: User | any = await apiClient.get<User>("/Auth/me");
          setUser(me);
          return { message: "Login Success", isSuccess: true };
        }

        return { message: "Invalid response from server", isSuccess: false };
      } catch (error: any) {
        return { message: error.detail, isSuccess: false };
      } finally {
        setLoading(false);
      }
    },
    [queryClient],
  );

  // logout
  const logout = async () => {
    try {
      await authService.logout();
    } catch {}

    setToken(null);
    setUser(null);
    localStorage.removeItem("authToken");

    queryClient.clear();
    window.location.replace("/login");
  };
  return (
    <AuthContext.Provider value={{ token, user, loading, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
