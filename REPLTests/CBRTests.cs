using Microsoft.VisualStudio.TestTools.UnitTesting;
using REPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPL.Tests
{
    [TestClass()]
    public class CBRTests
    {
        [TestMethod()]
        public void AddConversationTest()
        {
            var cbr = new CBR();

            Func<string, string[]> splitter = s => s.Split(null);

            cbr.AddConversation("What is your name", "필떵ㅎ", splitter);
            cbr.AddConversation("That is pretty name", "Thank you!", splitter);
            cbr.AddConversation("What are you doing now?", "Well..", splitter);

            var what = cbr._corpuses[0];
            Assert.AreEqual(what.Word, "What");
            Assert.AreEqual(1 / 4f, what._sentenceIdx[0]);
        }
    }
}