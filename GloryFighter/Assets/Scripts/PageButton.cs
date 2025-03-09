using UnityEngine;
using DG.Tweening; // DOTween namespace'ini ekleyin

public class PageButton : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform iconRT;
    [SerializeField] private GameObject label;

    [Header("Settings")]
    [SerializeField] private float iconMoveDistance = 50f; 
    [SerializeField] private float animationDuration = 0.3f;

    private float iconInitialY;

    private void Awake()
    {
      
        iconInitialY = iconRT.anchoredPosition.y;

      
        label.SetActive(false);
    }

    public void Select()
    {
 
        label.SetActive(true);


        iconRT.DOKill();

        iconRT.DOAnchorPosY(iconInitialY + iconMoveDistance, animationDuration)
            .SetEase(Ease.OutQuad); 
    }

    public void DeSelect()
    {
        label.SetActive(false);

        iconRT.DOKill();

        iconRT.DOAnchorPosY(iconInitialY, animationDuration)
            .SetEase(Ease.OutQuad);
    }
}