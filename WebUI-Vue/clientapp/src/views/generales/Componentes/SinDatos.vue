<template>
  <CBadge color="secondary color badge SinDatos" class="mr-2 text-wrap mt-2 SinDatos" shape="pill">
    {{ mensaje }} </CBadge
  >
</template>

<script>
export default {
  name: "SinDatos",
  props: { mensaje: { type: String, default: "No existen datos" } },
};
</script>
<style scoped>
.SinDatos {
    color: #002E6E !important;
}
</style>
