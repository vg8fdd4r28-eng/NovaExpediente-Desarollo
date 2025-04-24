<template>
  <div>
    <CRow>
      <CCol sm="12" md="2">
        <CButton
          block
          color="primary"
          @click="goBack()"
          class="btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark btn-primary"
        >
          Volver</CButton
        >
      </CCol>
    </CRow>
    <CCard
      style="
        border-radius: 16px !important;
        margin-top: 20px;
        margin-bottom: 20px;
      "
    >
      <CCardHeader
        style="
          border-radius: 16px !important;
          border-bottom: transparent !important;
        "
      >
        <CCol sm="12" md="12">
          <strong>{{ header }}</strong>
        </CCol>
        <div v-if="subHeader" class="sub-header">
        <CCol sm="12" md="12">
          <h5>{{ subHeader }}</h5>
        </CCol>
      </div>
      </CCardHeader>
      <CCardBody
        style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 20px"
      >
        <FormulateForm
          name="formulario"
          v-model="curriculum"
          class="mt-3"
          @submit="submit()"
          @failed-validation="validationError"
          :invalid-message="this.invalidMessage"
        >
          <div>
            <CRow>
              <CCol sm="5">
                <label>Documento</label>
                <FormulateInput
                  type="myfileupload"
                  name="documento"
                  help=""
                  input-class="form-control"
                  validation-name="mime:image/jpeg,image/png,image/gif,application/pdf"
                  class="hint"
                  tipo-documento="Curriculum Vitae"
                  estado-solicitud="RE"
                  :eliminada="false"
                  :visor-padre="false"
                  :habilitar-controles="true"
                  :isCSV="false"
                  @showModal="showModalVisor"
                />
              </CCol>
            </CRow>
            <CRow>
              <CCol sm="10" />
              <CCol sm="2" style="padding: 6px">
                <CButton
                  block
                  color="primary"
                  type="submit"
                  class="btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark btn-primary"
                  :disabled="action === 'VIEW'"
                >
                  Subir Curriculum</CButton
                >
              </CCol>
            </CRow>
            <hr />
          </div>
        </FormulateForm>
      </CCardBody>
    </CCard>
    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
  </div>
</template>

<script>
import Vue from "vue";
import common from "../../masterdata/common.js";
import ConfirmDialogue from "../../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import { CargarCV } from "../request";

export default {
  name: "formCurriculum",
  mixins: [common],
  components: { ConfirmDialogue },
  emits: ["view-list"],
  props: ["curriculumId", "formAction", "selectedCurriculumVitae"],
  data() {
    return {
      id: this.curriculumId,
      action: this.formAction,
      visorEnable: false,
      curriculum: {
        idColaborador: null,
        idCurriculum: null,
        correo: "",
        tipoDocumento: "",
        nombreDocumento: "",
        documento: null,
        fecha: ""
      },
      curriculumEdit: this.selectedCurriculumVitae,
      currentCurriculum: [],
      loading: true,
      responsiveOptions: [
        {
          breakpoint: "1024px",
          numVisible: 5
        },
        {
          breakpoint: "768px",
          numVisible: 3
        },
        {
          breakpoint: "560px",
          numVisible: 1
        }
      ],
      images: [],
      esEdit: false,
      header: "",
      subHeader: "",
    };
  },
  async mounted() {
    this.subHeader = this.action === "EDIT" ? "Editar Currículum Vitae" : "Crear Currículum Vitae";
    this.loading = false;
  },
  methods: {
    showModalVisor(show) {
      this.visorEnable = show;
    },
    goBack() {
      this.$emit("view-list", "lista-curriculum-vitae");
    },
    reset() {
      this.curriculum = {
        idColaborador: null,
        idCurriculum: null,
        correo: "",
        tipoDocumento: "",
        documento: null
      };
      this.id = null;
      this.currentCurriculum = [];
    },
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        fieldNames += `-  ${fields[key].label}\r\n`;
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    validationError() {
      Vue.$toast.warning(common.MENSAJE_ERROR, {
      });
    },
    async submit() {
    function base64ToFile(base64, fileName, contentType = '') {
        const byteCharacters = atob(base64);
        const byteArrays = [];

        for (let offset = 0; offset < byteCharacters.length; offset += 512) {
            const slice = byteCharacters.slice(offset, offset + 512);
            const byteNumbers = new Array(slice.length);
            for (let i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }
            const byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        return new File(byteArrays, fileName, { type: contentType });
    }

    try {
        this.curriculum.idColaborador = this.$root.infoColaboradorActual.id;
        this.curriculum.correo = this.$root.infoColaboradorActual.correo;
        const res = await this.Confirmacion_Generico(
            "Desea cargar el archivo de curriculum",
            "Carga de curriculum"
        );

        if (res) {
            let requestData;
            let file; // Declarar `file` aquí para que sea accesible fuera de los bloques condicionales.
            const fileInput = document.querySelector('.c-file-input__field');
            file = fileInput.files[0];
            let fileName = "";
            if (file) {
                fileName = file.name; // Obtener el nombre del archivo
                if (this.action === "EDIT") {
                  this.curriculumEdit.nombrearchivo = fileName;
                } else {
                  this.curriculum.nombreDocumento = fileName;
                }
            }

            // Preparar los datos según la acción
            const base64String = this.curriculum.documento.split(',')[1]; // Obtener solo la parte Base64
            const contentType = this.curriculum.documento.split(',')[0].split(':')[1].split(';')[0]; // Extraer el MIME type

            if (!base64String || !contentType) {
                throw new Error("El formato del documento no es válido.");
            }

            if (this.action === "EDIT") {
                fileName = this.curriculumEdit.nombrearchivo; // Nombre del archivo en modo edición
                file = base64ToFile(base64String, fileName, contentType);
                this.curriculumEdit.documentoBase64 = base64String;
                requestData = {
                    idCurriculum: this.curriculumEdit.idcurriculumvitae,
                    nombreDocumento: this.curriculumEdit.nombrearchivo,
                    fechaRegistro: new Date().toISOString(),
                    mensaje: "OK",
                    estado: "EXITO",
                    uuid: this.curriculumEdit.uuid,
                    documentoBase64: this.curriculum.documento,
                    correo: this.$root.infoColaboradorActual.correo,
                    tipoDocumento: 'CV'
                };
            } else {
                fileName = this.curriculum.nombreDocumento || 'documento.pdf'; // Nombre por defecto si no se proporciona uno
                file = base64ToFile(base64String, fileName, contentType);
                requestData = {
                    fechaRegistro: new Date().toISOString().split('T')[0],
                    nombreDocumento: this.curriculum.nombreDocumento,
                    documentoBase64: this.curriculum.documento,
                    correo: this.curriculum.correo,
                    tipoDocumento: "CV",
                    nombreUsuario: this.$root.infoColaboradorActual.nombre
                };
            }

            const Json = JSON.stringify(requestData);
            let loader = this.showLoader();
            await CargarCV(file, this.curriculum.idColaborador, Json)
                .then(response => {
                    Vue.$toast.success("El archivo fue cargado correctamente.");
                }).catch(error => {
                    Vue.$toast.error("No se pudo cargar el archivo", {});
                });
            this.hideLoader(loader);
        }
    } catch (error) {
        Vue.$toast.error("Ocurrió un error durante el proceso de carga.");
    }
      },

        }
};
</script>
