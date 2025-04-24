<template>
  <div>
    <CRow>
      <CCol md="12" sm="12" class="pull-right mb-1">
        <CButton
          color="primary"
          class="pull-right btn btn-info libro-marca-celeste ml-1"
          @click="toggleFilters"
        >
          <i class="pi pi-filter" />
          {{ btnFilter.text }}
        </CButton>
        <CButton
          color="primary"
          class="pull-right btn btn-info libro-marca-celete onHoverDark ml-1"
          @click="clearFilters"
        >
          <i class="pi pi-trash" />
          Limpiar filtros
        </CButton>
      </CCol>
    </CRow>
    <DataTable
      ref="dt"
      :value="sorteos"
      :paginator="true"
      class="p-datatable-consultas p-datatable-striped p-datatable-sm"
      :rows="10"
      dataKey="idSorteo"
      :rowHover="true"
      :selection.sync="selectedSorteo"
      :filters.sync="filters"
      :loading="loading"
      paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
      :rowsPerPageOptions="[5, 10, 25, 50]"
      currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} sorteos"
      sortField="idSorteo"
      :sortOrder="1"
      stateKey="dt-state-sorteos"
      selectionMode="single"
      :autoLayout="true"
      @update:filters="filterApplied"
      @filter="filterApplied"
    >
      <template #header>
        <CCollapse :show="!collapse">
          <CCard color="light">
            <CCardBody class="collapse--card">
              <CRow>
                <CCol sm="4">
                  <div class="p-field p-col p-me-4 p--div">
                    <span class="p-float-label">
                      <MultiSelect
                        v-model="filters['activo']"
                        :options="sorteoTypes"
                        optionLabel="name"
                        optionValue="value"
                        class="p-column-filter w-100"
                        display="chip"
                        :filter="true"
                      />
                      <label for="dropdown">Estado</label>
                    </span>
                  </div>
                </CCol>
                <CCol sm="4" />
                <CCol sm="4" />
              </CRow>
            </CCardBody>
          </CCard>
        </CCollapse>
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
              class="pull-right btn btn-info libro-marca-celeste ml-1"
              @click="createSorteo"
              >Crear Sorteo</CButton
            >
          </CCol>
        </CRow>
      </template>
      <template #empty> No se encontraron sorteos. </template>
      <template #loading>
        Cargando la información de sorteos. Por favor espere.
      </template>
      <Column
        field="nombre"
        header="Nombre"
        :sortable="true"
        sortField="nombre"
        filterField="nombre"
        filterMatchMode="contains"
        headerStyle="width: 40%"
      >
        <template #body="slotProps">
          <span class="p-column-title">Descripción</span>
          <a
            href=""
            style="cursor: pointer"
            @click.prevent="editSorteo(slotProps.data)"
            >{{ slotProps.data.nombre }}</a
          >
        </template>
      </Column>
      <Column
        field="descripcion"
        header="Descripcion"
        :sortable="true"
        sortField="nombre"
        filterField="nombre"
        filterMatchMode="contains"
        headerStyle="width: 25%"
      >
        <template #body="slotProps">
          <span class="p-column-title">Descripción</span>
          {{ slotProps.data.descripcion }}
        </template>
      </Column>
      <Column
        field="fechaVencimiento"
        header="Vence"
        :sortable="true"
        sortField="fechaVencimiento"
        filterField="fechaVencimiento"
        filterMatchMode="contains"
        headerStyle="width: 25%"
      >
        <template #body="slotProps">
          <span class="p-column-title">Descripción</span>
          {{ slotProps.data.fechaVencimiento }}
        </template>
      </Column>
      <Column
        header="Estado"
        filterField="activo"
        filterMatchMode="custom"
        :filterFunction="customFilter"
        headerStyle="display: none"
        bodyStyle="display: none"
      />
      <Column header="Acciones" :sortable="false" headerStyle="width: 10%">
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
            <CDropdownItem @click="viewSorteo(slotProps.data)">
              <CIcon name="cil-magnifying-glass" />&nbsp; Ver
            </CDropdownItem>
            <CDropdownItem @click="deleteSorteo(slotProps.data)">
              <CIcon name="cil-delete" />&nbsp; Eliminar
            </CDropdownItem>
          </CDropdown>
        </template>
      </Column>
    </DataTable>
    <confirm-dialogue ref="confirmDialogue" />
  </div>
</template>

<script>
import Vue from "vue";
import common from "../../masterdata/common.js";
import ConfirmDialogue from "../../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import { GetListaSorteos, DeleteSorteo } from "../request";
import moment from "moment";

export default {
  name: "listasorteos",
  mixins: [common],
  components: { ConfirmDialogue },
  data() {
    return {
      sorteos: [],
      dt: null,
      selectedSorteo: null,
      collapse: false,
      loading: true,
      filters: { 0:"Activo"},
      sorteoTypes: [
        { name: "Activo", value: "Activo" },
        { name: "Inactivo", value: "Inactivo" },
      ],
      btnFilter: {
        text: "Ocultar Filtros",
      },
    };
  },
  async mounted() {
    this.loadTable();
    this.loading = false;
  },
  methods: {
    async loadTable() {
      const {
        data: { sorteos },
      } = await GetListaSorteos();
      this.sorteos = sorteos.map(
        ({ idSorteo, nombre, descripcion, activo, fechaVencimiento }) => ({
          idSorteo: idSorteo,
          nombre: nombre,
          descripcion: descripcion,
          activo: activo ? "Activo" : "Inactivo",
          fechaVencimiento: moment(fechaVencimiento)
            .locale("es")
            .format("DD MMMM YYYY"),
        })
      );
    },
    createSorteo() {
      this.$emit("create-sorteo");
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    editSorteo(item, _x, _event) {
      this.selectedSorteo = item;
      this.$emit("edit-sorteo", this.selectedSorteo.idSorteo);
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    viewSorteo(item, _x, _event) {
      this.selectedSorteo = item;
      this.$emit("view-sorteo", this.selectedSorteo.idSorteo);
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    async deleteSorteo(item, _x, _event) {
      const idSorteo = item.idSorteo;
      const res = await this.Confirmacion_Generico(
        `Desea eliminar el sorteo ${item.nombre}, esta acción no puede deshacerse.`,
        "Eliminar Sorteo"
      );
      res
        ? await DeleteSorteo(idSorteo)
            .then((response) => {
              response.data === "OK"
                ? Vue.$toast.success("El sorteo se eliminó correctamente.")
                : Vue.$toast.error("El sorteo no pudo eliminarse");
            })
            .catch((response) => {
              console.error(response.Error);
              Vue.$toast.error("El sorteo no pudo eliminarse");
            })
        : null;
      await this.loadTable();
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
      return false;
    },
    toggleFilters() {
      this.collapse = !this.collapse;
      this.btnFilter.text = this.collapse
        ? "Mostrar Filtros"
        : "Ocultar Filtros";
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    filterApplied(_e) {},
    clearFilters() {
      this.filters = {};
      this.selectedSorteo = null;
    },
  },
};
</script>

<style scoped>
.collapse--card {
  padding-top: 15px !important;
  padding-bottom: 0 !important;
}
.p--div {
  padding: 10px 5px 0 5px !important;
}
</style>

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

.p-datatable {
  background-color: white;
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

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de etiqueta y valor asociado */

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
</style>