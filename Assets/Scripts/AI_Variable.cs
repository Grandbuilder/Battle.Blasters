using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Variable : MonoBehaviour {
	public Button ChargeButt;
	public Button Attack;
	public Button Defend;
	public Button SuperAttack;
	public Text Charge = null;
	public Text ChargeP2 = null;
	int chargeCount = 0;
	int chargeCountP2 = 0;
	string P1Choice = null;
	string P2Choice = null;
	string P1LastChoice = null;

	public void Start ()
    {
		ChargeButt.onClick.AddListener (Charging);
		Attack.onClick.AddListener (Attacking);
		Defend.onClick.AddListener (Defending);
		SuperAttack.onClick.AddListener (SuperAttacking);
    }

    public void Awake ()
    {
        ChargeButt = GameObject.Find("Charge").GetComponent<Button>();
        Attack = GameObject.Find("Attack").GetComponent<Button>();
        Defend = GameObject.Find("Guard").GetComponent<Button>();
        SuperAttack = GameObject.Find("Super Attack").GetComponent<Button>();

        Charge = GameObject.Find("Charge Num").GetComponent<Text>();
        ChargeP2 = GameObject.Find("Charge Num (P2)").GetComponent<Text>();
    }

	public void Update()
	{		
		if (chargeCount > 0)
			Attack.enabled = true;
		else if (chargeCount < 1)
			Attack.enabled = false;		
		if (chargeCount > 4)
			SuperAttack.enabled = true;
		else if (chargeCount < 5)
			SuperAttack.enabled = false;

        Charge.text = chargeCount.ToString();
        ChargeP2.text = chargeCountP2.ToString();
    }

    private void Charging() { SetChoice("Charge"); } //for listener
    private void Attacking() { SetChoice("Attack"); } //for listener
    private void Defending() { SetChoice("Defend"); } //for listener
    private void SuperAttacking() { SetChoice("SuperAttacking"); } //for listener

    private void SetChoice(string choice)
    {
        if (P1Choice == choice)
        {
            LockedOn();
            Debug.Log("LOCKED IN");
        }
        else
        {
            P1Choice = choice;
            Debug.Log("P1 Choice set to " + P1Choice);
        }
    }

    public void LockedOn()
	{
        Debug.Log("WE DOIN THIS");
		int atkChance = 10;
		int defChance = 10;
		int chrChance = 10;

		//RULES THE AI FOLLOWS IN STRATEGY START HERE

		if (chargeCount > 0)
			atkChance += (chargeCount * 5);

		if (P1LastChoice != "Attack") {
			defChance += 5;
		} else {
			chrChance += 5;
			atkChance += 5;
		}


		//-ABSOLUTE RULES (Sets something to 0, should come at the end)
		if (chargeCountP2 == 0)
			atkChance = 0;

		if (chargeCount == 0)
			defChance = 0;

		//RULES END HERE


		System.Random random = new System.Random();
		int randChoice = random.Next(0, (atkChance + defChance + chrChance)) + 1;

		if (chargeCountP2 > 4)
			P2Choice = "SuperAttacking";

		if (atkChance >= randChoice && P2Choice == null)
			P2Choice = "Attack";
		else
			randChoice -= atkChance;

		if (defChance >= randChoice && P2Choice == null)
			P2Choice = "Defend";
		else
			randChoice -= defChance;

		if(P2Choice == null)
			P2Choice = "Charge";

        Debug.Log("P1 CHOICE: " + P1Choice);
        Debug.Log("AI CHOICE: " + P2Choice);

		if (P1Choice == "Attack" && P2Choice == "Attack") {
            if (chargeCount > 0 && chargeCountP2 == 0)
                Application.LoadLevel("Player1Win");
            else if (chargeCount == 0 && chargeCountP2 > 0)
                Application.LoadLevel("Player2Win");
            else
            {
                if (chargeCount > 0)
                    chargeCount--;
                if (chargeCountP2 > 0)
                    chargeCountP2--;
            }
		}
		else if (P1Choice == "Defend" && P2Choice == "Attack") {
            if(chargeCountP2 > 0)
                chargeCountP2--;
		}
		else if (P1Choice == "Attack" && P2Choice == "Defend") {
            if (chargeCount > 0)
                chargeCount--;
		}
		else if (P1Choice == "Charge" && P2Choice == "Charge") {
			chargeCount++;
			chargeCountP2++;
		}
		else if (P1Choice == "Attack" && P2Choice == "Charge") {
            if (chargeCount > 0)
                Application.LoadLevel("Player1Win");
            else
                chargeCountP2++;
		}
		else if (P1Choice == "Charge" && P2Choice == "Attack") {
            if (chargeCountP2 > 0)
                Application.LoadLevel("Player2Win");
            else
                chargeCount++;
		}
        else if (P1Choice == "Defend" && P2Choice == "Charge")
        {
            chargeCountP2++;
        }
        else if (P1Choice == "Charge" && P2Choice == "Defend")
        {
            chargeCount++;
        }
        else if (P1Choice == "SuperAttacking" && P2Choice == "SuperAttacking") {
			chargeCount = chargeCount - 5;
			chargeCountP2 = chargeCountP2 - 5;
		}
		else if (P1Choice == "SuperAttacking") {
			Application.LoadLevel ("Player1Win");
		}
		else if (P2Choice == "SuperAttacking") {
			Application.LoadLevel ("Player2Win");
		}

		P1LastChoice = P1Choice;

		P1Choice = null;
		P2Choice = null;

		Charge.text = chargeCount.ToString ();
		ChargeP2.text = chargeCountP2.ToString ();
	}

}
