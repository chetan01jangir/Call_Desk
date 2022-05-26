using System.Collections.Generic;
using System.Runtime.Serialization;
using System;


    #region Person Entity
    [DataContract]
    [Serializable]
    public class Person
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Age { get; set; }
        [DataMember]
        public List<AddressDetails> AddressList { get; set; }
    }
    #endregion
    #region Error Handler
    [DataContract]
    [Serializable]
    public class ErrorHandler
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string Exception { get; set; }

    }
    #endregion
    #region Address
    [DataContract]
    [Serializable]
    public class AddressDetails
    {
        [DataMember]
        public string Steeet { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }

    }
    #endregion

