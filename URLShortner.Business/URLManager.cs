using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace URLShortner.Business
{
    public class URLManager
    {
        public URLResponse Shorten(string longUrl)
        {
            if (string.IsNullOrWhiteSpace(longUrl))
                throw new ArgumentException("You must provide a value for longUrl");

            var req = WebRequest.Create(GetUrl());
            req.Method = "POST";
            req.ContentType = "application/json";

            var postBody = string.Format(@"{{""longUrl"": ""{0}""}}", longUrl);
            var postData = Encoding.ASCII.GetBytes(postBody);

            req.ContentLength = postData.Length;
            var reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();


            var resp = req.GetResponse();
            using (var respReader = new StreamReader(resp.GetResponseStream()))
            {
                var responseBody = respReader.ReadToEnd();

                var deserializer = new JavaScriptSerializer();
                return deserializer.Deserialize<URLResponse>(responseBody);
            }
        }
        public URLResponse HashUrl(string strURL)
        {
            var result = Encrypt(strURL);
            URLResponse response = new URLResponse();
            response.hashurl = result;
            return response;
        }
        private string Encrypt(string stringToEncrypt)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
            byte[] key = { };
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes("A0D1nX0Q");
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        protected static string GetUrl()
        {
            string API_KEY = "AIzaSyCZm9HvMq2NNOE3EPu56gbKFCFCK7qapKvC8";
            const string API_URL = "https://www.googleapis.com/urlshortener/v1/url";

            if (string.IsNullOrWhiteSpace(API_KEY))
                return API_URL;
            else
                return string.Concat(API_URL, "?key=", API_KEY);
        }
    }
}
