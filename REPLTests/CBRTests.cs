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
            Func<string, string[]> splitter = s => s.Split(null);

            var cbr = new CBR(splitter);

            cbr.AddConversation("what your name", "필떵ㅎ");
            cbr.AddConversation("that pretty name", "Thank you!");
            cbr.AddConversation("what you doing now", "Well..");

            var what = cbr._corpuses[0];
            Assert.AreEqual(what.Word, "what");
            Assert.AreEqual(3, what._sentenceIdx[0].Count);
        }

        [TestMethod()]
        public void GetAnswerTest()
        {
            Func<string, string[]> splitter = s => s.Split(null);

            var cbr = new CBR(splitter);

            string ans1 = "필떵ㅎ", ans2 = "땡큐!", ans3 = "글세..";

            cbr.AddConversation("what your name", ans1);
            cbr.AddConversation("that pretty name", ans2);
            cbr.AddConversation("what you doing now", ans3);

            Assert.AreEqual(ans2, cbr.GetAnswer("what pretty name"));
        }
    }
}