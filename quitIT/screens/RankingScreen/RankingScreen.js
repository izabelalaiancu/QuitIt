import React from 'react';
import {View, TouchableOpacity, Image, Text, TextInput} from 'react-native';
import RNSecureStorage, {ACCESSIBLE} from 'rn-secure-storage';
import styles from './RankingScreen.style.js';
import {TokenFunction} from '../../constants/token';

const Ranking = (props) => {
  // RNSecureStorage.get('token')
  //   .then((value) => {
  //     console.log(value);
  //   })
  //   .catch((err) => {
  //     console.log(err);
  //   });
  return (
    <View style={styles.container}>
      <View style={styles.viewImagine}>
        <Image source={require('../../assets/aur.png')} style={styles.image} />
      </View>
      <Text style={styles.text}>Loc 1</Text>
      <View style={styles.viewImagine}>
        <Image
          source={require('../../assets/argint.png')}
          style={styles.image}
        />
      </View>
      <Text style={styles.text}>Loc 2</Text>
      <View style={styles.viewImagine}>
        <Image
          source={require('../../assets/cupru.png')}
          style={styles.image}
        />
      </View>
      <Text style={styles.text}>Loc 3</Text>
    </View>
  );
};

export default Ranking;
