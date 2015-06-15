using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WordyFly.Service.DataContracts.Response
{

    [DataContract]
    public class Message
    {
        public Message()
        {

        }

        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public MessageSeverity Severity { get; set; }

        public override string ToString()
        {

            return base.ToString();
        }
    }

    [DataContract]
    public enum MessageSeverity
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        Information = 1,
        [EnumMember]
        Warning = 2,
        [EnumMember]
        Error = 3,
    }
}

