using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

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
            var factory = MessagingFactory.CreateFromConnectionString(serviceBusSetting.ConnectionString);
            _queueClient = factory.CreateQueueClient(queueName, ReceiveMode.ReceiveAndDelete);
        }

        public async Task Recieve(BrokeredMessage arg)
        {
            try
            {
                var message = arg.GetBody<T>();
                if (message == null)
                {
                    return;
                }

                foreach (var processor in _processors)
                {
                    await processor.Process(message);
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

            await Task.Factory.StartNew(() =>
          {
              _queueClient.OnMessageAsync(Recieve, new OnMessageOptions()
              {
                  AutoComplete = true,
                  MaxConcurrentCalls = 1
              }); ;
              _isStarted = true;
          });
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


