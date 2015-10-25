using System;
using System.Runtime.Serialization;


namespace Daisy.Contracts.Error
{
    [DataContract]
    public class ArgumentFaultInfo : FaultInfo
    {
        [DataMember]
        public string ParamName { get; set; }
    }
}
