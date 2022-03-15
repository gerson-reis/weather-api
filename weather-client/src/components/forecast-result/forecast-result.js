import React from 'react';
import styles from './forecast-result.module.css';
import DayForecast from '../day-forecast-weather/day-forecast-weather';
import {GET_FORECAST} from '../../api';

const ForecasResult = () => {
    const [result, setResult] = React.useState(null);
    const [address, setAddress] = React.useState('');
    
    async function getForecastFromApi(event) {
        if(address != '')
        {
            event.preventDefault();
            const { url, options } = GET_FORECAST(address);
            const response = await fetch(url, options);
            setResult(await response.json());   
        }     
    }

    return (
        <div className={styles.test}>
            <form onSubmit={getForecastFromApi}>
                <input className="imput-address" type="text" name="address" value={address} onChange={e => setAddress(e.target.value)}></input>
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
