using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
