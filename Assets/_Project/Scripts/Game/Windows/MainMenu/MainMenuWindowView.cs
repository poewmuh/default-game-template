using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Windows.MainMenu
{
    public class MainMenuWindowView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;
        
        public Observable<Unit> OnPlayButtonClicked => _playButton.OnClickAsObservable();
        public Observable<Unit> OnQuitButtonClicked => _quitButton.OnClickAsObservable();
    }
}