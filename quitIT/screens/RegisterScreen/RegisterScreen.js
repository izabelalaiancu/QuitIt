import React, {useState} from 'react';
import {View, TouchableOpacity, Image, Text, TextInput} from 'react-native';
import {validate} from '../../constants/checkInputRegister.js';
import styles from './RegisterScreen.style.js';

const Register = (props) => {
  const [cont, setCont] = useState({
    nume: '',
    numeValidate: true,
    prenume: '',
    prenumeValidate: true,
    email: '',
    emailValidate: true,
    parola: '',
    parolaValidate: true,
  });
  const submit = () => {
    if (
      cont.numeValidate &&
      cont.prenumeValidate &&
      cont.emailValidate &&
      cont.parolaValidate &&
      cont.nume !== '' &&
      cont.prenume !== '' &&
      cont.email !== '' &&
      cont.parola !== ''
    ) {
      return true;
    } else {
      return false;
    }
  };
  const sendData = (account) => {
    if (submit()) {
      props.sendData(account);
    } else {
      alert('Input incorect');
    }
  };
  return (
    <View style={styles.container}>
      <Image source={require('../../assets/users.png')} />
      <Text style={styles.RegisterText}>Register</Text>
      <View
        style={[styles.input, !cont.numeValidate ? styles.errorInput : null]}>
        <TextInput
          style={styles.inputText}
          placeholder="First Name"
          placeholderTextColor="black"
          onChangeText={(text) => {
            console.log(text);
            validate(text, 'nume', cont, setCont);
          }}
        />
      </View>
      <View
        style={[
          styles.input,
          !cont.prenumeValidate ? styles.errorInput : null,
        ]}>
        <TextInput
          style={styles.inputText}
          placeholder="Last Name"
          placeholderTextColor="black"
          onChangeText={(text) => validate(text, 'prenume', cont, setCont)}
        />
      </View>
      <View style={styles.input}>
        <TextInput
          style={styles.inputText}
          placeholder="Email"
          placeholderTextColor="black"
          onChangeText={(text) => validate(text, 'email', cont, setCont)}
        />
      </View>
      <View
        style={[styles.input, !cont.parolaValidate ? styles.errorInput : null]}>
        <TextInput
          style={styles.inputText}
          placeholder="Password"
          placeholderTextColor="black"
          onChangeText={(text) => validate(text, 'parola', cont, setCont)}
          secureTextEntry={true}
        />
      </View>
      <TouchableOpacity
        style={styles.button}
        onPress={async () => {
          await sendData(cont);
          props.props.navigation.pop();
        }}>
        <Text style={styles.buttonsText}>Sign Up</Text>
      </TouchableOpacity>
    </View>
  );
};

export default Register;
