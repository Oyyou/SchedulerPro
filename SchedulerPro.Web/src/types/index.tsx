export interface IApiResponse<TData> {
  data?: TData;
  error?: any;
}

export interface IUser {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  timeZoneId: string;
}

export interface IMeeting {
  id: string;
  name: string;
  startTime: string;
  endTime: string;
  attendees: IUser[],
};