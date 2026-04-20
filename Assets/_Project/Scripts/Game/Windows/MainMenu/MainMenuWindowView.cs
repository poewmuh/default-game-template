using R3;
using UnityEngine;
using UnityEngine.UI;
using Utils.Buttons;

namespace Game.Windows.MainMenu
{
    public class MainMenuWindowView : MonoBehaviour
    {
        [SerializeField] private CustomButton _playButton;
        [SerializeField] private CustomButton _quitButton;
        
        public Observable<Unit> OnPlayButtonClicked => _playButton.OnClick;
        public Observable<Unit> OnQuitButtonClicked => _quitButton.OnClick;
    }
}