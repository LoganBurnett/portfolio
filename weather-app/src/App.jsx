import { useState, useEffect } from "react";
import axios from "axios";
import "./App.css";

function App() {
  const [searchTerm, setSearchTerm] = useState("");
  const [weatherInfo, setWeatherInfo] = useState({});
  const [error, setError] = useState(null);

  useEffect(() => {
    axios
      .get(
        "https://api.openweathermap.org/data/2.5/weather?q=london&units=imperial&appid={key}"
      )
      .then((res) => {
        setWeatherInfo(res.data);
      })
      .catch((err) => {
        setError(err.message);
      });
  }, []);

  useEffect(() => {
    if (!searchTerm) {
      return;
    }

    setError(null);

    axios
      .get(
        `https://api.openweathermap.org/data/2.5/weather?q=${searchTerm}&units=imperial&appid={key}`
      )
      .then((res) => {
        setWeatherInfo(res.data);
      })
      .catch((err) => {
        setError(err.message);
      });
  }, [searchTerm]);

  const handleChange = (event) => {
    setSearchTerm(event.target.value);
  };

  return (
    <>
      <section className="search-container">
        <input
          type="text"
          placeholder="Search for a city"
          value={searchTerm}
          onChange={handleChange}
        />
      </section>
      <section>
        <div>
          <h1>Weather in {weatherInfo.name}</h1>
          <img
            src={`https://openweathermap.org/img/wn/${weatherInfo.weather?.[0]?.icon}@2x.png`}
            alt={`Image for ${weatherInfo.weather?.[0]?.description}`}
          />
          <h2>{weatherInfo.weather?.[0]?.description.toUpperCase()}</h2>
          <h3>Temperature: {Math.round(weatherInfo.main?.temp)}°F</h3>
          <h3>Humidity: {weatherInfo.main?.humidity}%</h3>
          <h3>Wind Speed: {weatherInfo.wind?.speed}m/s</h3>
        </div>
      </section>
    </>
  );
}

export default App;
