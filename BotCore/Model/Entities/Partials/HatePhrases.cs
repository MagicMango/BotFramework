using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCore.Model.Entities
{
    public partial class HatePhrases
    {
        public HatePhrases()
        {
            Phrase = "There are no entries in db for {0} and {1}.";
        }
    }
}
