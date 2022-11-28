using AI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Domain.ContainerObjects
{
    public class SpotYield
    {
        public Term Term { get; private set; }
        public decimal Yield { get; private set; }
        public SpotYield(decimal yield, Term term)
        {
            Yield = yield;
            Term = term;
        }
    }
}
