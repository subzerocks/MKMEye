CREATE TABLE IF NOT EXISTS `cards` (
  `id` text,
  `Name` text,
  `pHash` text CHARACTER SET cp1251,
  `Set` text,
  `Type` text,
  `Cost` text,
  `Rarity` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

