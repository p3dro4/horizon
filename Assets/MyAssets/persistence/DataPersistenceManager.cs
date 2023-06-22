using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MyAssets
{
    public class DataPersistenceManager : MonoBehaviour
    {
        private GameData _gameData;
        private List<IDataPersistence> _dataPersistenceObjects = new List<IDataPersistence>();

        public static DataPersistenceManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
                Debug.Log("Multiple instances of DataPersistenceManager!");
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            this._dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();
            return new List<IDataPersistence>(dataPersistenceObjects);
        }

        // Update is called once per frame
        void Update()
        {
        }


        public void NewGame()
        {
            _gameData = new GameData();
        }

        public void LoadGame()
        {
            if (this._gameData == null)
            {
                Debug.Log("No game data to load!");
                NewGame();
            }

            foreach (var dataPersistenceObject in this._dataPersistenceObjects)
            {
                dataPersistenceObject.LoadData(_gameData);
            }

            Debug.Log("Game loaded!");
        }

        public void SaveGame()
        {
            foreach (var dataPersistenceObject in this._dataPersistenceObjects)
            {
                dataPersistenceObject.SaveData(ref _gameData);
            }

            Debug.Log("Game saved!");
        }
    }
}