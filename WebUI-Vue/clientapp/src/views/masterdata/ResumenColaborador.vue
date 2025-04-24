<template>
  <CRow class="mb-2">
  
    <CCol md="8" sm="12">
      <CRow>
        <!-- Identificación  -->
        <CCol md="5" sm="8">
          <CInput
            label="Identificación"
            :readonly="!editableIdentificacion"
            :value="colaborador.identificacion"
             name="identificacion"
            @blur="updateKeyPress"
          />
        </CCol>
        <!-- Nombre -->
        <CCol md="5">
          <CInput
            label="Nombre"
            :readonly="!editable || !puedePintar"
            :value="colaborador.nombre"
            name="nombre"
            @blur="updateKeyPress"
            autocomplete="nope"

          />
        </CCol>        

        <!-- Botón buscar  -->
        <CCol class="mt-4" md="2" sm="4">
          <CButton
            class="btn btn-info mt-1 pull-right w-100 libro-marca-celeste onHoverDark"
            @click="aceptar('Colaboradores')"
            :disabled="!searchable"
            v-show="visible"
            v-if="puedePintar && puedePintarSearch"
            ><li class="pi pi-search"></li>
          </CButton>
        </CCol>
      </CRow>
      <CRow>
        <!-- Primer apellido -->

        <CCol md="5">
          <CInput
            label="Primer apellido"
            :value="colaborador.primerapellido"
            :readonly="!editable || !puedePintar"
            name="primerapellido"
            @blur="updateKeyPress"
          />
        </CCol>

        <!-- Segundo apellido -->
        <CCol md="5">
          <CInput
            label="Segundo apellido"
            :value="colaborador.segundoapellido"
            :readonly="!editable || !puedePintar"
            name="segundoapellido"
            @blur="updateKeyPress"
          />
        </CCol>
      </CRow>
    </CCol>
      <CCol md="4" class="d-flex justify-content-center">
      <!-- <CImg
                :src="this.datosPersonales.foto"
                block
                class="mb-2 rounded-circle border border-light shadow"
                width="150"
                height="150"
              /> -->
      <div class="avatar-upload">
        <div v-if="editable && puedePintar" class="avatar-edit">
          <input
            type="file"
            id="imageUpload"
            accept=".png, .jpg, .jpeg"
            @change="actualizarVista"
            ref="archivoFoto"
            name="foto"
          />
          <label for="imageUpload"></label>
        </div>
        <div class="avatar-preview">
          <!-- <div
            id="imagePreview"
            style="background-image: url(https://st3.depositphotos.com/14846838/19463/v/1600/depositphotos_194633234-stock-illustration-man-woman-avatar-symbolising-team.jpg);"
          ></div> -->
          <CImg
            :src="colaborador.foto"
            block
            class="rounded-circle  "
            width="180"
            height="180"
            id="foto"
          />
        </div>
      </div>
    </CCol>    
  </CRow>
</template>

<script>
import Vue from "vue";
//import { Upsert } from "./request";
import { GetPhotoByIdentityCard } from "./request";

export default {
  name: "ResumenColaborador",
  props: {
    colaborador: { type: Object, required: true },
    estadoSolicitud: { type: String, default: "RE" },
    editable: { type: Boolean, default: true },
    editableIdentificacion: { type: Boolean, default: false },
    searchable: { type: Boolean, default: true },
    visible: { type: Boolean, default: true },
    habilitarControles: { type: Boolean, default: false }
  },
  watch: {
    // "colaborador.identificacion": {
    //   async handler(newVal) {
    //     if (newVal && newVal != this.identificacionColaborador) {
    //       console.log("colaborador.identificacion");
    //       // obtener la foto del colaborador
    //       await this.obtenerFoto(newVal);
    //       this.identificacionColaborador = newVal;
    //     }
    //   },
    //   immediate: true
    // }
  },
  data() {
    return {
      resultado: null,
      fotoColaborador: this.colaborador.foto,
      identificacionColaborador: this.colaborador.identificacion
      //fotoColaborador: null
    };
  },
  mounted() {
    // se cargarian los request en caso de ser necesario o en created...
  },
  methods: {
    updateKeyPress({ target: { name, value } }) {
      this.$emit("update:colaborador", {
        ...this.colaborador,
        [name]: value
      });
    },

    aceptar(type) {
      try {
        this.$emit("displayModal", type);
      } catch (error) {
        console.log(error);
      }
    },
    async obtenerFoto(numeroIdentificacion) {
      let response = null;
      if (numeroIdentificacion) {
        try {
          response = await GetPhotoByIdentityCard(numeroIdentificacion);
        } catch {
          response = null;
        }
      }
      this.fotoColaborador = response
        ? response.data.fotoColaborador.foto
        : response;
    },
    actualizarVista() {
      if (this.$refs.archivoFoto.files && this.$refs.archivoFoto.files[0]) {
        // se valida el formato
        if (
          "image/png;image/jpg;image/jpeg".includes(
            this.$refs.archivoFoto.files[0].type
          )
        ) {
          // se valida el tamaño que sea menor o igual a medio MB
          if (this.$refs.archivoFoto.files[0].size / 1024 / 1024 <= 0.5) {
            // se realiza la lectura del archivo
            var reader = new FileReader();
            reader.onload = function(e) {
              this.fotoColaborador = e.target.result;
              this.$emit("update:colaborador", {
                ...this.colaborador,
                foto: this.fotoColaborador
              });
            }.bind(this);

            reader.readAsDataURL(this.$refs.archivoFoto.files[0]);
          } else {
            Vue.$toast.warning(
              "El tamaño del archivo debe ser menor o igual a 0.5MB",
              {}
            );
          }
        } else {
          Vue.$toast.warning(
            "El formato del archivo no es válido. Solo se soportan JPG, JPEG y PNG",
            {}
          );
        }
      }
    }
  },
  computed: {
    puedePintar() {
      return this.habilitarControles;
      //    return this.estadoSolicitud === "RE" || this.estadoSolicitud === "CO";
      // let pinta = false;
      // if () {
      //   pinta = true;
      // }

      // return pinta;
    },
    puedePintarSearch() {
      return (
        this.estadoSolicitud == "RE" &&
        this.$root.hasPermission("PuedeSeleccionarColaborador")
      );
      // let pinta = false;
      // if (this.estadoSolicitud == "RE") {
      //   pinta = true;
      // }
      // return pinta;
    }
  }
};
</script>

<style>
.label-title {
  font-weight: 700;
}
.label-title {
  font-weight: 700;
}
.label-name {
  font-variant: small-caps;
  text-align: center;
}
.info {
  background-color: silver !important;
}
</style>
