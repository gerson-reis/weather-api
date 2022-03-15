import React from 'react';
import styles from './forecast-result.module.css';
import DayForecast from '../day-forecast-weather/day-forecast-weather';
import {GET_FORECAST} from '../../api';

const ForecasResult = () => {
    const [result, setResult] = React.useState(null);
    const [address, setAddress] = React.useState(null);
    
    async function getForecastFromApi() {
        const { url, options } = GET_FORECAST("4600 Silver Hill Rd, Washington, DC 20233");
        const response = await fetch(url, options);
        setResult(await response.json());        
    }

    return (
        <div className={styles.test}>
            <form onSubmit={getForecastFromApi}>
                <input type="text" onchange={() => setAddress()}></input>
            </form>     
            <button onClick={getForecastFromApi}>Forecast</button>
            {
                result && result.map((forecast) => (                    
                   <DayForecast key={Math.random()} data={forecast}/>
                ))
            }
        </div>
    );  
};

export default ForecasResult;
