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
        <strong> Servicios Laborales</strong>
        <small> Solicitud administrativa </small>
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
        <FormulateForm
          v-model="solicitud"
          @submit="submit()"
          @failed-validation="errorValidacion"
          :invalid-message="this.invalidMessage"
        >
          <FormulateErrors />
          <resumen-colaborador
            :colaborador.sync="colaborador"
            :estado-solicitud="datosSolicitud.estadoSolicitud"
            :searchable="pickPuedeBuscarColaborador"
            :editable="false"
            :habilitar-controles="
              this.habilitarControlesSolicitud(
                this.datosSolicitud.estadoSolicitud,
                this.origen
              )
            "
            @displayModal="displayModal"
          >
          </resumen-colaborador>
        </FormulateForm>
      </CCardBody>
    </CCard>

    <CCard style="border-radius: 16px !important;">
      <CCardHeader
        style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
      >
      </CCardHeader>

      <CCardBody>
        <FormulateForm
          v-model="solicitud"
          name="solicitud"
          @submit="submitSolicitud"
        >
          <CRow style="margin-bottom: 20px;">
            <CCol sm="1" class="align-self-center text-center">
              <img
                src="/img/icono-servicios-laborales.png"
                style="width:75px;height:75px"
              />
            </CCol>
            <CCol sm="11">
              <FormulateInput
                name="descripcion"
                type="textarea"
                rows="3"
                label="Indique alguna descripción adicional sobre la certificación de servicios laborales requerida"
                placeholder="Descripción"
                validation="required|not:0"
                validation-name="Empresa"
                :validation-messages="{
                  required: 'Debe indicar una descripción ',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
                :disabled="!this.puedePintar"
              />
            </CCol>
          </CRow>
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

          <CRow style="margin-bottom: 80px;">
            <CCol>
              <botonera-master-data
                :estado-solicitud="this.datosSolicitud.estadoSolicitud"
                nombreEventoProcesar="procesarAccion"
                @procesarAccion="procesarAccion"
                tipo="AD"
                v-if="muestraBotonera()"
              >
              </botonera-master-data>
            </CCol>
          </CRow>
        </FormulateForm>
      </CCardBody>
    </CCard>

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

    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
    <comment-dialogue ref="commentDialogue"></comment-dialogue>
  </div>
</template>

<script>
import {
  GetColaboradoresPerfil,
  GetEmpresaCorporacion,
  GetColaboradorDetailResumen,
  GetDetalleSolicitud
} from "../masterdata/request";

import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "../masterdata/ResumenColaborador";
import BotoneraMasterData from "../masterdata/BotoneraMasterData.vue";
import Vue from "vue";

// métodos comunes
import common_admin from "./common-admin.js";
import common from "../masterdata/common.js";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";
import Comments from "../generales/Componentes/Comments/Comments.vue";

export default {
  name: "ServiciosLaborales",
  mixins: [common_admin, common],
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
        descripcion: null,
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
      cargandoDatos: false,
      IdSolicitudJson: null, // id de la solicitud para cargar el json de una solicitud
      tituloModal: ""
    };
  },
  async mounted() {
    this.IdSolicitudJson = this.idSolicitud;

    if (this.IdSolicitudJson) {
      await this.obtenerSolicitud();
    } else {
      //carga el usuario en session
      this.TipoListaPick = "Colaboradores";
      this.selected(this.$root.infoColaboradorActual);
    }
  },
  methods: {
    muestraBotonera() {
      if (this.origen == "PendientesAtender" || this.origen == "CertificacionesTramitadas" ) {
        if (!this.idResponsable || this.idResponsable == this.$root.infoColaboradorActual.id) {
          return true;
        } else {
          return false;
        }
      } else {
        return false;
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
        var json = JSON.parse(documento);
        return {
          solicitud: {
            estado: json.estado,
            descripcion: json.descripcion
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

      GetDetalleSolicitud(this.IdSolicitudJson)
        .then(response => {
          // objeto devuelto por el API
          var solicitud = response.data.solicitudMasterData;

          // se inicializan los datos base de la solicitud
          this.datosSolicitud = this.getDatosSolicitud(solicitud);

          this.cargarDetalleColaborador(this.datosSolicitud.idcolaborador);

          let documento = this.leerDocumentoSolicitud(solicitud.docSolicitud);
          this.solicitud = documento.solicitud;
          //this.colaborador = documento.colaborador;

          //this.solicitud.iddeliverymanager = documento.solicitud.iddeliverymanager;

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
        })
        .catch(response => {
          console.log(response);
          this.terminaCargaDatos();
        });
    },
    /**
     * Se posterga la actualización del indicador de carga hasta que se haya "renderizado" la vista
     */
    terminaCargaDatos() {
      this.$nextTick(() => {
        this.cargandoDatos = false;
      });
    },

    async submit(formValues) {
      try {
        // const { data } = await Upsert(formValues);
        //  console.log(formValues);
      } catch (error) {
        console.log(error);
      }
    },
    selected(item) {
      switch (this.TipoListaPick) {
        case "Colaboradores":
          // obtener el detalle del colaborador
          this.cargarDetalleColaborador(item.id);
          this.showModal = false;
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
        foto: colaborador.foto,
        idempresacorporacion: colaborador.idempresacorporacion
      };

      this.terminaCargaDatos();
    },
    async displayModal(tipo) {
      this.TipoListaPick = tipo;
      this.clean();
      switch (tipo) {
        case "Colaboradores":
          // aqui estarian los request para cargar el picklist segun se requiera
          const {
            data: { colaboradores }
          } = await GetColaboradoresPerfil(this.colaborador.idcolaborador); //await GetAllColaboradores();
          this.dataPickList = colaboradores.map(
            ({
              idcolaborador,
              nombreCompleto,
              nombre,
              identificacion,
              primerapellido,
              segundoapellido,
              foto,
              iddeliverymanager,
              empresaCorporacion,
              idempresacorporacion
            }) => ({
              id: idcolaborador,
              nombre: nombre,
              nombreCompleto: nombreCompleto,
              identificacion: identificacion,
              primerapellido: primerapellido,
              segundoapellido: segundoapellido,
              foto: foto,
              iddeliverymanager: iddeliverymanager,
              idempresacorporacion: idempresacorporacion,
              empresaCorporacion: empresaCorporacion
            })
          );

          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombreCompleto", header: "Nombre" }
          ];

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
    },

    // Metodos de la botonera

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
        titulo: "Solicitud administrativa",
        mensajeDialogo:
          "Registre una justificación para denegar la certificación de servicios laborales ",
        btnOk: "Denegar",
        btnCancel: "Cancelar",
        mensajeExito: "Solicitud denegada.",
        mensajeError:
          "Se ha presentado un inconveniente al denegar la certificación de servicios laborales ."
      };
      await this.enviarSolicitud(accion, opciones);
    },
    async procesarAccion_Aprobar(accion) {
      let opciones = {
        tipo: "confirmar", // confirmar comentario ninguno,
        titulo: "Solicitud administrativa",
        mensajeDialogo:
          "Está seguro de aprobar la certificación de servicios laborales ?",
        btnOk: "Si",
        btnCancel: "No",
        mensajeExito: "Solicitud aprobada.",
        mensajeError:
          "Se ha presentado un inconveniente al aprobar la certificación de servicios laborales ."
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

    async submitSolicitud(formValues) {
      try {
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
            accion: "Enviar",
            docSolicitud: JSON.stringify({
              ...this.colaborador.idcolaborador,
              ...this.solicitud
            })
          };

          let opciones = {
            tipo: "confirmar", // confirmar comentario ninguno
            titulo: "Solicitud administrativa",
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
    }
  },
  watch: {
    /**
     * Controla el cambio de alguno de los campos de empresa colaborador, esto permitirá marcar
     * el documento con un estado de modificado
     */
    solicitud: function(newValue, oldValue) {
      if (!this.cargandoDatos) {
        this.solicitud.estado = "M";
      }
    }
  },
  computed: {
    puedePintar() {
      return this.habilitarControlesSolicitud(
        this.datosSolicitud.estadoSolicitud,
        this.origen
      );
    },
    pickPuedeBuscarColaborador() {
      return this.datosSolicitud.estadoSolicitud == common.REGISTRADA; // && !(this.usuarioActual.Roles.length == 1 && this.usuarioActual.Roles[0] == "COLABORADOR") ;
    },
    mostrarBotonera() {
      return (
        (this.origen == "CertificacionesTramitadas" &&
          this.idSolicitud == null) ||
        (this.origen == "PendientesAtender" && this.idSolicitud != null)
      );
    }
  }
};
</script>
<style>
@import "styles.css";
</style>
