using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace Pipeline.Contracts
{
    [AddInContract]
    public interface IPluginApi : IContract
    {
        DateTime GetLastModifiedDate(string path);
    }
}