using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Slot _slotPrefab;
    Dictionary<TileType, Slot> slots = new Dictionary<TileType, Slot>();
    private void OnEnable()
    {

        for (int i = 0; i < TileSpawner.Instance.TileConfig.TileTypeHolderList.Count; i++)
        {
            TileType tileType = TileSpawner.Instance.TileConfig.TileTypeHolderList[i].Type;

            if (tileType == TileType.Empty) continue;

            Slot slot = Instantiate(_slotPrefab, transform);

            if (!DataManager.Instance.HasTileTypeData(tileType))
            {
                DataManager.Instance.AddNewTileData(tileType);
            }

            slots.Add(TileSpawner.Instance.TileConfig.TileTypeHolderList[i].Type, slot);
            slot.Init(TileSpawner.Instance.TileConfig.TileTypeHolderList[i].TileSprite, DataManager.Instance.TileTypeCount(tileType));
        }

        DataManager.Instance.OnSlotDataChanged += OnSlotDataChanged;
    }

    private void OnDisable()
    {
        DataManager.Instance.OnSlotDataChanged -= OnSlotDataChanged;
    }

    private void OnSlotDataChanged(SlotData slotData)
    {
        slots[slotData.Type].UptadeItemCount(slotData.Count);
    }
}
