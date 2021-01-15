import React from 'react';
import {Platform, TouchableOpacity, Text} from 'react-native';
import {createStackNavigator} from '@react-navigation/stack';

import ListOfVices from '../screens/ListOfVices/ListOfVices.page';
import Ranking from '../screens/RankingScreen/RankingScreen.page';

import HeaderButton from '../components/HeaderButtonComponent';

const Stack = createStackNavigator();

function StackNavigator(props) {
  console.log('stack navigator');
  return (
    <Stack.Navigator
      initialName={'Viciile mele'}
      screenOptions={{
        headerLeft: () => {
          <TouchableOpacity onPress={() => props.navigation.toggleDrawer()}>
            <Text>Menu</Text>
          </TouchableOpacity>;
        },
      }}>
      <Stack.Screen name="Viciile mele" component={ListOfVices} />
      <Stack.Screen name="Ranking" component={Ranking} />
    </Stack.Navigator>
  );
}

export default StackNavigator;
