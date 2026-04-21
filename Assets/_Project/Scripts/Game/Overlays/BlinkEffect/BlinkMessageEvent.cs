using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;

namespace Game.Overlays.BlinkEffect
{
    public class BlinkMessageEvent
    {
        public readonly FadeType Type;
        public readonly UniTaskCompletionSource Completion;
        
        public BlinkMessageEvent(FadeType type, UniTaskCompletionSource completion = null)
        {
            Type = type;
            Completion = completion;
        }
    }
}