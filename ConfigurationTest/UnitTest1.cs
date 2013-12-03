using System;
using Configuration;
using Configuration.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigurationTest
{
    [TestClass]
    public class UnitTest1
    {
        public class SampleSection:IConfigurationSection
        {
           
            public bool DeleteEnabled { get; set; }
        }

        [TestMethod]
        public void TestConfiguration()
        {
            var section = new SampleSection
            {
                DeleteEnabled = true

            };

            var config = ConfigurationManager.GetManager().LoadConfiguration();


            config.AddConfigurationSection(section);

            ConfigurationManager.GetManager().SaveConfiguration();



            
        }
    }
}
