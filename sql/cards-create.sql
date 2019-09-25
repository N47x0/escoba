create database test;

use test;

create schema escoba;

create table if not exists Cards (
CardId INT AUTO_INCREMENT PRIMARY KEY,
Suit VARCHAR(32),
`Value` VARCHAR(32)
);

UPDATE `test`.`Deck`
SET
`DeckType` = 'Classic'
WHERE `DeckId` = 1;

INSERT INTO `test`.`Deck`
(`DeckType`)
VALUES
('Classic');

select * from Cards;