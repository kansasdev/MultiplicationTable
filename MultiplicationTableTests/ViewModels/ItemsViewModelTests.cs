using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiplicationTable.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplicationTable.ViewModels.Tests
{
    [TestClass()]
    public class ItemsViewModelTests
    {
        [TestMethod()]
        public void SetSquaresTest()
        {
            ItemsViewModel ivm = new ItemsViewModel();
            ivm.SetSquares(19, 11, MathOperation.DODAWANIE);
            Assert.Fail();
        }
    }
}