using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;

namespace Generador_Word
{
    public class GenerarCartaResponsiva
    {
        // Cambiar path dependiendo del servidor:
        private const string PATH_LOGO_PM = @"C:\Users\jatapia\Documents\INV_PMS\Generador_Word\assets\pm.jpg";
        private readonly Document _doc = new();
        private int _currentPage = 0;
        private Image _image = Image.FromFile(PATH_LOGO_PM);
        private string _respuesta = "";

        public Byte[]? CartaResponsivaEquipoComputo(CartaResponsiva cartaResponsiva)
        {
            Section section = _doc.AddSection();
            SetDocumentHeader(section);
            Pag1(section, cartaResponsiva);
            var returnStream = new MemoryStream();

            //_doc.SaveToFile("CartaResponsiva.docx", FileFormat.Docx2013);

            _doc.SaveToFile(returnStream, FileFormat.Docx2013);
            byte[] arr = returnStream.ToArray();
            returnStream.Flush();
            returnStream.Close();

            return _respuesta! == "Word generado exitosamente" ? arr : null;
        }

        private void SetDocumentHeader(Section section)
        {
            Paragraph paragraph = section.AddParagraph();

            _doc.Sections[0].PageSetup.DifferentFirstPageHeaderFooter = true;

            // Agregar tabla:
            Table table = section.AddTable(true);
            table.ResetCells(1, 3);
            TableCell cell1 = table.Rows[0].Cells[2];
            cell1.SplitCell(0, 2);

            TableCell cell_logo_pm = table[0, 0];
            cell_logo_pm.Width = 20;
            cell_logo_pm.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            paragraph = cell_logo_pm.AddParagraph();
            paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
            paragraph.AppendPicture(_image);

            TableCell cell_desc_doc = table[0, 1];
            cell_desc_doc.Width = 370;
            cell_desc_doc.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            paragraph = cell_desc_doc.AddParagraph();
            paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange text_desc_doc = paragraph.AppendText("CARTA RESPONSIVA DE EQUIPO DE CÓMPUTO");
            text_desc_doc.CharacterFormat.Bold = true;

            TableCell cell_desc_codigo = table[0, 2];
            TableRow row_desc_codigo = table.Rows[0];
            row_desc_codigo.Height = 30;
            cell_desc_codigo.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            paragraph = cell_desc_codigo.AddParagraph();
            paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange text_desc_codigo = paragraph.AppendText("Código");
            text_desc_codigo.CharacterFormat.Bold = true;
            paragraph.AppendText(": RE-ASS-STI-1");

            TableCell cell_desc_pagina = table[1, 2];
            TableRow row_desc_pagina = table.Rows[1];
            row_desc_pagina.Height = 30;
            cell_desc_pagina.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            paragraph = cell_desc_pagina.AddParagraph();
            paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
            paragraph.AppendText("Página ");
            _currentPage++;
            TextRange text_desc_pagina = paragraph.AppendText(_currentPage.ToString());
            text_desc_pagina.CharacterFormat.Bold = true;
            paragraph.AppendText(" de ");
            text_desc_pagina = paragraph.AppendText(_doc.PageCount.ToString());
            text_desc_pagina.CharacterFormat.Bold = true;

            paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
            section.HeadersFooters.FirstPageHeader.Tables.Add(table);
        }

        private string Pag1(Section section, CartaResponsiva cartaResponsiva)
        {
            Paragraph paragraph = section.AddParagraph();
            ParagraphStyle style = new(_doc);
            style.Name = "FontStyle";
            style.CharacterFormat.FontName = "Arial";
            style.CharacterFormat.FontSize = 11;
            _doc.Styles.Add(style);
            paragraph.ApplyStyle(style.Name);

            // Usuario y propietario:
            paragraph.AppendText("Por la presente ");
            TextRange text_presente = paragraph.AppendText(cartaResponsiva.Usuario);
            text_presente.CharacterFormat.Bold = true;
            paragraph.AppendText($" manifiesto haber recibido solamente para uso en labores que desempeño y como " +
                $"herramienta de trabajo, por parte de Process Management Solutions, S.A. de C.V. y " +
                $"{cartaResponsiva.Propietario} el equipo de cómputo que a continuación se describe:\n\n");
            paragraph.Format.HorizontalAlignment = HorizontalAlignment.Justify;

            // Informacion general equipo:
            foreach (InfoEquipo info in cartaResponsiva.InfoGeneralEquipo!)
            {
                paragraph.AppendText($"\t{info.Caracteristica}:\t\t\t");
                TextRange text_laptop = paragraph.AppendText($"{info.Descripcion}\r\n");
                text_laptop.CharacterFormat.Bold = true;
            }

            paragraph.AppendText("\tAsignación: ____");

            if((cartaResponsiva.Asignacion == true && cartaResponsiva.Cambio == true) || (cartaResponsiva.Asignacion == false && cartaResponsiva.Cambio == false))
            {
                _respuesta = "Selecciona solo una opción, entre asignacion y cambio.";
                return _respuesta;
            }
            else if(cartaResponsiva.Asignacion == true && cartaResponsiva.Cambio == false)
            {
                TextRange text_asignacion = paragraph.AppendText("SI");
                text_asignacion.CharacterFormat.Bold = true;
                paragraph.AppendText("___\tCambio: ___");
                paragraph.AppendText("___\r\n\n");
            }
            else if (cartaResponsiva.Asignacion == false && cartaResponsiva.Cambio == true)
            {
                paragraph.AppendText("___\tCambio: ___");
                TextRange text_cambio = paragraph.AppendText("SI");
                text_cambio.CharacterFormat.Bold = true;
                paragraph.AppendText("___\r\n\n");
            }

            // Propietario:
            paragraph.AppendText($"En tal virtud y al no haberse transmitido la propiedad del referido " +
                $"equipo, Process Management Solutions, S.A. de C.V. y {cartaResponsiva.Propietario} se " +
                $"reserva el derecho a retirarlo, cuando de acuerdo con las funciones desarrolladas se " +
                $"estime que ya no es necesario o cuando se incumpla con las políticas que mas adelante " +
                $"se precisaran.\n\n");

            paragraph.AppendText("El equipo se recibe en óptimas condiciones operativas y con la siguiente " +
                "configuración de hardware.\n\n");

            // Informacion hardware equipo:
            foreach (InfoEquipo infoHardware in cartaResponsiva.InfoHardwareEquipo!)
            {
                paragraph.AppendText($"\t{infoHardware.Caracteristica}:");
                TextRange text_procesador = paragraph.AppendText($"\t\t{infoHardware.Descripcion}\r\n");
                text_procesador.CharacterFormat.Bold = true;
            }

            paragraph.AppendText("\r\n\n");

            // Propietario:
            paragraph.AppendText($"Respecto a esta configuración, el usuario se compromete a no realizar " +
                $"ajustes ni reparaciones por su cuenta y en todo caso deberá hacer del conocimiento del " +
                $"PM Soluciones, S.A. de C.V. y {cartaResponsiva.Propietario} de cualquier anomalía que detecte " +
                $"en el equipo.\n\n");

            paragraph.AppendText("El equipo de referencia se entrega con la siguiente paquetería o software:\n\n");

            // Informacion software equipo:
            foreach (string infoSoftware in cartaResponsiva.InfoSoftwareEquipo!)
            {
                paragraph.AppendText($"\t• {infoSoftware}\r\n");
            }

            paragraph.AppendText("\r\n\n\n");

            // TODO:
            paragraph.AppendText("El equipo cuenta con el usuario local:\r\nUsuario:");
            TextRange text_usuario = paragraph.AppendText("\tmgonzalezi\r\n");
            text_usuario.CharacterFormat.Bold = true;
            paragraph.AppendText("Contraseña:");
            TextRange text_password = paragraph.AppendText("\tVentana3030\r\n\n");
            text_password.CharacterFormat.Bold = true;

            // TODO:
            paragraph.AppendText("La cuenta de correo se maneja con gmail por lo que se " +
                "podrá consultar desde la página\r\n");
            paragraph.AppendHyperlink("https://mail.google.com/mail", "https://mail.google.com/mail", HyperlinkType.WebLink);
            paragraph.AppendText("\r\n\nCuenta:\tmaria.gonzalez@people-media.com.mx \r\nContraseña: \tVentana3030\r\n");

            paragraph = new Paragraph(_doc);
            paragraph.AppendBreak(BreakType.PageBreak);
            _doc.LastSection.Paragraphs.Add(paragraph);

            // Get the first section of Word Document
            Section section2 = _doc.Sections[0];
            HeaderFooter header = section2.HeadersFooters.Header;
            Paragraph paragraph2 = header.AddParagraph();

            // Agregar tabla:
            Table table = header.AddTable(true);
            table.ResetCells(1, 3);
            TableCell cell1 = table.Rows[0].Cells[2];
            cell1.SplitCell(0, 2);

            TableCell cell_logo_pm = table[0, 0];
            cell_logo_pm.Width = 20;
            cell_logo_pm.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            paragraph2 = cell_logo_pm.AddParagraph();
            paragraph2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            paragraph2.AppendPicture(_image);

            TableCell cell_desc_doc = table[0, 1];
            cell_desc_doc.Width = 370;
            cell_desc_doc.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            paragraph2 = cell_desc_doc.AddParagraph();
            paragraph2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange text_desc_doc = paragraph2.AppendText("CARTA RESPONSIVA DE EQUIPO DE CÓMPUTO");
            text_desc_doc.CharacterFormat.Bold = true;

            TableCell cell_desc_codigo = table[0, 2];
            TableRow row_desc_codigo = table.Rows[0];
            row_desc_codigo.Height = 30;
            cell_desc_codigo.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            paragraph2 = cell_desc_codigo.AddParagraph();
            paragraph2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange text_desc_codigo = paragraph2.AppendText("Código");
            text_desc_codigo.CharacterFormat.Bold = true;
            paragraph2.AppendText(": RE-ASS-STI-1");

            TableCell cell_desc_pagina = table[1, 2];
            TableRow row_desc_pagina = table.Rows[1];
            row_desc_pagina.Height = 30;
            cell_desc_pagina.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            paragraph2 = cell_desc_pagina.AddParagraph();
            paragraph2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            paragraph2.AppendText("Página ");
            _currentPage++;
            TextRange text_desc_pagina = paragraph2.AppendText(_currentPage.ToString());
            text_desc_pagina.CharacterFormat.Bold = true;
            paragraph2.AppendText(" de ");
            text_desc_pagina = paragraph2.AppendText("2");
            text_desc_pagina.CharacterFormat.Bold = true;

            paragraph2.Format.HorizontalAlignment = HorizontalAlignment.Center;

            // Propietario:
            paragraph.AppendText($"\n\nRespecto de este Software, el usuario se compromete a respetar en " +
                $"todo momento la política corporativa existente y cuyos puntos fundamentales previenen los " +
                $"siguientes:\n" +
                $"1.- En virtud de que Process Management Solutions, S.A.de C.V.y {cartaResponsiva.Propietario} no " +
                $"es dueño de este Software ni de su documentación relacionada, el usuario no tiene el derecho " +
                $"de reproducirlo.\n" +
                $"2.- Con respecto a redes de área local y varias máquinas, los usuarios deberán usar " +
                $"el Software solamente de la manera establecida por el contrato de licencia de uso.\n" +
                $"3.- Los usuarios que sepan el uso no autorizado de Software o de documentación relacionada " +
                $"dentro de la empresa deberán notificar a Process Management Solutions, S.A.de C.V.y {cartaResponsiva.Propietario}\n" +
                $"4.- Según las leyes de derechos de autor, las personas implicadas en la reproducción ilegal de " +
                $"Software pueden ser demandadas por daños y perjuicios y enfrentar penas criminales, incluyendo " +
                $"multas y prisión.Process Management Solutions, S.A.de C.V.y {cartaResponsiva.Propietario} no permite " +
                $"la duplicación ilegal del Software.Los usuarios lo hagan, adquieran o usen copias no autorizadas " +
                $"de Software serán disciplinados de acuerdo con las circunstancias.Las medidas disciplinarias " +
                $"podrían incluir el despido.\n" +
                $"5.- Antes de proceder a realizar instalaciones de Software, cualquier duda sobre si el usuario " +
                $"puede copiar o usar un Software, deberá ser dirigido a su jefe inmediato.\n" +
                $"6.- El empleado que haga, adquiera, copie o use, copias no autorizadas, mediante licencia de uso " +
                $"de Software, responderá en lo personal, tanto civil como penalmente de sus acciones, sin " +
                $"responsabilidad para Process Management Solutions, S.A.de C.V.y {cartaResponsiva.Propietario}\n\n" +
                $"Lo anterior aplicara inclusive respecto de archivos en formato MP3, manifestando en este documento, " +
                $"tener conocimiento de que almacenamiento no está permitido ni en un disco duro de equipo asignado " +
                $"ni en espacio en el servidor, por lo que la firma de la presente significa mi conocimiento de que " +
                $"Process Management Solutions S.A. de C.V. y {cartaResponsiva.Propietario} procederá a establecer medidas " +
                $"de seguridad a fin de que tales archivos sean borrados, sin responsabilidad para ella, obligándome en " +
                $"lo futuro a no grabar este tipo de archivos, en los términos de los 6 puntos anteriores.\n\n\n\n");

            paragraph.AppendText("\tNombre de usuario completo:\t_____________________________________________\r\n\t\r\n" +
                "\tFirma:\t______________________________________________________\r\n\t\r\n" +
                "\tFecha de entrega:\t______________________________________________________\r\n");

            paragraph.Format.HorizontalAlignment = HorizontalAlignment.Justify;

            return _respuesta = "Word generado exitosamente";
        }
    }
}
