<template>
  <div>
    <div class="wrapper vld-parent">
      <CRow>
        <CCol lg="12">
          <CCard>
            <CCardHeader>
              <CIcon name="cil-justify-center" /><strong>
                Consulta de Incapacidades</strong
              >
              <small> Lista de Incapacidades</small>
            </CCardHeader>
            <CCardBody>
              <DataTable
                ref="dt"
                :value="incapacidades"
                :paginator="true"
                class="p-datatable-consultas p-datatable-striped p-datatable-sm"
                :rows="10"
                dataKey="idincapacidad"
                :rowHover="true"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} incapacidades"
                sortField="nombre"
                :sortOrder="1"
                stateKey="dt-state-incapacidad"
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
                          <i class="pi pi-download"></i>&nbsp;Exportar</CButton
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
                          <i class="pi " v-bind:class="iconClass()"></i>&nbsp;{{
                            nombreBotonComputed
                          }}</CButton
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
                      Mis incapacidades
                    </CBadge>
                    <CBadge
                      v-show="!showPropias"
                      color="info"
                      class="bg-warning mr-2"
                    >
                      Todas las incapacidades
                    </CBadge>
                  </div>
                </template>
                <template #empty>
                  No se encontraron incapacidades.
                </template>
                <template #loading>
                  Cargando la información de incapacidades. Por favor espere.
                </template>

                <!-- identificacion -->
                <Column
                  field="identificacionColaborador"
                  header="Identificación"
                  sortField="identificacionColaborador"
                  filterField="identificacionColaborador"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:12%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Identificación</span>
                    {{ slotProps.data.identificacionColaborador }}
                  </template>
                </Column>

                <!-- nombre  -->
                <Column
                  field="Nombre"
                  header="Nombre del colaborador"
                  sortField="colaboradorNombre"
                  filterField="colaboradorNombre"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:20%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Nombre del colaborador</span>
                    {{ slotProps.data.colaboradorNombre }}
                  </template>
                </Column>

                <!-- numero boleta  -->
                <Column
                  field="numboleta"
                  sortField="numboleta"
                  header="Boleta"
                  filterField="numboleta"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Número de boleta</span>
                    {{ slotProps.data.numboleta }}
                  </template>
                </Column>

                <!-- fecha inicio  -->
                <Column
                  field="fechainicio"
                  header="Inicio"
                  :sortable="true"
                  sortField="fechainicio"
                  filterField="fechainicio"
                  filterMatchMode="contains"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Fecha de inicio</span>
                    {{ slotProps.data.fechainicio }}
                  </template>
                </Column>

                <!-- fecha fin  -->
                <Column
                  field="fechafinal"
                  header="Fin"
                  sortField="fechafinal"
                  filterField="fechafinal"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Fecha de fin</span>
                    {{ slotProps.data.fechafinal }}
                  </template>
                </Column>

                <!-- cant dias  -->
                <Column
                  field="cantidaddias"
                  header="Días"
                  sortField="cantidaddias"
                  filterField="cantidaddias"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Cantidad de días</span>
                    {{ slotProps.data.cantidaddias }}
                  </template>
                </Column>

                <!-- tipo  -->
                <Column
                  field="indtiporegistro"
                  sortField="indtiporegistro"
                  header="Tipo"
                  filterField="indtiporegistro"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:12%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Tipo</span>
                    {{ slotProps.data.indtiporegistro }}
                  </template>
                </Column>

                <!-- Estado  -->
                <Column
                  field="estado"
                  header="Estado"
                  sortField="estado"
                  filterField="estado"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:8%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Estado</span>
                    {{ slotProps.data.estado }}
                  </template>
                </Column>

                <!-- referencia  -->
                <Column
                  field="referencia"
                  sortField="referencia"
                  header="Referencia"
                  filterField="referencia"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:15%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Referencia</span>
                    {{ slotProps.data.referencia }}
                  </template>
                </Column>

                <!-- <Column
                  field="idsolicitudmasterdata"
                  sortField="idsolicitudmasterdata"
                  header="# Solicitud"
                  filterField="idsolicitudmasterdata"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title"># de Solicitud</span>
                    {{ slotProps.data.idsolicitudmasterdata }}
                  </template>
                </Column> -->
              </DataTable>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    </div>
  </div>
</template>

<script>
import { GetIncapacidadesPorPerfil } from "./request";
import moment from "moment";
import DateRangePicker from "vue2-daterange-picker";
import "vue2-daterange-picker/dist/vue2-daterange-picker.css";
moment.locale("es");

import Vue from "vue";
import XLSX from "sheetjs-style";

export default {
  name: "ConsultaIncapacidad",
  components: {
    DateRangePicker
  },
  data() {
    return {
      incapacidades: [],
      // incapacidadSeleccionado: [],
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
    // se cargarian los request en caso de ser necesario o en created...
    this.initRanges();
    await this.cargarTabla();
    const id = this.$root.infoColaboradorActual.id;

    setTimeout(
      () => (
        (this.loading = false),
        (this.incapacidades = this.incapacidades.filter(function(item) {
          return item.idcolaborador == id;
        }))
      ),
      500
    );
  },
  methods: {
    pintaEstado(estado, style) {
      if (style) {
        switch (estado) {
          case "AN":
            return "badge rounded-pill bg-danger p-component";

          default:
            break;
        }
      } else {
        switch (estado) {
          case "AN":
            return "Anulada";
          case "AP":
            return "Aplicada";

          default:
            return "Por definir";
        }
      }
    },

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
        data: { incapacidades }
      } = await GetIncapacidadesPorPerfil(datos);
      this.incapacidades = incapacidades.map(
        ({
          idincapacidad,
          idcolaborador,
          fechainicio,
          fechafinal,
          cantidaddias,
          colaboradorNombre,
          identificacionColaborador,
          numboleta,
          idcargaincapacidades,
          indtiporegistro,
          idsolicitudmasterdata,
          estado
          // boleta
        }) => ({
          idincapacidad: idincapacidad,
          idcolaborador: idcolaborador,
          fechainicio: moment(fechainicio).format("L"),
          fechafinal: moment(fechafinal).format("L"),
          cantidaddias: cantidaddias,
          colaboradorNombre: colaboradorNombre,
          identificacionColaborador: identificacionColaborador,
          numboleta: numboleta,
          idcargaincapacidades: idcargaincapacidades,
          indtiporegistro: this.SetTipoCarga(indtiporegistro),
          idsolicitudmasterdata: idsolicitudmasterdata,
          fechaInicioSinFormato: fechainicio,
          fechaFinSinFormato: fechafinal,
          referencia:
            indtiporegistro == "C"
              ? idcargaincapacidades
              : idsolicitudmasterdata,
          estado: this.pintaEstado(estado, false)
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

        this.incapacidades = this.incapacidades.filter(function(item) {
          return item.idcolaborador == $this.$root.infoColaboradorActual.id;
        });
      }
    },
    puedeMostrarPropias() {
      return (
        this.$root.userRoles.some(p => p == "Expediente.DM") ||
        this.$root.userRoles.some(p => p == "Expediente.RRHH") ||
        this.$root.userRoles.some(p => p == "Expediente.Financiero")
      );
    },
    SetTipoCarga(tipo) {
      let res = "";

      switch (tipo) {
        case "C":
          res = "Carga";

          break;
        case "M":
          res = "Manual";

          break;

        default:
          res = "Sin definir";

          break;
      }

      return res;
    },
    async actualizarRango(rango) {
      await this.cargarTabla();
      if (this.showPropias) {
        const $this = this;

        this.incapacidades = this.incapacidades.filter(function(item) {
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
      var data = this.incapacidades.map(
        ({
          fechaInicioSinFormato,
          fechaFinSinFormato,
          cantidaddias,
          colaboradorNombre,
          identificacionColaborador,
          idSolicitud,
          numboleta,
          indtiporegistro,
          referencia,
          estado
        }) => ({
          Identificacion: identificacionColaborador,
          Nombre_Completo: colaboradorNombre,
          Fecha_Inicio: moment(fechaInicioSinFormato).format("YYYY/MM/DD"),
          Fecha_Fin: moment(fechaFinSinFormato).format("YYYY/MM/DD"),
          Cantidad_Dias: cantidaddias,
          ID_Solicitud: idSolicitud,
          Numero_Boleta: numboleta,
          Tipo_Registro: indtiporegistro,
          Referencia: referencia,
          Estado: estado
        })
      );

      const wb = XLSX.utils.book_new();
      const ws = XLSX.utils.json_to_sheet(data);
      XLSX.utils.book_append_sheet(wb, ws, "Incapacidades.csv");
      XLSX.writeFile(wb, `Incapacidades_${this.getDateTime()}.csv`);
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
        nombre = "Mis incapacidades";
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
</style>
