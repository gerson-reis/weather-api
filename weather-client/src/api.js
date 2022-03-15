export const API_URL = 'https://localhost:7167';

export function GET_FORECAST(address) {
    return {
      url: `${API_URL}/api/get-forecast-from/?address=${address}`,
      options: {
        method: 'get',
        headers: {
          'Content-Type': 'application/json',
        },
      },
    };
  }

  