<!-- Plantilla de Carga de incapacidades -->
<template>
  <div>
    <CCard>
      <CCardHeader>
        <CIcon name="cil-justify-center" /><strong>
          Cargar archivo de incapacidades</strong
        >
        <!-- <CBadge color="primary" class="ml-2" shape="pill">
          <span style="margin-top:.2rem; margin-bottom:.2rem">
            Carga de archivo
          </span>
        </CBadge> -->

        <!-- Acciones -->
        <div class="card-header-actions">
          <CButton
            block
            color="primary"
            @click="historial()"
            class="mr-1 libro-marca-azuloscuro"
          >
            <CIcon name="cil-file" />&nbsp;Regresar a la lista</CButton
          >
        </div>
      </CCardHeader>
      <CCardBody>
        <FormulateForm
          v-model="carga"
          @submit="submit()"
          @failed-validation="errorValidacion"
          :invalid-message="this.invalidMessage"
        >
          <h2 class="text-divider">
            <span>Seleccione un archivo para la carga</span>
          </h2>

          <CRow>
            <CCol sm="6">
              <label>Archivo de incapacidades</label>
              <FormulateInput
                type="myfileupload"
                name="archivo"
                help="Formatos válidos: .csv"
                input-class="form-control"
                validation-name="mime:image/jpeg,image/png,image/gif,application/pdf"
                class="hint"
                tipo-documento="Archivo de incapacidades"
                estado-solicitud="RE"
                :eliminada="false"
                :visor-padre="false"
                :habilitar-controles="true"
                :isCSV="true"
                @showModal="showModalVisor"
              />
            </CCol>
            <CCol md="3" sm="12" class="mt-2 pull-left">
              <div style="text-align: left">
                <label></label>
                <CButton
                  block
                  color="primary"
                  @click="verDetalle()"
                  class="mr-1 btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark btn-primary"
                >
                  <CIcon name="cil-file" /> Ver archivo de carga</CButton
                >
              </div>
            </CCol>
          </CRow>

          <FormulateErrors />

          <CRow>
            <CCol sm="12" class="mt-4 mb-3">
              <!-- Guardar -->
              <CButton
                class="pull-right btn btn-info libro-marca-celeste onHoverDark"
                type="submit"
              >
                <CIcon name="cil-save" />
                Enviar
              </CButton>
            </CCol>
          </CRow>
        </FormulateForm>
      </CCardBody>
    </CCard>

    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
    <comment-dialogue ref="commentDialogue"></comment-dialogue>

    <!-- Popup para ver el archivo del detalle -->
    <Dialog
      :visible.sync="showArchivo"
      :style="{ width: '85vw' }"
      :modal="true"
    >
      <template #header>
        <h5>Detalle del archivo de carga</h5>
      </template>
      <CRow class="pt-2">
        <CCol sm="12" lg="12" class="pull-right">
          <DataTable
            ref="dt"
            :value="listaArchivo"
            :paginator="true"
            class="p-datatable-consultas p-datatable-striped p-datatable-sm"
            :rows="10"
            dataKey="idcargaincapacidades"
            :rowHover="true"
            :filters.sync="filtersDetalle"
            :loading="loadingDetalle"
            paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
            :rowsPerPageOptions="[5, 10, 25, 50]"
            currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} cargas"
            sortField="nombre"
            :sortOrder="1"
            :autoLayout="true"
          >
            <template #header>
              <CRow>
                <CCol md="9" sm="12" class="mb-2">
                  <div class="table-header">
                    Buscar
                    <span class="p-input-icon-left w-75">
                      <i class="pi pi-user" />
                      <InputText
                        v-model="filtersDetalle['global']"
                        placeholder="Escribe aquí lo que buscas"
                        class="w-50"
                      />
                    </span>
                  </div>
                </CCol>
              </CRow>
            </template>
            <template #empty>
              No se encontraron cargas de incapacidades.
            </template>
            <template #loading>
              Cargando la información de cargas de incapacidades. Por favor
              espere.
            </template>

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
                <!-- <span class="p-column-title">Identificación</span> -->
                {{ formatoIdentificacion(slotProps.data["identificacion"]) }}
              </template>
            </Column>

            <Column
              field="nombre"
              header="Nombre del colaborador"
              sortField="nombre"
              filterField="nombre"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:22%;"
            >
              <template #body="slotProps">
                <!-- <span class="p-column-title">Nombre del colaborador</span> -->
                {{ slotProps.data.nombre }}
              </template>
            </Column>

            <Column
              field="numboleta"
              sortField="numboleta"
              header="Número de boleta"
              filterField="numboleta"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:14%;"
            >
              <template #body="slotProps">
                <!-- <span class="p-column-title">Número de boleta</span> -->
                <!-- {{ slotProps.data.numeroboleta }} -->
                {{ slotProps.data["numboleta"] }}
              </template>
            </Column>

            <Column
              field="fechainicio"
              header="Fecha de inicio"
              :sortable="true"
              sortField="fechainicio"
              filterField="fechainicio"
              filterMatchMode="contains"
              headerStyle="width:12%;"
            >
              <template #body="slotProps">
                <!-- <span class="p-column-title">Fecha de inicio</span> -->
                {{ slotProps.data["fechainicio"] }}
              </template>
            </Column>
            <Column
              field="fechafin"
              :sortable="true"
              header="Fecha de finalización"
              sortField="fechafin"
              filterField="fechafin"
              filterMatchMode="contains"
              headerStyle="width:14%;"
            >
              <template #body="slotProps">
                <!-- <span class="p-column-title">Fecha de finalización</span> -->
                {{ slotProps.data["fechafin"] }}
              </template>
            </Column>

            <Column
              field="cantdias"
              sortField="cantdias"
              header="Cantidad de días"
              filterField="cantdias"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:12%;"
            >
              <template #body="slotProps">
                <!-- <span class="p-column-title">Cantidad de días</span> -->
                {{ slotProps.data["cantdias"] }}
              </template>
            </Column>

            <Column
              field="tipo"
              sortField="tipo"
              header="Tipo"
              filterField="tipo"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:10%;"
            >
              <template #body="slotProps">
                <!-- <span class="p-column-title">Tipo</span> -->
                {{ slotProps.data["tipo"] }}
              </template>
            </Column>
          </DataTable>
        </CCol>
      </CRow>

      <template #footer>
        <!-- <CButton                            
              color="primary"
              @click="crearSolicitud()"
              class="mr-1  btn btn-info libro-marca-azuloscuro ">
              <span class="pi pi-check"></span> <span>Crear</span>
            </CButton> -->
        <CButton
          color="primary"
          @click="showArchivo = false"
          class="mr-1  btn btn-info libro-marca-celeste"
        >
          <span class="pi pi-times"></span> <span>Cancelar</span>
        </CButton>
      </template>
    </Dialog>
  </div>
</template>

<script>
import moment from "moment";
import Vue from "vue";
moment.locale("es");
// Métodos de llamados a API
import { CargaIncapacidades } from "./request";

// métodos comunes
import common from "../masterdata/common.js";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";

// Definición del componente DatosPersonales
export default {
  name: "CargaIncapacidad",
  mixins: [common],
  components: {
    "confirm-dialogue": ConfirmDialogue,
    "comment-dialogue": CommentDialogue
  },

  data() {
    return {
      carga: {
        // values de carga
        archivo: null
      },
      archivoJson: null,
      showModal: false, // controla la presentación del modal
      nombreTipoLista: "", // referencia el tipo de contenido de un pick
      tituloModal: "", // título para la ventana modal dependiendo del tipo de contenido del pick
      columnasPickList: [], // lista de columnas a mostrar en el componente tabla del pick
      dataPickList: [], // datos para el componente tabla del pick
      mensajeError: common.MENSAJE_ERROR,
      visorEnable: false,
      showArchivo: false,
      listaArchivo: [],
      loading: true,
      filters: {},
      loadingDetalle: true,
      filtersDetalle: {}
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...
  },

  methods: {
    formatoIdentificacion(id) {
      if (id) {
        return id.substring(2, id.length);
      } else {
        return "";
      }
    },
    historial() {
      this.$router.push({ name: "Historial de incapacidad" });
    },
    formatearFecha(date) {
      return moment(date).format("L");
    },
    showModalVisor(show) {
      this.visorEnable = show;
    },
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        if (fields[key].type !== "group") {
          fieldNames += `-  ${fields[key].label}\r\n`;
        } else {
          switch (fields[key].name) {
            case "afiliaciones":
              fieldNames += "-  Sección de otras afiliaciones\r\n";
              break;

            default:
              break;
          }
        }
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    /**
     * Atiende el evento "failed-validation" del FormulateForm
     * El método muestra un mensaje temporal indicando que el
     * formulario presenta errores de validación
     *
     * @data contiene la lista de validaciones que no se cumplen
     *  */

    errorValidacion(data) {
      Vue.$toast.warning(common.MENSAJE_ERROR, {
        // optional options Object
      });
    },

    ValidaFile(data) {
      let countColumns = Object.keys(data).length;

      if (countColumns != 7) {
        Vue.$toast.error(
          "La cantidad de columnas del archivo es incorrecta, deben ser 7 columnas.",
          {}
        );
        return false;
      }

      if (!Object.keys(data).includes("IDENTIFICACION")) {
        Vue.$toast.error(
          "El archivo no contiene la columna IDENTIFICACION.",
          {}
        );
        return false;
      }

      if (!Object.keys(data).includes("NOMBRE")) {
        Vue.$toast.error("El archivo no contiene la columna NOMBRE.", {});
        return false;
      }

      if (!Object.keys(data).includes("No. BOLETA")) {
        Vue.$toast.error("El archivo no contiene la columna No. BOLETA.", {});
        return false;
      }

      if (!Object.keys(data).includes("FECHA INICIO")) {
        Vue.$toast.error("El archivo no contiene la columna FECHA INICIO.", {});
        return false;
      }

      if (!Object.keys(data).includes("FECHA FIN")) {
        Vue.$toast.error("El archivo no contiene la columna FECHA FIN.", {});
        return false;
      }

      if (!Object.keys(data).includes("DIAS INCAP")) {
        Vue.$toast.error("El archivo no contiene la columna DIAS INCAP.", {});
        return false;
      }

      if (!Object.keys(data).includes("DESC. INCAPACIDAD")) {
        Vue.$toast.error(
          "El archivo no contiene la columna DESC. INCAPACIDAD.",
          {}
        );
        return false;
      }

      return true;
    },

    /**
     * Obtiene los datos del formulario para el submit
     */
    async submit(formValues) {
      try {
        if (!this.visorEnable) {
          let resultadoValidacion = this.validacionesSolicitud();
          if (resultadoValidacion.esValido) {
            const res = await this.Confirmacion_Generico(
              "Desea cargar el archivo de incapacidades",
              "Carga de incapacidades"
            );
            var result = "";

            if (res) {
              const reader = new FileReader();
              reader.readAsText(this.carga.archivo);
              const $this = this;
              reader.onload = async function(e) {
                const text = e.target.result;
                let data = $this.csvToArray(text);

                if ($this.ValidaFile(data[0])) {
                  data = $this.removeEmptyLines(data);
                  $this.archivoJson = JSON.stringify(data);

                  await CargaIncapacidades(
                    $this.carga.archivo,
                    $this.$root.infoColaboradorActual.id,
                    $this.archivoJson,
                    data.length
                  )
                    .then(response => {
                      $this.cleanForm();
                      Vue.$toast.success(
                        "El archivo fue cargado exitosamente",
                        {}
                      );
                      $this.$router.push({ name: "Historial de incapacidad" });
                    })
                    .catch(error => {
                      if (error.response.data.message.length > 0) {
                        Vue.$toast.error(error.response.data.message[0], {});
                      } else {
                        Vue.$toast.error("No se pudo cargar el archivo", {});
                      }
                    });
                }
              };
            }
          } else {
            Vue.$toast.warning(
              "La solicitud presenta las siguientes inconsistencias: <br/>" +
                resultadoValidacion.mensaje,
              {}
            );
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    validacionesSolicitud() {
      let resultado = { esValido: true };

      if (this.carga.archivo == null || this.carga.archivo == "") {
        resultado = {
          esValido: false,
          mensaje: "Debe adjuntar el archivo de carga."
        };
      }

      return resultado;
    },

    onUpload() {
      //this.$toast.add({severity: 'info', summary: 'Success', detail: 'File Uploaded', life: 3000});
    },
    clean() {
      this.showModal = false;
      this.nombreTipoLista = "";
      this.dataPickList = null;
    },
    cleanForm() {
      this.carga = {
        archivo: null
      };
    },

    verDetalle() {
      if (this.carga.archivo) {
        const reader = new FileReader();
        reader.readAsText(this.carga.archivo);
        const $this = this;
        reader.onload = async function(e) {
          const text = e.target.result;
          let data = $this.csvToArray(text);

          if ($this.ValidaFile(data[0])) {
            $this.showArchivo = true;
            data = $this.removeEmptyLines(data);

            const options = data.map(function(row) {
              return {
                identificacion: row["IDENTIFICACION"],
                nombre: row["NOMBRE"],
                numboleta: row["No. BOLETA"],
                fechainicio: row["FECHA INICIO"],
                fechafin: row["FECHA FIN"],
                cantdias: row["DIAS INCAP"],
                tipo: row["DESC. INCAPACIDAD"]
              };
            });

            $this.listaArchivo = options;

            $this.loadingDetalle = false;
          }
        };
      } else {
        Vue.$toast.warning("Debe de cargar un archivo.", {});
      }
    },
    csvToArray(str, delimiter = ",") {
      // slice from start of text to the first \n index
      // use split to create an array from string by delimiter
      const headers = str.slice(0, str.indexOf("\n")).split(delimiter);

      // slice from \n index + 1 to the end of the text
      // use split to create an array of each csv value row
      const rows = str.slice(str.indexOf("\n") + 1).split("\n");

      // Map the rows
      // split values from each row into an array
      // use headers.reduce to create an object
      // object properties derived from headers:values
      // the object passed as an element of the array
      const arr = rows.map(function(row) {
        const values = row.split(delimiter);
        const el = headers.reduce(function(object, header, index) {
          object[header.replace(";", "").trim()] = values[index];
          return object;
        }, {});
        return el;
      });

      // return the array
      return arr;
    },
    removeEmptyLines(arr) {
      return arr.filter(function(item) {
        return item.IDENTIFICACION != null && item.IDENTIFICACION != "";
      });
    }
  },
  computed: {
    today() {
      return new Date();
    }
  },
  watch: {
    // colaborador: function(newValue, oldValue) {
    //   if (!this.cargandoDatos) {
    //     this.colaborador.estado = "M";
    //   }
    // }
    //   solicitud: {
    // 	get: function() {
    // 	},
    // 	set: function(newValue) {
    // 	}
    // }
  }
};
</script>

<style>
.form-control:disabled,
.form-control[readonly] {
  background-color: #e7e7e7 !important;
  color: #767676 !important;
}

.text-divider {
  margin: 2em 0;
  line-height: 0;
  font-size: 0.875rem;
}
.text-divider span {
  background-color: #9c9c9c !important;
  color: #ffffff;
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
.avatar-upload .avatar-edit {
  position: absolute;
  right: 12px;
  z-index: 1;
  top: 10px;
}
.avatar-upload .avatar-edit input {
  display: none;
}
.avatar-upload .avatar-edit input + label {
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
.avatar-upload .avatar-edit input + label:hover {
  background: #f1f1f1;
  border-color: #d6d6d6;
}
.avatar-upload .avatar-edit input + label:after {
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
.p-button {
  background-color: #29abe2;
}
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
.libro-marca-celeste .disabled {
  background-color: #29abe2 !important;
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

/* tamaño de letra el componente de paginación */
.p-paginator-icon {
  font-size: 0.875rem !important;
}
.hint {
  font-size: 0.705rem !important;
  margin-top: 1%;
}

/* Responsive: en visualización normal se ocultan las etiquetas asociadas al título de cada columna */
.p-datatable-picklist .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-picklist .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-picklist .p-datatable-tbody > tr > td {
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

.p-datatable table {
  width: 99%;
  background-color: #f8f8f8;
}

.p-datatable .p-paginator-bottom {
    width: 99%;
}

.p-datatable.p-datatable-striped
  .p-datatable-tbody
  > tr:nth-child(even).p-highlight {
  background: rgba(12, 123, 190, 0.5);
  /* color: #495057; */
}

.p-dialog-mask {
  z-index: 1049 !important;
}
.btn-opciones {
  display: none;
}

.formulate-form-errors {
  background-color: red;
  color: white;
  white-space: pre-wrap;
}

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de
   etiqueta y valor asociado */
@media screen and (max-width: 40em) {
  .p-datatable.p-datatable-picklist .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-picklist .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-picklist .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-picklist .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-picklist:last-child {
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

  .ajustarTamano {
    width: -webkit-fill-available;
  }

  .alinearDerecha {
    text-align: right !important;
  }
  .alinearIzquierda {
    text-align: left !important;
  }
  .alinearCentro {
    text-align: center !important;
  }
}
/* FIN */
</style>
