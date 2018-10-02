using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.classes;
using ATM.interfaces;
using NUnit.Framework;
using NSubstitute;

namespace ATM.Test.Unit
{
    [TestFixture]
    public class ATMTest
    {
        private Analyser _uut;
        private IDecoder _decoder;

        [SetUp]
        public void Setup()
        {
            _decoder = Substitute.For<IDecoder>();
        }

        [Test]
        public void Test_FilterAircrafts()
        {
            
            
        }
    }
}