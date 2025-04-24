<template>
  <div>


    <CRow> 
      <CCol>
        
          <CButton
            block
            color="primary"
            @click="atras()"
            class="btn btn-primary btn-lg"
            style="height: 40px; width: 130px;float: right; color: rgba(98, 184, 235, 1); background-color: transparent; border-color: transparent;"
          >
            <i class="pi pi-arrow-circle-left"></i>&nbsp;Regresar</CButton
          >
      </CCol>
    </CRow> 

    <CCard style="border-radius: 16px !important;">
      <CCardHeader style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;">
        <strong> Experiencia en Proyectos</strong>
        <small> Solicitud de cambio </small>
        <CBadge
          color="primary"
          class="mr-2 libro-marca-azuloscuro"
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
      </CCardHeader>
      <CCardBody>
        <resumen-colaborador
          :colaborador.sync="colaborador"
          :estado-solicitud="datosSolicitud.estadoSolicitud"
          :searchable="pickPuedeBuscarColaborador"
          @displayModal="displayModal"
          :editable="false"
        >
        </resumen-colaborador>
      </CCardBody>
    </CCard>  

    <CRow class="mt-1">
          <CCol md="9" sm="12" class="mb-2">
            <CButton
              class="mt-1 btn btn-info pull-left libro-marca-celeste onHoverDark "
              @click="agregar()"
              v-if="this.puedePintar"
              v-tooltip.top="'Agregar un nuevo proyecto'"
            >
              <li class="pi pi-plus ml-1"></li>
              Nuevo proyecto
            </CButton>
            <CButton
              class="mt-1 ml-1 btn btn-info pull-left libro-marca-celeste onHoverDark "
              @click="displayModal('Proyectos')"
              v-if="this.puedePintar"
              v-tooltip.top="'Permite seleccionar un proyecto ya registrado para aplicar cambios'"
            >
              <li class="pi pi-copy ml-1"></li>
              Incluir proyecto
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

    <!-- <CCard style="border-radius: 16px !important;">
      <CCardHeader style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;">
      </CCardHeader>
      <CCardBody> -->
        <FormulateForm name="solicitud" @submit="submitSolicitud">
          <CRow style="margin-bottom: 20px;">
            <CCol lg="12">
              <DataTable
                ref="dt"
                :value="solicitud.proyectosColaboradorList"
                :paginator="true"
                class="p-datatable-formacion p-datatable-striped p-datatable-sm mb-4"
                :rows="5"
                dataKey="id"
                :selection.sync="proyectoSeleccionado"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} de registros"
                sortField="nombreCompleto"
                :sortOrder="1"
                stateKey="dt-state-formacion"
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

                <!-- Cliente -->
                <Column
                  header="Cliente"
                  :sortable="true"
                  sortField="nombreCliente"
                  filterField="nombreCliente"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Cliente</span>
                    {{ slotProps.data.esProyectoNova == "Si" ? slotProps.data.nombreClienteCorporacion : slotProps.data.nombreCliente }}
                    <span v-show="slotProps.data.esProyectoNova == 'Si'" class="badge badge-pill badge-info"><img src="/img/ico-nova.png"></span>
                  </template>
                </Column>

                <!-- Proyecto -->
                <Column
                  header="Proyecto"
                  :sortable="true"
                  sortField="nombreProyecto"
                  filterField="nombreProyecto"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Nombre</span>
                     <a
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Editar(slotProps.data)">

                        {{ slotProps.data.nombreProyecto }}

                      </a>
                    
                  </template>
                </Column>

                <!-- Fecha de inicio -->
                <Column
                  header="Inicio"
                  :sortable="true"
                  sortField="inicio"
                  filterField="inicio"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Inicio</span>
                    {{ slotProps.data.inicio }}
                  </template>
                </Column>

                <!-- Fecha de finalización -->
                <Column
                  header="Fin"
                  :sortable="true"
                  sortField="fin"
                  filterField="fin"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Fin</span>
                    {{ slotProps.data.fin }}
                  </template>
                </Column>

                <!-- Estado -->
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

                <!-- Acciones -->
                <Column
                  header=""
                  :sortable="false"
                  sortField="accion"
                  filterField="accion"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title"></span>
                    <div v-if="slotProps.data.estado !== 'N' && slotProps.data.estado !== 'E'" style="display:inline;">
                      <a
                        v-if="puedePintar"
                        class="pi pi-times-circle ml-1 iconExclude"
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Excluir(slotProps.data)"
                        title="Excluir proyecto de la lista"
                      ></a>
                    </div>
                    <div v-if="slotProps.data.estado !== 'E'" style="display:inline;">
                      <a
                        v-if="puedePintar"
                        class="pi pi-trash ml-1 iconTrash"
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Eliminar(slotProps.data)"
                        title="Eliminar proyecto"
                      ></a>
                    </div>
                    <div v-else>
                      <a
                        v-if="puedePintar"
                        class="pi pi-refresh ml-1 iconUndo"
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Undo(slotProps.data)"
                        title="Recuperar proyecto eliminado"
                      ></a>
                    </div>
                  </template>
                </Column>
              </DataTable>
            </CCol>
          </CRow>
             <CCard v-if="this.comentarios.length > 0" style="border-radius: 16px !important;">
      <CCardHeader style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;">
      <strong> Comentarios</strong>      
      </CCardHeader>
      <CCardBody>
          <comments
            :comments_wrapper_classes="['custom-scrollbar', 'comments-wrapper']"
            :comments="this.comentarios"
          ></comments>
      </CCardBody>
    </CCard>   

    <CRow style="margin-bottom: 80px;">
      <CCol>
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

        <!-- Diálogo para el formulario de edición de un proyecto -->
        <Dialog :visible.sync="showModalAgregar" :style="{width: '70vw'}"  :maximizable="true" :modal="true">
          <template #header>
            <!-- <h5>{{puedePintar == true ? 'Modificar experiencia en proyecto' : 'Ver proyecto'}}</h5> -->
            <h5>{{ tituloModalEdicion }}</h5>
          </template>
        <!-- <CModal
          :title="puedePintar == true ? 'Modificar experiencia en proyecto' : 'Ver curso'"
          :show.sync="showModalAgregar"
          color="primary"
          class="modal-libro"
          :closeOnBackdrop="false"
        > -->
          
          <!-- Formulario de edición de un proyecto -->
          <FormulateForm
            name="formProyecto"
            v-model="proyectoNuevoEditar"
            ref="formProyecto"
            @submit="submit"
            @failed-validation="errorValidacion"
            :invalid-message="this.invalidMessage"
          >
            <div>
              <FormulateErrors />
              <CRow>
                <CCol md="12">
                  <!-- TABS para separar datos generales del proyecto y datos de los contactos -->
                  <TabView>
                    <TabPanel header="Datos generales">
                      <!-- Indicador de proyecto NOVA -->
                      <CRow>
                        <CCol md="4">
                            <!-- <InputSwitch class="align-middle" v-model="esProyectoNova" />&nbsp;
                            <span :class="{'font-weight-light':!esProyectoNova, 'font-weight-normal':esProyectoNova}" class="align-middle">Proyecto Grupo Novacomp</span> -->
                             <span for="esProyectoNova" class="formulate-input-label formulate-input-label--before text-left ">
                            Es proyecto de Novacomp?
                          </span>
                             <FormulateInput
                                type="myInputSwitch"
                                name="esProyectoNova"
                                :input-class="['form-control', 'readonly']"
                                validation-name="Es proyecto Nova"
                                @change="onChangeEsProyectoNova()"
                                class="hint"
                                :disabled="!puedePintar || eliminada"
                              />
                        </CCol>
                      </CRow>

                      <!-- Datos de empresa, cliente y proyecto -->
                      <CRow>
                        <CCol md="4">
                          <FormulateInput
                            name="nombreEmpresa"
                            label="Nombre de la empresa"
                            validation="optional|max:100,length"
                            input-class="form-control"
                            validation-name="Empresa"
                            placeholder="Ingrese el nombre de la empresa"
                            :readonly="!puedePintar || eliminada"
                            v-show="!esProyectoNovacomp"
                            :error="!esProyectoNovacomp && !nombreEmpresaValido ? 'Debe registrar el nombre de la empresa' : null"
                          />
                          <FormulateInput
                            name="idEmpresaCorporacion"
                            :options="empresasList"
                            type="select"
                            label="Empresa de la corporación"
                            placeholder="Seleccione una empresa"
                            validation="optional|not:0"
                            validation-name="EmpresaCorporacion"
                            :validation-messages="{
                              required: 'Seleccione una empresa.',
                              not: 'Debe de seleccionar una opción válida.'
                            }"
                            :input-class="['form-control']"
                            @change="onChangeEmpresa($event, 'idEmpresaCorporacion')"
                            :disabled="!puedePintar || eliminada"                            
                            v-show="esProyectoNovacomp"   
                            :error="esProyectoNovacomp && !empresaCorporacionValido ? 'Debe seleccionar la empresa' : null"                         
                          />
                        </CCol>

                        <CCol md="4" v-show="!esProyectoNovacomp">                         
                          <FormulateInput
                            name="nombreCliente"
                            label="Nombre del cliente"
                            validation="optional|max:100,length"
                            input-class="form-control"
                            validation-name="Cliente"
                            placeholder="Ingrese el nombre del cliente"
                            :readonly="!puedePintar || eliminada"   
                            :error="!esProyectoNovacomp && !nombreClienteValido ? 'Debe registrar el nombre del cliente' : null"
                            @change="onChangeNombreCliente"
                          />
                        </CCol>

                        <CCol md="4" v-show="esProyectoNovacomp">
                          <FormulateInput
                            name="idCliente"
                            :options="clientesList"
                            type="select"
                            label="Cliente"
                            placeholder="Seleccione un cliente"
                            validation="optional|not:0"
                            validation-name="IdCliente"
                            :validation-messages="{
                              required: 'Seleccione un cliente.',
                              not: 'Debe de seleccionar una opción válida.'
                            }"
                            :input-class="['form-control']"
                            @change="onChangeCliente($event, 'idCliente')"
                            :disabled="!puedePintar || eliminada"
                            :error="esProyectoNovacomp && !clienteValido ? 'Debe seleccionar el cliente' : null"                         
                          />
                        </CCol>
                        

                        <CCol md="4">
                           <FormulateInput
                            name="nombreProyecto"
                            label="Nombre del proyecto"
                            validation="required|max:1000,length"
                            input-class="form-control"
                            validation-name="Nombre del proyecto"
                            placeholder="Ingrese el nombre del Proyecto"
                            :readonly="!puedePintar || eliminada"
                            @focus="showOverlayPanelProyectos()"
                            autocomplete="off"
                            autofill="false"
                          />
                          <OverlayPanel ref="showOverlayPanelProyectos" :showCloseIcon="true" :dismissable="true"
                                        style="z-index:1051;width: 90%;margin-top:-10px;"
                                        appendTo="#formProyecto">
                            <h6>Algunos proyectos del cliente:</h6>
                            <Listbox v-model="proyectoClienSeleccionadoList" :options="proyectosClienteList"  
                                     :filter="true" optionLabel="nombre" listStyle="max-height:150px" style="width:100%"
                                     @change="onProyectoClienteSelect">
                            </Listbox>
                           
                          </OverlayPanel>
                          <!-- <FormulateInput
                            name="rol"
                            label="Rol en el proyecto"
                            validation="required|max:100,length"
                            input-class="form-control"
                            validation-name="Rol"
                            placeholder="Ingrese el rol en el proyecto"
                            :readonly="!puedePintar"
                          /> -->
                        </CCol>
                        
                      </CRow>

                      <!-- Rol en el proyecto, fecha de inicio y fecha de finalización -->
                      <CRow>
                        <CCol md="4">
                          <!-- <FormulateInput
                            name="numeroLicitacion"
                            label="Número de licitación"
                            validation="max:100,length"
                            input-class="form-control"
                            validation-name="Número de licitación"
                            placeholder="Ingrese el número de licitación"
                            :readonly="!puedePintar"
                          /> -->
                          <FormulateInput
                            name="rol"
                            label="Rol en el proyecto"
                            validation="required|max:100,length"
                            input-class="form-control"
                            validation-name="Rol"
                            placeholder="Ingrese el rol en el proyecto"
                            :readonly="!puedePintar || eliminada"
                          />
                        </CCol>

                        <CCol md="4">
                          <FormulateInput
                            type="date"
                            name="fechainicio"
                            validation="required|validaFecha"
                            label="Fecha de inicio"
                            :validation-rules="{
                              validaFecha: ({ value }) => {
                                var ToDate = new Date();
                                var valueDate = new Date(value);
                                if (valueDate > ToDate) {
                                      return false;
                                }
                                return true;
                              }
                            }"
                            :validation-messages="{
                              validaFecha: 'La fecha de inicio debe ser menor o igual a hoy',
                              required: 'La fecha de inicio es obligatoria'
                            }"
                            min="1970-01-01"
                            max="2200-01-01"
                            input-class="form-control"
                            validation-name="Fecha inicio"
                            :help-class="['hint']"
                            :disabled="!puedePintar || eliminada"
                          />
                        </CCol>

                        <CCol md="4">
                          <FormulateInput
                            type="date"
                            name="fechafinal"
                            label="Fecha de finalización"
                            min="1970-01-01"
                            max="2200-01-01"
                            validation="optional|validarFechaFinal"
                            validation-name="Fecha de finalización"
                            :validation-rules="{validarFechaFinal}"
                            :validation-messages="{
                              validarFechaFinal: 'La fecha de finalización debe ser menor o igual a hoy y mayor a la fecha de inicio'
                            }"
                            input-class="form-control"
                            :help-class="['hint']"
                            :disabled="!puedePintar || eliminada"
                          />
                        </CCol>
                      </CRow>

                      <!-- Descripción del proyecto, herramientas y tecnologías -->
                      <CRow>
                        <CCol md="4">
                          <FormulateInput
                            type="textarea"
                            name="descripcion"
                            label="Descripción"
                            validation="required|max:8000,length"
                            validation-name="Descripción"
                            :input-class="['form-control']"                    
                            rows="5"
                            :help-class="['hint']"
                            :readonly="!puedePintar || eliminada"
                          />
                        </CCol>
                        <CCol md="4">
                          <!-- <FormulateInput
                            name="herramientas"
                            label="Herramientas"
                            validation="max:100,length"
                            input-class="form-control"
                            validation-name="Herramientas"
                            placeholder="Ingrese las herramientas utilizadas"fdd
                            :readonly="!puedePintar"
                          /> -->
                          <label for="herramientas" class="formulate-input-label formulate-input-label--before text-left w-100">
                            Herramientas utilizadas
                          </label>
                          <tags-input element-id="herramientas"
                                      v-model="proyectoNuevoEditar.herramientas"
                                      discard-search-text="Descartar resultado de búsqueda"
                                      :add-tags-on-blur="true"
                                      typeahead-style="dropdown"
                                      placeholder="Herramienta"
                                      :only-existing-tags="true"
                                      :existing-tags="herramientasList"
                                      :typeahead="true"
                                      :disabled="!puedePintar || eliminada">
                          </tags-input>
                          <div id="herramientas-help" class="formulate-input-help formulate-input-help--after hint">Digite el nombre de una herramienta (lenguaje de programación, ambiente de desarrollo, base de datos, calidad, infraestructura, buses / SOA)</div>
                        </CCol>
                        <CCol md="4">
                          <!-- <FormulateInput
                            name="tecnologias"
                            label="Tecnologías"
                            validation="max:100,length"
                            input-class="form-control"
                            validation-name="Tecnologías"
                            placeholder="Ingrese las tecnologías utilizadas"
                            :readonly="!puedePintar"
                          /> -->
                          <label for="tecnologias" class="formulate-input-label formulate-input-label--before text-left w-100">
                            Tecnologías utilizadas
                          </label>
                          <tags-input element-id="tecnologias"
                                      v-model="proyectoNuevoEditar.tecnologias"
                                      discard-search-text="Descartar resultado de búsqueda"
                                      :add-tags-on-blur="true"
                                      typeahead-style="dropdown"
                                      placeholder="Tecnología"
                                      :only-existing-tags="true"
                                      :existing-tags="tecnologiasList"
                                      :typeahead="true"
                                      :disabled="!puedePintar || eliminada">
                          </tags-input>
                          <div id="tecnologias-help" class="formulate-input-help formulate-input-help--after hint">Digite el nombre de una tecnología</div>
                        </CCol>
                      </CRow>
                    </TabPanel>
                    <TabPanel header="Contactos" >
                      <!-- Nombre del contacto, teléfono y correo -->
                      <!-- <CRow>
                        <CCol md="4">
                          <FormulateInput
                            name="nombreContacto"
                            label="Nombre del contacto"
                            placeholder="Ingrese el nombre del contacto"
                            validation-name="Nombre del contacto"
                            :input-class="['form-control']"
                            :help-class="['hint']"
                            :readonly="!puedePintar || eliminada"
                          />
                        </CCol>
                        <CCol md="4">
                          <FormulateInput
                            type="tel"
                            name="telefonoContacto"
                            label="Teléfono del contacto"
                            placeholder="Ingrese el número de teléfono"
                            help="Opcional o Formatos: +00 00000000 o +000 00000000"
                            :validation="[
                                            ['optional'],
                                            ['matches', /^\+(?:[0-9] ?){6,14}[0-9]$/]
                                          ]"
                            validation-name="Teléfono"
                            :input-class="['form-control']"
                            :help-class="['hint']"
                            :readonly="!puedePintar || eliminada"
                          />
                        </CCol>
                        <CCol md="4">
                          <FormulateInput
                            type="email"
                            name="correoContacto"
                            label="Correo del contacto"
                            placeholder="Ingrese el correo"
                            help="correo@dominio.ext"
                            validation="optional|email|max:100,length"
                            validation-name="Correo"
                            :input-class="['form-control']"
                            :help-class="['hint']"
                            :readonly="!puedePintar || eliminada"
                          />
                        </CCol>
                      </CRow> -->
                      
                      <!-- :estado-solicitud="datosSolicitud.estadoSolicitud" -->
                      <agrupacion-contactos
                        :contactos="proyectoNuevoEditar.contactos"
                        :habilitar-controles="this.habilitarControlesSolicitud(this.datosSolicitud.estadoSolicitud, this.origen)"
                        nombreFile="Documento afiliación"
                        @undo="undoContacto"
                        @modify="modificarContacto"
                        @add="agregarContacto"
                        @remove="eliminarContacto"
                      >
                      </agrupacion-contactos>
                    </TabPanel>
                  </TabView>
                </CCol>
              </CRow>
              <!-- <h2 class="text-divider"><span>Datos generales</span></h2> -->

              <!-- <h2 class="text-divider"><span>Datos del contacto</span></h2> -->
            </div>
          </FormulateForm>

        <template #footer>
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
                <!--  -->
              </CCol>
          </CRow>
        </template>

        <!-- </CModal> -->
        </Dialog>
      <!-- </CCardBody>
    </CCard> -->

 
    
    <!-- Diálogo para los PICKLIST -->
    <Dialog :visible.sync="showModal" :style="{width: '70vw'}"  :maximizable="true" :modal="true">
        <template #header>
          <h5>{{tituloModal}}</h5>
        </template>
        
        <pick-list
          @selected="selected"
          :nombreTipoLista="nombreTipoLista"
          :columnas="columnasPickList"
          :dataContenido="dataPickList"
        ></pick-list>

        <template #footer>
        </template>
    </Dialog>

    <!-- Diálogos de confirmación -->
    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
    <comment-dialogue ref="commentDialogue"></comment-dialogue>
  </div>
</template>

<script>
// ---------------------------------------------------------------------------------------------
// IMPORTS
// ---------------------------------------------------------------------------------------------
import {
  GetColaboradoresPerfil,
  GetAllColaboradores,
  GetEmpresaCorporacion,
  GetProyectosCliente,
  GetProyectosByColaborador,
  GetClientesEmpresa,
  GetColaboradorDetailResumen,
  GetDetalleSolicitud,
  GetListaHerramientas,
  ValidarProyecto
} from "./request";

import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "./ResumenColaborador";
import Vue from "vue";
import { aplicarFormatoFecha } from "../../utils";
import BotoneraMasterData from "./BotoneraMasterData.vue";
import _ from "lodash";
import ContactosProyecto from "../generales/Componentes/ContactosProyecto.vue";

// métodos comunes
import common from "./common.js";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";
import Comments from "../generales/Componentes/Comments/Comments.vue";

// ---------------------------------------------------------------------------------------------
// EXPORT
// ---------------------------------------------------------------------------------------------
export default {
  name: "ExperienciaProyectos",
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
    comments: Comments,
    "agrupacion-contactos": ContactosProyecto
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
      proyectoNuevoEditar: {
        rol: null,
        telefonoContacto: null,
        idEmpresaCorporacion: null,
        nombreEmpresa: null,
        idCliente: null,
        nombreCliente: null,
        nombreProyecto: null,
        nombreContacto: null,
        correoContacto: null,
        fechainicio: null,
        fechafinal: null,
        inicio: null,
        fin: null,
        descripcion: null,
        numeroLicitacion: null,
        herramientas:[],
        tecnologias:[],
        nombreEmpresaCorporacion: null,
        nombreClienteCorporacion: null,        
        esProyectoNova: "Si",
        contactos: []
      },
      //esProyectoNova: true,
      solicitud: {
        estado: "S",
        proyectosColaboradorList: []
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
      cursosList: [],
      loading: false,
      proyectoSeleccionado: [],
      showModalAgregar: false,
      showModalConfirmacion: false,
      proyectoEliminar: "",
      tituloModalEdicion: "",
      tituloModal:"",
      TipoListaPick: "",
      cargandoDatos: false,
      IdSolicitudJson: null,
      showModalVisorAdjuntos: false,
      esEdicion: false,
      visorEnable: false,
      empresasList: [],
      herramientasList: [],
      tecnologiasList: [],
      clientesList: [
        {
          label: "BAC",
          value: 1
        },
        {
          label: "CCSS",
          value: 2
        }
      ],
      proyectosClienteList:[],
      proyectoClienSeleccionadoList: null
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...
    this.IdSolicitudJson = this.idSolicitud;

    if (this.IdSolicitudJson) {
      this.cargarSolicitud();
    } else {
      //carga el usuario en session
      this.TipoListaPick = "Colaboradores";
      this.selected(this.$root.infoColaboradorActual);
    }
    await this.getListaEmpresasCorporacion();
    await this.getListaHerramientas();

    this.disableAutoComplete();
  },
  methods: {
    guardar(e){
      this.$formulate.submit('formProyecto');
    },
    showModalVisor(show) {
      this.visorEnable = show;
    },
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        if (key == "contactos") {
          fieldNames += `-  Datos de contactos\r\n`;
        }
        else{
          fieldNames += `-  ${fields[key].label}\r\n`;
        }
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    errorValidacion(data) {
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
            proyectosColaboradorList: colaborador.proyectosColaboradorList
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

    // Close() {
    //   this.showModalConfirmacion = false;
    // },
    // CloseVisor() {
    //   this.showModalVisorAdjuntos = false;
    //   this.showModalAgregar = true;
    // },
    async Eliminar(item) {
      this.proyectoSeleccionado = item;
      // this.showModalConfirmacion = true;
      this.proyectoEliminar = `'${this.proyectoSeleccionado.nombreProyecto}' para el cliente '${this.proyectoSeleccionado.esProyectoNova == 'No'? this.proyectoSeleccionado.nombreCliente : this.proyectoSeleccionado.nombreClienteCorporacion}'`;

      const res = await this.Eliminar_Generico(
        "proyecto",
        this.proyectoEliminar,
        "M"
      );

      const foundIndex = this.solicitud.proyectosColaboradorList.findIndex(
        x => x.id == item.id
      );
      if (res) {
        if (item.estado === "N") {
          this.solicitud.proyectosColaboradorList.splice(foundIndex);
          this.getNuevoId();
          Vue.$toast.success("El proyecto fue eliminado exitosamente.", {});
        } else {
          this.solicitud.proyectosColaboradorList[foundIndex].estado = "E";

          Vue.$toast.success("El proyecto fue eliminado exitosamente.", {});
          this.solicitud.estado = "M";
        }
      }
    },
    async Excluir(item) {
      this.proyectoSeleccionado = item;
      let proyectoExcluir = `'${this.proyectoSeleccionado.nombreProyecto}' para el cliente '${this.proyectoSeleccionado.esProyectoNova == 'No'? this.proyectoSeleccionado.nombreCliente : this.proyectoSeleccionado.nombreClienteCorporacion}'`;

      const res = await this.Excluir_Generico(
        "proyecto",
        proyectoExcluir,
        "M"
      );

      const foundIndex = this.solicitud.proyectosColaboradorList.findIndex(
        x => x.id == item.id
      );
      if (res) {
          this.solicitud.proyectosColaboradorList.splice(foundIndex);
          this.getNuevoId();
          Vue.$toast.success("El proyecto fue excluido exitosamente.", {});
      }
    },
    agregar() {
      this.tituloModalEdicion = "Agregar experiencia en un proyecto";
      this.cleanForm();
      this.showModalAgregar = true;
      this.esEdicion = false;
    },
    Undo(item) {
      const foundIndex = this.solicitud.proyectosColaboradorList.findIndex(
        x => x.idexperienciaproyecto == item.idexperienciaproyecto
      );
      this.solicitud.proyectosColaboradorList[foundIndex].estado = "S";
      // Vue.$toast.success("El curso fue eliminado exitosamente.", {});
    },
    async Editar(item) {
      let loader = this.showLoader();

      this.tituloModalEdicion = this.puedePintar ? "Modificar experiencia en un proyecto" : "Ver experiencia en un proyecto";
      this.proyectoSeleccionado = item;

      //   this.proyectoNuevoEditar = item;

      this.proyectoNuevoEditar = {
        idexperienciaproyecto: item.idexperienciaproyecto,
        nombreCliente: item.nombreCliente,
        nombreContacto: item.nombreContacto,
        nombreProyecto: item.nombreProyecto,
        rol: item.rol,
        telefonoContacto: item.telefonoContacto,
        correoContacto: item.correoContacto,
        fechainicio: item.fechainicio,
        fechafinal: item.fechafinal,
        inicio: item.inicio,
        fin: item.fin,
        descripcion: item.descripcion,
        numeroLicitacion: item.numeroLicitacion,
        herramientas:item.herramientas,
        tecnologias:item.tecnologias,
        idEmpresaCorporacion: item.idEmpresaCorporacion,
        idCliente: item.idCliente,
        nombreEmpresa: item.nombreEmpresa,
        esProyectoNova: item.esProyectoNova,
        nombreEmpresaCorporacion: item.nombreEmpresaCorporacion,
        nombreClienteCorporacion: item.nombreClienteCorporacion,
        estado: item.estado,
        id: item.id,
        contactos: item.contactos
      };

      if (this.proyectoNuevoEditar.idEmpresaCorporacion && this.proyectoNuevoEditar.idEmpresaCorporacion != null) {
        await this.cargarCientesEmpresa(
          this.proyectoNuevoEditar.idEmpresaCorporacion
        );
      }

      if (this.proyectoNuevoEditar.esProyectoNova == "Si") {
        await this.cargarProyectosCliente(this.proyectoNuevoEditar.nombreClienteCorporacion, this.proyectoNuevoEditar.idCliente);        
      }
      else{
        await this.cargarProyectosCliente(this.proyectoNuevoEditar.nombreCliente, this.proyectoNuevoEditar.idCliente);        
      }

      this.showModalAgregar = true;
      this.esEdicion = true;

      this.hideLoader(loader);
    },
    async submit(formValues) {
      try {
        if (!this.visorEnable) {
          let resultadoValidacion = await this.validacionesSolicitud();

          if (resultadoValidacion.esValido) {
            // actualiza la lista interna de cursos por colaborador, previamente validada
            if (this.actualizarLista()) {
              //  this.$formulate.reset("proyectoNuevoEditar");
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
          this.editarListaProyectos();
        } else {
          this.agregarListaProyectos();
        }
        // this.cleanForm();
        return true;
      } catch (error) {
        console.log(error);
        return false;
      }
    },
    editarListaProyectos() {
      const foundIndex = this.solicitud.proyectosColaboradorList.findIndex(
        x => x.id == this.proyectoNuevoEditar.id
      );

      this.solicitud.proyectosColaboradorList[foundIndex].nombreProyecto = this.proyectoNuevoEditar.nombreProyecto;
      this.solicitud.proyectosColaboradorList[foundIndex].nombreCliente = this.proyectoNuevoEditar.nombreCliente;
      this.solicitud.proyectosColaboradorList[foundIndex].rol = this.proyectoNuevoEditar.rol;
      this.solicitud.proyectosColaboradorList[foundIndex].telefonoContacto = this.proyectoNuevoEditar.telefonoContacto;
      this.solicitud.proyectosColaboradorList[foundIndex].nombreContacto = this.proyectoNuevoEditar.nombreContacto;
      this.solicitud.proyectosColaboradorList[foundIndex].esProyectoNova = this.proyectoNuevoEditar.esProyectoNova;
      this.solicitud.proyectosColaboradorList[foundIndex].herramientas = this.proyectoNuevoEditar.herramientas;
      this.solicitud.proyectosColaboradorList[foundIndex].tecnologias = this.proyectoNuevoEditar.tecnologias;

      this.solicitud.proyectosColaboradorList[foundIndex].correoContacto = this.proyectoNuevoEditar.correoContacto;
      this.solicitud.proyectosColaboradorList[foundIndex].fechainicio = this.proyectoNuevoEditar.fechainicio;
      this.solicitud.proyectosColaboradorList[foundIndex].fechafinal = this.proyectoNuevoEditar.fechafinal;
      this.solicitud.proyectosColaboradorList[foundIndex].inicio = this.proyectoNuevoEditar.fechainicio;
      this.solicitud.proyectosColaboradorList[foundIndex].fin = this.proyectoNuevoEditar.fechafinal;
      this.solicitud.proyectosColaboradorList[foundIndex].descripcion = this.proyectoNuevoEditar.descripcion;
      this.solicitud.proyectosColaboradorList[foundIndex].numeroLicitacion = this.proyectoNuevoEditar.numeroLicitacion;
      this.solicitud.proyectosColaboradorList[foundIndex].estado = this.proyectoNuevoEditar.estado != "N" ? "M" : this.proyectoNuevoEditar.estado;
      this.solicitud.proyectosColaboradorList[foundIndex].nombreEmpresaCorporacion = this.proyectoNuevoEditar.nombreEmpresaCorporacion;
      this.solicitud.proyectosColaboradorList[foundIndex].nombreClienteCorporacion = this.proyectoNuevoEditar.nombreClienteCorporacion;
      this.solicitud.proyectosColaboradorList[foundIndex].idEmpresaCorporacion = this.proyectoNuevoEditar.idEmpresaCorporacion;
      this.solicitud.proyectosColaboradorList[foundIndex].idCliente = this.proyectoNuevoEditar.idCliente;
      this.solicitud.proyectosColaboradorList[foundIndex].nombreEmpresa = this.proyectoNuevoEditar.nombreEmpresa;
      this.solicitud.proyectosColaboradorList[foundIndex].nombreEmpresa = this.proyectoNuevoEditar.nombreEmpresa;
      this.solicitud.proyectosColaboradorList[foundIndex].contactos = this.proyectoNuevoEditar.contactos;

      this.solicitud.estado = "M";
    },
    agregarListaProyectos() {
      this.solicitud.proyectosColaboradorList.push({
        idcolaborador: this.colaborador.idcolaborador,
        nombreProyecto: this.proyectoNuevoEditar.nombreProyecto,
        nombreCliente: this.proyectoNuevoEditar.nombreCliente,
        nombreContacto: this.proyectoNuevoEditar.nombreContacto,
        rol: this.proyectoNuevoEditar.rol,
        telefonoContacto: this.proyectoNuevoEditar.telefonoContacto,
        correoContacto: this.proyectoNuevoEditar.correoContacto,
        fechainicio: this.proyectoNuevoEditar.fechainicio,
        fechafinal: this.proyectoNuevoEditar.fechafinal,
        inicio: this.proyectoNuevoEditar.fechainicio,
        fin: this.proyectoNuevoEditar.fechafinal,
        descripcion: this.proyectoNuevoEditar.descripcion,
        numeroLicitacion: this.proyectoNuevoEditar.numeroLicitacion,
        esProyectoNova: this.proyectoNuevoEditar.esProyectoNova,
        herramientas: this.proyectoNuevoEditar.herramientas,
        tecnologias: this.proyectoNuevoEditar.tecnologias,
        estado: "N",
        idEmpresaCorporacion: this.proyectoNuevoEditar.idEmpresaCorporacion,
        idCliente: this.proyectoNuevoEditar.idCliente,
        nombreEmpresa: this.proyectoNuevoEditar.nombreEmpresa,
        nombreClienteCorporacion: this.proyectoNuevoEditar.nombreClienteCorporacion,
        nombreEmpresaCorporacion: this.proyectoNuevoEditar.nombreEmpresaCorporacion,
        contactos: this.proyectoNuevoEditar.contactos,
        idexperienciaproyecto: null //this.getNuevoId() // se utiliza representativamente para el manejo del id de la tabla, y sus acciones, no es un valor almacenable, este seria un identity en bd
      });
      this.solicitud.estado = "M";
      this.getNuevoId();
    },
    getNuevoId() {
      // if (this.solicitud.proyectosColaboradorList) {
      //   if (this.solicitud.proyectosColaboradorList.length > 0) {
      //     return (
      //       _.maxBy(this.solicitud.proyectosColaboradorList, "idexperienciaproyecto")
      //         .idexperienciaproyecto + 1
      //     );
      //   }
      // }

      // return 1;
      this.solicitud.proyectosColaboradorList = this.addIdentityToArrayObjects(this.solicitud.proyectosColaboradorList);
    },
    async selected(item) {
      switch (this.TipoListaPick) {
        case "Colaboradores":
          this.showModal = false;
          await this.cargarDetalleColaborador(item.id);     
          this.solicitud.proyectosColaboradorList = [];     
          break;

        case "Proyectos":
          this.showModal = false;
          this.cargarProyecto(item); 
          break;

        default:
          break;
      }
    },
    onUpload() {
      //this.$toast.add({severity: 'info', summary: 'Success', detail: 'File Uploaded', life: 3000});
    },
    showLoader() {
      return this.$loading.show({
        // Optional parameters
        container: this.fullPage ? null : this.$refs.formContainer,
        canCancel: true,
        onCancel: this.onCancel
      });
    },
    hideLoader(loader) {
      setTimeout(() => {
        loader.hide();
      }, 300);
    },
    cleanForm() {
      this.proyectoNuevoEditar.nombreProyecto = null;
      this.proyectoNuevoEditar.nombreCliente = null;
      this.proyectoNuevoEditar.nombreContacto = null;
      this.proyectoNuevoEditar.rol = null;
      this.proyectoNuevoEditar.telefonoContacto = null;
      this.proyectoNuevoEditar.correoContacto = null;
      this.proyectoNuevoEditar.fechainicio = null;
      this.proyectoNuevoEditar.fechafinal = null;
      this.proyectoNuevoEditar.inicio = null;
      this.proyectoNuevoEditar.fin = null;
      this.proyectoNuevoEditar.descripcion = null;
      this.proyectoNuevoEditar.numeroLicitacion = null;
      this.proyectoNuevoEditar.herramientas = [];
      this.proyectoNuevoEditar.tecnologias = [];
      this.proyectoNuevoEditar.nombreClienteCorporacion = null;
      this.proyectoNuevoEditar.nombreEmpresaCorporacion = null;
      this.proyectoNuevoEditar.idCliente = null;
      this.proyectoNuevoEditar.idEmpresaCorporacion = null;
      this.proyectoNuevoEditar.idexperienciaproyecto = null;
      this.proyectoNuevoEditar.esProyectoNova = "Si";
      this.proyectoNuevoEditar.contactos = [];
      
      this.clientesList = null;
      // this.proyectoNuevoEditar = null;
    },
    async displayModal(tipo) {
      this.TipoListaPick = tipo;
      this.clean();
      switch (tipo) {
        case "Colaboradores":
          // aqui estarian los request para cargar el picklist segun se requiera
          const {
            data: { colaboradores }
          } = await GetColaboradoresPerfil(this.$root.infoColaboradorActual.id); //GetAllColaboradores();
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

          this.tituloModal = "Lista de colaboradores";
          this.showModal = true;
          this.nombreTipoLista = "Colaboradores";
          break;
        case "ProyectosNova":
          // aqui estarian los request para cargar el picklist segun se requiera
          const {
            data: { proyectos }
          } = await GetAllProyectos();

          this.dataPickList = proyectos.map(
            ({
              idproyecto,
              nombre,
              idcliente,
              nombrecliente
            }) => ({
              id: idproyecto,
              nombre: nombre,
              idcliente: idcliente,
              nombrecliente: nombrecliente
            })
          );

          this.columnasPickList =  [
            { field: "nombrecliente", header: "Cliente" },
            { field: "nombre", header: "Proyecto" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "proyecto";
          this.tituloModal = "Lista de proyectos";
          break;

        case "Proyectos":
          // aqui estarian los request para cargar el picklist segun se requiera
          const {
            data: { experienciaProyectos }
          } = await GetProyectosByColaborador(this.colaborador.idcolaborador);
          
          this.dataPickList = experienciaProyectos.map(
            ({
              idexperienciaproyecto,
              nombrecliente,
              nombreproyecto,
              inicio,
              fin,
              contacto,
              correo,
              descripcion,
              fechainicio,
              fechafinal,
              licitacion,
              rolproyecto,
              telefono,
              tecnologiaProyecto,
              herramientaProyecto,
              idempresacorporacion,
              idcliente,
              nombreempresa,
              nombreempresacorporacion,
              nombreclienteempresacorporacion,
              contactos
            }) => ({
              id: idexperienciaproyecto,
              nombreCliente: (idempresacorporacion == null ? nombrecliente : nombreclienteempresacorporacion),
              nombreProyecto: nombreproyecto,
              inicio: aplicarFormatoFecha(fechainicio, "yyyy-MM-dd"),
              fin: aplicarFormatoFecha(fechafinal, "yyyy-MM-dd"),
              nombreContacto: contacto,
              correoContacto: correo,
              descripcion: descripcion,
              numeroLicitacion: licitacion,
              fechainicio: aplicarFormatoFecha(fechainicio, "yyyy-MM-dd"),
              fechafinal: aplicarFormatoFecha(fechafinal, "yyyy-MM-dd"),
              rol: rolproyecto,
              telefonoContacto: telefono,
              tecnologias: tecnologiaProyecto,
              herramientas: herramientaProyecto,
              idEmpresaCorporacion:idempresacorporacion,
              idCliente: idcliente,
              nombreEmpresa: nombreempresa,
              nombreEmpresaCorporacion: nombreempresacorporacion,
              nombreClienteCorporacion: nombreclienteempresacorporacion,
              contactos: contactos
            })
          );

          this.columnasPickList = [
            { field: "nombreCliente", header: "Cliente" },
            { field: "nombreProyecto", header: "Proyecto" },
            { field: "inicio", header: "Inicio" },
            { field: "fin", header: "Fin" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "proyecto";
          this.tituloModal = "Lista de proyectos";
          break;
        default:
          break;
      }
    },
    getJsonHerramientasTecnologias(lista){
      let arregloDatos = lista.split(",");
      let jsonObject = [];

      if (arregloDatos.length > 0) {
        arregloDatos.forEach(function (item, index) {
          jsonObject.push({"key":item,"value":item});
        });
      }
      return jsonObject;
    },
    clean() {
      this.showModal = false;
      this.nombreTipoLista = "";
      this.dataPickList = null;
      this.filters = {};
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
    cargarProyecto(proyecto) {
      this.cargandoDatos = true;
      let ref = this;

      // se determina si ya el proyecto está en la lista
      const foundIndex = this.solicitud.proyectosColaboradorList.findIndex(
        x => x.idexperienciaproyecto == proyecto.id
      );

      if (foundIndex == -1) {
        proyecto.contactos.forEach(function (element) {
          element.estado = "S";
          
          element.hashCode = ref.hashCode(element.contacto + element.telefono + element.correo);
        });

        this.solicitud.proyectosColaboradorList.push({
          ...proyecto,
          idexperienciaproyecto: proyecto.id,
          tecnologias: this.getJsonHerramientasTecnologias(proyecto.tecnologias),
          herramientas: this.getJsonHerramientasTecnologias(proyecto.herramientas),
          nombreProyecto: proyecto.nombreProyecto.trim() == ""? "Sin nombre" : proyecto.nombreProyecto,

          esProyectoNova: (proyecto.idEmpresaCorporacion != null ? "Si" : "No"),
          estado: "S",
        });        

        this.solicitud.estado = "M";
        this.getNuevoId();
      }
      else{
          Vue.$toast.error("El proyecto seleccionado ya está en la lista.", {});
      }
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
        docSolicitud: JSON.stringify({ ...this.colaborador.idcolaborador, ...this.solicitud })
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
    //   //this.$refs.form.$el.submit()
    //   alert("Método Eliminar");
    // }
    async submitSolicitud(formValues) {
      try {
        if (this.solicitud.estado == common.SOLICITUD_SINMODIFICACIONES) {
          Vue.$toast.warning(
            "La solicitud no tiene modificaciones que guardar.",
            {}
          );
        } else if (this.experienciasSinModificaciones()) {  
            Vue.$toast.warning(
            "Por favor excluir las experiencias en proyectos que no tienen modificaciones.",
            {}
          );
        } else if (this.contactosIncompletos()) {
          Vue.$toast.warning(
            "Por favor revisar la información de contactos. Indique al menos correo o teléfono.",
            {}
          );
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
    async validacionesSolicitud() {
      let resultado = { esValido: true };

      // si es un proyecto de NOVA se valida el ID de empresa de Nova así
      // como el ID del cliente según la empresa
      if (this.proyectoNuevoEditar.esProyectoNova == "Si") {
        if(!this.proyectoNuevoEditar.idEmpresaCorporacion){
          resultado = {
            esValido: false,
            mensaje: "Debe seleccionar la empresa de la corporación"
          };
        }
        else if(!this.proyectoNuevoEditar.idCliente){
          resultado = {
            esValido: false,
            mensaje: "Debe seleccionar el cliente para el que realizó el proyecto"
          };
        }
      }
      // sino se valida que haya registrado el nombre de la empresa
      // y el nombre del cliente
      else{
        if(!this.proyectoNuevoEditar.nombreEmpresa)
          resultado = {
            esValido: false,
            mensaje: "Debe registrar el nombre de la empresa con la cual realizó el proyecto"
          };
        else if(!this.proyectoNuevoEditar.nombreCliente)
          resultado = {
            esValido: false,
            mensaje: "Debe registrar el nombre del cliente para el que realizó el proyecto"
          };
      }

      // si no hay errores de validación revisar herramientas y tecnologías
      if (resultado.esValido === true) {
        if(this.proyectoNuevoEditar.herramientas && this.proyectoNuevoEditar.herramientas.length == 0)
          resultado = {
            esValido: false,
            mensaje: "Por favor indique las herramientas utilizadas en el proyecto"
          };
        else if(this.proyectoNuevoEditar.tecnologias && this.proyectoNuevoEditar.tecnologias.length == 0)
          resultado = {
            esValido: false,
            mensaje: "Por favor indique las tecnologías utilizadas en el proyecto"
          };
      }

      // si no hay errores de validación revisar que si se repite el proyecto para un cliente
      // las fechas de inicio y fin de la nueva experiencia no se traslapen con lo previo
      // if (resultado.esValido === true && this.proyectoNuevoEditar.estado == "N") {
      //    let datos = {
      //                   idColaborador: this.colaborador.idcolaborador,
      //                   nombreCliente: this.proyectoNuevoEditar.nombreCliente ? this.proyectoNuevoEditar.nombreCliente : this.proyectoNuevoEditar.nombreClienteCorporacion,
      //                   nombreProyecto: this.proyectoNuevoEditar.nombreProyecto,
      //                   fechaInicio: this.proyectoNuevoEditar.fechainicio,
      //                   fechaFin: this.proyectoNuevoEditar.fechafinal
      //                 };
      //    await ValidarProyecto(datos)
      //          .then(response => {
                        
      //             // objeto devuelto por el API
      //             if(!response.data.nuevaExperienciaValida){
      //               resultado = {
      //                             esValido: false,
      //                             mensaje: "Ya tiene registrada una experiencia para el mismo proyecto pero las fechas se traslapan, por favor corregir."
      //                           };            
      //             }
      //           })
      //          .catch();
      // }
      
      return resultado;
    },
    experienciasSinModificaciones(){
        const foundIndex = this.solicitud.proyectosColaboradorList.findIndex(
        x => x.estado == "S"
      );
      return foundIndex >= 0;
    },

    contactosIncompletos() {
      let list = [];

      list = this.solicitud.proyectosColaboradorList.find(x =>
        x.contactos.find(c => {
          return !c.correo && !c.telefono;
        })
      );

      return !!list;
    },

    async getListaEmpresasCorporacion() {
      const {
        data: { empresaCorporacions }
      } = await GetEmpresaCorporacion();

      this.empresasList = empresaCorporacions.map(
        ({ idempresacorporacion, nombre }) => ({
          value: idempresacorporacion,
          label: nombre
        })
      );
    },
    async getListaHerramientas(){
      const {
        data: { herramientas, tecnologias }
      } = await GetListaHerramientas();

      this.herramientasList = herramientas.map(
        ({ key, value }) => ({
          value: value,
          key: key
        })
      );

      this.tecnologiasList = tecnologias.map(
        ({ key, value }) => ({
          value: value,
          key: key
        })
      );
    },
    async onChangeEsProyectoNova() {  
      if(this.proyectoNuevoEditar.esProyectoNova == "No"){
        this.proyectoNuevoEditar.idEmpresaCorporacion = null;
        this.proyectoNuevoEditar.idCliente = null;
        this.proyectoNuevoEditar.nombreEmpresaCorporacion = null;
        this.proyectoNuevoEditar.nombreClienteCorporacion = null;
      }
      else{
        this.proyectoNuevoEditar.nombreEmpresa = "";
        this.proyectoNuevoEditar.nombreCliente = "";
      }
    },
    async onChangeEmpresa(evento, empresa) {
        
      await this.cargarCientesEmpresa(
        this.proyectoNuevoEditar.idEmpresaCorporacion
      );
      this.proyectoNuevoEditar.idCliente = null;

      let indice = -1;
      for (let index = 0; index < evento.target.length; index++) {
        const element = evento.target[index];
        if (element.value == evento.target.value) {
          indice = index;
        }
      }
      this.proyectoNuevoEditar.nombreEmpresaCorporacion = evento.target[indice].label;
    },
    async onChangeCliente(evento, cliente) {        
      let indice = -1;
      for (let index = 0; index < evento.target.length; index++) {
        const element = evento.target[index];
        if (element.value == evento.target.value) {
          indice = index;
          break;
        }
      }
      this.proyectoNuevoEditar.nombreClienteCorporacion = evento.target[indice].label;

      await this.cargarProyectosCliente(this.proyectoNuevoEditar.nombreClienteCorporacion, this.proyectoNuevoEditar.idCliente);
    },
    async cargarCientesEmpresa(idEmpresa) {      
      await GetClientesEmpresa(idEmpresa)
        .then(response => {          
          // objeto devuelto por el API
          var clientes = response.data.clientesEmpresa;

          this.clientesList = clientes.map(
            ({ idCliente, nombre }) => ({
              label: nombre,
              value: idCliente
            })
          );
        })
        .catch(response => {
          console.log(response);
        });
    },
    async cargarProyectosCliente(nombreCliente, idCliente) {   
      this.proyectosClienteList = [];   

      await GetProyectosCliente(nombreCliente, idCliente)
        .then(response => {          
          // objeto devuelto por el API
          var proyectos = response.data.proyectos;

          this.proyectosClienteList = proyectos.map(
            ({ nombreProyecto }) => ({
              nombre: nombreProyecto
            })
          );
        })
        .catch(response => {
          console.log(response);
        });
    },
    async onChangeNombreCliente(){
      let loader = this.showLoader();
      await this.cargarProyectosCliente(this.proyectoNuevoEditar.nombreCliente, this.proyectoNuevoEditar.idCliente);
      this.hideLoader(loader);
    },
    validarFechaFinal: function(context) {
        const { value, getFormValues } = context;
        var ToDate = new Date();
        var valueDate = new Date(value);
        var valoresFormulario = getFormValues();

        if (valueDate > ToDate) {
          return false;
        }

        if (new Date(valoresFormulario.fechainicio) > valueDate) {
          return false;
        }
        
        return true;
    },
    showOverlayPanelProyectos(event) {
      if (this.proyectosClienteList 
          && this.proyectosClienteList.length > 0
          && (this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
              this.datosSolicitud.estadoSolicitud == common.CORREGIR)) {

          this.$refs.showOverlayPanelProyectos.show(event);

      }
    },
    onProyectoClienteSelect(event){
      //this.proyectoNuevoEditar.nombreProyecto = this.proyectoClienSeleccionado.nombre;
      this.proyectoNuevoEditar.nombreProyecto = this.proyectoClienSeleccionadoList.nombre;
      this.$refs.showOverlayPanelProyectos.hide();
    },
    /**
     * Métodos para administrar la lista de contactos de una experiencia en un proyecto
     */
    undoContacto(index) {
      if (this.proyectoNuevoEditar.contactos[index].hashCode ==
          this.hashCode(this.proyectoNuevoEditar.contactos[index].contacto + 
                        this.proyectoNuevoEditar.contactos[index].telefono + 
                        this.proyectoNuevoEditar.contactos[index].correo)) {
        this.proyectoNuevoEditar.contactos[index].estado = "S";
      } else {
        this.proyectoNuevoEditar.contactos[index].estado = "M";
      }
    },
    agregarContacto() {
      this.proyectoNuevoEditar.contactos[
        this.proyectoNuevoEditar.contactos.length - 1
      ].estado = "N";
    },
    modificarContacto(index) {
      if (this.proyectoNuevoEditar.contactos[index].estado === "S") {
        this.proyectoNuevoEditar.contactos[index].estado = "M";
      }
    },
    eliminarContacto(index) {
      this.proyectoNuevoEditar.contactos[index].estado = "E";
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
    cursoAEliminar() {
      return this.proyectoEliminar;
    },
    puedePintar() {
      return this.habilitarControlesSolicitud(this.datosSolicitud.estadoSolicitud, this.origen);
      // return (
      //   this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
      //   this.datosSolicitud.estadoSolicitud == common.CORREGIR
      // );
    },
    puedePintarAdd() {
      return this.datosSolicitud.estadoSolicitud == common.REGISTRADA;
    },
    pickPuedeBuscarColaborador() {
      return this.datosSolicitud.estadoSolicitud == common.REGISTRADA; // && !(this.usuarioActual.Roles.length == 1 && this.usuarioActual.Roles[0] == "COLABORADOR") ;
    },
    eliminada() {
      return this.proyectoNuevoEditar.estado === "E";
    },
    esProyectoNovacomp() {
      return this.proyectoNuevoEditar && this.proyectoNuevoEditar.esProyectoNova &&  this.proyectoNuevoEditar.esProyectoNova == "Si";
    },
    nombreEmpresaValido(){
      return this.proyectoNuevoEditar && this.proyectoNuevoEditar.nombreEmpresa && this.proyectoNuevoEditar.nombreEmpresa.trim() != "";
    },
    nombreClienteValido(){
      return this.proyectoNuevoEditar && this.proyectoNuevoEditar.nombreCliente && this.proyectoNuevoEditar.nombreCliente.trim() != "";
    },
    empresaCorporacionValido(){
      return this.proyectoNuevoEditar && this.proyectoNuevoEditar.idEmpresaCorporacion && this.proyectoNuevoEditar.idEmpresaCorporacion != 0;
    },
    clienteValido(){
      return this.proyectoNuevoEditar && this.proyectoNuevoEditar.idCliente && this.proyectoNuevoEditar.idCliente != 0;
    }
  },
  watch: {
    /**
     * Controla el cambio de alguno de los campos de cursos, esto permitirá marcar
     * el documento con un estado de modificado
     */
    //  solicitud: function(newValue, oldValue) {
    // if (!this.cargandoDatos) {
    //   this.solicitud.estado = "M";
    // }

    // },
    solicitud: {
      handler(newValue, oldValue) {
        if (!this.cargandoDatos) {
          // concurrencia -- async - await is the best way to use it

          if (oldValue !== null) {
            // console.log(newValue);
            //  console.log(oldValue);
            this.solicitud.estado = "M";
          }

          //
        }
      } //,
      // deep: true
    },
    colaborador: function(newValue, oldValue) {
      if (!this.cargandoDatos) {
        this.solicitud.estado = "M";
      }
    }
  }
};
</script>

<style>
.iconTrash {
  color: #e55353;
}
.iconUndo {
  color: greenyellow;
}
.iconExclude {
  color: seagreen;
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

.hint {
  font-size: 0.705rem !important;
}

/* tamaño de letra el componente de paginación */
.p-paginator-icon {
  font-size: 0.875rem !important;
}

/* Responsive: en visualización normal se ocultan las etiquetas asociadas al título de cada columna */
.p-datatable-formacion .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-formacion .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-formacion .p-datatable-tbody > tr > td {
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

.p-listbox-item:nth-of-type(odd) {
  background-color:rgba(173, 216, 230, 0.5);
}


.p-datatable .p-datatable-tbody > tr > td {
    border: 1px solid rgba(175, 189, 216, 1)
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
  .p-datatable.p-datatable-formacion .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-formacion .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-formacion .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-formacion .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-formacion:last-child {
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

.tags-input-badge-selected-default{
  color: white !important;
  background-color: #29abe2 !important;
}
.tags-input-remove:before, .tags-input-remove:after{
  background-color: lightgrey !important;
}
.typeahead-dropdown{
  overflow: auto;
  height: 140px;
}
</style>
