using UnityEngine;

namespace _Project.Scripts.SaveSystem
{
    public static class UserDataSaveSystem
    {
        public static void SetUserName(string name)
        {
            string id = $"User_Name_";
            PlayerPrefs.SetString(id, name);
            CreateUser();
        }

        public static string GetUserName()
        {
            string id = $"User_Name_";
            return PlayerPrefs.GetString(id, "User");
        }

        public static int GetUserStatus()
        {
            string id = $"User_";
            return PlayerPrefs.GetInt(id, 0);
        }

        public static void CreateUser()
        {
            string id = $"User_";
            PlayerPrefs.SetInt(id, 1);
        }
    }
}
