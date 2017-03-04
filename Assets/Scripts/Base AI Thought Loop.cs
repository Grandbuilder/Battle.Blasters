using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
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

	public void LockedOn()
	{
		if (P1Choice == "Attack") {
			chargeCount--;
			chargeCountP2--;
		}
		else if (P1Choice == "Defend") {
			chargeCountP2--;
		}
		else if (P1Choice == "Attack") {
			chargeCount--;
		}
		else if (P1Choice == "Charge") {
			chargeCount++;
			chargeCountP2++;
		}
		else if (P1Choice == "Attack") {
			Application.LoadLevel ("Player1Win");
		}
		else if (P1Choice == "Charge") {
			Application.LoadLevel ("Player2Win");
		}
		else if (P1Choice == "SuperAttacking") {
			chargeCount = chargeCount - 5;
			chargeCountP2 = chargeCountP2 - 5;
		}
		else if (P1Choice == "SuperAttacking") {
			Application.LoadLevel ("Player1Win");
		}
		Charge.text = chargeCount.ToString ();
		ChargeP2.text = chargeCount.ToString ();
	}

}
