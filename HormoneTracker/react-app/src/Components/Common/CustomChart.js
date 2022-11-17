import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import { Link, redirect } from "react-router-dom";
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';

export default function CustomChart(props) {
    
    return(
        <LineChart width={1000} height={300} margin={{ top: 5, right: 20, bottom: 5, left: 0 }}>
            <Line type="monotone" dataKey="coef" data={props.chartData} isAnimationActive={false} stroke="#8884d8" />
            <CartesianGrid stroke="#ccc" strokeDasharray="5 5" />
            <XAxis dataKey="name" />
            <YAxis />
            <Tooltip />
        </LineChart>
    )
}