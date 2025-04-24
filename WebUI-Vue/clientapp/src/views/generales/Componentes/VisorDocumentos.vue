<template>
  <div>
    <Dialog
      :visible.sync="showModalVisorAdjuntosComputed"
      :style="{ width: '70vw' }"
      :maximizable="true"
      :modal="true"
    >
      <template #header>
        <h5>Visor de contenido</h5>
      </template>

      <div>
        <visor-adjuntos
          :archivo="this.src"
          :pdf="esPDF"
          name="visorAdjuntos"
          v-if="this.src && this.src !== ''"
        >
        </visor-adjuntos>
        <CCol class="pt-2" v-if="!this.src || this.src === ''">
          <span class="h6 text-danger">Documento no presentado.</span>
        </CCol>
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
import VisorAdjuntos from "../../generales/Componentes/VisorAdjuntos.vue";

export default {
  name: "VisorDocumentos",
  props: {
    src: {
      type: String,
      default: ""
    },
    showModalVisorAdjuntos: { type: Boolean, default: false }
  },
  components: {
    "visor-adjuntos": VisorAdjuntos
  },
  data() {
    return {};
  },
  methods: {
    CloseVisor() {
      this.$emit("showModalVisor", false);
      this.setShowModalVisor(false);
    },
    setShowModalVisor(value) {
      this.$emit("update:showModalVisorAdjuntos", value);
    }
  },
  computed: {
    esPDF() {
      if (this.src.includes("application/pdf")) {
        return true;
      }
      return false;
    },
    showModalVisorAdjuntosComputed: {
      get() {
        return this.showModalVisorAdjuntos;
      },
      set(value) {
        this.setShowModalVisor(value);
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
