<template>
    <div>
        <p>Autenticación en progreso</p>
    </div>
</template>

<script>
import Axios from "../config/axios" //import axios from 'axios'
import {  GetInfoColaborador, GetContextoSeguridad } from "../containers/request";

export default {
    async created() {
        try {
            
            var result = await this.$root.mgr.signinRedirectCallback();
            var returnToUrl = '/';

            if (result.state !== undefined) { 
                returnToUrl = result.state;
                await this.$root.setUser();

                const user = this.$root.user; 
                this.$root.infoColaboradorActual.correo = user.profile.email;
                this.$root.infoColaboradorActual.nombreUsuario = user.profile.name;
                await this.getContextoSeguridad();
                await this.getInfoColaboradorActual(user.profile.email);
                //await this.getUserPermissions();
            }

            await this.$router.push({ path: returnToUrl });
        } catch (e) {
            this.$router.push({ name: 'Unauthorized' });
        }
    },
    methods: {
        async getUserPermissions() {
        // try {
        //     const response = await axios.get("https://localhost:44311/api/UserPermissions"); 
        //     this.$root.userPermissions = response.data;
        // } catch (err) {
        //     console.log('Error al obtener los permisos');
        // }
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

    }
}
</script>