<template>
  <div>
    <CRow>
      <CCol md="10"> </CCol>
      <CCol sm="12" md="2">
        <CButton
          block
          color="primary"
          @click="goBack()"
          class="btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark btn-primary"
        >
          Volver</CButton
        >
      </CCol>
    </CRow>

    <CCard
      style="
        border-radius: 16px !important;
        margin-top: 20px;
        margin-bottom: 20px;
      "
    >
      <CCardHeader
        style="
          border-radius: 16px !important;
          border-bottom: transparent !important;
        "
      >
        <CCol sm="12" md="12">
          <strong>{{ header }}</strong>
        </CCol>
      </CCardHeader>
      <CCardBody
        style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 20px"
      >
        <div>
          <CRow><h5>Información del colaborador</h5></CRow>
          <CRow>
            <CCol sm="12" class="mt-2">
              <resumen-colaborador
                :colaborador.sync="colaborador"
                estado-solicitud="RE"
                :searchable="searchableColaborador"
                :editableIdentificacion="false"
                :editable="false"
                :habilitar-controles="true"
                :visible="true"
                @displayModal="displayModal"
              >
              </resumen-colaborador>
            </CCol>
          </CRow>
        </div>

        <FormulateForm
          name="formulario"
          v-model="unoAuno"
          class="mt-3"
          @submit="submit()"
          @failed-validation="validationError"
          :invalid-message="this.invalidMessage"
        >
          <div>
            <CRow>
              <CCol sm="12" md="6">
                <FormulateInput
                  type="text"
                  name="puesto"
                  v-model="this.colaborador.puesto"
                  label="Puesto"
                  validation="max:200,length"
                  validation-name="Puesto del colaborador"
                  :validation-messages="{
                    required: 'Ingrese un puesto.',
                  }"
                  :input-class="['form-control']"
                  disabled="true"
                />
              </CCol>

              <CCol sm="12" md="6">
                <FormulateInput
                  type="text"
                  name="cliente"
                  label="Cliente"
                  v-model="this.colaborador.cliente"
                  validation="max:200,length"
                  validation-name="Cliente"
                  :validations-messages="{
                    required: 'Ingrese un cliente',
                  }"
                  :input-class="['form-control']"
                  disabled="true"
                />
              </CCol>
            </CRow>

            <CRow>
              <CCol sm="12" md="12" class="wrapper">
                <label>Observación</label>
                <FormulateInput
                  type="myRichTextBox"
                  name="observacion"
                  :disabled="isDisabled"
                />
              </CCol>
            </CRow>

            <CRow>
              <CCol sm="12" md="6">
                <FormulateInput
                  name="idUnoAUnoEstadoColaborador"
                  :options="estadosColaborador"
                  type="select"
                  label="Seleccione el estado general del colaborador"
                  input-class="form-control"
                  validation="required|not:0"
                  valadation-name="Estado del colaborador"
                  :validation-messages="{
                    required: 'Seleccione un estado.',
                    not: 'Debe seleccionar una opción válida.',
                  }"
                  :disabled="isDisabled"
                />
              </CCol>
            </CRow>

            <CRow>
              <CCol sm="10" />
              <CCol sm="2" style="padding: 6px">
                <CButton
                  block
                  color="primary"
                  type="submit"
                  class="btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark btn-primary"
                  :disabled="action === 'VIEW'"
                >
                  {{ this.saveButtonName }}</CButton
                >
              </CCol>
            </CRow>

            <hr />

            <div v-show="showDetail">
              <CRow> </CRow>
              <h5>Contenido de la conversación</h5>
              <CRow>
                <CCol sm="12" md="12" class="wrapper">
                  <DataTable
                    ref="dt"
                    :value="listadoDetalle"
                    :paginator="true"
                    class="p-datatable-consultas p-datatable-striped p-datatable-sm"
                    :rows="10"
                    dataKey="IDUNOAUNODETALLE"
                    :rowHover="true"
                    :selection.sync="selectedDetalle"
                    :filters.sync="filters"
                    :loading="loading"
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    :rowsPerPageOptions="[5, 10, 25, 50]"
                    currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} contenidos"
                    sortField="IDUNOAUNODETALLE"
                    :sortOrder="1"
                    stateKey="dt-state-unoauno"
                    selectionMode="single"
                    :autoLayout="true"
                    @update:filters="filterApplied"
                    @filter="filterApplied"
                  >
                    <template #header>
                      <CRow>
                        <CCol md="6" sm="6" class="mb-2">
                          <div class="table-header w-100">
                            <span class="p-input-icon-left w-75">
                              <i class="pi pi-search" />
                              <InputText
                                v-model="filters['global']"
                                placeholder="Escribe aquí lo que buscas"
                                class="w-100"
                              />
                            </span>
                          </div>
                        </CCol>
                        <CCol md="6" sm="6" class="mb-2">
                          <CButton
                            color="primary"
                            class="pull-right btn btn-info libro-marca-celete onHoverDark ml-1"
                            @click="createDetalle"
                          >
                            Agregar Contenido
                          </CButton>
                        </CCol>
                      </CRow>
                    </template>
                    <template #empty> No se encontraron contenidos. </template>
                    <template #loading>
                      Cargando la información de los contenidos. Por favor
                      espere.
                    </template>
                    <Column
                      field="CATEGORIA"
                      header="Categoría"
                      :sortable="true"
                      sortField="CATEGORIA"
                      filterField="CATEGORIA"
                      filterMatchMode="contains"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Categoría</span>
                        {{ slotProps.data.CATEGORIA }}
                      </template>
                    </Column>

                    <Column
                      field="DETALLE"
                      header="Detalle"
                      sortField="DETALLE"
                      filterField="DETALLE"
                      filterMatchMode="contains"
                      :sortable="true"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Detalle</span>
                        {{ slotProps.data.DETALLE.substring(0, 20) }}
                      </template>
                    </Column>

                    <Column
                      field="PLANACCION"
                      sortField="PLANACCION"
                      header="Plan acción"
                      filterField="PLANACCION"
                      filterMatchMode="contains"
                      :sortable="true"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Plan acción</span>
                        {{ slotProps.data.PLANACCION.substring(0, 30) }}
                      </template>
                    </Column>

                    <Column
                      field="Estado"
                      sortField="ESTADO"
                      header="Estado General"
                      filterField="ESTADO"
                      filterMatchMode="contains"
                      :sortable="true"
                      headerStyle="width: 20%"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Estado</span>
                        {{ slotProps.data.ESTADO }}
                      </template>
                    </Column>

                    <Column
                      field="PORCENTAJEAVANCE"
                      sortField="PORCENTAJEAVANCE"
                      header="Avance"
                      filterField="PORCENTAJEAVANCE"
                      filterMatchMode="contains"
                      :sortable="true"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Avance</span>
                        {{ slotProps.data.PORCENTAJEAVANCE + "%" }}
                      </template>
                    </Column>

                    <Column
                      field="FECHARESOLUCION"
                      sortField="FECHARESOLUCION"
                      header="Fecha resolución"
                      filterField="FECHARESOLUCION"
                      filterMatchMode="contains"
                      :sortable="true"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Fecha resolución</span>
                        {{ slotProps.data.FECHARESOLUCION }}
                      </template>
                    </Column>

                    <Column
                      field="FECHAREALRESOLUCION"
                      sortField="FECHAREALRESOLUCION"
                      header="Fecha real resolución"
                      filterField="FECHAREALRESOLUCION"
                      filterMatchMode="contains"
                      :sortable="true"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title"
                          >Fecha real resolución</span
                        >
                        {{ slotProps.data.FECHAREALRESOLUCION }}
                      </template>
                    </Column>

                    <Column
                      header="Acciones"
                      :sortable="false"
                      headerStyle="width: 10%"
                    >
                      <template #body="slotProps">
                        <CDropdown
                          color="link"
                          size="sm"
                          :caret="false"
                          style="margin-top: -7px"
                          placement="left"
                        >
                          <template #toggler-content>
                            <i class="pi pi-ellipsis-v"></i>
                          </template>
                          <CDropdownItem @click="editDetalle(slotProps.data)">
                            <CIcon
                              style="color: darkblue"
                              name="cil-pencil"
                            />&nbsp;&nbsp; Editar
                          </CDropdownItem>
                        </CDropdown>
                      </template>
                    </Column>
                  </DataTable>
                </CCol>
              </CRow>
              <CRow>
                <CCol md="12" sm="12" class="mt-2">
                  <CButton
                    color="primary"
                    class="pull-right btn btn-info libro-marca-celete onHoverDark ml-1"
                    @click="goBack()"
                  >
                    Finalizar
                  </CButton>
                </CCol>
              </CRow>
            </div>
          </div>
        </FormulateForm>
      </CCardBody>
    </CCard>

    <!-- modal para seleccionar el colaborador -->
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
    <!-- modal para agregar el contenido de la conversacion -->

    <Dialog
      :visible.sync="showModalDetalle"
      :style="{ width: '70vw' }"
      :maximizable="true"
      :modal="true"
    >
      <template #header>
        <h5>{{ tituloModalDetalle }}</h5>
      </template>

      <!-- inputs y boton se guardar -->

      <FormulateForm
        name="formularioDetalle"
        v-model="unoAunoDetalle"
        class="mt-3"
        @submit="submitDetalle()"
        @failed-validation="validationError"
        :invalid-message="this.invalidMessage"
      >
        <div>
          <CRow>
            <CCol sm="12" md="6">
              <FormulateInput
                name="idUnoAUnoCategoria"
                :options="categoriasDetalle"
                type="select"
                label="Seleccione la categoría"
                input-class="form-control"
                validation="required|not:0"
                valadation-name="Categoría"
                :validation-messages="{
                  required: 'Seleccione una categoría.',
                  not: 'Debe seleccionar una opción válida.',
                }"
              />
            </CCol>
          </CRow>

          <CRow>
            <CCol sm="12" md="6" class="wrapper">
              <label>Detalle</label>
              <FormulateInput type="myRichTextBox" name="detalle" />
            </CCol>
            <CCol sm="12" md="6" class="wrapper">
              <label>Plan de acción</label>
              <FormulateInput type="myRichTextBox" name="planAccion" />
            </CCol>
          </CRow>

          <CRow>
            <CCol sm="12" md="6">
              <FormulateInput
                name="idUnoAUnoEstado"
                :options="estadosDetalle"
                type="select"
                label="Seleccione el estado"
                input-class="form-control"
                validation="required|not:0"
                valadation-name="Estado"
                :validation-messages="{
                  required: 'Seleccione un estado.',
                  not: 'Debe seleccionar una opción válida.',
                }"
              />
            </CCol>
            <CCol sm="12" md="6">
              <FormulateInput
                type="number"
                name="porcentajeAvance"
                label="Porcentaje de avance"
                validation="max:100|min:0"
                value="0"
                validation-name="Porcentaje de avance"
                :validation-messages="{
                  required: 'Ingrese un porcentaje.',
                }"
                :input-class="['form-control']"
              />
            </CCol>
          </CRow>

          <CRow>
            <CCol sm="12" md="6">
              <FormulateInput
                type="date"
                name="fechaResolucion"
                label="Fecha esperada de resolución"
                validation-name="Fecha de resolución"
                :validation-messages="{
                  required: 'Ingrese la fecha de expiración.',
                  after:
                    'La fecha de resolución no puede ser menor a la actual.',
                }"
                :input-class="['form-control']"
              />
            </CCol>
            <CCol sm="12" md="6">
              <FormulateInput
                type="date"
                name="fechaRealResolucion"
                label="Fecha real de resolución"
                validation-name="Fecha real de resolución"
                :validation-messages="{
                  required: 'Ingrese la fecha de resolución.',
                  after:
                    'La fecha de resolución no puede ser menor a la actual.',
                }"
                :input-class="['form-control']"
              />
            </CCol>
          </CRow>

          <CRow>
            <CCol sm="12" md="6">
              <FormulateInput
                type="text"
                name="diasFaltantes"
                label="Días faltantes"
                v-model="this.unoAunoDetalle.diasFaltantes"
                disabled="true"
                :input-class="['form-control']"
              />
            </CCol>
          </CRow>

          <CRow>
            <CCol sm="10" />
            <CCol sm="2" style="padding: 6px">
              <CButton
                block
                color="primary"
                type="submit"
                class="btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark btn-primary"
              >
                {{ this.saveButtonNameDetalle }}</CButton
              >
            </CCol>
          </CRow>
        </div>
      </FormulateForm>

      <template #footer> </template>
    </Dialog>

    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
    <comment-dialogue ref="commentDialogue"></comment-dialogue>
  </div>
</template>

<script>
import Vue from "vue";
import common from "../../masterdata/common.js";
import ResumenColaborador from "../../masterdata/ResumenColaborador";
import ConfirmDialogue from "../../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../../generales/Componentes/PopUpModal/CommentDialogue.vue";
import PickList from "../../generales/Componentes/PickList";
import moment from "moment";

import {
  GetDetalleUnoAUno,
  UpsertUnosAUnos,
  GetColaboradoresPerfil,
  GetColaboradorDetail,
  GetUnosAUnosEstadosColaboradores,
  GetUnosAUnosEstados,
  GetUnosAUnosCategorias,
  UpsertUnosAUnosDetalles,
} from "../request";

export default {
  name: "formUnoAUno",
  mixins: [common],
  components: {
    "pick-list": PickList,
    "resumen-colaborador": ResumenColaborador,
    "confirm-dialogue": ConfirmDialogue,
    "comment-dialogue": CommentDialogue,
  },
  emits: ["view-list"],
  props: ["idUnoAUno", "formAction"],
  data() {
    return {
      id: this.idUnoAUno,
      action: this.formAction,
      colaborador: {
        // values de colaborador
        idcolaborador: 0,
        identificacion: null,
        nombre: null,
        primerapellido: null,
        segundoapellido: null,
        foto: null,
        correo: "",
        puesto: "",
        cliente: "",
      },
      unoAuno: {
        idColaborador: null, // colaborador al que se le realiza el 1 a 1
        idUnoAUno: null,
        idUnoAUnoEstadoColaborador: null,
        observacion: "",
        activo: true,
        idUsuario: null, // DM
      },
      unoAunoDetalle: {
        idUnoAUnoDetalle: null,
        idUnoAUno: null,
        idUnoAUnoEstado: null,
        idUnoAUnoCategoria: null,
        detalle: "",
        planAccion: "",
        porcentajeAvance: 0,
        fechaResolucion: null,
        fechaRealResolucion: null,
        idUsuario: null,
        diasFaltantes: 0,
      },
      currentUnoAUNo: [],
      loading: true,
      estadosColaborador: [],
      responsiveOptions: [
        {
          breakpoint: "1024px",
          numVisible: 5,
        },
        {
          breakpoint: "768px",
          numVisible: 3,
        },
        {
          breakpoint: "560px",
          numVisible: 1,
        },
      ],
      listadoDetalle: [],
      esEdit: false,
      header: "",
      saveButtonName: "",
      saveButtonNameDetalle: "",
      //modal
      showModal: false, // controla la presentación del modal
      nombreTipoLista: "", // referencia el tipo de contenido de un pick
      tituloModal: "", // título para la ventana modal dependiendo del tipo de contenido del pick
      columnasPickList: [], // lista de columnas a mostrar en el componente tabla del pick
      dataPickList: [], // datos para el componente tabla del pick
      TipoListaPick: "",
      filters: {},
      showModalDetalle: false,
      tituloModalDetalle: "",
      selectedDetalle: null,
      categoriasDetalle: [],
      searchableColaborador: true,
    };
  },
  async mounted() {
    this.getEstadosColaboradores();
    this.getCategoriasUnoAUno();
    this.getEstadosUnoAUno();
    this.viewManagement();
    this.loading = false;
  },
  methods: {
    // eslint-disable-next-line no-unused-vars
    filterApplied(_e) {},

    /**
     * Método invocado cuando un usuario selecciona un registro en un Pick
     */
    async selected(item) {
      let loader = this.showLoader();
      switch (this.TipoListaPick) {
        case "Colaboradores":
          this.showModal = false;
          await this.cargarDetalleColaborador(item.id);
          break;

        default:
          break;
      }
      this.hideLoader(loader);
    },

    async cargarDetalleColaborador(id) {
      const {
        data: { colaborador },
      } = await GetColaboradorDetail(id);

      this.colaborador = {
        idcolaborador: colaborador.idcolaborador,
        identificacion: colaborador.identificacion,
        nombre: colaborador.nombre,
        primerapellido: colaborador.primerapellido,
        segundoapellido: colaborador.segundoapellido,
        foto: colaborador.foto,
        correo: colaborador.correo,
        puesto: colaborador.puesto,
        cliente: colaborador.cliente,
        // fechaingreso: aplicarFormatoFecha(
        //   colaborador.fechaingreso,
        //   "yyyy-MM-dd"
        // ),
      };
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
          // eslint-disable-next-line no-case-declarations
          const {
            data: { colaboradores },
          } = await GetColaboradoresPerfil(this.$root.infoColaboradorActual.id);
          this.dataPickList = colaboradores.map(
            ({ idcolaborador, nombreCompleto, identificacion }) => ({
              id: idcolaborador,
              nombre: nombreCompleto,
              identificacion: identificacion,
            })
          );

          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombre", header: "Nombre" },
          ];

          this.showModal = true;
          this.nombreTipoLista = "colaborador";
          this.tituloModal = "Lista de colaboradores";
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

    async getEstadosColaboradores() {
      const {
        data: { unosAUnosEstadosColaboradores },
      } = await GetUnosAUnosEstadosColaboradores();
      this.estadosColaborador = unosAUnosEstadosColaboradores.map(
        ({ nombre, idUnoAUnoEstadoColaborador }) => ({
          label: nombre,
          value: idUnoAUnoEstadoColaborador,
        })
      );
    },

    async getCategoriasUnoAUno() {
      const {
        data: { unosAUnosCategorias },
      } = await GetUnosAUnosCategorias();
      this.categoriasDetalle = unosAUnosCategorias.map(
        ({ nombre, idUnoAUnoCategoria }) => ({
          label: nombre,
          value: idUnoAUnoCategoria,
        })
      );
    },

    async getEstadosUnoAUno() {
      const {
        data: { unosAUnosEstados },
      } = await GetUnosAUnosEstados();
      this.estadosDetalle = unosAUnosEstados.map(
        ({ nombre, idUnoAUnoEstado }) => ({
          label: nombre,
          value: idUnoAUnoEstado,
        })
      );
    },

    async getDetail() {
      let loader = this.showLoader();
      const { data } = await GetDetalleUnoAUno(this.id);
      const document = data;
      this.listadoDetalle = data[0].Detalle;

      this.cargarDetalleColaborador(document[0].IDCOLABORADOR);

      this.currentUnoAUNo = document.map(
        ({
          IDCOLABORADOR,
          IDUNOAUNOESTADOCOLABORADOR,
          IDUNOAUNO,
          OBSERVACION,
          ACTIVO,
          IDUSUARIOCREA,
        }) => ({
          idColaborador: IDCOLABORADOR,
          idUnoAUno: IDUNOAUNO,
          idUnoAUnoEstadoColaborador: IDUNOAUNOESTADOCOLABORADOR,
          observacion: OBSERVACION,
          activo: ACTIVO,
          idUsuario: IDUSUARIOCREA,
        })
      );

      this.unoAuno = this.currentUnoAUNo[0];
      this.hideLoader(loader);
    },
    goBack() {
      this.$emit("view-list", "lista-unoauno");
    },
    reset() {
      this.unoAuno = {
        idColaborador: null,
        idUnoAUno: null,
        idUnoAUnoEstadoColaborador: null,
        observacion: "",
        idUsuario: null,
      };
      this.id = null;
      this.currentUnoAUNo = [];
    },
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        fieldNames += `-  ${fields[key].label}\r\n`;
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    validationError() {
      Vue.$toast.warning(common.MENSAJE_ERROR, {
        // optional options Object
      });
    },

    async submit() {
      this.unoAuno.idUsuario = this.$root.infoColaboradorActual.id;
      this.unoAuno.idColaborador = this.colaborador.idcolaborador;
      this.unoAuno.observacion = this.unoAuno.observacion.replace(/"/g, "'");

      if (this.colaborador.idcolaborador > 0) {
        if (this.unoAuno.observacion.trim().length > 0) {
          await UpsertUnosAUnos({
            data: JSON.stringify({ ...this.unoAuno }),
          })
            .then((response) => {
              if (!isNaN(response.data)) {
                //exito
                if (response.data > 0) {
                  Vue.$toast.success(
                    "El uno a uno fue ingresado exitosamente. Continue con el detalle",
                    {}
                  );
                  this.searchableColaborador = false;
                  this.unoAuno.idUnoAUno = response.data;
                  this.id = response.data;
                } else {
                  Vue.$toast.success(
                    "El uno a uno fue modificado exitosamente.",
                    {}
                  );
                }
              }
            })
            .catch((error) => {
              if (error.response.data.message.length > 0) {
                Vue.$toast.error(error.response.data.message[0], {});
              } else {
                Vue.$toast.error("No se pudo actualizar el uno a uno", {});
              }
            });
        } else {
          Vue.$toast.error("Ingrese la observación.", {});
        }
      } else {
        Vue.$toast.error("Debe de ingresar un colaborador.", {});
      }
    },

    createDetalle() {
      this.tituloModalDetalle = "Agregar contenido a la conversacion";
      this.saveButtonNameDetalle = "Crear contenido";
      this.showModalDetalle = true;

      this.unoAunoDetalle = {
        idUnoAUnoDetalle: null,
        idUnoAUno: null,
        idUnoAUnoEstado: null,
        idUnoAUnoCategoria: null,
        detalle: "",
        planAccion: "",
        porcentajeAvance: 0,
        fechaResolucion: null,
        fechaRealResolucion: null,
        idUsuario: null,
      };
    },

    // eslint-disable-next-line no-unused-vars
    editDetalle(item, _x, _event) {
      //   this.selectedDetalle = item;
      this.saveButtonNameDetalle = "Editar contenido";

      this.unoAunoDetalle = {
        idUnoAUnoDetalle: item.IDUNOAUNODETALLE,
        idUnoAUno: item.IDUNOAUNO,
        idUnoAUnoEstado: item.IDUNOAUNOESTADO,
        idUnoAUnoCategoria: item.IDUNOAUNOCATEGORIA,
        detalle: item.DETALLE,
        planAccion: item.PLANACCION,
        porcentajeAvance: item.PORCENTAJEAVANCE,
        fechaResolucion: item.FECHARESOLUCION,
        fechaRealResolucion: item.FECHAREALRESOLUCION,
        idUsuario: this.$root.infoColaboradorActual.id,
        diasFaltantes: moment(
          typeof item.FECHAREALRESOLUCION == typeof null
            ? moment().format()
            : item.FECHAREALRESOLUCION
        ).diff(
          moment(
            typeof item.FECHARESOLUCION == typeof null
              ? moment().format()
              : item.FECHARESOLUCION
          ),
          "days"
        ),
      };

      this.tituloModalDetalle = "Editar contenido a la conversacion";
      this.showModalDetalle = true;
    },

    validaDetalle() {
      if (this.unoAunoDetalle.porcentajeAvance < 0) {
        Vue.$toast.error("El porcentaje de avance debe ser mayor a 0.", {});
        return false;
      }

      if (!this.unoAunoDetalle.detalle.trim().length > 0) {
        Vue.$toast.error("Ingrese un detalle.", {});
        return false;
      }

      if (!this.unoAunoDetalle.planAccion.trim().length > 0) {
        Vue.$toast.error("Ingrese un plan de acción.", {});
        return false;
      }

      return true;
    },

    async submitDetalle() {
      const res = this.validaDetalle();

      if (res) {
        this.unoAunoDetalle.planAccion = this.unoAunoDetalle.planAccion.replace(
          /"/g,
          "'"
        );
        this.unoAunoDetalle.detalle = this.unoAunoDetalle.detalle.replace(
          /"/g,
          "'"
        );
        this.unoAunoDetalle.idUsuario = this.$root.infoColaboradorActual.id;
        this.unoAunoDetalle.idUnoAUno = this.unoAuno.idUnoAUno;

        const res = await UpsertUnosAUnosDetalles({
          data: JSON.stringify({ ...this.unoAunoDetalle }),
        });

        if (!isNaN(res.data)) {
          //exito
          if (res.data > 0) {
            Vue.$toast.success("El contenido fue creado exitosamente.", {});
            this.showModalDetalle = false;
            // this.unoAunoDetalle.idUnoAUnoDetalle = res.data;
            this.getDetail(); //refresca la vista
          } else {
            Vue.$toast.success("El contenido fue modificado exitosamente.", {});
            this.showModalDetalle = false;
            this.getDetail(); //refresca la vista
          }
        } else {
          //error
          Vue.$toast.error("El contenido no se pudo crear.", {});
        }
      }
    },

    viewManagement() {
      switch (this.action) {
        case "CREATE":
          this.reset();
          this.header = "Crear registro Uno a Uno";
          this.saveButtonName = "Continuar con detalle";
          this.searchableColaborador = true;
          break;
        case "EDIT":
          this.getDetail();
          this.header = " Editar registro Uno a Uno";
          this.saveButtonName = "Editar encabezado";
          this.searchableColaborador = false;
          break;
        case "VIEW":
          this.getDetail();
          this.header = "Ver registro Uno a Uno";
          this.saveButtonName = "Editar encabezado";
          this.searchableColaborador = false;
          break;
        default:
          //  this.getNewDetail();
          break;
      }
    },
  },
  computed: {
    isDisabled() {
      if (this.action === "VIEW") {
        return true;
      }
      return false;
    },
    today() {
      return new Date();
    },
    showDetail() {
      if (this.unoAuno.idUnoAUno != null) {
        return true;
      } else {
        return false;
      }
    },
  },
};
</script>
<style>
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

.delete {
  color: red;
  cursor: pointer;
}
.p-galleria .p-galleria-thumbnail-container {
  background: #f0f0f000;
}

.p-galleria-thumbnail-item {
  background-color: lightgrey;
}
.required label::after {
  content: " *";
  color: red;
}

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
