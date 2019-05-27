using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace commodities.Classes.Misc
{
    public class MethodResultData<T>
    {
        public MethodResultData(string message)
        {
            Successful = false;
            Message = message;
        }
        public MethodResultData(T data)
        {
            Successful = true;
            Message = string.Empty;
            Data = data;
        }
        public T Data { get; set; }
        public bool Successful { get; set; }
        public string Message { get; set; }
    }
}