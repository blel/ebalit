using System;
using System.Configuration;


namespace EbalitWebForms.Common
{
    /// <summary>
    /// App.Config extension for ebalit
    /// </summary>
    public class EbalitConfig:ConfigurationSection
    {
        /// <summary>
        /// Config file property to store whether resources shall be marked with IsDeleted property on mps server
        /// when they are removed from ms project
        /// </summary>
        [ConfigurationProperty ("DeleteResourcesRemovedFromMsProject", DefaultValue=false, IsRequired=false)]
        public Boolean DeleteResourcesRemovedFromMsProject
        {
            get
            {
                return (Boolean) this["DeleteResourcesRemovedFromMsProject"];
            }
            set
            {
                this["DeleteResourcesRemovedFromMsProject"] = value;
            }
        }

        [ConfigurationProperty("DeleteTasksRemovedFromMsProject", DefaultValue = false, IsRequired = false)]
        public Boolean DeleteTasksRemovedFromMsProject
        {
            get
            {
                return (Boolean) this["DeleteTasksRemovedFromMsProject"];
            }

            set
            {
                this["DeleteTasksRemovedFromMsProject"] = value;
            }
        }
    }
}