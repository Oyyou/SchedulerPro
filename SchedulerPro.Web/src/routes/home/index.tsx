import { useEffect, useState } from "react";
import { getAllMeetings } from "api/meetingService";
import { IMeeting, IUser } from "types";
import { AuthGuard, MeetingsTable, ScheduleMeetingModal, UsersTable } from "components";
import { getAllUsers } from "api/userService";
import styles from "./home.module.scss";

const HomePage = () => {
  const [showScheduleMeeting, setShowScheduleMeeting] = useState(false);
  const [users, setUsers] = useState<IUser[]>([]);
  const [meetings, setMeetings] = useState<IMeeting[]>([]);

  useEffect(() => {
    const fetchMeetings = async () => {
      const { data } = await getAllMeetings();
      if (data) {
        setMeetings(data);
      }
    }

    const fetchUsers = async () => {
      const { data } = await getAllUsers();
      if (data) {
        setUsers(data);
      }
    }

    fetchUsers();
    fetchMeetings();

  }, []);

  const handleOnModalClosed = () => {
    setShowScheduleMeeting(false);
  }

  return (
    <>
      {showScheduleMeeting && <ScheduleMeetingModal users={users} meetings={meetings} onClose={handleOnModalClosed} />}
      <div className={styles.homeContainer}>
        <button className={styles.scheduleButton} onClick={() => setShowScheduleMeeting(true)}>Schedule meeting</button>
        <UsersTable users={users} />
        <MeetingsTable meetings={meetings} />
      </div>
    </>
  );
};

const HomePageWithAuthGuard = () => (
  <AuthGuard>
    <HomePage />
  </AuthGuard>
)

export default HomePageWithAuthGuard;
