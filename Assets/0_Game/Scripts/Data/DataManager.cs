using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

[DefaultExecutionOrder(-2)]
public class DataManager : Singelton<DataManager>
{
    private Data _data;
    private string _path;
    public event Action<SlotData, int> OnSlotDataChanged = (_, _) => { };
    protected override void Awake()
    {
        base.Awake();

        _path = Path.Combine(Application.persistentDataPath, "Case_Data.json");
        print(_path);
        if (LoadData() == null)
        {
            _data = new Data();
            _data.slots = new List<SlotData>();
        }
    }


    private Data LoadData()
    {
        if (!File.Exists(_path)) return null;
        string json = File.ReadAllText(_path);
        _data = JsonUtility.FromJson<Data>(json);
        return _data;
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(_data);
        File.WriteAllText(_path, json);
    }

    public int TileTypeCount(TileType tileType)
    {
        var slot = _data.slots.Where(s => s.Type == tileType).FirstOrDefault();
        return slot.Count;
    }

    public bool HasTileTypeData(TileType tileType) => _data.slots.Count(s => s.Type == tileType) == 1;

    public void AddNewTileData(TileType tileType)
    {
        if (HasTileTypeData(tileType)) return;
        _data.slots.Add(new SlotData(tileType));
        SaveData();
    }

    public void UpdateTileData(TileType tileType, int amount)
    {
        SlotData slot = _data.slots.Where(s => s.Type == tileType).FirstOrDefault();
        int index = _data.slots.IndexOf(slot);
        slot.Count += amount;
        slot.Count = Math.Max(slot.Count, 0);
        _data.slots[index] = slot;
        OnSlotDataChanged.Invoke(slot, amount);
        SaveData();
    }
}


[Serializable]
public class Data
{
    public List<SlotData> slots;
}

[Serializable]
public struct SlotData
{
    public TileType Type;
    public int Count;
    public SlotData(TileType tileType)
    {
        Type = tileType;
        Count = 0;
    }
}
