<template>
  <SelectButton
    :options="optionsReferencias"
    :binary="true"
    :name="context.name"
    v-model="context.model"
    @input="onChange()"
    :disabled="context.attributes.disabled"
    class="switch"
  />
</template>

<script>
export default {
  name: "InputSwitchFormulate",
  props: {
    context: {
      type: Object,
      required: true
    },
    disabled:{
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      optionsReferencias: ["Si", "No"]
    };
  },
  methods: {
    onChange(){
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
