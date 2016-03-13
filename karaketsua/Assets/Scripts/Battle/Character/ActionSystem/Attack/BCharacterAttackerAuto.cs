﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using BattleScene;


namespace BattleScene
{
    public class BCharacterAttackerAuto : BCharacterAttackerBase
    {

        Transform effectCanvas;
        AutoAttackParameter nowAutoAction;

        void Awake()
        {
            base.Awake();
            effectCanvas = GameObject.FindGameObjectWithTag("EffectCanvas").transform;
            nowAutoAction = GetComponent<BCharacterEnemy>().characterParameter.autoAttackParameter;
        }

        public override void Enable()
        {
            base.Enable();
            
            //技のセット
            //selectAttackParameter = character.characterParameter.attackParameter[selectActionNumber];

            //BattleStage.Instance.ChangeTileColorsToAttack(selectAttackParameter.attackRange, this.character);
        }
        public override void Disable()
        {


            //CameraChange.Instance.
            base.Disable();
        }

        
        public void StartAutoAttack()
        {
            //ターゲットの検索と設定
            var target = SetTarget();
            if (target == false)
            {
                OnCompleteAction();
                return;
            }
            //攻撃の実行
            StartCoroutine("ExecuteAttack");
        }

        bool SetTarget()
        {
            //攻撃可能位置の設定
            var attackablePosition = nowAutoAction.attackRanges.Select(x => IntVect2D.Add(x, character.positionArray)).ToList();
            if (attackablePosition == null) return false;
            //デバッグ出力
            //攻撃可能位置にいるキャラクター
            var opponentCharacters = new List<BCharacterBase>();
            foreach (var pos in attackablePosition)
            {
                var chara=BCharacterManager.Instance.GetOpponentCharacterOnTileFormVect2D(pos,character.isEnemy);
                if (chara != null)
                {
                    opponentCharacters.Add(chara);
                }
            }
            if (opponentCharacters.Count==0) return false;

            //一番近い位置がターゲット
            attackTarget.Add(opponentCharacters.OrderBy(c=> IntVect2D.Distance(c.positionArray,character.positionArray)).First());
            return true;

        }

        public float cameraInterval = 1f;
        public float attackTime = 2f;
        IEnumerator ExecuteAttack()
        {
            //横を向く

            //カメラ移動
            BCameraChange.Instance.ActiveLeanMode();
            BCameraMove.Instance.MoveToAutoAttack(this, attackTarget[0].transform.position);

            yield return new WaitForSeconds(cameraInterval);

            //攻撃アニメーション
            animator.SetSingleAttack(0);


            isNowAction = true;

            //ダメージ
            foreach (var target in attackTarget)
            {
                target.Life.Damage((int)nowAutoAction.power);
            }
            //死亡
            foreach (var target in attackTarget)
            {
                target.Life.CheckDestroy();
            }

            //攻撃終了
            Invoke("OnCompleteAnimation", attackTime);
            isDone = true;
            yield return null;
        }

        //ダメージ量計算
        float CalcDamage()
        {
            return nowAutoAction.power;
        }

        void OnCompleteAnimation()
        {
            isNowAction = false;
            //行動終了
            OnCompleteAction();
            //character.OnEndActive();

        }





    }
}