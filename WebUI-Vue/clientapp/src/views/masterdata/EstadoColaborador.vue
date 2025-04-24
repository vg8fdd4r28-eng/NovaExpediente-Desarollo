<template>
  <div>
    <CRow>
      <CCol>
        <CButton
          block
          color="primary"
          @click="atras()"
          class="btn btn-primary btn-lg"
          style="
            height: 40px;
            width: 130px;
            float: right;
            color: rgba(98, 184, 235, 1);
            background-color: transparent;
            border-color: transparent;
          "
        >
          <i class="pi pi-arrow-circle-left"></i>&nbsp;Regresar</CButton
        >
      </CCol>
    </CRow>

    <CCard style="border-radius: 16px !important">
      <CCardHeader
        style="
          border-radius: 16px !important;
          border-bottom: transparent !important;
          background-color: #ffffff;
        "
      >
        <strong> Actualizar el estado de un colaborador</strong>
        <small> Datos Personales </small>
        <CBadge
          color="primary"
          class="mr-2 libro-marca-azuloscuro"
          shape="pill"
          v-if="this.datosSolicitud.idSolicitudMasterData"
        >
          <span style="margin-top: 0.2rem; margin-bottom: 0.2rem">
            {{ this.datosSolicitud.idSolicitudMasterData }}
          </span>
        </CBadge>
        <CBadge
          :color="this.obtenerColorEstado(this.datosSolicitud.estadoSolicitud)"
          class="mr-2"
          shape="pill"
        >
          <span style="margin-top: 0.2rem; margin-bottom: 0.2rem">
            {{
              this.obtenerDescripcionEstado(this.datosSolicitud.estadoSolicitud)
            }}
          </span>
        </CBadge>
      </CCardHeader>
      <CCardBody>
        <FormulateForm>
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

    <CCard style="border-radius: 16px !important">
      <CCardHeader
        style="
          border-radius: 16px !important;
          border-bottom: transparent !important;
          background-color: #ffffff;
        "
      >
      </CCardHeader>
      <CCardBody>
        <FormulateForm v-model="solicitud" @submit="submitSolicitud">
          <CRow>
            <CCol sm="6">
              <h2 class="text-divider">
                <span>Estado del colaborador</span>
              </h2>
              <b>{{ this.estadoActualLabel }}</b>
            </CCol>
            <CCol sm="6">
              <h2 class="text-divider">
                <span>Nuevo estado</span>
              </h2>
              <FormulateInput
                name="estadoColaborador"
                :options="{
                  1: 'Activo',
                  0: 'Inactivo',
                }"
                type="select"
                placeholder="Seleccione el estado"
                label=""
                :input-class="['form-control']"
                validation="required"
                validation-name="estado"
                :disabled="!puedePintar"
              />
            </CCol>
          </CRow>

          <CRow v-show="reactivacion">
            <CCol sm="6" class="required">
              <label>Solicita vacaciones</label>
              <FormulateInput
                type="myInputSwitch"
                name="solicitaVacaciones"
                :input-class="['form-control', 'readonly']"
                validation-name="Servicios profesionales"
                class="hint"
                :disabled="!puedePintar"
              />
            </CCol>
            <CCol sm="6" class="required">
              <label>Es servicios profesionales</label>
              <FormulateInput
                type="myInputSwitch"
                name="esservicioprofesional"
                :input-class="['form-control', 'readonly']"
                validation-name="Servicios profesionales"
                class="hint"
                :disabled="!puedePintar"
              />
            </CCol>
          </CRow>
          <CRow v-show="reactivacion">
            <CCol sm="6" class="">
              <FormulateInput
                type="number"
                name="idNomina"
                label="Id de nómina"
                placeholder="Ingrese el id de nómina"
                validation="number"
                input-class="form-control"
                validation-name="Id de nómina"
                :disabled="!puedePintar"
              />
            </CCol>
            <CCol sm="6" class="">
              <FormulateInput
                type="number"
                name="vacacionesdiasanuales"
                label="Cantidad de vacaciones por año"
                placeholder="Ingrese la cantidad de vacaciones por año"
                validation="number"
                input-class="form-control"
                validation-name="Cantidad de vacaciones por año"
                :disabled="!puedePintar"
              />
            </CCol>
          </CRow>
          <CRow v-show="reactivacion">
            
            <CCol sm="5" class="required">
              <FormulateInput
                name="paisFeriado"
                label="País para obtener los feriados"
                placeholder="Seleccione un país"
                validation-name="País para feriados"
                :validation-messages="{
                  required: 'Seleccione un país.',
                  not: 'Debe de seleccionar una opción válida.',
                }"
                :input-class="['form-control']"
                :disabled="true"
              />
            </CCol>
            <CCol sm="1" class="mt-4">
              <CButton
                class="btn btn-info mt-1 pull-right w-100 libro-marca-celeste onHoverDark"
                @click="displayModal('PaisesFeriados')"
                :disabled="!puedePintar"
              >
                <li class="pi pi-search"></li>
              </CButton>
            </CCol>
            <CCol sm="6" class="required">
              <FormulateInput
                type="date"
                name="fechaingreso"
                label="Fecha de ingreso"
                input-class="form-control"
                :help-class="['hint']"
                :disabled="!puedePintar"
              />
            </CCol>
          </CRow>
 <CRow v-show="reactivacion">
             <CCol sm="6" class="">
              <FormulateInput
                type="number"
                name="saldoVacaciones"
                label="Saldo de vacaciones"
                placeholder="Ingrese el saldo de vacaciones"
                validation="number"
                input-class="form-control"
                validation-name="Saldo de vacaciones"
                :disabled="!puedePintar"
              />
            </CCol>
          </CRow>
          <CRow v-show="!reactivacion">
             <CCol sm="6" class="">
      
              <FormulateInput
                name="motivoSalida"
                :options="{
                  1: 'Renuncia',
                  2: 'Despidos con responsabilidad patronal',
                  3: 'Despidos sin responsabilidad patronal',
                }"
                type="select"
                placeholder="Seleccione el motivo de salida"
                label="Motivo de salida"
                :input-class="['form-control']"
                validation="required"
                validation-name="motivo de salida"
                :disabled="!puedePintar"
              />
            </CCol>
          </CRow>

          <CCard
            v-if="this.comentarios.length > 0"
            style="border-radius: 16px !important"
          >
            <CCardHeader
              style="
                border-radius: 16px !important;
                border-bottom: transparent !important;
                background-color: #ffffff;
              "
            >
              <strong> Comentarios</strong>
            </CCardHeader>
            <CCardBody>
              <comments
                :comments_wrapper_classes="[
                  'custom-scrollbar',
                  'comments-wrapper',
                ]"
                :comments="this.comentarios"
              ></comments>
            </CCardBody>
          </CCard>

          <CRow style="margin-bottom: 80px">
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

    <CModal
      title="Seleccione un registro por favor"
      :show.sync="showModal"
      color="primary"
      class="modal-libro"
    >
      <div>
        <pick-list
          @selected="selected"
          :nombreTipoLista="nombreTipoLista"
          :columnas="columnasPickList"
          :dataContenido="dataPickList"
        ></pick-list>
      </div>
      <template #footer>
        <div></div>
      </template>
    </CModal>
    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
    <comment-dialogue ref="commentDialogue"></comment-dialogue>
  </div>
</template>

<script>
import {
  GetColaboradoresAllPorPerfil,
  GetColaboradorDetailResumen,
  GetDetalleSolicitud,
  GetAllPaises,
} from "./request";
import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "./ResumenColaborador";
import BotoneraMasterData from "./BotoneraMasterData.vue";
import Vue from "vue";

// métodos comunes
import common from "./common.js";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";
import Comments from "../generales/Componentes/Comments/Comments.vue";

export default {
  name: "EstadoColaborador",
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
    "botonera-master-data": BotoneraMasterData,
    "confirm-dialogue": ConfirmDialogue,
    "comment-dialogue": CommentDialogue,
    comments: Comments,
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
        foto: null,
      },
      solicitud: {
        estadoColaborador: 0,
        estadoActualColaborador: null,
        paisFeriado: "",
        esservicioprofesional: "No",
        esPayRoll: "Si",
        solicitaVacaciones: "Si",
        fechaingreso: null,
        vacacionesdiasanuales:0,
        idNomina:0,
        estado: "N",
        motivoSalida:1
      },
      comentarios: [],
      datosSolicitud: {
        idSolicitudMasterData: null,
        idTipoSolicitud: this.idTipoSolicitud,
        tipoSolicitud: this.tipoSolicitud,
        estadoSolicitud: "RE",
      },
      showModal: false,
      nombreTipoLista: "",
      columnasPickList: [],
      dataPickList: [],
      cargandoDatos: false,
      IdSolicitudJson: null,
    };
  },
  mounted() {
    // se cargarian los request en caso de ser necesario o en created...
    this.IdSolicitudJson = this.idSolicitud;

    if (this.IdSolicitudJson) {
      this.cargarSolicitud();
    } else {
      //carga el usuario en session
      this.TipoListaPick = "Colaboradores";
      this.selected(this.$root.infoColaboradorActual);
    }
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
           case common.ANULADA:
          return "Anulada";
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
            foto: colaborador.foto,
          },
          solicitud: {
            // estadoColaborador: colaborador.estadoColaborador == true ? 1 : 0,
            estadoColaborador: colaborador.estadoColaborador,
            estadoActualColaborador: colaborador.estadoActualColaborador,
            paisFeriado:  colaborador.paisFeriado,
        esservicioprofesional: colaborador.esservicioprofesional,
        esPayRoll: colaborador.esPayRoll,
        solicitaVacaciones: colaborador.solicitaVacaciones,
        fechaingreso: colaborador.fechaingreso,
        vacacionesdiasanuales:parseInt(colaborador.vacacionesdiasanuales),
        idNomina:parseInt(colaborador.idNomina),
        idpaisferiado: colaborador.idpaisferiado,
        motivoSalida: colaborador.motivoSalida

          },
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
        .then((response) => {
          // objeto devuelto por el API
          var solicitud = response.data.solicitudMasterData;

          // se inicializan los datos base de la solicitud
          this.datosSolicitud = this.getDatosSolicitud(solicitud);

          this.cargarDetalleColaborador(
            this.datosSolicitud.idcolaborador,
            true
          );

          let documento = this.leerDocumentoSolicitud(solicitud.docSolicitud);
          this.solicitud = documento.solicitud;
          this.colaborador = documento.colaborador;

          //this.solicitud.iddeliverymanager = documento.solicitud.iddeliverymanager;

          // comentarios de la solicitud
          this.comentarios = solicitud.comentarios.map(
            ({
              idComentarioSolicitud,
              responsable,
              comentario,
              accion,
              fecha,
            }) => ({
              id: idComentarioSolicitud,
              responsable: responsable,
              comentario: comentario,
              accion: accion,
              fecha: fecha,
            })
          );

          // se posterga la actualización del indicador de carga hasta que se haya "renderizado" la vista
          this.terminaCargaDatos();
          this.hideLoader(loader);
        })
        .catch((response) => {
          console.log(response);
          this.terminaCargaDatos();
          this.hideLoader(loader);
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
        console.log(formValues);
      } catch (error) {
        console.log(error);
      }
    },
    selected(item) {
      switch (this.TipoListaPick) {
        case "Colaboradores":
          // traer detail de colaborador

          this.cargarDetalleColaborador(item.id);
          this.showModal = false;

          break;
        case "PaisesFeriados":
          this.solicitud.paisFeriado = item.nombre;
          this.solicitud.idpaisferiado = item.id;
          this.showModal = false;
          break;
        default:
          break;
      }
    },
    async displayModal(tipo) {
      this.TipoListaPick = tipo;
      this.clean();
      switch (tipo) {
        case "Colaboradores":
          // aqui estarian los request para cargar el picklist segun se requiera
          const {
            data: { colaboradores },
          } = await GetColaboradoresAllPorPerfil(this.colaborador.idcolaborador); //await GetAllColaboradores();
          this.dataPickList = colaboradores.map(
            ({
              idcolaborador,
              nombreCompleto,
              nombre,
              identificacion,
              primerapellido,
              segundoapellido,
              foto,
              activo,
            }) => ({
              id: idcolaborador,
              nombre: nombre,
              nombreCompleto: nombreCompleto,
              identificacion: identificacion,
              primerapellido: primerapellido,
              segundoapellido: segundoapellido,
              foto: foto,
              estadoColaborador: activo,
            })
          );

          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombreCompleto", header: "Nombre" },
          ];

          this.showModal = true;
          this.nombreTipoLista = "Colaboradores";
          break;

        case "PaisesFeriados":
          const paisesFeriados = await GetAllPaises();
          this.dataPickList = paisesFeriados.data.paises.map(
            ({ idpais, nombre }) => ({
              id: idpais,
              nombre: nombre,
            })
          );

          this.columnasPickList = [
            // { field: "id", header: "Id" },
            { field: "nombre", header: "Nombre" },
          ];

          this.showModal = true;
          this.nombreTipoLista = "país";
          this.tituloModal = "Lista de países";
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
    async cargarDetalleColaborador(id) {
      this.cargandoDatos = true;
      const {
        data: { colaborador },
      } = await GetColaboradorDetailResumen(id);

      this.colaborador = {
        idcolaborador: colaborador.idcolaborador,
        identificacion: colaborador.identificacion,
        nombre: colaborador.nombre,
        primerapellido: colaborador.primerapellido,
        segundoapellido: colaborador.segundoapellido,
        foto: colaborador.foto,
      };

      if (
        this.datosSolicitud.estadoSolicitud != "AP" &&
        this.datosSolicitud.estadoSolicitud != "DE"
      ) {
        this.solicitud.estadoActualColaborador =
          colaborador.activo == true ? 1 : 0;
      }

      this.terminaCargaDatos();
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
            ...this.solicitud,
          }), //JSON.stringify(this.solicitud)
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
          ...this.solicitud,
        }),
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
          "Se ha presentado un inconveniente al enviar a revisión la solicitud de cambio.",
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
          "Se ha presentado un inconveniente al descartar la solicitud de cambio.",
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
          "Se ha presentado un inconveniente al denegar la solicitud de cambio.",
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
          "Se ha presentado un inconveniente al aprobar la solicitud de cambio.",
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
          if (
            this.solicitud.estadoColaborador ==
            this.solicitud.estadoActualColaborador
          ) {
            Vue.$toast.warning(
              "Debe de seleccionar un estado distinto al actual.",
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
                ...this.solicitud,
              }),
            };

            let opciones = {
              tipo: "confirmar", // confirmar comentario ninguno
              titulo: "Solicitud de cambio",
              mensajeDialogo: "Está seguro de enviar la solicitud?",
              btnOk: "Si",
              btnCancel: "No",
              mensajeExito: "Solicitud enviada exitosamente.",
              mensajeError:
                "Se ha presentado un inconveniente al enviar la solicitud de cambio.",
            };

            await this.Enviar(datos, opciones);
          }
        }
      } catch (error) {
        console.log(error);
      }
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
  watch: {
    /**
     * Controla el cambio de alguno de los campos de estado de colaborador, esto permitirá marcar
     * el documento con un estado de modificado
     */
    solicitud: function (newValue, oldValue) {
      if (!this.cargandoDatos) {
        this.solicitud.estado = "M";
      }
    },
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
    estadoActualLabel() {
      if (this.solicitud.estadoActualColaborador == 1) {
        return "Activo";
      } else {
        return "Inactivo";
      }
    },
    reactivacion() {
      if (this.solicitud) {
        return this.solicitud.estadoColaborador == '1';
      }
     
    }
  },
};
</script>
