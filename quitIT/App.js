import React from 'react';
import Login from './screens/LoginScreen/LoginScreen.page.js';
import Register from './screens/RegisterScreen/RegisterScreen.page.js';
import Ranking from './screens/RankingScreen/RankingScreen.page.js';
import AppNavigator from './navigation/appNavigator';

const App = () => {
  console.log('am intrat in app');
  return <AppNavigator />;
};

export default App;
