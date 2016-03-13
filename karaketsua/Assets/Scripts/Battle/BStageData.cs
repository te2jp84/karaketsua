﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleScene;

namespace BattleScene
{
    public class BStageData : DontDestroySingleton<BStageData>
    {

        public List<PlayerCharacterData> playerCharacters;
        public List<EnemyCharacterData> enemyCharacters;

        [System.Serializable]
        public class CharacterData
        {
            //public BCharacterBase prefab;
            public Vector2 position;
        }
        [System.Serializable]
        public class PlayerCharacterData:CharacterData
        {
            public BCharacterPlayer prefab;
        }
        [System.Serializable]
        public class EnemyCharacterData:CharacterData
        {
            public BCharacterEnemy prefab;
        }

    }

}