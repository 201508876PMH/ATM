using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ATM.Test.Unit
{
    [TestFixture]
    class DecoderTest
    {
        private Decoder _uut;

        [SetUp]
        public void setUp()
        {
            /*
             * We dont create a mock of our Decoder class, as it is the class from which we test from
             */

            //if we test multiple classes in conjunction, we add mocks here:
            //_idoor = Substitute.For<IDoor>();

            //Constructor injection
            //_uut = new DoorControl(_userValidation, _alarm, _idoor, _entryNot);


            
        }

       

    }
}
