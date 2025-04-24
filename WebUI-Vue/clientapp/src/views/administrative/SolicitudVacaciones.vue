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
        <strong> Solicitud de Vacaciones</strong>
        <small> Solicitud administrativa </small>
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
        <FormulateForm
          v-model="solicitud"
          name="solicitud"
          @submit="submitSolicitud"
        >
          <CRow>
            <CCol sm="1" class="align-self-center text-center">
              <img
                src="/img/icono-vacaciones.png"
                style="width: 75px; height: 75px"
              />
            </CCol>

            <CCol sm="2" v-show="this.MostrarSaldos">
              <CCol sm="12" class="align-self-center text-center">
                <label>Saldo actual</label>
                <h4>
                  <CBadge
                    :color="colorSaldo(this.solicitud.saldoVacaciones)"
                    class="mr-2"
                    shape="pill"
                  >
                    {{ this.saldoVacacionesComputed }}
                  </CBadge>
                </h4>
              </CCol>
            </CCol>

            <CCol sm="2">
              <!-- sm 2 -->
              <FormulateInput
                type="date"
                name="fechaInicio"
                label="Fecha de inicio"
                :validation="'required'"
                :validation-messages="{
                  required: 'Debe de ingresar la fecha de inicio.',
                  after:
                    'La fecha de inicio debe ser mayor o igual a la actual.',
                }"
                input-class="form-control"
                validation-name="Fecha inicio"
                :help-class="['hint']"
                :disabled="!puedePintar"
                @change="CalcularDiasAPI"
              />
            </CCol>
            <CCol sm="2">
              <!-- sm 2 -->
              <FormulateInput
                type="date"
                name="fechaFinal"
                label="Fecha de finalización"
                :validation="'required'"
                :validation-messages="{
                  required: 'Debe de ingresar la fecha de finalización.',
                  after:
                    'La fecha de finalización debe ser mayor o igual a la actual.',
                }"
                input-class="form-control"
                validation-name="Fecha finalización"
                :help-class="['hint']"
                :disabled="!puedePintar"
                @change="CalcularDiasAPI"
              />
            </CCol>
            <CCol sm="2">
              <FormulateInput
                name="cantDias"
                label="Cantidad de días"
                :input-class="['form-control']"
                :disabled="true"
              />
            </CCol>

            <CCol sm="2" v-show="this.MostrarSaldos">
              <CCol sm="12" class="align-self-center text-center">
                <label>Nuevo saldo</label>

                <h4>
                  <CBadge
                    :color="colorSaldo(this.solicitud.nuevoSaldo)"
                    class="mr-2"
                    shape="pill"
                  >
                    {{ this.nuevoSaldo }}
                  </CBadge>
                </h4>
              </CCol>
            </CCol>

            <!-- <CCol
              sm="2"
              v-show="this.colaborador.idnomina == null"
              class="mt-4"
            >
              <CButton
                color="primary"
                @click="consultarSaldo()"
                class="pull-right btn btn-info libro-marca-celeste onHoverDark w-100"
                v-tooltip.top="
                  'Envía un correo al encargado para consultar tu saldo.'
                "
              >
                <i class="pi pi-mail"></i>
                Consultar saldo</CButton
              >
            </CCol> -->
          </CRow>

          <CRow>
            <CCol sm="1" class="align-self-center text-center"> </CCol>
          </CRow>

          <CRow>
            <CCol sm="1" class="align-self-center text-center"> </CCol>
            <CCol sm="10">
              <FormulateInput
                name="descripcion"
                type="textarea"
                rows="3"
                validation="max:500,length"
                label="Indique alguna descripción adicional sobre la solicitud de vacaciones requerida"
                placeholder="Descripción"
                :input-class="['form-control']"
                :disabled="!this.puedePintar"
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
                tipo="AD"
                v-if="muestraBotonera()"
              >
              </botonera-master-data>
            </CCol>
          </CRow>
        </FormulateForm>
      </CCardBody>
    </CCard>

    <!-- <CRow v-show="this.colaborador.idnomina != null"> -->
    <CRow>
      <CCol sm="1" class="align-self-center text-center"> </CCol>
      <CCol sm="9">
        <div>
          <p class="speech-bubble">
            <b>Nota: </b> El saldo actual podría no estar actualizado, debido a
            solicitudes pendientes de aprobar o sincronización con el sistema de
            nómina.
          </p>
        </div>
      </CCol>
      <CCol sm="1"> </CCol>
    </CRow>

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
  GetDetalleSolicitud,
  GetVacacionesNomina,
  GetColaboradorVacaciones,
  GetCalculaCantidadDiasListQuery,
  EnvioCorreo,
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
import moment from "moment";

export default {
  name: "SolicitudVacaciones",
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
        fechaInicio: null,
        fechaFinal: null,
        //   medioInicio: "No",
        //  medioFinal: "No",
        cantDias: 0,
        estado: "S",
        saldoVacaciones: 0,
        nuevoSaldo: 0,
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
      IdSolicitudJson: null, // id de la solicitud para cargar el json de una solicitud
      tituloModal: "",
      MostrarSaldos:true
    };
  },
  async mounted() {
    this.IdSolicitudJson = this.idSolicitud;

    if (this.IdSolicitudJson) {
      await this.obtenerSolicitud();
    } else {
      //carga el usuario en session
      this.TipoListaPick = "Colaboradores";
      await this.selected(this.$root.infoColaboradorActual);
      // if (
      //   this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
      //   this.datosSolicitud.estadoSolicitud == common.CORREGIR
      // ) {
      //   await this.GetSaldoVacaciones(this.$root.infoColaboradorActual.id);
      // }
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
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        if (fields[key].type !== "group") {
          fieldNames += `-  ${fields[key].label}\r\n`;
        } else {
          switch (fields[key].name) {
            case "afiliaciones":
              fieldNames += "-  Sección de otras afiliaciones\r\n";
              break;

            default:
              break;
          }
        }
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    /**
     * Atiende el evento "failed-validation" del FormulateForm
     * El método muestra un mensaje temporal indicando que el
     * formulario presenta errores de validación
     *
     * @data contiene la lista de validaciones que no se cumplen
     *  */

    errorValidacion(data) {
      Vue.$toast.warning(common.MENSAJE_ERROR, {
        // optional options Object
      });
    },

    colorSaldo(saldo) {
      if (saldo > 0) {
        return "primary";
      } else {
        return "danger";
      }
    },
    async GetSaldoVacaciones(id) {
      if (id) {
        const {
          data: { colaboradorVacaciones,muestraSaldoVacaciones },
        } = await GetColaboradorVacaciones(id);
        this.solicitud.saldoVacaciones =
          colaboradorVacaciones.saldovacacionesactual.toFixed(2);
        this.MostrarSaldos = muestraSaldoVacaciones;
      }
    },

    async CalcularDiasAPI() {
      var final = moment(this.solicitud.fechaFinal);
      var inicial = moment(this.solicitud.fechaInicio);

      if (final < inicial) {
        Vue.$toast.error("La fecha final debe ser mayor a la inicial.", {});
        this.solicitud.cantDias = 0;
      } else {
        if (
          this.solicitud.fechaInicio != null &&
          this.solicitud.fechaFinal != null
        ) {
          const data = await GetCalculaCantidadDiasListQuery(
            this.colaborador.idcolaborador,
            this.solicitud.fechaInicio,
            this.solicitud.fechaFinal
          );
          this.solicitud.cantDias = data.data.cantidad;
        }
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
            fechaInicio: json.fechaInicio,
            fechaFinal: json.fechaFinal,
            descripcion: json.descripcion,
            //  medioFinal: json.medioFinal,
            cantDias: json.cantDias,
            saldoVacaciones: this.setValue(json.saldoVacaciones),
            nuevoSaldo: this.setValue(json.nuevoSaldo),
          },
        };
      } catch (error) {
        console.log("Error al leer el documento JSON de la solicitud");
        console.error(error);
      }
    },

    setValue(val) {
      var res;

      if (typeof val == typeof undefined) {
        return "No disponible";
      } else {
        return val;
      }
    },

    /**
     * Invoca al API que obtiene los datos de una solicitud
     */
    async cargarSolicitud() {
      this.cargandoDatos = true;

      GetDetalleSolicitud(this.IdSolicitudJson)
        .then((response) => {
          // objeto devuelto por el API
          var solicitud = response.data.solicitudMasterData;

          // se inicializan los datos base de la solicitud
          this.datosSolicitud = this.getDatosSolicitud(solicitud);

          this.cargarDetalleColaborador(this.datosSolicitud.idcolaborador);

          let documento = this.leerDocumentoSolicitud(solicitud.docSolicitud);

          if (
            this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
            this.datosSolicitud.estadoSolicitud == common.CORREGIR
          ) {
            this.GetSaldoVacaciones(this.colaborador.idcolaborador);
          }

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
        })
        .catch((response) => {
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
    async selected(item) {
      switch (this.TipoListaPick) {
        case "Colaboradores":
          // obtener el detalle del colaborador
          await this.cargarDetalleColaborador(item.id);

          setTimeout(() => {
            if (
              this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
              this.datosSolicitud.estadoSolicitud == common.CORREGIR
            ) {
              this.GetSaldoVacaciones(item.id);
            }
          }, 1500);

          this.showModal = false;
          break;

        default:
          break;
      }
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
        idempresacorporacion: colaborador.idempresacorporacion,
        codpaisresidencia: colaborador.codpaisresidencia,
        idnomina: colaborador.idnomina,
        solicitavacaciones: colaborador.solicitavacaciones,
        vacacionesdiasanuales: colaborador.vacacionesdiasanuales,
        saldovacaciones: colaborador.saldovacaciones,
        esservicioprofesional: colaborador.esservicioprofesional,
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
            data: { colaboradores },
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
              idempresacorporacion,
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
              empresaCorporacion: empresaCorporacion,
            })
          );

          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombreCompleto", header: "Nombre" },
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
        titulo: "Solicitud administrativa",
        mensajeDialogo:
          "Registre una justificación para denegar la solicitud de vacaciones",
        btnOk: "Denegar",
        btnCancel: "Cancelar",
        mensajeExito: "Solicitud denegada.",
        mensajeError:
          "Se ha presentado un inconveniente al denegar la solicitud de vacaciones.",
      };
      await this.enviarSolicitud(accion, opciones);
    },
    async procesarAccion_Aprobar(accion) {
      let opciones = {
        tipo: "confirmar", // confirmar comentario ninguno,
        titulo: "Solicitud administrativa",
        mensajeDialogo: "Está seguro de aprobar la solicitud de vacaciones?",
        btnOk: "Si",
        btnCancel: "No",
        mensajeExito: "Solicitud aprobada.",
        mensajeError:
          "Se ha presentado un inconveniente al aprobar la solicitud de vacaciones.",
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
              ...this.solicitud,
            }),
          };

          let opciones = {
            tipo: "confirmar", // confirmar comentario ninguno
            titulo: "Solicitud administrativa",
            mensajeDialogo: "Está seguro de enviar la solicitud?",
            btnOk: "Si",
            btnCancel: "No",
            mensajeExito: "Solicitud enviada exitosamente.",
            mensajeError:
              "Se ha presentado un inconveniente al enviar la solicitud de cambio.",
          };

          if (this.solicitud.cantDias == 0) {
            Vue.$toast.error(
              "La solicitud debe contener al menos un día de vacaciones.",
              {}
            );
            // } else if (this.solicitud.nuevoSaldo <= -3) {
            //   Vue.$toast.error(
            //     "El nuevo saldo no puede ser menor a -3 días.",
            //     {}
            //   );
          } else {
            // if (this.solicitud.nuevoSaldo <= -3) {
            //   Vue.$toast.warning(
            //     "El nuevo saldo no debe ser menor a -3 días.",
            //     {}
            //   );
            // }
            await this.Enviar(datos, opciones);
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    async consultarSaldo() {
      const {
        data: { error },
      } = await EnvioCorreo({
        IdColaborador: this.colaborador.idcolaborador,
        Tipo: 8,
      });

      // if (error) {
      //   Vue.$toast.success(data.error, {});
      // } else {
      //   Vue.$toast.success(
      //     "Recibirá la constancia de saldo de vacaciones vía correo.",
      //     {}
      //   );
      // }
      Vue.$toast.success(
        "Recibirá la constancia de saldo de vacaciones vía correo.",
        {}
      );
    },
  },
  watch: {
    /**
     * Controla el cambio de alguno de los campos de empresa colaborador, esto permitirá marcar
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
    mostrarBotonera() {
      return (
        (this.origen == "CertificacionesTramitadas" &&
          this.idSolicitud == null) ||
        (this.origen == "PendientesAtender" && this.idSolicitud != null)
      );
    },
    today() {
      return new Date();
    },
    year() {
      // return new Date().year;

      let d = new Date();
      let n = d.getFullYear();
      return n;
    },
    yesterday() {
      var date = new Date();
      date;
      date.setDate(date.getDate() - 1);
      return date;
    },
    nuevoSaldo() {
      var res = "";
      if (this.solicitud.saldoVacaciones != "No disponible") {
        res = (
          this.solicitud.saldoVacaciones - this.solicitud.cantDias
        ).toFixed(2);
      } else {
        res = "No disponible";
      }

      this.solicitud.nuevoSaldo = res;
      return res;
    },
    saldoVacacionesComputed() {
      var res = "";
      if (this.solicitud.saldoVacaciones != "No disponible") {
        res = this.solicitud.saldoVacaciones;
      } else {
        res = "No disponible";
      }

      this.solicitud.nuevoSaldo = res;
      return res;
    }
  },
};
</script>
<style>
@import "styles.css";
.note {
  color: #f9b115;
}

.speech-bubble {
  position: relative;
  background: #f9b115;
  border-radius: 0.4em;
}

.speech-bubble:after {
  content: "";
  position: absolute;
  left: 0;
  top: 50%;
  width: 0;
  height: 0;
  border: 0.063em solid transparent;
  border-right-color: #bd9d00;
  border-left: 0;
  border-top: 0;
  margin-top: -0.031em;
  margin-left: -0.062em;
}
</style>
