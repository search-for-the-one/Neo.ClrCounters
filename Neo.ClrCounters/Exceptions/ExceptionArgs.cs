using System;

namespace Neo.ClrCounters.Exceptions
{
    public class ExceptionArgs
    {
        public ExceptionArgs(DateTime timeStamp, int processId, string typeName, string message)
        {
            TimeStamp = timeStamp;
            ProcessId = processId;
            TypeName = typeName;
            Message = message;
        }

        public string TypeName { get; }

        public string Message { get; }
        public DateTime TimeStamp { get; }

        public int ProcessId { get; }
    }
}