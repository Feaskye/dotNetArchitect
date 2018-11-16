/*
SQLyog Ultimate v12.09 (64 bit)
MySQL - 5.7.22 : Database - ocelotgetwaydb
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`ocelotgetwaydb` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `ocelotgetwaydb`;

/*Table structure for table `ahphconfigreroutes` */

DROP TABLE IF EXISTS `ahphconfigreroutes`;

CREATE TABLE `ahphconfigreroutes` (
  `CtgRouteId` int(11) NOT NULL AUTO_INCREMENT COMMENT '配置路由主键',
  `AhphId` int(11) DEFAULT NULL COMMENT '网关主键',
  `ReRouteId` int(11) DEFAULT NULL COMMENT '路由主键',
  PRIMARY KEY (`CtgRouteId`),
  KEY `FK_Relationship_4` (`AhphId`),
  KEY `FK_Relationship_5` (`ReRouteId`),
  CONSTRAINT `FK_Relationship_4` FOREIGN KEY (`AhphId`) REFERENCES `ahphglobalconfiguration` (`AhphId`),
  CONSTRAINT `FK_Relationship_5` FOREIGN KEY (`ReRouteId`) REFERENCES `ahphreroute` (`ReRouteId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COMMENT='网关-路由,可以配置多个网关和多个路由';

/*Data for the table `ahphconfigreroutes` */

insert  into `ahphconfigreroutes`(`CtgRouteId`,`AhphId`,`ReRouteId`) values (2,1,1),(3,2,4);

/*Table structure for table `ahphglobalconfiguration` */

DROP TABLE IF EXISTS `ahphglobalconfiguration`;

CREATE TABLE `ahphglobalconfiguration` (
  `AhphId` int(11) NOT NULL AUTO_INCREMENT COMMENT '网关主键',
  `GatewayName` varchar(100) NOT NULL COMMENT '网关名称',
  `RequestIdKey` varchar(100) DEFAULT NULL COMMENT '全局请求默认key',
  `BaseUrl` varchar(100) DEFAULT NULL COMMENT '请求路由根地址',
  `DownstreamScheme` varchar(50) DEFAULT NULL COMMENT '下游使用架构',
  `ServiceDiscoveryProvider` varchar(500) DEFAULT NULL COMMENT '服务发现全局配置,值为配置json',
  `LoadBalancerOptions` varchar(500) DEFAULT NULL COMMENT '全局负载均衡配置',
  `HttpHandlerOptions` varchar(500) DEFAULT NULL COMMENT 'Http请求配置',
  `QoSOptions` varchar(200) DEFAULT NULL COMMENT '请求安全配置,超时、重试、熔断',
  `IsDefault` int(11) NOT NULL DEFAULT '0' COMMENT '是否默认配置, 1 默认 0 默认',
  `InfoStatus` int(11) NOT NULL DEFAULT '1' COMMENT '当前状态, 1 有效 0 无效',
  PRIMARY KEY (`AhphId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COMMENT='网关全局配置表';

/*Data for the table `ahphglobalconfiguration` */

insert  into `ahphglobalconfiguration`(`AhphId`,`GatewayName`,`RequestIdKey`,`BaseUrl`,`DownstreamScheme`,`ServiceDiscoveryProvider`,`LoadBalancerOptions`,`HttpHandlerOptions`,`QoSOptions`,`IsDefault`,`InfoStatus`) values (1,'test Gateway','test_gateway',NULL,NULL,NULL,NULL,NULL,NULL,1,1),(2,'WebApiA','WebApiA',NULL,NULL,NULL,NULL,NULL,NULL,1,1);

/*Table structure for table `ahphreroute` */

DROP TABLE IF EXISTS `ahphreroute`;

CREATE TABLE `ahphreroute` (
  `ReRouteId` int(11) NOT NULL AUTO_INCREMENT COMMENT '路由主键',
  `ItemId` int(11) DEFAULT NULL COMMENT '分类主键',
  `UpstreamPathTemplate` varchar(150) NOT NULL COMMENT '上游路径模板，支持正则',
  `UpstreamHttpMethod` varchar(50) NOT NULL COMMENT '上游请求方法数组格式',
  `UpstreamHost` varchar(100) DEFAULT NULL COMMENT '上游域名地址',
  `DownstreamScheme` varchar(50) NOT NULL COMMENT '下游使用架构',
  `DownstreamPathTemplate` varchar(200) NOT NULL COMMENT '下游路径模板,与上游正则对应',
  `DownstreamHostAndPorts` varchar(500) DEFAULT NULL COMMENT '下游请求地址和端口,静态负载配置',
  `AuthenticationOptions` varchar(300) DEFAULT NULL COMMENT '授权配置,是否需要认证访问',
  `RequestIdKey` varchar(100) DEFAULT NULL COMMENT '全局请求默认key',
  `CacheOptions` varchar(200) DEFAULT NULL COMMENT '缓存配置,常用查询和再次配置缓存',
  `ServiceName` varchar(100) DEFAULT NULL COMMENT '服务发现名称,启用服务发现时生效',
  `LoadBalancerOptions` varchar(500) DEFAULT NULL COMMENT '全局负载均衡配置',
  `QoSOptions` varchar(200) DEFAULT NULL COMMENT '请求安全配置,超时、重试、熔断',
  `DelegatingHandlers` varchar(200) DEFAULT NULL COMMENT '委托处理方法,特定路由定义委托单独处理',
  `Priority` int(11) DEFAULT NULL COMMENT '路由优先级,多个路由匹配上，优先级高的先执行',
  `InfoStatus` int(11) NOT NULL DEFAULT '1' COMMENT '当前状态, 1 有效 0 无效',
  PRIMARY KEY (`ReRouteId`),
  KEY `FK_分类路由信息` (`ItemId`),
  CONSTRAINT `FK_分类路由信息` FOREIGN KEY (`ItemId`) REFERENCES `ahphreroutesitem` (`ItemId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1 COMMENT='路由配置表';

/*Data for the table `ahphreroute` */

insert  into `ahphreroute`(`ReRouteId`,`ItemId`,`UpstreamPathTemplate`,`UpstreamHttpMethod`,`UpstreamHost`,`DownstreamScheme`,`DownstreamPathTemplate`,`DownstreamHostAndPorts`,`AuthenticationOptions`,`RequestIdKey`,`CacheOptions`,`ServiceName`,`LoadBalancerOptions`,`QoSOptions`,`DelegatingHandlers`,`Priority`,`InfoStatus`) values (1,1,'/api/values','[\"GET\"]','','Http','/api/Values','[{\"Host\": \"localhost\",\"Port\": 61492 }]',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1),(4,2,'/apia/user','[\"GET\"]','','Http','/api/user','[{\"Host\": \"localhost\",\"Port\": 65370 }]',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1);

/*Table structure for table `ahphreroutesitem` */

DROP TABLE IF EXISTS `ahphreroutesitem`;

CREATE TABLE `ahphreroutesitem` (
  `ItemId` int(11) NOT NULL AUTO_INCREMENT COMMENT '分类主键',
  `ItemName` varchar(100) NOT NULL COMMENT '分类名称',
  `ItemDetail` varchar(500) DEFAULT NULL COMMENT '分类描述',
  `ItemParentId` int(11) DEFAULT NULL COMMENT '上级分类,顶级节点为空',
  `InfoStatus` int(11) NOT NULL DEFAULT '1' COMMENT '当前状态, 1 有效 0 无效',
  PRIMARY KEY (`ItemId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COMMENT='路由分类表';

/*Data for the table `ahphreroutesitem` */

insert  into `ahphreroutesitem`(`ItemId`,`ItemName`,`ItemDetail`,`ItemParentId`,`InfoStatus`) values (1,'test cate',NULL,NULL,1),(2,'WebApiA',NULL,NULL,1);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
