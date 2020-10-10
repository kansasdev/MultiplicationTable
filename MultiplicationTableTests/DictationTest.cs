using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiplicationTable.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplicationTableTests
{
    [TestClass()]
    public class DictationTest
    {
        [TestMethod()]
        public void CheckWordsGeneration()
        {
            SpecialWords sw = new SpecialWords("chórzysta");
            //List<string> lst = sw.GetWrongWords(0,new List<string>());
        }
    }
}
