using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ClashRoyale
{
    public class AvailableCardUI : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;
        [SerializeField] private Color _availableColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _lockedColor;
        [SerializeField] private int _id;

        private CardSelector _selector;
        private CardStateTypes _currentState = CardStateTypes.None;

        public void SetState(CardStateTypes cardState)
        {
            _currentState = cardState;
            switch (cardState)
            {
                case CardStateTypes.None:
                    break;
                case CardStateTypes.Available:
                    _text.color = _availableColor;
                    break;
                case CardStateTypes.Selected:
                    _text.color = _selectedColor;
                    break;
                case CardStateTypes.Locked:
                    _text.color = _lockedColor;
                    break;
                default:
                    break;
            }
        }

        public void Click()
        {
            switch (_currentState)
            {               
                case CardStateTypes.Available:
                    _selector.SelectCard(_id);
                    SetState(CardStateTypes.Selected);
                    break;
                case CardStateTypes.Selected:
                    break;
                case CardStateTypes.Locked:
                    break;
                default:
                    break;
            }
        }

#if UNITY_EDITOR

        public void Create(CardSelector selector, Card card, int id)
        {
            _selector = selector;
            _id = id;
            _image.sprite = card.sprite;
            _text.text = card.name;

            EditorUtility.SetDirty(this);
        }
#endif
    }
}
