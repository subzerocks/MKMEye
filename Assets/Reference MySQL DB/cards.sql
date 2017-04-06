CREATE TABLE `cards` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `pHash` varchar(255) DEFAULT NULL,
  `Edition` varchar(8) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8