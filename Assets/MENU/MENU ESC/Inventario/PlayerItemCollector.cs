using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if(item.pego == true)
            {
                Debug.Log("Item j� pego");
                return;
            }

            if (item.Name == "MOEDA")
            {
                item.pego = true;

                int multiplicador = PlayerPrefs.GetInt("moedaX2", 0) == 1 ? 2 : 1;

                float moedasAtuais = PlayerPrefs.GetFloat("moeda", 0f);
                moedasAtuais += 1f * multiplicador;

                PlayerPrefs.SetFloat("moeda", moedasAtuais);

                Debug.Log("MOEDA COLETADA! Total: " + moedasAtuais);
                Destroy(collision.gameObject);
            }

            else
            {
                if (item != null)
                {
                    //bool itemAdded = inventoryController.AddItem(item.gameObject);
                    bool itemAdded = inventoryController.AddItemHotbar(item.gameObject);
                    if (itemAdded)
                    {
                        item.Pickup();
                        Destroy(collision.gameObject);
                    }
                }
            }
        }
    }
}
