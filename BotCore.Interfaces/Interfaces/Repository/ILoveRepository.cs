using System;

namespace BotCore.Interfaces.Repository
{
    public interface ILoveRepository: IDisposable, IBase
    {
        /// <summary>
        /// Will Return a Random Love Phrase for further process the Phrase will include {0} {1} for String Format function.
        /// <see cref="String.Format(string, string, string)"/>
        /// </summary>
        /// <returns></returns>
        ILovePhrase GetRandomLovePhrase();
        string GetRandomLovePhrase(string username, string[] loveSeeker);
    }
}
