import React, {useState} from 'react';
import {
  createDrawerNavigator,
  DrawerContentScrollView,
  DrawerItemList,
  DrawerItem,
} from '@react-navigation/drawer';
import RNSecureStorage, {ACCESSIBLE} from 'rn-secure-storage';

import StackNavigator from './stackNavigator';
import Ranking from '../screens/RankingScreen/RankingScreen.page';
import MyVices from '../screens/SelectMyVices/SelectMyVices.page';
import Wishlist from '../screens/Wishlist';

function CustomDrawerCotent(props) {
  const [isShown, setIsShown] = useState(false);

  const proceedHandler = () => {
    RNSecureStorage.remove('token')
      .then((val) => {
        console.log(val);
      })
      .catch((err) => {
        console.log(err);
      });
    setIsShown(false);
    // props.navigation.pop();
  };

  return (
    <DrawerContentScrollView {...props}>
      <DrawerItemList {...props} />
      <DrawerItem label="Logout" onPress={() => proceedHandler()} />
    </DrawerContentScrollView>
  );
}

const Drawer = createDrawerNavigator();

function MyDrawer() {
  return (
    <Drawer.Navigator
      drawerType="slide"
      drawerContent={(props) => <CustomDrawerCotent {...props} />}>
      <Drawer.Screen name="Viciile mele" component={StackNavigator} />
      <Drawer.Screen name="Ranking" component={Ranking} />
      <Drawer.Screen name="Selecteaza Viciile" component={MyVices} />
      <Drawer.Screen name="Wishlist" component={Wishlist} />
    </Drawer.Navigator>
  );
}

export default MyDrawer;
