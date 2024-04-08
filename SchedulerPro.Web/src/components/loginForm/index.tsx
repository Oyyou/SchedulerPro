import { FC, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAppContext } from "providers/appContextProvider";
import { login } from "api/authService";
import styles from "./loginForm.module.scss";

type loginFormProps = {
  toggleForm: () => void,
}

const LoginForm: FC<loginFormProps> = ({ toggleForm }) => {
  const navigate = useNavigate();

  const [loading, setLoading] = useState(false);

  const { token, setToken } = useAppContext();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError("");
    setLoading(true);

    const { data: authToken, error: authError } = await login(email, password);
    if (authToken) {
      localStorage.setItem('token', authToken);
      setToken(authToken);
    }

    if (authError) {
      setError(authError);
    }

    setLoading(false);
  }

  useEffect(() => {
    if (token) {
      navigate('/');
    }
  }, [token])

  return (
    <div className={styles.loginFormContainer}>
      <form onSubmit={onSubmit}>
        <label htmlFor="email">Email:</label>
        <input type="email" id="email" value={email} onChange={(e) => setEmail(e.target.value)} />
        <label htmlFor="password">Password:</label>
        <input type="password" id="password" value={password} onChange={(e) => setPassword(e.target.value)} />
        <input type="submit" value="Login" disabled={loading} />
      </form>
      {error && <p className={styles.error}>{error}</p>}
      <button className={styles.signupButton} onClick={toggleForm} disabled={loading}>or signup</button>
    </div>
  );
};

export default LoginForm;
