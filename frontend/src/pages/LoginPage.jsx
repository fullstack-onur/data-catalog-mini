import styles from '../styles/login.module.css';
import LoginInput from '@/components/login/LoginInput';
import { useRef } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuthStore } from '../store/useAuthStore';

export default function LoginPage() {

  const username = useRef();
  const password = useRef();
  const navigate = useNavigate();

  const login = useAuthStore((s) => s.login);

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      await login(username.current.value, password.current.value);
      navigate("/");
    } catch {
      alert("Wrong username or password!");
    }
  }

  
  return (
    <div className={styles.page}>
      <div className={styles.overlay}></div>
      <div className={styles.container}>
        <form className={styles.containerWrapper} onSubmit={handleLogin}>
          <img className={styles.logoImg} src="/logo-data-catalog.svg" alt="Data Catalog Mini" />
          <h4>Discover. Govern. Trust Your Data.</h4>
          <LoginInput label="Username" ref={username} />
          <LoginInput label="Password" password ref={password} />
          <button className={styles.loginBtn}>Login</button>
          <p className={styles.signUpText}>
            Don't have an account? <span><Link to="/signup">Sign Up</Link></span>
          </p>
        </form>
      </div>
    </div>
  );
}
