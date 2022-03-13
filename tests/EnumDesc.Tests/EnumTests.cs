using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumDesc.Tests
{
    [TestClass]
    public class EnumTests
    {
        [TestMethod]
        public void EveryMemberHasDesc_GetDescriptionList_Ok()
        {
            var res = Enum<EveryMemberHasDesc>.GetDescriptionList();
            Assert.IsNotNull(res);

            var list = new List<(string desc, EveryMemberHasDesc value)>()
            {
                (EveryMemberHasDesc.Blue_Desc.GetDescription(), EveryMemberHasDesc.Blue_Desc),
                (EveryMemberHasDesc.Purple_Desc.GetDescription(), EveryMemberHasDesc.Purple_Desc),
                (EveryMemberHasDesc.Red_Desc.GetDescription(), EveryMemberHasDesc.Red_Desc),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }

        [TestMethod]
        public void OnlyOneMemberHasDesc_GetDescriptionList_Ok()
        {
            var res = Enum<OnlyOneMemberHasDesc>.GetDescriptionList();
            Assert.IsNotNull(res);

            var list = new List<(string desc, OnlyOneMemberHasDesc value)>()
            {
                (nameof(OnlyOneMemberHasDesc.Blue_NoDesc), OnlyOneMemberHasDesc.Blue_NoDesc),
                (OnlyOneMemberHasDesc.Purple_Desc.GetDescription(), OnlyOneMemberHasDesc.Purple_Desc),
                (nameof(OnlyOneMemberHasDesc.Red_NoDesc), OnlyOneMemberHasDesc.Red_NoDesc),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }

        [TestMethod]
        public void NoMemberHasDesc_GetDescriptionList_ThrowNotSupported()
        {
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                var res = Enum<NoMemberHasDesc>.GetDescriptionList();
            });
        }

        [TestMethod]
        public void EveryMemberHasDesc_GetDescriptionDic_Ok()
        {
            var res = Enum<EveryMemberHasDesc>.GetDescriptionDic();
            Assert.IsNotNull(res);

            var list = new Dictionary<EveryMemberHasDesc, string>()
            {
                [EveryMemberHasDesc.Blue_Desc] = EveryMemberHasDesc.Blue_Desc.GetDescription(),
                [EveryMemberHasDesc.Purple_Desc] = EveryMemberHasDesc.Purple_Desc.GetDescription(),
                [EveryMemberHasDesc.Red_Desc] = EveryMemberHasDesc.Red_Desc.GetDescription(),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }

        [TestMethod]
        public void OnlyOneMemberHasDesc_GetDescriptionDic_Ok()
        {
            var res = Enum<OnlyOneMemberHasDesc>.GetDescriptionDic();
            Assert.IsNotNull(res);

            var list = new Dictionary<OnlyOneMemberHasDesc, string>()
            {
                [OnlyOneMemberHasDesc.Blue_NoDesc] = nameof(OnlyOneMemberHasDesc.Blue_NoDesc),
                [OnlyOneMemberHasDesc.Purple_Desc] = OnlyOneMemberHasDesc.Purple_Desc.GetDescription(),
                [OnlyOneMemberHasDesc.Red_NoDesc] = nameof(OnlyOneMemberHasDesc.Red_NoDesc),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }

        [TestMethod]
        public void NoMemberHasDesc_GetDescriptionDic_ThrowNotSupported()
        {
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                var res = Enum<NoMemberHasDesc>.GetDescriptionDic();
            });
        }
    }
}
