<template>
    <div>
        <p>Refrescamiento en progreso</p>
    </div>
</template>

<script>
import {  GetInfoColaborador, GetContextoSeguridad } from "../containers/request";

export default {
    async mounted() {
        try {
            let ref = this;
            await this.$root.mgr.signinSilentCallback()
                                .then(function(valor){
                                 //   console.log("signinSilentCallback con éxito " + valor);
                                    ref.$root.getUser().then(function(valor) {
                                            const user = valor; //ref.$root.user;
                                            ref.$root.setUser(); 
                                            
                                            ref.$root.infoColaboradorActual.correo = user.profile.email;
                                            ref.$root.infoColaboradorActual.nombreUsuario = user.profile.name;
                                            ref.getContextoSeguridad();
                                            ref.getInfoColaboradorActual(user.profile.email);
                                    });
                                });

            

        } catch (e) {
            console.log("Silent-Renew.vue: " + e);
        }
    },
    methods: {
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