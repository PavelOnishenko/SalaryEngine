import React from 'react';
import axios from 'axios';

function App() {
  const handleClick = async () => {
    try {
      const response = await axios.get('https://127.0.0.1:32770/initiate');
      console.log(response.data);
    } catch (error) {
      if(error.message !== 'Network Error') {
        console.error('Error making GET request:', error);
      }
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <button onClick={handleClick}>Initiate</button>
      </header>
    </div>
  );
}

export default App;