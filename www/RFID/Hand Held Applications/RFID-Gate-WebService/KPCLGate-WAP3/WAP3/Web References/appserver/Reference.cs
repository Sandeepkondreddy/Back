﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5485
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.5485.
// 
namespace KPCLGate.appserver {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="Service1Soap", Namespace="http://krishnapatnamport.nlr/")]
    public partial class Service1 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public Service1() {
            this.Url = "http://172.168.0.45/rfid-web/RFID-Service.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/TestInsertDetails", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string TestInsertDetails(string empno, string name) {
            object[] results = this.Invoke("TestInsertDetails", new object[] {
                        empno,
                        name});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginTestInsertDetails(string empno, string name, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("TestInsertDetails", new object[] {
                        empno,
                        name}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndTestInsertDetails(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/GetReaderDetails", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetReaderDetails(string ReaderIP) {
            object[] results = this.Invoke("GetReaderDetails", new object[] {
                        ReaderIP});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetReaderDetails(string ReaderIP, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetReaderDetails", new object[] {
                        ReaderIP}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetReaderDetails(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/Tagfree", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int Tagfree(string tagno) {
            object[] results = this.Invoke("Tagfree", new object[] {
                        tagno});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginTagfree(string tagno, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Tagfree", new object[] {
                        tagno}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndTagfree(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/Truckfree", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int Truckfree(string truckno) {
            object[] results = this.Invoke("Truckfree", new object[] {
                        truckno});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginTruckfree(string truckno, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Truckfree", new object[] {
                        truckno}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndTruckfree(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/InsertRegDetails", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string InsertRegDetails(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime) {
            object[] results = this.Invoke("InsertRegDetails", new object[] {
                        tagno,
                        truckno,
                        optype,
                        readerno,
                        readerip,
                        user,
                        trucktype,
                        remarks,
                        regtime});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginInsertRegDetails(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("InsertRegDetails", new object[] {
                        tagno,
                        truckno,
                        optype,
                        readerno,
                        readerip,
                        user,
                        trucktype,
                        remarks,
                        regtime}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndInsertRegDetails(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/InsertRegDetailsSync", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string InsertRegDetailsSync(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime) {
            object[] results = this.Invoke("InsertRegDetailsSync", new object[] {
                        tagno,
                        truckno,
                        optype,
                        readerno,
                        readerip,
                        user,
                        trucktype,
                        remarks,
                        regtime});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginInsertRegDetailsSync(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("InsertRegDetailsSync", new object[] {
                        tagno,
                        truckno,
                        optype,
                        readerno,
                        readerip,
                        user,
                        trucktype,
                        remarks,
                        regtime}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndInsertRegDetailsSync(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/GetTruckDetails", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetTruckDetails(string tagno) {
            object[] results = this.Invoke("GetTruckDetails", new object[] {
                        tagno});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetTruckDetails(string tagno, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetTruckDetails", new object[] {
                        tagno}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetTruckDetails(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/InsertUnRegDetails", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string InsertUnRegDetails(string regid, string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime) {
            object[] results = this.Invoke("InsertUnRegDetails", new object[] {
                        regid,
                        tagno,
                        truckno,
                        trucktype,
                        readerno,
                        readerip,
                        user,
                        unregtime});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginInsertUnRegDetails(string regid, string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("InsertUnRegDetails", new object[] {
                        regid,
                        tagno,
                        truckno,
                        trucktype,
                        readerno,
                        readerip,
                        user,
                        unregtime}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndInsertUnRegDetails(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/InsertUnRegDetailsSync", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string InsertUnRegDetailsSync(string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime) {
            object[] results = this.Invoke("InsertUnRegDetailsSync", new object[] {
                        tagno,
                        truckno,
                        trucktype,
                        readerno,
                        readerip,
                        user,
                        unregtime});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginInsertUnRegDetailsSync(string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("InsertUnRegDetailsSync", new object[] {
                        tagno,
                        truckno,
                        trucktype,
                        readerno,
                        readerip,
                        user,
                        unregtime}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndInsertUnRegDetailsSync(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/GetDBTime", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetDBTime() {
            object[] results = this.Invoke("GetDBTime", new object[0]);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDBTime(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDBTime", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetDBTime(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://krishnapatnamport.nlr/GetMasterDetails", RequestNamespace="http://krishnapatnamport.nlr/", ResponseNamespace="http://krishnapatnamport.nlr/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetMasterDetails(string getDetails) {
            object[] results = this.Invoke("GetMasterDetails", new object[] {
                        getDetails});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetMasterDetails(string getDetails, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetMasterDetails", new object[] {
                        getDetails}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataTable EndGetMasterDetails(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataTable)(results[0]));
        }
    }
}
