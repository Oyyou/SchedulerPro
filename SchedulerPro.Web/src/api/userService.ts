import { API_BASE_URL } from "./config";
import { IApiResponse, IMeeting, IUser } from "types";

export const getAllUsers = async (): Promise<IApiResponse<IUser[]>> => {
  const token = localStorage.getItem('token');
  try {
    const response = await fetch(`${API_BASE_URL}/users`, {
      method: "get",
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    const { users, error } = await response.json();

    return {
      data: users,
      error,
    };
  } catch (e) {
    console.error(e);
    return {
      error: JSON.stringify(e),
    }
  }
}

export const getUserById = async (id: string): Promise<IApiResponse<IUser>> => {
  const token = localStorage.getItem('token');
  try {
    const response = await fetch(`${API_BASE_URL}/users/${id}`, {
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
    console.error(e);
    return {
      error: JSON.stringify(e),
    }
  }
}

export const getUsersMeetings = async (id: string): Promise<IApiResponse<IMeeting[]>> => {
  const token = localStorage.getItem('token');
  try {
    const response = await fetch(`${API_BASE_URL}/users/${id}/meetings`, {
      method: "get",
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    const { meetings, error } = await response.json();

    return {
      data: meetings,
      error,
    };
  } catch (e) {
    console.error(e);
    return {
      error: JSON.stringify(e),
    }
  }
}

export const deleteUserById = async (id: string): Promise<IApiResponse<boolean>> => {
  const token = localStorage.getItem('token');
  try {
    const response = await fetch(`${API_BASE_URL}/users/${id}`, {
      method: "delete",
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    if (!response.ok) {
      const { error } = await response.json();
      return {
        error,
      };
    }

    return {
      data: true,
    };
  } catch (e) {
    console.error(e);
    return {
      error: JSON.stringify(e),
    }
  }
}