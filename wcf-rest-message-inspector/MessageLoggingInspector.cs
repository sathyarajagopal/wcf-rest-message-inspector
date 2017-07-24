using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace wcf_rest_message_inspector
{
    public class MessageLoggingInspector : IDispatchMessageInspector
    {
        private static Message CopyAndLogMessage(Log log, MessageBuffer buffer, string methodName)
        {
            var message = buffer.CreateMessage();
            log.Write(message, methodName);
            return buffer.CreateMessage();
        }

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var buffer = request.CreateBufferedCopy(int.MaxValue);
            var path = Path.Combine(Path.GetTempPath(), "wcf-rest-log.txt");
            var log = new Log(path, request.Headers.To);
            request = CopyAndLogMessage(log, buffer, "AfterReceiveRequest");
            return log;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            var log = (Log)correlationState;
            reply = CopyAndLogMessage(log, reply.CreateBufferedCopy(int.MaxValue), "BeforeSendReply");
            log.Flush();
        }
    }
}