﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordStats
{
    public interface DatabaseServer
    {
        void UpdateData(Config config, int aantalOnline);
    }
}
