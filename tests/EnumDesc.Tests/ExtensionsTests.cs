using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumDesc.Tests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void CheckEveryMemberHasDesc()
        {
            Assert.AreEqual("蓝色", EveryMemberHasDesc.Blue_Desc.GetDescription());
            Assert.AreEqual("紫色", EveryMemberHasDesc.Purple_Desc.GetDescription());
            Assert.AreEqual("红色", EveryMemberHasDesc.Red_Desc.GetDescription());
        }

        [TestMethod]
        public void CheckOnlyOneMemberHasDesc()
        {
            Assert.AreNotEqual("蓝色", OnlyOneMemberHasDesc.Blue_NoDesc.GetDescription());
            Assert.AreEqual(nameof(OnlyOneMemberHasDesc.Blue_NoDesc), OnlyOneMemberHasDesc.Blue_NoDesc.GetDescription());

            Assert.AreEqual("紫色", OnlyOneMemberHasDesc.Purple_Desc.GetDescription());

            Assert.AreNotEqual("红色", OnlyOneMemberHasDesc.Red_NoDesc.GetDescription());
            Assert.AreEqual(nameof(OnlyOneMemberHasDesc.Red_NoDesc), OnlyOneMemberHasDesc.Red_NoDesc.GetDescription());
        }

        [TestMethod]
        public void CheckNoMemberHasDesc()
        {
            // No [Description] enum would not generate extension class.

            var allTypes = typeof(NoMemberHasDesc).Assembly.GetTypes();

            Assert.IsTrue(allTypes.Any(type => type.Name.Contains($"{nameof(EveryMemberHasDesc)}Extensions")));
            Assert.IsTrue(allTypes.Any(type => type.Name.Contains($"{nameof(OnlyOneMemberHasDesc)}Extensions")));
            Assert.IsFalse(allTypes.Any(type => type.Name.Contains($"{nameof(NoMemberHasDesc)}Extensions")));
        }
    }
}