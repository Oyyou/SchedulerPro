import { FC, Fragment, useState } from "react";
import dayjs, { Dayjs } from "dayjs";
import { createMeeting } from "api/meetingService";
import { IMeeting, IUser } from "types";
import { getTimeZoneId } from "utils/dateTimeUtils";
import styles from "./scheduleMeetingModal.module.scss";
import { useAppContext } from "providers/appContextProvider";

type scheduleMeetingModalProps = {
  users: IUser[],
  meetings: IMeeting[],
  onClose: () => void,
}

const ScheduleMeetingModal: FC<scheduleMeetingModalProps> = ({ users, meetings, onClose }) => {

  const { scheduleMeetingService } = useAppContext();

  const [loading, setLoading] = useState(false);

  const [name, setName] = useState("");
  const [date, setDate] = useState(dayjs().format('YYYY-MM-DD'));
  const [time, setTime] = useState(dayjs().format('HH:mm'));
  const [duration, setDuration] = useState(60);
  const [selectedUsers, setSelectedUsers] = useState<IUser[]>([]);
  const [error, setError] = useState("");

  const handleUserSelect = (userId: string) => {
    const selectedUser = users.find(user => user.id === userId);
    if (selectedUser) {
      setSelectedUsers([...selectedUsers, selectedUser]);
    }
  };

  const handleUserRemove = (userId: string) => {
    setSelectedUsers(selectedUsers.filter(user => user.id !== userId));
  };

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const { isValid, error: formError } = scheduleMeetingService.isMeetingDataValid(
      name,
      date,
      time,
      duration,
      selectedUsers,
      meetings
    );

    if(formError) {
      setError(formError);
    }

    if (!isValid) {
      return;
    }

    setLoading(true);

    const { error } = await createMeeting(
      name,
      `${date}T${time}`,
      duration,
      selectedUsers.map(({ id }) => id),
    );

    if (error) {
      setError(error);
    } else {
      window.location.reload();
    }

    setLoading(false);
  };

  const selectableUser = users.filter((user) => selectedUsers.length === 0 || !selectedUsers.some(({ id }) => id === user.id));

  return (
    <div className={styles.overlay} onClick={onClose}>
      <div className={styles.scheduleMeetingModalContainer} onClick={(e) => e.stopPropagation()}>
        <button className={styles.closeButton} onClick={onClose}>X</button>
        <h3>Schedule meeting</h3>
        <form onSubmit={onSubmit} encType="multipart/form-data">
          <div className={styles.formFields}>
            <label htmlFor="name">Name:</label>
            <input id="name" type="text" value={name} onChange={(e) => setName(e.target.value)} />
            <label htmlFor="date">Date:</label>
            <input id="date" type="date" value={date} onChange={(e) => setDate(e.target.value)} />
            <label htmlFor="time">Time:</label>
            <input id="time" type="time" value={time} onChange={(e) => setTime(e.target.value)} />
            <label htmlFor="duration">Duration:</label>
            <input id="duration" type="text" value={duration} onChange={(e) => setDuration(parseInt(e.target.value))} />
            <label htmlFor="users">Attendees:</label>
            <select id="users" onChange={(e) => handleUserSelect(e.target.value)}>
              <option value="">Select Attendees</option>
              {selectableUser.map(user => (
                <option key={user.id} value={user.id}>{`${user.firstName} ${user.lastName}`}</option>
              ))}
            </select>
          </div>
          <div className={styles.selectedUsersContainer}>
            {selectedUsers.map((user) => (
              <Fragment key={user.id}>
                <span>{`${user.firstName} ${user.lastName}`}</span>
                <button onClick={() => handleUserRemove(user.id)}>Remove</button>
              </Fragment>
            ))}
          </div>
          <input type="submit" value="Schedule" disabled={loading} />
        </form>
        {error && <p>{error}</p>}
      </div>
    </div>
  )
}

export default ScheduleMeetingModal;
