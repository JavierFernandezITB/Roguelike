using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class ServicesReferences : MonoBehaviour
{
    protected EntityManagerService entityManagerService;
    protected RoomManagerService roomManagerService;
    protected InventoryManagerService inventoryManagerService;

    protected virtual void GetServices()
    {
        entityManagerService = GameObject.Find("/EntityManagerService")?.GetComponent<EntityManagerService>();
        roomManagerService = GameObject.Find("/RoomManagerService")?.GetComponent<RoomManagerService>();
        inventoryManagerService = GameObject.Find("/InventoryManagerService")?.GetComponent<InventoryManagerService>();
    }

    protected virtual void Persist<T>() where T : UnityEngine.Object
    {
        if (FindObjectsOfType<T>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
