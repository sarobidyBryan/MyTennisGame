# TennisGame
TennisGame
Description
Ce dépôt contient un jeu de tennis personnalisé développé en C# avec .NET 8. Le jeu inclut des fonctionnalités telles que la gestion des joueurs, des balles, des cibles, et des indicateurs de cible. Il permet également de suivre les scores et de gérer les mouvements des objets sur le terrain. L'interface utilisateur est construite avec Windows Forms, offrant une expérience interactive et visuelle.
Note : Ce projet est encore en cours de développement.
Fonctionnalités
•	Gestion des joueurs et des balles
•	Indicateurs de cible
•	Suivi des scores
•	Mouvements des objets sur le terrain
•	Interface utilisateur interactive avec Windows Forms
•	Connexion à une base de données PostgreSQL pour stocker les historiques de scores
Prérequis
•	.NET 8
•	Visual Studio 2022
•	PostgreSQL
Installation
1.	Clonez le dépôt :
  git clone https://github.com/votre-utilisateur/MyTennisGame.git
2.	Ouvrez le projet dans Visual Studio 2022.
3.	Assurez-vous que .NET 8 est installé sur votre machine.
4.	Configurez votre base de données PostgreSQL et mettez à jour la chaîne de connexion dans le fichier Connexion.cs :
 _connectionString = $"Host=localhost;Database=tennis;Username=postgres;Password='votre-mot-de-passe'";
Utilisation
1.	Lancez l'application depuis Visual Studio en appuyant sur F5 ou en sélectionnant Debug > Start Debugging.
2.	Une boîte de dialogue de démarrage apparaîtra. Cliquez sur Ok pour commencer le jeu.
3.	Utilisez les touches suivantes pour contrôler les joueurs et les indicateurs de cible :
•	Joueur 1 : A (gauche), D (droite)
•	Joueur 2 : NumPad4 (gauche), NumPad6 (droite)
•	Indicateur de cible : V (gauche), N (droite)
•	Redémarrer le jeu : Escape
•	Sauvegarder les scores : L
Structure du projet
•	Form1.cs : Contient la logique principale de l'interface utilisateur et du jeu.
•	TennisGameManager.cs : Gère l'état du jeu, les scores et les interactions entre les objets.
•	KeyboardManager.cs : Gère les entrées clavier pour contrôler les joueurs et les indicateurs.
•	CibleMovementService.cs et ShapeMovementService.cs : Gèrent les mouvements des cibles et des formes.
•	Connexion.cs : Gère la connexion à la base de données PostgreSQL.
•	FileWriter.cs : Gère l'écriture des historiques de scores dans un fichier.
Contribution
Les contributions sont les bienvenues ! Veuillez suivre les étapes ci-dessous pour contribuer :
1.	Forkez le dépôt.
2.	Créez une branche pour votre fonctionnalité (git checkout -b feature/ma-fonctionnalité).
3.	Commitez vos modifications (git commit -am 'Ajout de ma fonctionnalité').
4.	Poussez votre branche (git push origin feature/ma-fonctionnalité).
5.	Ouvrez une Pull Request.
   
Auteurs
sarobidyBryan
---
Merci d'avoir utilisé TennisGame ! Amusez-vous bien en jouant ! 🎾


