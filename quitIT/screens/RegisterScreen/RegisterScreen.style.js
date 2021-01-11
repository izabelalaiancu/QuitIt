import {StyleSheet, Dimensions} from 'react-native';

export default StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#324554',
    alignItems: 'center',
    justifyContent: 'center',
  },
  RegisterText: {
    fontSize: 30,
    color: 'white',
  },
  input: {
    width: '50%',
    backgroundColor: 'white',
    marginVertical: 5,
  },
  inputText: {
    paddingHorizontal: 20,
    paddingTop: 10,
  },
  errorInput: {
    borderWidth: 3,
    borderColor: 'red',
  },
  button: {
    marginTop: 30,
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
});
