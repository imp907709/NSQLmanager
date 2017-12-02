﻿
using System;
using System.IO;
using System.Text;
using IWebManagers;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Threading;
using System.Collections.Generic;

namespace WebManagers
{

    public class NoRequestBinded : Exception
    {
        
    }

    ///-->In new overwritten
    ///Base web manager for sending request with type method and reading response to URL
    ///WebRequest, Httpwebresponse
    public class WebManager : IWebManager
    {

        NetworkCredential _credentials;
        public WebRequest _request;
     
        public WebManager()
        {
            _request = null;            
        }
        public WebRequest AddRequest(string url, string method)
        {
            try
            {
                _request = WebRequest.Create(url);
                _request.Method = method;
                _request.ContentLength = 0;
                
                CredentialsBind();
                
            }
            catch (Exception e)
            {
                throw e;
            }
            return _request;
        }
        internal void AddHeader(HttpRequestHeader header, string value)
        {
            _request.Headers.Add(header, value);
        }
        public void AddBase64AuthHeader(string value)
        {
            _request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + System.Convert.ToBase64String(
            Encoding.ASCII.GetBytes(value)
            ));
        }
       
        internal string GetHeaderValue(string header)
        {
            string result = string.Empty;
            result = this._request.GetResponse().Headers.Get(header);
            return result;
        }
        public void SetCredentials(NetworkCredential credentials)
        {
            _credentials = credentials;
            CredentialsBind();
        }
        public void CredentialsBind()
        {
            if (this._request != null)
            {
                if (this._credentials != null)
                {
                    this._request.Credentials = _credentials;
                }
            }
        }
        public void CredentialsUnBind()
        {
            if (this._request != null)
            {
                this._request.Credentials = null;
            }
        }
        public virtual WebResponse GetResponseAuth(string url, string method)
        {

            AddRequest(url, method);            
            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public virtual WebResponse GetResponse(string url, string method)
        {

            AddRequest(url, method);
            CredentialsBind();
            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public virtual WebResponse GetResponse()
        {
            CredentialsBind();
            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (System.Net.WebException e)
            {                           
                string msg = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                System.Diagnostics.Trace.WriteLine(e.Message);
                System.Diagnostics.Trace.WriteLine(msg);
                throw new Exception(msg);
            }           
           
        }
        public virtual async Task<HttpWebResponse> GetResponseAsync(string url, string method)
        {
            HttpWebResponse resp;
            AddRequest(url, method);
            CredentialsBind();
            try
            {
                resp = (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }

            return await Task.FromResult(resp);
        }

        public WebResponse GetResponse64(string method) { throw new NotImplementedException(); }
        public WebResponse GetResponse(string method) { throw new NotImplementedException();  }
        public WebRequest AddRequest(string url) { throw new NotImplementedException(); }

    }

    public class WebRequestManager : IWebManager
    {

        WebRequest _request;
        NetworkCredential credentials;
        string GET="GET";
        string _url;
        object _lock = new object();
        int Timeout = 0;
        byte[] _content = null;
        string _method = null;
        Dictionary<HttpRequestHeader, string> _headres 
            = new Dictionary<HttpRequestHeader, string>();
        
        public WebRequest AddRequest(string url)
        {

            if (url == null) { throw new Exception("String not passed"); }
            SetUrl(url);
            this._request = WebRequest.Create(url);
            return this._request;            
            
        }
     
        public void SetUrl(string url_)
        {
            this._url = url_;
        }

        public void SetCredentials(NetworkCredential credentials)
        {
            this.credentials=credentials;
        }
        internal void bindCredentials()
        {
            CheckReq();
            if (this.credentials != null)
            {
                this._request.Credentials = this.credentials;
            }            
        }
     
        public bool SetHeader(HttpRequestHeader header, string value)
        {
            if (_headres.ContainsKey(header)) {
                _headres.Remove(header);
            }
            _headres.Add(header, value);
            return true;
        }
        public bool RemoveHeader(HttpRequestHeader header)
        {
            if (_headres.ContainsKey(header))
            {
                _headres.Remove(header);
            }
            return true;
        }
        internal bool bindHeaders()
        {          
            CheckReq();                       
            foreach (KeyValuePair<HttpRequestHeader, string> pair in _headres)
            {
                this._request.Headers.Clear();
                if (pair.Value != null)
                {
                    this._request.Headers.Add(pair.Key, pair.Value);
                }
            }
            return true;
        }
        public string GetHeader(string name)
        {
            string header=string.Empty;
            CheckReq();
            try
            {
                header = this._request.Headers.Get(name);
            } catch (Exception e) { }

            return header;
        }

        public void SetContent(string value)
        {
            value.ToCharArray().CopyTo(_content, 0);
        }
        void bindContent()
        {
            CheckReq();
            CheckContent();
            if (this._request.Method != GET)
            {
                try
                {
                    using (Stream str = this._request.GetRequestStream())
                    {
                        str.WriteAsync(_content, 0, _content.Length);
                    }
                }
                catch (Exception e) { }

            }
        }

        public void SetTimeout(int ms)
        {
            Timeout = ms;
        }
        void bindTimeout()
        {
            if (this._request != null && this.Timeout != 0)
            {
                this._request.Timeout = this.Timeout;
            }
        }

        public void SetBase64AuthHeader(string value)
        {
            SetHeader(HttpRequestHeader.Authorization, "Basic " + System.Convert.ToBase64String(
                Encoding.ASCII.GetBytes(value)
            ));
        }
        public void SetBase64AuthHeader()
        {
            if (this.credentials == null) { throw new Exception("Credentials not binded"); }
            string value_ = string.Format("{0}:{1}", this.credentials.UserName, this.credentials.Password);
            SetBase64AuthHeader(value_);
        }

        internal bool CheckReq()
        {
            if (this._request == null)
            {
                if (_url == null) { throw new NoRequestBinded(); }
                this._request = WebRequest.Create(_url);
            }
            
            return true;
        }
        internal bool CheckContent()
        {
            if (this._content == null) { return false; }
            if (this._content.Length == 0) { return false; }
            return true;
        }
        
        public void SetMethod(string method_)
        {
            if (method_ != null)
            {
                _method = method_;                
                //if (method_ != GET){this._request.ContentLength = 0; }
            }
            else { this._request.Method = GET; }
        }
        internal void bindMethod()
        {
            CheckReq();            
            if (_method!=null)
            {               
                this._request.Method = _method;
            }
        }

        internal void SwapMethod(string method_)
        {
            SetMethod(_method);
            SwapRequestsURL(_url);
        }
        internal void SwapRequestsURL(string url)
        {
            AddRequest(url);
            bindMethod();
            bindHeaders();
            if (this._request.Method != GET)
            {
                bindContent();
            }                      
        }
        internal void SwapContent(string value_)
        {
            CheckReq();
            SetContent(value_);
            bindMethod();
            bindHeaders();
            if (this._request.Method != GET)
            {
                bindContent();
            }
        }

        public HttpWebResponse GetHttpResponse(string method_)
        {

            CheckReq();
            SwapMethod(method_);          
        
            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e) { throw e; }

        }

        public WebResponse GetResponse(string method)
        {

            CheckReq();

            bindTimeout();
            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public WebResponse GetResponse(string url_, string method)
        {
            CheckReq();
            SetUrl(url_);
            SetMethod(method);
            bindTimeout();
            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public WebResponse GetResponse64(string method)
        {

            CheckReq();
            SetBase64AuthHeader();
            SetMethod(method);            
            bindTimeout();            
            SwapMethod(method);

            try
            {            
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public WebResponse GetResponse64(string url_, string method)
        {

            CheckReq();
            SetUrl(url_);
            SetBase64AuthHeader();
            SetMethod(method);
            bindTimeout();
            SwapMethod(method);

            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }

    /// <summary>
    /// DP Scope (data processing)    
    /// converts Responses to string
    /// </summary>
    public class WebResponseReader : IResponseReader
    {
             
        public string ReadResponse(WebResponse response)
        {
            string result = string.Empty;
            try
            {
                result = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception e){ System.Diagnostics.Trace.WriteLine(e.Message); }
            return result;
        }
        public string ReadResponse(HttpWebResponse response)
        {
            string result = string.Empty;
			try
            {
				Stream sm = response.GetResponseStream();
				StreamReader sr = new StreamReader(sm);
				result = sr.ReadToEnd();
			}
            catch(Exception e){ System.Diagnostics.Trace.WriteLine(e.Message); }
            return result;
        }
        public string ReadResponse(HttpResponseMessage response)
        {
            string result = string.Empty;
            System.Net.Http.HttpContent sm = response.Content;            
            Task<string> res = sm.ReadAsStringAsync();
            result = res.Result;
            return result;
        }
        public string ReadResponse(Task<HttpResponseMessage> response)
        {
            string result = string.Empty;
            Task<string> st = null;
            try
            {
                st = response.Result.Content.ReadAsStringAsync();
                result = st.Result;
            }
            catch (Exception e) { System.Diagnostics.Trace.WriteLine(e.Message); }

            return result;
        }
        public string ReadResponse(IHttpActionResult response)
        {
            string result  =null;
            Task<HttpResponseMessage> mes = response.ExecuteAsync(new System.Threading.CancellationToken());
            try
            {
                result = mes.Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e) { System.Diagnostics.Trace.WriteLine(e.Message); }
            return result;
        }

    }

    /// <summary>
    /// Wraps result in IhttpActionResult for ApiController return
    /// </summary>
    public class ReturnEntities : IHttpActionResult
    {
        HttpRequestMessage _returnedTask;
        public string _result;

        public ReturnEntities(string result_, HttpRequestMessage ar_)
        {
            this._returnedTask = ar_;
            this._result = result_;
        }

        async Task<HttpResponseMessage> IHttpActionResult.ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(_result, Encoding.UTF8, "text/plain");

            return await Task.FromResult(response);
        }
    }

    /// <summary>
    /// WEB scope deprecation posible (Only if several credentials to different hosts needed, several DBs? )
    /// Contains Credentials for URI
    /// Currently unused
    /// </summary>
    public class CredentialPool
    {
        CredentialCache credentialsCache;

        public void Add(Uri uri, string type, string username, string password)
        {
            credentialsCache = new CredentialCache();
            credentialsCache.Add(uri, type, new NetworkCredential(username, password));
        }
        public NetworkCredential credentials(Uri uri_, string type_)
        {
            return credentialsCache.GetCredential(uri_, type_);
        }
    }

}