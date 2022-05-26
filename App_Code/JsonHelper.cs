using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

    public static class JsonHelper
    {
        /// <summary>
        /// JSON Serialization
        /// </summary>
        public static string JsonSerializer<T>(T objToSeriz)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, objToSeriz);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            //Replace Json Date String                                         
            string p = @"\\/Date\((\d+)\+\d+\)\\/";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }

        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            //Convert "yyyy-MM-dd HH:mm:ss" String as "\/Date(1319266795390+0800)\/"
            //string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
            //MatchEvaluator matchEvaluator = new MatchEvaluator(
            //    ConvertDateStringToJsonDate);
            //Regex reg = new Regex(p);
            //jsonString = reg.Replace(jsonString, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// Convert Serialization Time /Date(1319266795390+0800) as String
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// Convert Date String as Json Time
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }

        public static string GetJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new

            System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows =
              new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName.Trim(), dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        public static string ConvertTableToJson(DataTable dtEmployee)
        {
            //DataTable dtEmployee = new DataTable();

            //dtEmployee.Columns.Add("EmpId", typeof(int));
            //dtEmployee.Columns.Add("Name", typeof(string));
            //dtEmployee.Columns.Add("Address", typeof(string));
            //dtEmployee.Columns.Add("Date", typeof(DateTime));

            ////
            //// Here we add five DataRows.
            ////
            //dtEmployee.Rows.Add(25, "Rk", "Gurgaon", DateTime.Now);
            //dtEmployee.Rows.Add(50, "Sachin", "Noida", DateTime.Now);
            //dtEmployee.Rows.Add(10, "Nitin", "Noida", DateTime.Now);
            //dtEmployee.Rows.Add(21, "Aditya", "Meerut", DateTime.Now);
            //dtEmployee.Rows.Add(100, "Mohan", "Banglore", DateTime.Now);

            //gvEmployees.DataSource = dtEmployee;


            string[] jsonArray = new string[dtEmployee.Columns.Count];
            string headString = string.Empty;

            for (int i = 0; i < dtEmployee.Columns.Count; i++)
            {
                jsonArray[i] = dtEmployee.Columns[i].Caption; // Array for all columns
                headString += "'" + jsonArray[i] + "' : '" + jsonArray[i] + i.ToString() + "%" + "',";
            }

            headString = headString.Substring(0, headString.Length - 1);

            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            if (dtEmployee.Rows.Count > 0)
            {
                for (int i = 0; i < dtEmployee.Rows.Count; i++)
                {
                    string tempString = headString;
                    sb.Append("{");

                    // To get each value from the datatable
                    for (int j = 0; j < dtEmployee.Columns.Count; j++)
                    {
                        tempString = tempString.Replace(dtEmployee.Columns[j] + j.ToString() + "%", dtEmployee.Rows[i][j].ToString());
                    }

                    sb.Append(tempString + "},");
                }
            }
            else
            {
                string tempString = headString;
                sb.Append("{");
                for (int j = 0; j < dtEmployee.Columns.Count; j++)
                {
                    tempString = tempString.Replace(dtEmployee.Columns[j] + j.ToString() + "%", "-");
                }

                sb.Append(tempString + "},");
            }

            sb = new StringBuilder(sb.ToString().Substring(0, sb.ToString().Length - 1));
            sb.Append("]");
            // return sb.ToString(); // json formated output
            return StripControlChars(sb.ToString());
        }



        public static string DataTableToJson(DataTable Dt)
        {
            string[] StrDc = new string[Dt.Columns.Count];

            string HeadStr = string.Empty;
            for (int i = 0; i < Dt.Columns.Count; i++)
            {

                StrDc[i] = Dt.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\":\"" + StrDc[i] + i.ToString() + "¾" + "\",";

            }

            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

            StringBuilder Sb = new StringBuilder();

            Sb.Append("[");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {

                string TempStr = HeadStr;

                for (int j = 0; j < Dt.Columns.Count; j++)
                {

                    TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString().Trim());
                }
                //Sb.AppendFormat("{{{0}}},",TempStr);

                Sb.Append("{" + TempStr + "},");
            }

            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

            if (Sb.ToString().Length > 0)
                Sb.Append("]");

            return StripControlChars(Sb.ToString());

        }
        public static string StripControlChars(string s)
        {
            return Regex.Replace(s, @"[^\x20-\x7F]", "");
        }

        #region "Epoch DateTime"
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetCurrentUnixTimestampMillis()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        public static DateTime DateTimeFromUnixTimestampMillis(long millis)
        {
            return UnixEpoch.AddMilliseconds(millis);
        }

        public static long GetCurrentUnixTimestampSeconds()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
        }

        public static DateTime DateTimeFromUnixTimestampSeconds(long seconds)
        {
            return UnixEpoch.AddSeconds(seconds);
        }

        // convert datetime to unix epoch seconds

        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalMilliseconds);
        }

        public static DateTime FromUnixTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTime);
        }

        //public static long ToUnixTime(this DateTime date)
        //{
        //    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //    return Convert.ToInt64((date - epoch).TotalSeconds);
        //}

        #endregion

    }
