using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class GameProgress
    {
        public List<string> InitialCardsList;

        public GameProgress(List<string> initialCardsList) => InitialCardsList = initialCardsList;
    }
}