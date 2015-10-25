using System;
using System.Runtime.Serialization;


namespace Daisy.Contracts
{
    [DataContract]
    public class ArgumentFaultInfo
    {
        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string ParamName { get; set; }
    }
}
