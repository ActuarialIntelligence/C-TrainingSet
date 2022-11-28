using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Domain.ContainerObjects
{
    public class Nominal
    {
        public decimal nominal { get; private set; }
        public Nominal(decimal nominal)
        {
            this.nominal = nominal;
        }
    }
}
