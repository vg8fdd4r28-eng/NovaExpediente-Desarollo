<template>
  <div>
    <TabView>
      <TabPanel header="Vacaciones">
        <div class="wrapper vld-parent">
          <CRow>
            <CCol lg="12">
              <CCard>
                <CCardHeader>
                  <CIcon name="cil-justify-center" /><strong>
                    Consulta de Vacaciones</strong
                  >
                  <small> Lista de vacaciones</small>
                </CCardHeader>
                <CCardBody>
                  <DataTable
                    ref="dt"
                    :value="vacaciones"
                    :paginator="true"
                    class="p-datatable-consultas p-datatable-striped p-datatable-sm"
                    :rows="10"
                    dataKey="idvacacion"
                    :rowHover="true"
                    :filters.sync="filters"
                    :loading="loading"
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    :rowsPerPageOptions="[5, 10, 25, 50]"
                    currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} vacaciones"
                    sortField="nombre"
                    :sortOrder="1"
                    stateKey="dt-state-vacacion"
                    :autoLayout="true"
                  >
                    <template #header>
                      <CRow>
                        <CCol md="3" sm="12" class="mb-2">
                          <div class="table-header">
                            Buscar
                            <span class="p-input-icon-left w-75">
                              <i class="pi pi-search" />
                              <InputText
                                v-model="filters['global']"
                                placeholder="Escribe lo que buscas"
                                class="w-100"
                              />
                            </span>
                          </div>
                        </CCol>
                        <CCol md="4" sm="12" class="mb-2">
                          <div class="float-right">
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
                        </CCol>
                        <CCol md="2" sm="12" class="mb-2 pull-right">
                          <div style="text-align: right">
                            <CButton
                              block
                              color="primary"
                              @click="exportar()"
                              class="mr-1 btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark btn-primary"
                            >
                              <i class="pi pi-download"></i
                              >&nbsp;Exportar</CButton
                            >
                          </div>
                        </CCol>
                        <CCol md="3" sm="12" class="mb-2 pull-right">
                          <div style="text-align: right">
                            <CButton
                              block
                              color="primary"
                              @click="mostrarPropias()"
                              class="mr-1 btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark btn-primary"
                              v-if="puedeMostrarPropias()"
                            >
                              <i class="pi " v-bind:class="iconClass()"></i
                              >&nbsp;{{ nombreBotonComputed }}</CButton
                            >
                          </div>
                        </CCol>
                      </CRow>
                      <div>
                        <CBadge
                          v-if="filters.global"
                          color="secondary"
                          class="mr-2"
                        >
                          {{ filters.global }}
                        </CBadge>
                        <CBadge
                          v-show="showPropias"
                          color="info"
                          class="bg-success mr-2"
                        >
                          Mis vacaciones
                        </CBadge>
                        <CBadge
                          v-show="!showPropias"
                          color="info"
                          class="bg-warning mr-2"
                        >
                          Todas las vacaciones
                        </CBadge>
                      </div>
                    </template>
                    <template #empty>
                      No se encontraron vacaciones.
                    </template>
                    <template #loading>
                      Cargando la información de vacaciones. Por favor espere.
                    </template>

                    <Column
                      field="identificacionColaborador"
                      header="Identificación"
                      sortField="identificacionColaborador"
                      filterField="identificacionColaborador"
                      filterMatchMode="contains"
                      :sortable="true"
                      headerStyle="width:15%;"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Identificación</span>
                        {{ slotProps.data.identificacionColaborador }}
                      </template>
                    </Column>

                    <Column
                      field="Nombre"
                      header="Nombre del colaborador"
                      sortField="colaboradorNombre"
                      filterField="colaboradorNombre"
                      filterMatchMode="contains"
                      :sortable="true"
                      headerStyle="width:30%;"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title"
                          >Nombre del colaborador</span
                        >
                        {{ slotProps.data.colaboradorNombre }}
                      </template>
                    </Column>

                    <Column
                      field="fechainicio"
                      header="Fecha de inicio"
                      :sortable="true"
                      sortField="fechainicio"
                      filterField="fechainicio"
                      filterMatchMode="contains"
                      headerStyle="width:15%;"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Fecha de inicio</span>
                        {{ slotProps.data.fechainicio }}
                      </template>
                    </Column>

                    <Column
                      field="fechafinal"
                      header="Fecha de finalización"
                      sortField="fechafinal"
                      filterField="fechafinal"
                      filterMatchMode="contains"
                      :sortable="true"
                      headerStyle="width:15%;"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title"
                          >Fecha de finalización</span
                        >
                        {{ slotProps.data.fechafinal }}
                      </template>
                    </Column>

                    <Column
                      field="cantidaddias"
                      header="Cantidad de días"
                      sortField="cantidaddias"
                      filterField="cantidaddias"
                      filterMatchMode="contains"
                      :sortable="true"
                      headerStyle="width:15%;"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Cantidad de días</span>
                        {{ slotProps.data.cantidaddias }}
                      </template>
                    </Column>
                    <Column
                      field="idSolicitud"
                      header="# Solicitud"
                      sortField="idSolicitud"
                      filterField="idSolicitud"
                      filterMatchMode="contains"
                      :sortable="true"
                      headerStyle="width:10%;"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title"># Solicitud</span>
                        {{ slotProps.data.idSolicitud }}
                      </template>
                    </Column>
                  </DataTable>
                </CCardBody>
              </CCard>
            </CCol>
          </CRow>
        </div>
      </TabPanel>
      <TabPanel header="Incapacidades">
        <consulta-incapacidad></consulta-incapacidad>
      </TabPanel>
      <TabPanel header="Permisos">
        <consulta-permisos></consulta-permisos>
      </TabPanel>
    </TabView>
  </div>
</template>

<script>
import { GetVacacionesPorPerfil } from "./request";
import moment from "moment";
import DateRangePicker from "vue2-daterange-picker";
import "vue2-daterange-picker/dist/vue2-daterange-picker.css";
moment.locale("es");

import ConsultaIncapacidad from "./ConsultaIncapacidad.vue";
import ConsultaPermisos from "./ConsultaPermisos.vue";

import Vue from "vue";
import XLSX from "sheetjs-style";

export default {
  name: "ConsultaVacaciones",
  components: {
    DateRangePicker,
    "consulta-incapacidad": ConsultaIncapacidad,
    "consulta-permisos": ConsultaPermisos
  },
  data() {
    return {
      vacaciones: [],
      vacacionSeleccionado: [],
      loading: true,
      filters: {},
      showPropias: true,
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
          "Dic"
        ],
        // daysOfWeek: moment.weekdaysMin(), //array of days - see moment documenations for details
        // monthNames: moment.monthsShort(), //array of month names - see moment documenations for details
        firstDay: 1 //ISO first day of week - see moment documenations for details
      },
      dateRange: {
        startDate: new Date(new Date().getFullYear(), new Date().getMonth(), 1), //new Date((new Date()).setDate((new Date()).getDate()-6)), //new Date(),
        endDate: new Date(
          new Date().getFullYear(),
          new Date().getMonth() + 1,
          0
        )
      },
      ranges: {
        //default value for ranges object (if you set this to false ranges will no be rendered)
        Hoy: [this.hoy, this.hoy],
        Ayer: [new Date().getDate() - 1, new Date().getDate() - 1],
        "Este mes": [moment().startOf("month"), moment().endOf("month")],
        "Este año": [moment().startOf("year"), moment().endOf("year")],
        "Última semana": [
          moment()
            .subtract(1, "week")
            .startOf("week"),
          moment()
            .subtract(1, "week")
            .endOf("week")
        ],
        "Mes anterior": [
          moment()
            .subtract(1, "month")
            .startOf("month"),
          moment()
            .subtract(1, "month")
            .endOf("month")
        ]
      }
    };
  },
  async mounted() {
    this.initRanges();

    // se cargarian los request en caso de ser necesario o en created...

    await this.cargarTabla();
    const id = this.$root.infoColaboradorActual.id;

    setTimeout(
      () => (
        (this.loading = false),
        (this.vacaciones = this.vacaciones.filter(function(item) {
          return item.idcolaborador == id;
        }))
      ),
      500
    );
  },
  methods: {
    async cargarTabla() {
      let datos = {
        idColaborador: this.$root.infoColaboradorActual.id,
        fechaInicio: new Date(
          this.dateRange.startDate.getFullYear(),
          this.dateRange.startDate.getMonth(),
          this.dateRange.startDate.getDate()
        ),
        fechaFin: new Date(
          this.dateRange.endDate.getFullYear(),
          this.dateRange.endDate.getMonth(),
          this.dateRange.endDate.getDate()
        )
      };

      const {
        data: { vacaciones }
      } = await GetVacacionesPorPerfil(datos); //await GetVacacionesPorPerfil(this.$root.infoColaboradorActual.id);
      this.vacaciones = vacaciones.map(
        ({
          idvacacion,
          idcolaborador,
          fechainicio,
          fechafinal,
          cantidaddias,
          colaboradorNombre,
          identificacionColaborador,
          idSolicitud
        }) => ({
          idvacacion: idvacacion,
          idcolaborador: idcolaborador,
          fechainicio: moment(fechainicio).format("L"),
          fechafinal: moment(fechafinal).format("L"),
          cantidaddias: cantidaddias,
          colaboradorNombre: colaboradorNombre,
          identificacionColaborador: identificacionColaborador,
          idSolicitud: idSolicitud,
          fechaInicioSinFormato: fechainicio,
          fechaFinSinFormato: fechafinal
        })
      );
    },
    mostrarPropias() {
      if (this.showPropias) {
        this.showPropias = false;
        this.cargarTabla();
      } else {
        this.showPropias = true;

        const $this = this;

        this.vacaciones = this.vacaciones.filter(function(item) {
          return item.idcolaborador == $this.$root.infoColaboradorActual.id;
        });
      }
    },
    puedeMostrarPropias() {
      return (
        this.$root.userRoles.some(p => p == "Expediente.DM") ||
        this.$root.userRoles.some(p => p == "Expediente.RRHH")
      );
    },
    async actualizarRango(rango) {
      await this.cargarTabla();
      if (this.showPropias) {
        const $this = this;

        this.vacaciones = this.vacaciones.filter(function(item) {
          return item.idcolaborador == $this.$root.infoColaboradorActual.id;
        });
      }
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
          new Date(hoy.getFullYear(), 11, 31)
        ],
        "Año anterior": [
          new Date(hoy.getFullYear() - 1, 0, 1),
          new Date(hoy.getFullYear() - 1, 11, 31)
        ]
        // 'Últimos 7 días': [s,hoy],
      };
    },
    async exportar() {
      var data = this.vacaciones.map(
        ({
          fechaInicioSinFormato,
          fechaFinSinFormato,
          cantidaddias,
          colaboradorNombre,
          identificacionColaborador,
          idSolicitud
        }) => ({
          Identificacion: identificacionColaborador,
          Nombre_Completo: colaboradorNombre,
          Fecha_Inicio: moment(fechaInicioSinFormato).format("YYYY/MM/DD"),
          Fecha_Fin: moment(fechaFinSinFormato).format("YYYY/MM/DD"),
          Cantidad_Dias: cantidaddias,
          ID_Solicitud: idSolicitud
        })
      );

      const wb = XLSX.utils.book_new();
      const ws = XLSX.utils.json_to_sheet(data);
      XLSX.utils.book_append_sheet(wb, ws, "Vacaciones.csv");
      XLSX.writeFile(wb, `Vacaciones_${this.getDateTime()}.csv`);
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
    iconClass() {
      return {
        "pi-list": this.showPropias,
        "pi-user": !this.showPropias
      };
    }
  },
  computed: {
    nombreBotonComputed() {
      let nombre = "";
      if (this.showPropias) {
        if (this.$root.userRoles.some(p => p == "Expediente.DM")) {
          nombre = "Mis consultores";
        } else {
          nombre = "Todas";
        }
      } else {
        nombre = "Mis vacaciones";
      }

      return nombre;
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
.p-datatable-consultas .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-consultas .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-consultas .p-datatable-tbody > tr > td {
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

.btn-opciones {
  display: none;
}

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de
   etiqueta y valor asociado */
@media screen and (max-width: 40em) {
  .p-datatable.p-datatable-consultas .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-consultas .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-consultas .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-consultas .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-consultas:last-child {
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

.calendars {
  flex-wrap: unset !important;
}
</style>
