import Vue from "vue";
import Router from "vue-router";

// Containers
const TheContainer = () => import("@/containers/TheContainer");

// Views
const Dashboard = () => import("@/views/Dashboard");

// Views - Pages
const Page404 = () => import("@/views/pages/Page404");
const Page500 = () => import("@/views/pages/Page500");
//const Login = () => import("@/views/pages/Login");
const Register = () => import("@/views/pages/Register");

const CategoriaNoticia = () => import("@/views/app/CategoriaNoticia");
const CategoriaConvenio = () => import("@/views/app/CategoriaConvenio");

const Noticia = () => import("@/views/app/Noticias/Noticias");

const Convenio = () => import("@/views/app/Convenio/Convenio");
const Actividad = () => import("@/views/app/Actividades/Actividades");

const Sorteos = () => import("@/views/app/Sorteos/Sorteos");

const GradosAcademicos = () =>
  import("@/views/generales/GradosAcademicos/GradosAcademicos");
const ClasificadorGrid = () =>
  import("@/views/generales/Clasificadores/ClasificadorGrid");
const ClasificadorForm = () =>
  import("@/views/generales/Clasificadores/ClasificadorForm");
const Colaborador = () =>
  import("@/views/generales/Colaborador/ColaboradorGrid");
const ColaboradorPerfil = () =>
  import("@/views/generales/Colaborador/ColaboradorPerfil");
const SinDatos = () => import("@/views/generales/Componentes/SinDatos");
const DetalleProyecto = () =>
  import("@/views/generales/Proyectos/DetalleProyecto");
//const Consultas = () => import("@/views/generales/Consultas/Consultas");
const Cliente = () => import("@/views/generales/Cliente/Cliente");
const Proyecto = () => import("@/views/generales/Proyectos/Proyecto");
//const Consulta = () => import("@/views/generales/Consultas/Consulta");  // original
const Consulta = () => import("@/views/administrative/ConsultaIA/ConsultaIA");
const InformeIA  = () => import("@/views/administrative/InformesIA/ConsultaIA");
const Curriculum = () => import("@/views/generales/Curriculum/Curriculum");
const CentroEstudio = () =>
  import("@/views/generales/CentroEstudio/CentroEstudio");
const Certificacion = () =>
  import("@/views/generales/Certificacion/Certificacion");
const Curso = () => import("@/views/generales/Curso/Curso");
const DivisionPolitica = () =>
  import("@/views/generales/DivisionPolitica/DivisionPolitica");
const Empresa = () => import("@/views/generales/Empresa/Empresa");
const Especialidad = () =>
  import("@/views/generales/Especialidad/Especialidad");
const HabilidadBlanda = () =>
  import("@/views/generales/HabilidadBlanda/HabilidadBlanda");
const Idioma = () => import("@/views/generales/Idioma/Idioma");
const NivelConocimiento = () =>
  import("@/views/generales/NivelConocimiento/NivelConocimiento");
const Pais = () => import("@/views/generales/Pais/Pais");
const Puesto = () => import("@/views/generales/Puesto/Puesto");
const Sector = () => import("@/views/generales/Sector/Sector");
const Proveedor = () => import("@/views/generales/Proveedor/Proveedor");
const AmbienteDesarrollo = () =>
  import("@/views/generales/AmbienteDesarrollo/AmbienteDesarrollo");
const BaseDatos = () => import("@/views/generales/BaseDatos/BaseDatos");
const Bus = () => import("@/views/generales/Bus/Bus");
const BPM = () => import("@/views/generales/BPM/BPM");
const Clasificacion = () =>
  import("@/views/generales/Clasificacion/Clasificacion");
const ERP = () => import("@/views/generales/ERP/ERP");
const TipoCore = () => import("@/views/generales/TipoCore/TipoCore");
const AreaOrganizativa = () =>
  import("@/views/generales/AreaOrganizativa/AreaOrganizativa");
const Infraestructura = () =>
  import("@/views/generales/Infraestructura/Infraestructura");
const Lenguaje = () => import("@/views/generales/Lenguaje/Lenguaje");
const Tecnologia = () => import("@/views/generales/Tecnologia/Tecnologia");
const TecnologiaQa = () =>
  import("@/views/generales/TecnologiaQa/TecnologiaQa");
const MasterData = () => import("@/views/masterdata/Solicitud");
const Administrative = () => import("@/views/administrative/Solicitud");
// Authentication
//const AuthCallback = () => import("@/views/auth/Callback");
const AuthCallback = () => import("@/views/Callback");
const AuthSilentRenew = () => import("@/views/silent-renew");
const AuthLogin = () => import("@/views/auth/Login");

const Inbox = () => import("@/views/masterdata/Inbox");
const Outbox = () => import("@/views/masterdata/Outbox");
const Tracking = () => import("@/views/masterdata/Tracking");
const TrackingAdmin = () => import("@/views/administrative/TrackingAdmin");
const ConsultasAdmin = () =>
  import("@/views/administrative/Consultas");
const History = () => import("@/views/masterdata/History");
const Consultants = () => import("@/views/masterdata/MyConsultants");
const ConsultaPermisos = () =>
  import("@/views/administrative/ConsultaPermisos");

const InboxAdmin = () => import("@/views/administrative/Inbox");
const OutboxAdmin = () => import("@/views/administrative/Outbox");
const RedirectAdmin = () => import("@/views/administrative/Redirect");

const RegistroColaborador = () =>
  import("@/views/administrative/RegistroColaborador");

const ConsultaIncapacidad = () =>
  import("@/views/administrative/ConsultaIncapacidad");

  const ArchivoIncapacidad = () =>
  import("@/views/administrative/ArchivoIncapacidad");
  
  const CargaIncapacidad = () =>
  import("@/views/administrative/CargaIncapacidad");

  const CargaSaldosVacaciones = () =>
  import("@/views/administrative/ListaCargaSaldos");

  const CargaImagen= () =>
  import("@/views/administrative/CargaImagen");

  const ArchivoSaldos = () =>
  import("@/views/administrative/ArchivoSaldos");

  const EnvioMsgPush= () =>
      import("@/views/administrative/EnvioMsgPush");

const ActualizacionDatosLaborales = () =>
    import("@/views/administrative/ActualizacionDatosLaborales");

const BitacoraVacaciones = () =>
    import("@/views/administrative/BitacoraVacaciones");


    const ReporteCertificaciones = () => import("@/views/administrative/ReporteCertificacionesColaborador");

    const ReporteCursos = () => import("@/views/administrative/ReporteCursosColaborador");
    const ReporteFormacion = () => import("@/views/administrative/ReporteFormacionColaborador");


const UnoAUno = () => import("@/views/administrative/UnoAUno/UnoAUno");

const CurriculumVitae = () => import("@/views/administrative/CargaCurriculumVitae/CurriculumVitae");

Vue.use(Router);

export default new Router({
  mode: "history", // https://router.vuejs.org/api/#mode
  linkActiveClass: "active",
  scrollBehavior: () => ({ y: 0 }),
  routes: configRoutes()
});

function configRoutes() {
  return [
    {
      path: "/",
      redirect: "/colaborador/detail/", //"/auth/login",//"/dashboard",
      name: "Inicio",
      component: TheContainer,
      children: [
        {
          path: "dashboard",
          name: "Dashboard",
          component: Dashboard,
          meta: {
            requiresAuth: true,
            permission: "AccederDashboard"
          }
        },
        {
          path: "callback",
          name: "Callback",
          component: AuthCallback
        },
        {
          path: "colaborador",
          name: "Colaboradores",
          component: Colaborador,
          meta: {
            requiresAuth: true,
            permission: "AccederColaboradores"
          }
        },
        {
          path: "colaborador/detail/",
          name: "Perfil del Colaborador",
          component: ColaboradorPerfil
        },     
        {
          path: "silent-renew",
          name: "Silent-Renew",
          component: AuthSilentRenew
        },// solicitudes masterdata
        {
          path: "masterdata",
          redirect: "/masterdata/Inbox",
          name: "Solicitudes de Cambio",
          component: {
            render(c) {
              return c("router-view");
            }
          },
          children: [
            {
              path: "masterdata",
              name: "MasterData",
              component: MasterData,
              props: true,
              meta: { requiresAuth: true }
            },
            {
              path: "Inbox",
              name: "Mi Buzón",
              component: Inbox,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "Outbox",
              name: "Tramitadas",
              component: Outbox,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "Tracking",
              name: "Seguimiento",
              component: Tracking,
              meta: {
                requiresAuth: true,
                permission: "AccederSeguimiento"
              }
            },
            {
              path: "History",
              name: "Mi historial",
              component: History,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "Consultants",
              name: "Mis consultores",
              component: Consultants,
              meta: {
                requiresAuth: true
              }
            }
          ]
        },
       // solicitudes admin
        {
          path: "administrative",
          redirect: "/administrative/Inbox",
          name: "Solicitudes administrativas",
          component: {
            render(c) {
              return c("router-view");
            }
          },
          children: [
            {
              path: "administrative",
              name: "Administrative",
              component: Administrative,
              props: true,
              meta: { requiresAuth: true }
            },
            {
              path: "Inbox",
              name: "Pendientes de atender",
              component: InboxAdmin,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "Outbox",
              name: "Mis trámites",
              component: OutboxAdmin,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "TrackingAdmin",
              name: "Seguimiento Administrativo",
              component: TrackingAdmin,
              meta: {
                requiresAuth: true,
                permission: "AccederSeguimientoAdmin"
              }
            },
            {
              path: "Consultas",
              name: "Consultas",
              component: ConsultasAdmin,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "ArchivoIncapacidad",
              name: "Carga de incapacidades",
              component: ArchivoIncapacidad,
              meta: { requiresAuth: true }
            },
            {
              path: "ArchivoSaldos",
              name: "Carga de saldos",
              component: ArchivoSaldos,
              meta: { requiresAuth: true }
            },
            {
              path: "ConsultaIncapacidad",
              name: "Consulta de incapacidad",
              component: ConsultaIncapacidad,
              meta: { requiresAuth: true }
            },
       
            {
              path: "ConsultaPermisos",
              name: "Consulta de permisos",
              component: ConsultaPermisos,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "CargaIncapacidades",
              name: "Carga de incapacidades",
              component: CargaIncapacidad,
              meta: { requiresAuth: true }
            },
            {
              path: "CargaCurriculumVitae",
              name: "Carga de CVs",
              component: CurriculumVitae,
              meta: { requiresAuth: true }
            },
            {
              path: "CargaSaldosVacaciones",
              name: "Carga de saldos de vacaciones",
              component: CargaSaldosVacaciones,
              meta: { requiresAuth: true }
            },
            {
              path: "CargaImagen",
              name: "Carga de imagenes a blob",
              component: CargaImagen,
              meta: { requiresAuth: true }
            },
            {
              
              path: "EnvioMsgPush",
              name: "Envio de Msg Push",
              component: EnvioMsgPush,
              meta: { requiresAuth: true }


            },

            {
              path: "UnoAUno",
              name: "Uno a Uno",
              component: UnoAUno,
              meta: { requiresAuth: true,
                  permission: "AccederUnoAUno"
               }
            },

            {
              path: "Redirect",
              name: "Redirect",
              component: RedirectAdmin,
              meta: {
                requiresAuth: true
              }
            }
          ]
        },
        //// Func administrativas

        {
          path: "administrativefunc",
          redirect: "/administrativefunc/Inbox",
          name: "Funciones administrativas",
          component: {
            render(c) {
              return c("router-view");
            }
          },
          children: [
            {
              path: "administrativefunc",
              name: "AdministrativeFunc",
              component: Administrative,
              props: true,
              meta: { requiresAuth: true }
            },

            {
              path: "RegistroColaborador",
              name: "Registro de colaborador",
              component: RegistroColaborador,
              meta: { requiresAuth: true }
            },

            {
              path: "CategoriaNoticia",
              name: "Categoría de Noticias",
              component: CategoriaNoticia,
              props: true,
              meta: { requiresAuth: true }
            },
            
            {
              path: "CategoriaConvenio",
              name: "Categoría de Convenios",
              component: CategoriaConvenio,
              props: true,
              meta: { requiresAuth: true }
            },

            {
              path: "Noticia",
              name: "Noticias",
              component: Noticia,
              props: true,
              meta: { requiresAuth: true }
            },

            {
              path: "Convenio",
              name: "Convenios",
              component: Convenio,
              props: true,
              meta: { requiresAuth: true }
            },
            {
              path: "Sorteos",
              name: "Sorteos",
              component: Sorteos,
              props: true,
              meta: { requiresAuth: true }
            },
            {
              path: "Actividad",
              name: "Actividades",
              component: Actividad,
              props: true,
              meta: { requiresAuth: true }
              },
              {
                  path: "ActualizacionDatosLaborales",
                  name: "Actualizacion de Datos Laborales",
                  component: ActualizacionDatosLaborales,
                  meta: { requiresAuth: true }
              },
                {   path: "BitacoraVacaciones",
                  name: "Bitácora de Vacaciones",
                  component: BitacoraVacaciones,
                  meta: { requiresAuth: true,
                          permission: "AccederBitacoraVacaciones" }
              },

              {
                path: "ReporteCertificaciones",
                name: "Reporte de Certificaciones",
                component: ReporteCertificaciones,
                props: true,
                meta: { requiresAuth: true }
              },


              {
                path: "ReporteCursos",
                name: "Reporte de Cursos",
                component: ReporteCursos,
                props: true,
                meta: { requiresAuth: true }
              },

              {
                path: "ReporteFormacion",
                name: "Reporte de Formacion",
                component: ReporteFormacion,
                props: true,
                meta: { requiresAuth: true }
              },

              
              // {
              //   path: "UnoAUno",
              //   name: "Uno a Uno",
              //   component: UnoAUno,
              //   props: true,
              //   meta: { requiresAuth: true }
              // },


            {
              path: "Redirect",
              name: "Redirect",
              component: RedirectAdmin,
              meta: {
                requiresAuth: true
              }
            }
          ]
        },

        /////Generales
        {
          path: "generales",
          redirect: "/generales/colaborador/detail",
          name: "Generales",
          component: {
            render(c) {
              return c("router-view");
            }
          },
          children: [
            {
              path: "gradosacademicos",
              name: "Grados Académicos",
              component: GradosAcademicos,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "clasificadores",
              name: "Clasificadores",
              component: ClasificadorGrid,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "clasificadores/form/:id?",
              name: "ClasificadoresForm",
              component: ClasificadorForm,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "componentes/sindatos",
              name: "SinDatos",
              component: SinDatos
            },
            {
              path: "proyectos/detalleproyecto/:id?/:proyecto?",
              name: "DetalleProyecto",
              component: DetalleProyecto,
              meta: {
                requiresAuth: true
              }
            },
            {
              path: "consulta",
              name: "Consulta",
              component: Consulta,
              meta: {
                requiresAuth: true,
              }
            },

            {
              path: "informe",
              name: "Informe",
              component: InformeIA,
              meta: {
                requiresAuth: true,
                permission: "AccederConsulta"
              }
            },

            {
              path: "cliente",
              name: "Cliente",
              component: Cliente,
              meta: { requiresAuth: true }
            },

            {
              path: "proyecto",
              name: "Proyecto",
              component: Proyecto,
              meta: { requiresAuth: true }
            },
            {
              path: "curriculum",
              name: "Curriculum",
              component: Curriculum,
              meta: { requiresAuth: true }
            },

            {
              path: "centroestudio",
              name: "Centro de Estudio",
              component: CentroEstudio,
              meta: { requiresAuth: true }
            },
            {
              path: "certificacion",
              name: "Certificación",
              component: Certificacion,
              meta: { requiresAuth: true }
            },
            {
              path: "curso",
              name: "Curso",
              component: Curso,
              meta: { requiresAuth: true }
            },
            {
              path: "divisionpolitica",
              name: "DivisionPolitica",
              component: DivisionPolitica,
              meta: { requiresAuth: true }
            },
            {
              path: "empresa",
              name: "Empresa",
              component: Empresa,
              meta: { requiresAuth: true }
            },
            {
              path: "especialidad",
              name: "Especialidad",
              component: Especialidad,
              meta: { requiresAuth: true }
            },
            {
              path: "habilidadblanda",
              name: "HabilidadBlanda",
              component: HabilidadBlanda,
              meta: { requiresAuth: true }
            },
            {
              path: "idioma",
              name: "Idioma",
              component: Idioma,
              meta: { requiresAuth: true }
            },
            {
              path: "nivelconocimiento",
              name: "NivelConocimiento",
              component: NivelConocimiento,
              meta: { requiresAuth: true }
            },
            {
              path: "pais",
              name: "Pais",
              component: Pais,
              meta: { requiresAuth: true }
            },
            {
              path: "puesto",
              name: "Puesto",
              component: Puesto,
              meta: { requiresAuth: true }
            },
            {
              path: "proveedor",
              name: "Proveedor",
              component: Proveedor,
              meta: { requiresAuth: true }
            },
            {
              path: "sector",
              name: "Sector",
              component: Sector,
              meta: { requiresAuth: true }
            },

            {
              path: "ambientedesarrollo",
              name: "AmbienteDesarrollo",
              component: AmbienteDesarrollo,
              meta: { requiresAuth: true }
            },
            {
              path: "basedatos",
              name: "BaseDatos",
              component: BaseDatos,
              meta: { requiresAuth: true }
            },
            {
              path: "bus",
              name: "Bus",
              component: Bus,
              meta: { requiresAuth: true }
            },
            {
              path: "bpm",
              name: "BPM",
              component: BPM,
              meta: { requiresAuth: true }
            },
            {
              path: "clasificacion",
              name: "Clasificacion",
              component: Clasificacion,
              meta: { requiresAuth: true }
            },
            {
              path: "erp",
              name: "ERP",
              component: ERP,
              meta: { requiresAuth: true }
            },

            {
              path: "areaorganizativa",
              name: "AreaOrganizativa",
              component: AreaOrganizativa,
              meta: { requiresAuth: true }
            },
            {
              path: "infraestructura",
              name: "Infraestructura",
              component: Infraestructura,
              meta: { requiresAuth: true }
            },
            {
              path: "lenguaje",
              name: "Lenguaje",
              component: Lenguaje,
              meta: { requiresAuth: true }
            },
            {
              path: "tecnologia",
              name: "Tecnologia",
              component: Tecnologia,
              meta: { requiresAuth: true }
            },
            {
              path: "tecnologiaqa",
              name: "TecnologiaQa",
              component: TecnologiaQa,
              meta: { requiresAuth: true }
            },
            {
              path: "tipocore",
              name: "TipoCore",
              component: TipoCore,
              meta: { requiresAuth: true }
            }
          ]
        }
      ]
    },//pages
    {
      path: "/pages",
      redirect: "/pages/404",
      name: "Pages",
      component: {
        render(c) {
          return c("router-view");
        }
      },
      children: [
        {
          path: "404",
          name: "Page404",
          component: Page404
        },
        {
          path: "500",
          name: "Page500",
          component: Page500
        },
        {
          path: "login",
          name: "Login",
          component: AuthLogin
        },
        {
          path: "register",
          name: "Register",
          component: Register
        }
      ]
    },//auth
    {
      path: "/auth",
      redirect: "/auth/login",
      name: "Auth",
      component: {
        render(c) {
          return c("router-view");
        }
      },
      children: [
        {
          path: "callback",
          name: "Callback",
          component: AuthCallback
        },
        {
          path: "login",
          name: "Login",
          component: AuthLogin
        }
      ]
    }
  ];
}
