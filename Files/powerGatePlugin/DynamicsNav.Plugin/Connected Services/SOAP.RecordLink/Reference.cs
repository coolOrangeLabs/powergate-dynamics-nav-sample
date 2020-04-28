﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DynamicsNav.Plugin.SOAP.RecordLink {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", ConfigurationName="SOAP.RecordLink.RecordLink_Port")]
    public interface RecordLink_Port {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/RecordLink:ReadLinks", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks_Result ReadLinks(DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/RecordLink:ReadLinks", ReplyAction="*")]
        System.Threading.Tasks.Task<DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks_Result> ReadLinksAsync(DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/RecordLink:CreateLink", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DynamicsNav.Plugin.SOAP.RecordLink.CreateLink_Result CreateLink(DynamicsNav.Plugin.SOAP.RecordLink.CreateLink request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/RecordLink:CreateLink", ReplyAction="*")]
        System.Threading.Tasks.Task<DynamicsNav.Plugin.SOAP.RecordLink.CreateLink_Result> CreateLinkAsync(DynamicsNav.Plugin.SOAP.RecordLink.CreateLink request);
        
        // CODEGEN: Generating message contract since the wrapper name (ModifyLink_Result) of message ModifyLink_Result does not match the default value (ModifyLink)
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/RecordLink:ModifyLink", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink_Result ModifyLink(DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/RecordLink:ModifyLink", ReplyAction="*")]
        System.Threading.Tasks.Task<DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink_Result> ModifyLinkAsync(DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink request);
        
        // CODEGEN: Generating message contract since the wrapper name (DeleteLink_Result) of message DeleteLink_Result does not match the default value (DeleteLink)
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/RecordLink:DeleteLink", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink_Result DeleteLink(DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/RecordLink:DeleteLink", ReplyAction="*")]
        System.Threading.Tasks.Task<DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink_Result> DeleteLinkAsync(DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:microsoft-dynamics-nav/xmlports/x50150")]
    public partial class RecordLinks : object, System.ComponentModel.INotifyPropertyChanged {
        
        private RecordLink[] recordLinkField;
        
        private string[] textField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RecordLink", Order=0)]
        public RecordLink[] RecordLink {
            get {
                return this.recordLinkField;
            }
            set {
                this.recordLinkField = value;
                this.RaisePropertyChanged("RecordLink");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
                this.RaisePropertyChanged("Text");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:microsoft-dynamics-nav/xmlports/x50150")]
    public partial class RecordLink : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int linkIdField;
        
        private string recordIdField;
        
        private string typeField;
        
        private string url1Field;
        
        private string contentField;
        
        public RecordLink() {
            this.linkIdField = 0;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int LinkId {
            get {
                return this.linkIdField;
            }
            set {
                this.linkIdField = value;
                this.RaisePropertyChanged("LinkId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string RecordId {
            get {
                return this.recordIdField;
            }
            set {
                this.recordIdField = value;
                this.RaisePropertyChanged("RecordId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
                this.RaisePropertyChanged("Type");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Url1 {
            get {
                return this.url1Field;
            }
            set {
                this.url1Field = value;
                this.RaisePropertyChanged("Url1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string Content {
            get {
                return this.contentField;
            }
            set {
                this.contentField = value;
                this.RaisePropertyChanged("Content");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ReadLinks", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", IsWrapped=true)]
    public partial class ReadLinks {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=0)]
        public string recordId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=1)]
        public string companyName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=2)]
        public int linkType;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=3)]
        public DynamicsNav.Plugin.SOAP.RecordLink.RecordLinks recordLinks;
        
        public ReadLinks() {
        }
        
        public ReadLinks(string recordId, string companyName, int linkType, DynamicsNav.Plugin.SOAP.RecordLink.RecordLinks recordLinks) {
            this.recordId = recordId;
            this.companyName = companyName;
            this.linkType = linkType;
            this.recordLinks = recordLinks;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ReadLinks_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", IsWrapped=true)]
    public partial class ReadLinks_Result {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=0)]
        public DynamicsNav.Plugin.SOAP.RecordLink.RecordLinks recordLinks;
        
        public ReadLinks_Result() {
        }
        
        public ReadLinks_Result(DynamicsNav.Plugin.SOAP.RecordLink.RecordLinks recordLinks) {
            this.recordLinks = recordLinks;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CreateLink", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", IsWrapped=true)]
    public partial class CreateLink {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=0)]
        public string recordId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=1)]
        public string companyName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=2)]
        public int linkType;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=3)]
        public string content;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=4)]
        public string url;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=5)]
        public int linkIdOut;
        
        public CreateLink() {
        }
        
        public CreateLink(string recordId, string companyName, int linkType, string content, string url, int linkIdOut) {
            this.recordId = recordId;
            this.companyName = companyName;
            this.linkType = linkType;
            this.content = content;
            this.url = url;
            this.linkIdOut = linkIdOut;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CreateLink_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", IsWrapped=true)]
    public partial class CreateLink_Result {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=0)]
        public int linkIdOut;
        
        public CreateLink_Result() {
        }
        
        public CreateLink_Result(int linkIdOut) {
            this.linkIdOut = linkIdOut;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ModifyLink", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", IsWrapped=true)]
    public partial class ModifyLink {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=0)]
        public int linkId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=1)]
        public string content;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=2)]
        public string url;
        
        public ModifyLink() {
        }
        
        public ModifyLink(int linkId, string content, string url) {
            this.linkId = linkId;
            this.content = content;
            this.url = url;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ModifyLink_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", IsWrapped=true)]
    public partial class ModifyLink_Result {
        
        public ModifyLink_Result() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DeleteLink", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", IsWrapped=true)]
    public partial class DeleteLink {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", Order=0)]
        public int linkId;
        
        public DeleteLink() {
        }
        
        public DeleteLink(int linkId) {
            this.linkId = linkId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DeleteLink_Result", WrapperNamespace="urn:microsoft-dynamics-schemas/codeunit/RecordLink", IsWrapped=true)]
    public partial class DeleteLink_Result {
        
        public DeleteLink_Result() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface RecordLink_PortChannel : DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RecordLink_PortClient : System.ServiceModel.ClientBase<DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port>, DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port {
        
        public RecordLink_PortClient() {
        }
        
        public RecordLink_PortClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RecordLink_PortClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RecordLink_PortClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RecordLink_PortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks_Result DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port.ReadLinks(DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks request) {
            return base.Channel.ReadLinks(request);
        }
        
        public void ReadLinks(string recordId, string companyName, int linkType, ref DynamicsNav.Plugin.SOAP.RecordLink.RecordLinks recordLinks) {
            DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks inValue = new DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks();
            inValue.recordId = recordId;
            inValue.companyName = companyName;
            inValue.linkType = linkType;
            inValue.recordLinks = recordLinks;
            DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks_Result retVal = ((DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port)(this)).ReadLinks(inValue);
            recordLinks = retVal.recordLinks;
        }
        
        public System.Threading.Tasks.Task<DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks_Result> ReadLinksAsync(DynamicsNav.Plugin.SOAP.RecordLink.ReadLinks request) {
            return base.Channel.ReadLinksAsync(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DynamicsNav.Plugin.SOAP.RecordLink.CreateLink_Result DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port.CreateLink(DynamicsNav.Plugin.SOAP.RecordLink.CreateLink request) {
            return base.Channel.CreateLink(request);
        }
        
        public void CreateLink(string recordId, string companyName, int linkType, string content, string url, ref int linkIdOut) {
            DynamicsNav.Plugin.SOAP.RecordLink.CreateLink inValue = new DynamicsNav.Plugin.SOAP.RecordLink.CreateLink();
            inValue.recordId = recordId;
            inValue.companyName = companyName;
            inValue.linkType = linkType;
            inValue.content = content;
            inValue.url = url;
            inValue.linkIdOut = linkIdOut;
            DynamicsNav.Plugin.SOAP.RecordLink.CreateLink_Result retVal = ((DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port)(this)).CreateLink(inValue);
            linkIdOut = retVal.linkIdOut;
        }
        
        public System.Threading.Tasks.Task<DynamicsNav.Plugin.SOAP.RecordLink.CreateLink_Result> CreateLinkAsync(DynamicsNav.Plugin.SOAP.RecordLink.CreateLink request) {
            return base.Channel.CreateLinkAsync(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink_Result DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port.ModifyLink(DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink request) {
            return base.Channel.ModifyLink(request);
        }
        
        public void ModifyLink(int linkId, string content, string url) {
            DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink inValue = new DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink();
            inValue.linkId = linkId;
            inValue.content = content;
            inValue.url = url;
            DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink_Result retVal = ((DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port)(this)).ModifyLink(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink_Result> DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port.ModifyLinkAsync(DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink request) {
            return base.Channel.ModifyLinkAsync(request);
        }
        
        public System.Threading.Tasks.Task<DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink_Result> ModifyLinkAsync(int linkId, string content, string url) {
            DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink inValue = new DynamicsNav.Plugin.SOAP.RecordLink.ModifyLink();
            inValue.linkId = linkId;
            inValue.content = content;
            inValue.url = url;
            return ((DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port)(this)).ModifyLinkAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink_Result DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port.DeleteLink(DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink request) {
            return base.Channel.DeleteLink(request);
        }
        
        public void DeleteLink(int linkId) {
            DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink inValue = new DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink();
            inValue.linkId = linkId;
            DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink_Result retVal = ((DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port)(this)).DeleteLink(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink_Result> DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port.DeleteLinkAsync(DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink request) {
            return base.Channel.DeleteLinkAsync(request);
        }
        
        public System.Threading.Tasks.Task<DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink_Result> DeleteLinkAsync(int linkId) {
            DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink inValue = new DynamicsNav.Plugin.SOAP.RecordLink.DeleteLink();
            inValue.linkId = linkId;
            return ((DynamicsNav.Plugin.SOAP.RecordLink.RecordLink_Port)(this)).DeleteLinkAsync(inValue);
        }
    }
}
