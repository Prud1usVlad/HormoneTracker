import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import { Link, redirect } from "react-router-dom";
import Header from '../Common/Header';
import {BootstrapTable, TableHeaderColumn} from 'react-bootstrap-table';
import React, { useState, useEffect } from 'react';
import Modal from 'react-bootstrap/Modal';
import axios from "axios";
import CustomChart from "../Common/CustomChart";

const API_URL = "https://localhost:7070/api/";
const token = localStorage.getItem("token");

//"eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2Njg2MTA2OTQsImV4cCI6MTY2ODY5NzA5NCwiaWF0IjoxNjY4NjEwNjk0LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDcwIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA3MCJ9.hrvmcUqvDGp_WyxpBHWv13ZCQWIYM7Lkz_dW7UZ0Vkpir-wRrDxcF1jClQ1GI3Tqqg1jpRFLj0lFjpvPkNEgdg"

const headers = { headers: { 'Authorization': `Bearer ${token}`}};
let counter = 0;

export default function Home() {

    const { t, i18n } = useTranslation();
    const [fullscreen, setFullscreen] = useState(true);
    const [show, setShow] = useState(false);
    const [patients, setPatients] = useState([]);
    const [patient, setPatient] = useState({});
    const [analysis, setAnalysis] = useState("");
    const [chartData, setChartData] = useState([{name:"Empty", coef:0}, {name:"Empty", coef:0}]);
    const [tips, setTips] = useState([]); 
    const [rawChartData, setRawChartData] = useState();

    useEffect(() => {
        async function fetchData() {
            let responce = await axios.get(API_URL + "Patients", headers);
            setPatients(responce.data);
        }

        fetchData();
    });

    useEffect(() => {
        setTips(patient.tips ? patient.tips : []);
    }, [patient]);

    useEffect(() => {
        let newData = [];
        console.log(rawChartData)
        if (rawChartData && rawChartData.data) {
            console.log("Raw data")
            for (const [key, values] of Object.entries(rawChartData.data)) {
                for (let data of values) {
                    newData.push({name:key + " analysis norm coeficient", coef:parseFloat(data)});
                }
            }
        }

        setChartData(newData);
    }, [rawChartData])

    const actionsFormatter = (cell, row) => {
        return (
            <div>
                <button class="btn btn-danger mx-1" onClick={() => onDelete(row.patientId)}><Trans i18nKey="delete"/></button>
                <button class="btn btn-primary mx-1" onClick={() => onDetails(row)}><Trans i18nKey="details"/></button>
            </div>
        )
    }

    const onDelete = async (id) => {
        if (window.confirm()) {
            console.log(await axios.delete(API_URL + "Patients/" + id, headers));
        }
    }
    
    const onDetails = (row) => {
        setPatient(row);
        setFullscreen(true);
        setShow(true);
        setChartData([{name:"Empty", coef:0}, {name:"Empty", coef:0}]);
    }

    const extractChartData = async () => {
        chartData.length = 0;
        
        try {
            let responce = await axios.get(`${API_URL}Charts/Patient/${patient.patientId}/${analysis}`, headers);
            console.log(responce);
            setRawChartData(responce.data);
            
        }
        catch {
            alert(t("chartWarning"));
        }
        
        console.log(chartData);
    }

    const onAddTip = (id) => {
        let text = document.getElementById(id).value;
        let tip = {comment:text, date:new Date().toJSON().slice(0, 10), patientId:patient.patientId};
        tips.push(tip);

        document.getElementById('tipEditor').value = "";
        
        axios.post(API_URL + "Tips", tip,  headers);
    }

    const onSave = async () => {
        if (patient.patientId === undefined) {
            console.log(await axios.post(API_URL + "Patients", patient, headers));
            
        }
        else {
            console.log(await axios.put(API_URL + "Patients/" + patient.patientId, patient, headers));
        }
        setFullscreen(false);
        setShow(false);
    }

    const updatePatient = (field, value) => {
        let newPatient = {};
        Object.assign(newPatient, patient);

        newPatient[field] = value;

        setPatient(newPatient);
    }
  
    return(
        <div>
            <Header />
            <Modal show={show} scrollable={true} fullscreen={fullscreen} onHide={() => setShow(false)}>
                <Modal.Header closeButton>
                <Modal.Title><Trans i18nKey="patientData"/></Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div class="container">
                        <div class="row justify-content-md-center m-4">
                        <div class="col align-self-center">
                            <h2><Trans i18nKey="basicData"/></h2>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-4 align-self-center">
                            <label class="form-label"><Trans i18nKey="name"/></label>
                            <input class="form-control" 
                                value={patient.name} 
                                onChange={e => updatePatient("name", e.target.value)}/>
                        </div>
                        <div class="col-4 align-self-center">
                            <label class="form-label"><Trans i18nKey="email"/></label>
                            <input type="email" class="form-control" 
                                value={patient.email}
                                onChange={e => updatePatient("email", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-4 align-self-center">
                            <label class="form-label"><Trans i18nKey="lastName"/></label>
                            <input class="form-control" 
                                value={patient.lastName}
                                onChange={e => updatePatient("lastName", e.target.value)}/>
                        </div>
                        <div class="col-4 align-self-center">
                            <label class="form-label"><Trans i18nKey="phone"/></label>
                            <input class="form-control" 
                                value={patient.phone}
                                onChange={e => updatePatient("phone", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-4 align-self-center">
                            <label class="form-label"><Trans i18nKey="midName"/></label>
                            <input class="form-control" 
                                value={patient.midName}
                                onChange={e => updatePatient("midName", e.target.value)}/>
                        </div>
                        <div class="col-4 align-self-center"> 
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col align-self-center">
                            <h2><Trans i18nKey="tips"/></h2>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        { tips.map((tip) => (
                            <div class="col">
                            <div class="card">
                                <div class="card-header">{tip.date}</div>
                                <div class="card-body">
                                    <p class="card-text">{tip.comment}</p>
                                </div>
                            </div>
                            </div>
                        ))}
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-6 align-self-center">
                            <label class="form-label"><Trans i18nKey="addNewTip"/></label>
                            <textarea class="form-control" id="tipEditor" rows="3"></textarea>
                        </div>
                        <div class ="col-1 align-self-center">
                            <button class="btn btn-primary" onClick={() => { onAddTip('tipEditor') }}>
                                <Trans i18nKey="add"/> 
                            </button>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col align-self-center">
                            <h2><Trans i18nKey="charts"/></h2>
                        </div>
                        <div class="col-3 align-self-center">
                            <input class="form-control" 
                                value={analysis} 
                                onChange={e => setAnalysis(e.target.value)}/>
                        </div>
                        <div class="col-7 align-self-center">
                            <button variant="primary" type="button" class="btn btn-primary" onClick={() => { extractChartData() }}>
                                <Trans i18nKey="search"/> 
                            </button>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col align-self-center" id="chartParent">
                            <CustomChart chartData={chartData}/>
                        </div>
                        </div>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <button type="button" class="btn btn-light" onClick={() => { setFullscreen(false); setShow(false); }}>
                        <Trans i18nKey="back"/>
                    </button>
                    <button variant="primary" type="button" class="btn btn-primary" onClick={() => { onSave(); }}>
                        <Trans i18nKey="save"/> 
                    </button>
                </Modal.Footer>
            </Modal>

            <div class="container">
                <div class="row m-5"><h2><Trans i18nKey="patientsList"/></h2></div>
                <div class="row">
                    <div class="col-2">
                        <button class="btn btn-primary mx-1" onClick={() => onDetails({})}><Trans i18nKey="add"/></button>
                    </div>
                    <BootstrapTable data={ patients } search={ true }> 
                        <TableHeaderColumn dataField='patientId' isKey><Trans i18nKey="patientId"/></TableHeaderColumn>
                        <TableHeaderColumn dataField='name'><Trans i18nKey="name"/></TableHeaderColumn>
                        <TableHeaderColumn dataField='lastName'><Trans i18nKey="lastName"/></TableHeaderColumn>
                        <TableHeaderColumn dataField='midName'><Trans i18nKey="midName"/></TableHeaderColumn>
                        <TableHeaderColumn dataField='email'><Trans i18nKey="email"/></TableHeaderColumn>
                        <TableHeaderColumn dataField='' dataFormat={ actionsFormatter }><Trans i18nKey="actions"/></TableHeaderColumn>
                    </BootstrapTable>
                </div>
            </div>
        </div>
    )
}