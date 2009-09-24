using NUnit.Framework;

using StarterProject.Core;

namespace StarterProject.Tests
{
    [TestFixture]
    public class When_asked_to_speak
    {
        [Test]
        public void should_say_hello_world()
        {
            var sut = new HelloWorld();
            var results = sut.Speak();
            Assert.AreEqual("Hello, World!", results);            
        }
        

    }
}
