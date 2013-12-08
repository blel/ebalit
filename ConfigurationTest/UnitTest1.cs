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

        public class AnotherSection : IConfigurationSection
        {
            public string Property1 { get; set; }
            public string Property2 { get; set; }
        }

        [TestMethod]
        public void TestConfiguration()
        {
            var section = new SampleSection
            {
                DeleteEnabled = true

            };

            var config = ConfigurationManager.GetManager().CurrentConfig;

            var q = config.GetConfigurationSection<SampleSection>();

            config.AddConfigurationSection(section);

            ConfigurationManager.GetManager().SaveConfig();
        }

        [TestMethod]
        public void TestAdditionalSection()
        {
            var newSection = new AnotherSection
            {
                Property1 = "adkjdkfajs",
                Property2 = "Balmer"

            };
            var curr = ConfigurationManager.GetManager().CurrentConfig.GetConfigurationSection<AnotherSection>();
            ConfigurationManager.GetManager().CurrentConfig.AddConfigurationSection(newSection);
            ConfigurationManager.GetManager().SaveConfig();
        }

    }
}
