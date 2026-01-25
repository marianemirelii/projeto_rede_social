import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { Login } from "./pages/login/Login";
import { Home } from "./pages/home/Home";

import "./App.css";

function App() {
  const token = localStorage.getItem("token");

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />

        <Route
          path="/"
          element={token ? <Home /> : <Navigate to="/login" />}
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
