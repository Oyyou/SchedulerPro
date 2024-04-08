import { getTimeZoneId } from "utils/dateTimeUtils";
import { API_BASE_URL } from "./config";
import { IApiResponse, IUser } from "types";

export const login = async (email: string, password: string) => {
  try {
    const body = {
      email,
      password,
    };
    const response = await fetch(`${API_BASE_URL}/auth/login`, {
      method: "POST",
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body)
    });

    const { token, error } = await response.json();

    return {
      data: token,
      error,
    };
  } catch (e) {
    return {
      error: JSON.stringify(e),
    }
  }
}

export const register = async (firstName: string, lastName: string, email: string, password: string) => {
  try {
    const body = {
      firstName,
      lastName,
      email,
      password,
      timeZoneId: getTimeZoneId(),
    };

    const response = await fetch(`${API_BASE_URL}/auth/register`, {
      method: "POST",
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body)
    });

    const { token, error } = await response.json();

    return {
      data: token,
      error,
    };
  } catch (e) {
    return {
      error: JSON.stringify(e),
    }
  }
}

export const verify = async (token: string): Promise<IApiResponse<IUser>> => {
  try {
    const response = await fetch(`${API_BASE_URL}/auth/verify`, {
      method: "get",
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    const { user, error } = await response.json();

    return {
      data: user,
      error,
    };
  } catch (e) {
    return {
      error: JSON.stringify(e),
    }
  }
}