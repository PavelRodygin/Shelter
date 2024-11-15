using System;
using System.Collections.Generic;

namespace CodeBase.Core.Systems.DataPersistenceSystem
{
    [Serializable]
    public class GameData
    {
        public bool isVolumeOn;
        public List<int> favoriteAddonsID;
        
        public GameData()   
        {
            isVolumeOn = true;
            favoriteAddonsID = new List<int>();
        }
    }
}