CREATE TABLE `Tags` (
  `id` int UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` varchar(50) DEFAULT NULL,
  `count` int DEFAULT NULL,
  `percentage` real DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1;
