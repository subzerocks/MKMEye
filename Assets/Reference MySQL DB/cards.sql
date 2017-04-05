CREATE TABLE `cards` (
  `id` int(11) NOT NULL,
  `Name` text,
  `pHash` varchar(255) CHARACTER SET cp1251 DEFAULT NULL,
  `Set` text,
  `Type` text,
  `Cost` text,
  `Rarity` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8