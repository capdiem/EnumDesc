using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace EnumDesc.Tests
{
    public partial class EnumTests
    {
        [TestMethod]
        public void IntEnum_GetDescriptionList_TValue_int_Ok()
        {
            var res = Enum<EveryMemberHasDesc>.GetDescriptionList<int>();
            Assert.IsNotNull(res);

            var list = new List<(int value, string desc)>()
            {
                ((int)EveryMemberHasDesc.Blue_Desc, EveryMemberHasDesc.Blue_Desc.GetDescription()),
                ((int)EveryMemberHasDesc.Purple_Desc, EveryMemberHasDesc.Purple_Desc.GetDescription()),
                ((int)EveryMemberHasDesc.Red_Desc, EveryMemberHasDesc.Red_Desc.GetDescription()),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }

        [TestMethod]
        public void IntEnum_GetDescriptionList_TValue_Nullable_int_Ok()
        {
            var res = Enum<EveryMemberHasDesc>.GetDescriptionList<int?>();
            Assert.IsNotNull(res);

            var list = new List<(int? value, string desc)>()
            {
                ((int?)EveryMemberHasDesc.Blue_Desc, EveryMemberHasDesc.Blue_Desc.GetDescription()),
                ((int?)EveryMemberHasDesc.Purple_Desc, EveryMemberHasDesc.Purple_Desc.GetDescription()),
                ((int?)EveryMemberHasDesc.Red_Desc, EveryMemberHasDesc.Red_Desc.GetDescription()),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }

        [TestMethod]
        public void ShortEnum_GetDescriptionList_TValue_short_Ok()
        {
            var res = Enum<ShortEnum>.GetDescriptionList<short>();
            Assert.IsNotNull(res);

            var list = new List<(short value, string desc)>()
            {
                ((short)ShortEnum.Blue_Desc, ShortEnum.Blue_Desc.GetDescription()),
                ((short)ShortEnum.Purple_Desc, ShortEnum.Purple_Desc.GetDescription()),
                ((short)ShortEnum.Red_Desc, ShortEnum.Red_Desc.GetDescription()),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }

        [TestMethod]
        public void ShortEnum_GetDescriptionList_TValue_Nullable_short_Ok()
        {
            var res = Enum<ShortEnum>.GetDescriptionList<short?>();
            Assert.IsNotNull(res);

            var list = new List<(short? value, string desc)>()
            {
                ((short?)ShortEnum.Blue_Desc, ShortEnum.Blue_Desc.GetDescription()),
                ((short?)ShortEnum.Purple_Desc, ShortEnum.Purple_Desc.GetDescription()),
                ((short?)ShortEnum.Red_Desc, ShortEnum.Red_Desc.GetDescription()),
            };

            Assert.IsTrue(Enumerable.SequenceEqual(res, list));
        }
    }
}
