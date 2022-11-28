using AI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Domain.ContainerObjects
{
    public class PlainListTermCashFlowSet
    {
        public IList<TermCashflowYieldSet> cashflowSet;
        public DateTime anchorDate;
        public Term termType;
        public decimal nominal;
        public RESTMethodType restMethodType;
    }
}
