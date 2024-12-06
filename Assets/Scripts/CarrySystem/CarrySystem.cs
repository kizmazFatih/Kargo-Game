using System.Collections.Generic; // Liste kullanabilmek için
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class CarrySystem : MonoBehaviour
{
    [SerializeField] private int capacity; //Karakterin taşıyabileceği max kutu sayısı
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
            if (carriedBoxes.Count >= capacity) return;
            // Eğer bu kutu daha önce alınmamışsa
            if (!carriedBoxes.Contains(other.gameObject))
            {

                PositionBoxes(other.gameObject); // Kutuların pozisyonlarını ayarla
            }
        }

        if (other.gameObject.tag == "Palette")
        {
            PaletteJob(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Usebul>() != null)
        {
            other.gameObject.GetComponent<Usebul>().ResetProgressBarValue();
        }
    }



    private void PositionBoxes(GameObject box)
    {
        carriedBoxes.Add(box); // Kutuyu listeye ekle
        GameObject currentBox = carriedBoxes[carriedBoxes.Count - 1];// İlk kutuyu hedef pozisyona yerleştir
        currentBox.transform.SetParent(targetPosition); // İlk kutuyu Target objesine child yap
        currentBox.transform.localPosition = Vector3.zero + new Vector3(0, carriedBoxes.Count - 1, 0); // İlk kutuyu hedefin konumuna yerleştir
        currentBox.transform.rotation = targetPosition.rotation; // İlk kutunun açısını ayarla

    }

    [Header("Palette Settings")]
    [SerializeField] private int palette_column;
    [SerializeField] private int palette_capacity;

    Vector3 NewPositionOfBox(Transform content)
    {
        Vector3 current_position = Vector3.zero;

        if ((content.childCount - 1) % palette_capacity == 0)
        {
            current_position = new Vector3(-6f, 2f, 0);
        }
        else if ((content.childCount - 1) % palette_column == 0)
        {
            current_position = new Vector3(-6f, 0, 2f);
        }
        else
        {
            current_position = new Vector3(2f, 0, 0);
        }

        return current_position;
    }

    void PaletteJob(Transform palette)
    {
        Transform content = palette.transform.GetChild(0).Find("Content");

        if (carriedBoxes.Count > 0)
        {
            for (int i = carriedBoxes.Count; i > 0; i--)
            {
                GameObject current_box = carriedBoxes[carriedBoxes.Count - 1];

                carriedBoxes.Remove(current_box);
                current_box.transform.SetParent(content);


                if (content.childCount == 1)
                {
                    current_box.transform.localPosition = Vector3.zero;//content.transform.position;
                }
                else if (content.childCount > 1)
                {
                    current_box.transform.localPosition = content.GetChild(content.childCount - 2).transform.localPosition + NewPositionOfBox(content);
                }
                current_box.transform.localRotation = Quaternion.Euler(0, 0, 0);
                current_box.transform.tag = "Untagged";


            }
        }
        else if (content.childCount > 0)
        {
            for (int i = 0; i < capacity; i++)
            {
                if (content.childCount == 0) break;

                GameObject current_box = content.GetChild(content.childCount - 1).gameObject;
                PositionBoxes(current_box);
            }
        }



    }
}
