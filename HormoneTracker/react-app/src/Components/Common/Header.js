import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import { Link, redirect } from "react-router-dom";

export default function Header() {

    const { t, i18n } = useTranslation();

    const changeLanguage = (lng) => {
        i18n.changeLanguage(lng);
    };
  

    return(
        <nav class="navbar-dark bg-primary">
        <div class="container-fluid">
          <div class="row justify-content-end">
                <div class="col-10 my-3">
                    <Link className="navbar-brand" to={"/"}>Hormone Tracker </Link>
                </div>
                <div class="col-1">
                    <button class="btn btn-light my-2" onClick={() => changeLanguage("en")}>English</button>
                </div>
                <div class="col-1">
                    <button class="btn btn-light my-2" onClick={() => changeLanguage("ua")}>Українська</button>
                </div>
            </div>
        </div>
      </nav>
    )
}