CREATE DATABASE tennis;
\c tennis;
CREATE TABLE players (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE
);



CREATE TABLE score_history (
    id SERIAL PRIMARY KEY,               -- Identifiant unique pour chaque entrée
    player_id INT NOT NULL,              -- Identifiant du joueur
    points_before INT NOT NULL,          -- Points avant le changement
    points_after INT NOT NULL,           -- Points après le changement
    action_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP -- Date et heure de l'action
);


INSERT INTO players (name) VALUES ('Player 1'), ('Player 2');

