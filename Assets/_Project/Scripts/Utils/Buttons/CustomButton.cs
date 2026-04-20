using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utils.Buttons
{
    public class CustomButton : Selectable, IPointerClickHandler, ISubmitHandler
    {
        public Subject<Unit> OnClick = new();
        
        [SerializeField] public Image _background;
        [SerializeField] private Graphic[] _coloredGraphics;

        [SerializeField] private GameObject _selectedGroup;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _selectedSprite;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _disabledColor = Color.gray5;

        private void Press()
        {
            if (!interactable) return;
            
            OnClick.OnNext(Unit.Default);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Press();
        }

        void ISubmitHandler.OnSubmit(BaseEventData eventData)
        {
            Press();
        }
        
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            if (state == SelectionState.Disabled)
            {
                SetView(false);
            }
        }
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (interactable)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
        }
        
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            SetView(true);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            SetView(false);
        }
        
        private void SetView(bool isSelected)
        {
            _selectedGroup.SetActive(isSelected);
            
            if (_background)
            {
                _background.sprite = isSelected ? _selectedSprite : _normalSprite;
            }

            foreach (var graphic in _coloredGraphics)
            {
                graphic.color = interactable ? 
                    isSelected ? _selectedColor : _normalColor :
                    _disabledColor;
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            transition = Transition.None;
        }
#endif
    }
}