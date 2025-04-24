export default [
  {
    _name: "CSidebarNav",
    _children: [
      {
        _name: "CSidebarNavItem",
        name: "Mi perfil",
        to: "/colaborador/detail/",
        icon: "perfil"
      },
      {
        _name: 'CSidebarNavDropdown',
        name: 'Inteligencia Artificial',
        icon: 'brainAIIcon',
        permission: 'AccederConsulta',
        items: [
          {
            name: 'Consulta',
            to: '/Generales/consulta',
            icon: 'consultas',
          },
          {
            name: 'Informe',
            to: '/Generales/informe',
            icon: 'seguimiento',
          },
          {
            name: 'Carga de CVs',
            to: '/Administrative/CargaCurriculumVitae',
            icon: 'generarCurriculumns',
          },
        ],
      },         
      {
        _name: "CSidebarNavItem",
        name: "DashBoard",
        to: "/dashboard",
        icon: "dashboard",//"cil-speedometer",
        badge: {
          color: "info",
          text: "Estadísticas"
        },
        permission: "AccederDashboard"
      },         
      {
        _name: "CSidebarNavItem",
        name: "Colaboradores",
        to: "/colaborador",
        icon: "colaboradores",
        permission: "AccederColaboradores"
      },      
      {
        _name: "CSidebarNavTitle",
        _children: ["Solicitud de cambios"],
      },
      {
        _name: "CSidebarNavItem",
        name: "Mi buzón",
        to: "/masterdata/Inbox",
        icon: "miBuzon",
        badge: {
          color: "warning",
          text: "0",
          id:"inboxCounterId"
        }
      },
      {
        _name: "CSidebarNavItem",
        name: "Tramitadas",
        to: "/MasterData/Outbox",
        icon: "tramitadas"
      },
      {
        _name: "CSidebarNavItem",
        name: "Seguimiento",
        to: "/MasterData/Tracking",
        icon: "seguimiento",
        permission: "AccederSeguimiento"
      },
      {
        _name: "CSidebarNavItem",
        name: "Mi historial",
        to: "/MasterData/History",
        icon: "historial",
      },
      {
        _name: "CSidebarNavItem",
        name: "Mis consultores",
        to: "/MasterData/Consultants",
        icon: "consultores",
        permission: "AccederMisConsultores"
      },      
      {
        _name: "CSidebarNavTitle",
        _children: ["Solicitudes administrativas"],
      },
      {
        _name: "CSidebarNavItem",
        name: "Mis trámites",
        to: "/Administrative/Outbox",
        icon: "misTramites"
        //permission:"AccederMinimoPermiso"
      },
      {
        _name: "CSidebarNavItem",
        name: "Pendientes de atender",
        to: "/Administrative/Inbox",
        icon: "miBuzon",
       //permission:"AccederMinimoPermiso",
        badge: {
          color: "warning",
          text: "0",
          id:"inboxAdminCounterId"
        }
      },
      {
        _name: "CSidebarNavItem",
        name: "Seguimiento administrativo",
        to: "/Administrative/TrackingAdmin",
        icon: "seguimiento",
        permission: "AccederSeguimiento"
      },
      {
        _name: "CSidebarNavItem",
        name: "Consultas",
        to: "/Administrative/Consultas",
        icon: "consultasAdmin"
        //permission:"AccederMinimoPermiso"
      },
      // {
      //   _name: "CSidebarNavItem",
      //   name: "Consulta de incapacidades",
      //   to: "/Administrative/ConsultaIncapacidad",
      //   icon: "seguimiento",
      // },
      // {
      //   _name: "CSidebarNavItem",
      //   name: "Consulta de permisos",
      //   to: "/Administrative/ConsultaPermisos",
      //   icon: "seguimiento",
      // },
      // {
      //   _name: "CSidebarNavItem",
      //   name: "Carga de incapacidades",
      //   to: "/Administrative/CargaIncapacidades",
      //   icon: "seguimiento",
      // },
      // {
      //  _name: "CSidebarNavItem",
      //  name: "Carga de vacaciones",
      //  to: "/Administrative/CargaSaldoVacaciones",
      //  icon: "seguimiento",
      //},
      {
        _name: "CSidebarNavItem",
        name: "Carga de incapacidades",
        to: "/Administrative/CargaIncapacidades",
        icon: "cargaIncapacidades",
        permission: "AccederCargaIncapacidades"
      },
      {
        _name: "CSidebarNavItem",
        name: "Carga de saldos de vacaciones",
        to: "/Administrative/CargaSaldosVacaciones",
        icon: "cargaIncapacidades",
        permission: "AccederCargaSaldos"
      },

      {
        _name: "CSidebarNavItem",
        name: "Uno a Uno",
        to: "/Administrative/UnoAUno",
        icon: "colaboradores",
        permission: "AccederUnoAUno"
      },
            // {
      //   _name: "CSidebarNavDropdown",
      //   name: "Uno a Uno",
      //   /* route: "/app", */
      //   icon: "colaboradores",
      //  // permission: "AccederUnoAUno",
      //   items: [
  
      //     {
      //       name: "Registro Uno A Uno",
      //       to: "/AdministrativeFunc/UnoAUno",
      //     },

        
      //   ],
      // },

      //{
      //  _name: "CSidebarNavItem",
      //  name: "Carga de imagenes a blob",
      //  to: "/Administrative/CargaImagen",
      //  icon: "cargaIncapacidades",
      //  // permission: "AccederCargaImagen"
      //},

       //   {
       //  _name: "CSidebarNavItem",
       //  name: "Envio de Msg Push",
       //  to: "/Administrative/EnvioMsgPush",
       //  icon: "cargaIncapacidades",
       //  permission: "AccederEnvioMsgPush"
       //},

      {
        _name: "CSidebarNavTitle",
        _children: ["Consulta de Catálogos"],
        permission: "AccederConfiguracionSistema",
      },
      {
        _name: "CSidebarNavDropdown",
        name: "Generales",
        route: "/Configuracion",
        icon: "configuracion",
        permission: "AccederConfiguracionSistema",
        items: [
          // {
          //   name: "Buzón de Solicitudes",
          //   to: "/generales/masterdata",
          // },
          {
            name: "Áreas organizativas",
            to: "/generales/areaorganizativa",
          },
          {
            name: "Centros de estudio",
            to: "/generales/centroestudio",
          },
          {
            name: "Certificaciones",
            to: "/generales/certificacion",
          },
          // {
          //   name: "Clasificadores",
          //   to: "/Generales/clasificadores",
          // },
          // {
          //   name: "Cursos",
          //   to: "/Generales/curso",
          // },
          {

        //    _name: "CSidebarNavItem",
            name: "Clientes",
            to: "/Generales/cliente",
        //    icon: "clientes",
            permission: "AccederClientes"
          },
          {
            name: "División política",
            to: "/Generales/divisionpolitica",
          },
          {
            name: "Empresas de la corporación",
            to: "/Generales/empresa",

          },
          {
            name: "Especialidades",
            to: "/Generales/especialidad",

          },
          {
            name: "Grados académicos",
            to: "/Generales/gradosacademicos",
            //to: "/Generales/curso",

          },
          {
            name: "Habilidades Blandas",
            to: "/Generales/habilidadblanda",

          },
          {
            name: "Idiomas",
            to: "/Generales/idioma",

          },
          {
            name: "Nivel de idioma",
            to: "/Generales/nivelconocimiento",

          },
          {
            name: "País",
            to: "/Generales/pais",

          },
          {
            name: "Puestos",
            to: "/Generales/puesto",

          },
          {
            name: "Proveedores de certificaciones",
            to: "/Generales/proveedor",

          },
          {
            name: "Sectores",
            to: "/Generales/sector",

          },
        ],
      },
      {
        _name: "CSidebarNavDropdown",
        name: "Tecnología",
        route: "/Tecnologia",
        icon: "tecnologia",
        permission: "AccederConfiguracionSistema",
        items: [
          {
            name: "Ambientes de desarrollo",
            to: "/Generales/ambientedesarrollo",
          },
          {
            name: "Bases de datos",
            to: "/Generales/basedatos",

          },
          {
            name: "BPM y herramientas de ofimática",
            to: "/Generales/bpm",

          },
          {
            name: "Buses y SOA",
            to: "/Generales/bus",

          },
         
          // {
          //   name: "Clasificaciones",
          //   to: "/Generales/clasificacion",

          // },
          {
            name: "ERPs y Core",
            to: "/Generales/erp",

          },
        
          {
            name: "Infraestructura",
            to: "/Generales/infraestructura",

          },
          {
            name: "Lenguajes de programación",
            to: "/Generales/lenguaje",

          },
          {
            name: "Tecnologías",
            to: "/Generales/tecnologia",

          },
          {
            name: "Tecnologías de calidad",
            to: "/Generales/tecnologiaqa",

          },
          {
            name: "Tipos de Core",
            to: "/Generales/tipocore",

          },
        
        ],
      },
      {
        _name: "CSidebarNavTitle",
        _children: ["Funciones Administrativas"],
        permission: "AccederFuncionesAdmin",
      },
      {

        _name: "CSidebarNavItem",
        name: "Registrar colaborador",
        to: "/AdministrativeFunc/RegistroColaborador",
        icon: "colaboradores",
        permission: "AccederFuncionesAdmin"
        },
        {

            _name: "CSidebarNavItem",
            name: "Actualizacion de Datos Laborales",
            to: "/AdministrativeFunc/ActualizacionDatosLaborales",
            icon: "colaboradores",
            permission: "AccederFuncionesAdmin"
        },
        {

          _name: "CSidebarNavItem",
          name: "Bitácora de Vacaciones",
          to: "/AdministrativeFunc/BitacoraVacaciones",
          icon: "tramitadas",
          permission: "AccederBitacoraVacaciones"
      },
      // {
      //   _name: "CSidebarNavDropdown",
      //   name: "Uno a Uno",
      //   /* route: "/app", */
      //   icon: "colaboradores",
      //  // permission: "AccederUnoAUno",
      //   items: [
  
      //     {
      //       name: "Registro Uno A Uno",
      //       to: "/AdministrativeFunc/UnoAUno",
      //     },

        
      //   ],
      // },
      {
        _name: "CSidebarNavDropdown",
        name: "Aplicación Móvil",
        /* route: "/app", */
        icon: "tecnologia",
        permission: "AccederConfiguracionAPP",
        items: [
          {
            name: "Categoría de noticias",
            to: "/AdministrativeFunc/CategoriaNoticia",
          },
          {
            name: "Categoría de convenios",
            to: "/AdministrativeFunc/CategoriaConvenio",
          },
          {
            name: "Noticias",
            to: "/AdministrativeFunc/Noticia",
          },
          {
            name: "Convenios",
            to: "/AdministrativeFunc/Convenio",
          },
          {
            name: "Sorteos",
            to: "/AdministrativeFunc/Sorteos"
          },
          {
            name: "Actividades",
            to: "/AdministrativeFunc/Actividad",
            },
                {
                //_name: "CSidebarNavItem",
                name: "Envio de Msg Push",
                to: "/Administrative/EnvioMsgPush",
                //icon: "cargaIncapacidades",
                //permission: "AccederEnvioMsgPush"
            }

        
        ],
      },
      {
        _name: "CSidebarNavDropdown",
        name: "Reportes Learning",
        /* route: "/app", */
        icon: "tramitadas",
        permission: "AccederReportes",
        items: [
          {
            name: "Reporte de Certificaciones",
            to: "/AdministrativeFunc/ReporteCertificaciones",
          },
          {
            name: "Reporte de Cursos",
            to: "/AdministrativeFunc/ReporteCursos",
          },
          {
            name: "Reporte de Formación",
            to: "/AdministrativeFunc/ReporteFormacion",
          }
        ],
      },

    ],
  },
];

