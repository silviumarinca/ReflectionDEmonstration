using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityFunction
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class InformationAttribute: Attribute
    {
        public string Description { get; set; }
    }
}
