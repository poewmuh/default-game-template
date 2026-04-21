using DG.Tweening;
using MessagePipe;
using R3;
using UnityEngine;
using VContainer;

namespace Game.Overlays.BlinkEffect
{
    public class BlinkOverlay : BaseOverlay
    {
        [SerializeField] private RectTransform _topBar;
        [SerializeField] private RectTransform _bottomBar;
        
        [SerializeField] private float _duration = 0.5f;
        
        [Inject] private ISubscriber<BlinkMessageEvent> _blinkMessageSub;
        
        private float _halfScreenHeight;
        
        private void Awake()
        {
            _blinkMessageSub.Subscribe(OnBlinkRequest).AddTo(this);

            _halfScreenHeight = Screen.height * .5f;
        }

        private async void OnBlinkRequest(BlinkMessageEvent msg)
        {
            if (msg.Type == FadeType.FadeIn)
                await Close().AsyncWaitForCompletion();
            else
                await Open().AsyncWaitForCompletion();
            
            msg.Completion?.TrySetResult();
        }
        
        private Sequence Close()
        {
            return DOTween.Sequence()
                .Join(AnimateBar(_topBar, _halfScreenHeight))
                .Join(AnimateBar(_bottomBar, _halfScreenHeight));
        }

        private Sequence Open()
        {
            return DOTween.Sequence()
                .Join(AnimateBar(_topBar, 0))
                .Join(AnimateBar(_bottomBar, 0));
        }
        
        private Tweener AnimateBar(RectTransform bar, float targetHeight)
        {
            return DOTween.To(
                () => bar.sizeDelta.y,
                y =>
                {
                    var size = bar.sizeDelta;
                    size.y = y;
                    bar.sizeDelta = size;
                },
                targetHeight,
                _duration
            ).SetEase(Ease.InOutCubic);
        }
    }
}