<template>
  <div>
    <CRow>
      <CCol md="10" />
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
          v-model="selectedNew"
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
                    name="titulo"
                    label="Título de la noticia"
                    validation="required|max:100,length"
                    validation-name="Titulo para la noticia"
                    :validation-messages="{
                      required: 'Ingrese un título para la noticia.',
                    }"
                    :input-class="['form-control']"
                    :disabled="isDisabled"
                  />
                </CCol>
                <CCol sm="12" class="wrapper">
                  <label>Contenido de la noticia</label>
                  <FormulateInput
                    type="myRichTextBox"
                    name="contenido"
                    :disabled="isDisabled"
                  />
                </CCol>
              </CCol>
              <CCol sm="6">
                <CCol sm="12">
                  <FormulateInput
                    type="email"
                    name="correoAMostrar"
                    label="Correo a mostrar en la noticia"
                    validation="required|email"
                    validation-name="Correo"
                    :validations-messages="{
                      required: 'Ingrese un correo para mostrar en la noticia',
                    }"
                    :input-class="['form-control']"
                    :disabled="isDisabled"
                  />
                </CCol>
                <CCol sm="12">
                  <FormulateInput
                    type="text"
                    name="nombreAMostrar"
                    label="Nombre a mostrar en la noticia"
                    validation="required|max:60,length"
                    validation-name="Nombre"
                    :validation-messages="{
                      required: 'Ingrese un nombre para mostrar en la noticia',
                    }"
                    :input-class="['form-control']"
                    :disabled="isDisabled"
                  />
                </CCol>
                <CCol sm="12">
                  <FormulateInput
                    name="idNoticiaCategoria"
                    :options="newsCategories"
                    type="select"
                    label="Seleccione la categoría"
                    input-class="form-control"
                    validation="required|not:0"
                    valadation-name="categoria"
                    :validation-messages="{
                      required: 'Seleccione una categoría.',
                      not: 'Debe seleccionar una opción válida.',
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
                      not: 'Debe seleccionar una opción válida.',
                    }"
                    :disabled="isDisabled"
                  />
                </CCol>
                <CCol sm="12">
                  <FormulateInput
                    type="date"
                    name="fechaVencimiento"
                    label="Fecha de expiración"
                    :validation="'required|after:' + today"
                    validation-name="expiracion"
                    :validation-messages="{
                      required: 'Ingrese la fecha de expiración.',
                      after:
                        'La fecha de expiración no puede ser menor a la actual.',
                    }"
                    :input-class="['form-control']"
                    :help-class="['hint']"
                    :disabled="isDisabled"
                  />
                </CCol>
                <CCol sm="12">
                  <label>¿Es pública?</label>
                  <FormulateInput
                    type="myInputSwitch"
                    name="esPublica"
                    :imput-class="['form-control']"
                    validation-name="Publica"
                    class="hint"
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
                  Guardar Noticia</CButton
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
                      imagen para que la información se vea reflejada en la
                      aplicación móvil, de caso contrario no se mostrará.
                    </p>
                  </div>
                </CCol>
              </CRow>
              <strong>Ingrese una imagen</strong>
              <CRow>
                <CCol sm="9">
                  <div style="height: 100px; padding: 20px">
                    <FormulateInput
                      type="myfileupload"
                      name="imagen"
                      :help="'Formatos válidos: png/jpg | Proporciones recomendadas: 1284x2778'"
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
                    :disabled="imagesDisabled"
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
                      :showIndicators="true"
                    >
                      <template #item="slotProps">
                        <div
                          style="height: 85%; width:85%; position: relative"
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
                            min-height: 60px;
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
    <confirm-dialogue ref="confirmDialogue" />
  </div>
</template>

<script>
import Vue from "vue";
import common from "../../masterdata/common.js";
import ConfirmDialogue from "../../generales/Componentes/PopUpModal/ConfirmDialogue.vue";

import {
  Categories,
  Languages,
  NewDetail,
  UploadImageNoticia,
  UpsertNoticias,
  DeleteImagenNoticia,
} from "../request";

export default {
  name: "formNoticia",
  mixins: [common],
  components: { ConfirmDialogue },
  emits: ["view-list"],
  props: ["selectedNewId", "formAction"],
  data() {
    return {
      id: this.selectedNewId,
      action: this.formAction,
      selectedNew: {
        idColaborador: null,
        idNoticia: null,
        idNoticiaCategoria: null,
        idIdioma: null,
        fechaVencimiento: null,
        titulo: "",
        contenido: "",
        activo: true,
        correoAMostrar: "",
        nombreAMostrar: "",
        esPublica: true,
        imagen: null,
      },
      currentNews: [],
      loading: true,
      newsCategories: [],
      languagesList: [],
      responsiveOptions: [
        {
          breakpoint: "1024px",
          numVisible: 5,
        },
        {
          breakpoint: "768px",
          numVisible: 3,
        },
        {
          breakpoint: "560px",
          numVisible: 1,
        },
      ],
      images: [],
      esEdit: false,
      header: "",
    };
  },
  async mounted() {
    this.getNewsCategories();
    this.getlanguages();
    this.viewManagement();
    this.loading = false;
  },
  methods: {
    async getNewsCategories() {
      const {
        data: { noticiasCategorias },
      } = await Categories();
      this.newsCategories = noticiasCategorias.map(
        ({ nombre, idNoticiaCategoria }) => ({
          label: nombre,
          value: idNoticiaCategoria,
        })
      );
    },
    async getlanguages() {
      const {
        data: { idiomas },
      } = await Languages();
      this.languagesList = idiomas.map(({ idIdioma, nombre }) => ({
        label: nombre,
        value: idIdioma,
      }));
    },
    async getNewDetail() {
      const { data } = await NewDetail(this.id);
      const document = data;
      const imagesData = data[0].IMAGENES;
      this.currentNews = document.map(
        ({
          IDNOTICIA,
          IDNOTICIACATEGORIA,
          IDIDIOMA,
          FECHAVENCIMIENTO,
          TITULO,
          CONTENIDO,
          ACTIVO,
          CORREOAMOSTRAR,
          NOMBREAMOSTRAR,
          ESPUBLICA,
        }) => ({
          idNoticia: IDNOTICIA,
          idNoticiaCategoria: IDNOTICIACATEGORIA,
          idIdioma: IDIDIOMA,
          fechaVencimiento: FECHAVENCIMIENTO,
          titulo: TITULO,
          contenido: CONTENIDO,
          activo: ACTIVO,
          correoAMostrar: CORREOAMOSTRAR,
          nombreAMostrar: NOMBREAMOSTRAR,
          esPublica: ESPUBLICA ? "Si" : "No",
        })
      );
      this.selectedNew = this.currentNews[0];

      this.images = imagesData.map(({ URLIMAGEN, IDIMAGEN }) => ({
        itemImageSrc: URLIMAGEN,
        thumbnailImageSrc: URLIMAGEN,
        alt: IDIMAGEN,
        title: "Imagen " + IDIMAGEN,
      }));
    },
    goBack() {
      this.$emit("view-list", "lista-noticias");
    },
    reset() {
      this.selectedNew = {
        idNoticia: null,
        idColaborador: null,
        idNoticiaCategoria: null,
        idIdioma: null,
        fechaVencimiento: null,
        titulo: "",
        contenido: "",
        activo: true,
        correoAMostrar: "",
        nombreAMostrar: "",
        esPublica: true,
      };
      this.id = null;
      this.currentNews = [];
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
      this.selectedNew.idColaborador = this.$root.infoColaboradorActual.id;
      this.selectedNew.contenido=  this.selectedNew.contenido.replace(/"/g, "'");
      const res = await UpsertNoticias({
        data: JSON.stringify({ ...this.selectedNew }),
      });
      // Validar si es edit o no desde el padre.
      if (!isNaN(res.data)) {
        if (res.data > 0) {
          Vue.$toast.success("La noticia fue creada exitosamente.", {});
          Vue.$toast.info(
            "¡Debe de al menos ingresar una imagen a la noticia para que esta se vea correctamente en el app!",
            {
              type: "info",
              duration: 8000,
              pauseOnHover: true,
            }
          );
          this.selectedNew.idNoticia = res.data;
        } else {
          Vue.$toast.success("La noticia fue modificada exitosamente.", {});
        }
      } else {
        Vue.$toast.error("La noticia no se pudo crear.", {});
      }
    },
    async guardarImagen() {
      this.selectedNew.idColaborador = this.$root.infoColaboradorActual.id;
      const res = await UploadImageNoticia(
        this.selectedNew.imagen,
        this.selectedNew.idNoticia,
        this.selectedNew.idColaborador
      );
      setTimeout(() => {
        if (res.data.indexOf("http") > -1) {
          Vue.$toast.success("La imagen fue agregada exitosamente.", {});
          var obj = {
            itemImageSrc: res.data,
            thumbnailImageSrc: res.data,
          };
          this.images.push(obj);
          this.selectedNew.imagen = null;
        } else {
          Vue.$toast.error("La imagen no se pudo agregar.", {});
        }
      }, 500);
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    async deleteImage(item, _x, _event) {
      const res = await this.Confirmacion_Generico(
        "Desea eliminar la imagen de la noticia, esta acción no puede deshacerse.",
        "Eliminar Imagen"
      );
      res
        ? await DeleteImagenNoticia(this.selectedNew.idNoticia, item)
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
          this.header = "Crear Noticia de Aplicación Móvil";
          break;
        case "EDIT":
          this.getNewDetail();
          this.header = " Editar Noticias de Aplicación Móvil";
          break;
        case "VIEW":
          this.getNewDetail();
          this.header = "Ver Noticia de Aplicación Móvil";
          break;
        default:
          this.getNewDetail();
          break;
      }
    },
  },
  computed: {
    isDisabled() {
      if (this.action === "VIEW") {
        return true;
      }
      return false;
    },
    today() {
      return new Date();
    },
    showImages() {
      if (this.selectedNew.idNoticia !== null) {
        return true;
      } else {
        return false;
      }
    },
    imagesDisabled() {
      if (this.action === "VIEW" || this.images.length >= 3) {
        return true;
      }
      return false;
    },
  },
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