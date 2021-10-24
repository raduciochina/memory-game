using System;
using System.Collections.Generic;
using System.Text;

namespace Memory_Game_App.Services
{
    public interface ILocalDataStore
    {
        void SaveSettings(string fileName, string text);
        string LoadSettings(string fileName);
    }
}
