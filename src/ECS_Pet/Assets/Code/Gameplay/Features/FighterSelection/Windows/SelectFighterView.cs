using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Code.StaticData;
using Code.Gameplay.Fighter;
using Code.Common.Extensions;

namespace Code.Gameplay.FighterSelection
{
    public class SelectFighterView : MonoBehaviour
    {
        private static readonly Color AvailableIconColor = Color.white;
        private static readonly Color UnavailableIconColor = new(0.35f, 0.35f, 0.35f, 0.45f);

        public FighterTypeId FighterTypeId;
        
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private GameObject selectedView;
        [SerializeField] private Image fighterIcon;
        [SerializeField] private Button button;
 
        public SelectFighterView Initialize(FighterConfig config, Action<FighterTypeId> selectAction)
        {
            FighterTypeId = config.FighterTypeId;
            fighterIcon.sprite = config.Icon;
            priceText.text = config.Price.ToString();
            
            button
                .RemoveAllClickListeners()
                .AddClickListener(() => { selectAction(config.FighterTypeId); });
            
            gameObject.SetActive(true);
            return this;
        }
        
        public void MarkAsSelected(bool isSelected) => 
            selectedView.SetActive(isSelected);

        public void SetInteractable(bool isInteractable)
        {
            button.interactable = isInteractable;
            fighterIcon.color = isInteractable
                ? AvailableIconColor
                : UnavailableIconColor;

            if (isInteractable == false)
            {
                MarkAsSelected(false);
            }
        }
    }
}
