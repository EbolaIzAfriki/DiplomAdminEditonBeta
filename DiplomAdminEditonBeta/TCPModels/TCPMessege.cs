using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomAdminEditonBeta
{
    public class TCPMessege
    {
        /// <summary>
        /// Код операции: 1 = Get, 2 = GetBy, 3 = Create, 4 = Update, 5 = Delete; При ответе сервера 0 = неудача, 1 = удача 
        /// </summary>
        public int CodeOperation;
        /// <summary>
        /// Коды сущности: 1 - User, 2 - Client, 3 - Carrier, 4 - Request, 5 - Service, 
        /// </summary>
        public int CodeEntity;
        public string Entity;
        /// <summary>
        /// Объект сообщения для передачи информации между клиентом и сервером
        /// </summary>
        /// <param name="O">Код операции: 1 = Get, 2 = GetBy, 3 = Create, 4 = Update, 5 = Delete; При ответе сервера 0 = неудача, 1 = удача </param>
        /// <param name="C">Коды сущности: 1 - User, 2 - Client, 3 - Carrier, 4 - Request, 5 - Service </param>
        /// <param name="E">Объект передачи</param>
        public TCPMessege(int O, int C, object E)
        {
            CodeOperation = O;
            CodeEntity = C;
            Entity = JsonConvert.SerializeObject(E, Formatting.Indented, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }
    }
}
