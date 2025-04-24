<template>
  <div class="wrapper vld-parent" ref="formContainer">
    <div>
      <CCard style="border-radius: 16px !important; margin-top: 20px">
        <CCardHeader
          style="
            border-radius: 16px !important;
            padding-bottom: 0px;
            border-bottom: transparent !important;
            background-color: #ffffff;
          "
        >
          <strong>Seguimiento</strong>
        </CCardHeader>
        <CCardBody style="padding-top: 0px; padding-bottom: 0px">
          <p>Lista de solicitudes de cambio.</p>
        </CCardBody>
      </CCard>

      <CRow>
        <CCol md="3" sm="12" class="mb-1">
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
        <CCol md="9" sm="12" class="pull-right mb-1" style="">
          <CButton
            color="primary"
            @click="exportar()"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark ml-1"
          >
            <i class="pi pi-download"></i>&nbsp;Exportar</CButton
          >
          <CButton
            color="primary"
            @click="clearFilters"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark ml-1"
          >
            <i class="pi pi-trash"></i>
            Limpiar filtros
          </CButton>
          <CButton
            color="primary"
            @click="toggleFilter"
            ref="btnFilter"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark ml-1"
          >
            <i class="pi pi-filter"></i>
            {{ btnFilter.text }}
          </CButton>
          <CButton
            color="primary"
            @click="toggleConsultar"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark ml-1"
          >
            <i class="pi pi-list"></i>
            {{ btnLista.label }}
          </CButton>
          <CButton
            color="primary"
            @click="showCambiarResponsable()"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark ml-1"
            v-if="esRRHH()"
          >
            <i class="pi pi-user-edit"></i>
            Cambiar Responsable
          </CButton>
          <CButton
            color="primary"
            @click="showAnularVacaciones()"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark ml-1"
            v-if="esRRHH()"
          >
            <i class="pi pi-times-circle"></i>
            Anular
          </CButton>
        </CCol>
      </CRow>

      <CRow>
        <CCol col>
          <!-- <CCard style="border-radius: 16px !important; margin-top: 20px;">
          <CCardHeader style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;">
            </CCardHeader>
            <CCardBody> -->
          <CRow>
            <SplitButton
              v-if="btnLista.label == 'Todas'"
              class="pull-right libro-marca-celeste onHoverDark ml-3"
              label="En tránsito"
              icon="pi pi-filter"
              :model="listaTransito"
            ></SplitButton>
            <CCol lg="12">
              <DataTable
                ref="dt"
                :value="listaSolicitudes"
                :paginator="true"
                class="p-datatable-colaboradores p-datatable-striped p-datatable-sm"
                :rows="10"
                dataKey="idSolicitud"
                :rowHover="true"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} solicitudes"
                :sortField="obtenerCampoOrdenamiento"
                :sortOrder="tipoOrden"
                :autoLayout="true"
                @update:filters="filterApplied"
                @filter="filterApplied"
                :selection.sync="lineaSeleccionada"
                selectionMode="single"
                stateStorage="session"
                stateKey="dt-state-tracking-admin"
              >
                <template #header>
                  <CCollapse :show="!collapse">
                    <CCard color="light">
                      <CCardBody
                        style="
                          padding-top: 15px !important;
                          padding-bottom: 0px !important;
                        "
                      >
                        <CRow>
                          <div
                            class="p-field p-col-12 p-md-4"
                            style="padding: 10px 5px 0px 5px !important"
                          >
                            <span class="p-float-label">
                              <MultiSelect
                                v-model="filters['empresas']"
                                :options="empresas"
                                optionLabel="nombre"
                                optionValue="valor"
                                class="p-column-filter w-100"
                                display="chip"
                                :filter="true"
                              >
                              </MultiSelect>
                              <label for="dropdown">Empresas</label>
                            </span>
                          </div>

                          <div
                            class="p-field p-col-12 p-md-4"
                            style="padding: 10px 5px 0px 5px !important"
                          >
                            <span class="p-float-label">
                              <MultiSelect
                                v-model="filters['esNomina']"
                                :options="esNomina"
                                optionLabel="nombre"
                                optionValue="valor"
                                class="p-column-filter w-100"
                                display="chip"
                                :filter="true"
                              >
                              </MultiSelect>
                              <label for="dropdown">Tipo de planilla</label>
                            </span>
                          </div>
                          <div
                            class="p-field p-col-12 p-md-4"
                            style="padding: 10px 5px 0px 5px !important"
                          >
                            <label>Período:&nbsp;</label>
                            <date-range-picker
                              opens="left"
                              :startDate="startDate"
                              :endDate="endDate"
                              :ranges="ranges"
                              :locale-data="locale"
                              @update="actualizarRango"
                              v-model="dateRange"
                            >
                            </date-range-picker>
                          </div>
                        </CRow>
                      </CCardBody>
                    </CCard>
                  </CCollapse>
                  <div>
                    <CBadge
                      v-if="filters.global"
                      style="background-color: #29abe2"
                      class="mr-2"
                    >
                      {{ filters.global }}
                    </CBadge>
                    <CBadge
                      v-show="filtroEnTransito !== 'Todas'"
                      color="info"
                      v-bind:class="badgeClass(filtroEnTransito)"
                      class="mr-2"
                    >
                      {{ filtroEnTransito }}
                    </CBadge>
                    <CBadge
                      v-show="filters.empresas"
                      v-for="item in filters.empresas"
                      :key="item.idempresacorporacion"
                      style="background-color: #29abe2"
                      class="mr-2"
                    >
                      {{ item }}
                    </CBadge>
                    <CBadge
                      v-show="filters.esNomina"
                      v-for="item in filters.esNomina"
                      :key="item.esNomina"
                      style="background-color: #29abe2"
                      class="mr-2"
                    >
                      {{ item }}
                    </CBadge>
                  </div>
                </template>
                <template #empty> No se encontraron solicitudes. </template>
                <template #loading>
                  Cargando la información de solicitudes. Por favor espere.
                </template>
                <!--   <Column
                      selectionMode="multiple"
                      headerStyle="width: 3em"
                    ></Column>-->
                <Column
                  field="idSolicitud"
                  header="Id"
                  :sortable="true"
                  headerStyle="width:6%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Id</span>
                    {{ slotProps.data.idSolicitud }}
                  </template>
                  <!-- <template #filter>
                                <InputText type="text" v-model="filters['name']" class="p-column-filter" placeholder="Search by name"/>
                            </template> -->
                </Column>
                <Column
                  header="Tipo de Solicitud"
                  :sortable="true"
                  sortField="tipoSolicitud"
                  filterField="tipoSolicitud"
                  filterMatchMode="contains"
                  headerStyle="width:13%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Tipo de Solicitud</span>

                    <a
                      href=""
                      style="cursor: hand"
                      @click.prevent="mostrarSolicitud(slotProps.data)"
                      >{{ slotProps.data.tipoSolicitud }}</a
                    >
                  </template>
                </Column>
                <Column
                  header="Estado"
                  :sortable="true"
                  sortField="estado"
                  filterField="estado"
                  filterMatchMode="contains"
                  headerStyle="width:8%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Estado</span>
                    {{ slotProps.data.estado }}
                  </template>
                </Column>
                <Column
                  header="Autor"
                  :sortable="true"
                  sortField="autor"
                  filterField="autor"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Autor</span>
                    {{ slotProps.data.autor }}
                  </template>
                </Column>
                <Column
                  header="Colaborador"
                  :sortable="true"
                  sortField="nombreColaborador"
                  filterField="nombreColaborador"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Colaborador</span>
                    {{ slotProps.data.nombreColaborador }}
                  </template>
                </Column>
                <Column
                  header="Responsable"
                  :v-show="this.$root.userRoles.some((p) => p == 'xxx')"
                  :sortable="true"
                  sortField="responsable"
                  filterField="responsable"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Responsable</span>
                    {{ slotProps.data.responsable }}
                  </template>
                </Column>
                <Column
                  header="Delivery Manager"
                  :sortable="true"
                  sortField="deliveryManager"
                  filterField="deliveryManager"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Delivery Manager</span>
                    {{ slotProps.data.deliveryManager }}
                  </template>
                </Column>
                <Column
                  header="Última modificación"
                  :v-show="this.$root.userRoles.some((p) => p == 'xxx')"
                  :sortable="true"
                  sortField="fechaModificacion"
                  filterField="fechaModificacion"
                  filterMatchMode="contains"
                  headerStyle="width:15%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Última modificación</span>
                    {{ formatearFecha(slotProps.data.fechaModificacion) }}
                    <div
                      class="progress"
                      v-if="
                        showMeter(
                          slotProps.data.codigoEstado,
                          slotProps.data.rango
                        )
                      "
                    >
                      <!-- <div class="progress-bar" v-bind:class="meterClass(slotProps.data.fechaModificacion)" role="progressbar" style="width: 100%" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100">{{getMeterLabel(slotProps.data.fechaModificacion)}}</div> -->
                      <div
                        class="progress-bar"
                        v-bind:class="meterClass(slotProps.data.rango)"
                        role="progressbar"
                        style="width: 100%"
                        aria-valuenow="100"
                        aria-valuemin="0"
                        aria-valuemax="100"
                      >
                        {{ getMeterLabel(slotProps.data.rango) }}
                      </div>
                    </div>
                  </template>
                </Column>
                <Column
                  header="Empresa"
                  filterField="empresas"
                  filterMatchMode="custom"
                  :filterFunction="customFilter"
                  headerStyle="display:none;"
                  bodyStyle="display:none;"
                >
                </Column>
                <Column
                  header="EsNomina"
                  filterField="esNomina"
                  filterMatchMode="custom"
                  :filterFunction="customFilter"
                  headerStyle="display:none;"
                  bodyStyle="display:none;"
                >
                </Column>
              </DataTable>
            </CCol>
          </CRow>
          <!-- </CCardBody>
          </CCard> -->
        </CCol>
      </CRow>
    </div>

    <Dialog
      :visible.sync="mostrarCambioResponsable"
      :style="{ width: '35vw' }"
      :modal="true"
    >
      <template #header>
        <h5>Cambiar responsable</h5>
      </template>
      <CRow class="pt-2">
        <CCol sm="12" lg="12" class="pull-right">
          <CSelect
            label="Seleccione el nuevo responsable"
            :options="responsableList"
            :value.sync="nuevoResponsable"
            add-label-classes="label-title"
          />
        </CCol>
      </CRow>

      <template #footer>
        <CButton
          color="primary"
          @click="cambiarResponsable()"
          class="mr-1 btn btn-info libro-marca-azuloscuro"
        >
          <span class="pi pi-check"></span> <span>Cambiar responsable</span>
        </CButton>
        <CButton
          color="primary"
          @click="mostrarCambioResponsable = false"
          class="mr-1 btn btn-info libro-marca-celeste"
        >
          <span class="pi pi-times"></span> <span>Cancelar</span>
        </CButton>
      </template>
    </Dialog>

    <Dialog
      :visible.sync="mostrarAnularVacaciones"
      :style="{ width: '35vw' }"
      :modal="true"
    >
      <template #header>
        <h5>Anular Solicitud</h5>
      </template>
      <CRow class="pt-2">
        <CCol sm="12" lg="12" class="pull-right">
          <!-- <FormulateInput
            type="textarea"
            name="comentario"
            label="Comentario"
            :input-class="['form-control']"
            rows="4"
            :help-class="['hint']"
          /> -->
          <CTextarea
            v-model="comentario"
            label="Comentario"
            placeholder="Ingrese un comentario para la anulación."
            vertical
            rows="4"
          />
        </CCol>
      </CRow>

      <template #footer>
        <CButton
          color="primary"
          @click="AnularVacaciones()"
          class="mr-1 btn btn-info libro-marca-azuloscuro"
        >
          <span class="pi pi-check"></span> <span>Anular</span>
        </CButton>
        <CButton
          color="primary"
          @click="mostrarAnularVacaciones = false"
          class="mr-1 btn btn-info libro-marca-celeste"
        >
          <span class="pi pi-times"></span> <span>Cancelar</span>
        </CButton>
      </template>
    </Dialog>
  </div>
</template>

<script>
import Vue from "vue";

import {
  GetSolicitudesPorPerfil,
  GetSolicitudesBuzon,
  CambiarResponsable,
  GetResponsablePorPerfil,
  AnularSolicitud,
  GetEmpresaCorporacion,
} from "./request";

import moment from "moment";
import DateRangePicker from "vue2-daterange-picker";
import "vue2-daterange-picker/dist/vue2-daterange-picker.css";
moment.locale("es");

import { aplicarFormatoFecha } from "../../utils";
import XLSX from "sheetjs-style";

export default {
  name: "TrackingAdmin",
  components: {
    DateRangePicker,
  },
  data() {
    return {
      comentario: "",
      mostrarCambioResponsable: false,
      mostrarAnularVacaciones: false,
      esTipoVacaciones: false,
      responsableList: [],
      nuevoResponsable: 0,
      lineaSeleccionada: null,
      collapse: false,
      dt: null,
      btnFilter: {
        text: "Ocultar filtros",
      },
      colaboradorSeleccionado: null,
      colaboradoresSeleccionados: [],
      solicitudes: null,
      filters: {},
      loading: true,
      primaryModal: false,
      items: [],
      tiposDeSolicitud: [],
      empresas: [],
      esNomina: [
        {
          nombre: "Nomina",
          valor: "Nomina",
        },
        {
          nombre: "Servicios profesionales / Otros casos",
          valor: "Servicios profesionales / Otros casos",
        },
      ],
      mostrarTiposSolicitud: false,
      solicitudSeleccionada: 0,
      optionsRadio: [
        // "Option1",
        { value: "Todos", label: "Todos", props: { checked: true } },
        {
          value: "Seleccionados",
          label: "Seleccionados",
          props: { disabled: true },
        },
      ],
      loader: null,
      esGrupo: true,
      btnLista: {
        label: "Todas",
      },
      campoOrdenamiento: "fechaModificacion",
      tipoOrden: 1,
      filtroEnTransito: "Todas",
      listaTransito: [
        {
          label: "A tiempo",
          icon: "pi pi-circle-on text-success",
          command: () => {
            this.filtroEnTransito = "A tiempo";
          },
        },
        {
          label: "Requiere atención",
          icon: "pi pi-circle-on text-warning",
          command: () => {
            this.filtroEnTransito = "Requiere atención";
          },
        },
        {
          label: "Vencidas",
          icon: "pi pi-circle-on text-danger",
          command: () => {
            this.filtroEnTransito = "Vencidas";
          },
        },
        {
          label: "Todas",
          icon: "pi pi-circle-off",
          command: () => {
            this.filtroEnTransito = "Todas";
          },
        },
      ],
      startDate: new Date(),
      endDate: new Date(),
      locale: {
        direction: "ltr", //direction of text
        format: "dd-mm-yyyy", //fomart of the dates displayed
        separator: " al ", //separator between the two ranges
        applyLabel: "Aceptar",
        cancelLabel: "Cancelar",
        weekLabel: "W",
        customRangeLabel: "Custom Range",
        daysOfWeek: ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb"],
        monthNames: [
          "Ene",
          "Feb",
          "Mar",
          "Abr",
          "May",
          "Jun",
          "Jul",
          "Ago",
          "Sep",
          "Oct",
          "Nov",
          "Dic",
        ],
        // daysOfWeek: moment.weekdaysMin(), //array of days - see moment documenations for details
        // monthNames: moment.monthsShort(), //array of month names - see moment documenations for details
        firstDay: 1, //ISO first day of week - see moment documenations for details
      },
      dateRange: {
        startDate: new Date(new Date().getFullYear(), new Date().getMonth(), 1), //new Date((new Date()).setDate((new Date()).getDate()-6)), //new Date(),
        endDate: new Date(
          new Date().getFullYear(),
          new Date().getMonth() + 1,
          0
        ),
      },
      ranges: {
        //default value for ranges object (if you set this to false ranges will no be rendered)
        Hoy: [this.hoy, this.hoy],
        Ayer: [new Date().getDate() - 1, new Date().getDate() - 1],
        "Este mes": [moment().startOf("month"), moment().endOf("month")],
        "Este año": [moment().startOf("year"), moment().endOf("year")],
        "Última semana": [
          moment().subtract(1, "week").startOf("week"),
          moment().subtract(1, "week").endOf("week"),
        ],
        "Mes anterior": [
          moment().subtract(1, "month").startOf("month"),
          moment().subtract(1, "month").endOf("month"),
        ],
      },
    };
  },
  async mounted() {
    this.initRanges();
    await this.cargarDatos("TrackingAdmin");
    this.getListaEmpresas();
    this.lineaSeleccionada = null;
  },
  methods: {
    async getListaEmpresas() {
      try {
        const { data } = await GetEmpresaCorporacion();
        this.empresas = data.empresaCorporacions;
        this.empresas.unshift({
          nombre: "Sin empresa",
          valor: "",
        });
      } catch (error) {
        console.log(error);
      }
    },
    esRRHH() {
      return this.$root.userRoles.some((p) => p == "Expediente.RRHH");
    },

    async showCambiarResponsable() {
      this.responsableList = [];
      this.nuevoResponsable = 0;
      if (this.lineaSeleccionada) {
        if (
          this.lineaSeleccionada.codigoEstado == "EPV" ||
          this.lineaSeleccionada.codigoEstado == "EA" ||
          this.lineaSeleccionada.codigoEstado == "EP"
        ) {
          let idResponsable = this.lineaSeleccionada.idusuarioresponsable;
          let IdColaboradorSolicitud = this.lineaSeleccionada.idAutor;
          let idTipoSolicitud = this.lineaSeleccionada.idTipoSolicitud;
          let codigoEstado = this.lineaSeleccionada.codigoEstado;

          //call method getresponsablesXPerfil
          const {
            data: { responsables },
          } = await GetResponsablePorPerfil(
            idResponsable,
            IdColaboradorSolicitud,
            idTipoSolicitud,
            codigoEstado
          );
          this.responsableList = responsables.map(
            ({ idcolaborador, nombreCompleto }) => ({
              value: idcolaborador,
              label: nombreCompleto,
            })
          );

          this.responsableList.push({
            label: "Seleccione un responsable",
            value: 0,
          });

          this.mostrarCambioResponsable = true;
        } else {
          Vue.$toast.warning(
            "Debe de seleccionar una solicitud en estado en Proceso o de Aprobación.",
            {}
          );
        }
      } else {
        Vue.$toast.warning("Debe de seleccionar una solicitud.", {});
      }
    },

    async cambiarResponsable() {
      if (this.nuevoResponsable > 0) {
        let result = await CambiarResponsable({
          id: this.lineaSeleccionada.idSolicitud,
          idResponsable: this.nuevoResponsable,
        });

        if (result.data > 0) {
          Vue.$toast.success(
            "Se ha cambiado el responsable correctamente.",
            {}
          );
          this.mostrarCambioResponsable = false;
          this.cargarDatos("TrackingAdmin");
          this.lineaSeleccionada = null;
        } else {
          Vue.$toast.error("No se pudo cambiar el responsable.", {});
        }
      } else {
        Vue.$toast.warning("Debe de seleccionar un responsable.", {});
      }
    },

    // showAnularVacaciones() {
    //   if (
    //     this.lineaSeleccionada.tipoSolicitud.trim() ==
    //       "Solicitud de vacaciones" ||
    //     this.lineaSeleccionada.tipoSolicitud.trim() == "Solicitud de permiso"
    //   ) {
    //     if (this.lineaSeleccionada.codigoEstado == "AP") {
    //       this.mostrarAnularVacaciones = true;
    //     } else {
    //       Vue.$toast.warning(
    //         "La solicitud debe de estar aprobada para la anulación.",
    //         {}
    //       );
    //     }
    //   } else {
    //     Vue.$toast.warning(
    //       "Debe de seleccionar una solicitud de vacaciones.",
    //       {}
    //     );
    //   }
    // },

    showAnularVacaciones() {
      if (this.lineaSeleccionada != null) {
        if (this.lineaSeleccionada.codigoEstado != "AN") {
          this.mostrarAnularVacaciones = true;
        } else {
          Vue.$toast.warning("La solicitud ya se encuentra anulada.", {});
        }
      } else {
        Vue.$toast.warning("Debe de seleccionar una solicitud.", {});
      }
    },

    async AnularVacaciones() {
      if (this.comentario.trim() == "") {
        Vue.$toast.warning("Debe de ingresar un comentario.", {});
      } else {
        let result = await AnularSolicitud({
          Id: this.lineaSeleccionada.idSolicitud,
          IdColaborador: this.$root.infoColaboradorActual.id,
          comentario: this.comentario,
        });

        if (result.data > 0) {
          Vue.$toast.success("Se ha anulado la solicitud correctamente.", {});
          this.mostrarAnularVacaciones = false;
          this.cargarDatos("TrackingAdmin");
          this.lineaSeleccionada = null;
          this.comentario = "";
        } else {
          Vue.$toast.error("No se pudo anular la solicitud.", {});
        }
      }
    },

    formatearFecha(fecha) {
      return aplicarFormatoFecha(fecha, "dd-MM-yyyy");
    },
    clearFilters() {
      this.filters = {};
      this.colaboradoresSeleccionados = null;
    },
    toggleFilter() {
      this.collapse = !this.collapse;
      this.btnFilter.text = this.collapse
        ? "Mostrar filtros"
        : "Ocultar filtros";
    },
    async toggleConsultar() {
      if (this.btnLista.label == "Todas") {
        await this.cargarDatos("Todas");
        this.campoOrdenamiento = "fechaModificacion";
        this.tipoOrden = -1;
      } else {
        await this.cargarDatos("TrackingAdmin");
        this.campoOrdenamiento = "fechaModificacion";
        this.tipoOrden = 1;
      }
      //  this.filters = {};
      this.btnLista.label =
        this.btnLista.label == "Todas" ? "En tránsito" : "Todas";
      this.filtroEnTransito = "Todas";
    },
    filterApplied(e) {},
    /**
     * Retorna una promesa para obtener la lista de solicitudes
     */
    async getLista(id, tipo) {
      try {
        let rango = {
          fechaInicio: new Date(
            this.dateRange.startDate.getFullYear(),
            this.dateRange.startDate.getMonth(),
            this.dateRange.startDate.getDate()
          ),
          fechaFin: new Date(
            this.dateRange.endDate.getFullYear(),
            this.dateRange.endDate.getMonth(),
            this.dateRange.endDate.getDate()
          ),
        };
        return GetSolicitudesBuzon(id, tipo, rango.fechaInicio, rango.fechaFin);
      } catch (error) {
        console.log(error);
      }
    },
    async ver(item, x, event) {
      let loader = this.showLoader();
      this.colaboradorSeleccionado = item;
      setTimeout(
        () =>
          this.$router.push({
            name: "Perfil del Colaborador",
            params: {
              id: item.idcolaborador,
              origen: "Colaborador",
              isConsulta: false,
            },
          }),
        500
      );
    },
    /**
     * Modifica el valor de la variable que indica si el modal de tipos de solicitudes debe mostrarse
     */
    async mostrarModalSolicitudes() {
      this.mostrarTiposSolicitud = true;
    },
    /**
     * Obtiene los datos del tipo de solicitud que el usuario desea crear
     * para luego invocar el método que se encarga de mostrar la vista
     * según el tipo de solicitud
     */
    async crearSolicitud() {
      try {
        let inbox = this;
        // oculta popup
        this.cancelarMostrarTiposSolicitud();

        // busca el tipo de solicitud seleccionada por el usuario
        let indice = this.tiposDeSolicitud.findIndex(function (tipo, index) {
          if (tipo.value == inbox.solicitudSeleccionada) return true;
        });
        // carga el formulario según el tipo de solicitud
        this.cargarSolicitud(
          this.obtenerObjetoCargarSolicitud(
            inbox.tiposDeSolicitud[indice].value,
            inbox.tiposDeSolicitud[indice].label,
            null // solicitud nueva
          )
        );
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Obtiene los datos del tipo de solicitud asociada a la solicitud
     * que el usuario desea abrir para luego invocar el método que se
     * encarga de mostrar la vista según el tipo de solicitud
     *
     * @datosSolicitud datos de la solicitud que seleccionada por el usuario
     */
    mostrarSolicitud(datosSolicitud) {
      // carga el formulario según el tipo de solicitud
      this.cargarSolicitud(
        this.obtenerObjetoCargarSolicitud(
          datosSolicitud.idTipoSolicitud,
          datosSolicitud.tipoSolicitud,
          datosSolicitud.idSolicitud
        )
      );
    },
    /**
     * Redirige al usuario a la vista correspondiente del tipo de solicitud
     */
    cargarSolicitud(solicitud) {
      this.$router.push({
        name: "Administrative",
        params: {
          origen: "TrackingAdmin", // origen desde donde se accedio la vista
          idTipoSolicitud: solicitud.idTipoSolicitud, // id del tipo de la solicitud generada
          tipoSolicitud: solicitud.tipoSolicitud, // nombre del tipo de la solicitud generada
          idSolicitud: solicitud.idSolicitud,
        },
      });
    },
    /**
     * Genera objeto con los datos de la solicitud que se desea mostrar
     */
    obtenerObjetoCargarSolicitud(idTipoSolicitud, tipoSolicitud, idSolicitud) {
      return {
        idTipoSolicitud: idTipoSolicitud,
        tipoSolicitud: tipoSolicitud,
        idSolicitud: idSolicitud,
      };
    },
    cancelarMostrarTiposSolicitud() {
      this.mostrarTiposSolicitud = false;
    },
    async submit(registroActual) {
      try {
        // let loader = this.showLoader();
        //const { data } = await Upsert(registroActual);
        //this.primaryModal = false;
        //this.toast = { show: true, message: "Se almaceno con exito" };
        //this.getLista();
        // this.hideLoader(loader);
      } catch (error) {
        // this.toast = {
        //    show: true,
        //    message: `Se produjo un errror ${error.message}`,
        // };
      }
    },
    showLoader() {
      return this.$loading.show({
        // Optional parameters
        container: this.fullPage ? null : this.$refs.formContainer,
        canCancel: true,
        onCancel: this.onCancel,
      });
    },
    hideLoader(loader) {
      setTimeout(() => {
        loader.hide();
      }, 300);
    },
    /**
     * Retorna la promesa asociada a la llamada asíncrona
     * del API que devuelve la lista de tipos de solicitud
     * según el perfil del usuario
     */
    async obtenerTiposSolicitudes() {
      return GetSolicitudesPorPerfil();
    },
    /***
     * Carga lo datos requeridos para la vista
     */
    async cargarDatos(tipo) {
      let tracking = this;
      let loader = tracking.showLoader();

      tracking.loading = true;

      await tracking
        .getLista(this.$root.infoColaboradorActual.id, tipo)
        // lista de solicitudes
        .then(function (result) {
          tracking.solicitudes = result.data.solicitudesMasterData;
          tracking.loading = false;
          tracking.hideLoader(loader);
        })
        .catch((error) => {
          console.error("(2) Inside error:", error);
        });
    },
    claseEstado: function (estado) {
      return {
        "text-success": estado === "AP",
        "text-danger": estado === "DE" || estado === "DC" || estado === "AN",
        "text-warning": !(
          estado === "AP" ||
          estado === "DE" ||
          estado === "DC"
        ),
      };
    },
    showMeter: function (estado, rango) {
      return (
        !(
          estado === "AP" ||
          estado === "DE" ||
          estado === "DC" ||
          estado === "AN"
        ) && rango != 0
      );
    },
    badgeClass(tipoFiltro) {
      return {
        "bg-success": tipoFiltro == "A tiempo",
        "bg-warning": tipoFiltro == "Requiere atención",
        "bg-danger": tipoFiltro == "Vencidas",
      };
    },
    meterClass(rango) {
      //(fecha){
      return {
        "bg-success": rango == 1,
        "bg-warning": rango == 2,
        "bg-danger": rango == 3,
      };
    },
    getMeterLabel(rango) {
      let label = "";
      switch (rango) {
        case 1:
          label = "<= 3 días";
          break;
        case 2:
          label = "> 3 y <= 8 días";
          break;
        case 3:
          label = "> 8 días";
          break;

        default:
          break;
      }
      return label;
    },
    obtenerDiferenciaDias(fecha) {
      var ToDate = new Date();
      var fromDate = new Date(fecha);
      var Difference_In_Time = ToDate.getTime() - fromDate.getTime();
      return Difference_In_Time / (1000 * 3600 * 24);
    },
    async exportar() {
      var data = this.$refs.dt.processedData.map(
        ({
          idSolicitud,
          tipoSolicitud,
          estado,
          autor,
          nombreColaborador,
          responsable,
          fechaModificacion,
          deliveryManager,
        }) => ({
          Id_Solicitud: idSolicitud,
          Tipo_Solicitud: tipoSolicitud,
          Estado: estado,
          Autor: autor,
          Colaborador: nombreColaborador,
          Responsable: responsable,
          Delivery_Manager: deliveryManager,
          Fecha_Ultima_Modificacion: fechaModificacion,
        })
      );

      const wb = XLSX.utils.book_new();
      const ws = XLSX.utils.json_to_sheet(data);
      XLSX.utils.book_append_sheet(wb, ws, "Reporte_Seguimiento_Admin.csv");
      XLSX.writeFile(
        wb,
        `Reporte_Seguimiento_Administrativo_${this.getDateTime()}.csv`
      );
      Vue.$toast.success("Generación exitosa del archivo.");
    },
    getDateTime() {
      const today = new Date();
      const date =
        today.getFullYear() +
        "-" +
        (today.getMonth() + 1) +
        "-" +
        today.getDate();
      const time =
        today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
      const dateTime = date + "_" + time;
      return dateTime;
    },
    customFilter(value, filter) {
      if (
        filter === undefined ||
        filter === null ||
        (typeof filter === "string" && filter.trim() === "") ||
        (typeof filter === "object" && filter.length === 0)
      ) {
        return true;
      }

      if (value === undefined || value === null) {
        return false;
      }

      for (let i = 0; i < filter.length; i++) {
        if (
          (value === "" && filter[i] === "") ||
          (filter[i] !== "" && value.indexOf(filter[i]) !== -1)
        ) {
          return true;
        }
      }
      return false; //value.indexOf(filter) !== -1;
    },
    getMonth() {
      var d = new Date();
      var n = d.getMonth();
      var months = new Array(
        "Enero",
        "Febrero",
        "Marzo",
        "Abril",
        "Mayo",
        "Junio",
        "Julio",
        "Agosto",
        "Setiembre",
        "Octubre",
        "Noviembre",
        "Diciembre"
      );
      return months[n] + " " + d.getFullYear().toString();
    },
    initRanges() {
      let hoy = new Date();
      let ayer = new Date();
      let y = hoy.getFullYear();
      let m = hoy.getMonth();
      let s = new Date();

      s.setDate(s.getDate() - 6);
      ayer.setDate(hoy.getDate() - 1);

      this.ranges = {
        //default value for ranges object (if you set this to false ranges will no be rendered)
        // 'Hoy': [hoy, hoy],
        // 'Ayer': [ayer, ayer],
        "Este mes": [new Date(y, m, 1), new Date(y, m + 1, 0)],
        "Mes anterior": [new Date(y, m - 1, 1), new Date(y, m, 0)],
        "Este año": [
          new Date(hoy.getFullYear(), 0, 1),
          new Date(hoy.getFullYear(), 11, 31),
        ],
        "Año anterior": [
          new Date(hoy.getFullYear() - 1, 0, 1),
          new Date(hoy.getFullYear() - 1, 11, 31),
        ],
        // 'Últimos 7 días': [s,hoy],
      };
    },
    async actualizarRango() {
      await this.cargarDatos("TrackingAdmin");
    },
  },
  computed: {
    obtenerCampoOrdenamiento() {
      return this.campoOrdenamiento;
    },
    listaSolicitudes() {
      if (this.btnLista.label == "Todas") {
        switch (this.filtroEnTransito) {
          case "Todas":
            return this.solicitudes;
          case "A tiempo":
            return this.solicitudes.filter(function (el) {
              return el.rango == 1;
            });
          case "Requiere atención":
            return this.solicitudes.filter(function (el) {
              return el.rango == 2;
            });
          case "Vencidas":
            return this.solicitudes.filter(function (el) {
              return el.rango == 3;
            });
          default:
            return this.solicitudes;
        }
      } else return this.solicitudes;
    },
  },
  watch: {
    lineaSeleccionada: function (newValue, oldValue) {
      if (
        this.lineaSeleccionada.tipoSolicitud == "Solicitud de vacaciones" ||
        this.lineaSeleccionada.tipoSolicitud == "Solicitud de permiso"
      ) {
        this.esTipoVacaciones = true;
      } else {
        this.esTipoVacaciones = false;
      }
    },
  },
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
.libro-marca-azuloscuro {
  background-color: #002e6e !important;
}
.libro-marca-celeste {
  background-color: #29abe2;
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

/* Responsive: en visualización normal se ocultan las etiquetas asociadas al título de cada columna */
.p-datatable-colaboradores .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-colaboradores .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-colaboradores .p-datatable-tbody > tr > td {
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
  background-color: #f8f8f8;
}

.p-datatable .p-paginator-bottom {
  width: 99%;
}

.btn-opciones {
  display: none;
}

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de
   etiqueta y valor asociado */
@media screen and (max-width: 40em) {
  .p-datatable.p-datatable-colaboradores .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-colaboradores .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-colaboradores .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-colaboradores .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-colaboradores:last-child {
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
}
/* FIN */
</style>
