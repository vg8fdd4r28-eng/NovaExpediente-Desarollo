<template>
  <div>
    <div class="wrapper vld-parent">
      <CRow>
        <CCol lg="12">
          <CCard>
            <CCardHeader>
              <CIcon name="cil-justify-center" /><strong>
                Consulta de saldo de colaboradores</strong
              >
              <small> Lista de saldos </small>
            </CCardHeader>
            <CCardBody>
              <DataTable
                ref="dt"
                :value="saldos"
                :paginator="true"
                class="p-datatable-consultas p-datatable-striped p-datatable-sm"
                :rows="10"
                dataKey="identificacion"
                :rowHover="true"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} saldos"
                sortField="nombre"
                :sortOrder="1"
                stateKey="dt-state-saldo"
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
                    <CCol md="4" sm="12" class="mb-2"> </CCol>
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
                    <CCol md="3" sm="12" class="mb-2 pull-right" style="display: block;">
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
                      Mi saldo
                    </CBadge>
                    <CBadge
                      v-show="!showPropias"
                      color="info"
                      class="bg-warning mr-2"
                    >
                      Saldos de mis consultores
                    </CBadge>
                  </div>
                </template>
                <template #empty>
                  No se encontraron saldos.
                </template>
                <template #loading>
                  Cargando la información de saldos. Por favor espere.
                </template>

                <!-- Identificación  -->
                <Column
                  field="identificacion"
                  header="Identificación"
                  sortField="identificacion"
                  filterField="identificacion"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:12%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Identificación</span>
                    {{ slotProps.data.identificacion }}
                  </template>
                </Column>

                <!-- Nombre  -->
                <Column
                  field="nombreCompleto"
                  header="Nombre del colaborador"
                  sortField="nombreCompleto"
                  filterField="nombreCompleto"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:20%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Nombre del colaborador</span>
                    {{ slotProps.data.nombreCompleto }}
                  </template>
                </Column>

                <Column
                  field="saldo"
                  header="Saldo de vacaciones"
                  sortField="saldo"
                  filterField="saldo"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="text-align: center;width:10%;"
                  bodyStyle="text-align: center"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Saldo de vacaciones</span>
                    {{ formatNumber(slotProps.data.saldo) }}
                  </template>
                </Column>
              </DataTable>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    </div>
  </div>
</template>

<script>
import { GetColaboradorVacacionesConsulta } from "./request";

import Vue from "vue";
import XLSX from "sheetjs-style";

export default {
  name: "ConsultaSaldoVacacionesColaboradores",
  components: {},
  data() {
    return {
      saldos: [],
      saldoSeleccionado: [],
      loading: true,
      filters: {},
      showPropias: true
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...

    const id = this.$root.infoColaboradorActual.id;
    await this.cargarTabla();
    this.loading = false;
  },
  methods: {
     formatNumber(value) {
            return value.toLocaleString(undefined, {minimumFractionDigits: 2});
        },
    async cargarTabla() {
      const id = this.$root.infoColaboradorActual.id;
      const {
        data: { colaboradorVacaciones }
      } = await GetColaboradorVacacionesConsulta(id);
      this.saldos = colaboradorVacaciones.map(
        ({
          identificacion,
          idcolaborador,
          idnomina,
          nombreCompleto,
          vacacionesDisponibles,
          saldovacacionesactual 
        }) => ({
          identificacion: identificacion,
          idcolaborador: idcolaborador,
          idnomina: idnomina,
          nombreCompleto: nombreCompleto,
          vacacionesDisponibles: vacacionesDisponibles,
          saldovacacionesactual: saldovacacionesactual,
          saldo: saldovacacionesactual
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

        this.saldos = this.saldos.filter(function(item) {
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
    async exportar() {
      var data = this.saldos.map(
        ({ identificacion, idnomina, nombreCompleto, saldo }) => ({
          Identificacion: identificacion,
          Nombre_Completo: nombreCompleto,
          En_Nomina: idnomina == null ? "NO" : "SI",
          Dias_Saldo: saldo
        })
      );

      const wb = XLSX.utils.book_new();
      const ws = XLSX.utils.json_to_sheet(data);
      XLSX.utils.book_append_sheet(wb, ws, "SaldoVacaciones.csv");
      XLSX.writeFile(wb, `SaldoVacaciones_${this.getDateTime()}.csv`);
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
          nombre = "Mis consultores";
        }
      } else {
        nombre = "Mi saldo";
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
