using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WordyFly.Service.DataContracts.Response
{

    public abstract class ResponseBase
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the Messages
        /// </summary>
        [DataMember]
        public Collection<Message> Messages { get; set; }

        /// <summary>
        /// Gets or sets the request id.
        /// </summary>
        [DataMember]
        public string RequestId { get; set; }

        #endregion
    }
}