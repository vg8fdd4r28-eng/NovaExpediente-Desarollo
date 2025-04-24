<template>
  <multiselect
    v-model="context.model"
    :options="context.options"
    :multiple="true"
    track-by="name"
    :disabled="context.attributes.disabled"
    :close-on-select="false"
    :clear-on-select="true"
    :preserve-search="true"
    placeholder="Seleccione sus opciones"
    :preselect-first="true"
    label="name"
  >
    <!-- <template slot="selection" slot-scope="{ values, search, isOpen }"><span class="multiselect__single" v-if="values.length &amp;&amp; !isOpen">{{ values.length }} options selected</span></template> -->
  </multiselect>
</template>

<script>
export default {
  name: "InputMultiSelect",
  props: {
    context: {
      type: Object,
      required: true
    },
    disabled: {
      type: Boolean,
      default: false
    },
    options: {
      type: Array,
      default: () => []
    },
    trackby: {
      type: String,
      default: "name"
    }
  },
  data() {
    return {};
  },
  methods: {}
};
</script>
<style></style>
