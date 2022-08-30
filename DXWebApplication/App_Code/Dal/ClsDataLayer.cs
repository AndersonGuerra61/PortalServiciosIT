using System;
using System.Data;


namespace DXWebApplication.App_Code.Dal
{
    public class ClsDataLayer 
    {
       
        DataSet dsReturn;
       
        public DataSet DsReturn
        {
            get
            {
                return dsReturn;
            }
            set
            {
                dsReturn = value;
            }
        }
        


    }
}
