import React from 'react';
import styles from './day-forecast-weather.module.css';

const dayForecast = ({data}) => {    
  return (
    <div className={styles.test}>      
      <div className={styles.baseDate}>
        <p>2022/10/05</p>
      </div>      
      <div className={styles.baseDate}>
        <p>temperature:</p>      
        <p>Max:</p><p>{data.temperature}</p>
        <p>Min:</p><p>{data.temperature}</p>
      </div>   
      <div className={styles.baseDate}>
        <p>Wind Speed:</p><p>{data.windSpeed}</p>
        <p>Wind Direction:</p><p>{data.windDirection}</p>
      </div>
      <div className={styles.baseDate}>
        <p>Resume day:</p>
        <p>{data.detailedForecast}</p>
      </div>
    </div>
  );  
};

export default dayForecast;