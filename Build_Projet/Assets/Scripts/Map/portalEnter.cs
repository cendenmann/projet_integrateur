/* 
 * Elisa Kalbé
 * portalEnter.cs
 * 
 * Script pour les actions suivants le passage d'un portail
 * Lorsqu'on passe dans un portail, il disparaît et réapparait 
 * aléatoirement dans la map.
 * A ajouter : boost
 * 
 * Dans la scene Game
 * A mettre sur l'objet : Circle (enfant de l'objet portal)
 *  
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalEnter : MonoBehaviour {


	// Lorsque le joueur passe un portail
	private void OnTriggerEnter(Collider joueur){

		// on verifie qu'il s'agit d'un joueur
		if (joueur.tag == "Equipe1" || joueur.tag == "Equipe2") {

			// on verifie qu'il n'a pas deja un bonus en cours
			GameObject player = joueur.transform.parent.gameObject;
			if (!player.GetComponent<PlayerInput> ().bonus) {

				// On récupère les caractéristiques du portail concerné
				GameObject parent = this.transform.parent.gameObject;

				// ---- On regarde quel bonus le joueur a eu
				// INVINCIBILITE : le joueur ne peux etre touché pendant quelques secondes
				if (parent.tag == "Invincible") {
					StartCoroutine (invincible (joueur));
				}

				// INVISIBILITE : le joueur ne peux etre vu pendant quelques secondes
				if (parent.tag == "Invisible") {
					StartCoroutine (invisible (joueur));
				}

				// DEGAT : le joueur fait plus de dégat pendant quelques secondes
				if (parent.tag == "Degat") {
					StartCoroutine (degat (joueur));
				}

				// ESPION : le joueur apparaît aux couleurs de l'équipe adversaire pendant quelques secondes
				if (parent.tag == "Espion") {
					StartCoroutine (espion (joueur));
				}

				// ACCELERATION : le vaisseau du joueur accelere pendant quelques secondes
				if (parent.tag == "Accelere") {
					StartCoroutine (accelere (joueur));
				}


				// ---- On change le portail de position
				// On modifie sa position
				parent.transform.position = Random.insideUnitSphere * 170; // idem que dans portalCreation ( a changer selon scale de la map)
				// On modifie sa rotation
				parent.transform.rotation = Random.rotationUniform;
			}
		}

	}

	// Routine pour acceleration
	private IEnumerator accelere(Collider joueur){

		// recupere joueur
		GameObject player = joueur.transform.parent.gameObject;

		player.GetComponent<PlayerInput> ().bonus = true; // a bonus

		// change vitesse
		player.GetComponent<PlayerInput> ().Speed = 70; // modifie valeur vitesse

		// attend 5 secondes
		yield return new WaitForSeconds (10);

		// fini acceleration
		player.GetComponent<PlayerInput> ().Speed = 30; //valeur acceleration par défault
		player.GetComponent<PlayerInput> ().bonus = false; 

	}

	// Routine pour invisibilité
	private IEnumerator invisible(Collider joueur){

		// recupere joueur et vaisseau
		GameObject player = joueur.transform.parent.gameObject;
		GameObject vaisseau = joueur.gameObject.transform.GetChild (0).gameObject;

		player.GetComponent<PlayerInput> ().bonus=true; // a bonus

		// recupere texture et change le alpha pour transparence
		Color alphaColor = vaisseau.GetComponent<MeshRenderer>().material.color;
		vaisseau.GetComponent<MeshRenderer> ().material.color = new Color(alphaColor.r, alphaColor.g, alphaColor.b, 0);

		// recupere et enleve nom du joueur
		GameObject text = joueur.transform.GetChild (4).gameObject;
		text.SetActive (false);

		// attend 5 secondes
		yield return new WaitForSeconds(10);

		// fin invisible
		vaisseau.GetComponent<MeshRenderer> ().material.color = new Color(alphaColor.r, alphaColor.g, alphaColor.b, 255);
		text.SetActive (true);
		player.GetComponent<PlayerInput> ().bonus=false;
	}

	// Routine pour augmenter degat
	private IEnumerator degat(Collider joueur){

		// recupere joueur
		GameObject player = joueur.transform.parent.gameObject;

		player.GetComponent<PlayerInput> ().bonus=true; // a bonus

		Debug.Log("debut degat (manque script guillaume)");
		// modifie valeur degat

		// attend 5 secondes
		yield return new WaitForSeconds(10);

		// fin degat
		player.GetComponent<PlayerInput> ().bonus=false;

	}

	// Routine pour mode espion
	private IEnumerator espion(Collider joueur){
		
		// recupere joueur et tag et text nom du joueur
		string tag = joueur.tag;
		GameObject player = joueur.transform.parent.gameObject;
		GameObject nomJoueur = joueur.gameObject.transform.GetChild (4).gameObject;
		GameObject text = nomJoueur.transform.GetChild (1).gameObject;

		player.GetComponent<PlayerInput> ().bonus=true; // a bonus

		// modifie couleur text
		if(joueur.tag == "Equipe1")
			text.GetComponent<TextMesh> ().color = Color.red;
		else
			text.GetComponent<TextMesh> ().color = Color.blue;

		// attend 5 secondes
		yield return new WaitForSeconds(10);

		// fin mode espion
		if(joueur.tag == "Equipe1")
			text.GetComponent<TextMesh> ().color = Color.blue;
		else
			text.GetComponent<TextMesh> ().color = Color.red;
		player.GetComponent<PlayerInput> ().bonus=false;
	}

	// Routine pour invincibilité
	private IEnumerator invincible(Collider joueur){

		// recupere joueur et boule d'invincibilité
		GameObject player = joueur.transform.parent.gameObject;
		GameObject invincible = joueur.gameObject.transform.GetChild (3).gameObject;

		player.GetComponent<PlayerInput> ().bonus=true; // a bonus

		// active boule invincibilité
		invincible.SetActive(true);

		// attend 5 secondes
		yield return new WaitForSeconds(10);

		// fin invincibilité
		invincible.SetActive(false);
		player.GetComponent<PlayerInput> ().bonus=false;

	}
}
