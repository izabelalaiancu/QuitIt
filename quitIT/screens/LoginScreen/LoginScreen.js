import React from 'react';
import {View, TouchableOpacity, Image, Text, TextInput} from 'react-native';
import styles from './LoginScreen.style.js';

const Login = (props) => {
  let email = '';
  let password = '';

  return (
    <View style={styles.container}>
      <View style={styles.viewLogo}>
        <Image source={require('../../assets/LOGO.png')} style={styles.logo} />
      </View>
      <Text style={styles.welcomeText}>Welcome!</Text>
      <View>
        <TextInput
          style={styles.input}
          placeholder="username"
          placeholderTextColor="black"
          onChangeText={(text) => (email = text)}
        />
        <TextInput
          style={styles.input}
          placeholder="password"
          placeholderTextColor="black"
          onChangeText={(text) => (password = text)}
          secureTextEntry={true}
        />
      </View>
      <View style={styles.buttonsView}>
        <TouchableOpacity
          style={styles.button}
          onPress={() => {
            props.login({username: email, password: password});
          }}>
          <Text style={styles.buttonsText}>Login</Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={styles.button}
          onPress={() => props.props.navigation.navigate('Register')}>
          <Text style={styles.buttonsText}>Register</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

export default Login;
