using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ClashRoyale
{
    public class AvailableDeckUI : MonoBehaviour
    {
        [SerializeField] private CardSelector _selector;
        [SerializeField] private List<AvailableCardUI> _availableCardUI = new List<AvailableCardUI>();

#if UNITY_EDITOR
        [SerializeField] private Transform _availableCardParent;
        [SerializeField] private AvailableCardUI _availableCardUIprefab;

        public void SetAllCardsCount(Card[] cards)
        {
            for (int i = 0; i < _availableCardUI.Count; i++)
            {
                var go = _availableCardUI[i].gameObject;
                EditorApplication.delayCall+= () => DestroyImmediate(go);
            }
            _availableCardUI.Clear();

            for (int i = 1; i < cards.Length; i++)
            {
                AvailableCardUI card = Instantiate(_availableCardUIprefab, _availableCardParent);
                card.Create(_selector, cards[i], i);
                _availableCardUI.Add(card);
            }

            EditorUtility.SetDirty(this);
        }
#endif

        public void UpdateCardsList(IReadOnlyList<Card> available, IReadOnlyList<Card> selected)
        {
            for (int i = 0; i < _availableCardUI.Count; i++)
            {
                _availableCardUI[i].SetState(CardStateTypes.Locked);
            }

            for (int i = 0; i < available.Count; i++)
            {
                _availableCardUI[available[i].id - 1].SetState(CardStateTypes.Available);
            }

            for (int i = 0; i < selected.Count; i++)
            {
                _availableCardUI[selected[i].id - 1].SetState(CardStateTypes.Selected);
            }
        }
    }
}
