using BotCore.Model.Entities;
using System;

namespace BotCore.Interfaces.Repository
{
    public interface IHateRepository: IDisposable, IBase
    {
        HatePhrases GetRandomHatePhrase();
    }
}
