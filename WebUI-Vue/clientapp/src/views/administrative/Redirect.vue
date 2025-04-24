<template>
  <div class="wrapper vld-parent" ref="formContainer">
    <div>
      Vista de redireccionamiento 
    </div>
  </div>
</template>

<script>
import {
  GetDetalleSolicitud
} from "./request";

import Vue from "vue";

export default {
  name: "Redirect",
  data() {
    return {

    };
  },
  async mounted() {
   
        if(!this.$router.app.$data.isAuthenticated)
        {
            
            await this.$router.app.authenticate(this.$router.currentRoute.fullPath);
            // .then(() => {
                
            //     var idColaborador = this.$root.infoColaboradorActual.id;
            // var idSolicitud = this.$route.query.id;

            //     
            //     if(idColaborador != null)
            //     {
            //         
            //         // validar que el responsable es el usuario autenticado y
            //         // que la solicitud no esté aprobada o denegada
            //         GetDetalleSolicitud(idSolicitud, idColaborador).then((response) => {
            //             
            //             this.$router.push({
            //                 name: "Administrative",
            //                 params: {
            //                             origen: "PendientesAtender", // origen desde donde se accedio la vista
            //                             idTipoSolicitud: 1028, // id del tipo de la solicitud generada
            //                             tipoSolicitud: "Servicios laborales", // nombre del tipo de la solicitud generada
            //                             idSolicitud: idSolicitud
            //                         }
            //             });   
            //         });
            //     }
            // });
        }
        else{
            let ref = this;
            this.$nextTick(() => {
                
                var correo = ref.$root.infoColaboradorActual.correo;
                var idSolicitud = ref.$route.query.id;
                var estado = ref.$route.query.e;

                
                if(correo != null && estado != null)
                {
                    
                    // validar que el responsable es el usuario autenticado y
                    // que la solicitud no esté aprobada o denegada
                     GetDetalleSolicitud(idSolicitud, estado, correo)
                     .then((response) => {
                        if(response.data.puedeAprobar)
                        {
                            ref.$router.push({
                                name: "Administrative",
                                params: {
                                            origen: "PendientesAtender", // origen desde donde se accedio la vista
                                            idTipoSolicitud: response.data.idTipoSolicitud, // id del tipo de la solicitud generada
                                            tipoSolicitud: response.data.tipoSolicitud, // nombre del tipo de la solicitud generada
                                            idSolicitud: idSolicitud
                                        }
                            });
                        }   
                        else{
                            
                            Vue.$toast.open({
                                message: response.data.mensajeValidacion,
                                type: "warning",
                                duration: 10000,
                                dismissible: true
                            })
                            ref.$router.push({ path: '/' });
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                        ref.$router.push({ path: '/' });
                    });
                }
                else{
                    ref.$router.push({ path: '/' });
                }
          });
        }
    

    // this.$router.push({
    //     name: "Administrative",
    //     params: {
    //       origen: "PendientesAtender", // origen desde donde se accedio la vista
    //       idTipoSolicitud: 1028, // id del tipo de la solicitud generada
    //       tipoSolicitud: "Servicios laborales", // nombre del tipo de la solicitud generada
    //       idSolicitud: this.$route.query.id
    //     }
    //   });   
  }
};
</script>

<style>
 
</style>
