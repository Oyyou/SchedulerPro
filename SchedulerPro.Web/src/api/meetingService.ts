import { getTimeZoneId } from "utils/dateTimeUtils";
import { API_BASE_URL } from "./config";
import { IApiResponse, IMeeting, IUser } from "types";

export const getAllMeetings = async (): Promise<IApiResponse<IMeeting[]>> => {
  const token = localStorage.getItem('token');
  try {
    const response = await fetch(`${API_BASE_URL}/meetings`, {
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

export const createMeeting = async (name: string, startDateTime: string, duration: number, attendeeIds: string[] = []): Promise<IApiResponse<IMeeting>> => {
  const token = localStorage.getItem('token');
  const body = {
    name,
    startTime: startDateTime,
    duration,
    attendeeIds,
    timeZoneId: getTimeZoneId(),
  }
  try {
    const response = await fetch(`${API_BASE_URL}/meetings/create`, {
      method: "post",
      headers: {
        'Authorization': `Bearer ${token}`,
        'content-type': 'application/json'
      },
      body: JSON.stringify(body),
    });

    const { meeting, error } = await response.json();

    return {
      data: meeting,
      error,
    };
  } catch (e) {
    console.error(e);
    return {
      error: JSON.stringify(e),
    }
  }
}

export const deleteMeeting = async (meetingId: string): Promise<IApiResponse<boolean>> => {
  const token = localStorage.getItem('token');
  try {
    const response = await fetch(`${API_BASE_URL}/meetings/${meetingId}`, {
      method: "delete",
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    if (response.ok) {
      return { data: true };
    } else {
      const { error } = await response.json();
      return { error };
    }
  } catch (e) {
    console.error(e);
    return { error: JSON.stringify(e) };
  }
}

export const removeUserFromMeeting = async (meetingId: string, userId: string): Promise<IApiResponse<boolean>> => {
  const token = localStorage.getItem('token');
  try {
    const response = await fetch(`${API_BASE_URL}/meetings/${meetingId}/removeuser/${userId}`, {
      method: "delete",
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    if (response.ok) {
      return { data: true };
    } else {
      const { error } = await response.json();
      return { error };
    }
  } catch (e) {
    console.error(e);
    return { error: JSON.stringify(e) };
  }
}