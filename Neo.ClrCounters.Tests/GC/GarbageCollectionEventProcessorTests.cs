using System.Collections.Generic;
using System.Linq;
using Microsoft.Diagnostics.Tracing.Analysis.GC;
using Moq;
using Neo.ClrCounters.GC;
using NUnit.Framework;

namespace Neo.ClrCounters.Tests.GC
{
    public class GarbageCollectionEventProcessorTests
    {
        [Test]
        public void Process()
        {
            var handlerMocks = new List<Mock<IGarbageCollectionEventHandler>> {new(), new(), new()};

            var mapperMock = new Mock<IGarbageCollectionArgsMapper>();
            mapperMock.Setup(x => x.Map(It.IsAny<int>(), It.IsAny<TraceGC>())).Returns(new GarbageCollectionArgs());

            var processor = new GarbageCollectionEventProcessor(handlerMocks.Select(x => x.Object), mapperMock.Object);
            processor.Process(1, new TraceGC(12));

            foreach (var handlerMock in handlerMocks)
                handlerMock.Verify(x => x.Handle(It.IsAny<GarbageCollectionArgs>()), Times.Once);

            mapperMock.Verify(x => x.Map(It.IsAny<int>(), It.IsAny<TraceGC>()), Times.Once);
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        public void Enabled(int count, bool enabled)
        {
            var processor = new GarbageCollectionEventProcessor(Enumerable.Repeat(new Mock<IGarbageCollectionEventHandler>().Object, count), null);
            Assert.AreEqual(enabled, processor.Enabled);
        }
    }
}