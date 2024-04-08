import { FC } from "react";
import moment from "moment-timezone";
import { useAppContext } from "providers/appContextProvider";
import { IUser } from "types";
import styles from "./usersTable.module.scss";
import { deleteUserById } from "api/userService";

type usersTableProps = {
  users: IUser[]
}

const UsersTable: FC<usersTableProps> = ({ users }) => {
  const { loggedInUser } = useAppContext();

  if (!loggedInUser) {
    return null;
  }

  const handleDeleteUser = async (userId: string) => {
    const { error } = await deleteUserById(userId);

    if (error) {
      return;
    }
    window.location.reload();
  }

  return (
    <div className={styles.usersTableContainer}>
      <h3>Users</h3>
      <table>
        <thead>
          <tr>
            <th>User</th>
            <th>Timezone</th>
            <th>Local time</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {users.map(({ id, firstName, lastName, timeZoneId }) => {
            const localTime = moment.utc().tz(timeZoneId).format('LT');
            return (
              <tr key={id}>
                <td><a href={`/user/${id}`}>{firstName} {lastName}</a></td>
                <td>{timeZoneId}</td>
                <td>{localTime}</td>
                <td>
                  {(loggedInUser.id === id) ?
                    <p className={styles.cantDelete}>Can't delete self</p> :
                    <button className={styles.deleteButton} onClick={() => handleDeleteUser(id)}>Delete user</button>}
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div >
  )
};

export default UsersTable;
