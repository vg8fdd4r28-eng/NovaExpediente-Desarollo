<template>
  <div class="c-app flex-row align-items-center" style="background-image:url('/img/fondo-login.jpg')">
    <CContainer>
      <CRow class="justify-content-center">
        <CCol md="8">
          <CCardGroup>
            <CCard class="p-4">
              <CCardBody>
                <CForm>
                  <h1>Bienvenido</h1>
                  <p class="text-muted text-justify">Para mantenerse conectado al <strong>expediente digital</strong> 
                    de Novacomp, ingrese sus credenciales de la organización.</p>
                  <!-- <CInput
                    placeholder="Username"
                    autocomplete="username email"
                  >
                    <template #prepend-content><CIcon name="cil-user"/></template>
                  </CInput>
                  <CInput
                    placeholder="Password"
                    type="password"
                    autocomplete="curent-password"
                  >
                    <template #prepend-content><CIcon name="cil-lock-locked"/></template>
                  </CInput> -->
                  <CRow>
                    <CCol col="12" class="text-left">
                      <CButton style="background-color:rgb(12, 123, 190) !important; color:white" class="px-4" @click="login">Iniciar sesión</CButton>
                    </CCol>
                    <!-- <CCol col="6" class="text-right">
                      <CButton color="link" class="px-0">Forgot password?</CButton>
                      <CButton color="link" class="d-lg-none">Register now!</CButton>
                    </CCol> -->
                  </CRow>
                </CForm>
              </CCardBody>
            </CCard>
            <CCard
              style="background-color:rgb(12, 123, 190) !important;"
              text-color="white"
              class="text-center py-5 d-md-down-none"
              body-wrapper
            >
              <CCardBody>
                <h2></h2>
               <div>
                <!-- <CIcon name="logo" height="48" alt="Logo"/> -->
                <img src="../../assets/Logo-blanco.png" class="w-100 h-100"/>
              </div>
              </CCardBody>
            </CCard>
          </CCardGroup>
        </CCol>
      </CRow>
    </CContainer>
  </div>
</template>

<script>
export default {
  name: 'Login',
  methods: {
    login(param) {
      this.$root.signIn('/'); //this.$AuthService.loginRedirect() //with a redirect
    }
  }
}
</script>
