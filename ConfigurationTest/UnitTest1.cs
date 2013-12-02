using System;
using Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigurationTest
{
    [TestClass]
    public class UnitTest1
    {
        public class SampleConfiguration:ConfigurationSection<SampleConfiguration>
        {
            public string NameIt { get; set; }
            public string Address { get; set; }
        }

        [TestMethod]
        public void TestConfiguration()
        {
            var config = new SampleConfiguration();
            config.Address = "Hello";
            config.NameIt = "darling";

            var manager = ConfigurationManager.GetManager();
            manager.SaveSection(config);

            var q = manager.ReadSection<SampleConfiguration>();
        }
    }
}
