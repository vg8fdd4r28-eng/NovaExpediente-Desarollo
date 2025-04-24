<template>
  <div>
    <DataTable
      ref="dt"
      :value="listado"
      :paginator="true"
      class="p-datatable-consultas p-datatable-striped p-datatable-sm"
      :rows="10"
      dataKey="idcurriculumvitae"
      :rowHover="true"
      :selection.sync="selectedCurriculumVitae"
      :filters.sync="filters"
      :loading="loading"
      paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
      :rowsPerPageOptions="[5, 10, 25, 50]"
      currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} currículums"
      sortField="nombrearchivo"
      :sortOrder="1"
      stateKey="dt-state-curriculum-vitae"
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
              class="
                pull-right
                btn btn-info
                libro-marca-celete
                onHoverDark
                ml-1
              "
              @click="create"
            >
              Crear Currículum
            </CButton>
          </CCol>
        </CRow>
      </template>
      <template #empty> No se encontraron currículums. </template>
      <template #loading>
        Cargando la información de currículums. Por favor espere.
      </template>
      <Column
        field="idcurriculumvitae"
        header="ID"
        :sortable="true"
        sortField="idcurriculumvitae"
        filterField="idcurriculumvitae"
        filterMatchMode="contains"
        headerStyle="width: 10%"
      >
        <template #body="slotProps">
          <span class="p-column-title">ID</span>
          {{ slotProps.data.idcurriculumvitae }}
        </template>
      </Column>

      <Column
        field="nombrearchivo"
        header="Nombre del archivo"
        :sortable="true"
        sortField="nombrearchivo"
        filterField="nombrearchivo"
        filterMatchMode="contains"
        headerStyle="width: 30%"
      >
        <template #body="slotProps">
          <span class="p-column-title">Nombre del archivo</span>
          <a
            href=""
            style="cursor: pointer"
            @click.prevent="edit(slotProps.data)"
          >{{ slotProps.data.nombrearchivo }}</a>
        </template>
      </Column>

      <Column
        field="fecha"
        header="Fecha"
        :sortable="true"
        sortField="fecha"
        filterField="fecha"
        filterMatchMode="contains"
        headerStyle="width: 20%"
      >
        <template #body="slotProps">
          <span class="p-column-title">Fecha</span>
          {{ new Date(slotProps.data.fecha).toLocaleString() }}
        </template>
      </Column>

      <Column
        field="estado"
        header="Estado"
        :sortable="true"
        sortField="estado"
        filterField="estadp"
        filterMatchMode="contains"
        headerStyle="width: 20%"
      >
        <template #body="slotProps">
          <span class="p-column-title">Estado</span>
          {{ slotProps.data.estado }}
        </template>
      </Column>

    </DataTable>
    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
  </div>
</template>

<script>
import Vue from "vue";
import common from "../../masterdata/common.js";
import ConfirmDialogue from "../../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import { GetCurriculumsVitae } from "../request";

export default {
  name: "ListaCurriculumVitae",
  mixins: [common],
  components: { ConfirmDialogue },
  emits: ["view-new", "create-new", "edit-new"],
  data() {
    return {
      listado: [],
      selectedCurriculumVitae: null,
      dt: null,
      showAddModal: false,
      loading: true,
      collapse: false,
      btnFilter: {
        text: "Ocultar Filtros"
      },
      filters: {}
    };
  },
  async mounted() {
    await this.loadTable();
    this.loading = false;
  },
  methods: {
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        fieldNames += `-  ${fields[key].label}\r\n`;
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    validationError() {
      Vue.$toast.warning(common.MENSAJE_ERROR, {});
    },
    async loadTable() {
      const idColaborador = this.$root.infoColaboradorActual.id;
      try {
        const {
          data: { curriculumVitaes }
        } = await GetCurriculumsVitae(idColaborador);

        this.listado = curriculumVitaes.map(
          ({
            idcurriculumvitae,
            nombrearchivo,
            fecha,
            mensaje,
            estado,
            uuid,
          }) => ({
            idcurriculumvitae: idcurriculumvitae,
            nombrearchivo: nombrearchivo,
            fecha: fecha,
            mensaje: mensaje,
            estado: estado,
            uuid: uuid
          })
        );
      } catch (error) {
        console.error("Error loading curriculum vitae:", error);
      }
    },
    filterApplied(_e) {},
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
    clearFilters() {
      this.filters = {};
      this.selectedCurriculumVitae = null;
    },
    toggleFilters() {
      this.collapse = !this.collapse;
      this.btnFilter.text = this.collapse
        ? "Mostrar Filtros"
        : "Ocultar Filtros";
    },
    // eslint-disable-next-line no-unused-vars
    view(item, _x, _event) {
      this.selectedCurriculumVitae = item;
      this.$emit("view", this.selectedCurriculumVitae.idcurriculumvitae);
    },
    // eslint-disable-next-line no-unused-vars
    edit(item, _x, _event) {
      this.selectedCurriculumVitae = item;
      this.$emit("edit", this.selectedCurriculumVitae);
    },
    create() {
      this.$emit("create");
    },
  },
  computed: {}
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
