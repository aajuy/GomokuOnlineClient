using DG.Tweening;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public PanelHandler popupWindow;

    public void OnButtonClick()
    {
        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(0.95f, 0.1f));
        seq.Append(transform.DOScale(1.05f, 0.1f));
        seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(() =>
        {
            popupWindow.Hide();
        });
    }
}