DROP TABLE UTENZE;
CREATE TABLE FIRMAINCLOUD.UTENZE (  
	KEY_UTE	 	INT AUTO_INCREMENT ,
	NOME	 	VARCHAR(30) NOT NULL,		-- Nome
	COGNOME	 	VARCHAR(30) NOT NULL,		-- Cognome
	DTINS 		VARCHAR(8)  NOT NULL,		-- Data inserimento
	DTNAS		VARCHAR(8),			-- Data Nascita	
	RAG_SOCIALE	VARCHAR(30),			-- Ragione Sociale
	TYP_UTE		VARCHAR(1),			-- Soggetto giuridico / persona fisica: 'P' Privato, 'A' Azienda
	P_IVA		VARCHAR(11),			-- PIVA Titolare  
	COD_FIS		VARCHAR(16),			-- CF Titolare 	
	KEY_REG 	INT,				-- Chiave tabella regioni
	KEY_COMUNE 	INT,				-- Chiave tabella comuni 
	KEY_PRO 	INT,				-- Chiave tabella province
	
	SST 		VARCHAR(10),			-- Toponimo
	STRADA 		VARCHAR(80),			-- Strada 	
	NCN 		VARCHAR(3),				-- numero civico	                                                       	
	
	PERS1 		VARCHAR(80),
	PERS2 		VARCHAR(80),
	PERS3 		VARCHAR(80),
	PERS4 		VARCHAR(80),
	PERS5 		VARCHAR(80),
	
	CELL	 	VARCHAR(16),			-- Telefono cellulare
	MAIL 		VARCHAR(30),
	KEY_TEMPL	INT,				-- Chiave permessi
	X_LINQ  	VARCHAR(1), 			-- Associa documento - null non associato / X associato
	DT_LINQ		VARCHAR(8),			-- Data ultima associazione
	OPER_S 		VARCHAR(16),		    	-- Operatore ultima modifica		
	PRIMARY KEY(KEY_UTE)
);
 
 DROP TABLE LOGIN;
CREATE TABLE FIRMAINCLOUD.LOGIN (
	KEY_LOG 	INT AUTO_INCREMENT,
	USER_ID 	VARCHAR(80) NOT NULL, 
	PASSWORD_ID 	VARCHAR(80) NOT NULL, 
	X_PERMISSION 	VARCHAR(1),
	X_ADMIN 	VARCHAR(1),
	PRIMARY KEY(KEY_LOG,USER_ID)
);

DROP TABLE LOGIN_ACCESS;
CREATE TABLE FIRMAINCLOUD.LOGIN_ACCESS (
	KEY_LOG_ACC 	INT NOT NULL,	
	DT_LOGIN	VARCHAR(12) NOT NULL,
	PRIMARY KEY(KEY_LOG_ACC,DT_LOGIN)
);
 
DROP TABLE REGIONI;
CREATE TABLE FIRMAINCLOUD.REGIONI (
	KEY_REG INT AUTO_INCREMENT,
	DESREG VARCHAR(80) NOT NULL, 
	PRIMARY KEY(KEY_REG)
);
 
 DROP TABLE PROVINCE;
 CREATE TABLE FIRMAINCLOUD.PROVINCE (
	KEY_PRO INT AUTO_INCREMENT,
	DESPRO VARCHAR(80) NOT NULL, 
	SIGPRO VARCHAR(4) NOT NULL,
	KEY_REG INT NOT NULL,
	PRIMARY KEY(KEY_PRO)
);
 
 DROP TABLE TEMPLATE;
CREATE TABLE FIRMAINCLOUD.TEMPLATE(  
	KEY_TEMPL 	INT AUTO_INCREMENT,
	DESCR	 	VARCHAR(60) NOT NULL,		-- Nome
	PATH		VARCHAR(512) NOT NULL,		-- Path File	
	DTINS 		VARCHAR(12)  NOT NULL,		-- Data inserimento	
	X_SIGN		VARCHAR(1),	
	PRIMARY KEY(KEY_TEMPL) 
);

DROP TABLE TEMPLATE_LINQ;
CREATE TABLE FIRMAINCLOUD.TEMPLATE_LINQ(  
	KEY_TEMPL_L	INT AUTO_INCREMENT,	
	KEY_UTE 	INT,		
	DTSIGN 		VARCHAR(8)  NOT NULL,		-- Data firma
	PRIMARY KEY(KEY_TEMPL_L,KEY_UTE) 
); 
 
 DROP TABLE CONFIG;
CREATE TABLE FIRMAINCLOUD.CONFIG ( 
	CONF_PARAM 	VARCHAR(6) NOT NULL,
	CONF_VALUE 	NVARCHAR(255),
	PRIMARY KEY(CONF_PARAM)
);  

DROP TABLE FLAG;
CREATE TABLE FIRMAINCLOUD.FLAG ( 
	KEY_FLAG	INT AUTO_INCREMENT,
	KEY_TEMPL 	VARCHAR(6),
	DFLAG 		VARCHAR(512) NOT NULL,		-- Descrizione Flag
	X_FLAG  	VARCHAR(1),			-- Abilita flag  - null non associato / X associato 
	PRIMARY KEY(KEY_FLAG)
);  
 
INSERT INTO REGIONI(KEY_REG,DESREG) 
VALUES
(1,'Abruzzo'),	
(2,'Basilicata'),	
(3,'Calabria'),	
(4,'Campania'), 
(5,'Emilia-Romagna'),	 
(6,'Friuli-Venezia Giulia'),	
(7,'Lazio'),	
(8,'Liguria'),
(9,'Lombardia'),	
(10,'Marche'),		
(11,'Molise'),	
(12,'Piemonte'),
(13,'Puglia'),	 
(14,'Sardegna'), 
(15,'Sicilia'),	 
(16,'Toscana'),
(17,'Trentino-Alto Adige'),	
(18,'Umbria'),	
(19,'Valle Aosta'),	
(20,'Veneto');


INSERT INTO PROVINCE(KEY_PRO,SIGPRO,DESPRO,KEY_REG) 
VALUES
(1,'AG','Agrigento',15),
(2,'AL','ALessandria',12),
(3,'AN','Ancona',10),	
(4,'AO','Aosta',19),	
(5,'AQ','Aquila',1),
(6,'AR','Arezzo',16),	
(7,'AP','AscolI-Piceno',10),
(8,'AT','Asti',12),	
(9,'AV','Avellino',4),
(10,'BA','Bari',13),	
(11,'BT','Barletta',13),
(12,'BL','Belluno',20),	
(13,'BN','Benevento',4),
(14,'BG','Bergamo',9),
(15,'BI','Biella',12),	
(16,'BO','Bologna',5),
(17,'BZ','Bolzano',17),
(18,'BS','Brescia',9),	
(19,'BR','Brindisi',13),
(20,'CA','Cagliari',14),
(21,'CL','Caltanissetta',15),
(22,'CB','Campobasso',11),
(23,'CI','Carbonia',14),
(24,'CE','Caserta',4),	
(25,'CT','Catania',15),	
(26,'CZ','Catanzaro',3),
(27,'CH','Chieti',1),	
(28,'CO','Como',9),	
(29,'CS','Cosenza',3),	
(30,'CR','Cremona',9),	
(31,'KR','Crotone',3),	
(32,'CN','Cuneo',12),	
(33,'EN','Enna',15),	
(34,'FM','Fermo',10),	
(35,'FE','Ferrara',5),	
(36,'FI','Firenze',16),	
(37,'FG','Foggia',13),
(38,'FC','Forli-Cesenatico',5),
(39,'FR','Frosinone',7),
(40,'GE','Genova',8),	
(41,'GO','Gorizia',6),	
(42,'GR','Grosseto',16),
(43,'IM','Imperia',8),
(44,'IS','Isernia',11),	
(45,'SP','La-Spezia',8),
(46,'LT','Latina',7),	
(47,'LE','Lecce',13),	
(48,'LC','Lecco',9),	
(49,'LI','Livorno',16),	
(50,'LO','Lodi',9),	
(51,'LU','Lucca',16),	
(52,'MC','Macerata',10),
(53,'MN','Mantova',9),	
(54,'MS','Massa-Carrara',16),
(55,'MT','Matera',2),	
(56,'VS','Medio Campidano',14),
(57,'ME','Messina',15),	
(58,'MI','Milano',9),	
(59,'MO','Modena',5),	
(60,'MB','Monza-Brianza',9),
(61,'NA','Napoli',4),	
(62,'NO','Novara',12),	
(63,'NU','Nuoro',14),	
(64,'OG','Ogliastra',14),
(65,'OT','Olbia Tempio',14),
(66,'OR','Oristano',14),
(67,'PD','Padova',20),	
(68,'PA','Palermo',15),	
(69,'PR','Parma',5),	
(70,'PV','Pavia',9),	
(71,'PG','Perugia',18),	
(72,'PU','Pesaro-Urbino',10),
(73,'PE','Pescara',1),	
(74,'PC','Piacenza',5),
(75,'PI','Pisa',16),	
(76,'PT','Pistoia',16),	
(77,'PN','Pordenone',6),
(78,'PZ','Potenza',2),	
(79,'PO','Prato',16),	
(80,'RG','Ragusa',15),	
(81,'RA','Ravenna',5),	
(82,'RC','Reggio-Calabria',3),
(83,'RE','Reggio-Emilia',5),
(84,'RI','Rieti',7),	
(85,'RN','Rimini',5),	
(86,'Roma','Roma',7),
(87,'RO','Rovigo',20),
(88,'SA','Salerno',4),	
(89,'SS','Sassari',14),	
(90,'SV','Savona',8),
(91,'SI','Siena',16),
(92,'SR','Siracusa',15),
(93,'SO','Sondrio',9),	
(94,'TA','Taranto',13),	
(95,'TE','Teramo',1),	
(96,'TR','Terni',18),	
(97,'TO','Torino',12),	
(98,'TP','Trapani',15),	
(99,'TN','Trento',17),	
(100,'TV','Treviso',20),	
(101,'TS','Trieste',6),	
(102,'UD','Udine',6),	
(103,'VA','Varese',9),	
(104,'VE','Venezia',20),	
(105,'VB','Verbania',12),
(106,'VC','Vercelli',12),
(107,'VR','Verona',20),
(108,'VV','Vibo-Valentia',3),
(109,'VI','Vicenza',20),	
(110,'VT','Viterbo',7);	

INSERT INTO CONFIG (CONF_PARAM,CONF_VALUE) VALUES ('City','Grosseto');  

INSERT INTO CONFIG (CONF_PARAM,CONF_VALUE) VALUES ('COUNT',0);  

INSERT INTO LOGIN (USER_ID,PASSWORD_ID,X_PERMISSION,X_ADMIN) VALUES ('Lapo','Password','X','X');

INSERT INTO FLAG (KEY_TEMPL,DFLAG,X_FLAG) 
VALUES 
(1,'al trattamento di particolari categorie di dati personali (sensibili), per le finalità e nei limiti indicati dalla menzionata informativa','C',0,NULL,145,640,10),
(1,'al trattamento delle mie immagini o video, per le finalità e nei limiti indicati dalla menzionata informativa','C',1,NULL,145,640,10),
(1,'al trattamento dei miei dati personali per finalità di marketing e pubblicità nei limiti indicati dalla menzionata informativa','C',2,NULL,145,640,10),

(1,'al trattamento di particolari categorie di dati personali (sensibili), per le finalità e nei limiti indicati dalla menzionata informativa','C',0,'X',63,640,10),
(1,'al trattamento delle mie immagini o video, per le finalità e nei limiti indicati dalla menzionata informativa','C',1,'X',63,640,10),
(1,'al trattamento dei miei dati personali per finalità di marketing e pubblicità nei limiti indicati dalla menzionata informativa','C',2,'X',63,640,10);

(1,'NOME','T',NULL,NULL,300,275,500),			-- NOME 
(1,'COGNOME','T',NULL,NULL,40,425,200),			-- COGNOME
(1,'CITY','T',NULL,NULL,40,425,200),			-- CITTA'
(1,'COD_FIS','T',NULL,NULL,40,425,200),			-- COD_FISCALE'

(1,'DAY','T',NULL,NULL,180,425,50),				-- DATA	GIORNO
(1,'MONTH','T',NULL,NULL,210,425,50),			-- DATA	MESE	
(1,'YEAR','T',NULL,NULL,240,425,50);			-- DATA ANNO

(1,'pers1','T',NULL,NULL,40,425,200),			-- campo personale 1
(1,'pers2','T',NULL,NULL,40,425,200),			-- campo personale 2
(1,'pers3','T',NULL,NULL,40,425,200),			-- campo personale 3
(1,'pers4','T',NULL,NULL,40,425,200),			-- campo personale 4
(1,'pers5','T',NULL,NULL,40,425,200),			-- campo personale 5
(1,'pers6','T',NULL,NULL,40,425,200);			-- campo personale 6
 




