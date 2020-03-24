using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClient.TaxStrategy
{
    public abstract class CustomerTaxStrategy
    {
        public abstract void PickStrategy(int customerId);
    }

    /// <summary>
    /// The 'Context' class
    /// </summary>
    public class CustomerContext
    { 
    
    }



}
