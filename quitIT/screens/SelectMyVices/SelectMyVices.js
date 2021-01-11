import React, {useState} from 'react';
import {View, Text, Image, TouchableOpacity} from 'react-native';
import RNSecureStorage from 'rn-secure-storage';
import SelectMultiple from 'react-native-select-multiple';
import styles from './SelectMyVices.style';

const vices = ['Alcool', 'Mancare', 'Tutun'];
const MyVices = (props) => {
  RNSecureStorage.get('token')
    .then((value) => {
      console.log('token aici');
      console.log(value); // Will return direct value
    })
    .catch((err) => {
      console.log(err);
    });

  const [selected, setSelected] = useState([]);
  const onSelectionChange = (selectedVices) => {
    setSelected(selectedVices);
  };

  return (
    <View style={styles.container}>
      <SelectMultiple
        items={vices}
        selectedItems={selected}
        onSelectionChange={onSelectionChange}
      />
    </View>
  );
};

export default MyVices;
