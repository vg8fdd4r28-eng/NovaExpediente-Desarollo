<template>
  <div>
    <div class="wrapper vld-parent">
      <CRow>
        <CCol lg="12">
          <CCard>
            <CCardHeader>
              <CIcon name="cil-justify-center" /><strong> Clientes</strong>
              <small> Lista de clientes</small>
            </CCardHeader>
            <CCardBody>
              <DataTable
                ref="dt"
                :value="clientes"
                :paginator="true"
                class="p-datatable-consultas p-datatable-striped p-datatable-sm"
                :rows="10"
                dataKey="idcliente"
                :rowHover="true"
                :selection.sync="clienteSeleccionado"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} clientes"
                sortField="nombre"
                :sortOrder="1"
                stateKey="dt-state-cliente"
                selectionMode="single"
                :autoLayout="true"
              >
                <template #header>
                  <!--<CRow>
                  <CCol md="3" sm="12" class="mb-2">
                    <div style="text-align: left">
                      <CButton
                        id="btnExportarColaboradoresConsulta"
                        block
                        color="primary"
                        @click="exportCSV($event)"
                        class="mr-1 btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark w-100 btn-primary"
                      >
                        <CIcon
                          name="cil-file"
                        />&nbsp;Exportar</CButton
                      >
                    </div>
                  </CCol>
                  <CCol md="3" sm="12" class="mb-2">
                    <div style="text-align: left">
                      <CButton
                        id="btnExportarColaboradoresConsulta"
                        block
                        color="primary"
                        @click="exportSelectedCSV($event)"
                        class="mr-1 btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark w-100 btn-primary"
                      >
                        <CIcon name="cil-file" />&nbsp;Exportar
                        seleccionados</CButton
                      >
                    </div>
                  </CCol>
                </CRow>-->
                  <CRow>
                    <CCol md="8" sm="12" class="mb-2">
                      <div class="table-header ">
                        Buscar
                        <span class="p-input-icon-left w-75">
                          <i class="pi pi-search" />
                          <InputText
                            v-model="filters['global']"
                            placeholder="escriba un texto"
                            class="w-100"
                          />
                        </span>
                      </div>
                    </CCol>
                  </CRow>
                </template>
                <template #empty>
                  No se encontraron clientes.
                </template>
                <template #loading>
                  Cargando la información de clientes. Por favor espere.
                </template>
                <Column
                  field="Nombre"
                  header="Nombre"
                  sortField="nombre"
                  filterField="nombre"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:40%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Nombre</span>

                    <a
                      href=""
                      style="cursor: hand;"
                      @click.prevent="Ver(slotProps.data)"
                    >
                      {{ slotProps.data.nombre }}</a
                    >
                  </template>
                </Column>

                <Column
                  field="nombrePais"
                  header="Pais"
                  :sortable="true"
                  sortField="nombrePais"
                  filterField="nombrePais"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Pais</span>
                    {{ slotProps.data.nombrePais }}
                  </template>
                </Column>

                <Column
                  field="nombreEmpresaCorporacion"
                  header="Empresa"
                  :sortable="true"
                  sortField="nombreEmpresaCorporacion"
                  filterField="nombreEmpresaCorporacion"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Empresa</span>
                    {{ slotProps.data.nombreEmpresaCorporacion }}
                  </template>
                </Column>

                <Column
                  field="nombreSector"
                  header="Sector"
                  :sortable="true"
                  sortField="nombreSector"
                  filterField="nombreSector"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Sector</span>
                    {{ slotProps.data.nombreSector }}
                  </template>
                </Column>

                <Column
                  field="descripcion"
                  header="Descripción"
                  :sortable="true"
                  sortField="descripcion"
                  filterField="descripcion"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Descripción</span>
                    {{ slotProps.data.descripcion }}
                  </template>
                </Column>
              </DataTable>

              <Dialog
                :visible.sync="showModalAgregar"
                :style="{ width: '70vw' }"
                :maximizable="true"
                :modal="true"
              >
                <template #header>
                  <h5>Detalle del cliente</h5>
                </template>

                <FormulateForm
                  name="formulario"
                  v-model="clienteDetalle"
                  @submit="submit"
                >
                  <div class="mt-3">
                    <!-- TABS para separar datos generales del cliente y datos de las referencias -->
                    <TabView>
                      <!-- clientes  -->
                      <TabPanel header="Datos generales">
                        <FormulateErrors />
                        <CRow>
                          <CCol sm="4">
                            <FormulateInput
                              name="idempresacorporacion"
                              label="Empresa corporación"
                              :input-class="['form-control']"
                              :disabled="true"
                            />
                          </CCol>
                          <CCol sm="4">
                            <FormulateInput
                              name="nombre"
                              label="Nombre del cliente"
                              input-class="form-control"
                              :readonly="true"
                            />
                          </CCol>

                          <CCol sm="4">
                            <FormulateInput
                              name="codigocliente"
                              label="Código del cliente"
                              input-class="form-control"
                              :readonly="true"
                            />
                          </CCol>
                        </CRow>

                        <CRow>
                          <CCol sm="4">
                            <FormulateInput
                              name="cedulaJuridica"
                              label="Cédula jurídica"
                              input-class="form-control"
                              :readonly="true"
                            />
                          </CCol>
                          <CCol sm="4">
                            <FormulateInput
                              type="tel"
                              name="telefono"
                              label="Teléfono"
                              :input-class="['form-control']"
                              :readonly="true"
                            />
                          </CCol>
                          <CCol sm="4">
                            <FormulateInput
                              type="email"
                              name="correo"
                              label="Correo"
                              :input-class="['form-control']"
                              :help-class="['hint']"
                              :readonly="true"
                            />
                          </CCol>
                        </CRow>
                        <CRow>
                          <CCol sm="4">
                            <FormulateInput
                              name="idpais"
                              label="País"
                              :input-class="['form-control']"
                              :disabled="true"
                            />
                          </CCol>
                          <CCol sm="4">
                            <FormulateInput
                              name="idsector"
                              label="Sector"
                              :input-class="['form-control']"
                              :disabled="true"
                            />
                          </CCol>
                          <CCol sm="4"> </CCol>
                        </CRow>
                        <CRow>
                          <CCol sm="4">
                            <label>Puede utilizarse como referencia?</label>
                            <FormulateInput
                              type="myInputSwitch"
                              name="esreferencia"
                              :input-class="['form-control', 'readonly']"
                              class="hint"
                              :disabled="true"
                            />

                            <label>Activo</label>
                            <FormulateInput
                              type="myInputSwitch"
                              name="activo"
                              :input-class="['form-control', 'readonly']"
                              class="hint"
                              :disabled="true"
                            />
                          </CCol>
                          <CCol sm="8">
                            <FormulateInput
                              type="textarea"
                              name="descripcion"
                              label="Descripción"
                              :input-class="['form-control']"
                              rows="4"
                              :help-class="['hint']"
                              :readonly="true"
                            />
                          </CCol>
                        </CRow>
                        <FormulateErrors />
                      </TabPanel>

                      <!-- referencias  -->
                      <TabPanel header="Referencias">
                        <CRow>
                          <CCol md="12" sm="12" class="mb-2">
                            <div class="table-header ">
                              <span class="p-input-icon-left pull-right">
                                <i class="pi pi-search" />
                                <InputText
                                  v-model="filtersReferencias['global']"
                                  class="w-100"
                                />
                              </span>
                            </div>
                          </CCol>

                          <CCol sm="12" class="mb-2">
                            <DataTable
                              ref="dt"
                              :value="clienteDetalle.referencias"
                              :paginator="true"
                              class="p-datatable-cliente p-datatable-striped p-datatable-sm"
                              :rows="5"
                              dataKey="idreferencia"
                              :selection.sync="clienteReferenciaSeleccionado"
                              :filters.sync="filtersReferencias"
                              :loading="loadingReferencias"
                              paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                              :rowsPerPageOptions="[5, 10, 25, 50]"
                              currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} de registros"
                              sortField="nombre"
                              :sortOrder="1"
                              stateKey="dt-state-clienteReferencia"
                              selectionMode="single"
                              :autoLayout="true"
                            >
                              <template #header> </template>
                              <template #empty>
                                No se encontraron registros.
                              </template>
                              <template #loading>
                                Cargando la información de selección dinámica.
                                Por favor espere.
                              </template>

                              <Column
                                header="Identificación"
                                :sortable="true"
                                sortField="identificacion"
                                filterField="identificacion"
                                filterMatchMode="contains"
                              >
                                <template #body="slotProps">
                                  {{ slotProps.data.identificacion }}
                                </template>
                              </Column>

                              <Column
                                header="Referencia"
                                :sortable="true"
                                sortField="nombre"
                                filterField="nombre"
                                filterMatchMode="contains"
                              >
                                <template #body="slotProps">
                                  <a
                                    href=""
                                    style="cursor: hand;"
                                    @click.prevent="
                                      VerReferencia(slotProps.data)
                                    "
                                  >
                                    {{ slotProps.data.nombre }}</a
                                  >
                                </template>
                              </Column>

                              <Column
                                header="Puesto"
                                :sortable="true"
                                sortField="puesto"
                                filterField="puesto"
                                filterMatchMode="contains"
                              >
                                <template #body="slotProps">
                                  {{ slotProps.data.puesto }}
                                </template>
                              </Column>

                              <Column
                                header="Activo"
                                :sortable="true"
                                sortField="activo"
                                filterField="activo"
                                filterMatchMode="contains"
                              >
                                <template #body="slotProps">
                                  {{
                                    slotProps.data.activo === true ? "SI" : "NO"
                                  }}
                                </template>
                              </Column>
                            </DataTable>
                          </CCol>
                        </CRow>
                      </TabPanel>
                    </TabView>
                  </div>
                </FormulateForm>

                <template #footer>
                  <div>
                    <CRow>
                      <CCol sm="12">
                        <FormulateInput
                          type="button"
                          label="Cancelar"
                          style="display:inline;"
                          @click="showModalAgregar = false"
                          :input-class="[
                            'btn',
                            'btn-info',
                            'pull-right',
                            'libro-marca-azuloscuro',
                            'ml-2'
                          ]"
                        />
                      </CCol>
                    </CRow>
                  </div>
                </template>
              </Dialog>

              <Dialog
                :visible.sync="showModalAgregarReferencia"
                :style="{ width: '60vw' }"
                :maximizable="true"
                :modal="true"
              >
                <template #header>
                  <h5>Detalle de la referencia</h5>
                </template>
                <FormulateForm
                  name="formularioReferencia"
                  v-model="referenciaDetalle"
                  @submit="submitReferencia"
                >
                  <div class="mt-3">
                    <FormulateErrors />
                    <CRow>
                      <CCol sm="6">
                        <FormulateInput
                          name="identificacion"
                          label="Identificación"
                          input-class="form-control"
                          :readonly="true"
                        />
                      </CCol>
                      <CCol sm="6">
                        <FormulateInput
                          name="nombre"
                          label="Nombre"
                          input-class="form-control"
                          :readonly="true"
                        />
                      </CCol>
                    </CRow>

                    <CRow>
                      <CCol sm="6">
                        <FormulateInput
                          name="correo"
                          label="Correo"
                          input-class="form-control"
                          :help-class="['hint']"
                          :readonly="true"
                        />
                      </CCol>
                      <CCol sm="6">
                        <FormulateInput
                          type="tel"
                          name="telefono"
                          label="Teléfono"
                          input-class="form-control"
                          :readonly="true"
                        />
                      </CCol>
                    </CRow>
                    <CRow>
                      <CCol sm="6">
                        <FormulateInput
                          name="extension"
                          label="Extensión"
                          input-class="form-control"
                          :readonly="true"
                        />
                      </CCol>
                      <CCol sm="6">
                        <FormulateInput
                          type="tel"
                          name="celular"
                          label="Celular"
                          input-class="form-control"
                          :help-class="['hint']"
                          :readonly="true"
                        />
                      </CCol>
                    </CRow>
                    <CRow>
                      <CCol sm="6">
                        <FormulateInput
                          name="puesto"
                          label="Puesto"
                          :input-class="['form-control', 'mt-2']"
                          :readonly="true"
                        />
                      </CCol>
                      <CCol sm="6">
                        <label>Activa</label>
                        <FormulateInput
                          type="myInputSwitch"
                          name="activo"
                          input-class="form-control"
                          class="hint mt-1"
                          :disabled="true"
                        />
                      </CCol>
                    </CRow>
                  </div>
                </FormulateForm>
                <template #footer>
                  <CRow>
                    <CCol sm="12">
                      <FormulateInput
                        type="button"
                        label="Cancelar"
                        style="display:inline;"
                        @click="showModalAgregarReferencia = false"
                        :input-class="[
                          'btn',
                          'btn-info',
                          'pull-right',
                          'libro-marca-azuloscuro',
                          'ml-2'
                        ]"
                      />
                    </CCol>
                  </CRow>
                </template>
              </Dialog>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    </div>
  </div>
</template>

<script>
import { GetClientesReferencias } from "./request";

export default {
  name: "Cliente",
  data() {
    return {
      clientes: [],
      clienteSeleccionado: [],
      clienteReferenciaSeleccionado: null,
      loading: true,
      loadingReferencias: false,
      filters: {},
      filtersReferencias: {},
      clienteDetalle: {
        idcliente: null,
        nombre: "",
        cedulaJuridica: "",
        descripcion: "",
        telefono: "",
        correo: "",
        esreferencia: "Si",
        idpais: null,
        idsector: null,
        idempresacorporacion: null,
        codigocliente: "",
        activo: "Si",
        referencias: []
      },
      referenciaDetalle: {
        idreferencia: null,
        idcliente: null,
        identificacion: "",
        nombre: "",
        correo: "",
        telefono: "",
        extension: "",
        celular: "",
        puesto: "",
        activo: "Si"
      },
      showModalAgregar: false,
      showModalAgregarReferencia: false
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...
    this.loading = false;
    await this.cargarTabla();
  },
  methods: {
    async cargarTabla() {
      const {
        data: { clientesReferencias }
      } = await GetClientesReferencias();

      if (clientesReferencias.length == 0) {
        this.solicitud.clientesList = [];
        Vue.$toast.warning("No existen clientes ingresados.");
      } else {
        this.clientes = clientesReferencias.map(
          ({
            idcliente,
            idsector,
            idpais,
            nombre,
            descripcion,
            referencias,
            codigocliente,
            cedulajuridica,
            correo,
            telefono,
            esreferencia,
            idempresacorporacion,
            activo,
            nombrePais,
            nombreSector,
            nombreEmpresaCorporacion
          }) => ({
            idcliente: idcliente,
            idsector: idsector,
            idpais: idpais,
            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion,
            referencias: referencias || [],
            codigocliente: codigocliente,
            cedulaJuridica: cedulajuridica,
            correo: correo,
            telefono: telefono,
            esreferencia: esreferencia,
            idempresacorporacion: idempresacorporacion,
            activo: activo,
            nombrePais,
            nombreSector,
            nombreEmpresaCorporacion
          })
        );
      }
    },
    async Ver(item) {
      this.clienteSeleccionado = item;
      this.showModalAgregar = true;

      this.clienteDetalle = {
        id: item.id,
        idcliente: item.idcliente,
        nombre: item.nombre,
        cedulaJuridica: item.cedulaJuridica,
        descripcion: item.descripcion,
        telefono: item.telefono,
        correo: item.correo,
        esreferencia: item.esreferencia == true ? "Si" : "No",
        idpais: item.nombrePais,
        idsector: item.nombreSector,
        estado: item.estado,
        idempresacorporacion: item.nombreEmpresaCorporacion,
        codigocliente: item.codigocliente,
        activo: item.activo == true ? "Si" : "No",
        referencias: item.referencias
      };

      this.referencias = item.referencias;
    },
    async VerReferencia(item) {
      this.referenciaDetalle.identificacion = item.identificacion;
      this.referenciaDetalle.nombre = item.nombre;
      this.referenciaDetalle.correo = item.correo;
      this.referenciaDetalle.telefono = item.telefono;
      this.referenciaDetalle.extension = item.extension;
      this.referenciaDetalle.celular = item.celular;
      this.referenciaDetalle.puesto = item.puesto;
      this.referenciaDetalle.activo = item.activo == true ? "Si" : "No";
      this.referenciaDetalle.idcliente = item.idcliente;
      this.referenciaDetalle.idreferencia = item.idreferencia;
      this.referenciaDetalle.estado = item.estado;
      this.showModalAgregarReferencia = true;
    },
    submit() {},
    submitReferencia() {}
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
