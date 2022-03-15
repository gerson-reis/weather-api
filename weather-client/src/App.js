import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import ForecasResult from './components/forecast-result/forecast-result';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
          <main className="AppBody">
            <Routes>
              <Route path="/" element={<ForecasResult/>} />
              {/* <Route path="/home/" element={<DayForecast/>} /> */}
            </Routes>
          </main>
      </BrowserRouter>
    </div>
  );
}


export default App;
