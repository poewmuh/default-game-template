using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Overlays;
using Game.Overlays.BlinkEffect;
using MessagePipe;

namespace Game.Utils.Extensions
{
    public static class UniTaskExtensions
    {
        public static UniTask Blink(this IPublisher<BlinkMessageEvent> pub, FadeType type, CancellationToken token = default)
        {
            var completion = new UniTaskCompletionSource();
            pub.Publish(new BlinkMessageEvent(type, completion));
            return completion.Task.AttachExternalCancellation(token);
        }
    }
}