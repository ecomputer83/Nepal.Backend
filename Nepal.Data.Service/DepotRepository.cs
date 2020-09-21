using Nepal.EF.DB.Context;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Data.Service
{
    public class DepotRepository : CoreRepository<Depot, CoreContext>
    {
        public DepotRepository(CoreContext context) : base(context)
        {

        }
    }
}
