using System.Collections.Generic;
using System.Linq;
using Moq;
using Neo.ClrCounters.Exceptions;
using NUnit.Framework;

namespace Neo.ClrCounters.Tests.Exceptions
{
    public class ExceptionEventProcessorTests
    {
        [Test]
        public void Process()
        {
            var handlerMocks = new List<Mock<IExceptionEventHandler>> {new(), new(), new()};
            var mapperMock = new Mock<IExceptionArgsMapper>();

            var processor = new ExceptionEventProcessor(handlerMocks.Select(x => x.Object), mapperMock.Object);
            processor.Process(null);

            foreach (var handlerMock in handlerMocks)
                handlerMock.Verify(x => x.Handle(It.IsAny<ExceptionArgs>()), Times.Once);
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        public void Enabled(int count, bool enabled)
        {
            var processor = new ExceptionEventProcessor(Enumerable.Repeat(new Mock<IExceptionEventHandler>().Object, count), null);
            Assert.AreEqual(enabled, processor.Enabled);
        }
    }
}