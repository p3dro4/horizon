using UnityEngine;
using System.Collections.Generic;

namespace MyAssets
{
    [System.Serializable]
    public class GameData
    {
        public List<GameObject> modules = new List<GameObject>();

        public GameData()
        {
            modules = new List<GameObject>();
            for (int i = 0; i < 9; i++)
                modules.Add(null);
        }
    }
}