using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDataManager
    {
        bool Add(string name, int id = -1);
        bool Delete(object name);
        bool Edit(int actualId, string newName);
        bool isExist(string name, int id = -1);
    }
}
