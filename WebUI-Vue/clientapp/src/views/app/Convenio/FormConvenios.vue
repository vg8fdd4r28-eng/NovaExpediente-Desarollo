<template>
  <div>
    <CRow>
      <CCol md="10"> </CCol>
      <CCol sm="12" md="2">
        <CButton
          block
          color="primary"
          @click="goBack()"
          class="
            btn
            pull-right
            btn btn-info
            libro-marca-celeste-claro
            onHoverDark
            btn-primary
          "
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
      </CCardHeader>
      <CCardBody
        style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 20px"
      >
        <FormulateForm
          name="formulario"
          v-model="convenio"
          class="mt-3"
          @submit="submit()"
          @failed-validation="validationError"
          :invalid-message="this.invalidMessage"
        >
          <div>
            <CRow>
              <CCol sm="6">
                <CCol sm="12">
                  <FormulateInput
                    type="text"
                    name="nombre"
                    label="Nombre del convenio"
                    validation="required|max:200,length"
                    validation-name="Nombre para el convenio"
                    :validation-messages="{
                      required: 'Ingrese un nombre para el convenio.'
                    }"
                    :input-class="['form-control']"
                    :disabled="isDisabled"
                  />
                </CCol>
                <CCol sm="12" class="wrapper">
                  <label>Descripción</label>
                  <FormulateInput
                    type="myRichTextBox"
                    name="descripcion"
                    :disabled="isDisabled"
                  />
                </CCol>
              </CCol>
              <CCol sm="6">
                <CCol sm="12">
                  <FormulateInput
                    type="text"
                    name="telefono"
                    label="Teléfono"
                    validation="required|max:100,length"
                    validation-name="Teléfono"
                    :validations-messages="{
                      required: 'Ingrese un teléfono para el convenio'
                    }"
                    :input-class="['form-control']"
                    :disabled="isDisabled"
                  />
                </CCol>
                <CCol sm="12">
                  <FormulateInput
                    type="text"
                    name="ubicacion"
                    label="Ubicación del convenio"
                    validation="required|max:100,length"
                    validation-name="Ubicación"
                    :validation-messages="{
                      required: 'Ingrese la ubicación del convenio'
                    }"
                    :input-class="['form-control']"
                    :disabled="isDisabled"
                  />
                </CCol>
                <CCol sm="12">
                  <FormulateInput
                    name="idConvenioCategoria"
                    :options="categoriasConvenio"
                    type="select"
                    label="Seleccione la categoría"
                    input-class="form-control"
                    validation="required|not:0"
                    valadation-name="Categoria de convenios"
                    :validation-messages="{
                      required: 'Seleccione una categoría.',
                      not: 'Debe seleccionar una opción válida.'
                    }"
                    :disabled="isDisabled"
                  />
                </CCol>
                <CCol sm="12">
                  <FormulateInput
                    name="idIdioma"
                    :options="languagesList"
                    type="select"
                    label="Seleccione el idioma"
                    input-class="form-control"
                    validation="required|not:0"
                    validation-name="idioma"
                    :validation-messages="{
                      required: 'Seleccione un idioma.',
                      not: 'Debe seleccionar una opción válida.'
                    }"
                    :disabled="isDisabled"
                  />
                </CCol>
              </CCol>
            </CRow>
            <CRow>
              <CCol sm="10" />
              <CCol sm="2" style="padding: 6px">
                <CButton
                  block
                  color="primary"
                  type="submit"
                  class="
                    btn
                    pull-right
                    btn btn-info
                    libro-marca-celeste-claro
                    onHoverDark
                    btn-primary
                  "
                  :disabled="action === 'VIEW'"
                >
                  Guardar Convenio</CButton
                >
              </CCol>
            </CRow>
            <hr />
            <div v-show="showImages">
              <CRow>
                <CCol sm="1" class="align-self-center text-center" />
                <CCol sm="9">
                  <div>
                    <p class="speech-bubble">
                      <b>Importante: </b> Se debe ingresar por lo menos una
                      imagen, máximo tres para que la información se vea
                      reflejada en la aplicación móvil.
                    </p>
                  </div>
                </CCol>
              </CRow>
              <strong>Ingrese una imagen</strong>
              <CRow>
                <!-- <CCol sm="12">
                  <p>Es permitido un máximo de 3 imágenes por convenio</p>
                </CCol> -->

                <CCol sm="9">
                  <div style="height: 100px; padding: 20px">
                    <FormulateInput
                      type="myfileupload"
                      name="imagen"
                      :help="
                        'Formatos válidos: png/jpg - Resolución: 1284x2778'
                      "
                      input-class="form-control"
                      validation-name="Imagen blob"
                      class="hint mt-1"
                      tipo-documento="Imagen blob"
                      estado-solicitud="RE"
                      :eliminada="false"
                      :visor-padre="false"
                      :habilitar-controles="true"
                      :isCSV="true"
                    />
                  </div>
                </CCol>
                <CCol sm="3">
                  <CButton
                    block
                    color="primary"
                    type="button"
                    @click="guardarImagen()"
                    class="
                        btn
                        pull-right
                        btn btn-info
                        libro-marca-celeste-claro
                        onHoverDark
                        btn-primary
                        mt-4
                      "
                    :disabled="isDisabledImages"
                  >
                    Guardar Imagen</CButton
                  >
                </CCol>
              </CRow>
              <CRow>
                <CCol sm="4" />
                <CCol sm="4">
                  <div style="height: 600px">
                    <Galleria
                      :value="images"
                      :responsiveOptions="responsiveOptions"
                      :numVisible="5"
                      containerStyle="width: 85%; height: 85%"
                    >
                      <template #item="slotProps">
                        <div
                          style="height: 85%; width: 85%; position: relative"
                        >
                          <img
                            :src="slotProps.item.itemImageSrc"
                            :alt="slotProps.item.alt"
                            style="max-width: 85%; max-height: 85%"
                          />
                        </div>
                        <div
                          v-show="action === 'EDIT'"
                          style="
                            border-radius: 12px;
                            position: absolute;
                            bottom: 10px;
                            right: 10px;
                            padding: 5px;
                            margin-left: 15px;
                          "
                          @click="deleteImage(slotProps.item.alt)"
                        >
                          <i class="pi pi-trash delete" title="Eliminar" />
                        </div>
                      </template>
                      <template #thumbnail="slotProps">
                        <div
                          style="
                            width: 50px;
                            min-height: 50px;
                            max-height: auto;
                            float: left;
                            margin: 3px;
                            padding: 3px;
                          "
                        >
                          <img
                            :src="slotProps.item.thumbnailImageSrc"
                            :alt="slotProps.item.alt"
                            style="max-width: 50%; height: auto"
                          />
                        </div>
                      </template>
                    </Galleria>
                  </div>
                </CCol>
                <CCol sm="4" />
              </CRow>
            </div>
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

import {
  GetConveniosCategorias,
  Languages,
  GetDetalleConvenio,
  UploadImageConvenio,
  UpsertConvenios,
  DeleteImagesConvenios
} from "../request";

export default {
  name: "formConvenio",
  mixins: [common],
  components: { ConfirmDialogue },
  emits: ["view-list"],
  props: ["convenioId", "formAction"],
  data() {
    return {
      id: this.convenioId,
      action: this.formAction,
      convenio: {
        idColaborador: null,
        idConvenio: null,
        idConvenioCategoria: null,
        idIdioma: null,
        nombre: "",
        descripcion: "",
        activo: true,
        telefono: "",
        ubicacion: "",
        imagen: null
      },
      currentConvenio: [],
      loading: true,
      categoriasConvenio: [],
      languagesList: [],
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
    };
  },
  async mounted() {
    this.getCategoriasConvenios();
    this.getlanguages();
    this.viewManagement();
    this.loading = false;
  },
  methods: {
    async getCategoriasConvenios() {
      const {
        data: { conveniosCategorias }
      } = await GetConveniosCategorias();
      this.categoriasConvenio = conveniosCategorias.map(
        ({ nombre, idConvenioCategoria }) => ({
          label: nombre,
          value: idConvenioCategoria
        })
      );
    },
    async getlanguages() {
      const {
        data: { idiomas }
      } = await Languages();
      this.languagesList = idiomas.map(({ idIdioma, nombre }) => ({
        label: nombre,
        value: idIdioma
      }));
    },
    async getDetail() {
      const { data } = await GetDetalleConvenio(this.id);
      const document = data;
      const imagesData = data[0].IMAGENES;
      this.currentConvenio = document.map(
        ({
          IDCONVENIO,
          IDCONVENIOCATEGORIA,
          IDIDIOMA,
          NOMBRE,
          DESCRIPCION,
          ACTIVO,
          TELEFONO,
          UBICACION
        }) => ({
          idConvenio: IDCONVENIO,
          idConvenioCategoria: IDCONVENIOCATEGORIA,
          idIdioma: IDIDIOMA,
          nombre: NOMBRE,
          descripcion: DESCRIPCION,
          activo: ACTIVO,
          telefono: TELEFONO,
          ubicacion: UBICACION
        })
      );
      this.convenio = this.currentConvenio[0];

      this.images = imagesData.map(({ URLIMAGEN, IDIMAGEN }) => ({
        itemImageSrc: URLIMAGEN,
        thumbnailImageSrc: URLIMAGEN,
        alt: IDIMAGEN,
        title: "Imagen " + IDIMAGEN
      }));
    },
    goBack() {
      this.$emit("view-list", "lista-convenios");
    },
    reset() {
      this.convenio = {
        idColaborador: null,
        idConvenio: null,
        idConvenioCategoria: null,
        idIdioma: null,
        nombre: "",
        descripcion: "",
        activo: true,
        telefono: "",
        ubicacion: "",
        imagen: null
      };
      this.id = null;
      this.currentConvenio = [];
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
        // optional options Object
      });
    },
    async submit() {
      this.convenio.idColaborador = this.$root.infoColaboradorActual.id;
      this.convenio.descripcion=  this.convenio.descripcion.replace(/"/g, "'");

      const res = await UpsertConvenios({
        data: JSON.stringify({ ...this.convenio })
      });

      if (!isNaN(res.data)) {
        //exito
        if (res.data > 0) {
          Vue.$toast.success("El convenio fue creado exitosamente.", {});
          Vue.$toast.info(
            "Debe de al menos ingresar una imagen al convenio para que esta se vea correctamente en el app.",
            { duration: 8000, pauseOnHover: true }
          );
          this.convenio.idConvenio = res.data;
        } else {
          Vue.$toast.success("El convenio fue modificado exitosamente.", {});
        }
      } else {
        //error
        Vue.$toast.error("El convenio no se pudo crear.", {});
      }
    },
    async guardarImagen() {
      if (this.convenio.imagen != null) {
        this.convenio.idColaborador = this.$root.infoColaboradorActual.id;
        const res = await UploadImageConvenio(
          this.convenio.imagen,
          this.convenio.idConvenio,
          this.convenio.idColaborador
        );

        setTimeout(() => {
          if (res.data.indexOf("http") > -1) {
            //exito
            Vue.$toast.success("La imagen fue agregada exitosamente.", {});

            var obj = {
              itemImageSrc: res.data,
              thumbnailImageSrc: res.data
            };

            this.images.push(obj);

            this.convenio.imagen = null;
          } else {
            //error
            Vue.$toast.error("La imagen no se pudo agregar.", {});
          }
        }, 500);
      } else {
        Vue.$toast.error("No se ha seleccionado una imagen.", {});
      }
    },
     /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    async deleteImage(item, _x, _event) {
      const res = await this.Confirmacion_Generico(
        "Desea eliminar la imagen del convenio, esta acción no puede deshacerse.",
        "Eliminar Imagen"
      );
      res
        ? await DeleteImagesConvenios(this.convenio.idConvenio, item)
            .then((response) => {
              if (response.data === "OK") {
                Vue.$toast.success("La imagen se eliminó correctamente");
                const index = this.images.findIndex((i) => i.alt === item);
                this.images.splice(index, 1);
              } else {
                Vue.$toast.error("La imagen no pudo eliminarse");
              }
            })
            .catch((response) => {
              console.error(response.Error);
              Vue.$toast.error("La imagen no pudo eliminarse");
            })
        : null;
    },
    viewManagement() {
      switch (this.action) {
        case "CREATE":
          this.reset();
          this.header = "Crear Convenio de Aplicación Móvil";
          break;
        case "EDIT":
          this.getDetail();
          this.header = " Editar Convenio de Aplicación Móvil";
          break;
        case "VIEW":
          this.getDetail();
          this.header = "Ver Convenio de Aplicación Móvil";
          break;
        default:
          this.getNewDetail();
          break;
      }
    }
  },
  computed: {
    isDisabled() {
      if (this.action === "VIEW") {
        return true;
      }
      return false;
    },
    isDisabledImages() {
      if (this.action === "VIEW" || this.images.length >= 3) {
        return true;
      }
      return false;
    },
    today() {
      return new Date();
    },
    showImages() {
      if (this.convenio.idConvenio != null) {
        return true;
      } else {
        return false;
      }
    }
  }
};
</script>
<style>
.speech-bubble {
  position: relative;
  background: #f9b115;
  border-radius: 0.4em;
}

.speech-bubble:after {
  content: "";
  position: absolute;
  left: 0;
  top: 50%;
  width: 0;
  height: 0;
  border: 0.063em solid transparent;
  border-right-color: #bd9d00;
  border-left: 0;
  border-top: 0;
  margin-top: -0.031em;
  margin-left: -0.062em;
}

.delete {
  color: red;
  cursor: pointer;
}
 .p-galleria .p-galleria-thumbnail-container {
     background: #f0f0f000;
}  

.p-galleria-thumbnail-item {
     background-color: lightgrey;

}
</style>
