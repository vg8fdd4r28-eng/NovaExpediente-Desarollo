<template>
  <div>
    <CCard
      style="border-radius: 16px !important; margin-top: 20px;margin-bottom: 20px;"
    >
      <CCardHeader
        style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
      >
        <strong>Carga Noticias APP</strong>
      </CCardHeader>
      <CCardBody
        style="padding-top: 0px; padding-bottom: 0px;margin-bottom: 20px;"
      >
        <FormulateForm name="formulario" v-model="data" class="mt-3">
          <div>
            <CRow>
              <CCol sm="6" class="" >
                <CCol sm="12">
                  <FormulateInput
                    type="text"
                    name="titulo"
                    label="Titulo de la noticia"
                    validation="required|max:100,length"
                    validation-name="Titulo para notificación Push"
                    :validation-messages="{
                      required: 'Ingrese un titulo para notificación Push.',
                    }"
                    :input-class="['form-control']"
                  />
                </CCol>

                <CCol sm="12" class="wrapper">
                  

                  <vue-editor v-model="msj"></vue-editor>

                  
                </CCol>
              </CCol>

              <CCol sm="6">
                <CCol sm="12">
                  <FormulateInput
                    type="myInputMultiSelect"
                    name="segmentos"
                    label="Seleccione la categoria"
                    input-class="form-control"
                    validation-name="Segmentos"
                    :options="options"
                    v-model="value"
                  />
                </CCol>
                <CCol sm="12">
                  <FormulateInput
                    type="myInputMultiSelect"
                    name="segmentos"
                    label="Seleccione el idioma"
                    input-class="form-control"
                    validation-name="Segmentos"
                    :options="options"
                    v-model="value"
                  />
                </CCol>

                <CCol sm="12">
                  <FormulateInput
                    type="text"
                    name="titulo"
                    label="Fecha de expiracion"
                    validation="required|max:100,length"
                    validation-name="Titulo para notificación Push"
                    :validation-messages="{
                      required: 'Ingrese un titulo para notificación Push.',
                    }"
                    :input-class="['form-control']"
                  />
                </CCol>
                
                <CCol sm="6" class="">
            <label>Escoja las imagenes</label>

            <FormulateInput
              type="myfileupload"
              name="imagen"
              :help="
                puedePintar === true ? 'Formatos válidos: png/jpg/gif/pdf' : ''
              "
              input-class="form-control"
              validation-name="Imagen blob"
              class="hint"
              tipo-documento="Imagen blob"
              :estado-solicitud="RE"
              :eliminada="eliminada"
              :visor-padre="true"
              :habilitar-controles="true"
              @showModal="showModalVisor"
              @showModalPadre="showModalVisorPreview"
              :isCSV="true"
            />
          </CCol>
              </CCol>

              <!-- <emojipicker @emoji="insert" :search="search">
  <div slot="emoji-invoker" slot-scope="{ events: { click: clickEvent } }" @click.stop="clickEvent">
    <button type="button">open</button>
  </div>
  <div slot="emoji-picker" slot-scope="{ emojis, insert }">
    <div>
      <div>
        <input type="text" v-model="search">
      </div>
      <div>
        <div v-for="(emojiGroup, category) in emojis" :key="category">
          <h5>{{ category }}</h5>
          <div>
            <span
              v-for="(emoji, emojiName) in emojiGroup"
              :key="emojiName"
              @click="insert(emoji)"
              :title="emojiName"
            >{{ emoji }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</emojipicker> -->
            </CRow>

            <CRow>
              <CCol sm="10"></CCol>
              <CCol sm="2">
                <!-- <FormulateInput
                  onclick="submit"
                  label="Enviar"
                  style="display:inline;margin-bottom: 20px;"
                  :input-class="[
                    'btn',
                    'btn-info',
                    'pull-right',
                    'libro-marca-azuloscuro'
                  ]"
                /> -->

                <CButton
                  block
                  color="primary"
                  @click="submit()"
                  class="btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark btn-primary"
                >
                  Enviar</CButton
                >
              </CCol>
            </CRow>
          </div>
        </FormulateForm>
      </CCardBody>
    </CCard>
  </div>
</template>

<script>
import { SendNotificationPush } from './request';
import { VueEditor } from "vue2-editor";
import Vue from 'vue';
import _ from 'lodash';
// métodos comunes
import common from '../masterdata/common.js';
import { GetClientes } from './request';

export default {
  name: 'EnvioMsgPush',
  mixins: [common],
  props: {
    origen: { type: String, default: '' }, // origen desde donde se accedio la vista
    idTipoSolicitud: { type: Number, default: 0 }, // id del tipo de la solicitud generada
    tipoSolicitud: { type: String, default: '' }, // nombre del tipo de la solicitud generada
    idSolicitud: { type: Number, default: null }, // id de la solicitud, para obtener el obj Json con la informacion a cargar
  },
  components: {
    VueEditor,
  },
  data() {
    return {
      data: {
        msj: '',
        titulo: '',
        todos: 'No',
        segmentos: [],
        segmentosStr: [],
      },
      showModal: false,
      showModalAgregar: false,
      showModalConfirmacion: false,
      showModalVisorAdjuntos: false,
      esEdicion: false,
      visorEnable: false,
      tituloModal: '',
      srcDocumento: '',
      value: null,
      options: [],
      search: '',
    };
  },
  async mounted() {
    await this.cargarClientes();
  },
  methods: {
    async cargarClientes() {
      const {
        data: { clientes },
      } = await GetClientes();
      this.options = clientes.map(({ idcliente, nombre }) => ({
        name: nombre,
        value: idcliente,
      }));
    },
    async submit(formValues) {
      this.data.segmentosStr = this.data.segmentos.map((x) => x.name);

      console.log(this.data);

      var res = await SendNotificationPush(this.data);
      console.log(res);

      this.data.msj = '';
      this.data.titulo = '';
      this.data.todos = 'No';
      this.data.segmentos = [];

      Vue.$toast.success('Se ha enviado el mensaje', {});
    },
    insert(emoji) {
      this.data.msj += emoji;
    },
  },
  computed: {
    helpTextArea() {
      if (this.data.msj) {
        return `Tiene disponible un máximo de ${8000 -
          this.data.msj.length} carácteres.`;
      }
      return null;
    },
  },
  watch: {},
};
</script>

<style>
.wrapper {
  position: relative;
  display: inline-block;
}

.regular-input {
  padding: 0.5rem 1rem;
  border-radius: 3px;
  border: 1px solid #ccc;
  width: 34rem;
  height: 8rem;
  outline: none;
}

.regular-input:focus {
  box-shadow: 0 0 0 3px rgba(66, 153, 225, 0.5);
}

.emoji-invoker {
  position: absolute;
  top: 2.5rem;
  right: 1.5rem;
  width: 2rem;
  height: 1.5rem;
  border-radius: 50%;
  cursor: pointer;
  transition: all 0.2s;
  padding: 0;
  background: transparent;
  border: 0;
}
.emoji-invoker:hover {
  transform: scale(1.1);
}
.emoji-invoker > svg {
  fill: #b1c6d0;
}

.emoji-picker {
  position: fixed;
  z-index: 1;
  font-family: Montserrat;
  border: 1px solid #ccc;
  width: 15rem;
  height: 20rem;
  overflow: scroll;
  padding: 1rem;
  box-sizing: border-box;
  border-radius: 0.5rem;
  background: #fff;
  box-shadow: 1px 1px 8px #c7dbe6;
}
.emoji-picker__search {
  display: flex;
}
.emoji-picker__search > input {
  flex: 1;
  border-radius: 10rem;
  border: 1px solid #ccc;
  padding: 0.5rem 1rem;
  outline: none;
}
.emoji-picker h5 {
  margin-bottom: 0;
  color: #b1b1b1;
  text-transform: uppercase;
  font-size: 0.8rem;
  cursor: default;
}
.emoji-picker .emojis {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}
.emoji-picker .emojis:after {
  content: '';
  flex: auto;
}
.emoji-picker .emojis span {
  padding: 0.2rem;
  cursor: pointer;
  border-radius: 5px;
}
.emoji-picker .emojis span:hover {
  background: #ececec;
  cursor: pointer;
}

/* FIN */
</style>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
