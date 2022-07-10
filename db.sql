# Id, UserName, Password, FirstName, LastName, EmailAddress, ActivationCode, Active
'fa048316-ecca-4c71-afe9-7e5485be4e7b', 'lordmad', '8f5c6YAFNceUfBTgZEGKow==,9F6QSfJjy4JNW/VqthsbVttCJ8E=', 'Lord', 'Mad', 'lordmad@gmail.com', '29c011b2d90148a8bb601f203be2ae31', '1'

SELECT * FROM users.User;
CREATE TABLE `User` (
  `Id` char(36) NOT NULL,
  `UserName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `EmailAddress` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ActivationCode` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Active` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_User_Id` (`Id`),
  UNIQUE KEY `IX_User_UserName` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


schema users
