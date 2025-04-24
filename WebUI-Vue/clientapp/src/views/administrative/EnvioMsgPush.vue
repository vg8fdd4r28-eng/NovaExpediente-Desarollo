<template>
  <div>
    <CCard
      style="border-radius: 16px !important; margin-top: 20px;margin-bottom: 20px;"
    >
      <CCardHeader
        style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
      >
        <strong>Notificaciones Push</strong>
      </CCardHeader>
      <CCardBody
        style="padding-top: 0px; padding-bottom: 0px;margin-bottom: 20px;"
      >
        <FormulateForm
          name="formulario"
          v-model="data"
          class="mt-3"
        >
          <div>
            <CRow>
              <CCol sm="6" class="">
                <CCol sm="12">
                  <FormulateInput
                    type="text"
                    name="titulo"
                    label="Titulo para notificación Push"
                    validation="required|max:100,length"
                    validation-name="Titulo para notificación Push"
                    :validation-messages="{
                      required: 'Ingrese un titulo para notificación Push.'
                    }"
                    :input-class="['form-control']"
                  />
                </CCol>

                <CCol sm="12" class="wrapper">
                  <FormulateInput
                    type="textarea"
                    name="msj"
                    label="Mensaje para notificación Push"
                    validation="required|max:8000,length"
                    validation-name="Mensaje para notificación Push"
                    :validation-messages="{
                      required: 'Ingrese un mensaje para notificación Push.'
                    }"
                    :input-class="['form-control']"
                    rows="3"
                    :help="helpTextArea"
                    :help-class="['hint']"
                  />

                  <!-- <textarea class="regular-input" v-model="msj"></textarea> -->

                  <emojipicker @emoji="insert" :search="search">
                    <button
                      class="emoji-invoker"
                      slot="emoji-invoker"
                      slot-scope="{ events: { click: clickEvent } }"
                      @click.stop="clickEvent"
                    >
                      <svg
                        viewBox="0 0 24 24"
                        xmlns="http://www.w3.org/2000/svg"
                        class="h-6 w-6 fill-current text-grey"
                      >
                        <path d="M0 0h24v24H0z" fill="none" />
                        <path
                          d="M11.99 2C6.47 2 2 6.48 2 12s4.47 10 9.99 10C17.52 22 22 17.52 22 12S17.52 2 11.99 2zM12 20c-4.42 0-8-3.58-8-8s3.58-8 8-8 8 3.58 8 8-3.58 8-8 8zm3.5-9c.83 0 1.5-.67 1.5-1.5S16.33 8 15.5 8 14 8.67 14 9.5s.67 1.5 1.5 1.5zm-7 0c.83 0 1.5-.67 1.5-1.5S9.33 8 8.5 8 7 8.67 7 9.5 7.67 11 8.5 11zm3.5 6.5c2.33 0 4.31-1.46 5.11-3.5H6.89c.8 2.04 2.78 3.5 5.11 3.5z"
                        />
                      </svg>
                    </button>
                    <div
                      slot="emoji-picker"
                      slot-scope="{ emojis, insert, display }"
                    >
                      <div
                        class="emoji-picker"
                        :style="{
                          top: display.y + 'px',
                          left: display.x + 'px'
                        }"
                      >
                        <div class="emoji-picker__search">
                          <input type="text" v-model="search" v-focus />
                        </div>
                        <div>
                          <div
                            v-for="(emojiGroup, category) in emojis"
                            :key="category"
                          >
                            <h5>{{ category }}</h5>
                            <div class="emojis">
                              <span
                                v-for="(emoji, emojiName) in emojiGroup"
                                :key="emojiName"
                                @click="insert(emoji)"
                                :title="emojiName"
                                >{{ emoji }}</span
                              >
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </emojipicker>
                </CCol>
              </CCol>

              <CCol sm="6">
                <CCol sm="12">
                  <FormulateInput
                    type="myInputMultiSelect"
                    name="segmentos"
                    label="Seleccione los segmentos"
                    input-class="form-control"
                    validation-name="Segmentos"
                    :options="options"
                  />
                </CCol>

                <CCol sm="6">
                  <label>Enviar a todos</label>
                  <FormulateInput
                    type="myInputSwitch"
                    name="todos"
                    :input-class="['form-control', 'readonly']"
                    validation-name="Enviar a todos"
                    class="hint"
                    :disabled="false"
                  />
                </CCol>
                <CCol sm="12">
                  <p class="hint">
                    Al seleccionar enviar a todos, no se tomarán en cuenta los
                    segmentos y se enviará la notificación a toda la población.
                  </p>
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
                 Enviar</CButton>
              </CCol>
            </CRow>
          </div>
        </FormulateForm>
      </CCardBody>
    </CCard>
  </div>
</template>

<script>
import { SendNotificationPush } from "./request";
import Vue from "vue";
import _ from "lodash";
// métodos comunes
import common from "../masterdata/common.js";
import { GetClientes } from "./request";

export default {
  name: "EnvioMsgPush",
  mixins: [common],
  props: {
    origen: { type: String, default: "" }, // origen desde donde se accedio la vista
    idTipoSolicitud: { type: Number, default: 0 }, // id del tipo de la solicitud generada
    tipoSolicitud: { type: String, default: "" }, // nombre del tipo de la solicitud generada
    idSolicitud: { type: Number, default: null } // id de la solicitud, para obtener el obj Json con la informacion a cargar
  },
  components: {},
  data() {
    return {
      data: {
        msj: "",
        titulo: "",
        todos: "No",
        segmentos: [],
        segmentosStr: []
      },
      showModal: false,
      showModalAgregar: false,
      showModalConfirmacion: false,
      showModalVisorAdjuntos: false,
      esEdicion: false,
      visorEnable: false,
      tituloModal: "",
      srcDocumento: "",
      value: null,
      options: [],
      search: ""
    };
  },
  async mounted() {
    await this.cargarClientes();
  },
  methods: {
    async cargarClientes() {
      const {
        data: { clientes }
      } = await GetClientes();
      this.options = clientes.map(({ idcliente, nombre }) => ({
        name: nombre,
        value: idcliente
      }));
    },
    async submit(formValues) {
      this.data.segmentosStr = this.data.segmentos.map(x => x.name);

      console.log(this.data);

      var res = await SendNotificationPush(this.data);
      console.log(res);

      this.data.msj = "";
      this.data.titulo = "";
      this.data.todos = "No";
      this.data.segmentos = [];

      Vue.$toast.success("Se ha enviado el mensaje", {});
    },
    insert(emoji) {
      this.data.msj += emoji;
    }
  },
  computed: {
    helpTextArea() {
      if (this.data.msj) {
        return `Tiene disponible un máximo de ${8000 -
          this.data.msj.length} carácteres.`;
      }
      return null;
    }
  },
  watch: {}
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
  content: "";
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
