using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryIns : MonoBehaviour
{
    #region Singleton
    public static InventoryIns instance;
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
}
