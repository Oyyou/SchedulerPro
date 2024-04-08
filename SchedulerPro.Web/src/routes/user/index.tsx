import { getUserById, getUsersMeetings } from "api/userService";
import { MeetingsTable } from "components";
import moment from "moment-timezone";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { IMeeting, IUser } from "types";


const UserPage = () => {
  const navigate = useNavigate();
  const { id } = useParams();

  const [loading, setLoading] = useState(true);
  const [user, setUser] = useState<IUser | null>(null);
  const [meetings, setMeetings] = useState<IMeeting[]>([]);

  useEffect(() => {

    const fetchUser = async () => {
      try {
        if (!id) {
          return;
        }

        const { data } = await getUserById(id);

        if (data) {
          setUser(data);
        }
      } catch (err) {
        console.error(err)
      } finally {
        setLoading(false);
      }
    }

    fetchUser();

  }, []);

  useEffect(() => {
    if (loading) {
      return;
    }

    const fetchMeetings = async () => {
      if (!id || !user) {
        return;
      }

      const { data, error } = await getUsersMeetings(id);

      if (data) {
        setMeetings(data);
      }
    }

    fetchMeetings();
  }, [user])

  useEffect(() => {
    // If it's finished loading, and there is no user, return home
    if (!loading && !user) {
      navigate('/');
    }
  }, [loading, user])

  if (!user) {
    return <p>Loading...</p>
  }

  const {
    firstName,
    lastName,
    timeZoneId,
  } = user;

  return (
    <div>
      <h2>{firstName} {lastName} - {moment.tz(timeZoneId).format("LLL")}</h2>
      <MeetingsTable meetings={meetings} tzOverride={timeZoneId} />
    </div>
  );
};

export default UserPage;