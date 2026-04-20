using System;

namespace Game.Application.Persistence
{
    [Serializable]
    public class SaveReferenceData
    {
        public string Filename { get; set; }
        public string DisplayName { get; set; }
        public DateTime LastSaveDateTime { get; set; }
    }
}