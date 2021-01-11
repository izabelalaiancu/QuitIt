import React, {useEffect, useState} from 'react';
import Register from './RegisterScreen.js';

const RegisterPage = (props) => {
  const sendData = (account) => {
    fetch('https://quitit-dev-as.azurewebsites.net/api/account/register', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        firstName: account.nume,
        lastName: account.prenume,
        email: account.email,
        password: account.parola,
      }),
    })
      .then((response) => {
        if (!response.ok) {
          throw response;
        }
        return response.json();
      })
      .catch((error) => {
        console.log('eroare');
        console.log(error);
      });
  };
  return <Register sendData={sendData} props={props} />;
};

export default RegisterPage;
