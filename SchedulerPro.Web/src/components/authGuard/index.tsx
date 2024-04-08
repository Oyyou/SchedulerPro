import { ReactNode, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAppContext } from "providers/appContextProvider";
import { verify } from "api/authService";

export const AuthGuard = ({ children }: { children: ReactNode }) => {

  const navigate = useNavigate();
  const { token, setToken } = useAppContext();

  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const runVerify = async () => {
      if (!loading) {
        return;
      }
      const { error } = await verify(token);

      if (error) {
        setToken("");
        localStorage.setItem("token", "");
        console.error("Access required");
        navigate('/login');
      }
      setLoading(false);
    }

    runVerify();

  }, [token])

  if (loading) {
    return null;
  }

  return (
    <>
      {children}
    </>
  );
}

export default AuthGuard;