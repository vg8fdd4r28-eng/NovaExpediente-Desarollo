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
          v-model="actividad"
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
                    label="Nombre de la actividad"
                    validation="required|max:500,length"
                    validation-name="Nombre para la actividad"
                    :validation-messages="{
                      required: 'Ingrese un nombre para la actividad.'
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
                <CCol sm="12">
                  <FormulateInput
                    type="datetime-local"
                    name="fecha"
                    label="Fecha y hora de la actividad"
                    :validation="'required|after:' + today"
                    validation-name="Fecha"
                    :validation-messages="{
                      required: 'Ingrese la fecha de la actividad .',
                      after:
                        'La fecha de expiración no puede ser menor a la actual.'
                    }"
                    :input-class="['form-control']"
                    :help-class="['hint']"
                    :disabled="isDisabled"
                  />
                </CCol>
                <CCol sm="12">
                  <FormulateInput
                    name="idIdioma"
                    :options="languagesList"
                    type="select"
                    label="Seleccione el idioma"
                    placeholder="Seleccione el idioma"
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
              <CCol sm="6">
                <CCol sm="12">
                  <FormulateInput
                    type="url"
                    name="enlace"
                    label="Enlace de la actividad"
                    validation="required"
                    validation-name="Ubicación"
                    :validation-messages="{
                      required: 'Ingrese el enlace de la actividad'
                    }"
                    :input-class="['form-control']"
                    :disabled="isDisabled"
                  />
                </CCol>

                <CCol sm="12">
                  <FormulateInput
                    type="text"
                    name="lat"
                    label="Latitud de la actividad"
                    validation="required"
                    validation-name="Latitud"
                    :validation-messages="{
                      required: 'Ingrese la latitud de la actividad'
                    }"
                    :input-class="['form-control']"
                    :disabled="true"
                  />
                </CCol>
                <CCol sm="12">
                  <FormulateInput
                    type="text"
                    name="lng"
                    label="Longitud de la actividad"
                    validation="required"
                    validation-name="Longitud"
                    :validation-messages="{
                      required: 'Ingrese la longitud de la actividad'
                    }"
                    :input-class="['form-control']"
                    :disabled="true"
                  />
                </CCol>

                <CCol sm="12" style="margin-bottom:3px; margin-top:15px">
                  <!-- <GmapMap ref="mapRef">
</GmapMap> -->
                  <!-- <l-map ref="myMap" @ready="doSomethingOnReady()"> </l-map> -->
                  <div class="info" style="height: 15%;">
                    <!-- <span>Center: {{ center }}</span> -->
                    <span
                      >Posicione en icono de novacomp en el punto de referencia
                      para seleccionar la latitud y longitud</span
                    >
                    <!-- <span>Zoom: {{ zoom }}</span>
      <span>Bounds: {{ bounds }}</span> -->
                  </div>
                  <div class="map">
                    <l-map
                      @ready="doSomethingOnReady()"
                      ref="myMap"
                      style="height: 250px; border-radius:40px"
                      :zoom="zoom"
                      :center="center"
                      @update:zoom="zoomUpdated"
                      @update:center="centerUpdated"
                      @update:bounds="boundsUpdated"
                    >
                      <l-tile-layer
                        :url="url"
                        :attribution="attribution"
                      ></l-tile-layer>
                      <!-- <l-marker :lat-lng="markerLatLng" :icon="icon"> </l-marker> -->

                      <l-marker :lat-lng="markerLatLng">
                        <l-icon
                          :icon-size="dynamicSize"
                          :icon-anchor="dynamicAnchor"
                          icon-url="https://res.cloudinary.com/novacomp/image/upload/v1672078833/NE/1617033508609-modified_gluqdb.png"
                        >
                        </l-icon>
                      </l-marker>
                    </l-map>
                  </div>
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
                  Guardar Actividad</CButton
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
                  <p>Es permitido un máximo de 3 imágenes por actividad</p>
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

import L from "leaflet";
import { LMap, LTileLayer, LMarker, LIcon } from "vue2-leaflet";

import {
  Languages,
  GetDetalleActividad,
  UploadImageActividades,
  UpsertActividades,
  DeleteImagesActividades
} from "../request";
import moment from "moment";

export default {
  name: "formActividad",
  mixins: [common],
  components: { ConfirmDialogue, LMap, LTileLayer, LMarker, LIcon },
  emits: ["view-list"],
  props: ["actividadId", "formAction"],
  data() {
    return {
      id: this.actividadId,
      action: this.formAction,
      actividad: {
        idColaborador: null,
        idActividad: null,
        idIdioma: null,
        nombre: "",
        descripcion: "",
        activo: true,
        enlace: "",
        lat: "",
        lng: "",
        fecha: null,
        imagen: null
      },
      currentActividad: [],
      loading: true,
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
      map: null,
      url: "https://tile.openstreetmap.org/{z}/{x}/{y}.png",
      attribution:
        "&copy; <a href='https://www.openstreetmap.org/copyright'>OpenStreetMap</a> contributors",
      markerLatLng: [9.93, -84.14],
      zoom: 11,
      center: [9.93, -84.14],
      bounds: null,
      icon: L.icon({
        iconUrl:
          "https://res.cloudinary.com/novacomp/image/upload/v1672078833/NE/1617033508609-modified_gluqdb.png",
        iconSize: [12, 25],
        iconAnchor: [12, 25]
      }),
      staticAnchor: [12, 25],
      customText: "Foobar",
      iconSize: 30
    };
  },
  async mounted() {
    this.getlanguages();
    this.viewManagement();
    this.loading = false;

    // this.$refs.mapRef.$mapPromise.then((map) => {
    //   map.panTo({lat: 9.93, lng: -84.14})
    // })

    this.$nextTick(() => {
      this.$refs.myMap.mapObject.ANY_LEAFLET_MAP_METHOD();
    });
  },
  methods: {
    zoomUpdated(zoom) {
      this.zoom = zoom;
    },
    centerUpdated(center) {
      this.center = center;
      this.markerLatLng = center;
      this.actividad.lat = center.lat;
      this.actividad.lng = center.lng;

    },
    boundsUpdated(bounds) {
      this.bounds = bounds;
    },
    doSomethingOnReady() {
      this.map = this.$refs.myMap.mapObject;
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
      const { data } = await GetDetalleActividad(this.id);
      const document = data;
      const imagesData = data[0].IMAGENES;
      this.currentActividad = document.map(
        ({
          IDACTIVIDAD,
          IDIDIOMA,
          NOMBRE,
          DESCRIPCION,
          ACTIVO,
          FECHA,
          LAT,
          LNG,
          ENLACE
        }) => ({
          idActividad: IDACTIVIDAD,
          idIdioma: IDIDIOMA,
          nombre: NOMBRE,
          descripcion: DESCRIPCION,
          activo: ACTIVO,
          fecha: FECHA,
          enlace: ENLACE,
          lat: LAT,
          lng: LNG
        })
      );
      this.actividad = this.currentActividad[0];

      this.images = imagesData.map(({ URLIMAGEN, IDIMAGEN }) => ({
        itemImageSrc: URLIMAGEN,
        thumbnailImageSrc: URLIMAGEN,
        alt: IDIMAGEN,
        title: "Imagen " + IDIMAGEN
      }));

      this.center = [this.actividad.lat, this.actividad.lng];
    },
    goBack() {
      this.$emit("view-list", "lista-actividades");
    },
    reset() {
      this.actividad = {
        idColaborador: null,
        idActividad: null,
        idIdioma: null,
        nombre: "",
        descripcion: "",
        activo: true,
        enlace: "",
        lat: "",
        lng: "",
        fecha: null,
        imagen: null
      };
      this.id = null;
      this.currentActividad = [];
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
      this.actividad.idColaborador = this.$root.infoColaboradorActual.id;

      this.actividad.fecha = moment(this.actividad.fecha).format(
        "YYYY/MM/DD hh:mm:ss"
      );

      this.actividad.lat = this.actividad.lat.toString();
      this.actividad.lng = this.actividad.lng.toString();
      this.actividad.descripcion=  this.actividad.descripcion.replace(/"/g, "'");

      const res = await UpsertActividades({
        data: JSON.stringify({ ...this.actividad })
      });

      if (!isNaN(res.data)) {
        //exito
        if (res.data > 0) {
          Vue.$toast.success("La actividad fue creada exitosamente.", {});
          Vue.$toast.info(
            "Debe de al menos ingresar una imagen a la actividad para que esta se vea correctamente en el app.",
            { duration: 8000, pauseOnHover: true }
          );
          this.actividad.idActividad = res.data;
        } else {
          Vue.$toast.success("La actividad fue modificada exitosamente.", {});
        }
      } else {
        //error
        Vue.$toast.error("La actividad no se pudo crear.", {});
      }
    },
    async guardarImagen() {
      if (this.actividad.imagen != null) {
        this.actividad.idColaborador = this.$root.infoColaboradorActual.id;
        const res = await UploadImageActividades(
          this.actividad.imagen,
          this.actividad.idActividad,
          this.actividad.idColaborador
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

            this.actividad.imagen = null;
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
        "Desea eliminar la imagen de la actividad, esta acción no puede deshacerse.",
        "Eliminar Imagen"
      );
      res
        ? await DeleteImagesActividades(this.actividad.idActividad, item)
            .then(response => {
              if (response.data === "OK") {
                Vue.$toast.success("La imagen se eliminó correctamente");
                const index = this.images.findIndex(i => i.alt === item);
                this.images.splice(index, 1);
              } else {
                Vue.$toast.error("La imagen no pudo eliminarse");
              }
            })
            .catch(response => {
              console.error(response.Error);
              Vue.$toast.error("La imagen no pudo eliminarse");
            })
        : null;
    },
    viewManagement() {
      switch (this.action) {
        case "CREATE":
          this.reset();
          this.header = "Crear Actividad de Aplicación Móvil";
          break;
        case "EDIT":
          this.getDetail();
          this.header = " Editar Actividad de Aplicación Móvil";
          break;
        case "VIEW":
          this.getDetail();
          this.header = "Ver Actividad de Aplicación Móvil";
          break;
        default:
          //   this.getNewDetail();
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
      if (this.actividad.idActividad != null) {
        return true;
      } else {
        return false;
      }
    },
    dynamicSize() {
      return [this.iconSize, this.iconSize * 1.15];
    },
    dynamicAnchor() {
      return [this.iconSize / 2, this.iconSize * 1.15];
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

.map {
  border: 1px solid #000000;
  -moz-border-radius: 7px;
  border-radius: 50px;
  padding: 10px;
  /* background-color: #223454; */
}

 .p-galleria .p-galleria-thumbnail-container {
     background: #f0f0f000;
}  

.p-galleria-thumbnail-item {
     background-color: lightgrey;

}

 /* .p-galleria-thumbnail-prev {
color:blue !important;

}
 .p-galleria-thumbnail-next {
  color:blue !important;
} */

</style>
