using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class Usebul : MonoBehaviour, IUsebul
{
    [SerializeField] private Image image;
    [SerializeField] private float cooldown;
    private Tween fillTween;



    public void ShowMyProgressBar()
    {
        image.gameObject.SetActive(true);

        // Eğer bir animasyon zaten çalışıyorsa önce onu iptal eder
        if (fillTween != null && fillTween.IsActive())
        {
            fillTween.Kill();
        }
        // Yeni animasyonu başlat ve Tween referansını sakla
        fillTween = image.DOFillAmount(1f, cooldown).SetEase(Ease.Linear).OnComplete(() => MyJob());

    }



    public void ResetProgressBarValue()
    {
        if (fillTween != null)
        {
            fillTween.Kill();
            fillTween = null; // Referansı sıfırlayarak yeniden başlatılabilir hale getirir
        }
        image.fillAmount = 0;
        image.gameObject.SetActive(false);
    }

    void MyJob()
    {
        PawnSwitcher.instance.CarActive();
    }
}
