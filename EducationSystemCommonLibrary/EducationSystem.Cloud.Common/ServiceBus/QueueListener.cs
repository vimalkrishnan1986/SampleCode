using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.ServiceBus;
using System.Threading;
using System.Text;
using Newtonsoft.Json;

namespace EducationSystem.Cloud.Common.ServiceBus
{
    public sealed class QueueListener<T> : IQueueListener<T>
    {
        private readonly List<IProcessor<T>> _processors;
        private readonly QueueClient _queueClient;
        private bool _isStarted;

        public QueueListener(ServiceBusSetting serviceBusSetting, string queueName)
        {
            if (serviceBusSetting == null)
            {
                throw new ArgumentNullException(nameof(serviceBusSetting));
            }
            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new ArgumentNullException(nameof(queueName));
            }
            _isStarted = false;
            _processors = new List<IProcessor<T>>();
            _queueClient = new QueueClient(serviceBusSetting.ConnectionString, queueName, ReceiveMode.PeekLock, RetryPolicy.Default);
        }

        public async Task Recieve(Message message, CancellationToken token)
        {
            try
            {
                var messageBody = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
                if (messageBody == null)
                {
                    return;
                }
                foreach (var processor in _processors)
                {
                    await processor.Process(messageBody);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task Start()
        {
            if (_isStarted)
            {
                throw new InvalidOperationException();
            }

            if (_processors.Count == 0)
            {
                throw new IndexOutOfRangeException("Processors list is empty");
            }

            _queueClient.RegisterMessageHandler(async (message, token) =>
            {
                await Recieve(message, token);
            }, new MessageHandlerOptions(async args => Console.WriteLine(args.Exception))
            { MaxConcurrentCalls = 1, AutoComplete = false });

            _isStarted = true;
        }

        public async Task Stop()
        {
            await _queueClient.CloseAsync();
        }

        public async Task Subscribe(IProcessor<T> processor)
        {
            await Task.Factory.StartNew(() => _processors.Add(processor));
        }
    }
}


