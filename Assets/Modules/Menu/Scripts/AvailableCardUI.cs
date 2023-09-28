using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ClashRoyale
{
    public class AvailableCardUI : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;

        public void Init(Card card)
        {
            _image.sprite = card.sprite;
            _text.text = card.name;

            EditorUtility.SetDirty(this);
        }
#endif
    }
}
