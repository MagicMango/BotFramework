using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCore.Interfaces
{
    public interface IControlLight: IBase
    {
        string ControlLights(string color, string mode);
    }
}
