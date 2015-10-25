using System;
using System.Runtime.Serialization;


namespace Daisy.Contracts.Error
{
    [DataContract]
    public class FaultInfo
    {
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
