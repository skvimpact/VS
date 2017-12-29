﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TSQLExample.DAL.RF_Resources {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ConfigurationElement", Namespace="http://schemas.datacontract.org/2004/07/System.Configuration")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TSQLExample.DAL.RF_Resources.ConnectionStringSettings))]
    public partial class ConfigurationElement : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool LockItemField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool LockItem {
            get {
                return this.LockItemField;
            }
            set {
                if ((this.LockItemField.Equals(value) != true)) {
                    this.LockItemField = value;
                    this.RaisePropertyChanged("LockItem");
                }
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ConnectionStringSettings", Namespace="http://schemas.datacontract.org/2004/07/System.Configuration")]
    [System.SerializableAttribute()]
    public partial class ConnectionStringSettings : TSQLExample.DAL.RF_Resources.ConfigurationElement {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConnectionStringField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProviderNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ConnectionString {
            get {
                return this.ConnectionStringField;
            }
            set {
                if ((object.ReferenceEquals(this.ConnectionStringField, value) != true)) {
                    this.ConnectionStringField = value;
                    this.RaisePropertyChanged("ConnectionString");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProviderName {
            get {
                return this.ProviderNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ProviderNameField, value) != true)) {
                    this.ProviderNameField = value;
                    this.RaisePropertyChanged("ProviderName");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RF_Resources.IRFResourceController")]
    public interface IRFResourceController {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRFResourceController/GetConnectionStringSettings", ReplyAction="http://tempuri.org/IRFResourceController/GetConnectionStringSettingsResponse")]
        TSQLExample.DAL.RF_Resources.ConnectionStringSettings GetConnectionStringSettings(string alias);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRFResourceController/GetConnectionStringSettings", ReplyAction="http://tempuri.org/IRFResourceController/GetConnectionStringSettingsResponse")]
        System.Threading.Tasks.Task<TSQLExample.DAL.RF_Resources.ConnectionStringSettings> GetConnectionStringSettingsAsync(string alias);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRFResourceControllerChannel : TSQLExample.DAL.RF_Resources.IRFResourceController, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RFResourceControllerClient : System.ServiceModel.ClientBase<TSQLExample.DAL.RF_Resources.IRFResourceController>, TSQLExample.DAL.RF_Resources.IRFResourceController {
        
        public RFResourceControllerClient() {
        }
        
        public RFResourceControllerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RFResourceControllerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RFResourceControllerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RFResourceControllerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TSQLExample.DAL.RF_Resources.ConnectionStringSettings GetConnectionStringSettings(string alias) {
            return base.Channel.GetConnectionStringSettings(alias);
        }
        
        public System.Threading.Tasks.Task<TSQLExample.DAL.RF_Resources.ConnectionStringSettings> GetConnectionStringSettingsAsync(string alias) {
            return base.Channel.GetConnectionStringSettingsAsync(alias);
        }
    }
}
