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
            btn btn-info btn-primary
            pull-right
            onHoverDark
            libro-marca-celeste-claro
          "
          >Volver</CButton
        >
      </CCol>
    </CRow>
    <CCard class="main--card">
      <CCardHeader class="main-card-header">
        <CCol sm="12" md="12">
          <strong>{{ header }}</strong>
        </CCol>
      </CCardHeader>
      <CCardBody class="main-card-body">
        <formulate-form
          name="formulario"
          class="mt-3"
          v-model="selectedSorteo"
          @submit="submit()"
          @failed-validation="validationError"
          :invalid-message="this.invalidMessage"
        >
          <div>
            <formulate-errors />
            <CRow>
              <CCol sm="7">
                <formulate-input
                  type="text"
                  name="nombre"
                  label="Nombre del Sorteo"
                  placeholder="Nombre del Sorteo"
                  validation="required|max:100,length"
                  :validation-messages="{
                    required: 'Ingrese un nombre para el sorteo',
                  }"
                  :input-class="['form-control']"
                  :disabled="isDisabled"
                />
              </CCol>
              <CCol sm="5" />
              <CCol sm="7">
                <formulate-input
                  name="descripcion"
                  type="textarea"
                  label="Descripción del Sorteo"
                  placeholder="Descripción del Sorteo"
                  rows="3"
                  validation="required|max:100,length"
                  :validation-messages="{
                    required: 'Ingrese una descripción para el sorteo',
                  }"
                  :input-class="['form-control']"
                  :disabled="this.formAction === 'VIEW'"
                />
                <CCol sm="5" />
              </CCol>
              <CCol sm="7">
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
                <CCol sm="5" />
              </CCol>
            </CRow>
            <CRow>
              <CCol sm="10" />
              <CCol sm="2">
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
                  :disabled="formAction === 'VIEW'"
                >
                  Guardar Sorteo</CButton
                >
              </CCol>
            </CRow>
            <CRow>
              <CCol sm="12">
                <label>Seleccione el colaborador ganador</label>
                <resumen-colaborador
                  :colaborador.sync="colaborador"
                  estado-solicitud="RE"
                  :searchable="formAction !== 'VIEW'"
                  :editable="false"
                  :habilitar-controles="true"
                  @displayModal="displayModal"
                >
                </resumen-colaborador>
              </CCol>
            </CRow>
            <hr />
            <div v-show="showImages">
              <CRow>
                <CCol sm="1" class="align-self-center text-center" />
                <CCol sm="9">
                  <div>
                    <p class="speech-bubble">
                      <b>Importante: </b> Se debe ingresar por lo menos dos
                      imágenes una para ganaste con el premio y otra para
                      perdiste; esto para que la información se vea reflejada en
                      la aplicación móvil, de caso contrario no se mostrará.
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
                          style="height: 85%; width: 85%; position: relative"
                        >
                          <img
                            :src="slotProps.item.itemImageSrc"
                            :alt="slotProps.item.alt"
                            style="max-width: 85%; max-height: 85%"
                          />
                        </div>
                        <div
                          v-show="formAction === 'EDIT'"
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
        </formulate-form>
      </CCardBody>
    </CCard>
    <Dialog
      :visible.sync="showModal"
      :style="{ width: '70vw' }"
      :maximizable="true"
      :modal="true"
    >
      <template #header>
        <h5>Selecciona un colaborador</h5>
      </template>
      <pick-list
        @selected="selected"
        :nombreTipoLista="nombreTipoLista"
        :columnas="columnasPickList"
        :dataContenido="dataPickList"
      ></pick-list>
      <template #footer></template>
    </Dialog>
    <confirm-dialogue ref="confirmDialogue" />
  </div>
</template>

<script>
import Vue from "vue";
import common from "../../masterdata/common.js";
import ConfirmDialogue from "../../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import PickList from "../../generales/Componentes/PickList.vue";
import ResumenColaborador from "../../masterdata/ResumenColaborador.vue";
import {
  GetDetalleSorteo,
  UpsertSorteo,
  GetColaboradorDetailResumen,
  ColaboradorGetAll,
  DeleteImagenSorteo,
  UploadImageSorteo,
} from "../request.js";

export default {
  components: { ConfirmDialogue, PickList, ResumenColaborador },
  props: ["selectedSorteoId", "formAction"],
  mixins: [common],
  data() {
    return {
      loading: true,
      header: "Header",
      selectedSorteo: {
        idSorteo: 0,
        nombre: "",
        descripcion: "",
        activo: false,
        idColaborador: 0,
        idGanador: null,
        esEdicion: false,
        fechaVencimiento: null,
        imagen: null,
      },
      currentSorteos: [],
      isEdit: false,
      showModal: false,
      pickListType: "Colaboradores",
      nombreTipoLista: "",
      dataPickList: [],
      columnasPickList: [],
      colaborador: {
        idcolaborador: 0,
        identificacion: null,
        nombre: null,
        primerapellido: null,
        segundoapellido: null,
        foto: null,
      },
      images: [],
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
    };
  },
  mounted() {
    this.viewManagement();
    this.loading = false;
  },
  methods: {
    async displayModal(type) {
      this.pickListType = type;
      this.clear();
      const {
        data: { colaboradores },
      } = await ColaboradorGetAll();
      switch (type) {
        case "Colaboradores":
          this.dataPickList = colaboradores.map(
            ({
              idcolaborador,
              nombreCompleto,
              nombre,
              identificacion,
              primerapellido,
              segundoapellido,
              foto,
              iddeliverymanager,
              empresaCorporacion,
              idempresacorporacion,
            }) => ({
              id: idcolaborador,
              nombre: nombre,
              nombreCompleto: nombreCompleto,
              identificacion: identificacion,
              primerapellido: primerapellido,
              segundoapellido: segundoapellido,
              foto: foto,
              iddeliverymanager: iddeliverymanager,
              idempresacorporacion: idempresacorporacion,
              empresaCorporacion: empresaCorporacion,
            })
          );
          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombreCompleto", header: "Nombre" },
          ];
          this.showModal = true;
          this.nombreTipoLista = "Colaboradores";
          break;
        default:
          break;
      }
    },
    async selected(item) {
      switch (this.pickListType) {
        case "Colaboradores":
          await this.cargarDetalleColaborador(item.id);
          this.selected(item.id);
          this.selectedSorteo.idGanador = item.id;
          this.showModal = false;
          break;
        default:
          break;
      }
    },
    async cargarDetalleColaborador(id) {
      const {
        data: { colaborador },
      } = await GetColaboradorDetailResumen(id);
      this.colaborador = {
        idcolaborador: colaborador.idcolaborador,
        identificacion: colaborador.identificacion,
        nombre: colaborador.nombre,
        primerapellido: colaborador.primerapellido,
        segundoapellido: colaborador.segundoapellido,
        foto: colaborador.foto,
        idempresacorporacion: colaborador.idempresacorporacion,
        codpaisresidencia: colaborador.codpaisresidencia,
        idnomina: colaborador.idnomina,
        solicitavacaciones: colaborador.solicitavacaciones,
        vacacionesdiasanuales: colaborador.vacacionesdiasanuales,
        saldovacaciones: colaborador.saldovacaciones,
        esservicioprofesional: colaborador.esservicioprofesional,
      };
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    async deleteImage(item, _x, _event) {
      const res = await this.Confirmacion_Generico(
        "Desea eliminar la imagen del sorteo, esta acción no puede deshacerse.",
        "Eliminar Imagen"
      );
      res
        ? await DeleteImagenSorteo(this.selectedSorteo.idSorteo, item)
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
    async guardarImagen() {
      this.selectedSorteo.idColaborador = this.$root.infoColaboradorActual.id;
      const res = await UploadImageSorteo(
        this.selectedSorteo.imagen,
        this.selectedSorteo.idSorteo,
        this.selectedSorteo.idColaborador
      );
      setTimeout(() => {
        if (res.data.indexOf("http") > -1) {
          Vue.$toast.success("La imagen fue agregada exitosamente.", {});
          var obj = {
            itemImageSrc: res.data,
            thumbnailImageSrc: res.data,
          };
          this.images.push(obj);
          this.selectedSorteo.imagen = null;
        } else {
          Vue.$toast.error("La imagen no se pudo agregar.", {});
        }
      }, 500);
    },
    async submit() {
      this.selectedSorteo.idColaborador = this.$root.infoColaboradorActual.id;
      this.selectedSorteo.idGanador = this.colaborador.idcolaborador;
      this.selectedSorteo.descripcion=  this.selectedSorteo.descripcion.replace(/"/g, "'");

      if (this.selectedSorteo.idColaborador !== null) {
        const res = await UpsertSorteo({
          data: JSON.stringify({ ...this.selectedSorteo }),
        });
        if (!isNaN(res.data)) {
          if (res.data > 0) {
            Vue.$toast.success("¡El sorteo fue creado exitosamente!");
            Vue.$toast.info(
              "¡Debe de al menos ingresar una imagen a la noticia para que esta se vea correctamente en el app!",
              {
                type: "info",
                duration: 8000,
                pauseOnHover: true,
              }
            );
            this.selectedSorteo.idSorteo = res.data;
          } else {
            Vue.$toast.success("¡El sorteo fue modificado exitosamente!");
          }
        } else {
          Vue.$toast.error("El sorteo no pudo crearse correctamente");
        }
      }
    },
    async getSorteoDetail() {
      const { data } = await GetDetalleSorteo(this.selectedSorteoId);
      const document = data;
      const imagesData = data[0].DATOS[0].IMAGENES;
      const idGanador = data[0].DATOS[0].IDGANADOR;
      const item = { id: idGanador };
      this.selected(item);
      this.currentSorteos = document.map(
        ({ IDSORTEO, NOMBRE, DESCRIPCION, ACTIVO, FECHAVENCIMIENTO }) => ({
          idSorteo: IDSORTEO,
          nombre: NOMBRE,
          descripcion: DESCRIPCION,
          activo: ACTIVO,
          esEdicion: this.isEdit ? true : false,
          idColaborador: this.$root.infoColaboradorActual.id,
          fechaVencimiento: FECHAVENCIMIENTO,
        })
      );
      const newDate = new Date(this.currentSorteos[0].fechaVencimiento)
        .toISOString()
        .split("T");
      this.currentSorteos[0].fechaVencimiento = newDate[0];
      this.currentSorteos[0].idGanador = idGanador;
      this.selectedSorteo = this.currentSorteos[0];
      this.images = imagesData.map(({ URLIMAGEN, IDIMAGEN }) => ({
        itemImageSrc: URLIMAGEN,
        thumbnailImageSrc: URLIMAGEN,
        alt: IDIMAGEN,
        title: "Imagen " + IDIMAGEN,
      }));
    },
    viewManagement() {
      switch (this.formAction) {
        case "CREATE":
          this.resetForm();
          this.selectedSorteo.activo = true;
          this.isEdit = false;
          this.header = "Crear Nuevo Sorteo de Aplicación Móvil";
          break;
        case "EDIT":
          this.getSorteoDetail();
          this.isEdit = true;
          this.header = "Editar Sorteo de Aplicación Móvil";
          break;
        case "VIEW":
          this.getSorteoDetail();
          this.header = "Ver Sorteo de Aplicación Móvil";
          break;
        default:
          this.getSorteoDetail();
          break;
      }
    },
    goBack() {
      this.$emit("view-list", "lista-sorteos");
    },
    resetForm() {
      this.selectedSorteo = {
        idSorteo: null,
        nombre: "",
        descripcion: "",
        activo: false,
        idColaborador: null,
        esEdicion: false,
        fechaVencimiento: null,
      };
    },
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        fieldNames += `-  ${fields[key].label}\r\n`;
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    validationError() {
      Vue.$toast.warning(common.MENSAJE_ERROR);
    },
    clear() {
      this.showModal = false;
      this.nombreTipoLista = "";
      this.dataPickList = null;
    },
  },
  computed: {
    isDisabled() {
      if (this.formAction === "VIEW") {
        return true;
      }
      return false;
    },
    today() {
      return new Date();
    },
    showImages() {
      if (this.selectedSorteo.idSorteo !== null) {
        return true;
      } else {
        return false;
      }
    },
    imagesDisabled() {
      if (this.formAction === "VIEW" || this.images.length >= 2) {
        return true;
      }
      return false;
    },
  },
};
</script>

<style scoped>
.main--card {
  border-radius: 16px !important;
  margin: 20px 0;
}
.main-card-header {
  border-radius: 16px !important;
  border-bottom: transparent !important;
}
.main-card-body {
  padding: 0 auto;
  margin-bottom: 20px;
}
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
</style>

<style>
.p-galleria .p-galleria-thumbnail-container {
  background: #f0f0f000;
}
.p-galleria-thumbnail-item {
  background-color: lightgrey;
}
</style>