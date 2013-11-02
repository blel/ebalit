﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MPSServiceLibrary.DataContracts
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Project", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    public partial class Project : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string NameField;
        
        private MPSServiceLibrary.DataContracts.Task[] TasksField;
        
        private System.Guid UniqueIdField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MPSServiceLibrary.DataContracts.Task[] Tasks
        {
            get
            {
                return this.TasksField;
            }
            set
            {
                this.TasksField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid UniqueId
        {
            get
            {
                return this.UniqueIdField;
            }
            set
            {
                this.UniqueIdField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Task", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    public partial class Task : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Nullable<double> ActualWorkField;
        
        private string NameField;
        
        private System.Nullable<System.Guid> ParentUniqueIdField;
        
        private System.Guid UniqueIdField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> ActualWork
        {
            get
            {
                return this.ActualWorkField;
            }
            set
            {
                this.ActualWorkField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> ParentUniqueId
        {
            get
            {
                return this.ParentUniqueIdField;
            }
            set
            {
                this.ParentUniqueIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid UniqueId
        {
            get
            {
                return this.UniqueIdField;
            }
            set
            {
                this.UniqueIdField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WorkingHours", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    public partial class WorkingHours : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.DateTime FromField;
        
        private int IdField;
        
        private string NotesField;
        
        private MPSServiceLibrary.DataContracts.Task TaskField;
        
        private System.DateTime ToField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime From
        {
            get
            {
                return this.FromField;
            }
            set
            {
                this.FromField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Notes
        {
            get
            {
                return this.NotesField;
            }
            set
            {
                this.NotesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MPSServiceLibrary.DataContracts.Task Task
        {
            get
            {
                return this.TaskField;
            }
            set
            {
                this.TaskField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime To
        {
            get
            {
                return this.ToField;
            }
            set
            {
                this.ToField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    public partial class DataConnectionFault : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string ErrorMessageField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessage
        {
            get
            {
                return this.ErrorMessageField;
            }
            set
            {
                this.ErrorMessageField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IWorkingHoursService")]
public interface IWorkingHoursService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/GetTasks", ReplyAction="http://tempuri.org/IWorkingHoursService/GetTasksResponse")]
    [System.ServiceModel.FaultContractAttribute(typeof(MPSServiceLibrary.DataContracts.DataConnectionFault), Action="http://tempuri.org/IWorkingHoursService/GetTasksDataConnectionFaultFault", Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    MPSServiceLibrary.DataContracts.Task[] GetTasks(MPSServiceLibrary.DataContracts.Project project);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/GetTasks", ReplyAction="http://tempuri.org/IWorkingHoursService/GetTasksResponse")]
    System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.Task[]> GetTasksAsync(MPSServiceLibrary.DataContracts.Project project);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/AddWorkingHours", ReplyAction="http://tempuri.org/IWorkingHoursService/AddWorkingHoursResponse")]
    [System.ServiceModel.FaultContractAttribute(typeof(MPSServiceLibrary.DataContracts.DataConnectionFault), Action="http://tempuri.org/IWorkingHoursService/AddWorkingHoursDataConnectionFaultFault", Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    void AddWorkingHours(MPSServiceLibrary.DataContracts.WorkingHours workingHours);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/AddWorkingHours", ReplyAction="http://tempuri.org/IWorkingHoursService/AddWorkingHoursResponse")]
    System.Threading.Tasks.Task AddWorkingHoursAsync(MPSServiceLibrary.DataContracts.WorkingHours workingHours);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/GetAllWorkingHours", ReplyAction="http://tempuri.org/IWorkingHoursService/GetAllWorkingHoursResponse")]
    [System.ServiceModel.FaultContractAttribute(typeof(MPSServiceLibrary.DataContracts.DataConnectionFault), Action="http://tempuri.org/IWorkingHoursService/GetAllWorkingHoursDataConnectionFaultFaul" +
        "t", Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    MPSServiceLibrary.DataContracts.WorkingHours[] GetAllWorkingHours(MPSServiceLibrary.DataContracts.Task task);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/GetAllWorkingHours", ReplyAction="http://tempuri.org/IWorkingHoursService/GetAllWorkingHoursResponse")]
    System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.WorkingHours[]> GetAllWorkingHoursAsync(MPSServiceLibrary.DataContracts.Task task);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/DeleteWorkingHours", ReplyAction="http://tempuri.org/IWorkingHoursService/DeleteWorkingHoursResponse")]
    [System.ServiceModel.FaultContractAttribute(typeof(MPSServiceLibrary.DataContracts.DataConnectionFault), Action="http://tempuri.org/IWorkingHoursService/DeleteWorkingHoursDataConnectionFaultFaul" +
        "t", Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    void DeleteWorkingHours(MPSServiceLibrary.DataContracts.WorkingHours workingHours);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/DeleteWorkingHours", ReplyAction="http://tempuri.org/IWorkingHoursService/DeleteWorkingHoursResponse")]
    System.Threading.Tasks.Task DeleteWorkingHoursAsync(MPSServiceLibrary.DataContracts.WorkingHours workingHours);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/UpdateWorkingHours", ReplyAction="http://tempuri.org/IWorkingHoursService/UpdateWorkingHoursResponse")]
    [System.ServiceModel.FaultContractAttribute(typeof(MPSServiceLibrary.DataContracts.DataConnectionFault), Action="http://tempuri.org/IWorkingHoursService/UpdateWorkingHoursDataConnectionFaultFaul" +
        "t", Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    MPSServiceLibrary.DataContracts.WorkingHours UpdateWorkingHours(MPSServiceLibrary.DataContracts.WorkingHours workingHours);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/UpdateWorkingHours", ReplyAction="http://tempuri.org/IWorkingHoursService/UpdateWorkingHoursResponse")]
    System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.WorkingHours> UpdateWorkingHoursAsync(MPSServiceLibrary.DataContracts.WorkingHours workingHours);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/GetWorkingHours", ReplyAction="http://tempuri.org/IWorkingHoursService/GetWorkingHoursResponse")]
    [System.ServiceModel.FaultContractAttribute(typeof(MPSServiceLibrary.DataContracts.DataConnectionFault), Action="http://tempuri.org/IWorkingHoursService/GetWorkingHoursDataConnectionFaultFault", Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    MPSServiceLibrary.DataContracts.WorkingHours GetWorkingHours(int id);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkingHoursService/GetWorkingHours", ReplyAction="http://tempuri.org/IWorkingHoursService/GetWorkingHoursResponse")]
    System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.WorkingHours> GetWorkingHoursAsync(int id);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IWorkingHoursServiceChannel : IWorkingHoursService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class WorkingHoursServiceClient : System.ServiceModel.ClientBase<IWorkingHoursService>, IWorkingHoursService
{
    
    public WorkingHoursServiceClient()
    {
    }
    
    public WorkingHoursServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public WorkingHoursServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public WorkingHoursServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public WorkingHoursServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public MPSServiceLibrary.DataContracts.Task[] GetTasks(MPSServiceLibrary.DataContracts.Project project)
    {
        return base.Channel.GetTasks(project);
    }
    
    public System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.Task[]> GetTasksAsync(MPSServiceLibrary.DataContracts.Project project)
    {
        return base.Channel.GetTasksAsync(project);
    }
    
    public void AddWorkingHours(MPSServiceLibrary.DataContracts.WorkingHours workingHours)
    {
        base.Channel.AddWorkingHours(workingHours);
    }
    
    public System.Threading.Tasks.Task AddWorkingHoursAsync(MPSServiceLibrary.DataContracts.WorkingHours workingHours)
    {
        return base.Channel.AddWorkingHoursAsync(workingHours);
    }
    
    public MPSServiceLibrary.DataContracts.WorkingHours[] GetAllWorkingHours(MPSServiceLibrary.DataContracts.Task task)
    {
        return base.Channel.GetAllWorkingHours(task);
    }
    
    public System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.WorkingHours[]> GetAllWorkingHoursAsync(MPSServiceLibrary.DataContracts.Task task)
    {
        return base.Channel.GetAllWorkingHoursAsync(task);
    }
    
    public void DeleteWorkingHours(MPSServiceLibrary.DataContracts.WorkingHours workingHours)
    {
        base.Channel.DeleteWorkingHours(workingHours);
    }
    
    public System.Threading.Tasks.Task DeleteWorkingHoursAsync(MPSServiceLibrary.DataContracts.WorkingHours workingHours)
    {
        return base.Channel.DeleteWorkingHoursAsync(workingHours);
    }
    
    public MPSServiceLibrary.DataContracts.WorkingHours UpdateWorkingHours(MPSServiceLibrary.DataContracts.WorkingHours workingHours)
    {
        return base.Channel.UpdateWorkingHours(workingHours);
    }
    
    public System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.WorkingHours> UpdateWorkingHoursAsync(MPSServiceLibrary.DataContracts.WorkingHours workingHours)
    {
        return base.Channel.UpdateWorkingHoursAsync(workingHours);
    }
    
    public MPSServiceLibrary.DataContracts.WorkingHours GetWorkingHours(int id)
    {
        return base.Channel.GetWorkingHours(id);
    }
    
    public System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.WorkingHours> GetWorkingHoursAsync(int id)
    {
        return base.Channel.GetWorkingHoursAsync(id);
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IProjectSyncService")]
public interface IProjectSyncService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProjectSyncService/AddProject", ReplyAction="http://tempuri.org/IProjectSyncService/AddProjectResponse")]
    [System.ServiceModel.FaultContractAttribute(typeof(MPSServiceLibrary.DataContracts.DataConnectionFault), Action="http://tempuri.org/IProjectSyncService/AddProjectDataConnectionFaultFault", Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    MPSServiceLibrary.DataContracts.Project AddProject(MPSServiceLibrary.DataContracts.Project project);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProjectSyncService/AddProject", ReplyAction="http://tempuri.org/IProjectSyncService/AddProjectResponse")]
    System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.Project> AddProjectAsync(MPSServiceLibrary.DataContracts.Project project);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProjectSyncService/SynchronizeProject", ReplyAction="http://tempuri.org/IProjectSyncService/SynchronizeProjectResponse")]
    [System.ServiceModel.FaultContractAttribute(typeof(MPSServiceLibrary.DataContracts.DataConnectionFault), Action="http://tempuri.org/IProjectSyncService/SynchronizeProjectDataConnectionFaultFault" +
        "", Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    void SynchronizeProject(MPSServiceLibrary.DataContracts.Project project);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProjectSyncService/SynchronizeProject", ReplyAction="http://tempuri.org/IProjectSyncService/SynchronizeProjectResponse")]
    System.Threading.Tasks.Task SynchronizeProjectAsync(MPSServiceLibrary.DataContracts.Project project);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProjectSyncService/GetProjects", ReplyAction="http://tempuri.org/IProjectSyncService/GetProjectsResponse")]
    [System.ServiceModel.FaultContractAttribute(typeof(MPSServiceLibrary.DataContracts.DataConnectionFault), Action="http://tempuri.org/IProjectSyncService/GetProjectsDataConnectionFaultFault", Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    MPSServiceLibrary.DataContracts.Project[] GetProjects();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProjectSyncService/GetProjects", ReplyAction="http://tempuri.org/IProjectSyncService/GetProjectsResponse")]
    System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.Project[]> GetProjectsAsync();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProjectSyncService/GetProject", ReplyAction="http://tempuri.org/IProjectSyncService/GetProjectResponse")]
    [System.ServiceModel.FaultContractAttribute(typeof(MPSServiceLibrary.DataContracts.DataConnectionFault), Action="http://tempuri.org/IProjectSyncService/GetProjectDataConnectionFaultFault", Name="DataConnectionFault", Namespace="http://schemas.datacontract.org/2004/07/MPSServiceLibrary.DataContracts")]
    MPSServiceLibrary.DataContracts.Project GetProject(int id);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProjectSyncService/GetProject", ReplyAction="http://tempuri.org/IProjectSyncService/GetProjectResponse")]
    System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.Project> GetProjectAsync(int id);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IProjectSyncServiceChannel : IProjectSyncService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class ProjectSyncServiceClient : System.ServiceModel.ClientBase<IProjectSyncService>, IProjectSyncService
{
    
    public ProjectSyncServiceClient()
    {
    }
    
    public ProjectSyncServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public ProjectSyncServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public ProjectSyncServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public ProjectSyncServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public MPSServiceLibrary.DataContracts.Project AddProject(MPSServiceLibrary.DataContracts.Project project)
    {
        return base.Channel.AddProject(project);
    }
    
    public System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.Project> AddProjectAsync(MPSServiceLibrary.DataContracts.Project project)
    {
        return base.Channel.AddProjectAsync(project);
    }
    
    public void SynchronizeProject(MPSServiceLibrary.DataContracts.Project project)
    {
        base.Channel.SynchronizeProject(project);
    }
    
    public System.Threading.Tasks.Task SynchronizeProjectAsync(MPSServiceLibrary.DataContracts.Project project)
    {
        return base.Channel.SynchronizeProjectAsync(project);
    }
    
    public MPSServiceLibrary.DataContracts.Project[] GetProjects()
    {
        return base.Channel.GetProjects();
    }
    
    public System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.Project[]> GetProjectsAsync()
    {
        return base.Channel.GetProjectsAsync();
    }
    
    public MPSServiceLibrary.DataContracts.Project GetProject(int id)
    {
        return base.Channel.GetProject(id);
    }
    
    public System.Threading.Tasks.Task<MPSServiceLibrary.DataContracts.Project> GetProjectAsync(int id)
    {
        return base.Channel.GetProjectAsync(id);
    }
}
