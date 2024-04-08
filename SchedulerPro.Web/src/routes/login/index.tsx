import { useState } from "react";
import { LoginForm, RegisterForm } from "components";
import styles from "./login.module.scss";

const LoginPage = () => {
  const [loggingIn, setLoggingIn] = useState(true);

  const toggleForm = () => setLoggingIn((prev) => !prev);

  return (
    <div className={styles.loginContainer}>
      {loggingIn ? <LoginForm toggleForm={toggleForm} /> : <RegisterForm toggleForm={toggleForm} />}
    </div>
  );
};

export default LoginPage;