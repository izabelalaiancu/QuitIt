import React, {useState, createContext} from 'react';

export const TokenFunction = (whatToDo, tokenAuth = '') => {
  const [token, setToken] = useState('');
  const getToken = () => {
    return token;
  };
  const seteazaToken = (t) => {
    setToken(t);
  };
  switch (whatToDo) {
    case 'get':
      return getToken();
    case 'set':
      return seteazaToken(tokenAuth);
  }
};
