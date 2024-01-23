using System;
using System.IO;
using Project.Scripts.NoMonoBehaviourClass;
using UnityEngine;

namespace Project.Scripts.Managers
{
    public class SaveManager : MonoBehaviour
    {
        private static SaveManager _instance;

        public const String SAVE_FILE_PATH = "/Save/Save.json";
        
        [SerializeField] private SaveFile _saveFile;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        
        
        public static SaveManager Instance
        {
            get { return _instance; }
        }

        public void LoadFromJSON()
        {
            if (!File.Exists(Application.streamingAssetsPath + SAVE_FILE_PATH))
            {
                return;
            }
            string json = File.ReadAllText(Application.streamingAssetsPath + SAVE_FILE_PATH);
            _saveFile = JsonUtility.FromJson<SaveFile>(json);
        }

        public void SaveToJSON(bool async = false)
        {
            if (_saveFile == null)
            {
                return;
            }
            string json = JsonUtility.ToJson(_saveFile);
            string path = Application.streamingAssetsPath + SAVE_FILE_PATH;
            if (async)
            {
                File.WriteAllTextAsync(path, json);
            }
            else
            {
                File.WriteAllText(path, json);
            }
        }

        public SaveFile GetSaveFile()
        {
            return _saveFile;
        }
    }
}
