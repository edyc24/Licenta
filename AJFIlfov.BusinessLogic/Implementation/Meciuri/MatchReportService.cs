using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing.Template;

namespace AJFIlfov.BusinessLogic.Implementation.MeciuriService
{
    public class MatchReportService
    {
        public void GenerateMatchReport(MatchReportData reportData, string outputFilePath)
        {
            string pdfTemplatePath = "C:\\Users\\eduard.cristea\\source\\repos\\Licenta\\AJFIlfov\\wwwroot\\raport_nou_ROMANIA_2024_CN U16.pdf";

            using (var reader = new PdfReader(pdfTemplatePath))
            {
                using (var writer = new PdfWriter(outputFilePath))
                {
                    using (var pdfDoc = new PdfDocument(reader, writer))
                    {
                        // Load the PDF form
                        var form = PdfAcroForm.GetAcroForm(pdfDoc, true);

                        // Fill in the form fields with data
                        form.GetField("MatchLocation").SetValue(reportData.MatchLocation);
                        form.GetField("MatchDate").SetValue(reportData.MatchDate);
                        form.GetField("StadiumName").SetValue(reportData.StadiumName);
                        form.GetField("RefereeName").SetValue(reportData.RefereeName);
                        form.GetField("AssistantReferee1").SetValue(reportData.AssistantReferee1);
                        form.GetField("AssistantReferee2").SetValue(reportData.AssistantReferee2);
                        form.GetField("HomeTeam").SetValue(reportData.HomeTeam);
                        form.GetField("AwayTeam").SetValue(reportData.AwayTeam);

                        // Flatten the form to make the changes permanent
                        form.FlattenFields();
                    }
                }
            }
        }
    }

    public class MatchReportData
    {
        public string MatchLocation { get; set; }
        public string MatchDate { get; set; }
        public string StadiumName { get; set; }
        public string RefereeName { get; set; }
        public string AssistantReferee1 { get; set; }
        public string AssistantReferee2 { get; set; }
        public string FourthOfficial { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Result { get; set; }
        public string HalftimeResult { get; set; }
        public List<string> HomeTeamWarnings { get; set; }
        public List<string> AwayTeamWarnings { get; set; }
        public List<string> HomeTeamExpulsions { get; set; }
        public List<string> AwayTeamExpulsions { get; set; }
        public List<string> Goals { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
