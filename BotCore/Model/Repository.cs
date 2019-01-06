using BotCore.Model.Entities;
using System;

namespace BotCore.Model
{
    public class Repository : IDisposable
    {
        protected StreamDBBotEntities context;

        public Repository()
        {
            context = new StreamDBBotEntities();
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
