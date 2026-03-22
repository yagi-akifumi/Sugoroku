using UnityEngine;

public static class SaveManager
{
    private const string SAVE_KEY = "GAME_SAVE_DATA";

    public static void Save(SaveData saveData)
    {
        PlayerPrefsHelper.SaveSetObjectData(SAVE_KEY, saveData);
    }

    public static SaveData Load()
    {
        if (!PlayerPrefsHelper.ExistsData(SAVE_KEY))
        {
            return null;
        }

        return PlayerPrefsHelper.LoadGetObjectData<SaveData>(SAVE_KEY);
    }

    public static void Delete()
    {
        PlayerPrefsHelper.RemoveObjectData(SAVE_KEY);
    }

    public static bool HasSaveData()
    {
        return PlayerPrefsHelper.ExistsData(SAVE_KEY);
    }
}