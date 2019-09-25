create database test;

use test;

create schema escoba;

create table if not exists Deck (
DeckId INT AUTO_INCREMENT PRIMARY KEY,
DeckType VARCHAR(16)
);

UPDATE `test`.`Deck`
SET
`DeckType` = 'Classic'
WHERE `DeckId` = 1;

INSERT INTO `test`.`Deck`
(`DeckType`)
VALUES
('Classic');

select * from Deck;