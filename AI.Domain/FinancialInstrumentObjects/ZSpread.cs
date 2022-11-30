using AI.Domain.ContainerObjects;
using AI.Domain.Enums;
using AI.Domain.Mathematical_Technique_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Domain.FinancialInstrumentObjects
{
    public class ZSpread
    {
        private ListTermCashflowSet cashFlowSet;
        int days;
        private decimal nominal;
        private decimal spread;

        public decimal Spread()
        {
            return spread == 0 ? CalculateZspread() : spread;
        }

        public ZSpread(ListTermCashflowSet cashFlowSet, decimal nominal)
        {
            this.cashFlowSet = cashFlowSet;
            this.nominal = nominal;
            days = cashFlowSet.termType == Term.YearlyEffective ? 365 :
                cashFlowSet.termType == Term.MonthlyEffective ? 30 : 0;
        }
        /// <summary>
        /// Obtains Z-Spread value via interpolation and returns the value.
        /// </summary>
        /// <returns></returns>
        public decimal CalculateZspread()
        {
            Annuity annuity = new Annuity(cashFlowSet, days);
            var result = Interpolation.Interpolate(annuity.GetZSpreadPV, 0.01m, 0.09m, nominal);
            spread = result;
            return result;
        }

    }
}
