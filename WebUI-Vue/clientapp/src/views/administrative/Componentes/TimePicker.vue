<template>
  <vue-timepicker
    :name="context.name"
    v-model="context.model"
    :hour-range="rangoHoras"
    format="hh:mm a"
    close-on-complete
    hide-disabled-hours
    :disabled="context.attributes.disabled"
    @change="onChange"
  ></vue-timepicker>
</template>

<script>
export default {
  name: "TimePicker",
  props: {
    context: {
      type: Object,
      required: true
    },
    disabled: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      rangoHoras: ['8a','9a','10a','11a','12p','1p','2p','3p','4p','5p']
    };
  },
  methods: {
    onChange() {
      this.$emit("change");
    }
  }
};
</script>
<style>
.switch {
  line-height: 10px !important;
}
</style>
