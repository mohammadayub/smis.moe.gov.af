using Clean.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence.Service
{
    public interface IProcessChangeListener
    {
        public int ModuleID { get; set; }
        public Task ProcessChangedAsync(int RecordID,int ToProcess,int FromProcess,BaseContext baseContext);
    }
}
