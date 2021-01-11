import React from 'react';
import {NavigationContainer} from '@react-navigation/native';
import {createStackNavigator} from '@react-navigation/stack';

import Login from '../screens/LoginScreen/LoginScreen.page';
import Register from '../screens/RegisterScreen/RegisterScreen.page';
import Side from './inAppNavigator';

const Stack = createStackNavigator();

function LoginNavigator() {
  console.log('login navigator');
  return (
    <Stack.Navigator
      initialName={'Login'}
      screenOptions={{gestureEnabled: false, headerShown: false}}>
      <Stack.Screen name="Login" component={Login} />
      <Stack.Screen name="Register" component={Register} />
      <Stack.Screen name="Side" component={Side} />
    </Stack.Navigator>
  );
}

export default LoginNavigator;
