import React from 'react';
import {View, Text, Image, TouchableOpacity} from 'react-native';
import RNSecureStorage, {ACCESSIBLE} from 'rn-secure-storage';
import styles from './ListOfVices.style';

const ListOfVices = (props) => {
  RNSecureStorage.get('token')
    .then((value) => {
      console.log('token aici');
      console.log(value); // Will return direct value
    })
    .catch((err) => {
      console.log(err);
    });
  return (
    <View style={styles.container}>
      <TouchableOpacity>
        <View style={styles.viewImagine}>
          <Image
            source={require('../../assets/mananci.jpg')}
            style={styles.image}
          />
        </View>
      </TouchableOpacity>
      <TouchableOpacity>
        <View style={styles.viewImagine}>
          <Image
            source={require('../../assets/bei.jpg')}
            style={styles.image}
          />
        </View>
      </TouchableOpacity>
      <TouchableOpacity>
        <View style={styles.viewImagine}>
          <Image
            source={require('../../assets/fumezi.jpg')}
            style={styles.image}
          />
        </View>
      </TouchableOpacity>
    </View>
  );
};

export default ListOfVices;
