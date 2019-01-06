using BotCore.Model.Entities;
using System;

namespace BotCore.Interfaces.Repository
{
    public interface ILoveRepository: IDisposable
    {
        LovePhrases GetRandomLovePhrase();
    }
}
