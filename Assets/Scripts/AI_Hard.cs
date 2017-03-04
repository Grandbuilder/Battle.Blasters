using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Hard : MonoBehaviour {
	public Button ChargeButt;
	public Button Attack;
	public Button Defend;
	public Button SuperAttack;
	public Button LockedIn;
	public Text Charge = null;
	public Text ChargeP2 = null;
	int chargeCount = 0;
	int chargeCountP2 = 0;
	string P1Choice = null;
	string P2Choice = null;
	bool turn1 = true;
	bool EZ = true;

	public void Start () {
		ChargeButt.onClick.AddListener (Charging);
		Attack.onClick.AddListener (Attacking);
		Defend.onClick.AddListener (Defending);
		SuperAttack.onClick.AddListener (SuperAttacking);
		LockedIn.onClick.AddListener (LockedOn);
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
	}
	private void Charging()
	{
		P1Choice = "Charge";
	}
	private void Attacking()
	{
		P1Choice = "Attack";
	}
	private void Defending()
	{
		P1Choice = "Defend";
	}
	private void SuperAttacking()
	{
		P1Choice = "SuperAttacking";
	}
	private void ChargingP2()
	{
		P2Choice = "Charge";
	}
	private void AttackingP2()
	{
		P2Choice = "Attack";
	}
	private void DefendingP2()
	{
		P2Choice = "Defend";
	}
	private void SuperAttackingP2()
	{
		P2Choice = "SuperAttacking";
	}

	public void LockedOn()
	{
		if (turn1) {
			ChargingP2 ();
			turn1 = false;
		} else if (P1Choice != "Attack" && chargeCountP2 > 0)
			AttackingP2 ();
		else if (P1Choice != "Attack" && chargeCountP2 < 1 && chargeCount < 1) {
			ChargingP2 ();
		} else {
			DefendingP2 ();
		}

		if (P1Choice == "Attack" && P2Choice == "Attack") {
			chargeCount--;
			chargeCountP2--;
		}
		else if (P1Choice == "Defend" && P2Choice == "Attack") {
			chargeCountP2--;
		}
		else if (P1Choice == "Attack" && P2Choice == "Defend") {
			chargeCount--;
		}
		else if (P1Choice == "Charge" && P2Choice == "Charge") {
			chargeCount++;
			chargeCountP2++;
		}
		else if (P1Choice == "Attack" && P2Choice == "Charge") {
			Application.LoadLevel ("Player1Win");
		}
		else if (P1Choice == "Charge" && P2Choice == "Attack") {
			Application.LoadLevel ("Player2Win");
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
		Charge.text = chargeCount.ToString ();
		ChargeP2.text = chargeCount.ToString ();
	}

}
