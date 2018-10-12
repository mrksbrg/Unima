﻿using System.Collections.Generic;
using System.IO;
using Anotar.Log4Net;
using Newtonsoft.Json;

namespace Cama.Core.Execution.Report.Cama
{
    public class CamaReportCreator : ReportCreator
    {
        public CamaReportCreator(string savePath)
            : base(savePath)
        {
        }

        public override void SaveReport(IList<MutationDocumentResult> mutations)
        {
            LogTo.Info("Saving cama report..");

            var camaReport = new CamaReport(mutations);
            File.WriteAllText(SavePath, JsonConvert.SerializeObject(camaReport));

            LogTo.Info("Cama report saved successfully.");
        }
    }
}