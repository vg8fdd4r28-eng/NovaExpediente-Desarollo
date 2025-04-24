import Oidc from "oidc-client";
import axiosSecurity from "../config/axiosSecurity";
import { GetClockService } from "./request";

var mgr = new Oidc.UserManager({
  authority: process.env.VUE_APP_authority, //'https://localhost:44310',
  client_id: "NovaExpedienteUI",
  redirect_uri: process.env.VUE_APP_URL + "/callback", //'http://localhost:8080/callback',
  response_type: "code", //'id_token token code',
  scope: "openid profile email NovaExpediente_api",
  post_logout_redirect_uri: process.env.VUE_APP_URL, //'http://localhost:8080',
  userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),

  automaticSilentRenew: true,
  silent_redirect_uri: process.env.VUE_APP_URL + "/silent-renew", //silent-renew.html',
  accessTokenExpiringNotificationTime: 10,

  clockService: {
    getEpochTime() {
      return GetClockService();
    }
  },
  clockSkew: 6000

  //   filterProtocolClaims: true,
  //   loadUserInfo: true
});

Oidc.Log.logger = console;
Oidc.Log.level = Oidc.Log.INFO;

// mgr.events.addUserLoaded(async function (user) {
//   await store.dispatch('refreshUserInfo');
//   await store.dispatch('ensureUserIsKnown');
// });

// mgr.events.addAccessTokenExpiring(function () {
//   // console.log('AccessToken Expiring：', arguments);
// });

// mgr.events.addAccessTokenExpired(function () {
// //   mgr.signoutRedirect().then(function (resp) {
// //     store.commit('set_user', null); //clear user details in vuex
// //   }).catch(function (err) {
// //     console.log(err)
// //   })
//     alert("Token expiró");
// });

// mgr.events.addSilentRenewError(function () {
//   console.error('Silent Renew Error：', arguments);
// });

// mgr.events.addUserSignedOut(function () {
//   mgr.signoutRedirect().then(function (resp) {
//     store.commit('set_user', null); //clear user details in vuex
//   }).catch(function (err) {
//     console.log(err)
//   })
// });

// class SecurityService {

//   constructor(){
//     // console.log('Creating SecurityService instance')
//   }

//   async getUser () {
//     let self = this;
//     return new Promise((resolve, reject) => {
//       mgr.getUser().then(function (user) {
//         if (user == null) {
//           return resolve(null)
//         } else {
//           return resolve(user)
//         }
//       }).catch(function (err) {
//         console.log(err)
//         return reject(err)
//       });
//     })
//   }

//   signIn (returnToUrl) {
//     returnToUrl ? mgr.signinRedirect({ state: returnToUrl })
//         : mgr.signinRedirect();
//   }

//   signOut () {
//     var self = this;
//     mgr.signoutRedirect().then(function (resp) {
//       // console.log('signed out', resp);
//     }).catch(function (err) {
//       console.log(err)
//     })
//   }
// }

// let service = new SecurityService();

// export {service as default, mgr};

export default mgr;
