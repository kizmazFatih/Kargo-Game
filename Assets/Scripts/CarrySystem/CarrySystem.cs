using System.Collections.Generic; // Liste kullanabilmek için
using UnityEngine;

public class CarrySystem : MonoBehaviour
{
    public Transform targetPosition; // Kutu konumunun alınacağı boş obje
    private List<GameObject> carriedBoxes = new List<GameObject>(); // Taşınan kutuların listesi

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<Usebul>() != null)
        {
            other.gameObject.GetComponent<Usebul>().ShowMyProgressBar();
        }


        if (other.CompareTag("Box")) // Kutuya temas
        {
            // Eğer bu kutu daha önce alınmamışsa
            if (!carriedBoxes.Contains(other.gameObject))
            {
                carriedBoxes.Add(other.gameObject); // Kutuyu listeye ekle
                PositionBoxes(); // Kutuların pozisyonlarını ayarla
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Usebul>() != null)
        {
            other.gameObject.GetComponent<Usebul>().ResetProgressBarValue();
        }
    }



    private void PositionBoxes()
    {

        // İlk kutuyu hedef pozisyona yerleştir
        GameObject currentBox = carriedBoxes[carriedBoxes.Count - 1];
        currentBox.transform.SetParent(targetPosition); // İlk kutuyu Target objesine child yap
        currentBox.transform.localPosition = Vector3.zero + new Vector3(0, carriedBoxes.Count - 1, 0); // İlk kutuyu hedefin konumuna yerleştir
        currentBox.transform.rotation = targetPosition.rotation; // İlk kutunun açısını ayarla







    }
}
