<template>
  <div>
    <CRow>
      <CCol md="12" class="" v-if="puedePintar">
        <div
          :class="{ 'has-file': showRemovedBtn }"
          class="c-file-input js-file-input"
        >
          <div class="c-file-input__indicator">
            <li class="c-file-input__indicator__icon pi pi-paperclip"></li>
          </div>
          <label class="c-file-input__label js-file-input__label">
            <span style="cursor:pointer;">{{ theFileName }}</span>
            <input
              ref="myCustomInputFile"
              type="file"
              :name="context.name"
              :accept="acceptedFiles"
              @change="myUploader"
              class="c-file-input__field js-file-input__field fileupload"
            />
          </label>
          <div
            v-if="this.showRemovedBtn"
            @click="removerArchivo"
            class="block float-right"
            style="cursor:pointer;"
            title="Quitar documento"
          >
            <li
              class="pi pi-trash text-danger"
              style="margin-top: 35%; padding: 0px 5px 0 5px;"
            ></li>
          </div>
          <div
            v-if="this.showRemovedBtn && !this.isCSV"
            @click="VerAdjunto"
            class="block float-right"
            style="cursor:pointer;"
            title="Ver contenido"
          >
            <li
              class="pi pi-eye text-primary"
              style="margin-top: 35%; padding: 0px 5px 0 5px;"
            ></li>
          </div>
        </div>
      </CCol>

      <CCol
        v-if="!puedePintar && this.context.model && this.context.model !== ''"
      >
        <div
          @click="VerAdjunto"
          class="btn btn-info block float-left"
          style="cursor:pointer;"
          title="Ver contenido del documento"
        >
          Ver documento
        </div>
      </CCol>
      <CCol
        class="pt-2"
        v-if="
          !puedePintar && (!this.context.model || this.context.model === '')
        "
      >
        <span class="h6 text-danger">Documento no presentado.</span>
      </CCol>
    </CRow>

    <Dialog
      :visible.sync="showModalVisorAdjuntos"
      :style="{ width: '70vw' }"
      :maximizable="true"
      :modal="true"
    >
      <template #header>
        <h5>Visor de contenido</h5>
      </template>

      <div>
        <visor-adjuntos :archivo="src" :pdf="esPDF" name="visorAdjuntos">
        </visor-adjuntos>
      </div>
      <template #footer>
        <div>
          <CRow>
            <CCol col="12" class="pull-right">
              <CButton
                block
                color="primary"
                class="mr-1 pull-right btn btn-info libro-marca-azuloscuro"
                @click="CloseVisor"
              >
                Aceptar</CButton
              >
            </CCol>
          </CRow>
        </div>
      </template>
    </Dialog>
  </div>
</template>

<script>
import VisorAdjuntos from "../../../generales/Componentes/VisorAdjuntos";
import common from "../../../masterdata/common.js";
import Vue from "vue";

export default {
  name: "FileUploadFormulate",
  mixins: [common],
  props: {
    context: {
      type: Object,
      required: true
    }, // custom props
    tipoDocumento: {
      type: String,
      required: true
    },
    estadoSolicitud: {
      type: String,
      required: true
    },
    index: {
      type: Number,
      default: -1
    },
    eliminada: {
      type: Boolean,
      default: false
    },
    visorPadre: {
      type: Boolean,
      default: false
    },
    habilitarControles: { type: Boolean, default: false },
    isCSV: { type: Boolean, default: false }
  },
  components: {
    "visor-adjuntos": VisorAdjuntos
  },
  data() {
    return {
      chooseLabel: "Seleccione el archivo",
      nombreImg: "",
      showModalVisorAdjuntos: false,
      src: null,
      esPDF: false,
      files: [],
      value: "",
      showRemovedBtn: false,
      labelPlaceholder: "Seleccione un archivo"
    };
  },
  methods: {
    async myUploader(event) {
      var file = event.currentTarget.files[0];

      if (file.size > 1572864 && !this.isCSV) {
        Vue.$toast.error("El tamaño del archivo debe ser menor a 1.5MB.", {});
      } else {
        if (this.isCSV) {
          this.context.model = file;
        } else {
          this.esPDF = false;

          var reader = new FileReader();

          reader.onload = function(e) {
            this.src = e.target.result;
            this.context.model = e.target.result;
            this.showRemovedBtn = true;
          }.bind(this);

          this.files = event.currentTarget.files;
          reader.readAsDataURL(event.currentTarget.files[0]);

          if (event.currentTarget.files && event.currentTarget.files[0]) {
            if (event.currentTarget.files[0].type === "application/pdf") {
              this.esPDF = true;
            }
          }
          if (this.index > -1) {
            this.$emit("changeEstado", this.index);
          }
        }
      }
    },
    VerAdjunto() {
      // debe leer flag del tipo de render
      // si es padre
      if (this.visorPadre) {
        this.$emit("showModalPadre", { src: this.src, show: true });
      } else {
        //sino
        this.esPDF = this.src.includes("application/pdf");
        this.$emit("showModal", true);
        this.showModalVisorAdjuntos = true;
      }
    },
    removerArchivo() {
      this.context.model = "";
      document.getElementsByClassName("fileupload").value = "";
      this.src = "";
      this.showRemovedBtn = false;
      this.files = [];
      this.$refs.myCustomInputFile.value = "";
      this.esPDF = false;
    },
    CloseVisor() {
      this.$emit("showModal", false);
      this.showModalVisorAdjuntos = false;
    }
  },
  computed: {
    theFileName() {
      if (this.context.model && this.context.model !== "") {
        return this.tipoDocumento ? this.tipoDocumento : "Documento adjunto";
      }
      return this.labelPlaceholder;
    },
    puedeModificar() {
      return (
        (this.estadoSolicitud == common.REGISTRADA ||
          this.estadoSolicitud == common.CORREGIR) &&
        this.eliminada == false
      );
    },
    puedePintar() {
      // return (this.habilitarControles &&
      //   this.eliminada == false);
      return this.habilitarControles && this.eliminada == false;
    },
    acceptedFiles() {
      if (this.isCSV) {
        return ".csv, .png, .jpg, .jpeg, .pdf";
      } else {
        return ".png, .jpg, .jpeg, .pdf";
      }
    }
  },
  watch: {
    context() {
      if (this.context.model && this.context.model !== "") {
        this.showRemovedBtn = true;
        this.src = this.context.model;
      } else {
        this.showRemovedBtn = false;
      }
    }
  }
};
</script>

<style>
.p-dialog-header {
  background-color: #002e6e !important;
  color: #ffffff !important;
}
.p-dialog-content {
  border-color: #002e6e !important;
  /* width: 600px; */
}
.p-dialog-header-maximize {
  color: #ffffff !important;
}
.p-dialog-header-close {
  color: #ffffff !important;
}
.p-dialog-mask {
  z-index: 1049 !important;
}

.p-fileupload-buttonbar {
  width: 450px;
  height: 450px;
}
.p-fileuploadr {
  width: 450px;
  height: 450px;
}
.p-fileupload-content {
  width: 450px;
  height: 450px;
}

.hint {
  font-size: 0.605rem !important;
}

/* ***************************************************************************** */
.o-container {
  padding-top: 40px;
  padding-bottom: 40px;
}

.c-field__error {
  font-size: 12px;
  color: #d71149;
}

.c-file-input {
  position: relative;
  display: block;
  height: 36px;
  border: 1px dashed #ddd;
  background-color: #fff;
}

/* line 24, app/assets/stylesheets/mweb/6-components/_components.file-input.scss */
.c-file-input:invalid,
.c-field--error .c-file-input {
  background-color: #ffe6e9;
  border-color: #ff566a;
}

/* line 28, app/assets/stylesheets/mweb/6-components/_components.file-input.scss */
.c-file-input:invalid:focus,
.c-field--error .c-file-input:focus {
  background-color: #ffe6e9;
  border-color: #ff566a;
}

/* line 34, app/assets/stylesheets/mweb/6-components/_components.file-input.scss */
.c-file-input__label {
  position: absolute;
  top: 0;
  left: 0;
  width: 80%;
  height: 100%;
  padding-left: 36px;
  padding-right: 36px;
  line-height: 36px;
  color: #999;
  font-size: 12px;
  overflow: hidden;
  word-wrap: break-word;
  z-index: 1;
}

/* line 50, app/assets/stylesheets/mweb/6-components/_components.file-input.scss */
.c-file-input__field {
  position: absolute !important;
  height: 1px !important;
  width: 1px !important;
  padding: 0 !important;
  overflow: hidden !important;
  clip: rect(0, 0, 0, 0) !important;
  z-index: -1;
}

/* line 59, app/assets/stylesheets/mweb/6-components/_components.file-input.scss */
.c-file-input__field:focus {
  outline: none;
}

/* line 64, app/assets/stylesheets/mweb/6-components/_components.file-input.scss */
.c-file-input__indicator {
  position: absolute;
  left: 0;
  top: 0;
  height: 100%;
  width: 36px;
  z-index: 2;
}

/* line 73, app/assets/stylesheets/mweb/6-components/_components.file-input.scss */
.c-file-input__indicator__icon {
  color: #bbb;
  position: absolute;
  top: 50%;
  -webkit-transform: translate(0, -50%);
  -ms-transform: translate(0, -50%);
  -o-transform: translate(0, -50%);
  transform: translate(0, -50%);
  left: 4px;
  font-size: 20px;
}

/* line 81, app/assets/stylesheets/mweb/6-components/_components.file-input.scss */
.has-file .c-file-input__indicator__icon {
  color: #000000;
}

/* line 86, app/assets/stylesheets/mweb/6-components/_components.file-input.scss */
.c-file-input__remove {
  display: none;
  position: absolute;
  top: 0;
  right: 0;
  height: 100%;
  width: 36px;
  z-index: 2;
}

.has-file > .c-file-input__remove {
  display: block;
}

/* line 100, app/assets/stylesheets/mweb/6-components/_components.file-input.scss */
.c-file-input__remove__icon {
  position: absolute;
  top: 50%;
  -webkit-transform: translate(0, -50%);
  -ms-transform: translate(0, -50%);
  -o-transform: translate(0, -50%);
  transform: translate(0, -50%);
  left: 4px;
  font-size: 20px;
  color: #ff566a;
}
</style>
