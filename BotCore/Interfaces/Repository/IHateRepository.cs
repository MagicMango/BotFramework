using BotCore.Model.Entities;
using System;

namespace BotCore.Interfaces.Repository
{
    public interface IHateRepository: IDisposable, IBase
    {
        /// <summary>
        /// Will Return a Random hate Phrase for further process the Phrase will include {0} {1} for String Format function.
        /// <see cref="String.Format(string, string, string)"/>
        /// </summary>
        /// <returns></returns>
        HatePhrases GetRandomHatePhrase();
    }
}
