﻿//ActionSelect
//作成日
//<summary>
//
//</summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum CommandButton { Attack,Skill,Wait};

public class ActionSelect : Singleton<ActionSelect>
{
	//public
    public GameObject commands;
    public GameObject andoButton;
    public GameObject bottomUI;
	//private

    Character activeCharacter;

	void Awake ()
	{
        commands.SetActive(false);
        andoButton.SetActive(false);
        bottomUI.SetActive(false);
    }
	

    //ボタンを表示
    public void SetActiveAction(Character activeChara)
    {
        activeCharacter = activeChara;
        commands.SetActive(true);
        andoButton.SetActive(false);
        bottomUI.SetActive(true);
    }
    public void EndActiveAction()
    {
        commands.SetActive(false);
        andoButton.SetActive(false);
        bottomUI.SetActive(false);
    }

    //ボタンから使用

    public void OnAttackButton()
    {
        activeCharacter.SetAttackMode();
        commands.SetActive(false);
        andoButton.SetActive(true);
    }
    public void OnSkillButton()
    {
        activeCharacter.SetSkillMode();
        commands.SetActive(false);
        andoButton.SetActive(true);
    }
    public void OnWaitButton()
    {
        activeCharacter.SetWaitMode();
        EndActiveAction();
    }

    public void OnAndoButton()
    {
        activeCharacter.SetAndo();
        commands.SetActive(true);
        andoButton.SetActive(false);


        //SetActiveAction();
    }

}