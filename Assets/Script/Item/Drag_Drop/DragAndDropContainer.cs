using UnityEngine;
using UnityEngine.UI;

public class DragAndDropContainer : MonoBehaviour
{
    public Image image;
    public Item item;
    public bool Use;
    // public MyData data;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
}
