export const Axios = {
  getCantMes: () =>
    Promise.resolve([
      { value: "6", label: "6", key: 1 },
      { value: "12", label: "12", key: 2 },
      { value: "18", label: "18", key: 3 },
      { value: "24", label: "24", key: 4 },
      { value: "30", label: "30", key: 5 },
      { value: "36", label: "36", key: 6 },
      { value: "42", label: "42", key: 7 },
      { value: "+48", label: "+48", key: 8 }
    ]),
  getGrados: () =>
    Promise.resolve([
      { value: "Técnico Medio", label: "Técnico Medio", key: 1 },
      { value: "Diplomado", label: "Diplomado", key: 1 },
      { value: "Bachiller", label: "Bachiller", key: 1 },
      { value: "Licenciatura", label: "Licenciatura", key: 1 },
      { value: "Maestría", label: "Maestría", key: 1 },
      { value: "Doctorado", label: "Doctorado", key: 1 }
    ]),
  getEspecialidad: () =>
    Promise.resolve([
      { value: ".Net", label: ".Net", key: 1 },
      { value: ".NET / Xamarin", label: ".NET / Xamarin", key: 1 },
      { value: "Android", label: "Android", key: 1 },
      { value: "Arquitecto", label: "Arquitecto", key: 1 },
      { value: "AS400", label: "AS400", key: 1 },
      { value: "AS400 / Oracle", label: "AS400 / Oracle", key: 1 },
      { value: "BI", label: "BI", key: 1 },
      { value: "Big Data", label: "Big Data", key: 1 },
      { value: "COBOL", label: "COBOL", key: 1 },
      { value: "FOX", label: "FOX", key: 1 },
      { value: "Infraestructura", label: "Infraestructura", key: 1 },
      { value: "Ing. Industrial.", label: "Ing. Industrial.", key: 1 },
      { value: "Ionic ", label: "Ionic", key: 1 },
      { value: "iOS", label: "iOS", key: 1 },
      { value: "Java", label: "Java", key: 1 },
      { value: "Java / RPG", label: "Java / RPG", key: 1 },
      { value: "Microsof Azure", label: "Microsof Azure", key: 1 },
      { value: "Mobile", label: "Mobile", key: 1 },
      { value: "móvil híbrido", label: "móvil híbrido", key: 1 },
      { value: "Oracle", label: "Oracle", key: 1 },
      { value: "Oracle / .Net", label: "Oracle / .Net", key: 1 },
      { value: "PHP", label: "PHP", key: 1 },
      { value: "QA", label: "QA", key: 1 },
      { value: "React", label: "React", key: 1 },
      { value: "Scrum Master", label: "Scrum Master", key: 1 },
      { value: "SharePoint", label: "SharePoint", key: 1 },
      { value: "Transactor Unisys", label: "Transactor Unisys", key: 1 }
    ]),

  getCertificacion: () =>
    Promise.resolve([
      "IBM CERTIFIED ISERIES DB2/UDB SOFTWARE DEVELOP.",
      "ISTQB ADVANCED LEVEL-TECHNICAL DEVELOP.",
      "MCPD: MICROSOFT CERTIFIED PROFESSIONAL DEV.",
      "OTRAS: Certificación VWare VSP5, HP Accredited Sales Professional 2011, HP-Sales Certified Personal Systems 2013, HP Sales Certified - Workstations 2013",
      "ORACLE SERVICE ORIENTED ARQUITECTURE INFRAESRUCTURE IMPLEMENTATION CERTIFIED EXPERT"
    ]),

  getIdioma: () => Promise.resolve(["Ingles", "Frances", "Mandarin"]),

  getAmbienteDesarrollo: () =>
    Promise.resolve([
      "MICROSFT VISUAL STUDIO .NET CORE",
      "ORACEL DEVELOPER SUITE",
      "SQLDeveloper",
      "VISUAL STUDIO CODE"
    ]),

  getERP: () => Promise.resolve(["ERP 1", "ERP 2", "ERP 3"]),

  getCalidad: () => Promise.resolve(["Calidad 1", "Calidad 2", "Calidad 3"]),

  getTecnologia: () =>
    Promise.resolve([
      "Android",
      "ASP.NET",
      "Bootstrap",
      "COBIT",
      "CSS",
      "CSS3",
      "Eclipse. Transact SQL",
      "Glassfish Server",
      "HTML (JSP y JSF)",
      "HTML",
      "HTML5 y CSS3",
      "HTML5",
      "IBM Rational Application Developer",
      "IntelliJDEA",
      "Jquery (Javascript)",
      "NAF de CODISA",
      "NetBeans Eclipse",
      "NetBeans",
      "OAD",
      "Oracle Web logic Server",
      "Restful Services",
      "SQL Navigator y SQL Developer",
      "UN Virtual Box",
      "Visual Studio",
      "VMWare.",
      "Web Logic Server",
      "WebSphere Application Server V7 (IBM)"
    ]),
  getBaseDatos: () =>
    Promise.resolve([
      "MySQL",
      "MongoDB",
      "SQL Server",
      "Oracle",
      "Informix",
      "DB2"
    ]),
  getLenguajes: () =>
    Promise.resolve([
      "PL/SQL",
      "Java",
      "Java – POO",
      "PHP",
      "JavaScript",
      "C#",
      "RPG",
      "C",
      "C++",
      "RPG ILE",
      "Microsoft Visual Studio .Net (C#)",
      "VB.NET",
      "Microsoft Visual C#.NET",
      "Oracle Forms 10g y Oracle Reports 10g",
      "PL/SQL",
      "Visual Basic y .Net."
    ]),

  getInfraestructura: () =>
    Promise.resolve([
      "Infraestructura 1",
      "Infraestructura 2",
      "Infraestructura 3"
    ]),
  getBus: () => Promise.resolve(["Bus 1", "Bus 2", "Bus 3"]),
  getBPM: () =>
    Promise.resolve([
      "POWER AUTOMATE",
      "AURA PORTAL BPM",
      "BIZAGI MODELER",
      "GENEXUS BPM",
      "IBM LOMBARDI BPM",
      "MICROSOFT FLOW FORMA BPM",
      "NINTEX FLOW",
      "ORACLE BPM SUITE"
    ])
};
