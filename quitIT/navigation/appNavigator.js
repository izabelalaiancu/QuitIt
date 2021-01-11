import React, {useState} from 'react';
import {NavigationContainer} from '@react-navigation/native';
import RNSecureStorage from 'rn-secure-storage';

import InAppNavigator from './inAppNavigator';
import LoginNavigator from './loginNavigator';

function AppNavigator() {
  console.log('am intrat');
  const [istoken, setIsToken] = useState(false);
  RNSecureStorage.exists('token').then(
    (res) => {
      res ? setIsToken(true) : setIsToken(false);
    },
    (err) => {
      console.log(err);
    },
  );
  return (
    <NavigationContainer>
      {istoken ? <InAppNavigator /> : <LoginNavigator />}
    </NavigationContainer>
  );
}

export default AppNavigator;
