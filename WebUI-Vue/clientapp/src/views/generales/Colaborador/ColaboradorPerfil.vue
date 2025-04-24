<template>
  <div>
    <!-- Botón de atrás -->
    <CRow v-if="this.$route.params.origen != undefined">
      <CCol sm="12">
        <CCol
          md="2"
          sm="12"
          class="pull-right mb-2"
          style="padding-left:5px; padding-right:0px"
        >
          <CButton
            block
            color="primary"
            @click="atras()"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark"
          >
            <i class="pi pi-arrow-circle-left"></i>
            Regresar a la lista</CButton
          >
        </CCol>
      </CCol>
    </CRow>

    <!-- Cards de información personal -->
    <CRow class="pb-3">
      <CCol sm="12" md="4">
        <CCard
          id="TFondosPersonales1"
          ref="TFondosPersonales1"
          v-if="!isConsulta"
          accent-color="info"
          class="h-100"
        >
          <CCardHeader
            id="TDatosPersonales1"
            ref="TDatosPersonales1"
            v-if="!this.isCollapsedDetail1"
            style="background: #54AADD; visibility: visible;"
          >
            <CIcon
              name="acercaDelColaborador"
              class="text-primary font-weight-bold"
            />
            <strong> Acerca del colaborador</strong>
            <div class="card-header-actions">
              <CLink
                class="card-header-action btn-minimize"
                @click="cambioPanelesDatosPersonales('panel1')"
              >
                <CIcon
                  :name="`cil-chevron-${isCollapsedDetail1 ? 'bottom' : 'top'}`"
                />
              </CLink>
            </div>
          </CCardHeader>
          <CCollapse :show="isCollapsedDetail1" :duration="400">
            <CCardBody color="">
              <CRow>
                <CCol sm="12" md="12">
                  <CRow>
                    <CCol sm="12" md="2" class="d-flex">
                      <CButton
                        @click="cargarSolicitud('Datos Personales')"
                        style="color:#2196F3; visibility: visible; margin-bottom: 131px;"
                        class="d-inline px-0 py-0"
                        v-tooltip.right="
                          'Registrar solicitud de cambio para agregar o modificar la información acerca del colaborador.'
                        "
                      >
                        <CIcon
                          style="color: rgba(84, 170, 221, 1)"
                          class="posicion-icono"
                          :name="`cilPencil`"
                        />
                      </CButton>
                    </CCol>
                    <CCol sm="12" md="8" class="d-flex justify-content-center ">
                      <CImg
                        :src="this.colaborador.foto"
                        block
                        class="mb-2 rounded-circle border border-light shadow"
                        width="150"
                        height="150"
                      />
                    </CCol>
                    <CCol sm="12" md="2" class="d-flex">
                      <CLink
                        class="card-header-action btn-minimize"
                        @click="cambioPanelesDatosPersonales('panel1')"
                      >
                        <CIcon
                          style="color: rgba(84, 170, 221, 1)"
                          :name="
                            `cil-chevron-${
                              isCollapsedDetail1 ? 'bottom' : 'top'
                            }`
                          "
                        />
                      </CLink>
                    </CCol>
                  </CRow>
                  <CRow class="pt-2 justify-content-center">
                    <CCol sm="6" md="8" class="d-flex justify-content-center ">
                      <h5 class="label-title label-name">
                        {{ this.colaborador.nombre }}
                      </h5>
                    </CCol>
                  </CRow>
                  <CRow class="pt-2">
                    <CCol sm="6" md="5">
                      <span class="label-title">Puesto</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.puesto }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="pt-2">
                    <CCol sm="6" md="5">
                      <span class="label-title">Especialidad</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.ramaespecialidad }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="pt-2">
                    <CCol sm="6" md="5">
                      <span class="label-title">Líder asignado</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.deliveryManager }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="pt-2">
                    <CCol sm="6" md="5">
                      <span class="label-title">Área</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.areaOrganizativa }}</span>
                    </CCol>
                  </CRow>

                  <CRow class="pt-2">
                    <CCol sm="6" md="5">
                      <span class="label-title">Ubicado en </span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.ubicadoEn }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="pt-2">
                    <CCol sm="6" md="5">
                      <span class="label-title">Empresa </span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.empresaCorporacion }}</span>
                    </CCol>
                  </CRow>

                  <CRow class="pt-2">
                    <CCol sm="6" md="5">
                      <span class="label-title">País origen</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.paisOrigen }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="pt-2">
                    <CCol sm="6" md="5">
                      <span class="label-title">CPIC</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.numerocpic }}</span>
                      <button
                        style="margin-left:2px;"
                        class="btn btnVerDocumento onHoverDarkVerDoc"
                        title="Ver documento del CPIC"
                        v-if="this.colaborador.numerocpic"
                        @click="verDocumentoCPIC"
                      >
                        Ver documento
                        <!-- <i class="pi pi-eye" /> -->
                      </button>
                    </CCol>
                  </CRow>

                  <CRow class="pt-2">
                    <CCol sm="6" md="5">
                      <span class="label-title">Estado</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.estado }}</span>
                    </CCol>
                  </CRow>
                </CCol>
              </CRow>
            </CCardBody>
          </CCollapse>
        </CCard>
      </CCol>
      <CCol sm="12" md="8">
        <CCard
          id="TFondosPersonales2"
          ref="TFondosPersonales2"
          v-if="!isConsulta"
          accent-color="info"
          class="h-100"
        >
          <CCardHeader
            id="TDatosPersonales2"
            ref="TDatosPersonales2"
            v-if="!this.isCollapsedDetail1"
            style="background: #54AADD; visibility: visible;"
          >
            <CIcon
              name="datosPersonales"
              class="text-primary font-weight-bold"
            /><strong> Datos Personales</strong>
            <div class="card-header-actions">
              <CLink
                class="card-header-action btn-minimize"
                @click="cambioPanelesDatosPersonales('panel1')"
              >
                <CIcon
                  :name="`cil-chevron-${isCollapsedDetail1 ? 'bottom' : 'top'}`"
                />
              </CLink>
            </div>
          </CCardHeader>
          <CCollapse :show="isCollapsedDetail1" :duration="400">
            <CCardBody>
              <CRow style="margin-bottom: 13px;">
                <CCol>
                  <CButton
                    @click="cargarSolicitud('Datos Personales')"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.right="
                      'Registrar solicitud de cambio para agregar o modificar la información de datos personales.'
                    "
                  >
                    <CIcon
                      style="color: rgba(84, 170, 221, 1)"
                      class="posicion-icono"
                      :name="`cilPencil`"
                    />
                  </CButton>
                </CCol>
                <CCol>
                  <div class="card-header-actions">
                    <CLink
                      class="card-header-action btn-minimize"
                      @click="cambioPanelesDatosPersonales('panel1')"
                    >
                      <CIcon
                        style="color: rgba(84, 170, 221, 1)"
                        :name="
                          `cil-chevron-${isCollapsedDetail1 ? 'bottom' : 'top'}`
                        "
                      />
                    </CLink>
                  </div>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Identificación</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.identificacion }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Estado civil</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.estadocivil }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Fecha nacimiento</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.fechanacimiento }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Edad</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.edad }}</span>
                </CCol>
              </CRow>

              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Género</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.genero }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Hijos</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.numerohijos }}</span>
                </CCol>
              </CRow>

              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Fecha de ingreso</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.fechaingreso }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Antigüedad</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.antiguedad }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Celular</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.celular }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Teléfono</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.telefono }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">País de residencia</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.paisResidencia }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Ciudad</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.ciudadComputed }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Dirección exacta</span>
                </CCol>
                <CCol sm="12" md="9">
                  <span>{{ this.colaborador.direccion }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Correo empresa</span>
                </CCol>
                <CCol sm="6" md="9">
                  <span>{{ this.colaborador.correo }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Correo personal</span>
                </CCol>
                <CCol sm="6" md="9">
                  <span>{{ this.colaborador.correopersonal }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="12" md="12">
                  <span class="label-title">En caso de emergencia</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span class="label-title">Llamar a</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.personallamar }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Al teléfono</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.telefonoemergencia }}</span>
                </CCol>
              </CRow>

              <CRow>
                <CCol sm="12" md="12">
                  <span class="label-title">Esquema de vacunación</span>
                </CCol>
                <CCol sm="12" md="12" v-if="this.tieneCertificadoVacunacion">
                  <ul id="vacunacion">
                    <li v-for="item in vacunasColaborador" :key="item.idVacuna">
                      {{
                        "Vacuna: " +
                          item.nombreVacuna +
                          " | Fecha de aplicación: " +
                          item.fecha
                      }}
                      <!-- {{ this.setVacunacion(item) }} -->
                    </li>
                  </ul>
                </CCol>
                <CCol sm="12" md="12">
                  <button
                    style="margin-left:2px;"
                    class="btn btnVerDocumento onHoverDarkVerDoc"
                    title="Ver documento de vacunación"
                    v-if="this.tieneCertificadoVacunacion"
                    @click="verDocumentoVacunacion"
                  >
                    Ver certificado
                    <!-- <i class="pi pi-eye" /> -->
                  </button>
                  <p
                    v-if="!this.tieneCertificadoVacunacion"
                    class="btnVerDocNoExiste"
                  >
                    No se ha registrado el esquema de vacunación
                  </p>
                </CCol>
              </CRow>
            </CCardBody>
          </CCollapse>
        </CCard>
      </CCol>
      <CCol sm="12" md="12">
        <CCard v-if="isConsulta" accent-color="info" class="h-100">
          <CCardHeader>
            <CIcon
              name="acercaDelColaborador"
              class="text-primary font-weight-bold"
            /><strong> Acerca del colaborador</strong>
            <div class="card-header-actions">
              <CLink
                class="card-header-action btn-minimize"
                @click="isCollapsedDetail3 = !isCollapsedDetail3"
              >
                <CIcon
                  :name="`cil-chevron-${isCollapsedDetail3 ? 'bottom' : 'top'}`"
                />
              </CLink>
            </div>
          </CCardHeader>
          <CCollapse :show="isCollapsedDetail3" :duration="400">
            <CCardBody color="">
              <CRow>
                <CCol sm="12" md="4">
                  <CCol sm="12" md="12" class="d-flex justify-content-center ">
                    <CImg
                      :src="this.colaborador.foto"
                      block
                      class="mb-2 rounded-circle border border-light shadow"
                      width="150"
                      height="150"
                    />
                  </CCol>
                  <CRow class="pt-2">
                    <CCol
                      sm="12"
                      md="12"
                      class="d-flex justify-content-center "
                    >
                      <h5 class="label-title label-name">
                        {{ this.colaborador.nombre }}
                      </h5>
                    </CCol>
                    <CCol
                      sm="12"
                      md="12"
                      class="d-flex justify-content-center "
                    >
                      <span>
                        {{ this.colaborador.puesto }}
                      </span>
                    </CCol>
                  </CRow>
                </CCol>
                <CCol sm="12" md="8">
                  <CRow class="form-group">
                    <CCol sm="6" md="2">
                      <span class="label-title">Empresa</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.empresaCorporacion }}</span>
                    </CCol>
                    <CCol sm="6" md="2">
                      <span class="label-title">Estado</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.estado }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="form-group">
                    <CCol sm="6" md="2">
                      <span class="label-title">Fecha de ingreso</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.fechaingreso }}</span
                      ><br />
                      <span class="font-italic small">{{
                        this.colaborador.antiguedad
                      }}</span>
                    </CCol>
                    <CCol sm="6" md="2">
                      <span class="label-title">Especialidad</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.ramaespecialidad }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="form-group">
                    <CCol sm="6" md="2">
                      <span class="label-title">Área</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.areaOrganizativa }}</span>
                    </CCol>
                    <CCol sm="6" md="2">
                      <span class="label-title">CPIC</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.numerocpic }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="form-group">
                    <CCol sm="6" md="2">
                      <span class="label-title">Líder Asignado</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.deliveryManager }}</span>
                    </CCol>
                    <CCol sm="6" md="2">
                      <span class="label-title">Ubicado en</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.ubicadoEn }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="form-group">
                    <CCol sm="6" md="2">
                      <span class="label-title">País origen</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.paisOrigen }}</span>
                    </CCol>
                    <CCol sm="6" md="2">
                      <span class="label-title">País residencia</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.paisResidencia }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="form-group">
                    <CCol sm="6" md="2">
                      <span class="label-title">Celular</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.celular }}</span>
                    </CCol>
                    <CCol sm="6" md="2">
                      <span class="label-title">Fecha de nacimiento</span>
                    </CCol>
                    <CCol sm="6" md="4">
                      <span>{{ this.colaborador.fechanacimiento}}</span
                      ><br />
                      <span class="font-italic small">{{
                        this.colaborador.nacimiento
                      }}</span>
                    </CCol>
                  </CRow>
                  <CRow class="form-group">
                    <CCol sm="6" md="2">
                      <span class="label-title">Dirección</span>
                    </CCol>
                    <CCol sm="6" md="10">
                      <span>{{ this.colaborador.direccion }}</span>
                    </CCol>
                  </CRow>
                </CCol>
              </CRow>
            </CCardBody>
          </CCollapse>
        </CCard>
      </CCol>
    </CRow>

    <!-- Formación - Experiencias - Proyectos -->
    <TabView>
      <TabPanel header="Formación">
        <CRow>
          <CCol sm="12" md="12" class="mt-2 text-center">
            <p class="texto-seccion">
              Te encuentras en la sección de Formación, donde puedes consultar
              tu información académica. Para agregar o modificar tu información
              puedes registrar solicitudes de cambio y enviarlas a validación
              con talento humano (la revisión tendrá un plazo de 24 horas). Te
              comentamos lo siguiente: es importante adjuntar la documentación
              que evidencia tus registros de formación y el tamaño máximo
              soportado para los archivos es de 1.5 MB.
            </p>
          </CCol>
        </CRow>
        <CRow>
          <CCol sm="12" md="6" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TFormaciones" ref="TFormaciones">
                <CIcon
                  name="formacionAcademica"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  Formación Académica</strong
                >
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Formación Académica')"
                    v-if="this.panelFormacionAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de formación academica.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panel1')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedFormaciones ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedFormaciones" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <Timeline
                    :timeline-items="timelineItems"
                    :message-when-no-items="messageWhenNoItems"
                    dateLocale="es-CR"
                    colorDots="rgb(12, 123, 190)"
                  />
                  <CButton
                    @click="cargarSolicitud('Formación Académica')"
                    class="estilo-personalizado"
                    v-if="!this.panelFormacionAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de formación academica.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="12" md="6" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TCertificaciones" ref="TCertificaciones">
                <CIcon
                  name="certificaciones"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  Certificaciones</strong
                >
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Certificaciones')"
                    v-if="this.panelCertificacionesAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de certificaciones.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panel1')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedFormaciones ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedFormaciones" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <Timeline
                    :timeline-items="timelineCertificacionesItems"
                    :message-when-no-items="messageWhenNoItems"
                    :uniqueYear="true"
                    dateLocale="es-CR"
                    colorDots="rgb(12, 123, 190)"
                  />
                  <CButton
                    @click="cargarSolicitud('Certificaciones')"
                    class="estilo-personalizado"
                    v-if="!this.panelCertificacionesAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar certificaciones.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
        </CRow>
        <CRow>
          <CCol sm="12" md="6" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TEspecialidades" ref="TEspecialidades">
                <CIcon
                  name="especialidades"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados"> Especialidades</strong>
                <div class="card-header-actions">
                  <CButton
                    @click="
                      cargarSolicitud('Asignar especialidad a colaborador')
                    "
                    v-if="this.panelEspecialidadesAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de especialidades.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panel2')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedEspecialidades ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedEspecialidades" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="especialidades.length == 0"
                    mensaje="No tiene información registrada de especialidades"
                  ></SinDatos>
                  <CButton
                    @click="
                      cargarSolicitud('Asignar especialidad a colaborador')
                    "
                    class="estilo-personalizado"
                    v-if="!this.panelEspecialidadesAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar especialidades.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in especialidades"
                    :key="item.idespecialidadconsultor"
                    color="info"
                  >
                    <small class="text-muted"
                      >{{ item.experiencia }} años de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.especialidad }}</strong
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="12" md="6" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TCursos" ref="TCursos">
                <CIcon
                  name="participacionCursos"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  Participación en Cursos</strong
                >
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Cursos')"
                    v-if="this.panelParticipacionCursoAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de participación en cursos.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panel2')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedEspecialidades ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedEspecialidades" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="cursos.length == 0"
                    mensaje="No tiene registrada información de participación en cursos"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Cursos')"
                    class="estilo-personalizado"
                    v-if="!this.panelParticipacionCursoAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar participación en cursos.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in cursos"
                    :key="item.idcursocolaborador"
                    color="info"
                  >
                    <small class="text-muted">{{ item.entidad }} </small><br />
                    <strong class="h6">{{ item.curso }}</strong
                    ><br />

                    <button
                      class="btn btnVerDocumento onHoverDarkVerDoc"
                      href=""
                      @click.prevent="
                        showVisorType(item.idcursocolaborador, 'cursos')
                      "
                      v-if="item.tieneCertificado"
                    >
                      Ver documento</button
                    ><br />
                    <p class="text-danger" v-if="!item.tieneCertificado">
                      Documento no presentado
                    </p>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
        </CRow>
        <CRow>
          <CCol sm="12" md="6" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TIdiomas" ref="TIdiomas">
                <CIcon
                  name="idiomas"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados"> Idiomas</strong>
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Idiomas (Nivel de idioma)')"
                    v-if="this.panelIdiomasAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de idiomas.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panel3')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${isCollapsedIdiomas ? 'bottom' : 'top'}`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedIdiomas" :duration="400">
                <CCardBody>
                  <SinDatos
                    v-if="idiomas.length == 0"
                    mensaje="No tiene información registrada de idiomas"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Idiomas (Nivel de idioma)')"
                    class="estilo-personalizado"
                    v-if="!this.panelIdiomasAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar idiomas.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in idiomas"
                    :key="item.ididioma"
                    color="info"
                  >
                    <small class="text-muted">{{ item.idioma }} </small><br />
                    <strong class="h6">{{ item.nivelIdioma }}</strong
                    ><br />

                    <button
                      class="btn btnVerDocumento onHoverDarkVerDoc"
                      href=""
                      @click.prevent="
                        showVisorType(
                          `${item.ididioma + '-' + item.idcolaborador}`,
                          'idiomas'
                        )
                      "
                      v-if="item.tieneCertificado"
                    >
                      Ver documento</button
                    ><br />
                    <p class="text-danger" v-if="!item.tieneCertificado">
                      Documento no presentado
                    </p>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="12" md="6" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TCursosImpartir" ref="TCursosImpartir">
                <CIcon
                  name="cursosImpartir"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  Cursos que desearía impartir</strong
                >
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Cursos que desearía impartir')"
                    v-if="this.panelDeseaImpartirAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de Cursos que desearía impartir.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panel3')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${isCollapsedIdiomas ? 'bottom' : 'top'}`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedIdiomas" :duration="400">
                <CCardBody>
                  <SinDatos
                    v-if="cursosImpartir.length == 0"
                    mensaje="No tiene información registrada de cursos que desería impartir"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Cursos que desearía impartir')"
                    class="estilo-personalizado"
                    v-if="!this.panelDeseaImpartirAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar cursos que desearía impartir.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CBadge
                    v-for="item in cursosImpartir"
                    :key="item.idcursoimpartir"
                    color="info"
                    class="mr-2"
                  >
                    {{ item.curso }}</CBadge
                  >
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
        </CRow>
        <CRow>
          <!-- <CCol sm="12" md="6" class="mt-2">
                <CCard accent-color="info" class="h-100">
                  <CCardHeader>
                    <CIcon
                      name="habilidadesBlandas"
                      class="text-primary font-weight-bold"
                    /><strong> Habilidades blandas</strong>
                    <div class="card-header-actions">
                      <CLink
                        class="card-header-action btn-minimize"
                        @click="
                          isCollapsedHabilidades = !isCollapsedHabilidades
                        "
                      >
                        <CIcon
                          :name="
                            `cil-chevron-${
                              isCollapsedHabilidades ? 'bottom' : 'top'
                            }`
                          "
                        />
                      </CLink>
                    </div>
                  </CCardHeader>
                  <CCollapse :show="isCollapsedHabilidades" :duration="400">
                    <CCardBody class="cardBodyOverflow">
                      <SinDatos
                        v-if="habilidades.length == 0"
                        mensaje="No tiene información registrada de habilidades blandas"
                      ></SinDatos>

                      <CDataTable
                        class="mb-0 table-outline"
                        hover
                        :items="habilidades"
                        :fields="getColumnasHabilidades"
                        head-color="light"
                        no-sorting
                        v-if="habilidades.length > 0"
                      >
                        <td slot="habilidad" slot-scope="{ item }">
                          <span class="small text-muted">{{
                            item.habilidad
                          }}</span>
                        </td>

                        <td slot="descripcionNivel" slot-scope="{ item }">
                          <span
                            class="badge rounded-pill text-white font-weight-normal"
                            v-bind:class="[
                              item.nivel == 'E'
                                ? 'bg-success'
                                : [
                                    item.nivel == 'P'
                                      ? 'bg-warning'
                                      : [item.nivel == 'A' ? 'bg-danger' : '']
                                  ]
                            ]"
                            style="font-size:.7rem;"
                            >{{ item.descripcionNivel }}</span
                          >
                        </td>
                      </CDataTable>
                    </CCardBody>
                  </CCollapse>
                </CCard>
              </CCol> -->
          <CCol sm="12" md="6" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TAfiliaciones" ref="TAfiliaciones">
                <CIcon
                  name="afiliaciones"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  Otras afiliaciones
                </strong>
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Datos Personales')"
                    v-if="this.panelFormacionAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de otras afiliaciones.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panel4')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedAfiliaciones ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedAfiliaciones" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <Timeline
                    :timeline-items="timelineAfiliaciones"
                    :message-when-no-items="messageWhenNoItems"
                    dateLocale="es-CR"
                    colorDots="rgb(12, 123, 190)"
                  />
                  <CButton
                    @click="cargarSolicitud('Datos Personales')"
                    class="estilo-personalizado"
                    v-if="!this.panelFormacionAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar otras afiliaciones.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
        </CRow>
      </TabPanel>
      <TabPanel header="Experiencia en Tecnología">
        <CRow>
          <CCol sm="12" md="12" class="mt-2 text-center">
            <p class="texto-seccion">
              Te encuentras en la sección de Experiencia, donde puedes consultar
              tu tiempo acumulado de experiencia y el nivel de conocimiento en
              diferentes tecnologías como bases de datos, lenguajes de
              programación y ambientes de desarrollo entre otras. Para agregar o
              modificar tu información puedes registrar solicitudes de cambio y
              enviarlas a validación con tu Delivery Manager. Te comentamos lo
              siguiente: el aplicativo te dejará registrar una cantidad de meses
              de experiencia considerando tu última fecha de actualización en
              una tecnología específica.
            </p>
          </CCol>
        </CRow>
        <CRow>
          <CCol sm="6" md="4" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TBasesDatos" ref="TBasesDatos">
                <CIcon
                  name="baseDatos"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados"> Bases de datos</strong>
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    v-if="this.panelBaseDatosAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de bases de datos.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panelTecnologia1')"
                  >
                    <CIcon
                      :name="`cil-chevron-${isCollapsedBD ? 'bottom' : 'top'}`"
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedBD" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="basesDatos.length == 0"
                    mensaje="No tiene información registrada de bases de datos"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    class="estilo-personalizado"
                    v-if="!this.panelBaseDatosAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar bases de datos.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in basesDatos"
                    :key="item.idbasedatos"
                    color="info"
                  >
                    <small class="text-muted"
                      >{{ item.experiencia }} meses de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.baseDatos }}</strong
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="6" md="4" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TLenguajes" ref="TLenguajes">
                <CIcon
                  name="lenguajesProgramacion"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  Lenguajes de Programación</strong
                >
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    v-if="this.panelLenguajeProgramacionAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de Lenguajes de Programación.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panelTecnologia1')"
                  >
                    <CIcon
                      :name="`cil-chevron-${isCollapsedBD ? 'bottom' : 'top'}`"
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedBD" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="lenguajes.length == 0"
                    mensaje="No tiene información registrada de lenguajes de programación"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    class="estilo-personalizado"
                    v-if="!this.panelLenguajeProgramacionAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar lenguajes de programación.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in lenguajes"
                    :key="item.idlenguajeprogramacion"
                    color="info"
                  >
                    <small class="text-muted"
                      >{{ item.experiencia }} meses de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.lenguajeProgramacion }}</strong
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="6" md="4" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TAmbientes" ref="TAmbientes">
                <CIcon
                  name="ambientesDesarrollo"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  Ambientes de desarrollo</strong
                >
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    v-if="this.panelAmbienteDesarrolloAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de ambientes de desarrollo.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panelTecnologia1')"
                  >
                    <CIcon
                      :name="`cil-chevron-${isCollapsedBD ? 'bottom' : 'top'}`"
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedBD" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="ambientesDesarrollo.length == 0"
                    mensaje="No tiene información registrada de ambientes de desarrollo"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    class="estilo-personalizado"
                    v-if="!this.panelAmbienteDesarrolloAgregar"
                    style="visibility: visible;"
                    v-tooltip.left="
                      'Registrar solicitud de cambio para agregar ambientes de desarrollo.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in ambientesDesarrollo"
                    :key="item.idambientedesarrollo"
                    color="info"
                  >
                    <small class="text-muted"
                      >{{ item.experiencia }} meses de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.ambienteDesarrollo }}</strong
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="6" md="4" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TTecnologias" ref="TTecnologias">
                <CIcon
                  name="tecnologias"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados"> Tecnologías</strong>
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    v-if="this.panelTecnologiasAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de tecnologías.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panelTecnologia2')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedTecnologias ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedTecnologias" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="tecnologias.length == 0"
                    mensaje="No tiene registrada información de tecnologías"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    class="estilo-personalizado"
                    v-if="!this.panelTecnologiasAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar tecnologías.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in tecnologias"
                    :key="item.idtecnologia"
                    color="info"
                  >
                    <small class="text-muted"
                      >{{ item.experiencia }} meses de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.tecnologia }}</strong
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="6" md="4" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TErps" ref="TErps">
                <CIcon
                  name="erps"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados"> ERPs y Core</strong>
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    v-if="this.panelERPCoreAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de ERPs y Core.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panelTecnologia2')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedTecnologias ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedTecnologias" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="erps.length == 0"
                    mensaje="No tiene registrada información de ERPs ni COREs"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    class="estilo-personalizado"
                    v-if="!this.panelERPCoreAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar ERPs y Core.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in erps"
                    :key="item.iderpcore"
                    color="info"
                  >
                    <small class="text-muted"
                      >{{ item.experiencia }} meses de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.erpCore }}</strong>
                    <br /><small class="text-muted"
                      ><strong>Tipo:</strong> {{ item.tipoCore }}
                      <strong>Tecnología:</strong>
                      {{ item.tecnologia }}</small
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="6" md="4" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TCalidad" ref="TCalidad">
                <CIcon
                  name="calidad"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  Herramientas de calidad</strong
                >
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    v-if="this.panelCalidadnAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de herramientas de calidad.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panelTecnologia2')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedTecnologias ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedTecnologias" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="herramientasCalidad.length == 0"
                    mensaje="No tiene información registrada de herramientas de calidad"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    class="estilo-personalizado"
                    v-if="!this.panelCalidadnAgregar"
                    style="visibility: visible;"
                    v-tooltip.left="
                      'Registrar solicitud de cambio para agregar herramientas de calidad.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in herramientasCalidad"
                    :key="item.idcalidad"
                    color="info"
                  >
                    <small class="text-muted"
                      >{{ item.experiencia }} meses de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.calidad }}</strong
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="6" md="4" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TInfraestructura" ref="TInfraestructura">
                <CIcon
                  name="infraestructura"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  Infraestructura</strong
                >
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    v-if="this.panelInfraestructuraAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de infraestructura.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panelTecnologia3')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedInfraestructura ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedInfraestructura" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="infraestructuras.length == 0"
                    mensaje="No tiene información registrada sobre infraestructura"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    class="estilo-personalizado"
                    v-if="!this.panelInfraestructuraAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar infraestructura.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in infraestructuras"
                    :key="item.idinfraestructura"
                    color="info"
                  >
                    <small class="text-muted"
                      >{{ item.experiencia }} meses de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.infraestructura }}</strong
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="6" md="4" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TBuses" ref="TBuses">
                <CIcon
                  name="busesSOA"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados"> Buses y SOA</strong>
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    v-if="this.panelBusesSOAAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de Buses y SOA.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panelTecnologia3')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedInfraestructura ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedInfraestructura" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="buses.length == 0"
                    mensaje="No tiene información registrada sobre buses empresariales y SOA"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    class="estilo-personalizado"
                    v-if="!this.panelBusesSOAAgregar"
                    style="visibility: visible;"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar buses y SOA.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout
                    v-for="item in buses"
                    :key="item.idbus"
                    color="info"
                  >
                    <small class="text-muted"
                      >{{ item.experiencia }} meses de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.bus }}</strong
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
          <CCol sm="6" md="4" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TOfimatica" ref="TOfimatica">
                <CIcon
                  name="bpm"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  BPM y Ofimática</strong
                >
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    v-if="this.panelOfimaticaAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de BPM y ofimática.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panelTecnologia3')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedInfraestructura ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedInfraestructura" :duration="400">
                <CCardBody class="cardBodyOverflow">
                  <SinDatos
                    v-if="bpms.length == 0"
                    mensaje="No tiene información registrada de herramientas de BPM y Ofimática"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    class="estilo-personalizado"
                    v-if="!this.panelOfimaticaAgregar"
                    style="visibility: visible;"
                    v-tooltip.left="
                      'Registrar solicitud de cambio para agregar BPM y ofimática.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                  <CCallout v-for="item in bpms" :key="item.idbpm" color="info">
                    <small class="text-muted"
                      >{{ item.experiencia }} meses de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.bpm }}</strong
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>

    <CCol sm="6" md="4" class="mt-2">
            <CCard accent-color="info" class="h-100">
              <CCardHeader id="TCloud" ref="TCloud">
                <CIcon
                  name="cloud"
                  class="text-primary font-weight-bold"
                />
                <strong class="titulosPersonalizados">
                  Cloud</strong
                >
                <div class="card-header-actions">
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    v-if="this.panelCloudAgregar"
                    style="color:#2196F3; visibility: visible;"
                    class="d-inline px-0 py-0"
                    v-tooltip.top="
                      'Registrar solicitud de cambio para agregar o modificar la información de BPM y ofimática.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPencil`" />
                  </CButton>
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="cambioPaneles('panelTecnologia4')"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${
                          isCollapsedCloud ? 'bottom' : 'top'
                        }`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedCloud" :duration="400">
                <CCardBody class="cardBodyOverflow">
                 <SinDatos
                    v-if="clouds.length == 0"
                    mensaje="No tiene información registrada de herramientas de Cloud"
                  ></SinDatos>
                  <CButton
                    @click="cargarSolicitud('Experiencia en tecnologías')"
                    class="estilo-personalizado"
                    v-if="!this.panelCloudAgregar"
                    style="visibility: visible;"
                    v-tooltip.left="
                      'Registrar solicitud de cambio para agregar herramientas de Cloud.'
                    "
                  >
                    <CIcon class="posicion-icono" :name="`cilPlus`" />
                  </CButton>
                   <CCallout
                    v-for="item in clouds"
                    :key="item.idcloud"
                    color="info"
                  >
                    <small class="text-muted"
                      >{{ item.experiencia }} meses de experiencia</small
                    ><br />
                    <strong class="h6">{{ item.cloud }}</strong
                    ><br />
                    <meter
                      max="1.0"
                      min="0.0"
                      high=".80"
                      low=".20"
                      :value="item.porcentajeConocimiento"
                      optimum="1"
                    ></meter>
                    &nbsp;<small class="text-muted">{{
                      item.nivelConocimiento
                    }}</small>
                  </CCallout>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>

       
        </CRow>
      </TabPanel>
      <TabPanel header="Proyectos">
        <CRow>
          <CCol sm="12" md="12" class="mt-2 text-center">
            <p class="texto-seccion">
              Te encuentras en la sección de Proyectos, donde puedes consultar
              el historial de los proyectos en los que has participado, tanto
              con nuestra compañía Novacomp como en patronos anteriores que has
              tenido. Para agregar o modificar tu información puedes registrar
              solicitudes de cambio y enviarlas a validación con tu Delivery
              Manager.
            </p>
          </CCol>
        </CRow>
        <CRow>
          <CCol sm="12" md="12" class="mt-2">
            <CCard accent-color="info">
              <CCardHeader>
                <CIcon
                  name="cil-justify-center"
                  class="text-primary font-weight-bold"
                /><strong class="titulosPersonalizados">
                  Experiencia en proyectos</strong
                >
                <div class="card-header-actions">
                  <CLink
                    class="card-header-action btn-minimize"
                    @click="isCollapsedProyectos = !isCollapsedProyectos"
                  >
                    <CIcon
                      :name="
                        `cil-chevron-${isCollapsedProyectos ? 'bottom' : 'top'}`
                      "
                    />
                  </CLink>
                </div>
              </CCardHeader>
              <CCollapse :show="isCollapsedProyectos" :duration="400">
                <CCardBody class="h-100">
                  <!--   <SinDatos
                        v-if="proyectos.length == 0"
                        mensaje="No tiene información registrada de proyectos"
                      ></SinDatos>-->

                  <!--   <CRow>
                        <CCol lg="12">
                          <CDataTable
                            :hover="true"
                            :striped="true"
                            :bordered="true"
                            :small="true"
                            :fixed="true"
                            :items="proyectos"
                            :fields="getColumnas"
                            :items-per-page="5"
                            :dark="false"
                            pagination
                            :table-filter="tableFilter"
                            :sorter="true"
                            :clickable-rows="true"
                            @row-clicked="rowClicked"
                            :noItemsView="{
                              noResults: 'No existen coincidencias',
                              noItems: 'No existen datos'
                            }"
                            :itemsPerPageSelect="{
                              label: 'Filas: ',
                              values: [5, 10, 20, 30]
                            }"
                            :sorterValue="{ column: 'nombre', asc: 'true' }"
                          >
                            // eslint-disable-next-line vue/no-unused-vars
                           -     <template #acciones="{item}">
                              <td>
                                <CDropdown
                                  color="link"
                                  size="sm"
                                  :caret="false"
                                  style="margin-top:-7px"
                                  placement="left"
                                >
                                  <template #toggler-content>
                                    <CIcon name="cil-options" />
                                  </template>
                                  <CDropdownItem
                                    @click="verDetalleProyectos(item)"
                                  >
                                    <CIcon name="cil-magnifying-glass" />&nbsp;
                                    Ver Detalle
                                  </CDropdownItem>
                                      <CDropdownItem @click="modificar(item)">
                            <CIcon name="cil-pencil" />&nbsp;Modificar
                          </CDropdownItem>
                         <CDropdownItem @click="eliminar(item)">
                            <CIcon name="cil-delete" />&nbsp;Eliminar
                          </CDropdownItem>
                          <CDropdownItem>
                            <CIcon name="cis-history" />&nbsp;Historial
                          </CDropdownItem>
                                </CDropdown>
                              </td>
                            </template>
                          </CDataTable>
                        </CCol>
                      </CRow>-->

                  <CRow>
                    <CCol lg="12">
                      <DataTable
                        :value="proyectos"
                        :expandedRows.sync="expandedRows"
                        dataKey="idexperienciaproyecto"
                        @rowExpand="onRowExpand"
                        @rowCollapse="onRowCollapse"
                        class="p-datatable-proyectos p-datatable-striped p-datatable-sm"
                        :paginator="true"
                        :rows="10"
                        :rowHover="true"
                        :loading="loading"
                        paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                        :rowsPerPageOptions="[5, 10, 25, 50]"
                        currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} proyectos"
                        sortField="nombreproyecto"
                        :sortOrder="1"
                        stateStorage="session"
                        stateKey="dt-state-proyectos"
                        selectionMode="single"
                        :autoLayout="true"
                      >
                        <template #header>
                          <div class="table-header-container">
                            <Button
                              class="btn btn-primary p-mr-2 libro-marca-azuloscuro"
                              icon="cil-plus"
                              label="Expand All"
                              @click="expandAll"
                              style="margin-left:4px"
                              >Mostrar todos</Button
                            >
                            <Button
                              class="btn btn-primary libro-marca-azuloscuro"
                              icon="pi pi-minus"
                              label="Collapse All"
                              @click="collapseAll"
                              style="margin-left:4px"
                              >Ocultar todos</Button
                            >
                            <CButton
                              @click="
                                cargarSolicitud('Experiencia en Proyectos')
                              "
                              class="estilo-personalizado-proyectos"
                              v-tooltip.top="
                                'Registrar solicitud de cambio para agregar proyectos.'
                              "
                              style="margin-left:2px;margin-rigth:2px"
                            >
                              <CIcon class="posicion-icono" :name="`cilPlus`" />
                            </CButton>
                          </div>
                        </template>
                        <Column :expander="true" headerStyle="width: 3rem" />
                        <Column
                          field="nombreempresa"
                          header="Empresa"
                          sortable
                          headerStyle="width: 15%"
                        >
                          <template #body="slotProps">
                            <span class="p-column-title">Cliente</span>
                            {{
                              slotProps.data.idempresacorporacion != null
                                ? slotProps.data.nombreempresacorporacion
                                : slotProps.data.nombreempresa
                            }}
                          </template>
                        </Column>
                        <Column
                          field="nombrecliente"
                          sortable
                          header="Cliente"
                          headerStyle="width: 20%"
                        >
                          <template #body="slotProps">
                            <span class="p-column-title">Cliente</span>
                            {{
                              slotProps.data.idempresacorporacion != null
                                ? slotProps.data.nombreclienteempresacorporacion
                                : slotProps.data.nombrecliente
                            }}
                            <span
                              v-show="
                                slotProps.data.idempresacorporacion != null
                              "
                              class="badge badge-pill badge-info"
                              ><img src="/img/ico-nova.png"
                            /></span>
                          </template>
                        </Column>
                        <Column
                          field="nombreproyecto"
                          header="Nombre del proyecto"
                          sortable
                          headerStyle="width: 40%"
                        ></Column>
                        <Column
                          field="inicio"
                          sortable
                          header="Fecha inicio"
                          headerStyle="width: 12%"
                        ></Column>
                        <Column
                          field="fin"
                          sortable
                          header="Fecha fin"
                          headerStyle="width: 10%"
                        ></Column>

                        <template #expansion="slotProps">
                          <div class="ml-2">
                            <CRow class="mt-2">
                              <CCol md="2" sm="12">
                                <h6>
                                  Descripción:
                                </h6>
                              </CCol>
                              <CCol md="10" sm="12">
                                {{ slotProps.data.descripcion }}
                              </CCol>
                            </CRow>
                            <CRow class="mt-2">
                              <CCol md="2" sm="12">
                                <h6>
                                  Rol en el proyecto:
                                </h6>
                              </CCol>
                              <CCol md="4" sm="12">
                                {{ slotProps.data.rolproyecto }}
                              </CCol>
                              <CCol md="2" sm="12">
                                <h6>
                                  Licitación:
                                </h6>
                              </CCol>
                              <CCol md="4" sm="12">
                                {{ slotProps.data.licitacion }}
                              </CCol>
                            </CRow>

                            <CRow class="mt-2">
                              <CCol md="2" sm="12">
                                <h6>
                                  Tecnologías:
                                </h6>
                              </CCol>
                              <CCol md="10" sm="12">
                                <CBadge
                                  v-for="item in slotProps.data
                                    .tecnologiaProyecto"
                                  :key="item"
                                  color="secondary"
                                  class="ml-2"
                                  shape="pill"
                                >
                                  <h6
                                    style="margin-top:.2rem; margin-bottom:.2rem; font-size:.6rem;text-transform: uppercase;"
                                  >
                                    {{ item }}
                                  </h6>
                                </CBadge>
                              </CCol>
                            </CRow>
                            <CRow class="mt-2">
                              <CCol md="2" sm="12">
                                <h6>
                                  Herramientas:
                                </h6>
                              </CCol>
                              <CCol md="10" sm="12">
                                <CBadge
                                  v-for="item in slotProps.data
                                    .herramientaProyecto"
                                  :key="item"
                                  color="secondary"
                                  class="ml-2"
                                  shape="pill"
                                >
                                  <h6
                                    style="margin-top:.2rem; margin-bottom:.2rem; font-size:.6rem;text-transform: uppercase;"
                                  >
                                    {{ item }}
                                  </h6>
                                </CBadge>
                              </CCol>
                            </CRow>
                            <CRow class="mt-1">
                              <CCol
                                md="12"
                                class="text-center pt-1 bg-secondary"
                              >
                                <h6>Contactos</h6>
                              </CCol>
                            </CRow>
                            <CRow>
                              <CCol
                                v-if="slotProps.data.contactos.length == 0"
                                md="12"
                                class="text-left"
                              >
                                <span>No tiene contactos registrados</span>
                              </CCol>
                              <CCol
                                v-for="item in slotProps.data.contactos"
                                :key="item.idexperienciaproyectocontacto"
                                md="4"
                                class="text-left text-sm-left"
                              >
                                <label
                                  style="font-size: 1rem; font-weight: 300"
                                  >{{ item.contacto }}</label
                                >
                                <br />
                                <span
                                  class="pi pi-phone text-muted"
                                  style="width: 1.2rem;text-align: center; color:black !important;font-size:.8rem;"
                                  data-toggle="tooltip"
                                  title=""
                                  data-original-title="(870) 288-4149"
                                ></span>
                                <span class="text-muted small">{{
                                  item.telefono != ""
                                    ? item.telefono
                                    : "No registrado"
                                }}</span>
                                <br />
                                <span
                                  class="pi pi-envelope text-muted"
                                  style="width: 1.2rem;text-align: center; color:black !important;font-size:.8rem;"
                                  data-toggle="tooltip"
                                  data-original-title=""
                                  title=""
                                ></span>
                                <span class="text-muted small text-truncate">{{
                                  item.correo != ""
                                    ? item.correo
                                    : "No registrado"
                                }}</span>
                              </CCol>
                            </CRow>
                          </div>
                        </template>
                      </DataTable>
                    </CCol>
                  </CRow>
                </CCardBody>
              </CCollapse>
            </CCard>
          </CCol>
        </CRow>
      </TabPanel>
    </TabView>

    <visor-documentos
      :src="srcDocumento"
      :showModalVisorAdjuntos.sync="showModalVisorAdjuntos"
      @showModalVisor="showModalVisor"
    >
    </visor-documentos>
  </div>
</template>

<script>
import { columnasProyectos, columnasHabilidadesBlandas } from "./constantes";

import {
  GetDetail,
  GetByColaborador,
  GetAfiliacionByColaborador,
  GetCertificadoAfiliacion,
  GetCertificadoCPIC,
  GetEspecialidadesConsultor,
  GetIdiomasColaborador,
  GetCertificacionesColaborador,
  GetCursosColaborador,
  GetCursosImpartir,
  GetHabilidadesColaborador,
  GetExperienciasBaseDatos,
  GetExperienciaLenguaje,
  GetExperienciaAmbienteDesarrollo,
  GetExperienciaTecnologia,
  GetExperienciaERP,
  GetExperienciaCalidad,
  GetExperienciaInfraestructura,
  GetExperienciaBPM,
  GetExperienciaBus,
  GetExperienciaProyecto,
  GetExperienciaCloud,
  GetTecnologiaProyecto,
  GetHerramientaProyecto,
  GetCursoColaboradorDocumento,
  GetCertificacionDocumentoById,
  GetIdiomaColaboradorDocumentoById,
  GetFormacionAcademicaDocumentoById,
  GetVacunacionColaboradorPerfil,
  GetVacunacionColaboradorDocumento,
  GetSolicitudesPorPerfil
} from "./request";

import Timeline from "timeline-vuejs";
import { aplicarFormatoFecha } from "../../../utils";
import SinDatos from "../Componentes/SinDatos";
import CoreUIIcons from "../../icons/CoreUIIcons.vue";
import VisorDocumentos from "../Componentes/VisorDocumentos.vue";
import Vue from "vue";
export default {
  name: "ColaboradorPerfil",
  data() {
    return {
      colaborador: {
        id: null,
        identificacion: "",
        nombre: "",
        primerapellido: "",
        segundoapellido: "",
        fechanacimiento: "",
        edad: 0,
        genero: "",
        fechaingreso: "",
        antiguedad: 0,
        direccion: "",
        celular: "",
        telefono: "",
        correo: "",
        correopersonal: "",
        resenaperfil: "",
        numerocpic: "",
        ramaespecialidad: "",
        activo: true,
        estadocivil: "",
        telefonoemergencia: "",
        personallamar: "",
        numerohijos: 0,
        areaOrganizativa: "",
        deliveryManager: ""
      },
      query: "",
      query: "",
      isConsulta: false,
      loading: true,
      expandedRows: [],
      isCollapsedDetail1: true,
      isCollapsedDetail2: true,
      isCollapsedDetail3: true,
      isCollapsedFormaciones: true,
      isCollapsedCertificaciones: true,
      isCollapsedEspecialidades: true,
      isCollapsedIdiomas: true,
      isCollapsedCursos: true,
      isCollapsedCursosImpartir: true,
      isCollapsedHabilidades: true,
      isCollapsedBD: true,
      isCollapsedLenguajes: true,
      isCollapsedAmbientes: true,
      isCollapsedTecnologias: true,
      isCollapsedERPSCore: true,
      isCollapsedHerramientasQA: true,
      isCollapsedInfraestructura: true,
      isCollapsedBusesSOA: true,
      isCollapsedBPM: true,
      isCollapsedCloud: true,
      isCollapsedProyectos: true,
      isCollapsedTecnologiasProyecto: true,
      isCollapsedHerramientasProyecto: true,
      isCollapsedAfiliaciones: true,
      messageWhenNoItems: "No hay datos registrados",
      timelineItems: [],
      timelineCertificacionesItems: [],
      timelineAfiliaciones: [],
      especialidades: [],
      idiomas: [],
      cursos: [],
      cursosImpartir: [],
      habilidades: [],
      afiliaciones: [],
      basesDatos: [],
      lenguajes: [],
      ambientesDesarrollo: [],
      tecnologias: [],
      erps: [],
      herramientasCalidad: [],
      infraestructuras: [],
      buses: [],
      bpms: [],
      clouds: [],
      proyectos: [],
      tecnologiasProyectos: [],
      herramientasProyectos: [],
      proyectoLenguaje: [],
      proyectoTecnologia: [],
      proyectoClasificador: [],
      tableFilter: { placeholder: "escriba un texto", label: "Buscar:" },
      showModalVisorAdjuntos: false,
      srcDocumento: null,
      tiposDeSolicitud: [],
      panelFormacionAgregar: true,
      panelCertificacionesAgregar: true,
      panelEspecialidadesAgregar: true,
      panelParticipacionCursoAgregar: true,
      panelIdiomasAgregar: true,
      panelDeseaImpartirAgregar: true,
      panelAfiliacionesAgregar: true,
      panelBaseDatosAgregar: true,
      panelLenguajeProgramacionAgregar: true,
      panelAmbienteDesarrolloAgregar: true,
      panelTecnologiasAgregar: true,
      panelERPCoreAgregar: true,
      panelCalidadnAgregar: true,
      panelInfraestructuraAgregar: true,
      panelBusesSOAAgregar: true,
      panelOfimaticaAgregar: true,
      panelCloudAgregar:true,
      demoID: "demoID",
      tieneCertificadoVacunacion: false,
      vacunasColaborador: []
    };
  },
  components: {
    Timeline,
    SinDatos,
    CoreUIIcons,
    "visor-documentos": VisorDocumentos
  },
  async mounted() {
    window.verDocumentoAfiliacion = this.verDocumentoAfiliacion.bind(this);
    window.verDocumentoCertificacion = this.verDocumentoCertificacion.bind(
      this
    );
    window.verDocumentoFormacion = this.verDocumentoFormacion.bind(this);

    this.isConsulta = this.$root.userRoles.some(p => p == "Expediente.RRHH")
      ? false
      : !this.$route.params.origen
      ? false
      : this.$route.params.isConsulta ||
        this.$root.userRoles.some(p => p == "Expediente.DM") ||
        this.$root.userRoles.some(p => p == "Expediente.Licitaciones") ||
        this.$root.userRoles.some(p => p == "Expediente.Financiero");

    await this.loadData();
    this.loading = false;
    this.$route.params.id = null;
  },
  methods: {
    showVisorType(id, type) {
      switch (type) {
        case "cursos":
          this.verDocumentoCursos(id);
          break;
        case "idiomas":
          this.verDocumentoIdiomas(id);
          break;

        default:
          break;
      }
    },
    showLoader() {
      return this.$loading.show({
        // Optional parameters
        container: this.fullPage ? null : this.$refs.formContainer,
        canCancel: true,
        onCancel: this.onCancel
      });
    },
    hideLoader(loader) {
      setTimeout(() => {
        loader.hide();
      }, 300);
    },
    showModalVisor(show) {
      this.showModalVisorAdjuntos = show;
    },
    atras() {
      switch (this.$route.params.origen) {
        case "Consulta":
          this.$router.push({
            name: "Consulta"
          });
          return;
        case "Colaborador":
          this.$router.push({ name: "Colaboradores" });
          return;
        default:
          return;
      }
    },
    async initData(loader) {
      if (!this.$route.params.id) {
        if (this.$root.infoColaboradorActual.correo) {
          const delay = ms => new Promise(res => setTimeout(res, ms));
          await delay(2000);
        }
      }
      await GetDetail(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      )
        .then(response => {
          if (response.data.colaborador) {
            const {
              fechaingreso,
              fechanacimiento,
              estadocivil,
              genero,
              nombre,
              primerapellido,
              segundoapellido,
              activo
            } = response.data.colaborador;
            this.colaborador = {
              ...response.data.colaborador,
              fechaingreso: aplicarFormatoFecha(fechaingreso, "dd/MM/yyyy"),
              fechanacimiento: aplicarFormatoFecha(
                fechanacimiento,
                "dd/MM/yyyy"
              ),
              estadocivil: this.definirEstadoCivil(estadocivil, genero),
              genero: this.definirGenero(genero),
              nombre: nombre + " " + primerapellido + " " + segundoapellido,
              estado: this.definirEstado(activo),
              id: response.data.colaborador.idcolaborador
            };
          } else {
            this.hideLoader(loader);
            Vue.$toast.warning(
              "Su cuenta de correo no está asociada a un colaborador de la compañía. " +
                "Por favor enviar un correo a novaexpediente@crnova.com indicando la situación. ",
              { duration: 3000 }
            );

            this.$router.push({
              name: "Auth"
            });
          }
        })
        .catch(error => {
          console.log(error);
          Vue.$toast.warning(
            "La sesión se ha cerrado. Por favor ingrese de nuevo. " +
              "Si el error persiste, enviar un correo a novaexpediente@crnova.com indicando la situación. ",
            { duration: 3000 }
          );

          this.$router.push({
            name: "Auth"
          });
          this.hideLoader(loader);
        });
    },
    async initDataFormacionAcademica() {
      const {
        data: { formacionesAcademicas }
      } = await GetByColaborador(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.timelineItems = formacionesAcademicas.map(
        ({
          anio,
          nombre,
          descripcion,
          gradoAcademico,
          centroEstudio,
          tieneCertificado,
          idformacionacademica
        }) => ({
          from: new Date(anio, 0),
          title: nombre,
          description:
            gradoAcademico +
            ", " +
            centroEstudio +
            " " +
            this.getHtmlCertificadoFormaciones(
              tieneCertificado,
              idformacionacademica
            )
        })
      );
    },
    getHtmlCertificadoFormaciones(tieneDocumento, idformacionacademica) {
      if (tieneDocumento) {
        return (
          "<br/><button class='btn btnVerDocumento onHoverDarkVerDoc' onclick='verDocumentoFormacion(" +
          idformacionacademica +
          ")'>Ver documento</button>"
        );
      } else {
        return "<br/><p class='text-danger' style='font-weight:400'>Documento no presentado</p>";
      }
    },
    async verDocumentoFormacion(idformacionacademica) {
      let loader = this.showLoader();
      const {
        data: { formacionAcademica }
      } = await GetFormacionAcademicaDocumentoById(idformacionacademica);

      if (formacionAcademica && formacionAcademica.certificado) {
        this.showModalVisorAdjuntos = true;
        this.srcDocumento = formacionAcademica.certificado;
      } else {
        Vue.$toast.warning(
          "No se tiene registrado el documento de la certificación.",
          {}
        );
      }
      this.hideLoader(loader);
    },

    async initDataAfiliaciones() {
      const {
        data: { afiliaciones }
      } = await GetAfiliacionByColaborador(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.timelineAfiliaciones = afiliaciones.map(
        ({
          fechaInicio,
          nombre,
          descripcion,
          idAfiliacionColaborador,
          tieneCertificado
        }) => ({
          from: new Date(fechaInicio.substring(0, 10)),
          title: nombre,
          description:
            descripcion +
            this.getHtmlCertificado(tieneCertificado, idAfiliacionColaborador)
        })
      );
    },
    getHtmlCertificado(tieneDocumento, idAfiliacionColaborador) {
      if (tieneDocumento) {
        return (
          "<br/><button class='btn btnVerDocumento onHoverDarkVerDoc' onclick='verDocumentoAfiliacion(" +
          idAfiliacionColaborador +
          ")'>Ver documento</button>"
        );
      } else {
        return "<br/><p class='text-danger' style='font-weight:400'>Documento no presentado</p>";
      }
    },
    async verDocumentoAfiliacion(idAfiliacionColaborador) {
      let loader = this.showLoader();
      const {
        data: { certificadoAfiliacion }
      } = await GetCertificadoAfiliacion(idAfiliacionColaborador);

      if (certificadoAfiliacion && certificadoAfiliacion.certificado) {
        this.showModalVisorAdjuntos = true;
        this.srcDocumento = certificadoAfiliacion.certificado;
      } else {
        Vue.$toast.warning(
          "No se tiene registrado el documento de la afiliación.",
          {}
        );
      }
      this.hideLoader(loader);
      //console.log(certificadoAfiliacion.certificado);
      //  alert(idAfiliacionColaborador);

      //  return true;
    },
    async verDocumentoCPIC() {
      try {
        const {
          data: { certificadoCPIC }
        } = await GetCertificadoCPIC(this.colaborador.id);

        if (certificadoCPIC && certificadoCPIC.certificado) {
          this.showModalVisorAdjuntos = true;
          this.srcDocumento = certificadoCPIC.certificado;
        } else {
          Vue.$toast.warning(
            "No se tiene registrado el documento del CPIC",
            {}
          );
        }
      } catch (error) {
        Vue.$toast.error("No se pudo obtener el documento del CPIC", {});
      }
    },

    async verDocumentoVacunacion() {
      try {
        const {
          data: { vacunacionColaborador }
        } = await GetVacunacionColaboradorDocumento(this.colaborador.id);

        if (vacunacionColaborador && vacunacionColaborador.certificado) {
          this.showModalVisorAdjuntos = true;
          this.srcDocumento = vacunacionColaborador.certificado;
        } else {
          Vue.$toast.warning(
            "No se tiene registrado el certificado de vacunación",
            {}
          );
        }
      } catch (error) {
        Vue.$toast.error("No se pudo obtener el certificado de vacunación", {});
      }
    },

    async verDocumentoCursos(id) {
      try {
        const {
          data: { certificado }
        } = await GetCursoColaboradorDocumento(id);

        if (certificado && certificado.certificado) {
          this.showModalVisorAdjuntos = true;
          this.srcDocumento = certificado.certificado;
        } else {
          Vue.$toast.warning(
            "No se tiene registrado el documento del curso",
            {}
          );
        }
      } catch (error) {
        Vue.$toast.error("No se pudo obtener el documento del curso", {});
      }
    },
    async verDocumentoIdiomas(id) {
      try {
        const {
          data: { idiomasColaborador }
        } = await GetIdiomaColaboradorDocumentoById(id);

        if (idiomasColaborador && idiomasColaborador.certificado) {
          this.showModalVisorAdjuntos = true;
          this.srcDocumento = idiomasColaborador.certificado;
        } else {
          Vue.$toast.warning(
            "No se tiene registrado el documento del idioma",
            {}
          );
        }
      } catch (error) {
        Vue.$toast.error("No se pudo obtener el documento del idioma", {});
      }
    },
    async initDataEspecialidades() {
      const {
        data: { especialidadesConsultor }
      } = await GetEspecialidadesConsultor(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.especialidades = especialidadesConsultor.map(
        ({
          especialidad,
          experiencia,
          porcentajeConocimiento,
          nivelConocimiento,
          idespecialidadconsultor
        }) => ({
          especialidad: especialidad,
          experiencia: experiencia,
          porcentajeConocimiento: porcentajeConocimiento,
          nivelConocimiento: nivelConocimiento,
          idespecialidadconsultor: idespecialidadconsultor
        })
      );
    },
    async initDataIdiomas() {
      const {
        data: { idiomasColaborador }
      } = await GetIdiomasColaborador(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.idiomas = idiomasColaborador.map(
        ({
          ididioma,
          idnivelidioma,
          idioma,
          nivelIdioma,
          tieneCertificado,
          idcolaborador
        }) => ({
          idioma: idioma,
          nivelIdioma: nivelIdioma,
          tieneCertificado: tieneCertificado,
          ididioma: ididioma,
          idnivelidioma: idnivelidioma,
          idcolaborador: idcolaborador
        })
      );
    },
    async initDataCertificaciones() {
      const {
        data: { certificacionesColaborador }
      } = await GetCertificacionesColaborador(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.timelineCertificacionesItems = certificacionesColaborador.map(
        ({
          anio,
          certificacion,
          descripcion,
          proveedorCertificacion,
          idcertificacioncolaborador,
          tieneCertificado
        }) => ({
          from: new Date(anio, 0),
          title: certificacion,
          description:
            proveedorCertificacion +
            " " +
            this.getHtmlCertificadoCertificacion(
              tieneCertificado,
              idcertificacioncolaborador
            )
        })
      );
    },
    getHtmlCertificadoCertificacion(
      tieneDocumento,
      idcertificacioncolaborador
    ) {
      if (tieneDocumento) {
        return (
          "<br/><button class='btn btnVerDocumento onHoverDarkVerDoc' onclick='verDocumentoCertificacion(" +
          idcertificacioncolaborador +
          ")'>Ver documento</button>"
        );
      } else {
        return "<br/><p class='text-danger' style='font-weight:400'>Documento no presentado</p>";
      }
    },
    async verDocumentoCertificacion(idcertificacioncolaborador) {
      let loader = this.showLoader();
      const {
        data: { certificado }
      } = await GetCertificacionDocumentoById(idcertificacioncolaborador);

      if (certificado && certificado.certificado) {
        this.showModalVisorAdjuntos = true;
        this.srcDocumento = certificado.certificado;
      } else {
        Vue.$toast.warning(
          "No se tiene registrado el documento de la certificación.",
          {}
        );
      }
      this.hideLoader(loader);
    },
    async initDataCursos() {
      const {
        data: { cursosColaborador }
      } = await GetCursosColaborador(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.cursos = cursosColaborador.map(
        ({ nombre, nombreentidad, idcursocolaborador, tieneCertificado }) => ({
          curso: nombre,
          entidad: nombreentidad,
          idcursocolaborador: idcursocolaborador,
          tieneCertificado: tieneCertificado
        })
      );
    },
    async initDataCursosImpartir() {
      const {
        data: { cursosImpartir }
      } = await GetCursosImpartir(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.cursosImpartir = cursosImpartir.map(({ curso }) => ({
        curso: curso
      }));
    },
    async initDataHabilidades() {
      const {
        data: { habilidadesColaborador }
      } = await GetHabilidadesColaborador(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.habilidades = habilidadesColaborador.map(
        ({ habilidadBlanda, nivelhabilidad }) => ({
          habilidad: habilidadBlanda,
          nivel: nivelhabilidad,
          descripcionNivel: definirNivelHabilidad(nivelhabilidad)
        })
      );
      function definirNivelHabilidad(valor) {
        switch (valor) {
          case "E":
            return "Experto";
          case "P":
            return "Promedio";
          case "A":
            return "Área por mejorar";
          default:
            return "No aplica";
        }
      }
    },
    async initDataBD() {
      const {
        data: { experienciasBaseDatos }
      } = await GetExperienciasBaseDatos(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.basesDatos = experienciasBaseDatos.map(
        ({
          baseDatos,
          mesesexperiencia,
          porcentajeConocimiento,
          nivelConocimiento,
          idbasedatos
        }) => ({
          baseDatos: baseDatos,
          nivelConocimiento: nivelConocimiento,
          experiencia: mesesexperiencia,
          porcentajeConocimiento: porcentajeConocimiento,
          idbasedatos: idbasedatos
        })
      );
    },
    async initDataLenguajes() {
      const {
        data: { experienciaLenguajes }
      } = await GetExperienciaLenguaje(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.lenguajes = experienciaLenguajes.map(
        ({
          lenguajeProgramacion,
          mesesexperiencia,
          porcentajeConocimiento,
          nivelConocimiento,
          idlenguajeprogramacion
        }) => ({
          lenguajeProgramacion: lenguajeProgramacion,
          nivelConocimiento: nivelConocimiento,
          experiencia: mesesexperiencia,
          porcentajeConocimiento: porcentajeConocimiento,
          idlenguajeprogramacion: idlenguajeprogramacion
        })
      );
    },
    async initDataAmbientesDesarrollo() {
      const {
        data: { experienciaAmbientesDesarrollo }
      } = await GetExperienciaAmbienteDesarrollo(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.ambientesDesarrollo = experienciaAmbientesDesarrollo.map(
        ({
          ambienteDesarrollo,
          nivelConocimiento,
          idambientedesarrollo,
          mesesexperiencia,
          porcentajeConocimiento
        }) => ({
          ambienteDesarrollo: ambienteDesarrollo,
          nivelConocimiento: nivelConocimiento,
          experiencia: mesesexperiencia,
          porcentajeConocimiento: porcentajeConocimiento,
          idambientedesarrollo: idambientedesarrollo
        })
      );
    },
    async initDataTecnologias() {
      const {
        data: { experienciaTecnologias }
      } = await GetExperienciaTecnologia(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.tecnologias = experienciaTecnologias.map(
        ({
          tecnologia,
          nivelConocimiento,
          idtecnologia,
          mesesexperiencia,
          porcentajeConocimiento
        }) => ({
          tecnologia: tecnologia,
          nivelConocimiento: nivelConocimiento,
          idtecnologia: idtecnologia,
          experiencia: mesesexperiencia,
          porcentajeConocimiento: porcentajeConocimiento
        })
      );
    },
    async initDataERPS() {
      const {
        data: { experienciasERPs }
      } = await GetExperienciaERP(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.erps = experienciasERPs.map(
        ({
          erpCore,
          nivelConocimiento,
          iderpcore,
          mesesexperiencia,
          porcentajeConocimiento,
          tipoCore,
          tecnologia
        }) => ({
          erpCore: erpCore,
          nivelConocimiento: nivelConocimiento,
          iderpcore: iderpcore,
          experiencia: mesesexperiencia,
          porcentajeConocimiento: porcentajeConocimiento,
          tipoCore: tipoCore,
          tecnologia: tecnologia
        })
      );
    },
    async initDataHerramientasCalidad() {
      const {
        data: { experienciasCalidad }
      } = await GetExperienciaCalidad(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.herramientasCalidad = experienciasCalidad.map(
        ({
          calidad,
          nivelConocimiento,
          idcalidad,
          mesesexperiencia,
          porcentajeConocimiento
        }) => ({
          calidad: calidad,
          nivelConocimiento: nivelConocimiento,
          idcalidad: idcalidad,
          experiencia: mesesexperiencia,
          porcentajeConocimiento: porcentajeConocimiento
        })
      );
    },
    async initDataInfraestructura() {
      const {
        data: { experienciasInfraestructuras }
      } = await GetExperienciaInfraestructura(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.infraestructuras = experienciasInfraestructuras.map(
        ({
          infraestructura,
          nivelConocimiento,
          idinfraestructura,
          mesesexperiencia,
          porcentajeConocimiento
        }) => ({
          infraestructura: infraestructura,
          nivelConocimiento: nivelConocimiento,
          idinfraestructura: idinfraestructura,
          experiencia: mesesexperiencia,
          porcentajeConocimiento: porcentajeConocimiento
        })
      );
    },
    async initDataBus() {
      const {
        data: { experienciasBus }
      } = await GetExperienciaBus(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.buses = experienciasBus.map(
        ({
          bus,
          nivelConocimiento,
          idbus,
          mesesexperiencia,
          porcentajeConocimiento
        }) => ({
          bus: bus,
          nivelConocimiento: nivelConocimiento,
          idbus: idbus,
          experiencia: mesesexperiencia,
          porcentajeConocimiento: porcentajeConocimiento
        })
      );
    },
    async initDataBPM() {
      const {
        data: { experienciasBPM }
      } = await GetExperienciaBPM(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.bpms = experienciasBPM.map(
        ({
          bpm,
          nivelConocimiento,
          idbpm,
          mesesexperiencia,
          porcentajeConocimiento
        }) => ({
          bpm: bpm,
          nivelConocimiento: nivelConocimiento,
          idbpm: idbpm,
          experiencia: mesesexperiencia,
          porcentajeConocimiento: porcentajeConocimiento
        })
      );
    },

    async initDataCloud() {
      const {
        data: { experienciaCloud }
      } = await GetExperienciaCloud(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.clouds = experienciaCloud.map(
        ({
          cloud,
          nivelConocimiento,
          idcloud,
          mesesexperiencia,
          porcentajeConocimiento
        }) => ({
          cloud: cloud,
          nivelConocimiento: nivelConocimiento,
          idcloud: idcloud,
          experiencia: mesesexperiencia,
          porcentajeConocimiento: porcentajeConocimiento
        })
      );
    },

    async initDataProyectosColaborador() {
      const {
        data: { experienciaProyectos }
      } = await GetExperienciaProyecto(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.proyectos = experienciaProyectos.map(
        ({
          idexperienciaproyecto,
          nombreproyecto,
          nombrecliente,
          inicio,
          fin,
          rolproyecto,
          licitacion,
          descripcion,
          tecnologiaProyecto,
          herramientaProyecto,
          contacto,
          telefono,
          correo,
          nombreempresa,
          nombreempresacorporacion,
          nombreclienteempresacorporacion,
          idempresacorporacion,
          idcliente,
          contactos
        }) => ({
          idexperienciaproyecto: idexperienciaproyecto,
          nombreproyecto: nombreproyecto,
          nombrecliente: nombrecliente,
          inicio: inicio,
          fin: fin,
          rolproyecto: rolproyecto,
          licitacion: licitacion,
          descripcion: descripcion,
          tecnologiaProyecto: tecnologiaProyecto.split(","),
          herramientaProyecto: herramientaProyecto.split(","),
          contacto: contacto,
          telefono: telefono,
          correo: correo,
          nombreempresa: nombreempresa,
          nombreempresacorporacion: nombreempresacorporacion,
          nombreclienteempresacorporacion: nombreclienteempresacorporacion,
          idempresacorporacion: idempresacorporacion,
          idcliente: idcliente,
          contactos: contactos
        })
      );
    },
    async initDataTecnologiasProyecto(idexperienciaproyecto) {
      const {
        data: { tecnologiaProyectos }
      } = await GetTecnologiaProyecto(idexperienciaproyecto);

      this.tecnologiasProyectos = tecnologiaProyectos.map(
        ({ idtecnologiaproyecto, nombre }) => ({
          idtecnologiaproyecto: idtecnologiaproyecto,
          nombre: nombre
        })
      );
    },
    async initDataHerramientasProyecto(idexperienciaproyecto) {
      const {
        data: { herramientaProyectos }
      } = await GetHerramientaProyecto(idexperienciaproyecto);

      this.herramientasProyectos = herramientaProyectos.map(
        ({ idherramientaproyecto, nombre }) => ({
          idherramientaproyecto: idherramientaproyecto,
          nombre: nombre
        })
      );
    },

    async initDataEsquemaVacunacion() {
      const {
        data: { vacunacionColaboradorPerfil }
      } = await GetVacunacionColaboradorPerfil(
        this.$route.params.id || this.$root.infoColaboradorActual.id
      );

      this.tieneCertificadoVacunacion =
        vacunacionColaboradorPerfil.tieneCertificado;

      this.vacunasColaborador = vacunacionColaboradorPerfil.vacunasColaboradorPerfil.map(
        ({ idVacuna, nombreVacuna, fecha }) => ({
          nombreVacuna: nombreVacuna,
          fecha: aplicarFormatoFecha(fecha, "dd/MM/yyyy"),
          idVacuna: idVacuna
        })
      );
    },

    setVacunacion(item) {
      let res =
        "Vacuna: " + item.nombreVacuna + "  Fecha de aplicación: " + item.fecha;

      return res;
    },
    async loadData() {
      let loader = this.showLoader();

      await this.initData(loader);
      await this.initDataFormacionAcademica();
      await this.initDataEspecialidades();
      await this.initDataIdiomas();
      await this.initDataCertificaciones();
      await this.initDataCursos();
      await this.initDataCursosImpartir();
      await this.initDataHabilidades();
      await this.initDataAfiliaciones();
      this.hideLoader(loader);

      await this.initDataBD();
      await this.initDataLenguajes();
      await this.initDataAmbientesDesarrollo();
      await this.initDataTecnologias();
      await this.initDataERPS();
      await this.initDataHerramientasCalidad();
      await this.initDataInfraestructura();
      await this.initDataBus();
      await this.initDataBPM();
      await this.initDataCloud();
      await this.initDataProyectosColaborador();
      await this.cargarDatos();
      this.accionesPaneles();
      await this.initDataEsquemaVacunacion();
    },
    definirEstadoCivil(valor, genero) {
      const identificador = genero === "M" ? "o" : genero === "F" ? "a" : "@";
      switch (valor) {
        case "S":
          return "Solter" + identificador;
        case "C":
          return "Casad" + identificador;
        case "D":
          return "Divorciad" + identificador;
        default:
          return "Sin Definir";
      }
    },
    definirGenero(valor) {
      switch (valor) {
        case "M":
          return "Masculino";
        case "F":
          return "Femenino";
        case "I":
          return "Indefinido";
        default:
          return "No definido";
      }
    },
    definirEstado(valor) {
      if (valor) {
        return "Activo";
      } else {
        return "Inactivo";
      }
    },
    async rowClicked(item, index) {
      await this.initDataTecnologiasProyecto(item.idexperienciaproyecto);
      await this.initDataHerramientasProyecto(item.idexperienciaproyecto);
    },

    onRowExpand(event) {
      // this.$toast.add({
      //  severity: "info",
      //  summary: "Product Expanded",
      //   detail: event.data.name,
      //   life: 3000
      //});
    },
    onRowCollapse(event) {
      //  this.$toast.add({
      //   severity: "success",
      //  summary: "Product Collapsed",
      //  detail: event.data.name,
      //  life: 3000
      // });
    },
    expandAll() {
      this.expandedRows = this.proyectos.filter(p => p.idexperienciaproyecto);
      // this.$toast.add({
      //  severity: "success",
      //  summary: "All Rows Expanded",
      //   life: 3000
      // });
    },
    collapseAll() {
      this.expandedRows = null;
      //  this.$toast.add({
      //    severity: "success",
      //    summary: "All Rows Collapsed",
      //    life: 3000
      //  });
    },
    formatCurrency(value) {
      return value.toLocaleString("en-US", {
        style: "currency",
        currency: "USD"
      });
    },
    /**
     * Redirige al usuario a la vista correspondiente del tipo de solicitud
     */
    cargarSolicitud(value) {
      let solicitud = this.tiposDeSolicitud.find(
        solicitud => solicitud.label === value
      );
      this.$router.push({
        name: "MasterData",
        params: {
          origen: "Inbox", // origen desde donde se accedio la vista
          idTipoSolicitud: solicitud.value, // id del tipo de la solicitud generada
          tipoSolicitud: solicitud.label, // nombre del tipo de la solicitud generada
          idSolicitud: null
        }
      });
    },
    async cargarDatos() {
      let colaboradorPerfil = this;

      colaboradorPerfil.loading = true;

      await colaboradorPerfil
        .obtenerTiposSolicitudes()
        // tipos de solicitudes
        .then(function(result) {
          colaboradorPerfil.tiposDeSolicitud = result.data.tiposDeSolicitud.map(
            ({ idTipoSolicitud, tipoSolicitud }) => ({
              value: idTipoSolicitud,
              label: tipoSolicitud
            })
          );
          //  if (colaboradorPerfil.tiposDeSolicitud.length > 0)
          //    colaboradorPerfil.solicitudSeleccionada = colaboradorPerfil.tiposDeSolicitud[0].value;
          //  else colaboradorPerfil.solicitudSeleccionada = 0;
        })
        .catch(error => {
          console.error("(2) Inside error:", error);
        });
    },
    async obtenerTiposSolicitudes() {
      return await GetSolicitudesPorPerfil();
    },

    // seccion de cambios de variable y colores de los encabezados

    cambioPaneles(value) {
      //formacion academica
      if (value == "panel1") {
        this.isCollapsedFormaciones = !this.isCollapsedFormaciones;
        if (this.isCollapsedFormaciones == false) {
          this.$refs.TFormaciones.style.backgroundColor = "#54AADD";
          this.$refs.TCertificaciones.style.backgroundColor = "#54AADD";
        } else {
          this.$refs.TFormaciones.style.backgroundColor = "#B1B1B1";
          this.$refs.TCertificaciones.style.backgroundColor = "#B1B1B1";
        }
      }
      if (value == "panel2") {
        this.isCollapsedEspecialidades = !this.isCollapsedEspecialidades;
        if (this.isCollapsedEspecialidades == false) {
          this.$refs.TEspecialidades.style.backgroundColor = "#54AADD";
          this.$refs.TCursos.style.backgroundColor = "#54AADD";
        } else {
          this.$refs.TEspecialidades.style.backgroundColor = "#B1B1B1";
          this.$refs.TCursos.style.backgroundColor = "#B1B1B1";
        }
      }
      if (value == "panel3") {
        this.isCollapsedIdiomas = !this.isCollapsedIdiomas;
        if (this.isCollapsedIdiomas == false) {
          this.$refs.TIdiomas.style.backgroundColor = "#54AADD";
          this.$refs.TCursosImpartir.style.backgroundColor = "#54AADD";
        } else {
          this.$refs.TIdiomas.style.backgroundColor = "#B1B1B1";
          this.$refs.TCursosImpartir.style.backgroundColor = "#B1B1B1";
        }
      }
      if (value == "panel4") {
        this.isCollapsedAfiliaciones = !this.isCollapsedAfiliaciones;
        if (this.isCollapsedAfiliaciones == false) {
          this.$refs.TAfiliaciones.style.backgroundColor = "#54AADD";
        } else {
          this.$refs.TAfiliaciones.style.backgroundColor = "#B1B1B1";
        }
      }
      //experiencia en tecnologias
      if (value == "panelTecnologia1") {
        this.isCollapsedBD = !this.isCollapsedBD;
        if (this.isCollapsedBD == false) {
          this.$refs.TBasesDatos.style.backgroundColor = "#54AADD";
          this.$refs.TLenguajes.style.backgroundColor = "#54AADD";
          this.$refs.TAmbientes.style.backgroundColor = "#54AADD";
        } else {
          this.$refs.TBasesDatos.style.backgroundColor = "#B1B1B1";
          this.$refs.TLenguajes.style.backgroundColor = "#B1B1B1";
          this.$refs.TAmbientes.style.backgroundColor = "#B1B1B1";
        }
      }

      if (value == "panelTecnologia2") {
        this.isCollapsedTecnologias = !this.isCollapsedTecnologias;
        if (this.isCollapsedTecnologias == false) {
          this.$refs.TTecnologias.style.backgroundColor = "#54AADD";
          this.$refs.TErps.style.backgroundColor = "#54AADD";
          this.$refs.TCalidad.style.backgroundColor = "#54AADD";
        } else {
          this.$refs.TTecnologias.style.backgroundColor = "#B1B1B1";
          this.$refs.TErps.style.backgroundColor = "#B1B1B1";
          this.$refs.TCalidad.style.backgroundColor = "#B1B1B1";
        }
      }

      if (value == "panelTecnologia3") {
        this.isCollapsedInfraestructura = !this.isCollapsedInfraestructura;
        if (this.isCollapsedInfraestructura == false) {
          this.$refs.TInfraestructura.style.backgroundColor = "#54AADD";
          this.$refs.TBuses.style.backgroundColor = "#54AADD";
          this.$refs.TOfimatica.style.backgroundColor = "#54AADD";
        } else {
          this.$refs.TInfraestructura.style.backgroundColor = "#B1B1B1";
          this.$refs.TBuses.style.backgroundColor = "#B1B1B1";
          this.$refs.TOfimatica.style.backgroundColor = "#B1B1B1";
        }
      }



 if (value == "panelTecnologia4") {
        this.isCollapsedCloud = !this.isCollapsedCloud;
        if (this.isCollapsedCloud == false) {
          this.$refs.TCloud.style.backgroundColor = "#54AADD";
        } else {
          this.$refs.TCloud.style.backgroundColor = "#B1B1B1";
        }
      }

    },
    accionesPaneles() {
      // formación academica
      if (this.timelineItems.length == 0) {
        this.panelFormacionAgregar = false;
      }
      if (this.timelineCertificacionesItems.length == 0) {
        this.panelCertificacionesAgregar = false;
      }
      if (this.especialidades.length == 0) {
        this.panelEspecialidadesAgregar = false;
      }
      if (this.cursos.length == 0) {
        this.panelParticipacionCursoAgregar = false;
      }
      if (this.idiomas.length == 0) {
        this.panelIdiomasAgregar = false;
      }
      if (this.cursosImpartir.length == 0) {
        this.panelDeseaImpartirAgregar = false;
      }
      if (this.cursosImpartir.length == 0) {
        this.panelDeseaImpartirAgregar = false;
      }
      // experiencia en tecnología
      if (this.basesDatos.length == 0) {
        this.panelBaseDatosAgregar = false;
      }
      if (this.ambientesDesarrollo.length == 0) {
        this.panelAmbienteDesarrolloAgregar = false;
      }
      if (this.lenguajes.length == 0) {
        this.panelLenguajeProgramacionAgregar = false;
      }
      if (this.tecnologias.length == 0) {
        this.panelTecnologiasAgregar = false;
      }
      if (this.erps.length == 0) {
        this.panelERPCoreAgregar = false;
      }
      if (this.herramientasCalidad.length == 0) {
        this.panelCalidadnAgregar = false;
      }
      if (this.infraestructuras.length == 0) {
        this.panelInfraestructuraAgregar = false;
      }
      if (this.buses.length == 0) {
        this.panelBusesSOAAgregar = false;
      }
      if (this.bpms.length == 0) {
        this.panelOfimaticaAgregar = false;
      }
         if (this.clouds.length == 0) {
        this.panelCloudAgregar = false;
      }
    },

    cambioPanelesDatosPersonales(value) {
      //formacion academica
      if (value == "panel1") {
        this.isCollapsedDetail1 = !this.isCollapsedDetail1;
        if (this.isCollapsedDetail1 == false) {
          this.$refs.TFondosPersonales1.style.visibility = "hidden";
          this.$refs.TFondosPersonales2.style.visibility = "hidden";
        } else {
          this.$refs.TFondosPersonales1.style.visibility = "visible";
          this.$refs.TFondosPersonales2.style.visibility = "visible";
        }
      }
    }
  },
  computed: {
    getColumnas() {
      return columnasProyectos;
    },
    getColumnasHabilidades() {
      return columnasHabilidadesBlandas;
    },
    currentUser() {
      return this.$root.infoColaboradorActual.id;
    },
    ciudadComputed() {
      if (this.colaborador.idpaisresidencia != 51) {
        return this.colaborador.ciudadresidencia;
      } else {
        return this.colaborador.canton;
      }
    }
  },
  watch: {
    // async currentUser() {
    //   if (this.$route.params.origen == undefined) {
    //     await this.loadData();
    //   }
    // }
  }
};
</script>
<style>
.p-dialog-header {
  background-color: #002e6e !important;
  color: #ffffff !important;
}
.p-dialog-content {
  border-color: #002e6e !important;
  /* width: 600px; */
}
.p-dialog-header-maximize {
  color: #ffffff !important;
}
.p-dialog-header-close {
  color: #ffffff !important;
}

.p-dialog-mask {
  z-index: 1049 !important;
}
.label-title {
  font-weight: 700;
}
.label-name {
  font-variant: small-caps;
  text-align: center;
}
.timeline .wrapper-item .section-year {
  font-size: 1rem !important;
}
.timeline .wrapper-item .date-item {
  font-size: 0.8rem !important;
  color: gainsboro !important;
}
.timeline .wrapper-item .description-item {
  font-size: 0.7rem !important;
}
.timeline-item .title-item {
  font-size: 0.8rem !important;
  font-variant: small-caps;
}
.timeline-item .item {
  border-left: 2px solid #ccd5db !important;
}

.cardBodyOverflow {
  overflow-y: auto;
  max-height: 400px;
  overflow-x: hidden;
}

.table-striped tbody tr:nth-of-type(odd) {
  background-color: rgba(173, 216, 230, 0.5) !important;
}

.move-right {
  padding-right: 0px !important;
  padding-left: 0px !important;
}

.libro-marca-celeste {
  background-color: #29abe2;
}

.libro-marca-azuloscuro {
  background-color: #002e6e;
}

.nav-pills .nav-link.active,
.nav-pills .show > .nav-link {
  color: #fff;
  background-color: #002e6e;
}

.nav-link {
  color: #002e6e;
}

.nav-link:hover {
  color: #29abe2;
}
.onHoverDark:hover {
  color: #fff;
  background-color: #002e6e;
}

.btnVerDocumento {
  /* padding: 2pt 2pt 2pt 2pt; */
  border-color: #002e6e;
  color: #002e6e;
  font-size: 11px;
}
.btnVerDocNoExiste {
  /* padding: 2pt 2pt 2pt 2pt; */
  border-color: red;
  color: red;
  font-size: 11px;
}
.onHoverDarkVerDoc:hover {
  color: white !important;
  background-color: #002e6e;
}
.onHoverLigth:hover {
  color: #29abe2;
}

.card-fixed-height {
  height: 480px;
}
.card-fixed-min-height {
  min-height: 400px;
  height: 450px;
}

.breadcrumb-item + .font-xl.breadcrumb-item::before {
  color: rgb(140, 195, 38);
  content: ">>";
  padding: 0px 10px;
}
.table-striped tbody tr:nth-of-type(odd) {
  background-color: rgba(173, 216, 230, 0.5) !important;
}
.page-item.active .page-link {
  background-color: #29abe2;
  border-color: #29abe2;
}
.page-link {
  color: #29abe2;
}
.btn-link {
  color: #002e6e;
}
.pagination {
  justify-content: flex-end !important;
}

/* Responsive: en visualización normal se ocultan las etiquetas asociadas al título de cada columna */
.p-datatable-proyectos .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-proyectos .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-proyectos .p-datatable-tbody > tr > td {
  overflow-wrap: break-word;
}
.p-datatable .p-datatable-header {
  background-color: unset;
  border: unset;
}

/* configuración de los íconos de ordenamiento */
.p-datatable .p-sortable-column .p-sortable-column-icon {
  color: darkgray;
  font-size: 0.75rem;
}
.p-datatable .p-sortable-column.p-highlight .p-sortable-column-icon {
  color: white;
}

/* configuración del "striped" y fila seleccionada de una tabla */
.p-datatable.p-datatable-striped .p-datatable-tbody > tr:nth-child(even) {
  background-color: rgba(173, 216, 230, 0.5);
}

.p-datatable .p-datatable-tbody > tr.p-highlight {
  background: rgba(12, 123, 190, 0.5);
  /* color: #495057; */
}

.p-datatable.p-datatable-striped
  .p-datatable-tbody
  > tr:nth-child(even).p-highlight {
  background: rgba(12, 123, 190, 0.5);
  /* color: #495057; */
}

.p-tabview-nav {
  justify-content: center;
}

.icon-personalizado {
  margin-left: 92%;
  margin-top: 8px;
  margin-bottom: 5px;
}

.p-tabview .p-tabview-nav li .p-tabview-nav-link:not(.p-disabled):focus {
  box-shadow: 0 0 0 0#ebedef;
}

.estilo-personalizado {
  float: right;
  width: 25px;
  height: 26px;
  border-radius: 50%;
  background-color: lightblue;
  padding: 0px;
  margin-left: -25px;
}

.estilo-personalizado-proyectos {
  float: right;
  width: 25px;
  height: 26px;
  border-radius: 50%;
  background-color: lightblue;
  padding: 0px;
}

.posicion-icono {
  margin-left: 3px;
}

.titulosPersonalizados {
  color: white;
}

.card-header {
  background: rgba(177, 177, 177, 1);
}

.card-accent-info {
  border-top: 2px solid rgb(255, 255, 255) !important;
  border-radius: 10px;
}

.c-icon {
  color: white;
}

.SinDatos[data-v-26dd32cc] {
  color: grey !important;
}

.badge-secondary {
  color: black;
  background-color: #ced2d8;
}

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de
   etiqueta y valor asociado */
@media screen and (max-width: 40em) {
  .p-datatable.p-datatable-proyectos .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-proyectos .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-proyectos .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-proyectos .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-proyectos:last-child {
    border-bottom: 1px solid var(--surface-d);
  }
}

@media screen and (max-width: 1200px) {
  .card-fixed-height {
    height: auto !important;
  }
}
@media screen and (max-width: 1200px) {
  .card-fixed-min-height {
    height: auto !important;
    min-height: auto !important;
  }
}
</style>
