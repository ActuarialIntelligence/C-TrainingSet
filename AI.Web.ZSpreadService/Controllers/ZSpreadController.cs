using AI.Domain.ContainerObjects;
using AI.Domain.FinancialInstrumentObjects;
using AI.Domain.Mathematical_Technique_Objects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AI.Web.ZSpreadService.Controllers
{
    [Route("api/ZSpread")]
    [ApiController]
    public class ZSpreadController : ControllerBase
    {

        // GET api/values
        [HttpPost("Z-SpreadPresentValue")]
        public ActionResult<decimal>
            ZSpreadPresentValue(ListTermCashflowSet cashFlowSet, int days, decimal nominal)
        {
            Annuity annuity = new Annuity(cashFlowSet, days);
            var result = Interpolation.Interpolate(annuity.GetZSpreadPV, 0.01m, 0.09m, nominal);
            return result;
        }

        // GET api/values
        [HttpPost("AnnuityPresentValue")]
        public ActionResult<decimal>
            AnnuityPresentValue(PlainListTermCashFlowSet cashFlowSet, int days, decimal nominal)
        {
            var cashFlowList =
                new ListTermCashflowSet(cashFlowSet.cashflowSet, cashFlowSet.termType);
            Annuity annuity = new Annuity(cashFlowList, days);

            var result = annuity.GetPV();
            return result;
        }
        /// <summary>
        /// POST
        /// </summary>
        /// <param name="cashFlowSet"></param>
        /// <returns></returns>
        [HttpPost("Z-Spread")]
        public ActionResult<decimal> ZSpread(PlainListTermCashFlowSet cashFlowSet)
        {
            var cashFlowList =
    new ListTermCashflowSet(cashFlowSet.cashflowSet, cashFlowSet.termType);

            var res = new ZSpread(cashFlowList, cashFlowSet.nominal);
            var result = res.CalculateZspread();
            return result;
        }


        [HttpPost("RunPython")]
        public ActionResult<string> RunPython(string script)
        {
            script.Replace(' ', '\n');
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\rajiyer\PycharmProjects\pythonProject\venv\Scripts\python.exe";
            var sw = new StreamWriter(@"C:\Users\rajiyer\PycharmProjects\pythonProject\main.py");
            sw.Write(script);
            sw.Close();
            var scriptPath = @"C:\Users\rajiyer\PycharmProjects\pythonProject\main.py";
            psi.Arguments = $"\"{scriptPath}\"";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            var errors = "";
            var results = "";
            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }
            return results;
        }

        [HttpPost("TestRunPython")]
        public ActionResult<string> TestRunPython(string script)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\rajiyer\PycharmProjects\pythonProject\venv\Scripts\python.exe";
            var scriptPath = @"C:\Users\rajiyer\PycharmProjects\pythonProject\main.py";
            psi.Arguments = $"\"{scriptPath}\"";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            var errors = "";
            var results = "";
            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }
            return results;
        }
    }
}
