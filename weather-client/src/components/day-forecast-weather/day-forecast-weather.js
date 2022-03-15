import React from 'react';
import styles from './day-forecast-weather.module.css';

const dayForecast = ({data}) => {    
  return (
    <div className={styles.base}>      
      <div className={styles.baseDate}>
        <p>Start time: {data.startTime}</p>
        <p>End time: {data.endTime}</p>
      </div>      
      <div className={styles.baseDate}>
        <p>temperature: {data.temperature}</p>
      </div>   
      <div className={styles.baseDate}>
        <p>Wind Speed:</p><p>{data.windSpeed}</p>
      </div>
      <div className={styles.baseDate}>
        <p>Resume day: {data.detailedForecastDay}</p>
      </div>
    </div>
  );  
};

export default dayForecast;