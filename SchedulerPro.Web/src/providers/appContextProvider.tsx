import { createContext, ReactNode, useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import moment from "moment-timezone";
import { getTimeZoneId } from "utils/dateTimeUtils";
import { verify } from "api/authService";
import { IUser } from "types";
import scheduleMeetingService, { IScheduleMeetingService } from "services/scheduleMeetingService";

interface IAppContextInput {
  logout: () => void,
  token: string,
  setToken: (token: string) => void,
  loggedInUser: IUser | null,
  setLoggedInUser: (user: IUser) => void,
  scheduleMeetingService: IScheduleMeetingService;
}

const AppContext = createContext<IAppContextInput>({} as IAppContextInput);

const AppContextProvider = ({ children }: { children: ReactNode }) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string>(() => localStorage.getItem("token") ?? "");
  const [loggedInUser, setLoggedInUser] = useState<IUser | null>(localStorage.getItem("loggedInUser") ? JSON.parse(localStorage.getItem("loggedInUser") ?? "") : null);

  useEffect(() => {
    moment.tz.setDefault(getTimeZoneId());
  },)

  useEffect(() => {
    if (!token) {
      localStorage.removeItem('token');
      navigate("/login");
    } else {
      const verifyToken = async () => {
        const { data } = await verify(token);

        if (!data) {
          logout();
          navigate("/login");
        } else {
          localStorage.setItem('token', token);
          setLoggedInUser(data);
        }
      };

      verifyToken();
    }
  }, [token]);

  useEffect(() => {
    if (!loggedInUser) {
      localStorage.removeItem('loggedInUser');
    } else {
      localStorage.setItem('loggedInUser', JSON.stringify(loggedInUser));
    }
  }, [loggedInUser])

  const logout = () => {
    setToken("");
    setLoggedInUser(null);
  }

  return (
    <AppContext.Provider
      value={{
        logout,
        token,
        setToken,
        loggedInUser,
        setLoggedInUser,
        scheduleMeetingService
      }}>
      {children}
    </AppContext.Provider>
  )
}

const useAppContext = () => {
  const {
    logout,
    token,
    setToken,
    loggedInUser,
    setLoggedInUser,
    scheduleMeetingService
  } = useContext(AppContext);

  return {
    logout,
    token,
    setToken,
    loggedInUser,
    setLoggedInUser,
    scheduleMeetingService
  };
}

export {
  useAppContext,
  AppContext,
  AppContextProvider,
};