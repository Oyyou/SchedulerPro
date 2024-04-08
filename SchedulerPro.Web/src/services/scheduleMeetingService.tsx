import dayjs, { Dayjs } from "dayjs";
import { getTimeZoneId } from "utils/dateTimeUtils";
import { IMeeting, IUser } from "types";

export interface IScheduleMeetingService {
  isUserBusy(userId: string, newMeetingDateTime: Dayjs, duration: number, meetings: IMeeting[]): boolean;
  isUserOutOfHours(user: IUser, date: string, time: string, duration: number): boolean;
  isMeetingDataValid(name: string, date: string, time: string, duration: number, selectedUsers: IUser[], meetings: IMeeting[]): { isValid: boolean, error?: string };
}

const scheduleMeetingService: IScheduleMeetingService = {
  isUserBusy(userId: string, newMeetingDateTime: Dayjs, duration: number, meetings: IMeeting[]): boolean {
    const currentSt = newMeetingDateTime;
    const currentEt = currentSt.add(duration, 'minutes');

    for (const meeting of meetings) {
      if (!meeting.attendees.some((attendee) => attendee.id === userId)) {
        continue;
      }

      const startTime = dayjs.tz(meeting.startTime);
      const endTime = dayjs.tz(meeting.endTime);
      if (
        (currentSt.isSameOrAfter(startTime) && currentSt.isBefore(endTime)) ||
        (currentEt.isAfter(startTime) && currentEt.isSameOrBefore(endTime))
      ) {
        return true;
      }
    }
    return false;
  },

  isUserOutOfHours(user: IUser, date: string, time: string, duration: number): boolean {
    const userStartTime = dayjs.tz(`${date}T08:00:00`, user.timeZoneId).tz("UTC");
    const userEndTime = dayjs.tz(`${date}T18:00:00`, user.timeZoneId).tz("UTC");
    const meetingStartTime = dayjs.tz(`${date}T${time}`, getTimeZoneId()).tz("UTC");
    const meetingEndTime = meetingStartTime.add(duration, 'minutes');

    if (meetingStartTime < userStartTime || meetingEndTime > userEndTime) {
      return true;
    }

    return false;
  },

  isMeetingDataValid(name: string, date: string, time: string, duration: number, selectedUsers: IUser[], meetings: IMeeting[]): { isValid: boolean, error?: string } {
    if (name.trim() === "") {
      return {
        isValid: false,
        error: "Name is required",
      }
    }

    const now = dayjs().tz();
    const selectedDateTime = dayjs.tz(`${date}T${time}`, getTimeZoneId()).tz("UTC");

    if (selectedDateTime.isBefore(now)) {
      return {
        isValid: false,
        error: "Date and time must be in the future",
      }
    }

    if (selectedUsers.length < 2) {
      return {
        isValid: false,
        error: "Select at least 2 attendees",
      }
    }

    for (const user of selectedUsers) {
      if (this.isUserBusy(user.id, selectedDateTime, duration, meetings)) {
        return {
          isValid: false,
          error: `${user.firstName} ${user.lastName} is already in a meeting at the selected time`,
        }
      }

      if (this.isUserOutOfHours(user, date, time, duration)) {
        return {
          isValid: false,
          error: `${user.firstName} ${user.lastName} is not working at those hours`,
        }
      }
    }
    return {
      isValid: true,
    }
  },
};

export default scheduleMeetingService;