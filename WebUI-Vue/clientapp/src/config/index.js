export default {
  clientid: process.env.VUE_APP_MSAL_CLIENTID, //'b7d74dd5-86a5-458a-8b2c-926bf4f79c45',
  redirecturl: process.env.VUE_APP_MSAL_REDIRECTURL, //'http://localhost:8080/callback',
  authority: process.env.VUE_APP_MSAL_AUTHORITY, //'https://login.microsoftonline.com/ce43a082-3244-4c00-b7d9-83699b3b98fc',
  graphscopes: ['user.read'],
  graphendpoint: process.env.VUE_APP_MSAL_GRAPHENDPOINT, //'https://graph.microsoft.com/v1.0/me',
  appinsightsid: process.env.VUE_APP_MSAL_APPINSIGHTSID, //''
}
