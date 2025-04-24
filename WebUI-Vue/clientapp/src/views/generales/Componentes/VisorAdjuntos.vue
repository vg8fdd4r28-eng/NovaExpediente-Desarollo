<template>
  <div class="mt-3">
    <CRow v-if="!pdf">
      <CCol sm="12" md="12" class="pull-right mb-3">
        <CImg
          :src="archivo"
          block
          class="rounded-square visor "
          width="auto"
          height="auto"
          id="fileAdjunto"
          align="center"
        />
      </CCol>
    </CRow>

    <CRow v-else>
      <CCol sm="12" md="12" class="pull-right mb-3 pdf">
        <vue-pdf-app :pdf="archivo"></vue-pdf-app>
      </CCol>
    </CRow>
  </div>
</template>

<script>
import VuePdfApp from "vue-pdf-app";
// import this to use default icons for buttons
import "vue-pdf-app/dist/icons/main.css";

export default {
  name: "VisorAdjuntos",
  components: {
    "vue-pdf-app": VuePdfApp
  },
  props: {
    archivo: { type: String, default: "" },
    pdf: { type: Boolean, default: false }
 },
  // watch: {
  //   archivo: async function(newVal, oldVal) {
    
  //     await this.imageToBase64(newVal);
  //   }
  // },
  
  data() {
    return {
      src: this.archivo
    };
  },
  methods: {
    imageToBase64(file) {
      var reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.src = reader.result;
      };
      reader.onerror = function(error) {
        console.log("Error: ", error);
      };
    }
  },
  computed: {
    detalleArchivo() {
    //  if (this.archivo != null) {
     //   const esImagen = false;
        /// console.log(this.archivo[0].type);
        //convertir 64
     //   this.imageToBase64(this.archivo);
        // obtener el type
        // if (this.archivo[0].type == "image/png") {
        //   esImagen = true;
        // }

      //  return { src: this.src, imagen: esImagen };
      return this.src
      }
    
  }
};
</script>

<style>
.pdf {
  width: 500px;
  height: 600px;
}
.visor {
height: 100% !important;
width: 100% !important; 

}

</style>
