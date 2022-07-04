using SchoolLearnApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLearnApp.Classes
{
    class BaseChanger
    {
        public static void DeleteService(int serviceId)
        {
            var data = SchoolLanguageEntities.GetContext();

            var selectService = from service in data.Service
                                where service.ID == serviceId
                                select service;

            data.Service.Remove(selectService.FirstOrDefault());
            data.SaveChanges();
        }
    }
}
