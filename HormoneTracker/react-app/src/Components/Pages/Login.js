import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import { Link, redirect } from "react-router-dom";
import React, { useState, useEffect } from 'react';
import axios from "axios";
import { useNavigate } from 'react-router-dom';

const API_URL = "https://localhost:7070/api/Security/Token";

export default function Login() {

    const { t, i18n } = useTranslation();
    const [ password, setPassword ] = useState("");
    const [ email, setEmail ] = useState("");
    const [ authorised, setAuthorised ] = useState(true);
    const navigate = useNavigate();

    const changeLanguage = (lng) => {
        i18n.changeLanguage(lng);
    };


    useEffect(() => {
        
    }, [authorised]);
    

    const onSubmit = async () => {
        try {
            let responce = await axios.post(API_URL, { Password:password, Email:email });
            console.log(responce);

            localStorage.setItem('token', responce.data.Token);
            localStorage.setItem('role', responce.data.Role);

            let str = localStorage.getItem('role')

            if (str === "admin") {
                console.log(str + "---");
                navigate("/AdminPanel");
            }
            else if (str === "doctor") {
                console.log(str + "+++");
                navigate("/");
            }
            else {
                alert(t("accessDenied"));
            }

            setAuthorised(true);
        }
        catch {
            alert(t("loginError"));
        }

        setPassword("");
    }
  
    return(
        <div class="container p-5">
            <div class="row justify-content-end my-5">
                <div class="col-1">
                    <button class="btn btn-dark" onClick={() => changeLanguage("en")}>English</button>
                </div>
                <div class="col-1">
                    <button class="btn btn-dark" onClick={() => changeLanguage("ua")}>Українська</button>
                </div>
            </div>
            <div class="row justify-content-center my-5">
                <form class="col-4">
                    <h3 className="p-3"><Trans i18nKey="loginHeader"></Trans></h3>
                    <div class="mb-3">
                        <label for="inputEmail" class="form-label"><Trans i18nKey="email"></Trans></label>
                        <input type="email" class="form-control" id="inputEmail" aria-describedby="emailHelp"
                            value={email}
                            onChange={e => setEmail(e.target.value)} />
                    </div>
                    <div class="mb-3">
                        <label for="inputPassword" class="form-label"><Trans i18nKey="password"></Trans></label>
                        <input type="password" class="form-control" id="inputPassword"
                            value={password}
                            onChange={e => setPassword(e.target.value)} />
                    </div>
                </form>
                
            </div>
            <div class="row justify-content-center my-5">
                <div class="col-4">
                    <button class="btn btn-primary" onClick={ () => onSubmit() }><Trans i18nKey="submit"></Trans></button>
                </div>
            </div>
        </div>
    )
}