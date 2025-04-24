<template>
  <CDropdown
    inNav
    class="c-header-nav-items"
    placement="bottom-end"
    add-menu-classes="pt-0"
  >
    <template #toggler>
      <CHeaderNavLink>
        <div class="c-avatar">
          <img
            :src="avatar"
            class="c-avatar-img "
            style="height: 100% !important"
          />
        </div>
      </CHeaderNavLink>
    </template>
 <!--   <CDropdownHeader tag="div" class="text-center" color="light">
      <strong>Account</strong>
    </CDropdownHeader>
    <CDropdownItem>
      <CIcon name="cil-bell"/> Updates
      <CBadge color="info" class="ml-auto">{{ itemsCount }}</CBadge>
    </CDropdownItem>
    <CDropdownItem>
      <CIcon name="cil-envelope-open" /> Messages
      <CBadge color="success" class="ml-auto">{{ itemsCount }}</CBadge>
    </CDropdownItem>
    <CDropdownItem>
      <CIcon name="cil-task" /> Tasks
      <CBadge color="danger" class="ml-auto">{{ itemsCount }}</CBadge>
    </CDropdownItem>
    <CDropdownItem>
      <CIcon name="cil-comment-square" /> Comments
      <CBadge color="warning" class="ml-auto">{{ itemsCount }}</CBadge>
    </CDropdownItem>-->
    <CDropdownHeader
      tag="div"
      class="text-center"
      color="light"
    >
      <strong>{{userName}}</strong>
    </CDropdownHeader>
    <CDropdownItem @click="miPerfil">
      <CIcon name="cil-user" /> Mi Perfil
    </CDropdownItem>
    <!-- <CDropdownItem>
      <CIcon name="cil-settings" /> Configuración
    </CDropdownItem> -->
  <!--  <CDropdownItem>
      <CIcon name="cil-dollar" /> Payments
      <CBadge color="secondary" class="ml-auto">{{ itemsCount }}</CBadge>
    </CDropdownItem>-->
    <!-- <CDropdownItem>
      <CIcon name="cil-file" /> Proyectos
      <CBadge color="primary" class="ml-auto">{{ itemsCount }}</CBadge>
    </CDropdownItem> -->
    <CDropdownDivider v-if="this.currentUser"/>
   <!--  <CDropdownItem>
      <CIcon name="cil-shield-alt" /> Lock Account
    </CDropdownItem>-->
    <CDropdownItem v-if="currentUser" @click="logout">
      <CIcon name="cil-lock-locked" /> Salir
    </CDropdownItem>
  </CDropdown>
</template>

<script>
import {  GetPhoto, GetInfoColaborador, GetContextoSeguridad } from "./request";

export default {
  name: 'TheHeaderDropdownAccnt',
  data () {
    return { 
      itemsCount: 42,
      userName: "",
      avatar: "/img/avatars/currentUser.jpg"
    }
  },
  mounted() {
    this.setUser()
  },
  methods: {
    miPerfil(){
      this.$router.push({ path: '/generales/colaborador/detail/' })
    },
    logout() {
      
      // this.$currentUser = null
      // this.$AuthService.logout()
      
      this.$root.mgr.removeUser();
      this.$root.mgr.revokeAccessToken();
      this.$root.mgr.clearStaleState();
      localStorage.removeItem('accessToken');  
      localStorage.clear();

      this.$root.isAuthenticated = false;
      this.$root.user = '';
      this.$root.userPermissions = ["AccederDashboard"];
      this.$root.userRoles = [];
      this.$root.infoColaboradorActual = {
        id: null,
        identificacion: null,
        nombre: "",
        correo: "",
        nombreUsuario:""
      }

    function deleteAllCookies() {
        var cookies = document.cookie.split(";");

        for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i];
            var eqPos = cookie.indexOf("=");
            var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
        }
    }

    deleteAllCookies();
      //this.$root.signIn(this.$root.$route.path);
      this.$router.push({ path: '/auth/login' })
    },
    login() {
      //this.$AuthService.loginPopup() //with a popup
      this.$AuthService.loginRedirect() //with a redirect
    },
    async setUser() {     
      const user = this.$root.user; 
      if (user)
      {
        this.$root.infoColaboradorActual.correo = user.profile.email;
        this.$root.infoColaboradorActual.nombreUsuario = user.profile.name;
        //this.userName = user.profile.preferred_username;
        await this.getContextoSeguridad();
        await this.getInfoColaboradorActual(user.profile.email);
        await this.getPhoto(user.profile.email);
      }
      else {
        this.userName = "Desconocido"
      }
      // this.$currentUser = this.$AuthService.getUser()
      // if (this.$currentUser)
      // {
      //   this.userName = this.$currentUser.name
      //   this.getPhoto(this.$currentUser.displayableId) 
      // }
      // else {
      //   this.userName = "Desconocido"
      // }
    },
    async getPhoto(email) {
      try {
        const {
          data: { fotoColaborador }
        } =  await GetPhoto(email);
        if(fotoColaborador == null)
          this.avatar = "/img/avatars/currentUser.jpg";
        else
          this.avatar = fotoColaborador.foto;        
      } catch (error) {
        this.avatar = "/img/avatars/currentUser.jpg";
      }

    },
    async getInfoColaboradorActual(email) {
      GetInfoColaborador(email)
          .then(response => {
                this.$root.infoColaboradorActual.id = response.data.infoColaborador.id;
                this.$root.infoColaboradorActual.correo = response.data.infoColaborador.correo;
                this.userName = this.$root.infoColaboradorActual.nombre = response.data.infoColaborador.nombre;
                this.$root.infoColaboradorActual.identificacion = response.data.infoColaborador.identificacion;
                this.$root.infoColaboradorActual.contadorInbox = response.data.infoColaborador.contadorInbox;
                this.$root.infoColaboradorActual.contadorAdminInbox = response.data.infoColaborador.contadorAdminInbox;
                this.$root.infoColaboradorActual.idnomina = response.data.infoColaborador.idnomina;
                this.$root.refreshInboxCounter();
                this.$root.refreshInboxAdminCounter();
              }
            )
          .catch(response => { 
                this.userName = "Desconocido";
                this.$root.infoColaboradorActual.id = null;
                this.$root.infoColaboradorActual.identificacion = null;
                this.$root.infoColaboradorActual.nombre =  "";
                this.$root.infoColaboradorActual.correo = "";
                this.$root.infoColaboradorActual.nombreUsuario = "";
              }
            );            
    },
    async getContextoSeguridad() {
      GetContextoSeguridad()
          .then(response => {
                
                this.$root.userPermissions = response.data.permisos;
                this.$root.userRoles = response.data.roles;
              }
            )
          .catch(response => { 
                console.log(response);
              }
            );            
    }
  },
  computed:{
    currentUser(){
      const user = this.$root.user; 
      if (user)
      {
        return user;
      }
      else {
        return null;
      }
    }
  },
  watch:{
    currentUser(){
      this.setUser();
    }
  }
}
</script>

<style scoped>
  .c-icon {
    margin-right: 0.3rem;
  }
</style>