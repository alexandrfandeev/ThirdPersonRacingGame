using UnityEngine;

namespace _Project.Scripts.SaveSystem
{
    public static class LevelDataSaveSystem 
    {
        public static int GetLevelState(int level)
        {
            string id = $"Level_{level}";
            return PlayerPrefs.GetInt(id, 0);
        }

        public static void SetLevelComplete(int level)
        {
            string id = $"Level_{level}";
            PlayerPrefs.SetInt(id, 1);
        }
    }
}
