using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace uTorrent.WebUI.Communication
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void NoParametersShouldReturnEmptyString()
        {
            var parameters = new object[] { };
            var converter = new DisplayNameConverter();
            string name = converter.Convert(parameters, null, null, null).ToString();

            Assert.AreEqual(string.Empty, name);
        }
    }
}
