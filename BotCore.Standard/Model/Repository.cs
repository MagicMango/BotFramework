using System;
using BotEntitiesNETCore.Models.Entities;

namespace BotCore.Model
{
    public class Repository : IDisposable
    {
        protected StreamDBBotEntitiesCore context;

        public Repository()
        {
            context = new StreamDBBotEntitiesCore();
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
