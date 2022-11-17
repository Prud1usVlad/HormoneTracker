import i18n from "i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import { initReactI18next } from "react-i18next";

i18n
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    // we init with resources
    resources: {
      en: {
        translations: {
          email:"Email",
          password: "Password",
          submit: "Submit",
          loginHeader: "Login",
          patientId: "Patient ID",
          name: "Name",
          lastName:"Last name",
          midName:"Mid name",
          patientsList:"List of the patients",
          actions: "Actions",
          delete: "Delete",
          add: "Add",
          details: "Details",
          patientData: "Edit patient data",
          phone: "Phone number",
          save: "Save changes",
          back: "Back",
          basicData: "Basic data",
          tips: "Tips",
          addNewTip: "Add new tip",
          charts: "Charts",
          search: "Search",
          onDelete: "Do you rely want to delete",
          chartWarning: "An error occurred while trying to get chart data for this patient. Try spell check your input or try later.",
          loginError: "An error occurred in process of authorization, please check your credentials.",
          accessDenied: "Resource access denied to this account.",
          adminPanelHeader: "Adminpanel",
          doctors:"Doctors",
          admins:"Admins",
          products:"Products",
          doctorsList:"Doctors list",
          adminsList:"Admins list",
          productsList:"Products list",
          doctorId:"Doctor Id",
          productId:"Product Id",
          description:"Description",
          title:"Title",
          doctorData:"Doctor data",
          productData:"Product data",
          normCoefficient: "Coefficient from norm: ",
          value: "Value: ",
        }
      },
      ua: {
        translations: {
            email:"Електронна пошта",
            password: "Пароль",
            submit: "Відправити",
            loginHeader: "Вхід в систему",
            patientId: "ID пацієнта",
            name: "Ім'я",
            lastName:"Прізвище",
            midName:"Побатькові",
            patientsList: "Список пацієнтів",
            actions: "Дії",
            delete: "Видалити",
            add:"Додати",
            details: "Детальніше",
            patientData: "Дані пацієнта",
            phone: "Номер телефону",
            save: "зберегти зміни",
            back: "Назад",
            basicData: "Базова інформація",
            tips: "Вказівки",
            addNewTip: "Додати нову вказівку",
            charts: "Графіки",
            search: "Шукати",
            onDelete: "Ви дійсно хочете видалити?",
            chartWarning: "Сталася помилка під час спроби отримати дані графіку для поточного користувача. Перевірки правильність введених даних або спробуйте ще раз пізніше.",
            loginError: "Сталася помилка під час авторизації, перевірте введені дані.",
            accessDenied: "Для цього аккаунту доступ до ресурсу заборонений.",
            adminPanelHeader: "Адмінпанель",
            doctors:"Лікарі",
            admins:"Адміністратори",
            products:"Продукти",
            doctorsList:"Список лікарів",
            adminsList:"Список адміністраторів",
            productsList:"Список продуктів",
            doctorId:"Id лікаря",
            productId:"Id продукту",
            description:"Описання",
            title:"Назва",
            doctorData:"Дані лікаря",
            productData:"Дані продукту",
            normCoefficient: "Коефіцієнт норми: ",
            value: "Значення: ",
        }
      }
    },
    fallbackLng: "en",
    debug: true,

    // have a common namespace used around the full app
    ns: ["translations"],
    defaultNS: "translations",

    keySeparator: false, // we use content as keys

    interpolation: {
      escapeValue: false
    }
  });

export default i18n;
