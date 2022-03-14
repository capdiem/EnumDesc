using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace EnumDesc.Tests
{
    public partial class EnumTests
    {
        [TestMethod]
        public void EveryMemberHasDesc_GetDescriptionList_WithAll()
        {
            var res = Enum<EveryMemberHasDesc>.GetDescriptionList<EveryMemberHasDesc?>(withAll: true);
            Assert.IsNotNull(res);

            var list = new List<(EveryMemberHasDesc? value, string desc)>()
            {
                (null, "All"),
                ((EveryMemberHasDesc?)EveryMemberHasDesc.Blue_Desc, EveryMemberHasDesc.Blue_Desc.GetDescription()),
                ((EveryMemberHasDesc?)EveryMemberHasDesc.Purple_Desc, EveryMemberHasDesc.Purple_Desc.GetDescription()),
                ((EveryMemberHasDesc?)EveryMemberHasDesc.Red_Desc, EveryMemberHasDesc.Red_Desc.GetDescription()),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }

        [TestMethod]
        public void EveryMemberHasDesc_GetDescriptionList_WithAll_AllDesc()
        {
            var res = Enum<EveryMemberHasDesc>.GetDescriptionList<EveryMemberHasDesc?>(withAll: true, allDesc: "所有");
            Assert.IsNotNull(res);

            var list = new List<(EveryMemberHasDesc? value, string desc)>()
            {
                (null, "所有"),
                ((EveryMemberHasDesc?)EveryMemberHasDesc.Blue_Desc, EveryMemberHasDesc.Blue_Desc.GetDescription()),
                ((EveryMemberHasDesc?)EveryMemberHasDesc.Purple_Desc, EveryMemberHasDesc.Purple_Desc.GetDescription()),
                ((EveryMemberHasDesc?)EveryMemberHasDesc.Red_Desc, EveryMemberHasDesc.Red_Desc.GetDescription()),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }


        [TestMethod]
        public void EveryMemberHasDesc_GetDescriptionList_WithAll_AllDesc_AllValue()
        {
            var res = Enum<EveryMemberHasDesc>.GetDescriptionList<int>(withAll: true, allDesc: "所有", allValue: -100);
            Assert.IsNotNull(res);

            var list = new List<(int value, string desc)>()
            {
                (-100, "所有"),
                ((int)EveryMemberHasDesc.Blue_Desc, EveryMemberHasDesc.Blue_Desc.GetDescription()),
                ((int)EveryMemberHasDesc.Purple_Desc, EveryMemberHasDesc.Purple_Desc.GetDescription()),
                ((int)EveryMemberHasDesc.Red_Desc, EveryMemberHasDesc.Red_Desc.GetDescription()),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }

        [TestMethod]
        public void EveryMemberHasDesc_GetDescriptionDic_WithAll()
        {
            var res = Enum<EveryMemberHasDesc>.GetDescriptionDic<int>(withAll: true, allValue: -1);
            Assert.IsNotNull(res);

            var list = new Dictionary<int, string>()
            {
                [-1] = "All",
                [(int)EveryMemberHasDesc.Blue_Desc] = EveryMemberHasDesc.Blue_Desc.GetDescription(),
                [(int)EveryMemberHasDesc.Purple_Desc] = EveryMemberHasDesc.Purple_Desc.GetDescription(),
                [(int)EveryMemberHasDesc.Red_Desc] = EveryMemberHasDesc.Red_Desc.GetDescription(),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }

        [TestMethod]
        public void EveryMemberHasDesc_GetDescriptionDic_WithAll_AllDesc()
        {
            var res = Enum<EveryMemberHasDesc>.GetDescriptionDic<int>(withAll: true, allDesc: "所有", allValue: -1);
            Assert.IsNotNull(res);

            var list = new Dictionary<int, string>()
            {
                [-1] = "所有",
                [(int)EveryMemberHasDesc.Blue_Desc] = EveryMemberHasDesc.Blue_Desc.GetDescription(),
                [(int)EveryMemberHasDesc.Purple_Desc] = EveryMemberHasDesc.Purple_Desc.GetDescription(),
                [(int)EveryMemberHasDesc.Red_Desc] = EveryMemberHasDesc.Red_Desc.GetDescription(),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }
    }
}
