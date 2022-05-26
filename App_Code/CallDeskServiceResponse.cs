using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;
using System.ServiceModel.Web;
using System.Reflection;


    public class CallDeskServiceResponse
    {
        public CallDeskServiceRequest ServiceRequest { get; set; }
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public object Result { get; set; }
        public string RequestEndTime { get; set; }

        public static Stream ServiceJsonResponse(CallDeskServiceResponse result, string strMethodName = null)
        {
            var jsonSeria = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };

            //For Ignoring Null properties start
             jsonSeria.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            //For Ignoring Null Properties end
            
            var json = jsonSeria.Serialize(result);            
            //LogResponse(result, json);
            //result.ServiceRequest = null;
            var finalJson = jsonSeria.Serialize(result);
            //WriteAllLogs(strMethodName + " Service Call End With Response " + json);
            
            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(finalJson));
        }

        #region Ignore Null property values
        private class NullPropertiesConverter : JavaScriptConverter
        {
            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                var jsonExample = new Dictionary<string, object>();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    //check if decorated with ScriptIgnore attribute
                    bool ignoreProp = prop.IsDefined(typeof(ScriptIgnoreAttribute), true);

                    var value = prop.GetValue(obj, BindingFlags.Public, null, null, null);
                    if (value != null && !ignoreProp)
                        jsonExample.Add(prop.Name, value);
                }

                return jsonExample;
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get { return GetType().Assembly.GetTypes(); }
            }
        }
        #endregion
       
    }


