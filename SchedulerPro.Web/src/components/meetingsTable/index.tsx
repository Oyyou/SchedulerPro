import { FC } from "react";
import moment from "moment-timezone";
import { IMeeting } from "types";
import styles from "./meetingsTable.module.scss";
import { useAppContext } from "providers/appContextProvider";
import { deleteMeeting, removeUserFromMeeting } from "api/meetingService";

type meetingsTableProps = {
  meetings: IMeeting[]
  tzOverride?: string,
}

const MeetingsTable: FC<meetingsTableProps> = ({ meetings, tzOverride }) => {
  const { loggedInUser } = useAppContext();

  if (!loggedInUser) {
    return null;
  }

  const handleDeletingMeeting = async (meetingId: string) => {
    const { error } = await deleteMeeting(meetingId);

    if (error) {
      return;
    }
    window.location.reload();
  }

  const handleRemovingSelfFromMeeting = async (meetingId: string) => {
    const { error } = await removeUserFromMeeting(meetingId, loggedInUser.id);

    if (error) {
      return;
    }
    window.location.reload();
  }

  return (
    <div className={styles.meetingsTableContainer}>
      <h3>Meetings</h3>
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Meeting</th>
            <th>Start</th>
            <th>Duration</th>
            <th>Attendees</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {meetings.map(({ id, name, startTime, endTime, attendees }) => {
            const st = tzOverride ? moment.utc(startTime).tz(tzOverride) : moment.utc(startTime).local();
            const et = tzOverride ? moment.utc(endTime).tz(tzOverride) : moment.utc(endTime).local();
            const duration = et.diff(st, 'minutes');
            return (
              <tr key={id}>
                <td>{st.format('LL')}</td>
                <td>{name}</td>
                <td>{st.format('LT')}</td>
                <td>{duration} minutes</td>
                <td>
                  <ul>
                    {attendees.map(({ id, firstName, lastName }) => (
                      <li key={id}><a href={`/user/${id}`}>{firstName} {lastName}</a></li>
                    ))}
                  </ul>
                </td>
                <td className={styles.actionsContainer}>
                  <button className={styles.deleteButton} onClick={() => handleDeletingMeeting(id)}>Remove meeting</button>
                  {attendees.some(({ id }) => id === loggedInUser.id) &&
                    <button className={styles.leaveButton} onClick={() => handleRemovingSelfFromMeeting(id)}>Leave meeting</button>}
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};

export default MeetingsTable;
