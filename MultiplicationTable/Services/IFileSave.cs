using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultiplicationTable.Services
{
    public interface IFileSave
    {
        Task<bool> SaveXml(string content, string filename);
    }
}
