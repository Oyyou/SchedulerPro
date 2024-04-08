import { useEffect, useState } from 'react';
import { getTimeZoneId } from 'utils/dateTimeUtils';
import styles from './header.module.scss';
import { useAppContext } from 'providers/appContextProvider';

const Header = () => {

  const [currentTime, setCurrentTime] = useState(new Date());
  const { loggedInUser, logout } = useAppContext();

  useEffect(() => {
    const intervalId = setInterval(() => {
      setCurrentTime(new Date());
    }, 1000);

    // Cleanup function to clear the interval when the component unmounts
    return () => clearInterval(intervalId);
  }, []);

  if (!loggedInUser) {
    return null;
  }

  return (
    <div className={styles.headerOuterContainer}>
      <div className={styles.headerContainer}>
        <nav className={styles.navContainer}>
          <h1 className={styles.logo}><a href='/'>Scheduler Pro</a></h1>
          <div className={styles.infoContainer}>
            <p className={styles.currentTime}>{getTimeZoneId()} - {currentTime.toLocaleString()}</p>
            <button className={styles.logoutButton} onClick={logout}>Logout</button>
          </div>
        </nav>
      </div>
    </div>
  );
};

export default Header;
