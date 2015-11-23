/***************************************************************
*   IRCClient.cs - Turns the raw output of IRCServer into
*                       more convient events.
****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRCLib
{
    class IRCRoom
    {
        string roomName = "";   // NO #
        string roomLog ="";
    }

    class IRCClient
    {
        List<string> rooms = new List<string>();

    }
}
