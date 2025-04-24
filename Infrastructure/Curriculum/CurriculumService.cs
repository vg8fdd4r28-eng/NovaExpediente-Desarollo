using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using NovaExpediente.Application.Common.CV;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace NovaExpediente.Infrastructure.Curriculum
{
    public class CurriculumService : ICurriculumService
    {
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment _Environment;
        private string rutaPlantilla, rutaDocumentos;

        public CurriculumService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _Environment = environment;
        }


        // datos del JSON
        static Dictionary<string, object> datos;
        static List<Dictionary<string, object>> datosCopia;

        const string LLAVE_RAIZ = "DatosCurriculum";

        enum Plantilla
        {
            NovacompEspanol = 1,
            InnovativeEspanol = 2,
            InnovativeIngles = 3,
            ITQSEspanol = 4,
            ITQSIngles = 5,
            NovaShore = 6,
            NovaTestingEspanol = 7,
            NovaTestingIngles = 8
        };

        public Dictionary<string, object> GenerarCV(string JsonCV, int TipoCV)
        {
            Dictionary<string, object> result = new Dictionary<string, object>() { {"Resultado", 1 },
                                                                                   {"Mensaje", "" },
                                                                                   {"Archivo", null }};

            List<string> listaDocumentos = null;
            int pos = 0;

            try
            {
                if (!ValidarParametros(JsonCV, TipoCV))
                {
                    result["Resultado"] = -1;
                    result["Mensaje"] = "El conjunto de datos no puede estar vacío o el tipo de plantilla solicitada no es válida";
                    return result;
                }

                string baseAddress = Configuration.GetSection("GeneracionCV").GetSection("RutaPlantillas").Value;

                // obtiene las rutas de las plantillas y donde se almacenan los documentos
                rutaPlantilla = Path.Combine(_Environment.ContentRootPath, baseAddress, ObtenerNombreArchivoPlantilla((Plantilla)TipoCV, Configuration));

                rutaDocumentos = Path.Combine(_Environment.ContentRootPath, Configuration.GetSection("GeneracionCV").GetSection("RutaDocumentosGenerados").Value);

                // se carga el JSON
                datos = LectorJson.leer(JsonCV);

                datosCopia = ObtenerDiccionarioDeListaAll(LLAVE_RAIZ);

                listaDocumentos = new List<string>();


                foreach (var item in datosCopia)
                {
                    // obtiene los datos de un colaborador
                    datos = ObtenerDiccionarioDeLista(LLAVE_RAIZ, pos);

                    string nuevoDocumento = Path.Combine(rutaDocumentos, string.Format("CV_{0}_{1}_{2}.docx",
                                                                          datos["NombreProfesional"].ToString(),
                                                                          ((Plantilla)TipoCV).ToString(),
                                                                          DateTime.Now.ToString("ddMMyyyy")));

                    File.Copy(rutaPlantilla, nuevoDocumento, true);

                    ReplaceValues(nuevoDocumento, datos);

                    listaDocumentos.Add(nuevoDocumento);

                    pos++;
                }

            }
            catch (Exception ex)
            {
                result["Resultado"] = -1;
                result["Mensaje"] = "Error:" + ": " + ex.ToString();
            }
            finally
            {

                // si no hubo errores y se generaron los documentos, se genera ZIP con todos los documentos
                if ((int)result["Resultado"] == 1 && listaDocumentos != null && listaDocumentos.Count > 0)
                {
                    result["Mensaje"] = "Generación exitosa";
                    result["Archivo"] = EmpacarDocumentos(listaDocumentos);
                    EliminarDocumentos(listaDocumentos);
                }
            }

            return result;
        }

        private void ReplaceValues(string outputFile, Dictionary<string, object> datos)
        {

            using (WordprocessingDocument doc = WordprocessingDocument.Open(outputFile, true)) // open word document and modify it
            {

                ReplaceStringInWordDocumentHeader(doc, "«NOMBREPROFESIONAL»", datos["NombreProfesional"].ToString());
                ReplaceStringInWordDocumentHeader(doc, "«puestoProfesional»", datos["PuestoProfesional"].ToString());


                List<Dictionary<string, object>> titulos = ObtenerLista("TituloUniversitario");
                List<Dictionary<string, object>> especialidades = ObtenerLista("Especialidades");
                int contador = 0;
                string perfilProfesional = "{0} con {1} en {2}, especialista en ",
                       textoEspecialidad = "{0} con {1} años de experiencia",
                       profesion = string.Empty,
                       gradoAcademico = string.Empty;


                if (titulos != null)
                {
                    foreach (var titulo in titulos)
                    {
                        profesion = titulo["Profesion"].ToString();
                        gradoAcademico = titulo["GradoAcademico"].ToString();
                    }
                }
                perfilProfesional = string.Format(perfilProfesional, datos["PuestoProfesional"].ToString(), gradoAcademico, profesion);

                if (especialidades != null)
                {
                    foreach (var especialidad in especialidades)
                    {
                        contador++;

                        perfilProfesional += string.Format(textoEspecialidad,
                                                           especialidad["Especialidad"].ToString(),
                                                           especialidad["Experiencia"].ToString()) +
                                             (contador == especialidades.Count ? "." : (contador == especialidades.Count - 1 ? " y " : ", "));
                    }
                }

                ReplaceStringInWordDocument(doc, "«perfilProfesional»", perfilProfesional);

                List<Dictionary<string, object>> titulosAcademicos = ObtenerLista("FormacionAcademica");
                contador = 0;
                string universidad = "";
                if (titulosAcademicos != null)
                {

                    foreach (var titulo in titulosAcademicos)
                    {
                        contador++;

                        universidad += titulo["Universidad"].ToString();


                        universidad += (titulo["TituloUniversitario"].ToString() + " (" +
                                                   titulo["FechaTituloUniversitario"].ToString() + ")" +
                                                   (contador == titulosAcademicos.Count ? string.Empty : Environment.NewLine));
                    }
                }

                ReplaceStringInWordDocument(doc, "«universidad»", universidad);


                List<Dictionary<string, object>> experienciasLaborales = ObtenerLista("Proyectos");
                contador = 0;
                string experiencias = "";

                if (experienciasLaborales != null)
                {
                    foreach (var experiencia in experienciasLaborales)
                    {

                        experiencias += ValidaCampo(experiencia["Cliente"].ToString()) + Environment.NewLine;
                        experiencias += ValidaCampo(experiencia["Puesto"].ToString()) + Environment.NewLine;

                        experiencias += (experiencia.ContainsKey("FechaInicio") ? ValidaCampo(experiencia["FechaInicio"].ToString()) : " .. ") + " - " + (experiencia.ContainsKey("FechaFin") ? ValidaCampo(experiencia["FechaFin"].ToString()) : " .. ") + Environment.NewLine;

                        experiencias += ValidaCampo(experiencia["Ciudad"].ToString()) + ", " + ValidaCampo(experiencia["Pais"].ToString()) + Environment.NewLine;
                        experiencias += ValidaCampo(experiencia["ResenaLaboral"].ToString()) + Environment.NewLine;


                        if (experiencia.ContainsKey("Tecnologias"))
                        {
                            List<Dictionary<string, object>> tecnologias = (List<Dictionary<string, object>>)experiencia["Tecnologias"];


                            experiencias += "Tecnologías: ";

                            contador = 0;
                            foreach (var tecnologia in tecnologias)
                            {
                                contador++;
                                experiencias += tecnologia["Nombre"].ToString() +
                                                           (contador == tecnologias.Count ? Environment.NewLine : ", ");
                            }
                        }

                        if (experiencia.ContainsKey("Herramientas"))
                        {
                            List<Dictionary<string, object>> herramientas = (List<Dictionary<string, object>>)experiencia["Herramientas"];

                            experiencias += "Herramientas: ";
                            contador = 0;
                            foreach (var herramienta in herramientas)
                            {
                                contador++;
                                experiencias += (herramienta["Nombre"].ToString() +
                                                           (contador == herramientas.Count ? Environment.NewLine : ", "));
                            }
                        }
                    }
                }

                ReplaceStringInWordDocument(doc, "«experienciaLaboral»", experiencias);


                Dictionary<string, object> habilidadesTecnicas = ObtenerDiccionario("HabilidadesTecnicas");
                contador = 0;
                string habilidades = "";

                if (habilidadesTecnicas.ContainsKey("Tecnologias"))
                {
                    List<Dictionary<string, object>> tecnologias = (List<Dictionary<string, object>>)habilidadesTecnicas["Tecnologias"];
                    habilidades += "Tecnologías: ";

                    contador = 0;
                    foreach (var tecnologia in tecnologias)
                    {
                        contador++;
                        habilidades += (tecnologia["Nombre"].ToString() +
                                                   (contador == tecnologias.Count ? Environment.NewLine : ", "));
                    }
                }

                habilidades += Environment.NewLine;

                if (habilidadesTecnicas.ContainsKey("AmbientesDesarrollo"))
                {
                    List<Dictionary<string, object>> herramientas = (List<Dictionary<string, object>>)habilidadesTecnicas["AmbientesDesarrollo"];

                    habilidades += "Ambientes de Desarrollo: ";
                    contador = 0;
                    foreach (var herramienta in herramientas)
                    {
                        contador++;
                        habilidades += (herramienta["Nombre"].ToString() +
                                                   (contador == herramientas.Count ? Environment.NewLine : ", "));
                    }
                }

                habilidades += Environment.NewLine;

                if (habilidadesTecnicas.ContainsKey("BaseDatos"))
                {
                    List<Dictionary<string, object>> herramientas = (List<Dictionary<string, object>>)habilidadesTecnicas["BaseDatos"];

                    habilidades += "Bases de Datos: ";
                    contador = 0;
                    foreach (var herramienta in herramientas)
                    {
                        contador++;
                        habilidades += (herramienta["Nombre"].ToString() +
                                                   (contador == herramientas.Count ? Environment.NewLine : ", "));
                    }
                }

                habilidades += Environment.NewLine;

                if (habilidadesTecnicas.ContainsKey("LenguajesProgramacion"))
                {
                    List<Dictionary<string, object>> herramientas = (List<Dictionary<string, object>>)habilidadesTecnicas["LenguajesProgramacion"];

                    habilidades += "Lenguajes de Programación: ";
                    contador = 0;
                    foreach (var herramienta in herramientas)
                    {
                        contador++;
                        habilidades += (herramienta["Nombre"].ToString() +
                                                   (contador == herramientas.Count ? Environment.NewLine : ", "));
                    }
                }

                habilidades += Environment.NewLine;

                if (habilidadesTecnicas.ContainsKey("Infraestructura"))
                {
                    List<Dictionary<string, object>> herramientas = (List<Dictionary<string, object>>)habilidadesTecnicas["Infraestructura"];

                    habilidades += "Infraestructura: ";

                    contador = 0;
                    foreach (var herramienta in herramientas)
                    {
                        contador++;
                        habilidades += (herramienta["Nombre"].ToString() +
                                                   (contador == herramientas.Count ? Environment.NewLine : ", "));
                    }
                }

                habilidades += Environment.NewLine;

                if (habilidadesTecnicas.ContainsKey("ERPS"))
                {
                    List<Dictionary<string, object>> erps = (List<Dictionary<string, object>>)habilidadesTecnicas["ERPS"];

                    habilidades += "ERPs y Core: ";
                    contador = 0;
                    foreach (var erp in erps)
                    {
                        contador++;
                        habilidades += (erp["Nombre"].ToString() +
                                                   (contador == erps.Count ? Environment.NewLine : ", "));
                    }
                }
                habilidades += Environment.NewLine;


                if (habilidadesTecnicas.ContainsKey("Calidad"))
                {
                    List<Dictionary<string, object>> herramientas = (List<Dictionary<string, object>>)habilidadesTecnicas["Calidad"];

                    habilidades += "Herramientas de Calidad: ";
                    contador = 0;
                    foreach (var herramienta in herramientas)
                    {
                        contador++;
                        habilidades += (herramienta["Nombre"].ToString() +
                                                   (contador == herramientas.Count ? Environment.NewLine : ", "));
                    }
                }

                habilidades += Environment.NewLine;

                if (habilidadesTecnicas.ContainsKey("Bus"))
                {
                    List<Dictionary<string, object>> buses = (List<Dictionary<string, object>>)habilidadesTecnicas["Bus"];

                    habilidades += "Buses y SOA: ";
                    contador = 0;
                    foreach (var bus in buses)
                    {
                        contador++;
                        habilidades += (bus["Nombre"].ToString() +
                                                   (contador == buses.Count ? Environment.NewLine : ", "));
                    }
                }
                habilidades += Environment.NewLine;


                if (habilidadesTecnicas.ContainsKey("BpmOfimatica"))
                {
                    List<Dictionary<string, object>> herramientas = (List<Dictionary<string, object>>)habilidadesTecnicas["BpmOfimatica"];

                    habilidades += "Business Process Management y Ofimática: ";

                    contador = 0;
                    foreach (var herramienta in herramientas)
                    {
                        contador++;
                        habilidades += (herramienta["Nombre"].ToString() +
                                                   (contador == herramientas.Count ? Environment.NewLine : ", "));
                    }
                }

                habilidades += Environment.NewLine;


                ReplaceStringInWordDocument(doc, "«habilidadesTecnicas»", habilidades);



                List<Dictionary<string, object>> idiomas = ObtenerLista("Idiomas");
                contador = 0;
                string idiomasColaborador = "";
                if (idiomas != null)
                {
                    foreach (var idioma in idiomas)
                    {
                        contador++;
                        //idiomasColaborador += (idioma["Nombre"].ToString() + (contador == idiomas.Count ? string.Empty : Environment.NewLine));
                        idiomasColaborador += (idioma["Nombre"].ToString() + (Environment.NewLine));
                    }
                }

                ReplaceStringInWordDocument(doc, "«idioma»", idiomasColaborador);

                List<Dictionary<string, object>> cursos = ObtenerLista("Cursos");
                contador = 0;
                string cursosColaborador = "";

                if (cursos != null)
                {
                    foreach (var curso in cursos)
                    {
                        contador++;
                        //cursosColaborador += (curso["Nombre"].ToString() + (contador == cursos.Count ? string.Empty : Environment.NewLine));
                        cursosColaborador += (curso["Nombre"].ToString() + (Environment.NewLine));
                    }
                }

                ReplaceStringInWordDocument(doc, "«curso»", cursosColaborador);



                List<Dictionary<string, object>> certificaciones = ObtenerLista("Certificaciones");
                contador = 0;
                string certificacionesColaborador = "";

                if (certificaciones != null)
                {
                    foreach (var certificacion in certificaciones)
                    {
                        contador++;
                        //certificacionesColaborador += (certificacion["Nombre"].ToString() + (contador == certificaciones.Count ? string.Empty : Environment.NewLine));
                        certificacionesColaborador += (certificacion["Nombre"].ToString() + (Environment.NewLine));
                    }




                    ReplaceStringInWordDocument(doc, "«certificacion»", certificacionesColaborador);

                }
            }
        }


        public WordprocessingDocument ReplaceStringInWordDocument(WordprocessingDocument wordprocessingDocument, string replaceWhat, string replaceFor)
        {
            List<WordMatchedPhrase> matchedPhrases = FindWordMatchedPhrases(wordprocessingDocument, replaceWhat);

            Document document = wordprocessingDocument.MainDocumentPart.Document;
            int i = 0;
            bool isInPhrase = false;
            bool isInEndOfPhrase = false;
            foreach (Text text in document.Descendants<Text>()) // <<< Here
            {
                char[] textChars = text.Text.ToCharArray();
                List<WordMatchedPhrase> curParPhrases = matchedPhrases.FindAll(a => (a.firstCharParOccurance.Equals(i) || a.lastCharParOccurance.Equals(i)));
                StringBuilder outStringBuilder = new StringBuilder();

                for (int c = 0; c < textChars.Length; c++)
                {
                    if (isInEndOfPhrase)
                    {
                        isInPhrase = false;
                        isInEndOfPhrase = false;
                    }

                    foreach (var parPhrase in curParPhrases)
                    {
                        if (c == parPhrase.charStartInFirstPar && i == parPhrase.firstCharParOccurance)
                        {
                            outStringBuilder.Append(replaceFor);
                            isInPhrase = true;
                        }
                        if (c == parPhrase.charEndInLastPar && i == parPhrase.lastCharParOccurance)
                        {
                            isInEndOfPhrase = true;
                        }

                    }
                    if (isInPhrase == false && isInEndOfPhrase == false)
                    {
                        outStringBuilder.Append(textChars[c]);
                    }
                }
                text.Text = outStringBuilder.ToString();
               // text.Append(new Break());
                i = i + 1;
            }

            return wordprocessingDocument;
        }

        private List<WordMatchedPhrase> FindWordMatchedPhrases(WordprocessingDocument wordprocessingDocument, string replaceWhat)
        {
            char[] replaceWhatChars = replaceWhat.ToCharArray();
            int overlapsRequired = replaceWhatChars.Length;
            int currentChar = 0;
            int firstCharParOccurance = 0;
            int lastCharParOccurance = 0;
            int startChar = 0;
            int endChar = 0;
            List<WordMatchedPhrase> wordMatchedPhrases = new List<WordMatchedPhrase>();
            Document document = wordprocessingDocument.MainDocumentPart.Document;
            int i = 0;
            foreach (Text text in document.Descendants<Text>())
            {
                char[] textChars = text.Text.ToCharArray();
                for (int c = 0; c < textChars.Length; c++)
                {
                    char compareToChar = replaceWhatChars[currentChar];
                    if (textChars[c] == compareToChar)
                    {
                        currentChar = currentChar + 1;
                        if (currentChar == 1)
                        {
                            startChar = c;
                            firstCharParOccurance = i;
                        }
                        if (currentChar == overlapsRequired)
                        {
                            endChar = c;
                            lastCharParOccurance = i;
                            WordMatchedPhrase matchedPhrase = new WordMatchedPhrase
                            {
                                firstCharParOccurance = firstCharParOccurance,
                                lastCharParOccurance = lastCharParOccurance,
                                charEndInLastPar = endChar,
                                charStartInFirstPar = startChar
                            };
                            wordMatchedPhrases.Add(matchedPhrase);
                            currentChar = 0;
                        }
                    }
                    else
                    {
                        currentChar = 0;

                    }
                }
                i = i + 1;
            }

            //same to header

            //same to footer


            return wordMatchedPhrases;

        }

        public WordprocessingDocument ReplaceStringInWordDocumentHeader(WordprocessingDocument wordprocessingDocument, string replaceWhat, string replaceFor)
        {

            Document document = wordprocessingDocument.MainDocumentPart.Document;

            foreach (var headerPart in document.MainDocumentPart.HeaderParts)
            {
                //Gets the text in headers
                foreach (var currentText in headerPart.RootElement.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>())
                {
                    currentText.Text = currentText.Text.Replace(replaceWhat, replaceFor);
                }
            }


            return wordprocessingDocument;
        }


        public WordprocessingDocument ReplaceDataInWordDocument(WordprocessingDocument wordprocessingDocument, string replaceWhat, string replaceFor)
        {

            Document document = wordprocessingDocument.MainDocumentPart.Document;

            foreach (var part in document.MainDocumentPart.Parts)
            {
                //Gets the text in headers

                //foreach (var currentText in headerPart.RootElement.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>())
                //{
                //    currentText.Text = currentText.Text.Replace(replaceWhat, replaceFor);
                //}
            }


            return wordprocessingDocument;
        }


        public static void CreateAndAddParagraphStyle(WordprocessingDocument wordprocessingDocument, string replaceWhat, string replaceFor,
            StyleDefinitionsPart styleDefinitionsPart,
            string styleid, string stylename, string aliases = "")
        {

            // Get the Styles part for this document.
            StyleDefinitionsPart part =
                wordprocessingDocument.MainDocumentPart.StyleDefinitionsPart;

            // If the Styles part does not exist, add it and then add the style.
            if (part == null)
            {
                part = AddStylesPartToPackage(wordprocessingDocument);
            }

            // Set up a variable to hold the style ID.
            string parastyleid = "OverdueAmountPara";

            // Create and add a paragraph style to the specified styles part 
            // with the specified style ID, style name and aliases.
            CreateAndAddParagraphStyle(wordprocessingDocument, replaceWhat, replaceFor,
                part,
                parastyleid,
                "Overdue Amount Para",
                "Late Due, Late Amount");

            // Add a paragraph with a run and some text.
            Paragraph p =
                new Paragraph(
                    new Run(
                        new Text("This is some text in a run in a paragraph.")));

            // Add the paragraph as a child element of the w:body element.
            wordprocessingDocument.MainDocumentPart.Document.Body.AppendChild(p);

            //Add new line in a document
            wordprocessingDocument.MainDocumentPart.Document.Body.AppendChild(new Break());


            // If the paragraph has no ParagraphProperties object, create one.
            if (p.Elements<ParagraphProperties>().Count() == 0)
            {
                p.PrependChild<ParagraphProperties>(new ParagraphProperties());
            }

            // Get a reference to the ParagraphProperties object.
            ParagraphProperties pPr = p.ParagraphProperties;

            // If a ParagraphStyleId object doesn't exist, create one.
            if (pPr.ParagraphStyleId == null)
                pPr.ParagraphStyleId = new ParagraphStyleId();

            // Set the style of the paragraph.
            pPr.ParagraphStyleId.Val = parastyleid;


        }

        // Add a StylesDefinitionsPart to the document.  Returns a reference to it.
        public static StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument doc)
        {
            StyleDefinitionsPart part;
            part = doc.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            Styles root = new Styles();
            root.Save(part);
            return part;
        }


        private void EliminarDocumentos(List<string> listaDocumentos)
        {
            foreach (var archivo in listaDocumentos)
            {
                File.Delete(archivo);
            }
        }

        private byte[] EmpacarDocumentos(List<string> listaDocumentos)
        {
            string nombreArchivoEmpacado = Path.Combine(rutaDocumentos, string.Format("{0}.zip", Guid.NewGuid().ToString()));

            using (var repositorioMemoria = new MemoryStream())
            {
                using (var archivoZip = new ZipArchive(repositorioMemoria, ZipArchiveMode.Create, true))
                {
                    foreach (var archivo in listaDocumentos)
                    {
                        var nombreArchivo = Path.GetFileName(archivo);
                        var entradaArchivoZip = archivoZip.CreateEntry(nombreArchivo, CompressionLevel.Optimal);

                        using (var entryStream = entradaArchivoZip.Open())
                        {
                            using (var fileToCompressStream = new MemoryStream(File.ReadAllBytes(archivo)))
                            {
                                fileToCompressStream.CopyTo(entryStream);
                            }
                        }
                    }
                }

                using (var fileStream = new FileStream(nombreArchivoEmpacado, FileMode.Create))
                {
                    repositorioMemoria.Position = 0;
                    repositorioMemoria.WriteTo(fileStream);
                }

            }

            var contenidoArchivoEmpacado = File.ReadAllBytes(nombreArchivoEmpacado);

            File.Delete(nombreArchivoEmpacado);

            return contenidoArchivoEmpacado;
        }

        private bool ValidarParametros(string JsonCV, int TipoCV)
        {
            if (JsonCV.Trim().Equals(string.Empty))
            {
                return false;
            }
            // se valida que la plantilla solicitada sea válida
            if (!Enum.IsDefined(typeof(Plantilla), TipoCV))
            {
                return false;
            }

            return true;
        }

        private static string ObtenerNombreArchivoPlantilla(Plantilla tipoPlantilla, IConfiguration Configuration)
        {
            string nombreArchivo = string.Empty;
            switch (tipoPlantilla)
            {
                case Plantilla.NovacompEspanol:
                    nombreArchivo = Configuration.GetSection("GeneracionCV").GetSection("Plantilla-Novacomp-Espanol").Value;
                    break;
                case Plantilla.InnovativeEspanol:
                    nombreArchivo = Configuration.GetSection("GeneracionCV").GetSection("Plantilla-Innovative-Espanol").Value;
                    break;
                case Plantilla.InnovativeIngles:
                    nombreArchivo = Configuration.GetSection("GeneracionCV").GetSection("Plantilla-Innovative-Ingles").Value;
                    break;
                case Plantilla.ITQSEspanol:
                    nombreArchivo = Configuration.GetSection("GeneracionCV").GetSection("Plantilla-ITQS-Espanol").Value;
                    break;
                case Plantilla.ITQSIngles:
                    nombreArchivo = Configuration.GetSection("GeneracionCV").GetSection("Plantilla-ITQS-Ingles").Value;
                    break;
                case Plantilla.NovaShore:
                    nombreArchivo = Configuration.GetSection("GeneracionCV").GetSection("Plantilla-NovaShore").Value;
                    break;
                case Plantilla.NovaTestingEspanol:
                    nombreArchivo = Configuration.GetSection("GeneracionCV").GetSection("Plantilla-NovaTesting-Espanol").Value;
                    break;
                case Plantilla.NovaTestingIngles:
                    nombreArchivo = Configuration.GetSection("GeneracionCV").GetSection("Plantilla-NovaTesting-Ingles").Value;
                    break;
                default:
                    break;
            }
            return nombreArchivo;
        }

        private static Dictionary<string, object> ObtenerDiccionarioDeLista(string key, int pos)
        {
            List<Dictionary<string, object>> l = (List<Dictionary<string, object>>)datosCopia;
            return (Dictionary<string, object>)l[pos];
        }

        private static List<Dictionary<string, object>> ObtenerDiccionarioDeListaAll(string key)
        {
            List<Dictionary<string, object>> l = (List<Dictionary<string, object>>)datos[key];
            return l;
        }

        private static List<Dictionary<string, object>> ObtenerLista(string key)
        {
            if (datos.ContainsKey(key))
            {
                return (List<Dictionary<string, object>>)datos[key];

            }
            else
            {
                return (List<Dictionary<string, object>>)null;

            }

        }

        private static Dictionary<string, object> ObtenerDiccionario(string key)
        {
            return (Dictionary<string, object>)datos[key];
        }

        private string ValidaCampo(string campo)
        {

            string resultado = "N/A";

            if (campo != null)
            {
                resultado = campo;
            }
            if (campo != "")
            {
                resultado = campo;
            }

            return resultado;

        }

        private class WordMatchedPhrase
        {
            public int charStartInFirstPar { get; set; }
            public int charEndInLastPar { get; set; }

            public int firstCharParOccurance { get; set; }
            public int lastCharParOccurance { get; set; }
        }

    }
}
