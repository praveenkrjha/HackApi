using RabbitMQ.Client;
using System;
using System.Text;

namespace JDA.Common
{
    //interface ILogger
    //{
    //    void Write(string entry);
    //}
    public class RabbitLogger : IDisposable
    {
        readonly IModel _model;
        private readonly string _exchangeName;
        private readonly string _routingKey;

        public RabbitLogger(IModel model, string exchangeName, string routingKey)
        {
            _model = model;
            _exchangeName = exchangeName;
            _routingKey = routingKey;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_model != null && _model.IsOpen)
                {
                    _model.Close();
                }
            }
        }

        public void Write(string entry)
        {
            byte[] message = Encoding.UTF8.GetBytes(entry);
            _model.BasicPublish(_exchangeName, _routingKey, null, message);
        }

        ~RabbitLogger()
        {
            Dispose(false);
        }

    }
}
