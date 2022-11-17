import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import { Link, redirect } from "react-router-dom";
import {BootstrapTable, TableHeaderColumn} from 'react-bootstrap-table';
import React, { useState, useEffect } from 'react';
import Modal from 'react-bootstrap/Modal';
import axios from "axios";
import Header from "../Common/Header";

const API_URL = "https://localhost:7070/api/";
const token = localStorage.getItem("token");

//"eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2Njg2MTA2OTQsImV4cCI6MTY2ODY5NzA5NCwiaWF0IjoxNjY4NjEwNjk0LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDcwIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA3MCJ9.hrvmcUqvDGp_WyxpBHWv13ZCQWIYM7Lkz_dW7UZ0Vkpir-wRrDxcF1jClQ1GI3Tqqg1jpRFLj0lFjpvPkNEgdg"

const headers = { headers: { 'Authorization': `Bearer ${token}`}};

export default function AdminPanel() {

    const { t, i18n } = useTranslation();
    const [ doctors, setDoctors ] = useState([]);
    const [ products, setProducts ] = useState([]);
    const [ doctor, setDoctor ] = useState({});
    const [ product, setProduct ] = useState({});
    const [ data, setData ] = useState([]);
    const [ dataInput, setDataInput ] = useState({name:'', value:0, normCoefficient:0});

    const [fullscreen, setFullscreen] = useState(true);
    const [show, setShow] = useState(false);

    const [fullscreen1, setFullscreen1] = useState(true);
    const [show1, setShow1] = useState(false);

    useEffect(() => {
        async function fetchData() {
            let responce = await axios.get(API_URL + "Doctors", headers);
            setDoctors(responce.data);

            responce = await axios.get(API_URL + "Products", headers);
            setProducts(responce.data);
        }
        fetchData();
    });

    const doctorActionsFormatter = (cell, row) => {
        return (
            <div>
                <button class="btn btn-danger mx-1" onClick={() => onDoctorDelete(row.doctorId)}><Trans i18nKey="delete"/></button>
                <button class="btn btn-primary mx-1" onClick={() => onDoctorDetails(row)}><Trans i18nKey="details"/></button>
            </div>
        )
    }

    const productActionsFormatter = (cell, row) => {
        return (
            <div>
                <button class="btn btn-danger mx-1" onClick={() => onProductDelete(row.productId)}><Trans i18nKey="delete"/></button>
                <button class="btn btn-primary mx-1" onClick={() => onProductDetails(row)}><Trans i18nKey="details"/></button>
            </div>
        )
    }

    const onDoctorDelete = async (id) => {
        if (window.confirm()) {
            console.log(await axios.delete(API_URL + "Doctors/" + id, headers));
        }
    }

    const onProductDelete = async (id) => {
        if (window.confirm()) {
            console.log(await axios.delete(API_URL + "Products/" + id, headers));
        }
    }
    
    const onProductDetails = (row) => {
        setProduct(row);
        setData(row.data ? row.data : []);
        setDataInput({name:'', value:0, normCoefficient:0});
        setFullscreen1(true);
        setShow1(true);
    }

    const onDoctorDetails = (row) => {
        setDoctor(row);
        setFullscreen(true);
        setShow(true);
    }

    const updateObj = (type, field, value) => {
        let newObj = {};

        if (type === "doctor") {
            Object.assign(newObj, doctor);
            newObj[field] = value;
            setDoctor(newObj);
        }
        else if (type === "product") {
            Object.assign(newObj, product);
            newObj[field] = value;
            setProduct(newObj);
        }
        else if (type === "dataInput") {
            Object.assign(newObj, dataInput);
            newObj[field] = value;
            setDataInput(newObj);
        }
    }

    const onSave = async (type) => {
        
        if (type === "doctor") {
            if (doctor.doctorId === undefined) {
                console.log(await axios.post(API_URL + "Doctors", doctor, headers));
                
            }
            else {
                console.log(await axios.put(API_URL + "Doctors/" + doctor.doctorId, doctor, headers));
            }
        }
        else if (type === "product") {
            console.log(product);

            if (product.productId === undefined) {
                console.log(await axios.post(API_URL + "Products", product, headers));
            }
            else {
                console.log("asd");
                console.log(await axios.put(API_URL + "Products/" + product.productId, product, headers));
            }
        }

        setFullscreen(false);
        setShow(false);
        setFullscreen1(false);
        setShow1(false);
    }

    const onAddData = () => {
        let newData = data;
        newData.push(dataInput);
        setData(newData);

        setDataInput({name:'', value:0, normCoefficient:0});
    }

    return(
        <div>
            <Header />
            <div class="container p-5">
                <div class="row m-5"><h2><Trans i18nKey="adminPanelHeader"/></h2></div>
            </div>

            <div class="container">
                <ul class="nav nav-tabs" id="myTab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="home-tab" data-bs-toggle="tab" data-bs-target="#home-tab-pane" type="button" role="tab" aria-controls="home-tab-pane" aria-selected="true"><Trans i18nKey="doctors"/></button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="profile-tab" data-bs-toggle="tab" data-bs-target="#profile-tab-pane" type="button" role="tab" aria-controls="profile-tab-pane" aria-selected="false"><Trans i18nKey="products"/></button>
                    </li>
                </ul>
                <div class="tab-content" id="myTabContent">
                    <div class="tab-pane fade show active" id="home-tab-pane" role="tabpanel" aria-labelledby="home-tab" tabindex="0">
                        <div class="container">
                            <div class="row m-5"><h2><Trans i18nKey="doctorsList"/></h2></div>
                            
                            <div class="row m-5">
                                <div class="col-2">
                                    <button class="btn btn-primary mx-1" onClick={() => onDoctorDetails({})}><Trans i18nKey="add"/></button>
                                </div>
                                <BootstrapTable data={ doctors } search={ true }> 
                                    <TableHeaderColumn dataField='doctorId' isKey><Trans i18nKey="doctorId"/></TableHeaderColumn>
                                    <TableHeaderColumn dataField='name'><Trans i18nKey="name"/></TableHeaderColumn>
                                    <TableHeaderColumn dataField='lastName'><Trans i18nKey="lastName"/></TableHeaderColumn>
                                    <TableHeaderColumn dataField='midName'><Trans i18nKey="midName"/></TableHeaderColumn>
                                    <TableHeaderColumn dataField='email'><Trans i18nKey="email"/></TableHeaderColumn>
                                    <TableHeaderColumn dataField='' dataFormat={ doctorActionsFormatter }><Trans i18nKey="actions"/></TableHeaderColumn>
                                </BootstrapTable>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="profile-tab-pane" role="tabpanel" aria-labelledby="profile-tab" tabindex="0">
                        <div class="container">
                            <div class="row m-5"><h2><Trans i18nKey="productsList"/></h2></div>
                            <div class="row m-5">
                                <div class="col-2">
                                    <button class="btn btn-primary mx-1" onClick={() => onProductDetails({})}><Trans i18nKey="add"/></button>
                                </div>
                                <BootstrapTable data={ products } search={ true }> 
                                    <TableHeaderColumn dataField='productId' isKey><Trans i18nKey="productId"/></TableHeaderColumn>
                                    <TableHeaderColumn dataField='name'><Trans i18nKey="title"/></TableHeaderColumn>
                                    <TableHeaderColumn dataField='discription'><Trans i18nKey="description"/></TableHeaderColumn>
                                    <TableHeaderColumn dataField='' dataFormat={ productActionsFormatter }><Trans i18nKey="actions"/></TableHeaderColumn>
                                </BootstrapTable>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="contact-tab-pane" role="tabpanel" aria-labelledby="contact-tab" tabindex="0">


                    </div>
                </div>
            </div>

            <Modal show={show} scrollable={true} fullscreen={fullscreen} onHide={() => setShow(false)}>
                <Modal.Header closeButton>
                    <Modal.Title><Trans i18nKey="doctorData"/></Modal.Title>
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
                            <input class="form-control" autocomplete="off" 
                                value={doctor.name} 
                                onChange={e => updateObj("doctor", "name", e.target.value)}/>
                        </div>
                        <div class="col-4 align-self-center">
                            <label class="form-label"><Trans i18nKey="email"/></label>
                            <input class="form-control" autocomplete="off"
                                value={doctor.email}
                                onChange={e => updateObj("doctor", "email", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-4 align-self-center">
                            <label class="form-label"><Trans i18nKey="lastName"/></label>
                            <input class="form-control" autocomplete="off"
                                value={doctor.lastName}
                                onChange={e => updateObj("doctor", "lastName", e.target.value)}/>
                        </div>
                        <div class="col-4 align-self-center">
                            <label class="form-label"><Trans i18nKey="phone"/></label>
                            <input class="form-control" autocomplete="off"
                                value={doctor.phone}
                                onChange={e => updateObj("doctor", "phone", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-4 align-self-center">
                            <label class="form-label"><Trans i18nKey="midName"/></label>
                            <input class="form-control" autocomplete="off"
                                value={doctor.midName}
                                onChange={e => updateObj("doctor", "midName", e.target.value)}/>
                        </div>
                        <div class="col-4 align-self-center"> 
                        </div>
                        </div>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <button type="button" class="btn btn-light" onClick={() => { setFullscreen(false); setShow(false); }}>
                        <Trans i18nKey="back"/>
                    </button>
                    <button variant="primary" type="button" class="btn btn-primary" onClick={() => { onSave("doctor"); }}>
                        <Trans i18nKey="save"/> 
                    </button>
                </Modal.Footer>
            </Modal>

            <Modal show={show1} scrollable={true} fullscreen={fullscreen1} onHide={() => setShow1(false)}>
                <Modal.Header closeButton>
                    <Modal.Title><Trans i18nKey="productData"/></Modal.Title>
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
                            <label class="form-label"><Trans i18nKey="title"/></label>
                            <input class="form-control" 
                                value={product.name} 
                                onChange={e => updateObj("product", "name", e.target.value)}/>
                        </div>
                        <div class="col-4 align-self-center">
                            <label class="form-label"><Trans i18nKey="description"/></label>
                            <input type="email" class="form-control" 
                                value={product.discription}
                                onChange={e => updateObj("product", "discription", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        { data.map((item) => (
                            <div class="col">
                            <div class="card">
                                <div class="card-header">{item.name}</div>
                                <div class="card-body">
                                    <p class="card-text"><Trans i18nKey="value"/>{item.value}</p>
                                    <p class="card-text"><Trans i18nKey="normCoefficient"/>{item.normCoefficient}</p>
                                </div>
                            </div>
                            </div>
                        ))}
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-3 align-self-center">
                            <label class="form-label"><Trans i18nKey="title"/></label>
                            <input class="form-control" 
                                value={dataInput.name} 
                                onChange={e => updateObj("dataInput", "name", e.target.value)}/>
                        </div>
                        <div class="col-3 align-self-center">
                            <label class="form-label"><Trans i18nKey="value"/></label>
                            <input class="form-control" 
                                value={dataInput.value} 
                                onChange={e => updateObj("dataInput", "value", e.target.value)}/>
                        </div>
                        <div class="col-3 align-self-center">
                            <label class="form-label"><Trans i18nKey="normCoefficient"/></label>
                            <input class="form-control" 
                                value={dataInput.normCoefficient} 
                                onChange={e => updateObj("dataInput", "normCoefficient", e.target.value)}/>
                        </div>
                        <div class ="col-1 align-self-center">
                            <button class="btn btn-primary" onClick={() => { onAddData() }}>
                                <Trans i18nKey="add"/> 
                            </button>
                        </div>
                        </div>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <button type="button" class="btn btn-light" onClick={() => { setFullscreen1(false); setShow1(false); }}>
                        <Trans i18nKey="back"/>
                    </button>
                    <button variant="primary" type="button" class="btn btn-primary" onClick={() => { onSave("product"); }}>
                        <Trans i18nKey="save"/> 
                    </button>
                </Modal.Footer>
            </Modal>


        </div>
    )
}