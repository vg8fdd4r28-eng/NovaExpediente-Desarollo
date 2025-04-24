<!-- Plantilla de Registro de vacunación -->
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
        <strong> Registro de vacunación</strong>
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

    <FormulateForm
      v-model="solicitud"
      @submit="submit()"
      @failed-validation="errorValidacion"
      :invalid-message="this.invalidMessage"
    >
      <FormulateErrors />

      <CCard style="border-radius: 16px !important;">
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong> Esquema de vacunación</strong>
        </CCardHeader>
        <CCardBody>
          <!-- Certificado de vacunación -->
          <CRow>
            <CCol sm="6">
              <label>Certificado de vacunación</label>
              <FormulateInput
                type="myfileupload"
                name="certificadoVacunacion"
                :help="
                  puedePintar === true
                    ? 'Formatos válidos: png/jpg/gif/pdf, Código QR u otros.'
                    : ''
                "
                input-class="form-control"
                validation-name="mime:image/jpeg,image/png,image/gif,application/pdf"
                class="hint"
                tipo-documento="Certificado de vacunación"
                :estado-solicitud="datosSolicitud.estadoSolicitud"
                :eliminada="false"
                :visor-padre="false"
                :habilitar-controles="
                  this.habilitarControlesSolicitud(
                    this.datosSolicitud.estadoSolicitud,
                    this.origen
                  )
                "
                @showModal="showModalVisor"
              />
            </CCol>
          </CRow>
        </CCardBody>
      </CCard>

      <CCard style="border-radius: 16px !important;">
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong> Listado de Vacunas</strong>
        </CCardHeader>
        <CCardBody>
          <agrupacion-vacunas
            :vacunasColaborador="solicitud.vacunasColaborador"
            :vacunasDLL="this.vacunas || []"
            :estado-solicitud="datosSolicitud.estadoSolicitud"
            :habilitar-controles="
              this.habilitarControlesSolicitud(
                this.datosSolicitud.estadoSolicitud,
                this.origen
              )
            "
            nombreFile="Documento vacunación"
            @undo="undoVacuna"
            @modify="modificarVacuna"
            @add="agregarVacuna"
            @remove="eliminarVacuna"
          >
          </agrupacion-vacunas>
        </CCardBody>
      </CCard>
      <FormulateErrors />

      <!-- <h2
            class="text-divider"
            style="margin-top:35px;"
            v-if="this.comentarios.length > 0"
          >
            <span>Comentarios</span>
          </h2>
          <comments
            :comments_wrapper_classes="['custom-scrollbar', 'comments-wrapper']"
            :comments="this.comentarios"
          ></comments> -->

      <botonera-master-data
        :estado-solicitud="this.datosSolicitud.estadoSolicitud"
        nombreEventoProcesar="procesarAccion"
        @procesarAccion="procesarAccion"
        tipo="AD"
        v-if="muestraBotonera()"
      >
      </botonera-master-data>
    </FormulateForm>

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
    <!-- <comment-dialogue ref="commentDialogue"></comment-dialogue> -->
  </div>
</template>

<script>
import Vue from "vue";
// Métodos de llamados a API
import {
  GetColaboradoresPerfil,
  GetColaboradorDetailResumen,
  GetDetalleSolicitud,
  GetVacunaList,
  GetVacunacionColaboradorDetail
} from "../masterdata/request";

// Componentes
import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "../masterdata/ResumenColaborador.vue";
import { aplicarFormatoFecha } from "../../utils";
import RepeatableVacunas from "../generales/Componentes/RepeatableVacunas.vue";
import BotoneraMasterData from "../masterdata/BotoneraMasterData.vue";

// métodos comunes
import common from "../masterdata/common.js";
import _groupBy from "lodash/groupBy";
import _find from "lodash/find";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
//import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";
//import Comments from "../generales/Componentes/Comments/Comments.vue";

// Definición del componente DatosPersonales
export default {
  name: "RegistroVacunacion",
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
    "agrupacion-vacunas": RepeatableVacunas,
    "botonera-master-data": BotoneraMasterData,
    "confirm-dialogue": ConfirmDialogue
    // "comment-dialogue": CommentDialogue//,
    //comments: Comments
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
      // values de solicitud
      solicitud: {
        certificadoVacunacion: null,
        vacunasColaborador: [],
        estado: "N"
      },
      //   solicitud: null,
      datosSolicitud: {
        idSolicitudMasterData: null,
        idTipoSolicitud: this.idTipoSolicitud,
        tipoSolicitud: this.tipoSolicitud,
        estadoSolicitud: "RE"
      },
      comentarios: [],
      showModal: false, // controla la presentación del modal
      nombreTipoLista: "", // referencia el tipo de contenido de un pick
      tituloModal: "", // título para la ventana modal dependiendo del tipo de contenido del pick
      columnasPickList: [], // lista de columnas a mostrar en el componente tabla del pick
      dataPickList: [], // datos para el componente tabla del pick
      TipoListaPick: "",
      cargandoDatos: false, // indica si se están cargando datos desde la BD para que el watch no modifique otros indicadores
      codigoPaisCostaRica: 51, // identificador de Costa Rica en la tabla de Paises
      IdSolicitudJson: null, // id de la solicitud para cargar el json de una solicitud
      mensajeError: common.MENSAJE_ERROR,
      visorEnable: false,
      vacunas: []
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...
    await this.cargarVacunas();

    this.IdSolicitudJson = this.idSolicitud;

    this.TipoListaPick = "Colaboradores";
    if (this.IdSolicitudJson) {
      let loader = this.showLoader();
      await this.cargarSolicitud();
      this.hideLoader(loader);
    } else {
      await this.selected(this.$root.infoColaboradorActual);
      //await this.GetVacunacionColaborador(this.$root.infoColaboradorActual.id);
    }

    this.disableAutoComplete();
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
    showModalVisor(show) {
      this.visorEnable = show;
    },
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        if (fields[key].type !== "group") {
          fieldNames += `-  ${fields[key].label}\r\n`;
        } else {
          switch (fields[key].name) {
            case "vacunasColaborador":
              fieldNames += "-  Sección de vacunas\r\n";
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
    /**
     * Obtiene los datos del formulario para el submit
     */
    async submit(formValues) {
      try {
        if (this.solicitud.estado == common.SOLICITUD_SINMODIFICACIONES) {
          Vue.$toast.warning(
            "La solicitud no tiene modificaciones que guardar.",
            {}
          );
        } else {
          let resultadoValidacion = this.validacionesSolicitud();
          if (resultadoValidacion.esValido) {
            let datos = {
              idSolicitud: this.datosSolicitud.idSolicitudMasterData,
              idTipoSolicitud: this.datosSolicitud.idTipoSolicitud,
              codigoEstado: this.datosSolicitud.estadoSolicitud,
              idAutor: this.$root.infoColaboradorActual.id,
              idResponsable: this.$root.infoColaboradorActual.id,
              idColaborador: this.colaborador.idcolaborador,
              accion: "Enviar",
              solicitudSinAprobacion: true,
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
          } else {
            Vue.$toast.warning(
              "La solicitud presenta las siguientes inconsistencias: <br/>" +
                resultadoValidacion.mensaje,
              {}
            );
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    validacionesSolicitud() {
      let resultado = { esValido: true };

      if (this.solicitud.vacunasColaborador.length == 0)
        resultado = {
          esValido: false,
          mensaje: "Debe asociar al menos una vacuna"
        };

      if (
        !this.solicitud.certificadoVacunacion ||
        this.solicitud.certificadoVacunacion.trim() === ""
      )
        resultado = {
          esValido: false,
          mensaje: "Debe asociar el certificado de la vacunación"
        };

      if (!this.validaVacunas(this.solicitud.vacunasColaborador))
        resultado = {
          esValido: false,
          mensaje: "Debe ingresar al menos 1 vacuna"
        };

      if (this.tieneFechasDuplicadas(this.solicitud.vacunasColaborador))
        resultado = {
          esValido: false,
          mensaje: "Existen vacunas con fechas duplicadas"
        };

      return resultado;
    },

    validaVacunas(list) {
      let filterList = list.filter(function(item) {
        return item.estado != "E";
      });

      return filterList.length > 0;
    },

    tieneFechasDuplicadas(list) {
      // let duplicateIds = list
      //   .map(e => e["fecha"])
      //   .map((e, i, final) => final.indexOf(e) !== i && i)
      //   .filter(obj => list[obj])
      //   .map(e => list[e]["fecha"]);

      // return duplicateIds.length == 0;

      const byFecha = _groupBy(list, "fecha");
      const duplicado = _find(byFecha, vs => {
        return vs.length >= 2;
      });
      return !!duplicado;
    },

    /**
     * Invoca al API que devuelve la lista de vacunas
     */
    async cargarVacunas() {
      const {
        data: { vacunas }
      } = await GetVacunaList();
      this.vacunas = vacunas.map(({ idVacuna, nombre }) => ({
        label: nombre,
        value: idVacuna
      }));

      // this.vacunas = [{ idvacuna:1, nombre: "nombre", fecha: "2022-01-01" }];
      // this.vacunas = [{ value:1, label: "nombre"}];
    },

    async GetVacunacionColaborador(id) {
      const {
        data: { vacunacionColaborador }
      } = await GetVacunacionColaboradorDetail(id);

      this.solicitud.vacunasColaborador = await this.setVacunacion(
        (vacunacionColaborador && vacunacionColaborador.vacunas) || null
      );

      // this.solicitud.vacunasColaborador =
      //   (vacunacionColaborador && vacunacionColaborador.vacunas) || [];

      if (vacunacionColaborador) {
        this.solicitud.certificadoVacunacion =
          vacunacionColaborador.certificado;
        this.solicitud.idVacunacionColaborador =
          vacunacionColaborador.idVacunacionColaborador;
      }

      if (vacunacionColaborador && vacunacionColaborador.vacunas) {
        if (vacunacionColaborador.vacunas.length > 0) {
          this.solicitud.estado = "M";
        }
      }
    },

    /**
     * Método invocado cuando un usuario selecciona un registro en un Pick
     */
    async selected(item) {
      let loader = this.showLoader();
      switch (this.TipoListaPick) {
        case "Colaboradores":
          this.showModal = false;
          await this.cargarDetalleColaborador(item.id);
          //    await this.GetVacunacionColaborador(this.$root.infoColaboradorActual.id);
          await this.GetVacunacionColaborador(item.id);
          break;

        default:
          break;
      }
      this.hideLoader(loader);
    },

    /**
     * Invoca al API que devuelve la información de un colaborador según su ID
     * @id identificador de un colaborador
     */
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

      //  this.solicitud.estado = common.SOLICITUD_SINMODIFICACIONES; // datos personales sin modificaciones

      this.terminaCargaDatos();
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
            certificadoVacunacion: colaborador.certificadoVacunacion,
            estado: colaborador.estado,
            vacunasColaborador: colaborador.vacunasColaborador
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

      await GetDetalleSolicitud(this.IdSolicitudJson)
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
          } = await GetColaboradoresPerfil(this.$root.infoColaboradorActual.id);
          this.dataPickList = colaboradores.map(
            ({ idcolaborador, nombreCompleto, identificacion }) => ({
              id: idcolaborador,
              nombre: nombreCompleto,
              identificacion: identificacion
            })
          );

          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombre", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "colaborador";
          this.tituloModal = "Lista de colaboradores";
          break;

        case "Paises":
          const {
            data: { paises }
          } = await GetAllPaises();
          this.dataPickList = paises.map(({ idpais, nombre }) => ({
            id: idpais,
            nombre: nombre
          }));

          this.columnasPickList = [
            // { field: "id", header: "Id" },
            { field: "nombre", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "país";
          this.tituloModal = "Lista de países";
          break;

        case "Cantones":
          const {
            data: { cantones }
          } = await GetAllCantones();
          this.dataPickList = cantones.map(
            ({ idcanton, nombre, provincianombre }) => ({
              id: idcanton,
              nombre: nombre,
              provincianombre: provincianombre
            })
          );

          this.columnasPickList = [
            //{ field: "id", header: "Id" },
            { field: "provincianombre", header: "Provincia" },
            { field: "nombre", header: "Cantón" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "cantón";
          this.tituloModal = "Lista de cantones";
          break;

        case "PaisesOrigen":
          const paisesOrigen = await GetAllPaises();

          this.dataPickList = paisesOrigen.data.paises.map(
            ({ idpais, nombre }) => ({
              id: idpais,
              nombre: nombre
            })
          );

          this.columnasPickList = [
            // { field: "id", header: "Id" },
            { field: "nombre", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "país";
          this.tituloModal = "Lista de países";
          break;
        default:
          break;
      }
    },

    /**
     * Modifica la estructura de las vacunas de un colaborador, formateando la fecha
     * y agregando el atributo del "estado" a cada una de las vacunas
     */
    async setVacunacion(vacunas) {
      // si el colaborador tiene vacunas se formatea la fecha y se agrega el atributo del estado a
      // cada una de las afiliaciones
      if (vacunas && vacunas != null && vacunas.length > 0) {
        vacunas.forEach(vacuna => {
          vacuna.fecha = aplicarFormatoFecha(vacuna.fecha, "yyyy-MM-dd");

          // se agrega el campo ESTADO con un valor "S" (S: Sin modificaciones N: Nuevo E: Eliminado M: Modificado)
          if (!vacuna.estado)
            this.$set(vacuna, "estado", common.SOLICITUD_SINMODIFICACIONES);
        });

        return vacunas;
      }
      // // sino se regresa una lista con una afiliacion nueva y vacía
      else {
        // return vacunas;
        return [this.getVacunaNueva()];
      }
    },

    /**
     * Contenido de una nueva afiliación
     */
    getVacunaNueva() {
      return {
        idVacuna: null,
        fecha: null,
        estado: "N"
      };
    },
    onUpload() {
      //this.$toast.add({severity: 'info', summary: 'Success', detail: 'File Uploaded', life: 3000});
    },
    clean() {
      this.showModal = false;
      this.nombreTipoLista = "";
      this.dataPickList = null;
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
    // procesarAccion_Eliminar(accion) {
    //   this.confirmacionEliminarSolicitud=true;
    // },
    undoVacuna(index) {
      this.solicitud.vacunasColaborador[index].estado = "S";
    },
    agregarVacuna() {
      this.solicitud.vacunasColaborador[
        this.solicitud.vacunasColaborador.length - 1
      ].estado = "N";
    },
    modificarVacuna(index) {
      if (this.solicitud.vacunasColaborador[index].estado === "S") {
        this.solicitud.vacunasColaborador[index].estado = "M";
      }
    },
    eliminarVacuna(index) {
      this.solicitud.vacunasColaborador[index].estado = "E";
    }
  },
  computed: {
    pickPuedeBuscarColaborador() {
      return this.datosSolicitud.estadoSolicitud == common.REGISTRADA; // && !(this.usuarioActual.Roles.length == 1 && this.usuarioActual.Roles[0] == "COLABORADOR") ;
    },
    pickPuedeBuscar() {
      return (
        this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
        this.datosSolicitud.estadoSolicitud == common.CORREGIR
      );
    },
    puedePintar() {
      return this.habilitarControlesSolicitud(
        this.datosSolicitud.estadoSolicitud,
        this.origen
      );
      // return (
    },

    helpTextArea() {
      if (this.solicitud) {
        return `Tiene disponible un máximo de ${8000 -
          this.solicitud.direccion.length} carácteres.`;
      }
      return null;
    },
    mostrarBotonera() {
      return (
        (this.origen == "CertificacionesTramitadas" &&
          this.idSolicitud == null) ||
        (this.origen == "PendientesAtender" && this.idSolicitud != null)
      );
    }
  },
  watch: {
    /**
     * Controla el cambio de alguno de los campos de datos personales, esto permitirá marcar
     * el documento con un estado de modificado
     */
    solicitud: {
      handler(newValue, oldValue) {
        if (!this.cargandoDatos) {
          // concurrencia -- async - await is the best way to use it

          if (oldValue !== newValue) {
            // console.log(newValue);
            // console.log(oldValue);
            //this.solicitud.estado = "M";
          }

          //
        }
      },
      deep: true
    }
  }
};
</script>

<style>
.form-control:disabled,
.form-control[readonly] {
  background-color: #e7e7e7 !important;
  color: #767676 !important;
}

.text-divider {
  margin: 2em 0;
  line-height: 0;
  font-size: 0.875rem;
}
.text-divider span {
  background-color: #9c9c9c !important;
  color: #ffffff;
  padding: 0.5em 0.5em 0.5em 0.5em;
}
.text-divider:before {
  content: " ";
  display: block;
  border-top: 1px dotted rgb(187, 187, 187);
  border-bottom: 1px solid #f7f7f7;
}

.avatar-upload {
  position: relative;
  max-width: 205px;
  /* margin: 50px auto; */
}
.avatar-upload .avatar-edit {
  position: absolute;
  right: 12px;
  z-index: 1;
  top: 10px;
}
.avatar-upload .avatar-edit input {
  display: none;
}
.avatar-upload .avatar-edit input + label {
  display: inline-block;
  width: 34px;
  height: 34px;
  margin-bottom: 0;
  border-radius: 100%;
  background: #ffffff;
  border: 1px solid transparent;
  box-shadow: 0px 2px 4px 0px rgba(0, 0, 0, 0.12);
  cursor: pointer;
  font-weight: normal;
  transition: all 0.2s ease-in-out;
}
.avatar-upload .avatar-edit input + label:hover {
  background: #f1f1f1;
  border-color: #d6d6d6;
}
.avatar-upload .avatar-edit input + label:after {
  content: "\e908";
  font-family: "primeicons";
  color: #757575;
  position: absolute;
  top: 10px;
  left: 0;
  right: 0;
  text-align: center;
  margin: auto;
}
.avatar-upload .avatar-preview {
  width: 192px;
  height: 192px;
  position: relative;
  border-radius: 100%;
  border: 6px solid #f8f8f8;
  box-shadow: 0px 2px 4px 0px rgba(0, 0, 0, 0.1);
}
.avatar-upload .avatar-preview > div {
  width: 100%;
  height: 100%;
  border-radius: 100%;
  background-size: cover;
  background-repeat: no-repeat;
  background-position: center;
}
.p-button {
  background-color: #29abe2;
}
.label-title {
  font-weight: 700;
}
.label-name {
  font-variant: small-caps;
  text-align: center;
}
.libro-marca-azuloscuro {
  background-color: #002e6e !important;
}
.libro-marca-celeste {
  background-color: #29abe2;
}
.libro-marca-celeste .disabled {
  background-color: #29abe2 !important;
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

.modal-header {
  background-color: #002e6e !important;
}
.modal-primary .modal-content {
  border-color: #002e6e !important;
  width: 600px;
}

/* Configuración de estilos para tabla PRIMEVUE */
/* INICIO */

/* Se modifica el tamaño de la letra a 14px */
.p-component {
  font-size: 0.875rem !important;
}
.p-dropdown-label.p-inputtext {
  font-size: 0.875rem !important;
}

/* Se oculta la flecha del dropdown */
.dropdown-toggle::after {
  display: none;
}
.dropleft .dropdown-toggle::before {
  display: none;
}

/* tamaño de letra el componente de paginación */
.p-paginator-icon {
  font-size: 0.875rem !important;
}
.hint {
  font-size: 0.705rem !important;
  margin-top: 1%;
}

/* Responsive: en visualización normal se ocultan las etiquetas asociadas al título de cada columna */
.p-datatable-picklist .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-picklist .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-picklist .p-datatable-tbody > tr > td {
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

.p-dialog-mask {
  z-index: 1049 !important;
}
.btn-opciones {
  display: none;
}

.formulate-form-errors {
  background-color: red;
  color: white;
  white-space: pre-wrap;
}

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de
   etiqueta y valor asociado */
@media screen and (max-width: 40em) {
  .p-datatable.p-datatable-picklist .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-picklist .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-picklist .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-picklist .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-picklist:last-child {
    border-bottom: 1px solid var(--surface-d);
  }

  .dropdown-toggle::after {
    display: inline-block;
  }
  .dropleft .dropdown-toggle::before {
    display: inline-block;
  }
  .dropleft {
    display: none;
  }
  .pi-ellipsis-v {
    display: none;
  }
  .btn-opciones {
    display: flex;
  }
  .p-paginator-bottom {
    padding: 0px;
  }

  .ajustarTamano {
    width: -webkit-fill-available;
  }

  .alinearDerecha {
    text-align: right !important;
  }
  .alinearIzquierda {
    text-align: left !important;
  }
  .alinearCentro {
    text-align: center !important;
  }
}
/* FIN */
</style>
