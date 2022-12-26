using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilesManager.Persistence.IDapperImplementation
{
    public interface ITablesManager
    {
        void CreateTable(Dictionary<Type, Type[]> modelAndNavPropsTypes);
        void DeleteTables(params Type[] modelTypes);
        void ClearTables(params Type[] modelTypes);
    }
}
