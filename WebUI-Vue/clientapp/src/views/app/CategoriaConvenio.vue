<template>
  <div>
    <div class="wrapper vld-parent">
      <CCard
        style="border-radius: 16px !important; margin-top: 20px; padding: 6px"
      >
        <strong>Categorias de convenios</strong>
        <small>Listado de categorias de convenios</small>
      </CCard>

      <CRow>
        <CCol md="6" sm="6" class="mb-2">
          <div class="table-header">
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
            @click="createCategory()"
            class="
              mt-1
              ml-3
              btn btn-info
              pull-right
              libro-marca-celeste
              onHoverDark
            "
            v-tooltip.top="'Permite agregar una nueva categoría.'"
            >Agregar Categoría</CButton
          >
        </CCol>
      </CRow>
      <CRow>
        <CCol lg="12">
          <CCard>
            <CRow class="mt-1"> </CRow>
            <CCardBody>
              <DataTable
                ref="dt"
                :value="categories"
                :paginator="true"
                class="p-datatable-consultas p-datatable-striped p-datatable-sm"
                :rows="10"
                dataKey="idConvenioCategoria"
                :rowHover="true"
                :selection.sync="selectedCategory"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} categorias de convenios"
                sortField="nombre"
                :sortOrder="1"
                stateKey="dt-state-categorias-convenios"
                selectionMode="single"
                :autoLayout="true"
              >
                <template #empty> No se encontraron categorias. </template>
                <template #loading>
                  Cargando la información de categorias. Por favor espere.
                </template>
                <Column
                  field="nombre"
                  header="Nombre"
                  sortField="nombre"
                  filterField="nombre"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width: 25%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Nombre</span>
                    <a
                      href=""
                      style="cursor: hand"
                      @click.prevent="editCategory(slotProps.data)"
                      >{{ slotProps.data.nombre }}</a
                    >
                  </template>
                </Column>
                <Column
                  field="descripcion"
                  header="Descripción"
                  :sortable="true"
                  sortField="descripcion"
                  filterField="descripcion"
                  filterMatchMode="contains"
                  headerStyle="width:65%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Descripción</span>
                    {{ slotProps.data.descripcion }}
                  </template>
                </Column>

                <Column
                  header="Acciones"
                  :sortable="false"
                  headerStyle="width:10%;"
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
                      <CDropdownItem @click="viewCategory(slotProps.data)">
                        <CIcon name="cil-magnifying-glass" />&nbsp; Ver
                      </CDropdownItem>
                      <!-- <CDropdownItem @click="deleteCategory(slotProps.data)">
                        <CIcon name="cil-delete" />&nbsp; Eliminar
                      </CDropdownItem> -->
                    </CDropdown>
                  </template>
                </Column>
              </DataTable>

              <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>

              <Dialog
                :visible.sync="showAddModal"
                :style="{ width: '70vw' }"
                :modal="true"
              >
                <template #header>
                  <h5>{{ tituloModalEdicionComputed }}</h5>
                </template>

                <FormulateForm
                  name="form"
                  v-model="newCategory"
                  @submit="submitForm"
                  @failed-validation="validationError"
                  :invalid-message="this.invalidMessage"
                >
                  <div style="background-color: transparent">
                    <FormulateErrors />
                    <CRow class="mt-3">
                      <CCol sm="12">
                        <FormulateInput
                          type="text"
                          name="nombre"
                          placeholder="Nombre de la categoría"
                          label="Nombre de la categoría"
                          validation="required|max:100,length"
                          :disabled="isDisabled"
                          :validation-messages="{
                            required: 'Ingrese un nombre para la categoría',
                          }"
                          :input-class="['form-control']"
                        />
                      </CCol>
                      <CCol sm="12">
                        <FormulateInput
                          name="descripcion"
                          type="textarea"
                          rows="3"
                          label="Ingrese la descripción de la categoría"
                          placeholder="Descripción"
                          validation="required|max:500,length"
                          :disabled="isDisabled"
                          :validation-messages="{
                            required: 'Ingrese una descripción de categoría ',
                          }"
                          :input-class="['form-control']"
                        />
                      </CCol>
                    </CRow>
                    <CRow>
                      <CCol sm="12">
                        <FormulateInput
                          type="button"
                          label="Cancelar"
                          style="display: inline"
                          v-show="tipoModal !== 'Ver'"
                          @click="showAddModal = false"
                          :input-class="[
                            'btn',
                            'btn-info',
                            'pull-right',
                            'libro-marca-azuloscuro',
                            'ml-2',
                          ]"
                        />
                        <FormulateInput
                          type="submit"
                          label="Guardar"
                          style="display: inline"
                          v-show="tipoModal !== 'Ver'"
                          :input-class="[
                            'btn',
                            'btn-info',
                            'pull-right',
                            'libro-marca-azuloscuro',
                          ]"
                        />
                      </CCol>
                    </CRow>
                  </div>
                </FormulateForm>
              </Dialog>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    </div>
  </div>
</template>


<script>
import Vue from "vue";
import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import common from "../masterdata/common.js";
import { GetConveniosCategorias, UpsertConveniosCategoriasCommand, DeleteConveniosCategorias } from "./request";

export default {
  name: "categoriaConvenio",
  mixins: [common],
  components: { ConfirmDialogue },
  data() {
    return {
      categories: [],
      selectedCategory: null,
      showAddModal: false,
      isEdit: false,
      newCategory: {
        idColaborador: null,
        id: null,
        nombre: null,
        descripcion: null,
        esEditar: false,
      },
      loading: true,
      filters: {},
      tituloModalEdicion: "",
      tipoModal: "",
    };
  },
  async mounted() {
    this.loading = false;
    await this.cargarTabla();
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
      Vue.$toast.warning(common.MENSAJE_ERROR, {
        // optional options Object
      });
    },
    async cargarTabla() {
      const {
        data: { conveniosCategorias },
      } = await GetConveniosCategorias();

      this.categories = conveniosCategorias.map(
        ({ nombre, descripcion, idConvenioCategoria }) => ({
          idConvenioCategoria: idConvenioCategoria,
          nombre: nombre,
          descripcion: descripcion,
        })
      );
    },
    createCategory() {
      this.tituloModalEdicion = "Agregar nueva categoría de convenios";
      this.tipoModal = "Agregar";
      this.showAddModal = true;
      this.isEdit = false;
      this.newCategory = {
        id: null,
        nombre: null,
        descripcion: null,
        idColaborador: this.$root.infoColaboradorActual.id,
      };
    },

    async editCategory(item, _x, _event) {
      this.tituloModalEdicion = "Editar categoría de convenios";
      this.tipoModal = "Editar";
      this.isEdit = true;
      this.selectedCategory = item;
      this.newCategory.id = item.idConvenioCategoria;
      this.newCategory.nombre = item.nombre;
      this.newCategory.descripcion = item.descripcion;
      this.newCategory.idColaborador = this.$root.infoColaboradorActual.id;

      this.showAddModal = true;
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    async deleteCategory(item, _x, _event) {
      const id = item.idConvenioCategoria;
      const res = await this.Confirmacion_Generico(
        `Desea eliminar la categoría ${item.nombre}`,
        "Eliminar Categoría"
      );
      if (res) {
        await DeleteConveniosCategorias(id)
          .then(() => {
            Vue.$toast.success(
              "La categoría de convenio fue eliminada exitosamente.",
              {}
            );
          })
          .catch((response) => {
            console.log(response.Error);
            Vue.$toast.error("La categoría de convenio no pudo eliminarse.");
          });
        await this.cargarTabla();
      } else {
        // mostrar error
      }
    },

    async viewCategory(item, _x, _event) {
      this.selectedCategory = item;
      this.newCategory.nombre = item.nombre;
      this.newCategory.descripcion = item.descripcion;
      this.tipoModal = "Ver";
      this.showAddModal = true;
    },
    async submitForm() {
      await UpsertConveniosCategoriasCommand({
        idColaborador: this.newCategory.idColaborador,
        id: this.newCategory.id,
        nombre: this.newCategory.nombre,
        descripcion: this.newCategory.descripcion,
        esEditar: this.isEdit,
      })
        .then(() => {
          Vue.$toast.success(
            "La categoría de convenios fue creada exitosamente."
          );
        })
        .then(() => {
          setTimeout(async () => {
            await this.cargarTabla();
          }, 1000);
        })
        .catch(() => {
          Vue.$toast.error("La categoría de convenios no se pudo crear.", {});
        });
      this.showAddModal = false;
    },
  },
  computed: {
    tituloModalEdicionComputed() {
      let res = this.tituloModalEdicion;
      if (this.tipoModal === "Ver") {
        res = "Ver categoría de convenios";
      }
      return res;
    },
    isDisabled() {
      if (this.tipoModal === "Ver") {
        return true;
      }
      return false;
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