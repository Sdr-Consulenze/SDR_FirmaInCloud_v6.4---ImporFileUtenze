DELETE FROM FLAG WHERE KEY_TEMPL = 1;    
INSERT INTO FLAG (KEY_TEMPL,NAME_DOC,DFLAG,TYP_FLAG,ID_FLAG,X_FLAG,X_CORD,Y_CORD,WIDTH,N_PAGE)
VALUES
(1,'Privacy Villa CasaGrande','the processing of my personal data for the accommodation and accessories services such as the use of the Wellness Center requested by you.','C',0,NULL,180,510,10,1),
(1,'Privacy Villa CasaGrande','the processing of my personal data for the accommodation and accessories services such as the use of the Wellness Center requested by you.','C',0,'X',95,510,10,1),

(1,'Privacy Villa CasaGrande','the eventual treatment of my sensitive data for the accommodation and accessories services such as the use of the Wellness Center requested by you','C',1,NULL,180,450,10,1),
(1,'Privacy Villa CasaGrande','the eventual treatment of my sensitive data for the accommodation and accessories services such as the use of the Wellness Center requested by you','C',1,'X',95,450,10,1),

(1,'Privacy Villa CasaGrande','the external communication of data relating to my stay to the exclusive purpose of allowing the function of receiving messages and calls addressed to me','C',2,NULL,180,380,10,1),
(1,'Privacy Villa CasaGrande','the external communication of data relating to my stay to the exclusive purpose of allowing the function of receiving messages and calls addressed to me','C',2,'X',95,380,10,1),

(1,'Privacy Villa CasaGrande','the preservation of my generalities in order to accelerate the registration procedures in case of my subsequent stays','C',3,NULL,180,310,10,1),
(1,'Privacy Villa CasaGrande','the preservation of my generalities in order to accelerate the registration procedures in case of my subsequent stays','C',3,'X',95,310,10,1),

(1,'Privacy Villa CasaGrande','the sending to my domicile or to my e-mail address, periodic documentation on tariffs and offers','C',4,NULL,180,250,10,1),
(1,'Privacy Villa CasaGrande','the sending to my domicile or to my e-mail address, periodic documentation on tariffs and offers','C',4,'X',95,250,10,1),


(1,'Privacy Villa CasaGrande','NOME','T',NULL,NULL,150,578,500,1),				-- NOME 
(1,'Privacy Villa CasaGrande','COGNOME','T',NULL,NULL,200,578,200,1),				-- COGNOME

(1,'Privacy Villa CasaGrande','PERS1','T',NULL,NULL,480,470,200,1),				-- campo personale 1 (TARGA)
(1,'Privacy Villa CasaGrande','CELL','T',NULL,NULL,290,470,200,1),				-- Cellulare
 
(1,'Privacy Villa CasaGrande','MAIL','T',NULL,NULL,300,220,250,1),				-- MAIL

(1,'Privacy Villa CasaGrande','DAY','T',NULL,NULL,100,180,50,1),				-- DATA	GIORNO
(1,'Privacy Villa CasaGrande','MONTH','T',NULL,NULL,150,180,50,1),				-- DATA	MESE	
(1,'Privacy Villa CasaGrande','YEAR','T',NULL,NULL,200,180,50,1),				-- DATA ANNO
	
(1,'Privacy Villa CasaGrande','FIRMA','F',NULL,NULL,100,130,140,1);				-- FIRMA

