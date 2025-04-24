import 'core-js/stable'
import Vue from 'vue'
import VueFormulate from '@braid/vue-formulate'
import CoreuiVue from '@coreui/vue'
import { es } from '@braid/vue-formulate-i18n'

import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import VueToast from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-sugar.css';
//import PrimeVue from "primevue/config";
import App from './App'
import router from './router'
import { iconsSet as icons } from './assets/icons/icons.js'
import store from './store'
import '../node_modules/timeline-vuejs/dist/timeline-vuejs.css'

import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import ColumnGroup from 'primevue/columngroup'
import InputText from 'primevue/inputtext'
import Calendar from 'primevue/calendar'
import DropDown from 'primevue/dropdown'
import ProgressBar from 'primevue/progressbar'
import SplitButton  from 'primevue/splitbutton'
import MultiSelect  from 'primevue/multiselect'
import Chips from 'primevue/chips';
import Checkbox from 'primevue/checkbox';
import SelectButton from 'primevue/selectbutton';
import Dialog from 'primevue/dialog';
import InputSwitch from 'primevue/inputswitch';
import Tooltip from 'primevue/tooltip';
import TabView from 'primevue/tabview';
import TabPanel from 'primevue/tabpanel';
import FileUpload from 'primevue/fileupload';
import OverlayPanel from 'primevue/overlaypanel';
import Listbox from 'primevue/listbox';
//import ConfirmationService from 'primevue/confirmationservice';
//import ConfirmDialog from 'primevue/confirmdialog';
import Galleria from 'primevue/galleria';

//import ToastService from 'primevue/toastservice';
import 'primevue/resources/themes/saga-blue/theme.css'
import 'primevue/resources/primevue.min.css'
import 'primeicons/primeicons.css'
import 'primeflex/primeflex.css';


import FileUploadFormulate from "./views/generales/Componentes/Formulate/FileUpload";
import InputSwitchFormulate from "./views/generales/Componentes/Formulate/InputSwitch";
import InputMultiSelect from "./views/generales/Componentes/Formulate/MultiSelect";
import RichTextBox from "./views/generales/Componentes/Formulate/RichTextBox";

import VoerroTagsInput from '@voerro/vue-tagsinput';
import '../node_modules/@voerro/vue-tagsinput/dist/style.css'

import VueApexCharts from 'vue-apexcharts'


import VueTimepicker from 'vue2-timepicker'
import 'vue2-timepicker/dist/VueTimepicker.css'
Vue.component('VueTimepicker',VueTimepicker);


import TimePickerFormulate from "./views/administrative/Componentes/TimePicker";
import Cloudinary from "cloudinary-vue";
import Multiselect from 'vue-multiselect';
import EmojiPicker from 'vue-emoji-picker';
// import * as VueGoogleMaps from 'vue2-google-maps'
import { LMap, LTileLayer, LMarker,LIcon } from 'vue2-leaflet';
import 'leaflet/dist/leaflet.css';
import Chat from 'vue-advanced-chat';
import { register } from 'vue-advanced-chat'
register()


Vue.component('l-map', LMap);
Vue.component('l-tile-layer', LTileLayer);
Vue.component('l-marker', LMarker);
Vue.component('l-icon', LIcon);

Vue.component('FileUploadFormulate',FileUploadFormulate);
Vue.component('InputSwitchFormulate',InputSwitchFormulate);
Vue.component('InputMultiSelect',InputMultiSelect);
Vue.component('TimePickerFormulate',TimePickerFormulate);
Vue.component('RichTextBox', RichTextBox);
//Vue.component('vue-advanced-chat', Chat);


//Vue.use(ConfirmationService);

Vue.use(VueFormulate, {
  classes: {
      outer: "form-group",
      label: ["text-left", "w-100"],
      error: ["small", "text-danger", "text-left"],
      errors: "list-unstyled"
  },
  plugins: [ es ],
  locale:"es",
  validationNameStrategy: ['label', 'validationName', 'name',  'type'],
  // slotProps: {
  //   help: ['extraHelpText']
  // },
  library: {
    myfileupload: {
      classification: 'file',
      component: 'FileUploadFormulate',
      slotProps: {
        component: ['tipoDocumento','estadoSolicitud','index','eliminada','visorPadre','habilitarControles','isCSV']
      }
    },
    myInputSwitch: {
      classification: 'text',
      component: 'InputSwitchFormulate'
    },
    myTimePicker: {
      classification: 'time',
      component: 'TimePickerFormulate'
    },
     myInputMultiSelect: {
      classification: 'text',
      component: 'InputMultiSelect',
      slotProps: {
        component: ['options','disabled','trackby']
      }
    },
    myRichTextBox: {
      classification: 'text',
      component: 'RichTextBox',
      slotProps: ['disabled'],
    },
  }
});

Vue.config.performance = true;
Vue.use(CoreuiVue);
Vue.use(Loading);
Vue.use(VueToast);

Vue.use(VueApexCharts)
Vue.component('apexchart', VueApexCharts)

//Vue.use(PrimeVue);
//Vue.use(ConfirmationService);

//app.use(PrimeVue, { ripple: true });
//Vue.use(ToastService);

// Vue.use(VueGoogleMaps, {
//   load: {
//     key: 'YOUR_API_TOKEN',
//     libraries: 'places', // This is required if you use the Autocomplete plugin
//     // OR: libraries: 'places,drawing'
//     // OR: libraries: 'places,drawing,visualization'
//     // (as you require)
 
//     //// If you want to set the version, you can do so:
//     // v: '3.26',
//   },
 
//   //// If you intend to programmatically custom event listener code
//   //// (e.g. `this.$refs.gmap.$on('zoom_changed', someFunc)`)
//   //// instead of going through Vue templates (e.g. `<GmapMap @zoom_changed="someFunc">`)
//   //// you might need to turn this on.
//   // autobindAllEvents: false,
 
//   //// If you want to manually install components, e.g.
//   //// import {GmapMarker} from 'vue2-google-maps/src/components/marker'
//   //// Vue.component('GmapMarker', GmapMarker)
//   //// then disable the following:
//   // installComponents: true,
// })


Vue.component('DataTable', DataTable);
Vue.component('Column', Column);
Vue.component('ColumnGroup', ColumnGroup);
Vue.component('InputText', InputText);
Vue.component('Calendar', Calendar);
Vue.component('DropDown', DropDown);
Vue.component('ProgressBar', ProgressBar);
Vue.component('SplitButton', SplitButton);
Vue.component('MultiSelect', MultiSelect);
Vue.component('Chips', Chips);
Vue.component('FileUpload', FileUpload);
Vue.component('Checkbox', Checkbox);
Vue.component('SelectButton', SelectButton);
Vue.component('Dialog', Dialog);
Vue.component('InputSwitch', InputSwitch);
Vue.component('TabView', TabView);
Vue.component('TabPanel', TabPanel);
Vue.component('OverlayPanel', OverlayPanel);
Vue.component('Listbox', Listbox);
Vue.component('Galleria', Galleria);


//Vue.component('ConfirmDialog', ConfirmDialog);

Vue.prototype.$log = console.log.bind(console)
Vue.directive('tooltip', Tooltip);

Vue.component('tags-input', VoerroTagsInput);

// // integración Microsoft Authentication Library
// import AuthService from './msal'
// Vue.prototype.$AuthService = new AuthService()
// Vue.prototype.$currentUser = null

Vue.use(Cloudinary, {
  configuration: { 
    cloudName: "novacomp",
    secure: true }
});

Vue.component('multiselect', Multiselect)
Vue.component('emojipicker', EmojiPicker)


/**
 * mecanismo de autenticación con OIDC-CLIENT
 */
 import mgr from './security/security.js';
 import Axios from './config/axios.js' ;
import { isNull } from 'underscore'
 /**
 * Variables globales
 */
const globalData = {
  isAuthenticated: false,
  user: '',
  mgr: mgr,
  userPermissions: ["AccederDashboard"],
  userRoles: [],
  infoColaboradorActual : {
    id: null,
    identificacion: null,
    nombre: "",
    correo: "",
    nombreUsuario:"",
    contadorInbox: 0,
    contadorAdminInbox: 0
  }
};

/**
 * Métodos globales
 */
const globalMethods = {
  /**
   * En caso que el usuario actual no se ha autenticado entonces se dispara
   * el mecanismo de autenticación con Identity Server
   *
   * @param {string} returnPath 
   */
  async authenticate(returnPath) {
    const user = await this.$root.getUser(); //see if the user details are in local storage
    if (!!user) {
      this.isAuthenticated = true;
      this.user = user;
    } else {
      await this.$root.signIn(returnPath);
    }
  },

  /**
   * Obtiene del local storage la información del usuario autenticado
   * @returns La información del usuario autenticado
   */
  async getUser () {
    try {
      let user = await this.mgr.getUser();
      return user;
    } catch (err) {
      console.log(err);
    }
  },

  /**
   * De acuerdo a la configuración del OIDC-CLIENT se redirecciona al usuario
   * a la página de LOGIN del Identity Server
   * @param {string} returnPath
   */
  signIn (returnPath) {    
    returnPath ? this.mgr.signinRedirect({ state: returnPath })
        : this.mgr.signinRedirect();
  },

  async setUser() {
    const user = await this.$root.getUser(); //see if the user details are in local storage
    if (!!user) {
      this.isAuthenticated = true;
      this.user = user;
    }
  },

  hasPermission(permission){
    return this.userPermissions.some(p => p == permission);
  },

  refreshInboxCounter(){
    let counter = document.getElementById('inboxCounterId');
    counter.innerText = this.infoColaboradorActual.contadorInbox;
  },

  refreshInboxAdminCounter(){
    
    const permisoAcced = this.$root.userPermissions.find(item => item === "AccederMinimoPermiso");
    
    if(permisoAcced){
      let counter = document.getElementById('inboxAdminCounterId');
      counter.innerText = this.infoColaboradorActual.contadorAdminInbox;
    }
  
  },

  async saveUserLog(to){
    try {
      if(this.user && this.user != "") {
        await Axios.post("/Log/SaveUserLog",
        { "email": this.user.profile.email,
          "opcion": to.path,
          "nombreOpcion": to.name,
          "params": JSON.stringify(to.params),
          "subjectId": this.user.profile.sub,
          "nombreUsuario": this.user.profile.name
        });
      }
      else{
        await this.getUser()
        .then(response => {    
                
          Axios.post("/Log/SaveUserLog",
            { "email": this.user.profile.email,
              "opcion": to.path,
              "nombreOpcion": to.name,
              "params": JSON.stringify(to.params),
              "subjectId": this.user.profile.sub,
              "nombreUsuario": this.user.profile.name
            });
        })
      }
    } catch (error) {
      console.log(error);
        }
  }
};

// // intercepción de navegación a funcionalidades del menú
// router.beforeResolve((to, from, next) => {
//   if (process.env.VUE_APP_REQUIERE_AUTENTICACION === "si")
//   {
//     if (to.fullPath === "/auth/login" && router.app.$AuthService.getUser()){
//       next({ path: '/' });
//     }
//     else if (to.matched.some(record => record.meta.requiresAuth)) {    
//       if (!router.app.$AuthService.getUser()) {
//           next({ path: '/auth/login' });
//       } else {
//           next();
//       }
//     } else {
//       next();
//     }
//   } else {
//     next();
//   }  
// })

let expediente = new Vue({
  el: '#app',
  router,
  store,
  icons,
  data: globalData,
  methods: globalMethods,
  template: '<App/>',
  components: {
    App
  }
})


/**
 * Se verifica si una opción del menú requiere que el usuario se haya
 * autenticado. En caso que no se tenga la información del usuario
 * se redirige a la página de LOGIN del Identity Server
 */



 router.beforeResolve(async (to, from, next) => {
  // if (process.env.VUE_APP_REQUIERE_AUTENTICACION === "si")
  // {
    let app = router.app.$data || {isAuthenticated: false} ;

    await router.app.saveUserLog(to);

    if (app.isAuthenticated) {
      if (to.meta.permission) {
        let isAllowed = router.app.userPermissions.some(p => p == to.meta.permission);
        if (! isAllowed) return next('/')
      }
      next();
    } else if (to.matched.some(record => record.meta.requiresAuth)) {
      //la autenticación es requerida. Se dispara el proceso de LOGIN, enviando la ruta de retorno cuando
      //el usuario se autentica
      router.app.authenticate(to.path).then(() => {
        if (to.meta.permission) { //s && to.meta.permissions.length > 0) {
          let isAllowed = router.app.userPermissions.some(p => p == to.meta.permission); //to.meta.permissions.includes(p));
          if (! isAllowed){
            console.log('URL no autorizado:' + to.path);
            return next('/');
          }
        }
       // console.log('authenticating a protected url:' + to.path);
        next();
      });
    } else {
      //no se requiere usuario autenticado, se navega normal a la opción del menú
      next()
    }
  // }
});



/**
 * Cada llamada a un API se intercepta para incluir el token de acceso del usuario autenticado
 */
 Axios.interceptors.request.use((config) => {
    const user = expediente.user;
    if (user) {
      const authToken = user.access_token;
      if (authToken) {        
        config.headers.Authorization = `Bearer ${authToken}`;
      }
    }
    return config;
  },
  (err) => {
    console.log("err")
    //What do you want to do when a call fails?
  }
);

mgr.events.addSilentRenewError(function (error) {
  console.log('Silent Renew Error：', error);
});

mgr.events.addAccessTokenExpiring(function () {
  // expediente.mgr.signinSilent()
  // .then((data) =>
  // {
  //   debugger
  //   console.log("addAccessTokenExpiring:signinSilent");
  //   console.log(data);
  //     //this.handleUser(user); // This function just set the current user
  // })
  // .catch((error) =>
  // {
  //     debugger
  //     console.log("addAccessTokenExpiring:signinSilent:error");
  //     console.log(error);
  //     // this.userManager.getUser()
  //     //     .then((user: CoreApi.Authentication.Interfaces.OidcClientUser) =>
  //     //     {
  //     //         this.handleUser(user);
  //     //     });
  // });
});

mgr.events.addAccessTokenExpired(function () {
  
  expediente.mgr.signinSilent()
  .then(function (data) {
    
    console.log(data);
  })
  .catch(function (err) {
    
    console.log(err);
    expediente.mgr.removeUser();
    expediente.mgr.revokeAccessToken();
    expediente.mgr.clearStaleState();
    localStorage.removeItem('accessToken');  
    localStorage.clear();

    expediente = globalData;
    // expediente.isAuthenticated = false;
    // expediente.user = '';
    // expediente.userPermissions = ["AccederDashboard"];
    // expediente.userRoles = [];
    // expediente.infoColaboradorActual = {
    //   id: null,
    //   identificacion: null,
    //   nombre: "",
    //   correo: "",
    //   nombreUsuario:""
    // };

    router.push({ path: '/auth/login' })
  });
});