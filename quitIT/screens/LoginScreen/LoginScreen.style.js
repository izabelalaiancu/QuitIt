import {StyleSheet, Dimensions} from 'react-native';

export default StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#324554',
    alignItems: 'center',
    justifyContent: 'center',
  },
  welcomeText: {
    fontSize: 30,
    color: 'white',
  },
  inputsView: {},
  input: {
    padding: 1,
    margin: 5,
    borderBottomColor: 'grey',
    borderBottomWidth: 2,
    width: Dimensions.get('window').width / 2,
    backgroundColor: 'white',
    paddingHorizontal: 5,
  },
  buttonsView: {
    marginTop: 30,
  },
  button: {
    borderRadius: 50,
    backgroundColor: 'gray',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 10,
    margin: 5,
    paddingHorizontal: 30,
  },
  buttonsText: {
    fontSize: 20,
    color: 'white',
  },
  viewLogo: {
    width: 350,
    height: 200,
  },
  logo: {
    width: '100%',
    height: '100%',
  },
});
