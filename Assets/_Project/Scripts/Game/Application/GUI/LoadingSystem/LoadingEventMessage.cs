using System;

namespace Game.Application.GUI.LoadingSystem
{
    public class LoadingEventMessage
    {
        public Func<float> ProgressFunc { get; }
        
        public LoadingEventMessage(Func<float> progressFunc)
        {
            ProgressFunc = progressFunc;
        }
    }
}