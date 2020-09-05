using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceBALApi.Models
{
    public class GenericReturnModel
    {
        public class OutputStatus
        {
            public int status { get; set; }
            public string message { get; set; }
            public string res { get; set; }
        }

        public class Info
        {
            public OutputStatus info = new OutputStatus();
        }

        public class DataReturnModel<T> : Info where T : class
        {
            public T data { get; set; }
        }
    }
}