using System;
using System.Collections.Generic;

namespace Game.Application.Persistence
{
    [Serializable]
    public class SavesMetaData
    {
        public List<SaveReferenceData> Saves { get; set; } = new();
    }
}