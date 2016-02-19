/*
SQLyog Professional v12.09 (64 bit)
MySQL - 5.6.27-log : Database - minie_db_v0
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`minie_db_v0` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `minie_db_v0`;

/*Table structure for table `cm_entrance` */

DROP TABLE IF EXISTS `cm_entrance`;

CREATE TABLE `cm_entrance` (
  `id` char(36) NOT NULL,
  `name` varchar(64) NOT NULL,
  `desc` varchar(256) NOT NULL,
  `node` char(36) DEFAULT NULL,
  `creator` char(36) DEFAULT NULL,
  `creation_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `cm_entrance_node_fk` (`node`),
  KEY `cm_entrance_creator_fk` (`creator`),
  CONSTRAINT `cm_entrance_creator_fk` FOREIGN KEY (`creator`) REFERENCES `sys_user` (`id`) ON DELETE SET NULL,
  CONSTRAINT `cm_entrance_node_fk` FOREIGN KEY (`node`) REFERENCES `cm_node` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cm_entrance` */

insert  into `cm_entrance`(`id`,`name`,`desc`,`node`,`creator`,`creation_time`) values ('2744f490-169d-49b7-9ac9-ccad850ad6e2','zdh#1616','智能化楼#1616','ec492d63-ede3-45bd-8260-2d84675302ee','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('8871ecc3-bc3c-4044-86ac-1bbc748d6c6b','zdh#1220','自动化楼1220','f2f31343-2da9-4a3c-935e-be40dc7e37f7','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('9a992cca-ef7f-4ea4-b30d-1592785eb3e9','zdh','自动化楼北','cb397866-75f2-4659-bdd0-78c5690ff023','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('d268db69-a2ae-4c7d-b105-bc69e27ee5f9','zdh#1216','自动化楼#1216','9446f9a4-ee7e-4b27-af6d-275bd9cbacbe','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('d94fbab3-7d7b-4a60-9e01-3934abb480c7','zdh#1214','自动化楼#1214','100d6b1a-e180-4bd8-87ad-75433fac3a3c','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('e0f1d68e-e72c-4c72-9dfb-fabd3afdf463','zky','自动化大门','71e603e0-0401-490f-ad92-5b9331e46f50','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('ed2636a2-8fd4-4d97-971e-9cbdbbf07170','zdh','自动化楼南','cb397866-75f2-4659-bdd0-78c5690ff023','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('f1ca7933-818c-49de-a60b-27d487476d97','znh','智能化楼','29a507fe-6a03-47c2-9109-60b595adfef5','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48');

/*Table structure for table `cm_friend_privilege` */

DROP TABLE IF EXISTS `cm_friend_privilege`;

CREATE TABLE `cm_friend_privilege` (
  `id` char(36) NOT NULL,
  `user_friend` char(36) NOT NULL,
  `entrance` char(36) NOT NULL,
  `valid_from_time` datetime DEFAULT NULL,
  `valid_to_time` datetime DEFAULT NULL,
  `creation_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `cm_friend_privilege_user_friend_fk` (`user_friend`),
  KEY `cm_friend_privilege_entrance_fk` (`entrance`),
  CONSTRAINT `cm_friend_privilege_entrance_fk` FOREIGN KEY (`entrance`) REFERENCES `cm_entrance` (`id`) ON DELETE CASCADE,
  CONSTRAINT `cm_friend_privilege_user_friend_fk` FOREIGN KEY (`user_friend`) REFERENCES `cm_user_friend` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cm_friend_privilege` */

/*Table structure for table `cm_node` */

DROP TABLE IF EXISTS `cm_node`;

CREATE TABLE `cm_node` (
  `id` char(36) NOT NULL,
  `name` varchar(128) NOT NULL,
  `desc` varchar(1024) NOT NULL,
  `address` varchar(256) NOT NULL,
  `category` int(11) DEFAULT NULL,
  `parent` char(36) DEFAULT NULL,
  `creator` char(36) DEFAULT NULL,
  `creation_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `cm_node_category_fk` (`category`),
  KEY `cm_node_parent_fk` (`parent`),
  KEY `cm_node_creator_fk` (`creator`),
  CONSTRAINT `cm_node_category_fk` FOREIGN KEY (`category`) REFERENCES `cm_node_category` (`id`) ON DELETE SET NULL,
  CONSTRAINT `cm_node_creator_fk` FOREIGN KEY (`creator`) REFERENCES `sys_user` (`id`) ON DELETE SET NULL,
  CONSTRAINT `cm_node_parent_fk` FOREIGN KEY (`parent`) REFERENCES `cm_node` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cm_node` */

insert  into `cm_node`(`id`,`name`,`desc`,`address`,`category`,`parent`,`creator`,`creation_time`) values ('100d6b1a-e180-4bd8-87ad-75433fac3a3c','1214','中科唯实','北京市昌平区中关村东路95号#1',400,'cb397866-75f2-4659-bdd0-78c5690ff023','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('29a507fe-6a03-47c2-9109-60b595adfef5','智能化楼','#2','北京市昌平区中关村东路95号#2',200,'71e603e0-0401-490f-ad92-5b9331e46f50','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('71e603e0-0401-490f-ad92-5b9331e46f50','自动化所','自动化大厦','北京市昌平区中关村东路95号',100,NULL,'85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('9446f9a4-ee7e-4b27-af6d-275bd9cbacbe','1216','中科唯实','北京市昌平区中关村东路95号#3',400,'cb397866-75f2-4659-bdd0-78c5690ff023','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('cb397866-75f2-4659-bdd0-78c5690ff023','自动化楼','#1','北京市昌平区中关村东路95号#1',200,'71e603e0-0401-490f-ad92-5b9331e46f50','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('ec492d63-ede3-45bd-8260-2d84675302ee','1604','模式识别实验室','北京市昌平区中关村东路95号#3',400,'29a507fe-6a03-47c2-9109-60b595adfef5','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48'),('f2f31343-2da9-4a3c-935e-be40dc7e37f7','1220','中科唯实','北京市昌平区中关村东路95号#2',400,'cb397866-75f2-4659-bdd0-78c5690ff023','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48');

/*Table structure for table `cm_node_category` */

DROP TABLE IF EXISTS `cm_node_category`;

CREATE TABLE `cm_node_category` (
  `id` int(11) NOT NULL,
  `name` varchar(64) NOT NULL,
  `desc` varchar(256) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cm_node_category` */

insert  into `cm_node_category`(`id`,`name`,`desc`) values (100,'room','住户'),(200,'unit','单元'),(300,'building','楼房'),(400,'community','社区');

/*Table structure for table `cm_node_user` */

DROP TABLE IF EXISTS `cm_node_user`;

CREATE TABLE `cm_node_user` (
  `id` char(36) NOT NULL,
  `node` char(36) DEFAULT NULL,
  `user` char(36) DEFAULT NULL,
  `is_host` tinyint(1) NOT NULL DEFAULT '0',
  `creation_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `cm_node_user_user_fk` (`user`),
  KEY `cm_node_user_node_fk` (`node`),
  CONSTRAINT `cm_node_user_node_fk` FOREIGN KEY (`node`) REFERENCES `cm_node` (`id`) ON DELETE CASCADE,
  CONSTRAINT `cm_node_user_user_fk` FOREIGN KEY (`user`) REFERENCES `cm_user` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cm_node_user` */

insert  into `cm_node_user`(`id`,`node`,`user`,`is_host`,`creation_time`) values ('0342a707-d2cf-417b-a16c-7284b4bbd389','ec492d63-ede3-45bd-8260-2d84675302ee','3da74019-960e-4c4b-804e-35319d6e68ef',1,'2016-02-19 16:29:48'),('60d16675-3fb5-4ad2-b527-205ecf9149db','f2f31343-2da9-4a3c-935e-be40dc7e37f7','7529134e-8d13-4e93-9a09-2aa1de4a21d4',1,'2016-02-19 16:29:48'),('9dbc591d-d138-4355-bf6b-a32e7c5e59a7','9446f9a4-ee7e-4b27-af6d-275bd9cbacbe','0439062d-2030-4df6-a00e-626bb772cca3',1,'2016-02-19 16:29:48'),('dd08bd06-52a6-480a-9a7a-21351be7b1c5','100d6b1a-e180-4bd8-87ad-75433fac3a3c','3da74019-960e-4c4b-804e-35319d6e68ef',1,'2016-02-19 16:29:48');

/*Table structure for table `cm_user` */

DROP TABLE IF EXISTS `cm_user`;

CREATE TABLE `cm_user` (
  `id` char(36) NOT NULL,
  `username` varchar(32) NOT NULL,
  `cellphone` varchar(32) NOT NULL,
  `password` varchar(64) NOT NULL,
  `nickname` varchar(64) NOT NULL,
  `email` varchar(64) NOT NULL,
  `creation_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cm_user` */

insert  into `cm_user`(`id`,`username`,`cellphone`,`password`,`nickname`,`email`,`creation_time`) values ('0439062d-2030-4df6-a00e-626bb772cca3','dirk','13810955224','e10adc3949ba59abbe56e057f20f883e','nickname','dirk@163.com','2016-02-19 16:29:48'),('3da74019-960e-4c4b-804e-35319d6e68ef','fjw','18611784218','e10adc3949ba59abbe56e057f20f883e','nickname','fjw@qq.com','2016-02-19 16:29:48'),('7529134e-8d13-4e93-9a09-2aa1de4a21d4','002','13810299313','e10adc3949ba59abbe56e057f20f883e','nickname','lxd@163.com','2016-02-19 16:29:48'),('fae03364-b3a8-4a3d-b4b2-02235e201ecb','test','13810955224','e10adc3949ba59abbe56e057f20f883e','nickname','test@163.com','2016-02-19 16:29:48');

/*Table structure for table `cm_user_audit_data` */

DROP TABLE IF EXISTS `cm_user_audit_data`;

CREATE TABLE `cm_user_audit_data` (
  `object_id` char(36) NOT NULL,
  `index` bigint(20) unsigned NOT NULL,
  `value` char(36) NOT NULL,
  KEY `object_id_i` (`object_id`),
  KEY `index_i` (`index`),
  KEY `cm_user_audit_data_value_fk` (`value`),
  CONSTRAINT `cm_user_audit_data_object_id_fk` FOREIGN KEY (`object_id`) REFERENCES `cm_user` (`id`) ON DELETE CASCADE,
  CONSTRAINT `cm_user_audit_data_value_fk` FOREIGN KEY (`value`) REFERENCES `minie_audit_v0`.`sys_audit_frontend` (`obj_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cm_user_audit_data` */

/*Table structure for table `cm_user_friend` */

DROP TABLE IF EXISTS `cm_user_friend`;

CREATE TABLE `cm_user_friend` (
  `id` char(36) NOT NULL,
  `user` char(36) NOT NULL,
  `userfriend` char(36) NOT NULL,
  `creation_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `cm_user_friend_user_fk` (`user`),
  KEY `cm_user_friend_userfriend_fk` (`userfriend`),
  CONSTRAINT `cm_user_friend_user_fk` FOREIGN KEY (`user`) REFERENCES `cm_user` (`id`) ON DELETE CASCADE,
  CONSTRAINT `cm_user_friend_userfriend_fk` FOREIGN KEY (`userfriend`) REFERENCES `cm_user` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cm_user_friend` */

insert  into `cm_user_friend`(`id`,`user`,`userfriend`,`creation_time`) values ('0c65fcbf-7644-432b-ada8-f8a44aacb0a0','7529134e-8d13-4e93-9a09-2aa1de4a21d4','fae03364-b3a8-4a3d-b4b2-02235e201ecb','2016-02-19 16:29:48'),('0d214de8-8010-4d23-bb0a-2a94ba7fb691','fae03364-b3a8-4a3d-b4b2-02235e201ecb','3da74019-960e-4c4b-804e-35319d6e68ef','2016-02-19 16:29:48'),('267cd312-a9ed-473d-8b90-6f1b56f0ecdb','7529134e-8d13-4e93-9a09-2aa1de4a21d4','0439062d-2030-4df6-a00e-626bb772cca3','2016-02-19 16:29:48'),('326d023a-466d-4763-99fa-e901433316f3','0439062d-2030-4df6-a00e-626bb772cca3','7529134e-8d13-4e93-9a09-2aa1de4a21d4','2016-02-19 16:29:48'),('35e45c5b-482c-4f74-86a0-198753b7dfd7','3da74019-960e-4c4b-804e-35319d6e68ef','fae03364-b3a8-4a3d-b4b2-02235e201ecb','2016-02-19 16:29:48'),('45dfe12f-78e7-4176-988a-9e7297a8385a','0439062d-2030-4df6-a00e-626bb772cca3','3da74019-960e-4c4b-804e-35319d6e68ef','2016-02-19 16:29:48'),('48941090-a28c-48db-9bcd-fbf4c5eaf155','3da74019-960e-4c4b-804e-35319d6e68ef','0439062d-2030-4df6-a00e-626bb772cca3','2016-02-19 16:29:48'),('8468829f-7caf-4c74-81a0-5617b1a7a6ff','fae03364-b3a8-4a3d-b4b2-02235e201ecb','0439062d-2030-4df6-a00e-626bb772cca3','2016-02-19 16:29:48'),('9c4827bf-91b3-4b79-b27f-9b044b8bfeb7','0439062d-2030-4df6-a00e-626bb772cca3','fae03364-b3a8-4a3d-b4b2-02235e201ecb','2016-02-19 16:29:48'),('a4cb2003-9f81-46e9-955e-58af306a9ef1','7529134e-8d13-4e93-9a09-2aa1de4a21d4','3da74019-960e-4c4b-804e-35319d6e68ef','2016-02-19 16:29:48'),('cade871f-335f-47ed-a4c0-bc73395e5c14','fae03364-b3a8-4a3d-b4b2-02235e201ecb','7529134e-8d13-4e93-9a09-2aa1de4a21d4','2016-02-19 16:29:48'),('e3213dbb-5cde-4229-9c80-6083f3625111','3da74019-960e-4c4b-804e-35319d6e68ef','7529134e-8d13-4e93-9a09-2aa1de4a21d4','2016-02-19 16:29:48');

/*Table structure for table `schema_version` */

DROP TABLE IF EXISTS `schema_version`;

CREATE TABLE `schema_version` (
  `name` varchar(255) NOT NULL,
  `version` bigint(20) unsigned NOT NULL,
  `migration` tinyint(1) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `schema_version` */

insert  into `schema_version`(`name`,`version`,`migration`) values ('',2,0);

/*Table structure for table `sys_group` */

DROP TABLE IF EXISTS `sys_group`;

CREATE TABLE `sys_group` (
  `id` char(36) NOT NULL,
  `name` varchar(128) NOT NULL,
  `desc` varchar(1024) NOT NULL,
  `is_system` tinyint(1) NOT NULL DEFAULT '0',
  `node` char(36) DEFAULT NULL,
  `creation_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `sys_group_node_fk` (`node`),
  CONSTRAINT `sys_group_node_fk` FOREIGN KEY (`node`) REFERENCES `cm_node` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_group` */

insert  into `sys_group`(`id`,`name`,`desc`,`is_system`,`node`,`creation_time`) values ('c0a64940-f174-4a1c-a4b3-acdc05f854a6','zdh_user_group','自动化大厦用户组',0,NULL,'2016-02-19 16:29:48'),('c3be7f2b-064f-416a-9d6f-d48d7e635077','zdh_admin_group','自动化大厦管理员组',0,NULL,'2016-02-19 16:29:48'),('eb5a4c7f-a7ef-4fac-ab66-f71fb87b2892','sys_admin_group','系统管理员组',1,NULL,'2016-02-19 16:29:48');

/*Table structure for table `sys_group2role` */

DROP TABLE IF EXISTS `sys_group2role`;

CREATE TABLE `sys_group2role` (
  `object_id` char(36) NOT NULL,
  `value` varchar(64) NOT NULL,
  KEY `sys_group2role_value_fk` (`value`),
  KEY `object_id_i` (`object_id`),
  CONSTRAINT `sys_group2role_object_id_fk` FOREIGN KEY (`object_id`) REFERENCES `sys_group` (`id`) ON DELETE CASCADE,
  CONSTRAINT `sys_group2role_value_fk` FOREIGN KEY (`value`) REFERENCES `sys_role` (`name`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_group2role` */

insert  into `sys_group2role`(`object_id`,`value`) values ('eb5a4c7f-a7ef-4fac-ab66-f71fb87b2892','sys_admin'),('c3be7f2b-064f-416a-9d6f-d48d7e635077','sys_user'),('c0a64940-f174-4a1c-a4b3-acdc05f854a6','community_admin');

/*Table structure for table `sys_group2user` */

DROP TABLE IF EXISTS `sys_group2user`;

CREATE TABLE `sys_group2user` (
  `object_id` char(36) NOT NULL,
  `value` char(36) NOT NULL,
  KEY `sys_group2user_value_fk` (`value`),
  KEY `object_id_i` (`object_id`),
  CONSTRAINT `sys_group2user_object_id_fk` FOREIGN KEY (`object_id`) REFERENCES `sys_group` (`id`) ON DELETE CASCADE,
  CONSTRAINT `sys_group2user_value_fk` FOREIGN KEY (`value`) REFERENCES `sys_user` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_group2user` */

insert  into `sys_group2user`(`object_id`,`value`) values ('eb5a4c7f-a7ef-4fac-ab66-f71fb87b2892','85c80166-615a-456a-be07-431aaf0bfe91'),('c3be7f2b-064f-416a-9d6f-d48d7e635077','e0f5a985-088b-4bc1-8df9-cab11c9ecf91');

/*Table structure for table `sys_permission` */

DROP TABLE IF EXISTS `sys_permission`;

CREATE TABLE `sys_permission` (
  `name` varchar(64) NOT NULL,
  `desc` varchar(1024) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_permission` */

insert  into `sys_permission`(`name`,`desc`) values ('ad','广告操作'),('community','社区操作'),('entrance','编辑门禁'),('role','角色管理操作'),('user','用户操作');

/*Table structure for table `sys_role` */

DROP TABLE IF EXISTS `sys_role`;

CREATE TABLE `sys_role` (
  `name` varchar(64) NOT NULL,
  `desc` varchar(1024) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_role` */

insert  into `sys_role`(`name`,`desc`) values ('community_admin','社区管理员'),('community_user','社区用户'),('sys_admin','系统管理员'),('sys_user','系统用户');

/*Table structure for table `sys_role2user` */

DROP TABLE IF EXISTS `sys_role2user`;

CREATE TABLE `sys_role2user` (
  `object_id` varchar(64) NOT NULL,
  `index` bigint(20) unsigned NOT NULL,
  `value` char(36) NOT NULL,
  KEY `sys_role2user_value_fk` (`value`),
  KEY `object_id_i` (`object_id`),
  KEY `index_i` (`index`),
  CONSTRAINT `sys_role2user_object_id_fk` FOREIGN KEY (`object_id`) REFERENCES `sys_role` (`name`) ON DELETE CASCADE,
  CONSTRAINT `sys_role2user_value_fk` FOREIGN KEY (`value`) REFERENCES `sys_user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_role2user` */

/*Table structure for table `sys_role_permission` */

DROP TABLE IF EXISTS `sys_role_permission`;

CREATE TABLE `sys_role_permission` (
  `id` char(36) NOT NULL,
  `mode` int(11) NOT NULL,
  `role` varchar(64) NOT NULL,
  `perm` varchar(64) NOT NULL,
  `creation_time` datetime DEFAULT NULL,
  `valid_thru_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `sys_role_permission_role_fk` (`role`),
  KEY `sys_role_permission_perm_fk` (`perm`),
  CONSTRAINT `sys_role_permission_perm_fk` FOREIGN KEY (`perm`) REFERENCES `sys_permission` (`name`),
  CONSTRAINT `sys_role_permission_role_fk` FOREIGN KEY (`role`) REFERENCES `sys_role` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_role_permission` */

insert  into `sys_role_permission`(`id`,`mode`,`role`,`perm`,`creation_time`,`valid_thru_time`) values ('0cd9119a-7793-488d-a0cc-01b6c454fa1c',1,'community_user','ad','2016-02-19 16:29:48','2017-02-18 16:29:48'),('1bebb867-bb7c-4a69-8262-14b38a5b36c6',5,'community_admin','user','2016-02-19 16:29:48','2017-02-18 16:29:48'),('2320fc10-5721-4bc9-a3a1-5de327dde6ce',1,'sys_user','role','2016-02-19 16:29:48','2017-02-18 16:29:48'),('238fc110-ae52-4c16-8663-1953b86c8287',15,'sys_admin','ad','2016-02-19 16:29:48','2017-02-18 16:29:48'),('2cf71de2-4ce1-40b2-90ec-d4e3be984c52',1,'community_user','entrance','2016-02-19 16:29:48','2017-02-18 16:29:48'),('34d18047-33a7-4b20-ba0b-58a26c06fd79',15,'sys_admin','user','2016-02-19 16:29:48','2017-02-18 16:29:48'),('5aaa129e-c3f1-4aee-84a9-26a34d7b737f',15,'sys_admin','entrance','2016-02-19 16:29:48','2017-02-18 16:29:48'),('6696be30-1cd2-40ea-b346-75ed8d249217',1,'sys_user','entrance','2016-02-19 16:29:48','2017-02-18 16:29:48'),('723cfb60-59b0-417a-aae4-b0cc8688a482',15,'community_admin','entrance','2016-02-19 16:29:48','2017-02-18 16:29:48'),('74ed0490-8561-471a-8c3e-90cda5f6c78a',15,'community_admin','community','2016-02-19 16:29:48','2017-02-18 16:29:48'),('8c888b34-3064-4198-a793-d7bfbbe4e712',15,'sys_admin','community','2016-02-19 16:29:48','2017-02-18 16:29:48'),('923ddd34-a8ac-4da1-bbf2-f0639e6e60ed',15,'sys_admin','role','2016-02-19 16:29:48','2017-02-18 16:29:48'),('a038ba8a-ed95-4452-b81a-2b14a7b79e0a',1,'community_user','user','2016-02-19 16:29:48','2017-02-18 16:29:48'),('a112666b-aa85-4fee-829a-b49830c8780e',1,'community_user','community','2016-02-19 16:29:48','2017-02-18 16:29:48'),('bb3caea0-171b-485e-af64-91b20f8c3f0c',15,'community_admin','ad','2016-02-19 16:29:48','2017-02-18 16:29:48'),('c9b7f250-b063-41ca-89ec-c163f65420f3',1,'sys_user','user','2016-02-19 16:29:48','2017-02-18 16:29:48'),('d4466dc6-cc30-44ef-874b-53a7763fa605',1,'sys_user','community','2016-02-19 16:29:48','2017-02-18 16:29:48'),('e8ceb4ea-d871-439f-bcd8-00750cf59b40',1,'sys_user','ad','2016-02-19 16:29:48','2017-02-18 16:29:48');

/*Table structure for table `sys_user` */

DROP TABLE IF EXISTS `sys_user`;

CREATE TABLE `sys_user` (
  `id` char(36) NOT NULL,
  `username` varchar(16) NOT NULL,
  `cellphone` char(11) NOT NULL,
  `password` varchar(64) NOT NULL,
  `fullname` varchar(32) NOT NULL,
  `code` varchar(64) NOT NULL,
  `email` varchar(128) NOT NULL,
  `creator` char(36) DEFAULT NULL,
  `creation_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `sys_user_creator_fk` (`creator`),
  CONSTRAINT `sys_user_creator_fk` FOREIGN KEY (`creator`) REFERENCES `sys_user` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_user` */

insert  into `sys_user`(`id`,`username`,`cellphone`,`password`,`fullname`,`code`,`email`,`creator`,`creation_time`) values ('85c80166-615a-456a-be07-431aaf0bfe91','fjw','18611784218','e10adc3949ba59abbe56e057f20f883e','fangjianwei','110105198202020002','fjw@qq.com',NULL,'2016-02-19 16:29:48'),('e0f5a985-088b-4bc1-8df9-cab11c9ecf91','dirk','13810955224','e10adc3949ba59abbe56e057f20f883e','changcuihai','110105198202020002','dirk@163.com','85c80166-615a-456a-be07-431aaf0bfe91','2016-02-19 16:29:48');

/*Table structure for table `sys_user_audit_data` */

DROP TABLE IF EXISTS `sys_user_audit_data`;

CREATE TABLE `sys_user_audit_data` (
  `object_id` char(36) NOT NULL,
  `index` bigint(20) unsigned NOT NULL,
  `value` char(36) NOT NULL,
  KEY `object_id_i` (`object_id`),
  KEY `index_i` (`index`),
  KEY `sys_user_audit_data_value_fk` (`value`),
  CONSTRAINT `sys_user_audit_data_object_id_fk` FOREIGN KEY (`object_id`) REFERENCES `sys_user` (`id`) ON DELETE CASCADE,
  CONSTRAINT `sys_user_audit_data_value_fk` FOREIGN KEY (`value`) REFERENCES `minie_audit_v0`.`sys_audit_backend` (`obj_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_user_audit_data` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
