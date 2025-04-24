<template>
  <div>
    <CRow>
      <CCol>
        <CButton
          block
          color="primary"
          @click="atras()"
          class="btn btn-primary btn-lg"
          style="height: 40px; width: 130px;float: right; color: rgba(98, 184, 235, 1); background-color: transparent; border-color: transparent;"
        >
          <i class="pi pi-arrow-circle-left"></i>&nbsp;Regresar</CButton
        >
      </CCol>
    </CRow>

    <CCard style="border-radius: 16px !important;">
      <CCardHeader
        style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
      >
        <strong>Experiencia en tecnologías</strong>
        <small> Solicitud de cambio </small>
        <CBadge
          color="primary"
          class="mr-2 libro-marca-azuloscuro"
          shape="pill"
          v-if="this.datosSolicitud.idSolicitudMasterData"
        >
          <span style="margin-top:.2rem; margin-bottom:.2rem">
            {{ this.datosSolicitud.idSolicitudMasterData }}
          </span>
        </CBadge>
        <CBadge
          :color="this.obtenerColorEstado(this.datosSolicitud.estadoSolicitud)"
          class="mr-2"
          shape="pill"
        >
          <span style="margin-top:.2rem; margin-bottom:.2rem">
            {{
              this.obtenerDescripcionEstado(this.datosSolicitud.estadoSolicitud)
            }}
          </span>
        </CBadge>
      </CCardHeader>
      <CCardBody>
        <resumen-colaborador
          :colaborador.sync="colaborador"
          :estado-solicitud="datosSolicitud.estadoSolicitud"
          :searchable="pickPuedeBuscarColaborador"
          @displayModal="displayModal"
          :editable="false"
        ></resumen-colaborador>
      </CCardBody>
    </CCard>

    <CCard style="border-radius: 16px !important;">
      <CCardHeader
        style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
      >
        <strong>Lista de experiencias por actualizar</strong>
      </CCardHeader>
      <CCardBody>
        <FormulateForm v-model="solicitud" @submit="submit">
          <!-- :estado-solicitud="this.datosSolicitud.estadoSolicitud" -->
          <agrupacion-experiencia
            :experiencias="solicitud.experiencias"
            :habilitar-controles="
              this.habilitarControlesSolicitud(
                this.datosSolicitud.estadoSolicitud,
                this.origen
              )
            "
            :idColaborador="this.colaborador.idcolaborador"
            @undo="undoExperiencia"
            @modify="modificarExperiencia"
            @add="agregarExperiencia"
            @remove="eliminarExperiencia"
          >
          </agrupacion-experiencia>
          <CCard
            v-if="this.comentarios.length > 0"
            style="border-radius: 16px !important;"
          >
            <CCardHeader
              style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
            >
              <strong> Comentarios</strong>
            </CCardHeader>
            <CCardBody>
              <comments
                :comments_wrapper_classes="[
                  'custom-scrollbar',
                  'comments-wrapper'
                ]"
                :comments="this.comentarios"
              ></comments>
            </CCardBody>
          </CCard>

          <CRow style="margin-bottom: 80px;margin-top: 20px;">
            <CCol>
              <botonera-master-data
                :estado-solicitud="this.datosSolicitud.estadoSolicitud"
                nombreEventoProcesar="procesarAccion"
                @procesarAccion="procesarAccion"
                v-if="muestraBotonera()"
              >
              </botonera-master-data>
            </CCol>
          </CRow>
        </FormulateForm>
      </CCardBody>
    </CCard>

    <!-- Diálogo para los PICKLIST -->
    <Dialog
      :visible.sync="showModal"
      :style="{ width: '70vw' }"
      :maximizable="true"
      :modal="true"
    >
      <template #header>
        <h5>{{ tituloModal }}</h5>
      </template>

      <pick-list
        @selected="selected"
        :nombreTipoLista="nombreTipoLista"
        :columnas="columnasPickList"
        :dataContenido="dataPickList"
      ></pick-list>

      <template #footer> </template>
    </Dialog>

    <!-- Diálogos de confirmación -->
    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
    <comment-dialogue ref="commentDialogue"></comment-dialogue>
  </div>
</template>

<script>
/**
 * Constantes para los estados de una solicitud
 */

import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "./ResumenColaborador";
import RepeatableExperiencia from "../generales/Componentes/RepeatableExperiencia";
import BotoneraMasterData from "./BotoneraMasterData.vue";
import _ from "lodash";

import {
  GetColaboradoresPerfil,
  GetAllColaboradores,
  GetColaboradorDetail,
  GetDetalleSolicitud,
  GetColaboradorDetailResumen
} from "./request";
import Vue from "vue";
// métodos comunes
import common from "./common.js";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";
import Comments from "../generales/Componentes/Comments/Comments.vue";

export default {
  name: "ExperienciaProyectosInternos",
  mixins: [common],
  props: {
    origen: { type: String, default: "" }, // origen desde donde se accedio la vista
    idTipoSolicitud: { type: Number, default: 0 }, // id del tipo de la solicitud generada
    tipoSolicitud: { type: String, default: "" }, // nombre del tipo de la solicitud generada
    idSolicitud: { type: Number, default: null }, // id de la solicitud, para obtener el obj Json con la informacion a cargar
    idResponsable: { type: Number, default: null } // id  del responsable de la solicitud
  },
  components: {
    "pick-list": PickList,
    "resumen-colaborador": ResumenColaborador,
    "agrupacion-experiencia": RepeatableExperiencia,
    "botonera-master-data": BotoneraMasterData,
    "confirm-dialogue": ConfirmDialogue,
    "comment-dialogue": CommentDialogue,
    comments: Comments
  },
  data() {
    return {
      colaborador: {
        // values de colaborador
        idcolaborador: 0,
        identificacion: null,
        nombre: null,
        primerapellido: null,
        segundoapellido: null,
        foto: null
      },
      solicitud: {
        idColaborador: null,
        identificacion: null,
        nombreColaborador: "",
        primerapellido: "",
        segundoapellido: "",
        foto: null,
        experiencias: [],
        estado: "S"
      },
      comentarios: [],
      datosSolicitud: {
        idSolicitudMasterData: null,
        idTipoSolicitud: this.idTipoSolicitud,
        tipoSolicitud: this.tipoSolicitud,
        estadoSolicitud: "RE"
      },
      showModal: false,
      nombreTipoLista: "",
      columnasPickList: [],
      dataPickList: [],
      TipoListaPick: "",
      cargandoDatos: false,
      tituloModal: "",
      IdSolicitudJson: null // id de la solicitud para cargar el json de una solicitud
    };
  },
  async mounted() {
    let loader = this.showLoader();
    // se cargarian los request en caso de ser necesario o en created...
    this.IdSolicitudJson = this.idSolicitud;

    if (this.IdSolicitudJson) {
      await this.cargarSolicitud();
    } else {
      //carga el usuario en session
      this.TipoListaPick = "Colaboradores";
      await this.selected(this.$root.infoColaboradorActual);
    }
    this.hideLoader(loader);
  },
  methods: {
    obtenerDescripcionEstado(codigoEstadoSolicitud) {
      switch (codigoEstadoSolicitud) {
        case common.REGISTRADA:
          return "Registrada";
        case common.APROBADA:
          return "Aprobada";
        case common.DENEGADA:
          return "Denegada";
        case common.DESCARTADA:
          return "Descartada";
        case common.ENAPROBACION:
          return "En aprobación";
        case common.CORREGIR:
          return "En correcciones";
        case common.ENVALIDACION:
          return "En validación";
        case common.PENDIENTEACTUALIZACION:
          return "Pendiente de actualización";
        default:
          break;
      }
    },

    /**
     * Invoca a un método que obtiene el documento asociado a una solicitud así como
     * los datos propios de la solicitud
     */
    async obtenerSolicitud() {
      await this.cargarSolicitud();
    },
    /**
     * Carga el objeto SOLICITUD con el documento JSON que viene de base de datos
     * @documento hilera JSON con los datos de una solicitud
     */
    leerDocumentoSolicitud(documento) {
      try {
        var colaborador = JSON.parse(documento);

        return {
          colaborador: {
            idcolaborador: colaborador.idcolaborador,
            identificacion: colaborador.identificacion,
            nombre: colaborador.nombre,
            primerapellido: colaborador.primerapellido,
            segundoapellido: colaborador.segundoapellido,
            foto: colaborador.foto
          },
          solicitud: {
            estado: colaborador.estado,
            experiencias: colaborador.experiencias
          }
        };
      } catch (error) {
        console.log("Error al leer el documento JSON de la solicitud");
        console.error(error);
      }
    },
    /**
     * Invoca al API que obtiene los datos de una solicitud
     */
    async cargarSolicitud() {
      this.cargandoDatos = true;
      let loader = this.showLoader();

      GetDetalleSolicitud(this.IdSolicitudJson)
        .then(response => {
          // objeto devuelto por el API
          var solicitud = response.data.solicitudMasterData;

          // se inicializan los datos base de la solicitud
          this.datosSolicitud = this.getDatosSolicitud(solicitud);

          this.cargarDetalleColaborador(this.datosSolicitud.idcolaborador);

          let documento = this.leerDocumentoSolicitud(solicitud.docSolicitud);
          this.solicitud = documento.solicitud;
          this.colaborador = documento.colaborador;

          // comentarios de la solicitud
          this.comentarios = solicitud.comentarios.map(
            ({
              idComentarioSolicitud,
              responsable,
              comentario,
              accion,
              fecha
            }) => ({
              id: idComentarioSolicitud,
              responsable: responsable,
              comentario: comentario,
              accion: accion,
              fecha: fecha
            })
          );

          // se posterga la actualización del indicador de carga hasta que se haya "renderizado" la vista
          this.terminaCargaDatos();
          this.hideLoader(loader);
        })
        .catch(response => {
          this.cargandoDatos = false;
          this.hideLoader(loader);
        });
    },
    async submit(formValues) {
      try {
        if (this.solicitud.estado == common.SOLICITUD_SINMODIFICACIONES) {
          Vue.$toast.warning(
            "La solicitud no tiene modificaciones que guardar.",
            {}
          );
        } else if (this.experienciasSinModificaciones()) {
          Vue.$toast.warning(
            "Por favor excluir las experiencias en tecnologías que no tienen modificaciones.",
            {}
          );
        } else if (!this.experienciasDuplicadas()) {
          //   let resultadoValidacion = this.validacionesSolicitud();
          //   if (resultadoValidacion.esValido) {
          let datos = {
            idSolicitud: this.datosSolicitud.idSolicitudMasterData,
            idTipoSolicitud: this.datosSolicitud.idTipoSolicitud,
            codigoEstado: this.datosSolicitud.estadoSolicitud,
            idAutor: this.$root.infoColaboradorActual.id,
            idResponsable: this.$root.infoColaboradorActual.id,
            idColaborador: this.colaborador.idcolaborador,
            accion: "Enviar",
            docSolicitud: JSON.stringify({
              ...this.colaborador.idcolaborador,
              ...this.solicitud
            })
          };

          let opciones = {
            tipo: "confirmar", // confirmar comentario ninguno
            titulo: "Solicitud de cambio",
            mensajeDialogo: "Está seguro de enviar la solicitud?",
            btnOk: "Si",
            btnCancel: "No",
            mensajeExito: "Solicitud enviada exitosamente.",
            mensajeError:
              "Se ha presentado un inconveniente al enviar la solicitud de cambio."
          };

          await this.Enviar(datos, opciones);
        }
      } catch (error) {
        console.log(error);
      }
    },
    experienciasSinModificaciones() {
      const foundIndex = this.solicitud.experiencias.findIndex(
        x => x.estado == "S"
      );

      return foundIndex >= 0;
    },
    experienciasDuplicadas() {
      const ids = this.solicitud.experiencias.map(
        ({ idTipoExperiencia, idExperiencia }) => ({
          idTipoExperiencia: idTipoExperiencia,
          idExperiencia: idExperiencia
        })
      );
      const set = [...new Set(ids.map(el => JSON.stringify(el)))].map(e =>
        JSON.parse(e)
      );

      const hasDuplicates = set.length < this.solicitud.experiencias.length;

      const grouped = _.groupBy(
        this.solicitud.experiencias,
        exp => `${exp.idTipoExperiencia}_${exp.idExperiencia}`
      );

      const filtered = _.filter(grouped, f => f.length > 1);

      const experienciaDuplicada = _.head(_.head(filtered));

      if (experienciaDuplicada) {
        const tiposExperiencias = this.TiposExperiencia();
        const nombreTipoExp =
          tiposExperiencias[experienciaDuplicada.idTipoExperiencia];
        const nombreExp = _.find(experienciaDuplicada.opcionesExperiencia, {
          value: experienciaDuplicada.idExperiencia
        });

        if (nombreExp) {
          Vue.$toast.warning(
            `Por favor revisar la experiencia en <strong>${nombreTipoExp} : ${nombreExp.label}</strong> se encuentra duplicada`,
            { duration: 5000 }
          );
        } else {
          Vue.$toast.warning(
            "Por favor revisar existen experiencias duplicadas."
          );
        }
        return true;
      }

      return false;
    },
    /**
     * Método invocado cuando un usuario selecciona un registro en un Pick
     */
    async selected(item) {
      switch (this.TipoListaPick) {
        case "Colaboradores":
          this.showModal = false;
          await this.cargarDetalleColaborador(item.id);
          this.solicitud.experiencias = [];
          this.solicitud.estado = "S";
          break;

        default:
          break;
      }
    },
    async cargarDetalleColaborador(id) {
      this.cargandoDatos = true;
      const {
        data: { colaborador }
      } = await GetColaboradorDetailResumen(id);

      this.colaborador = {
        idcolaborador: colaborador.idcolaborador,
        identificacion: colaborador.identificacion,
        nombre: colaborador.nombre,
        primerapellido: colaborador.primerapellido,
        segundoapellido: colaborador.segundoapellido,
        foto: colaborador.foto
      };
      this.terminaCargaDatos();
    },
    /**
     * Muestra la ventana modal para un pick, según el tipo correspondiente se invoca al API para
     * obtener los datos que se deben mostrar en el componente tabla del pick
     *
     * @tipo corresponde al tipo de pick a mostrar
     */
    async displayModal(tipo) {
      this.TipoListaPick = tipo;
      this.clean();
      switch (tipo) {
        case "Colaboradores":
          // aqui estarian los request para cargar el picklist segun se requiera
          const {
            data: { colaboradores }
          } = await GetColaboradoresPerfil(this.$root.infoColaboradorActual.id); //await GetAllColaboradores();
          this.dataPickList = colaboradores.map(
            ({
              idcolaborador,
              nombreCompleto,
              nombre,
              identificacion,
              primerapellido,
              segundoapellido,
              foto
            }) => ({
              id: idcolaborador,
              nombre: nombre,
              nombreCompleto: nombreCompleto,
              identificacion: identificacion,
              primerapellido: primerapellido,
              segundoapellido: segundoapellido,
              foto: foto
            })
          );

          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombreCompleto", header: "Nombre" }
          ];
          this.tituloModal = "Lista de colaboradores";
          this.showModal = true;
          this.nombreTipoLista = "Colaboradores";
          break;

        default:
          break;
      }
    },
    clean() {
      this.showModal = false;
      this.nombreTipoLista = "";
      this.dataPickList = null;
      this.filters = {};
    },
    async GetExperiencias(experiencias) {
      // si el colaborador tiene experiencias se formatea la cantidad y se agrega el atributo del estado a
      // cada una de las experiencias
      if (experiencias && experiencias != null && experiencias.length > 0) {
        experiencias.forEach(experiencia => {
          // se agrega el campo ESTADO con un valor "S" (S: Sin modificaciones N: Nuevo E: Eliminado M: Modificado)
          if (!experiencia.estado) this.$set(experiencia, "estado", "S");
        });

        return experiencias;
      }
      // sino se regresa una lista con una afiliacion nueva y vacía
      // else {
      //   return [this.getExperienciaNueva()];
      //  }
    },
    getExperienciaNueva() {
      return {
        idTipoExperiencia: null,
        idExperiencia: null,
        cantMesesXExperiencia: 0,
        estado: "N"
      };
    },
    /**
     * Evento para procesar la acción de los botones del flujo de trabajo
     */
    procesarAccion(accion) {
      return this["procesarAccion_" + accion](
        accion,
        this.datosSolicitud.idSolicitudMasterData
      );
    },
    /**
     * Guarda la solicitud en la base de datos, siempre y cuando la misma tenga cambios registrados.
     * Genera el objeto requerido para luego invocar al método común de guardado.
     */
    async procesarAccion_Guardar(accion, idSolicitud) {
      if (this.solicitud.estado == common.SOLICITUD_SINMODIFICACIONES) {
        Vue.$toast.warning(
          "La solicitud no tiene modificaciones que guardar.",
          {}
        );
      } else {
        let datos = {
          idSolicitud: this.datosSolicitud.idSolicitudMasterData,
          idTipoSolicitud: this.datosSolicitud.idTipoSolicitud,
          codigoEstado: this.datosSolicitud.estadoSolicitud,
          idAutor: this.$root.infoColaboradorActual.id,
          idResponsable: this.$root.infoColaboradorActual.id,
          idColaborador: this.colaborador.idcolaborador,
          docSolicitud: JSON.stringify({
            ...this.colaborador.idcolaborador,
            ...this.solicitud
          }) //JSON.stringify(this.solicitud)
        };

        this.datosSolicitud.idSolicitudMasterData = await this.Guardar(
          datos,
          this.datosSolicitud.idSolicitudMasterData
        );
      }
    },
    async procesarAccion_Revisar(accion) {
      let opciones = {
        tipo: "comentario", // confirmar comentario ninguno,
        titulo: "Solicitud de cambio",
        mensajeDialogo:
          "Registre una observación para el autor de la solicitud",
        btnOk: "Enviar a revisión",
        btnCancel: "Cancelar",
        mensajeExito: "Solicitud enviada a revisión.",
        mensajeError:
          "Se ha presentado un inconveniente al enviar a revisión la solicitud de cambio."
      };
      await this.enviarSolicitud(accion, opciones);
    },
    async procesarAccion_Descartar(accion) {
      let opciones = {
        tipo: "comentario", // confirmar comentario ninguno,
        titulo: "Solicitud de cambio",
        mensajeDialogo:
          "Registre una justificación para descartar la solicitud de cambio",
        btnOk: "Descartar",
        btnCancel: "Cancelar",
        mensajeExito: "Solicitud descartada.",
        mensajeError:
          "Se ha presentado un inconveniente al descartar la solicitud de cambio."
      };
      await this.enviarSolicitud(accion, opciones);
    },
    async procesarAccion_Denegar(accion) {
      let opciones = {
        tipo: "comentario", // confirmar comentario ninguno,
        titulo: "Solicitud de cambio",
        mensajeDialogo:
          "Registre una justificación para denegar la solicitud de cambio",
        btnOk: "Denegar",
        btnCancel: "Cancelar",
        mensajeExito: "Solicitud denegada.",
        mensajeError:
          "Se ha presentado un inconveniente al denegar la solicitud de cambio."
      };
      await this.enviarSolicitud(accion, opciones);
    },
    async procesarAccion_Aprobar(accion) {
      let opciones = {
        tipo: "confirmar", // confirmar comentario ninguno,
        titulo: "Solicitud de cambio",
        mensajeDialogo: "Está seguro de aprobar la solicitud?",
        btnOk: "Si",
        btnCancel: "No",
        mensajeExito: "Solicitud aprobada.",
        mensajeError:
          "Se ha presentado un inconveniente al aprobar la solicitud de cambio."
      };
      await this.enviarSolicitud(accion, opciones);
    },
    async enviarSolicitud(accion, opciones) {
      try {
        let datos = this.obtenerObjetoSolicitudEnviar(accion);
        await this.Enviar(datos, opciones);
      } catch (error) {
        console.log(error);
      }
    },

    obtenerObjetoSolicitudEnviar(accion) {
      return {
        idSolicitud: this.datosSolicitud.idSolicitudMasterData,
        idTipoSolicitud: this.datosSolicitud.idTipoSolicitud,
        codigoEstado: this.datosSolicitud.estadoSolicitud,
        idAutor: this.$root.infoColaboradorActual.id,
        idResponsable: this.$root.infoColaboradorActual.id,
        idColaborador: this.colaborador.idcolaborador,
        accion: accion,
        docSolicitud: JSON.stringify({
          ...this.colaborador.idcolaborador,
          ...this.solicitud
        })
      };
    },
    /**
     * Se posterga la actualización del indicador de carga hasta que se haya "renderizado" la vista
     */
    terminaCargaDatos() {
      this.$nextTick(() => {
        this.cargandoDatos = false;
      });
    },
    /**
     * Métodos para administrar la lista de contactos de una experiencia en un proyecto
     */
    undoExperiencia(index) {
      if (
        this.solicitud.experiencias[index].hashCode ==
        this.hashCode(
          this.solicitud.experiencias[index].contacto +
            this.solicitud.experiencias[index].telefono +
            this.solicitud.experiencias[index].correo
        )
      ) {
        this.solicitud.experiencias[index].estado = "S";
      } else {
        this.solicitud.experiencias[index].estado = "M";
      }
    },
    agregarExperiencia() {
      this.solicitud.experiencias[
        this.solicitud.experiencias.length - 1
      ].estado = "N";
      this.solicitud.estado = "M";
    },
    modificarExperiencia(index) {
      this.solicitud.estado = "M";
    },
    eliminarExperiencia(index) {
      this.solicitud.experiencias[index].estado = "E";
    },
    showLoader() {
      return this.$loading.show({
        // Optional parameters
        container: this.fullPage ? null : this.$refs.formContainer,
        canCancel: true,
        onCancel: this.onCancel
      });
    },
    hideLoader(loader) {
      setTimeout(() => {
        loader.hide();
      }, 300);
    },

    muestraBotonera() {
      if (this.origen == "Inbox") {
        if (!this.idResponsable || this.idResponsable == this.$root.infoColaboradorActual.id) {
          return true;
        } else {
          return false;
        }
      } else {
        return false;
      }
    }

  },
  watch: {},
  computed: {
    pickPuedeBuscarColaborador() {
      return this.datosSolicitud.estadoSolicitud == common.REGISTRADA; // && !(this.usuarioActual.Roles.length == 1 && this.usuarioActual.Roles[0] == "COLABORADOR") ;
    }
  }
};
</script>
<style>
.label-title {
  font-weight: 700;
}
.label-name {
  font-variant: small-caps;
  text-align: center;
}
.timeline .wrapper-item .section-year {
  font-size: 1rem !important;
}
.timeline .wrapper-item .date-item {
  font-size: 0.8rem !important;
  color: gainsboro !important;
}
.timeline .wrapper-item .description-item {
  font-size: 0.7rem !important;
}
.timeline-item .title-item {
  font-size: 0.8rem !important;
  font-variant: small-caps;
}
.timeline-item .item {
  border-left: 2px solid #ccd5db !important;
}

.cardBodyOverflow {
  overflow-y: auto;
  max-height: 400px;
  overflow-x: hidden;
}

.table-striped tbody tr:nth-of-type(odd) {
  background-color: rgba(173, 216, 230, 0.5) !important;
}

.move-right {
  padding-right: 0px !important;
  padding-left: 0px !important;
}

.libro-marca-celeste {
  background-color: #29abe2;
}

.libro-marca-azuloscuro {
  background-color: #002e6e;
}

.nav-pills .nav-link.active,
.nav-pills .show > .nav-link {
  color: #fff;
  background-color: #002e6e;
}

.nav-link {
  color: #002e6e;
}

.nav-link:hover {
  color: #29abe2;
}
.onHoverDark:hover {
  color: #fff;
  background-color: #002e6e;
}
.onHoverLigth:hover {
  color: #29abe2;
}

.card-fixed-height {
  height: 480px;
}
.card-fixed-min-height {
  min-height: 400px;
  height: 450px;
}

.breadcrumb-item + .font-xl.breadcrumb-item::before {
  color: rgb(140, 195, 38);
  content: ">>";
  padding: 0px 10px;
}
.table-striped tbody tr:nth-of-type(odd) {
  background-color: rgba(173, 216, 230, 0.5) !important;
}
.page-item.active .page-link {
  background-color: #29abe2;
  border-color: #29abe2;
}
.page-link {
  color: #29abe2;
}
.btn-link {
  color: #002e6e;
}
.pagination {
  justify-content: flex-end !important;
}

/* Responsive: en visualización normal se ocultan las etiquetas asociadas al título de cada columna */
.p-datatable-proyectos .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-proyectos .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-proyectos .p-datatable-tbody > tr > td {
  overflow-wrap: break-word;
}
.p-datatable .p-datatable-header {
  background-color: unset;
  border: unset;
}

/* configuración de los íconos de ordenamiento */
.p-datatable .p-sortable-column .p-sortable-column-icon {
  color: darkgray;
  font-size: 0.75rem;
}
.p-datatable .p-sortable-column.p-highlight .p-sortable-column-icon {
  color: white;
}

/* configuración del "striped" y fila seleccionada de una tabla */
.p-datatable.p-datatable-striped .p-datatable-tbody > tr:nth-child(even) {
  background-color: rgba(173, 216, 230, 0.5);
}

.p-datatable .p-datatable-tbody > tr.p-highlight {
  background: rgba(12, 123, 190, 0.5);
  /* color: #495057; */
}

.p-datatable.p-datatable-striped
  .p-datatable-tbody
  > tr:nth-child(even).p-highlight {
  background: rgba(12, 123, 190, 0.5);
  /* color: #495057; */
}

.p-datatable .p-datatable-tbody > tr > td {
  border: 1px solid rgba(175, 189, 216, 1);
}
.p-datatable table {
  width: 99%;
}

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de
   etiqueta y valor asociado */
@media screen and (max-width: 40em) {
  .p-datatable.p-datatable-proyectos .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-proyectos .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-proyectos .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-proyectos .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-proyectos:last-child {
    border-bottom: 1px solid var(--surface-d);
  }
}

@media screen and (max-width: 1200px) {
  .card-fixed-height {
    height: auto !important;
  }
}
@media screen and (max-width: 1200px) {
  .card-fixed-min-height {
    height: auto !important;
    min-height: auto !important;
  }
}
</style>
