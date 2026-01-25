import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { login } from "../../store/authSlice";
import type { AppDispatch, RootState } from "../../store";
import { useNavigate } from "react-router-dom";
import "./login.css";

export function Login() {
  const dispatch = useDispatch<AppDispatch>();
  const { loading, error } = useSelector((state: RootState) => state.auth);

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [touched, setTouched] = useState(false);

  const navigate = useNavigate();

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setTouched(true);

    if (!email || !password) return;

    const result = await dispatch(
      login({
        email,
        password,
      })
    );

    // ✅ login com sucesso
    if (login.fulfilled.match(result)) {
      navigate("/", { replace: true });
    }
  }

  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleSubmit}>
        <h1 className="logo">DevHub</h1>
        <h2 className="form-title">Entrar na sua conta</h2>

        <div className="input-field">
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          {touched && !email && (
            <span className="error-message">Email é obrigatório</span>
          )}
        </div>

        <div className="input-field">
          <input
            type="password"
            placeholder="Senha"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          {touched && !password && (
            <span className="error-message">Senha é obrigatória</span>
          )}
        </div>

        {error && <p className="error-message">{error}</p>}

        <button className="btn-login" disabled={loading}>
          {loading ? "Entrando..." : "Entrar"}
        </button>

        <p className="register-text">
          Novo aqui? <a href="/register">Crie sua conta</a>
        </p>
      </form>
    </div>
  );
}
