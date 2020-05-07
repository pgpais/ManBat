using Assets.Scripts.Enemy;
using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : SingletonPersistentMonobehaviour<GameManager>
    {
        private Player _player;
        private List<EnemyCharacter> _enemies = new List<EnemyCharacter>();

        void Start()
        {
            FetchSceneObjects();
            SceneManager.sceneLoaded += this.FetchSceneObjects;
        }

        private void FetchSceneObjects()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in enemies)
            {
                _enemies.Add(go.GetComponent<EnemyCharacter>());
            }
        }

        public void FetchSceneObjects(Scene scene, LoadSceneMode mode)
        {
            FetchSceneObjects();
        }

        public void ResetLevel()
        {
            _player.Reset();
            foreach(EnemyCharacter enemy in _enemies)
            {
                enemy.Reset();
            }
        }
    }
}
