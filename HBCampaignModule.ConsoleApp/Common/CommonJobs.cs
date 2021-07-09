using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.ConsoleApp.Common
{
    public static class CommonJobs
    {
        public static T GetTypeProductItem<T>(string[] values, int index)
        {
            try
            {
                return (T)Convert.ChangeType(values[index], typeof(T));
            }
            catch (Exception ex)
            {
                return Activator.CreateInstance<T>();
            }
        }

        public static string[] OperationValueShred(string operationValue)
        {
            return operationValue.Trim().Split(" ");
        }
    }
}
