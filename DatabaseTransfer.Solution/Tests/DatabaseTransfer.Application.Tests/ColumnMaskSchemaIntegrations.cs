using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseTransfer.Application.Tests
{
    /// <summary>
    ///     No longer leveraged / leveraging GlobalShopDataTypeExtensions(gsseo)
    /// </summary>
    [TestClass]
    public class ColumnMaskSchemaIntegrations
    {
        [TestMethod]
        public void DateTime_MMDDYY_Pass()
        {
            // inventory_mstr -> DATE_LAST_AUDIT (MMDDYY)

            var stringDate = "032309";
            var stringDateMask = "MMddy";

            if (DateTime.TryParseExact(stringDate, stringDateMask, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateParseResult))
            {
                Assert.IsNotNull(dateParseResult);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DateTime_YYMMDD_Pass()
        {
            // inventory_mstr -> DATE_LAST_CHG (YYMMDD)

            var stringDate = "960920";
            var stringDateMask = "yMMdd";

            if (DateTime.TryParseExact(stringDate, stringDateMask, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateParseResult))
            {
                Assert.IsNotNull(dateParseResult);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}