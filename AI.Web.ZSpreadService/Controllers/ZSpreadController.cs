using AI.Domain.ContainerObjects;
using AI.Domain.FinancialInstrumentObjects;
using AI.Domain.Mathematical_Technique_Objects;
using Microsoft.AspNetCore.Mvc;

namespace AI.Web.ZSpreadService.Controllers
{
    [Route("[controller]")]
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
    }
}
