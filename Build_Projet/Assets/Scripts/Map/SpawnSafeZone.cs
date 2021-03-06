/* 
 * Elisa Kalbé
 * SpawnSafeZone.cs
 * 
 * scrit qui etabli la zone safe du spawn
 *  - balles des joueurs adverses ne traversent pas la zone
 *  - joueurs adverses ne peuvent traverser la zone
 *  - joueurs de l'équipe ne peuvent tirer à l'interieur de la zone
 *  - une fois sortis, les joueurs de l'équipe peuvent revenir dans la zone
 * 
 * Paramètre :
 * 		- equipe : 1 sur l'objet SafeZoneBlue, 2 sur l'objet SafeZoneRed
 * 			indique l'équipe à laquelle correspond la safezone
 * 
 * Dans scene Game
 * A mettre sur les objets : SafeZoneBlue, SafeZoneRed
 * 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSafeZone : MonoBehaviour {

	public int equipe; // equipe associée a la safezone


	// Lorsque le joueur sort de la zone
	private void OnTriggerExit(Collider collision){

		// On verife qu'il s'agit d'une balle
		if (collision.tag == "Bullet") {
			// on detruit la balle lorsqu'elle sort de la zone safe
			Destroy(collision.gameObject);
		}
	}

	// Lorsqu'un joueur vient dans la zone
	// Lorsqu'un joueur tire en direction de la zone
	private void OnTriggerEnter(Collider collision){

		// joueur ne peut rentrer que si c'est la safezone de son équipe
		if (collision.tag == "Equipe1" && equipe == 2) {
			GameObject player = collision.transform.parent.gameObject;
			player.transform.Rotate (0, 90, 0);

		}

		else if(collision.tag == "Equipe2" && equipe ==1){
			GameObject player = collision.transform.parent.gameObject;
			player.transform.Rotate (0, 90, 0);


		}
		// On verife qu'il s'agit d'une balle
		if (collision.tag == "Bullet") {
			// on detruit la balle lorsqu'elle entre dans la zone safe
			Destroy(collision.gameObject);
		}
	}

}
