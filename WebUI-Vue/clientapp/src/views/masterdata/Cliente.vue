<template>
  <div>
    <CCard>
      <CCardHeader>
        <CIcon name="cil-justify-center" /><strong> Clientes</strong>
        <small> Solicitud de cambio </small>
        <CBadge
          color="primary"
          class="mr-2"
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
        <div class="card-header-actions">
          <CButton
            block
            color="primary"
            @click="atras()"
            class="mr-1 libro-marca-azuloscuro"
          >
            <i class="pi pi-arrow-circle-left"></i>&nbsp;Regresar a la
            lista</CButton
          >
        </div>
      </CCardHeader>

      <CCardBody>
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

        <h2 class="text-divider"></h2>

        <CRow class="mt-1">
          <CCol md="9" sm="12" class="mb-2">
            <CButton
              class="mt-1 mr-1 btn btn-info mt-1 pull-left libro-marca-celeste onHoverDark "
              @click="Agregar()"
              v-if="this.puedePintar"
              v-tooltip.top="'Permite agregar un nuevo cliente.'"
            >
              <li class="pi pi-plus ml-1"></li>
              Agregar cliente
            </CButton>
            <CButton
              class="mt-1 btn btn-info pull-left libro-marca-celeste onHoverDark "
              @click="displayModal('Clientes')"
              v-if="this.puedePintar"
              v-tooltip.top="
                'Permite seleccionar un cliente ya registrado para aplicar cambios'
              "
            >
              <li class="pi pi-copy ml-1"></li>
              Incluir cliente
            </CButton>
          </CCol>

          <CCol md="3" sm="12" class="mb-2">
            <div class="table-header ">
              <span class="p-input-icon-left pull-right">
                <i class="pi pi-search" />
                <InputText v-model="filters['global']" class="w-100" />
              </span>
            </div>
          </CCol>
        </CRow>

        <FormulateForm name="solicitud" @submit="submitSolicitud">
          <CRow>
            <CCol lg="12">
              <DataTable
                ref="dt"
                :value="solicitud.clientesList"
                :paginator="true"
                class="p-datatable-cliente p-datatable-striped p-datatable-sm mb-4"
                :rows="5"
                dataKey="id"
                :selection.sync="clienteSeleccionado"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} de registros"
                sortField="nombre"
                :sortOrder="1"
                stateKey="dt-state-cliente"
                selectionMode="single"
                :autoLayout="true"
              >
                <template #header> </template>
                <template #empty>
                  No se encontraron registros.
                </template>
                <template #loading>
                  Cargando la información de selección dinámica. Por favor
                  espere.
                </template>

                <!-- nombre  -->
                <Column
                  header="Cliente"
                  :sortable="true"
                  sortField="nombre"
                  filterField="nombre"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Cliente</span>
                    <a
                      href=""
                      style="cursor: hand;"
                      @click.prevent="Editar(slotProps.data)"
                    >
                      {{ slotProps.data.nombre }}</a
                    >
                  </template>
                </Column>
                <!-- descripcion  -->
                <Column
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
                <!-- estado  -->
                <Column
                  header="Estado"
                  :sortable="true"
                  sortField="estado"
                  filterField="estado"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Estado</span>
                    <span :class="pintaEstado(slotProps.data.estado, true)">
                      {{ pintaEstado(slotProps.data.estado, false) }}
                    </span>
                  </template>
                </Column>
                <!-- acciones  -->
                <Column
                  header=""
                  :sortable="false"
                  sortField="accion"
                  filterField="accion"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title"></span>

                    <div
                      v-if="
                        slotProps.data.estado !== 'N' &&
                          slotProps.data.estado !== 'E'
                      "
                      style="display:inline;"
                    >
                      <a
                        v-if="puedePintar"
                        class="pi pi-times-circle ml-1 iconExclude"
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Excluir(slotProps.data)"
                        title="Excluir cliente de la lista"
                      ></a>
                    </div>
                    <div
                      v-if="slotProps.data.estado !== 'E'"
                      style="display:inline;"
                    >
                      <a
                        v-if="puedePintar"
                        class="pi pi-trash ml-1 iconTrash"
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Eliminar(slotProps.data)"
                        title="Eliminar cliente"
                      ></a>
                    </div>
                    <div v-else>
                      <a
                        v-if="puedePintar"
                        class="pi pi-refresh ml-1 iconUndo"
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Undo(slotProps.data)"
                        title="Recuperar cliente eliminado"
                      ></a>
                    </div>
                  </template>
                </Column>
              </DataTable>

              <h2
                class="text-divider"
                style="margin-top:35px;"
                v-if="this.comentarios.length > 0"
              >
                <span>Comentarios</span>
              </h2>
              <comments
                :comments_wrapper_classes="[
                  'custom-scrollbar',
                  'comments-wrapper'
                ]"
                :comments="this.comentarios"
              ></comments>

              <botonera-master-data
                :estado-solicitud="this.datosSolicitud.estadoSolicitud"
                nombreEventoProcesar="procesarAccion"
                @procesarAccion="procesarAccion"
                v-if="muestraBotonera()"
              >
              </botonera-master-data>
            </CCol>
          </CRow>
        </FormulateForm>

        <Dialog
          :visible.sync="showModalAgregar"
          :style="{ width: '70vw' }"
          :maximizable="true"
          :modal="true"
        >
          <template #header>
            <h5>{{ tituloModalEdicionComputed }}</h5>
          </template>

          <FormulateForm
            name="formulario"
            v-model="clienteNuevoEditar"
            @submit="submit"
            @failed-validation="errorValidacion"
            :invalid-message="this.invalidMessage"
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
                        :options="empresaCorpList"
                        type="select"
                        label="Empresa corporación"
                        placeholder="Seleccione una empresa"
                        validation="required|not:0"
                        validation-name="Empresa corporación"
                        :validation-messages="{
                          required: 'Seleccione una empresa.',
                          not: 'Debe de seleccionar una opción válida.'
                        }"
                        :input-class="['form-control']"
                        :disabled="!puedePintar"
                      />
                    </CCol>
                    <CCol sm="4">
                      <FormulateInput
                        name="nombre"
                        label="Nombre del cliente"
                        placeholder="Ingrese el nombre del cliente"
                        validation="required|max:100,length"
                        input-class="form-control"
                        validation-name="Nombre del cliente"
                        :readonly="!puedePintar"
                      />
                    </CCol>

                    <CCol sm="4">
                      <FormulateInput
                        name="codigocliente"
                        label="Código del cliente"
                        placeholder="Ingrese la código del cliente"
                        validation="required|max:50,length"
                        input-class="form-control"
                        validation-name="Código del cliente"
                        :readonly="!puedePintar"
                      />
                    </CCol>
                  </CRow>

                  <CRow>
                    <CCol sm="4">
                      <FormulateInput
                        name="cedulaJuridica"
                        label="Cédula jurídica"
                        placeholder="Ingrese la cédula jurídica del cliente"
                        input-class="form-control"
                        validation-name="Cédula jurídica"
                        help="0-000-000000"
                        validation="optional|matches:/[A-Za-z0-9]{1}-[A-Za-z0-9]{3}-[A-Za-z0-9]{6}/|max:13,length"
                        :help-class="['hint']"
                        :readonly="!puedePintar"
                      />
                    </CCol>
                    <CCol sm="4">
                      <FormulateInput
                        type="tel"
                        name="telefono"
                        label="Teléfono"
                        placeholder="Ingrese el número de teléfono"
                        help="Formatos: +00 00000000 o +000 00000000"
                        :validation="[
                          ['optional'],
                          ['matches', /^\+(?:[0-9] ?){6,14}[0-9]$/]
                        ]"
                        validation-name="Teléfono"
                        :input-class="['form-control']"
                        :help-class="['hint']"
                        :readonly="!puedePintar"
                      />
                    </CCol>
                    <CCol sm="4">
                      <FormulateInput
                        type="email"
                        name="correo"
                        label="Correo"
                        placeholder="Ingrese el correo"
                        help="correo@dominio.ext"
                        validation="optional|email"
                        validation-name="Correo"
                        :input-class="['form-control']"
                        :help-class="['hint']"
                        :readonly="!puedePintar"
                      />
                    </CCol>
                  </CRow>
                  <CRow>
                    <CCol sm="4">
                      <FormulateInput
                        name="idpais"
                        :options="paisList"
                        type="select"
                        label="País"
                        placeholder="Seleccione un país"
                        validation="required|not:0"
                        validation-name="País"
                        :validation-messages="{
                          required: 'Seleccione un país.',
                          not: 'Debe de seleccionar una opción válida.'
                        }"
                        :input-class="['form-control']"
                        :disabled="!puedePintar"
                      />
                    </CCol>
                    <CCol sm="4">
                      <FormulateInput
                        name="idsector"
                        :options="sectorList"
                        type="select"
                        label="Sector"
                        placeholder="Seleccione un sector"
                        validation="required|not:0"
                        validation-name="Sector"
                        :validation-messages="{
                          required: 'Seleccione un sector.',
                          not: 'Debe de seleccionar una opción válida.'
                        }"
                        :input-class="['form-control']"
                        :disabled="!puedePintar"
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
                        validation-name="Referencia"
                        class="hint"
                        :disabled="!puedePintar || eliminada"
                      />

                      <label>Activo</label>
                      <FormulateInput
                        type="myInputSwitch"
                        name="activo"
                        :input-class="['form-control', 'readonly']"
                        validation-name="Referencia"
                        class="hint"
                        :disabled="!puedePintar || eliminada"
                      />
                    </CCol>
                    <CCol sm="8">
                      <FormulateInput
                        type="textarea"
                        name="descripcion"
                        label="Descripción"
                        validation="max:8000,length"
                        validation-name="Descripción"
                        :input-class="['form-control']"
                        :help="
                          `Tiene disponible un máximo de ${8000 -
                            descripcionComputed} carácteres.`
                        "
                        rows="4"
                        :help-class="['hint']"
                        :readonly="!puedePintar"
                      />
                    </CCol>
                  </CRow>
                  <FormulateErrors />
                </TabPanel>

                <!-- referencias  -->
                <TabPanel header="Referencias">
                  <CRow>
                    <CCol md="3" sm="12" class="mb-2 mt-1">
                      <CButton
                        class="mt-1 btn btn-info mt-1 pull-left libro-marca-celeste onHoverDark w-100"
                        @click="AgregarReferencia()"
                        v-if="this.puedePintar && puedePintarAdd"
                      >
                        <li class="pi pi-plus"></li>
                        Agregar referencia
                      </CButton>
                    </CCol>

                    <CCol md="9" sm="12" class="mb-2">
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
                        :value="referencias"
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
                          Cargando la información de selección dinámica. Por
                          favor espere.
                        </template>

                        <Column
                          header="Identificación"
                          :sortable="true"
                          sortField="identificacion"
                          filterField="identificacion"
                          filterMatchMode="contains"
                        >
                          <template #body="slotProps">
                            <span class="p-column-title">Identificación</span>
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
                            <span class="p-column-title">Referencia</span>
                            <a
                              href=""
                              style="cursor: hand;"
                              @click.prevent="EditarReferencia(slotProps.data)"
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
                            <span class="p-column-title">Puesto</span>
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
                            <span class="p-column-title">Activo</span>
                            {{ slotProps.data.activo === true ? "SI" : "NO" }}
                          </template>
                        </Column>

                        <Column
                          header="Estado"
                          :sortable="true"
                          sortField="estado"
                          filterField="estado"
                          filterMatchMode="contains"
                        >
                          <template #body="slotProps">
                            <span class="p-column-title">Estado</span>
                            <span
                              :class="pintaEstado(slotProps.data.estado, true)"
                            >
                              {{ pintaEstado(slotProps.data.estado, false) }}
                            </span>
                          </template>
                        </Column>

                        <Column
                          header=""
                          :sortable="false"
                          sortField="accion"
                          filterField="accion"
                          filterMatchMode="contains"
                        >
                          <template #body="slotProps">
                            <span class="p-column-title"></span>

                            <div v-if="slotProps.data.estado !== 'E'">
                              <a
                                v-if="puedePintar"
                                class="pi pi-trash ml-1 iconTrash"
                                href=""
                                style="cursor: hand;"
                                @click.prevent="
                                  EliminarReferencia(slotProps.data)
                                "
                                title="Eliminar referencia"
                              ></a>
                            </div>
                            <div v-else>
                              <a
                                v-if="puedePintar"
                                class="pi pi-refresh ml-1 iconUndo"
                                href=""
                                style="cursor: hand;"
                                @click.prevent="UndoReferencia(slotProps.data)"
                                title="Recuperar referencia eliminada"
                              ></a>
                            </div>
                          </template>
                        </Column>
                      </DataTable>
                    </CCol>
                  </CRow>
                </TabPanel>
              </TabView>

              <Dialog
                header="Eliminar referencia"
                :visible.sync="confirmacionEliminarReferencia"
                :style="{ width: '30vw' }"
                :modal="true"
              >
                <div class="confirmation-content mt-2">
                  <CRow>
                    <CCol col="2">
                      <i
                        class="pi pi-exclamation-triangle p-mr-3"
                        style="font-size: 2rem"
                      />
                    </CCol>
                    <CCol col="10">
                      <span>{{
                        `Está seguro que desea eliminar la referencia ` +
                          this.referenciaEliminar +
                          ` ?`
                      }}</span>
                    </CCol>
                  </CRow>
                </div>
                <template #footer>
                  <CButton
                    class="mt-1 btn btn-info mt-1 pull-right libro-marca-celeste onHoverDark"
                    type="button"
                    @click="aceptarConfirmacionRef()"
                  >
                    Aceptar
                  </CButton>
                  <CButton
                    class="mt-1 btn btn-info mt-1 pull-right libro-marca-celeste onHoverDark"
                    type="button"
                    @click="cancelarConfirmacionRef()"
                  >
                    Cancelar
                  </CButton>
                </template>
              </Dialog>
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
                  <FormulateInput
                    type="submit"
                    @click="guardar()"
                    label="Guardar"
                    style="display:inline;"
                    :input-class="[
                      'btn',
                      'btn-info',
                      'pull-right',
                      'libro-marca-azuloscuro'
                    ]"
                    v-if="puedePintar && !eliminada"
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
            <h5>{{ tituloModalEdicionReferencia }}</h5>
          </template>
          <FormulateForm
            name="formularioReferencia"
            v-model="referenciaNuevaEditar"
            @submit="submitReferencia"
            @failed-validation="errorValidacionRef"
            :invalid-message="this.invalidMessageRef"
          >
            <div class="mt-3">
              <FormulateErrors />
              <CRow>
                <CCol sm="6">
                  <FormulateInput
                    name="identificacion"
                    label="Identificación"
                    validation="max:20,length|required"
                    input-class="form-control"
                    validation-name="Identificación"
                    placeholder="Ingrese la identificación"
                    :readonly="!puedePintar"
                  />
                </CCol>
                <CCol sm="6">
                  <FormulateInput
                    name="nombre"
                    label="Nombre"
                    validation="max:100,length|required"
                    input-class="form-control"
                    validation-name="Nombre de la referencia"
                    placeholder="Ingrese el nombre"
                    :readonly="!puedePintar"
                  />
                </CCol>
              </CRow>

              <CRow>
                <CCol sm="6">
                  <FormulateInput
                    name="correo"
                    label="Correo"
                    input-class="form-control"
                    validation-name="Correo de la referencia"
                    placeholder="Ingrese el correo"
                    help="correo@dominio.ext"
                    validation="email|max:100,length|required"
                    :help-class="['hint']"
                    :readonly="!puedePintar"
                  />
                </CCol>
                <CCol sm="6">
                  <FormulateInput
                    type="tel"
                    name="telefono"
                    label="Teléfono"
                    input-class="form-control"
                    validation-name="Teléfono"
                    help="Formatos: +00 00000000 o +000 00000000"
                    :validation="[
                      ['optional'],
                      ['matches', /^\+(?:[0-9] ?){6,14}[0-9]$/]
                    ]"
                    :help-class="['hint']"
                    placeholder="Ingrese el teléfono"
                    :readonly="!puedePintar"
                  />
                </CCol>
              </CRow>
              <CRow>
                <CCol sm="6">
                  <FormulateInput
                    name="extension"
                    validation="max:10,length"
                    label="Extensión"
                    input-class="form-control"
                    validation-name="Extensión"
                    placeholder="Ingrese la extensión"
                    :readonly="!puedePintar"
                  />
                </CCol>
                <CCol sm="6">
                  <FormulateInput
                    type="tel"
                    name="celular"
                    label="Celular"
                    input-class="form-control"
                    validation-name="Celular"
                    help="Formatos: +00 00000000 o +000 00000000"
                    :validation="[
                      ['optional'],
                      ['matches', /^\+(?:[0-9] ?){6,14}[0-9]$/]
                    ]"
                    :help-class="['hint']"
                    placeholder="Ingrese el celular"
                    :readonly="!puedePintar"
                  />
                </CCol>
              </CRow>
              <CRow>
                <CCol sm="6">
                  <FormulateInput
                    name="puesto"
                    validation="max:100,length"
                    label="Puesto"
                    :input-class="['form-control', 'mt-2']"
                    validation-name="Puesto"
                    placeholder="Ingrese el puesto"
                    :readonly="!puedePintar"
                  />
                </CCol>
                <CCol sm="6">
                  <label>Activa</label>
                  <FormulateInput
                    type="myInputSwitch"
                    name="activo"
                    input-class="form-control"
                    validation-name="Referencia activo"
                    class="hint mt-1"
                    :disabled="!puedePintar || eliminadaRef"
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
                <FormulateInput
                  type="submit"
                  @click="guardarReferencia()"
                  label="Guardar"
                  style="display:inline;"
                  :input-class="[
                    'btn',
                    'btn-info',
                    'pull-right',
                    'libro-marca-azuloscuro'
                  ]"
                  v-if="puedePintar && !eliminadaRef"
                />
              </CCol>
            </CRow>
          </template>
        </Dialog>
      </CCardBody>
    </CCard>

    <!-- Diálogo para los PICKLIST -->
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
    <comment-dialogue ref="commentDialogue"></comment-dialogue>
    <visor-documentos
      :src="srcDocumento"
      :showModalVisorAdjuntos.sync="showModalVisorAdjuntos"
      @showModalVisor="showModalVisorDocumentos"
    >
    </visor-documentos>
  </div>
</template>

<script>
import {
  GetAllColaboradores,
  GetClientesReferencias,
  GetSectores,
  GetAllPaises,
  GetColaboradorDetailResumen,
  GetDetalleSolicitud,
  GetEmpresaCorporacion,
  ValidaClienteDependencias
} from "./request";
import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "./ResumenColaborador";
import BotoneraMasterData from "./BotoneraMasterData.vue";
import VisorDocumentos from "../generales/Componentes/VisorDocumentos.vue";
import _ from "lodash";
import Vue from "vue";

// métodos comunes
import common from "./common.js";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";
import Comments from "../generales/Componentes/Comments/Comments.vue";

export default {
  name: "Cliente",
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
    "botonera-master-data": BotoneraMasterData,
    "confirm-dialogue": ConfirmDialogue,
    "comment-dialogue": CommentDialogue,
    "visor-documentos": VisorDocumentos,
    comments: Comments
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
      clienteNuevoEditar: {
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
        estado: "S",
        referencias: []
      },
      solicitud: {
        estado: "S",
        clientesList: []
      },
      referencias: [],

      referenciaNuevaEditar: {
        idreferencia: null,
        idcliente: null,
        identificacion: "",
        nombre: "",
        correo: "",
        telefono: "",
        extension: "",
        celular: "",
        puesto: "",
        activo: "Si",
        estado: "S"
      },
      comentarios: [],
      datosSolicitud: {
        idSolicitudMasterData: null,
        idTipoSolicitud: this.idTipoSolicitud,
        tipoSolicitud: this.tipoSolicitud,
        estadoSolicitud: "RE"
      },
      showModal: false,
      nombreTipoLista: "",
      columnasPickList: [],
      dataPickList: [],
      filters: {},
      loading: false,
      clienteSeleccionado: [],
      showModalAgregar: false,
      showModalConfirmacion: false,
      clienteEliminar: "",
      tituloModalEdicion: "",
      TipoListaPick: "",
      paisList: [],
      sectorList: [],
      empresaCorpList: [],
      optionsReferencias: ["Si", "No"],
      optionsReferenciaActivo: ["Si", "No"],
      clienteReferenciasList: [],
      clienteReferenciaSeleccionado: [],
      filtersReferencias: {},
      loadingReferencias: false,
      showModalAgregarReferencia: false,
      showModalConfirmacionReferencia: false,
      referenciaEliminar: "",
      tituloModalEdicionReferencia: "",
      esEdicionReferencia: false,
      cargandoDatos: false,
      IdSolicitudJson: null,
      showModalVisorNormal: false,
      esEdicion: false,
      visorEnable: false,
      tituloModal: "",
      srcDocumento: "",
      showModalVisorAdjuntos: false,
      confirmacionEliminarReferencia: false
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...
    await this.cargarPais();
    // await this.cargarClientes();
    await this.cargarSectores();
    await this.cargarEmpresasCorporacion();

    this.IdSolicitudJson = this.idSolicitud;

    if (this.IdSolicitudJson) {
      this.cargarSolicitud();
    } else {
      //carga el usuario en session
      this.TipoListaPick = "Colaboradores";
      this.selected(this.$root.infoColaboradorActual);
    }
  },
  methods: {
    guardar(e) {
      this.$formulate.submit("formulario");
    },
    guardarReferencia(e) {
      this.$formulate.submit("formularioReferencia");
    },
    showModalVisorPreview(data) {
      this.srcDocumento = data.src;
      this.showModalVisorAdjuntos = data.show;
      this.visorEnable = data.show;
    },
    showModalVisorDocumentos(show) {
      this.showModalVisorAdjuntos = show;
    },
    showModalVisor(show) {
      this.visorEnable = show;
    },
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        fieldNames += `-  ${fields[key].label}\r\n`;
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    errorValidacion(data) {
      Vue.$toast.warning(common.MENSAJE_ERROR, {
        // optional options Object
      });
    },
    invalidMessageRef(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        fieldNames += `-  ${fields[key].label}\r\n`;
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    errorValidacionRef(data) {
      Vue.$toast.warning(common.MENSAJE_ERROR, {
        // optional options Object
      });
    },
    obtenerDescripcionEstado(codigoEstadoSolicitud) {
      switch (codigoEstadoSolicitud) {
        case common.REGISTRADA:
          return "Registrada";
        case common.APROBADA:
          return "Aprobada";
        case common.DENEGADA:
          return "Denegada";
        case common.DESCARTADA:
          return "Descartada";
        case common.ENAPROBACION:
          return "En aprobación";
        case common.CORREGIR:
          return "En correcciones";
        case common.ENVALIDACION:
          return "En validación";
        case common.PENDIENTEACTUALIZACION:
          return "Pendiente de actualización";
        default:
          break;
      }
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
            estado: colaborador.estado,
            clientesList: colaborador.clientesList
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
         let loader = this.showLoader();

      GetDetalleSolicitud(this.IdSolicitudJson)
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
            this.hideLoader(loader);
        })
        .catch(response => {
          console.log(response);
          this.terminaCargaDatos();
            this.hideLoader(loader);
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

    RemoveReferencia() {
      this.referencias = this.referencias.filter(
        x => x.idreferencia != this.clienteReferenciaSeleccionado.idreferencia
      );
      this.showModalConfirmacionReferencia = false;
    },
    Close() {
      this.showModalConfirmacion = false;
    },
    CloseConfirmacionReferencia() {
      this.showModalConfirmacionReferencia = false;
    },
    async Eliminar(item) {
      this.clienteSeleccionado = item;
      this.clienteEliminar = this.clienteSeleccionado.nombre;

      if (await this.ValidaDependenciasEliminar()) {
        const res = await this.Eliminar_Generico(
          "cliente",
          this.clienteEliminar,
          "M"
        );

        const foundIndex = this.solicitud.clientesList.findIndex(
          x => x.id == item.id
        );
        if (res) {
          if (item.estado === "N") {
            this.solicitud.clientesList = this.solicitud.clientesList.filter(
              x => x.id !== item.id
            );
            this.solicitud.clientesList = this.addIdentityToArrayObjects(
              this.solicitud.clientesList
            );

            Vue.$toast.success("El cliente fue eliminado exitosamente.", {});
          } else {
            this.solicitud.clientesList[foundIndex].estado = "E";

            Vue.$toast.success("El cliente fue eliminado exitosamente.", {});
            this.solicitud.estado = "M";
          }
        }
      }
    },

    async Excluir(item) {
      this.clienteSeleccionado = item;
      this.clienteEliminar = this.clienteSeleccionado.nombre;

      const res = await this.Excluir_Generico(
        "cliente",
        this.clienteEliminar,
        "M"
      );

      if (res) {
        this.solicitud.clientesList = this.solicitud.clientesList.filter(
          x => x.id !== item.id
        );
        this.solicitud.clientesList = this.addIdentityToArrayObjects(
          this.solicitud.clientesList
        );
        Vue.$toast.success("El cliente fue eliminado exitosamente.", {});
      }
    },

    async EliminarReferencia(item) {
      this.clienteReferenciaSeleccionado = item;
      this.referenciaEliminar = this.clienteReferenciaSeleccionado.nombre;
      this.confirmacionEliminarReferencia = true;
    },
    aceptarConfirmacionRef() {
      const foundIndex = this.referencias.findIndex(
        x => x.idreferencia == this.clienteReferenciaSeleccionado.idreferencia
      );

      if (this.clienteReferenciaSeleccionado.estado === "N") {
        this.referencias = this.referencias.filter(
          x =>
            x.idreferencia !== this.clienteReferenciaSeleccionado.idreferencia
        );
        Vue.$toast.success("La referencia fue eliminada exitosamente.", {});
        this.confirmacionEliminarReferencia = false;
      } else {
        this.referencias[foundIndex].estado = "E";
        Vue.$toast.success("La referencia fue eliminada exitosamente.", {});
        this.solicitud.estado = "M";
        this.confirmacionEliminarReferencia = false;
      }
      this.clienteNuevoEditar.referencias = this.referencias;
    },
    cancelarConfirmacionRef() {
      this.confirmacionEliminarReferencia = false;
    },

    Agregar() {
      this.esEdicion = false;
      this.tituloModalEdicion = "Agregar cliente";
      this.cleanForm();
      this.showModalAgregar = true;
    },
    Undo(item) {
      const foundIndex = this.solicitud.clientesList.findIndex(
        x => x.id == item.id
      );

      if (
        this.solicitud.clientesList[foundIndex].hashCode ==
        this.hashCode(
          this.clienteSeleccionado.idpais.toString() +
            this.clienteSeleccionado.idsector.toString() +
            this.clienteSeleccionado.idempresacorporacion.toString() +
            this.clienteSeleccionado.nombre +
            this.clienteSeleccionado.cedulaJuridica +
            this.clienteSeleccionado.descripcion +
            this.clienteSeleccionado.telefono +
            this.clienteSeleccionado.correo +
            this.clienteSeleccionado.esreferencia +
            this.clienteSeleccionado.codigocliente +
            this.clienteSeleccionado.activo +
            JSON.stringify(this.clienteSeleccionado.referencias)
        )
      ) {
        this.solicitud.clientesList[foundIndex].estado = "S";
      } else {
        this.solicitud.clientesList[foundIndex].estado = "M";
      }

      // this.solicitud.clientesList[foundIndex].estado = "S";
    },
    UndoReferencia(item) {
      const foundIndex = this.referencias.findIndex(
        x => x.idreferencia == item.idreferencia
      );
      this.referencias[foundIndex].estado = "S";
    },
    CancelarAgregar() {
      this.showModalAgregar = false;
      this.referencias = [];
    },
    AgregarReferencia() {
      this.cleanFormReferencias();
      this.esEdicionReferencia = false;
      this.showModalAgregarReferencia = true;
      // this.showModalAgregar = false;
      this.tituloModalEdicionReferencia = "Agregar referencia";
    },
    submitReferencia() {
      if (this.esEdicionReferencia) {
        this.AceptarEditarReferencia();
        this.showModalAgregarReferencia = false;
      } else {
        this.referencias.push({
          idreferencia: this.getNuevoReferenciaId(),
          idcliente: this.clienteSeleccionado.idcliente, // en una nueva aun no existe
          identificacion: this.referenciaNuevaEditar.identificacion,
          nombre: this.referenciaNuevaEditar.nombre,
          correo: this.referenciaNuevaEditar.correo,
          telefono: this.referenciaNuevaEditar.telefono,
          extension: this.referenciaNuevaEditar.extension,
          celular: this.referenciaNuevaEditar.celular,
          puesto: this.referenciaNuevaEditar.puesto,
          activo: this.referenciaNuevaEditar.activo == "Si" ? true : false,
          estado: "N"
        });

        this.showModalAgregarReferencia = false;
        this.showModalAgregar = true;
      }
      this.clienteNuevoEditar.referencias = this.referencias;
    },
    AceptarEditarReferencia() {
      const foundIndex = this.referencias.findIndex(
        x => x.idreferencia == this.referenciaNuevaEditar.idreferencia
      );

      this.referencias[
        foundIndex
      ].identificacion = this.referenciaNuevaEditar.identificacion;
      this.referencias[foundIndex].nombre = this.referenciaNuevaEditar.nombre;
      this.referencias[foundIndex].correo = this.referenciaNuevaEditar.correo;
      this.referencias[
        foundIndex
      ].telefono = this.referenciaNuevaEditar.telefono;
      this.referencias[
        foundIndex
      ].extension = this.referenciaNuevaEditar.extension;
      this.referencias[foundIndex].celular = this.referenciaNuevaEditar.celular;
      this.referencias[foundIndex].puesto = this.referenciaNuevaEditar.puesto;
      this.referencias[foundIndex].activo =
        this.referenciaNuevaEditar.activo == "Si" ? true : false;

      if (this.referencias[foundIndex].estado != "N") {
        this.referencias[foundIndex].estado = "M";
      }
    },
    CancelarAgregarReferencia() {
      this.showModalAgregarReferencia = false;
      this.showModalAgregar = true;
    },
    async Editar(item) {
      this.esEdicion = true;
      this.tituloModalEdicion = "Modificar cliente";
      this.clienteSeleccionado = item;
      this.showModalAgregar = true;

      this.clienteNuevoEditar = {
        id: item.id,
        idcliente: item.idcliente,
        nombre: item.nombre,
        cedulaJuridica: item.cedulaJuridica,
        descripcion: item.descripcion,
        telefono: item.telefono,
        correo: item.correo,
        esreferencia: item.esreferencia,
        idpais: item.idpais,
        idsector: item.idsector,
        estado: item.estado,
        idempresacorporacion: item.idempresacorporacion,
        codigocliente: item.codigocliente,
        activo: item.activo,
        referencias: item.referencias
      };

      this.referencias = item.referencias;
    },
    async EditarReferencia(item) {
      //change assign to = {object}
      this.esEdicionReferencia = true;
      this.tituloModalEdicionReferencia = "Modificar referencia";
      this.referenciaNuevaEditar.identificacion = item.identificacion;
      this.referenciaNuevaEditar.nombre = item.nombre;
      this.referenciaNuevaEditar.correo = item.correo;
      this.referenciaNuevaEditar.telefono = item.telefono;
      this.referenciaNuevaEditar.extension = item.extension;
      this.referenciaNuevaEditar.celular = item.celular;
      this.referenciaNuevaEditar.puesto = item.puesto;
      this.referenciaNuevaEditar.activo = item.activo == true ? "Si" : "No";
      this.referenciaNuevaEditar.idcliente = item.idcliente;
      this.referenciaNuevaEditar.idreferencia = item.idreferencia;
      this.referenciaNuevaEditar.estado = item.estado;
      this.showModalAgregarReferencia = true;
      // this.showModalAgregar = false;
    },

    async submit(formValues) {
      try {
        if (!this.visorEnable) {
          let resultadoValidacion = this.validacionesSolicitud();

          if (resultadoValidacion.esValido) {
            // actualiza la lista interna de CLIENTE, previamente validada
            if (this.actualizarLista()) {
              this.showModalAgregar = false;
            }
          } else {
            Vue.$toast.warning(resultadoValidacion.mensaje, {});
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    actualizarLista() {
      try {
        if (this.esEdicion) {
          this.editarListaClientes();
        } else {
          this.agregarListaClientes();
        }
        // this.cleanForm();
        return true;
      } catch (error) {
        console.log(error);
        return false;
      }
    },
    editarListaClientes() {
      const foundIndex = this.solicitud.clientesList.findIndex(
        x => x.id == this.clienteNuevoEditar.id
      );

      let cambios = true;

      if (
        this.solicitud.clientesList[foundIndex].hashCode ==
        this.hashCode(
          this.clienteNuevoEditar.idpais.toString() +
            this.clienteNuevoEditar.idsector.toString() +
            this.clienteNuevoEditar.idempresacorporacion.toString() +
            this.clienteNuevoEditar.nombre +
            this.clienteNuevoEditar.cedulaJuridica +
            this.clienteNuevoEditar.descripcion +
            this.clienteNuevoEditar.telefono +
            this.clienteNuevoEditar.correo +
            this.clienteNuevoEditar.esreferencia +
            this.clienteNuevoEditar.codigocliente +
            this.clienteNuevoEditar.activo +
            JSON.stringify(this.clienteNuevoEditar.referencias)
        )
      ) {
        cambios = false;
        Vue.$toast.warning("No se realizaron cambios.", {});
      }

      this.solicitud.clientesList[
        foundIndex
      ].nombre = this.clienteNuevoEditar.nombre;

      this.solicitud.clientesList[
        foundIndex
      ].cedulaJuridica = this.clienteNuevoEditar.cedulaJuridica;

      this.solicitud.clientesList[
        foundIndex
      ].descripcion = this.clienteNuevoEditar.descripcion;

      this.solicitud.clientesList[
        foundIndex
      ].telefono = this.clienteNuevoEditar.telefono;

      this.solicitud.clientesList[
        foundIndex
      ].correo = this.clienteNuevoEditar.correo;

      this.solicitud.clientesList[
        foundIndex
      ].esreferencia = this.clienteNuevoEditar.esreferencia;

      this.solicitud.clientesList[
        foundIndex
      ].idpais = this.clienteNuevoEditar.idpais;

      this.solicitud.clientesList[
        foundIndex
      ].idsector = this.clienteNuevoEditar.idsector;

      this.solicitud.clientesList[
        foundIndex
      ].idempresacorporacion = this.clienteNuevoEditar.idempresacorporacion;

      this.solicitud.clientesList[
        foundIndex
      ].codigocliente = this.clienteNuevoEditar.codigocliente;

      this.solicitud.clientesList[
        foundIndex
      ].activo = this.clienteNuevoEditar.activo;

      this.solicitud.clientesList[foundIndex].referencias = this.referencias; //revisar

      // if (this.solicitud.clientesList[foundIndex].estado != "N") {
      //   this.solicitud.clientesList[foundIndex].estado = "M";
      // }

      // this.solicitud.estado = "M";
      if (this.solicitud.clientesList[foundIndex].estado != "N") {
        if (cambios) {
          this.solicitud.clientesList[foundIndex].estado = "M";
        } else {
          this.solicitud.clientesList[foundIndex].estado = "S";
        }
      }

      if (cambios) {
        this.solicitud.estado = "M";
      }
    },
    agregarListaClientes() {
      this.solicitud.clientesList.push({
        idcolaborador: this.colaborador.idcolaborador,
        estado: "N",
        nombre: this.clienteNuevoEditar.nombre,
        cedulaJuridica: this.clienteNuevoEditar.cedulaJuridica,
        descripcion: this.clienteNuevoEditar.descripcion,
        telefono: this.clienteNuevoEditar.telefono,
        correo: this.clienteNuevoEditar.correo,
        esreferencia: this.clienteNuevoEditar.esreferencia,
        idpais: this.clienteNuevoEditar.idpais,
        idsector: this.clienteNuevoEditar.idsector,
        idempresacorporacion: this.clienteNuevoEditar.idempresacorporacion,
        codigocliente: this.clienteNuevoEditar.codigocliente,
        activo: this.clienteNuevoEditar.activo,
        referencias: this.referencias // revisar
      });
      this.solicitud.clientesList = this.addIdentityToArrayObjects(
        this.solicitud.clientesList
      );
      this.solicitud.estado = "M";
    },
    getNuevoId() {
      if (this.solicitud.clientesList) {
        if (this.solicitud.clientesList.length > 0) {
          return (
            _.maxBy(this.solicitud.clientesList, "idcliente").idcliente + 1
          );
        }
      }

      return 1;
    },
    getNuevoReferenciaId() {
      if (this.referencias) {
        if (this.referencias.length > 0) {
          return _.maxBy(this.referencias, "idreferencia").idreferencia + 1;
        }
      }

      return 1;
    },
    selected(item) {
      switch (this.TipoListaPick) {
        case "Colaboradores":
          this.showModal = false;

          // ir al detalle para obtener id puesto

          this.cargarDetalleColaborador(item.id);

          break;

        case "Clientes":
          this.showModal = false;
          this.cargarClientePickList(item);
          break;

        default:
          break;
      }
    },
    cargarClientePickList(cliente) {
      this.cargandoDatos = true;
      if (!this.existeCliente(cliente)) {
        this.solicitud.clientesList.push({
          ...cliente,
          idcliente: cliente.id,
          estado: "S",
          hashCode: this.hashCode(
            cliente.idpais.toString() +
              cliente.idsector.toString() +
              cliente.idempresacorporacion.toString() +
              cliente.nombre +
              cliente.cedulaJuridica +
              cliente.descripcion +
              cliente.telefono +
              cliente.correo +
              cliente.esreferencia +
              cliente.codigocliente +
              cliente.activo +
              JSON.stringify(cliente.referencias)
          )
        });

        this.solicitud.clientesList = this.addIdentityToArrayObjects(
          this.solicitud.clientesList
        );

        this.solicitud.estado = "M";
        this.terminaCargaDatos();
      } else {
        Vue.$toast.warning("El cliente seleccionado ya está en la lista.");
      }
    },
    existeCliente(item) {
      const list = this.solicitud.clientesList.filter(
        x => x.idcliente === item.id
      );
      const list2 = this.solicitud.clientesList.filter(
        x => x.idcliente === item.idcliente
      );
      if (list.length > 0 || list2.length > 0) {
        return true;
      } else {
        return false;
      }
    },
    async cargarClientes() {
      this.filters = {};
      const {
        data: { clientesReferencias }
      } = await GetClientesReferencias();
      if (clientesReferencias.length == 0) {
        this.solicitud.clientesList = [];
        Vue.$toast.warning("No existen clientes ingresados.");
      } else {
        this.solicitud.clientesList = clientesReferencias.map(
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
            activo
          }) => ({
            idcliente: idcliente,
            idsector: idsector,
            idpais: idpais,
            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion,
            estado: "S",
            referencias: (referencias || []).map(ref => ({
              ...ref,
              estado: "S"
            })),
            codigocliente: codigocliente,
            cedulaJuridica: cedulajuridica,
            correo: correo,
            telefono: telefono,
            esreferencia: esreferencia,
            idempresacorporacion: idempresacorporacion,
            activo: activo
          })
        );
      }
    },
    addEstado(list) {
      let lista = list.map(ref => ({ ...ref, estado: "S" }));
      return lista;
    },
    async cargarSectores() {
      const {
        data: { sectores }
      } = await GetSectores();
      this.sectorList = sectores.map(({ idSector, nombre }) => ({
        label: nombre,
        value: idSector
      }));
    },
    async cargarPais() {
      const {
        data: { paises }
      } = await GetAllPaises();
      this.paisList = paises.map(({ idpais, nombre }) => ({
        label: nombre,
        value: idpais
      }));
    },
    async cargarEmpresasCorporacion() {
      const {
        data: { empresaCorporacions }
      } = await GetEmpresaCorporacion();
      this.empresaCorpList = empresaCorporacions.map(
        ({ idempresacorporacion, nombre }) => ({
          label: nombre,
          value: idempresacorporacion
        })
      );
    },
    async displayModal(tipo) {
      this.TipoListaPick = tipo;
      this.clean();
      switch (tipo) {
        case "Colaboradores":
          const {
            data: { colaboradores }
          } = await GetAllColaboradores();
          this.dataPickList = colaboradores.map(
            ({
              idcolaborador,
              nombreCompleto,
              nombre,
              identificacion,
              primerapellido,
              segundoapellido,
              foto
            }) => ({
              id: idcolaborador,
              nombre: nombre,
              nombreCompleto: nombreCompleto,
              identificacion: identificacion,
              primerapellido: primerapellido,
              segundoapellido: segundoapellido,
              foto: foto
            })
          );

          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombreCompleto", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "Colaborador";
          this.tituloModal = "Lista de colaboradores";

          break;
        case "Clientes":
          const {
            data: { clientesReferencias }
          } = await GetClientesReferencias();
          this.dataPickList = clientesReferencias.map(
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
              activo
            }) => ({
              id: idcliente,
              idsector: idsector.toString(),
              idpais: idpais.toString(),
              nombre: nombre,
              descripcion: descripcion == null ? "" : descripcion,
              estado: "S",
              referencias: (referencias || []).map(ref => ({
                ...ref,
                estado: "S"
              })),
              codigocliente: codigocliente,
              cedulaJuridica: cedulajuridica,
              correo: correo,
              telefono: telefono,
              esreferencia: esreferencia == true ? "Si" : "No",
              idempresacorporacion: idempresacorporacion.toString(),
              idcliente: idcliente,
              activo: activo == true ? "Si" : "No"
            })
          );

          this.columnasPickList = [
            { field: "id", header: "Id" },
            { field: "nombre", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "Cliente";
          this.tituloModal = "Lista de clientes";

          break;

        default:
          break;
      }
    },
    clean() {
      this.showModal = false;
      this.nombreTipoLista = "";
      this.dataPickList = null;
      this.filters = {};
    },
    cleanForm() {
      this.solicitud.idcliente = null;
      this.solicitud.nombreCliente = "";
      this.solicitud.cedulaJuridica = "";
      this.solicitud.descripcion = "";
      this.solicitud.telefono = "";
      this.solicitud.correo = "";
      this.solicitud.referencia = false;
      this.solicitud.esreferencia = "Si";
      this.solicitud.activo = "Si";
      this.solicitud.codigocliente = "";
      this.solicitud.pais = null;
      this.solicitud.sector = null;
      this.solicitud.clienteReferenciasList = [];
      this.referencias = [];
    },
    cleanFormReferencias() {
      this.referenciaNuevaEditar.identificacion = "";
      this.referenciaNuevaEditar.nombre = "";
      this.referenciaNuevaEditar.activo = "Si";
      this.referenciaNuevaEditar.correo = "";
      this.referenciaNuevaEditar.telefono = "";
      this.referenciaNuevaEditar.extension = "";
      this.referenciaNuevaEditar.celular = "";
      this.referenciaNuevaEditar.puesto = "";
      this.referenciaNuevaEditar.idcliente = null;
      this.referenciaNuevaEditar.idreferencia = null;
    },
    onUpload() {
      //this.$toast.add({severity: 'info', summary: 'Success', detail: 'File Uploaded', life: 3000});
    },
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
      this.terminaCargaDatos();
    },
    pintaEstado(estado, style) {
      if (style) {
        switch (estado) {
          case "S":
            return "";
          case "N":
            return "badge rounded-pill bg-success p-component";
          case "M":
            return "badge rounded-pill bg-warning p-component";
          case "E":
            return "badge rounded-pill bg-danger p-component";

          default:
            break;
        }
      } else {
        switch (estado) {
          case "S":
            return "Sin cambios";
          case "N":
            return "Nuevo";
          case "M":
            return "Modificado";
          case "E":
            return "Eliminado";

          default:
            break;
        }
      }
    },

    // Metodos de la botonera

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
    //   this.confirmacionEliminarSolicitud = true;
    // },
    async submitSolicitud(formValues) {
      try {
        if (this.solicitud.estado == common.SOLICITUD_SINMODIFICACIONES) {
          Vue.$toast.warning(
            "La solicitud no tiene modificaciones que guardar.",
            {}
          );
        } else if (this.solicitudSinModificaciones()) {
          let mensaje = "";
          if (this.solicitud.clientesList.length == 0) {
            mensaje = "Por favor incluir al menos un registro a la lista.";
          } else {
            mensaje =
              "Por favor excluir los clientes que no tienen modificaciones.";
          }

          Vue.$toast.warning(mensaje, {});
        } else {
          //   let resultadoValidacion = this.validacionesSolicitud();
          //   if (resultadoValidacion.esValido) {
          let datos = {
            idSolicitud: this.datosSolicitud.idSolicitudMasterData,
            idTipoSolicitud: this.datosSolicitud.idTipoSolicitud,
            codigoEstado: this.datosSolicitud.estadoSolicitud,
            idAutor: this.$root.infoColaboradorActual.id,
            idResponsable: this.$root.infoColaboradorActual.id,
            idColaborador: this.colaborador.idcolaborador,
            accion: "Enviar",
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
          // } else {
          //   Vue.$toast.warning(
          //     "La solicitud presenta las siguientes inconsistencias: <br/>" +
          //       resultadoValidacion.mensaje,
          //     {}
          //   );
          // }
        }
      } catch (error) {
        console.log(error);
      }
    },
    validacionesSolicitud() {
      let resultado = { esValido: true };

      // if (
      //   !this.cursoNuevoEditar.archivocurso ||
      //   this.cursoNuevoEditar.archivocurso == ""
      // ) {
      //   resultado = {
      //     esValido: false,
      //     mensaje: "Debe agregar el certificado del curso"
      //   };
      // }

      return resultado;
    },
    solicitudSinModificaciones() {
      const foundIndex = this.solicitud.clientesList.findIndex(
        x => x.estado == "S"
      );

      if (foundIndex >= 0 || this.solicitud.clientesList.length == 0) {
        return true;
      }

      return false;
    },

    async ValidaDependenciasEliminar() {
      
      let result = "";
      let res = true;

      result = await ValidaClienteDependencias({
        id: this.clienteSeleccionado.idcliente
      });


      if (result.data == "OK") {
       res = true;
      } else {
         Vue.$toast.error(result.data, {});
        res = false;
      }

      return res;
    },
    muestraBotonera() {
      if (this.origen == "Inbox") {
        if (!this.idResponsable || this.idResponsable == this.$root.infoColaboradorActual.id) {
          return true;
        } else {
          return false;
        }
      } else {
        return false;
      }
    }
  },
  computed: {
    clienteAEliminar() {
      return this.clienteEliminar;
    },
    referenciaAEliminar() {
      return this.referenciaEliminar;
    },
    tieneReferencia() {
      return this.referencia == false ? "No" : "Si";
    },
    puedePintar() {
      // let pinta = false;
      // if (
      //   this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
      //   this.datosSolicitud.estadoSolicitud == common.CORREGIR
      // ) {
      //   pinta = true;
      // }

      // return pinta;
      return this.habilitarControlesSolicitud(
        this.datosSolicitud.estadoSolicitud,
        this.origen
      );
    },
    puedePintarAdd() {
      let pinta = false;
      if (this.datosSolicitud.estadoSolicitud == common.REGISTRADA) {
        pinta = true;
      }

      return pinta;
    },
    descripcionComputed() {
      if (this.clienteNuevoEditar) {
        if (this.clienteNuevoEditar.descripcion) {
          return this.clienteNuevoEditar.descripcion.length;
        } else {
          return 0;
        }
      } else {
        return 0;
      }
    },

    pickPuedeBuscarColaborador() {
      return this.datosSolicitud.estadoSolicitud == common.REGISTRADA; // && !(this.usuarioActual.Roles.length == 1 && this.usuarioActual.Roles[0] == "COLABORADOR") ;
    },
    eliminada() {
      return this.clienteNuevoEditar.estado === "E";
    },
    eliminadaRef() {
      return this.referenciaNuevaEditar.estado === "E";
    },
    tituloModalEdicionComputed() {
      let res = this.tituloModalEdicion;

      if (this.clienteNuevoEditar.estado === "E") {
        res = "Ver";
      }
      if (
        this.datosSolicitud.estadoSolicitud != common.REGISTRADA &&
        this.datosSolicitud.estadoSolicitud != common.CORREGIR
      ) {
        res = "Ver";
      }

      return res;
    },
    today() {
      return new Date();
    }
  },
  watch: {
    /**
     * Controla el cambio de alguno de los campos de clientes, esto permitirá marcar
     * el documento con un estado de modificado
     */
    solicitud: function(newValue, oldValue) {
      if (!this.cargandoDatos) {
        this.solicitud.estado = "M";
      }
    }
  }
};
</script>

<style>
.pi-exclamation-triangle {
  color: red !important;
}
/*todo*/
.button.close {
  color: red !important;
  background-color: red !important;
}
.modal-ku {
  width: 750px !important;
  margin: auto !important;
}
.modal-lg {
  max-width: 80% !important;
}

.text-divider {
  margin: 2em 0;
  line-height: 0;
  font-size: 0.875rem;
}
.text-divider span {
  background-color: gainsboro;
  padding: 0.5em 0.5em 0.5em 0.5em;
}
.text-divider:before {
  content: " ";
  display: block;
  border-top: 1px dotted rgb(187, 187, 187);
  border-bottom: 1px solid #f7f7f7;
}

.iconTrash {
  color: #e55353;
}
.iconUndo {
  color: greenyellow;
}
.bg-danger {
  color: white;
}
.bg-info {
  color: white;
}
.bg-warning {
  color: white;
}
.bg-success {
  color: white;
}
.text-divider {
  margin: 2em 0;
  line-height: 0;
  font-size: 0.875rem;
}
.text-divider span {
  background-color: gainsboro;
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
.avatar-upload .avatar-show {
  position: absolute;
  right: 12px;
  z-index: 1;
  top: 10px;
  display: none;
}
.avatar-upload .avatar-show input {
  display: none;
}
.avatar-upload .avatar-show input + label {
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
.avatar-upload .avatar-show input + label:hover {
  background: #f1f1f1;
  border-color: #d6d6d6;
}
.avatar-upload .avatar-show input + label:after {
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

/* */

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

.p-button {
  background-color: #29abe2;
}

.onHoverDark:hover {
  color: #fff;
  background-color: #002e6e;
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
  width: 700px !important;
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
.p-datatable-cliente .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-cliente .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-cliente .p-datatable-tbody > tr > td {
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
  .p-datatable.p-datatable-cliente .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-cliente .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-cliente .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-cliente .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-cliente:last-child {
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
