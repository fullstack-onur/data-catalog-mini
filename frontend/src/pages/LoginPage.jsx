import styles from '../styles/login.module.css';
import LoginInput from '@/components/login/LoginInput';
import { useRef } from 'react';
import { useNavigate, Link } from 'react-router-dom';

export default function LoginPage() {

  const username = useRef();
  const password = useRef();
  const navigate = useNavigate();

  const handleLogin = (e) => {
    // Dummy function
    // TO DO : Integrate with real backend.
    e.preventDefault(); 
    localStorage.setItem("auth_token", "dummy-token");
    if (username.current.value === "admin" && password.current.value === "admin") {
      navigate('/');
    } 
  };

  
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
