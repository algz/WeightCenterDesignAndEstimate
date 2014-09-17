using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALFactory;

namespace BLL
{
    public class BLLDBOper
    {
        private readonly IDAL.IDBOper dal = DataAccess.CreateDBOperData();

        public void CreateDBTable()
        {
            dal.CreateTableWeightDesignData();
            dal.CreateTableTypeWeightData();
            dal.CreateTableCoreEnvelopeDesignData();
        }
    }
}
