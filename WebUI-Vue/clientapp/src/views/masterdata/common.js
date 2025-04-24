import Vue from "vue";
import {
  GuardarSolicitud,
  EliminarSolicitud,
  EnviarSolicitud
} from "./request";

/**
 * Constantes para los estados de una solicitud
 */
const REGISTRADA = "RE";
const APROBADA = "AP";
const ANULADA = "AN";
const DENEGADA = "DE";
const DESCARTADA = "DC";
const ENAPROBACION = "EA";
const CORREGIR = "CO";
const ENVALIDACION = "EV";
const PENDIENTEACTUALIZACION = "PE";
const ENPROCESO = "EP";
const ENPROCESOVACACIONES = "EPV";

/**
 * Constantes y métodos comúnes para todas las solicitudes
 */
export default {
  REGISTRADA: REGISTRADA,
  APROBADA: APROBADA,
  ANULADA: ANULADA,
  DENEGADA: DENEGADA,
  DESCARTADA: DESCARTADA,
  ENAPROBACION: ENAPROBACION,
  CORREGIR: CORREGIR,
  ENVALIDACION: ENVALIDACION,
  PENDIENTEACTUALIZACION: PENDIENTEACTUALIZACION,
  ENPROCESO: ENPROCESO,
  ENPROCESOVACACIONES: ENPROCESOVACACIONES,
  MENSAJE_ERROR:
    "El formulario presenta errores de validación, por favor corregir.",
  SOLICITUD_SINMODIFICACIONES: "S",

  methods: {
    /**
     * Regresa a la lista de solicitudes de MI BUZON o TRAMITADAS según
     * el origen desde el cual navegó el usuario
     *
     * @returns None
     */
    atras() {
      switch (this.$route.params.origen) {
        case "Inbox":
          this.$router.push({
             name: "Mi Buzón"
          });
          return;
        case "Outbox":
          this.$router.push({ name: "Tramitadas" });
          return;
        case "History":
          this.$router.push({ name: "Mi historial" });
          return;
        case "Consultants":
          this.$router.push({ name: "Mis consultores" });
          return;
        case "CertificacionesTramitadas":
          this.$router.push({
            name: "Mis trámites"
          });
          return;
        case "PendientesAtender":
          this.$router.push({ name: "Pendientes de atender" });
          return;
        case "TrackingAdmin":
          this.$router.push({ name: "Seguimiento Administrativo" });
          return;
        default:
          this.$router.push({ name: "Seguimiento" });
          return;
      }
    },
    /**
     * Determina la descripción del estado de la solicitud a partir del código del estado
     * @param {Código del estado de una solicitud} codigoEstadoSolicitud
     * @returns Descripción del estado
     */
    obtenerDescripcionEstado(codigoEstadoSolicitud) {
      switch (codigoEstadoSolicitud) {
        case REGISTRADA:
          return "Registrada";
        case APROBADA:
          return "Aprobada";
        case DENEGADA:
          return "Denegada";
        case DESCARTADA:
          return "Descartada";
        case ENAPROBACION:
          return "En aprobación";
        case CORREGIR:
          return "En correcciones";
        case ENVALIDACION:
          return "En validación";
        case PENDIENTEACTUALIZACION:
          return "Pendiente de actualización";
        case ENPROCESO:
          return "En proceso";
        case ENPROCESOVACACIONES:
          return "En proceso";
        case ANULADA:
          return "Anulada";
        default:
          break;
      }
    },
    /**
     * Obtiene el color que aplica al BADGE que muestra la descripción del
     * estado de una solicitud
     * @param {Código del estado de una solicitud} codigoEstadoSolicitud
     * @returns Color para el BADGE
     */
    obtenerColorEstado(codigoEstadoSolicitud) {
      switch (codigoEstadoSolicitud) {
        case REGISTRADA:
        case CORREGIR:
        case ENAPROBACION:
        case ENVALIDACION:
        case PENDIENTEACTUALIZACION:
          return "warning";
          break;
        case APROBADA:
          return "success";
          break;
        case DENEGADA:
        case DESCARTADA:
          return "danger";
          break;
        case ANULADA:
          return "danger";
          break;

        default:
          return "info";
          break;
      }
    },
    /**
     * Genera un objeto que utilizan todas las solicitudes con los datos base de las mismas
     * @param {Objeto con la info base de una solicitud que viene de base de datos} solicitud
     * @returns Objeto con la estructura esperada para los datos de cualquier solicitud
     */
    getDatosSolicitud(solicitud) {
      return {
        idSolicitudMasterData: solicitud.idSolicitud,
        idTipoSolicitud: solicitud.idTipoSolicitud,
        tipoSolicitud: solicitud.tipoSolicitud,
        estadoSolicitud: solicitud.codigoEstado,
        idcolaborador: solicitud.idColaborador
      };
    },
    /**
     * Ejecuta llamada al API para guardar la solicitud
     * @param {Datos de la solicitud a guardar} datos
     * @param {Identificador de la solicitud a guardar} idSolicitud
     * @returns Id de la solicitud guardada. En caso de ser nueva se devuelve el valor retornado desde BD
     */
    async Guardar(datos, idSolicitud) {
      let id = idSolicitud;
      await GuardarSolicitud(datos)
        .then(response => {
          id = response.data;
          Vue.$toast.success("Solicitud guardada exitosamente.", {});
        })
        .catch(response => {
          Vue.$toast.error(
            "Se presentó un problema al guardar la solicitud.",
            {}
          );
        });
      // retorna el id de la solicitud guardada
      return id;
    },
    async Enviar(datos, opciones) {
      let ok = false;
      datos = { ...datos, comentario: "" };
      if (opciones.tipo == "comentario") {
        // solicitar comentario
        let retVal = await this.$refs.commentDialogue.show({
          title: opciones.titulo,
          message: opciones.mensajeDialogo,
          okButton: opciones.btnOk,
          cancelButton: opciones.btnCancel
        });
        ok = retVal.result;
        datos.comentario = retVal.comment;
      } else if (opciones.tipo == "confirmar") {
        // solicitar confirmación
        ok = await this.$refs.confirmDialogue.show({
          title: opciones.titulo,
          message: opciones.mensajeDialogo,
          okButton: opciones.btnOk,
          cancelButton: opciones.btnCancel
        });
      } else {
        ok = true;
      }

      if (ok) {
        //console.log(datos);
        await EnviarSolicitud(datos)
          .then(async response => {
            Vue.$toast.success(opciones.mensajeExito, {});
            if (datos.codigoEstado == "RE" || datos.codigoEstado == "CO") {
              if (datos.solicitudSinAprobacion) {
                ok = await this.$refs.confirmDialogue.show({
                  title: "Solicitud completa",
                  message:
                    "Estimado usuario, su solicitud se aplicó con éxito. Favor validar la información registrada en el perfil del colaborador. Gracias",
                  okButton: "Aceptar",
                  cancelButton: "NoMostrar"
                });
              } else {
                ok = await this.$refs.confirmDialogue.show({
                  title: "Solicitud completa",
                  message:
                    "Estimado usuario, su solicitud se registró con éxito y fue enviada a alguno de los miembros de RRHH para su revisión. Favor estar pendiente si la misma fue aprobada, enviada a corrección o denegada. Gracias",
                  okButton: "Aceptar",
                  cancelButton: "NoMostrar"
                });
              }
            }
            this.atras();
          })
          .catch(response => {
            if (
              response &&
              response.response &&
              response.response.data &&
              response.response.data.IdTipoSolicitud
            ) {
              Vue.$toast.warning(response.response.data.IdTipoSolicitud[0], {
                duration: 5000
              });
              if (datos.idSolicitud) {
                this.ElimininarSolicitud(datos.idSolicitud);
              } else {
                this.atras();
              }
            } else {
              Vue.$toast.error(opciones.mensajeError, {});
              console.log(response);
            }
          });
      }
    },
    /**
     * Solicita confirmación para eliminar una solicitud. En caso de ser
     * afirmativo se ejecuta el API para eliminar
     * @param {Texto de la acción disparada desde los botones del flujo} accion
     * @param {Identificador de la solicitud a eliminar} idSolicitud
     * @returns
     */
    async procesarAccion_Eliminar(accion, idSolicitud) {
      // se configura el popup de confirmación de borrado
      // y se queda a la espera de la indicación del usuario
      const ok = await this.$refs.confirmDialogue.show({
        title: "Eliminar solicitud",
        message:
          "Está seguro que desea eliminar la solicitud? La acción no se puede deshacer.",
        okButton: "Eliminar",
        cancelButton: "Cancelar"
      });

      if (ok) {
        if (idSolicitud) {
          // si la respuesta es positiva se dispara el borrado en la base de datos
          await EliminarSolicitud(idSolicitud)
            .then(response => {
              Vue.$toast.success(
                "La solicitud fue eliminada exitosamente.",
                {}
              );
              this.atras();
            })
            .catch(response => {
              console.log(response.Error);
              Vue.$toast.error("La solicitud no pudo eliminarse.", {});
            });
        } else {
          Vue.$toast.success("La solicitud fue eliminada exitosamente.", {});
          this.atras();
        }
      } else {
        Vue.$toast.info("La acción de borrado fue cancelada.", {});
      }

      return false;
    },
    /**
     * Invova al API que se encarga del borrado de una soliciytud en base de datos.
     * @param {Identificador de la solicitud a eliminar} idSolicitud
     */
    async EliminaSolicitud(idSolicitud) {
      // si la respuesta es positiva se dispara el borrado en la base de datos
      await EliminarSolicitud(idSolicitud)
        .then(response => {
          Vue.$toast.success("La solicitud fue eliminada exitosamente.", {});
          this.atras();
        })
        .catch(response => {
          console.log(response.Error);
          Vue.$toast.error("La solicitud no pudo eliminarse.", {});
        });
    },
    async Eliminar_Generico(tipoSolicitud, nombre, genero) {
      // se configura el popup de confirmación de borrado
      // y se queda a la espera de la indicación del usuario
      let ident = "la";
      if (genero === "M") {
        ident = "el";
      }
      const ok = await this.$refs.confirmDialogue.show({
        title: "Eliminar " + tipoSolicitud,
        message:
          "Está seguro que desea eliminar " +
          ident +
          " " +
          tipoSolicitud +
          " " +
          nombre +
          " ?",
        okButton: "Eliminar",
        cancelButton: "Cancelar"
      });

      if (ok) {
        return true;
      } else {
        Vue.$toast.info("La acción de borrado fue cancelada.", {});
      }

      return false;
    },
    async Excluir_Generico(tipoSolicitud, nombre, genero) {
      // se configura el popup de confirmación de borrado
      // y se queda a la espera de la indicación del usuario
      let ident = "la";
      if (genero === "M") {
        ident = "el";
      }
      const ok = await this.$refs.confirmDialogue.show({
        title: "Excluir " + tipoSolicitud,
        message:
          "Está seguro que desea excluir " +
          ident +
          " " +
          tipoSolicitud +
          " " +
          nombre +
          " ?",
        okButton: "Excluir",
        cancelButton: "Cancelar"
      });

      if (ok) {
        return true;
      } else {
        Vue.$toast.info("La acción de excluir fue cancelada.", {});
      }

      return false;
    },
    async Eliminar_Generico_Modal(tipoSolicitud, nombre, genero) {
      // se configura el popup de confirmación de borrado
      // y se queda a la espera de la indicación del usuario
      let ident = "la";
      if (genero === "M") {
        ident = "el";
      }
      const ok = await this.$refs.confirmDialogueModal.show({
        title: "Eliminar " + tipoSolicitud,
        message:
          "Está seguro que desea eliminar " +
          ident +
          " " +
          tipoSolicitud +
          " " +
          nombre +
          " ? La acción no se puede deshacer.",
        okButton: "Eliminar",
        cancelButton: "Cancelar"
      });

      if (ok) {
        return true;
      } else {
        Vue.$toast.info("La acción de borrado fue cancelada.", {});
      }

      return false;
    },

    async Confirmacion_Generico(mensaje, title) {
      // se configura el popup de confirmación
      // y se queda a la espera de la indicación del usuario
      const ok = await this.$refs.confirmDialogue.show({
        title: title,
        message: mensaje,
        okButton: "Aceptar",
        cancelButton: "Cancelar"
      });

      if (ok) {
        return true;
      } else {
        Vue.$toast.info("La acción fue cancelada.", {});
      }

      return false;
    },

    showLoader() {
      return this.$loading.show({
        // Optional parameters
        container: this.fullPage ? null : this.$refs.formContainer,
        canCancel: true,
        onCancel: this.onCancel
      });
    },
    async hideLoader(loader) {
      setTimeout(() => {
        loader.hide();
      }, 300);
    },

    /**
     * Agrega o actualiza el ID con el número de posición de cada objeto JSON en el arreglo
     * @param {Colección de objetos JSON} arr
     * @returns Arreglo actualizado
     */
    addIdentityToArrayObjects(arr) {
      if (arr) {
        return arr.map((a, idx) => ({ ...a, id: idx }));
      }
    },
    hashCode(s) {
      return s.split("").reduce(function(a, b) {
        a = (a << 5) - a + b.charCodeAt(0);
        return a & a;
      }, 0);
    },
    habilitarControlesSolicitud(estadoSolicitud, origen) {
      return (
        (estadoSolicitud == REGISTRADA || estadoSolicitud == CORREGIR) &&
        (origen == "Inbox" ||
          origen == "CertificacionesTramitadas" ||
          origen == "PendientesAtender")
      );
    },
    TiposExperiencia() {
      const arr = {
        1: "Base de datos",
        2: "Lenguajes",
        3: "Ambientes de desarrollo",
        4: "Tecnologías",
        5: "ERPs / Core",
        6: "Calidad",
        7: "Infraestructura",
        8: "Buses y SOA",
        9: "BPM",
        10: "Cloud"
      };
      return arr;
    },
    disableAutoComplete() {
      let elements = document.querySelectorAll("input");

      if (!elements) {
        return;
      }

      elements.forEach(element => {
        element.setAttribute("autocomplete", "nope");
      });
    }
  }
};
