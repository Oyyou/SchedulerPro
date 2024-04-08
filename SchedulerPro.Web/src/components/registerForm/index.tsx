import { FC, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAppContext } from "providers/appContextProvider";
import { register } from "api/authService";
import styles from "./registerForm.module.scss";

type registerFormProps = {
  toggleForm: () => void,
}

interface FormState {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

const RegisterForm: FC<registerFormProps> = ({ toggleForm }) => {
  const navigate = useNavigate();

  const [loading, setLoading] = useState(false);

  const { token, setToken } = useAppContext();

  const [formState, setFormState] = useState<FormState>({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
  });

  const [error, setError] = useState("");

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError("");
    setLoading(true);

    const { data: authToken, error: authError } = await register(
      formState.firstName,
      formState.lastName,
      formState.email,
      formState.password,
    );
    if (authToken) {
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

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { id, value } = e.target;
    setFormState({ ...formState, [id]: value });
  }

  return (
    <div className={styles.registerFormContainer}>
      <form onSubmit={onSubmit}>
        <label htmlFor="firstName">First name:</label>
        <input type="text" id="firstName" value={formState.firstName} onChange={handleInputChange} />
        <label htmlFor="lastName">Last name:</label>
        <input type="text" id="lastName" value={formState.lastName} onChange={handleInputChange} />
        <label htmlFor="email">Email:</label>
        <input type="email" id="email" value={formState.email} onChange={handleInputChange} />
        <label htmlFor="password">Password:</label>
        <input type="password" id="password" value={formState.password} onChange={handleInputChange} />
        <input type="submit" value="Register" disabled={loading} />
      </form>
      {error && <p className={styles.error}>{error}</p>}
      <button className={styles.loginButton} onClick={toggleForm} disabled={loading}>back to login</button>
    </div>
  );
};

export default RegisterForm;
