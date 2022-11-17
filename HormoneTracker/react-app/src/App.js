import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Header from './Components/Common/Header';
import Login from './Components/Pages/Login';
import Home from './Components/Pages/Home';
import AdminPanel from './Components/Pages/AdminPanel';


const token = localStorage.getItem("token");

function App() {
  return (
    <BrowserRouter>
			<Routes>
          <Route path="/" element={ token ? <Home /> : <Login />} />
          <Route path="/Login" element={<Login />} />
          <Route path="/AdminPanel" element={<AdminPanel />} />				
				  <Route path="/*" element={<Navigate to="/" />} />
			</Routes>
		</BrowserRouter>
  );
}

export default App;
