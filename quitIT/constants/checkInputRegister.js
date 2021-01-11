export function validate(text, type, cont, setCont) {
  console.log('am intrat');
  console.log(cont);
  console.log('text: ' + text);
  console.log('type: ' + type);
  const alph = /^[a-zA-Z]+$/;
  const alphnum = /^[ A-Za-z0-9_@./#&+-]*$/;
  const emailReg = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
  switch (type) {
    case 'nume':
      if (alph.test(text)) {
        setCont({...cont, numeValidate: true, nume: text});
      } else {
        setCont({...cont, numeValidate: false});
      }
      break;
    case 'prenume':
      if (alph.test(text)) {
        setCont({...cont, prenumeValidate: true, prenume: text});
      } else {
        setCont({...cont, prenumeValidate: false});
      }
      break;
    case 'email':
      if (emailReg.test(text)) {
        setCont({...cont, emailValidate: true, email: text});
      } else {
        setCont({...cont, emailValidate: false});
      }
      break;
    case 'parola':
      if (alphnum.test(text)) {
        setCont({...cont, parolaValidate: true, parola: text});
      } else {
        setCont({...cont, emailValidate: false});
      }
  }
}
