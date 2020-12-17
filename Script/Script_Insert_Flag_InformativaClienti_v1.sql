DELETE FROM FLAG WHERE KEY_TEMPL = 1;    
INSERT INTO FLAG (KEY_TEMPL,NAME_DOC,DFLAG,TYP_FLAG,ID_FLAG,X_FLAG,X_CORD,Y_CORD,WIDTH,N_PAGE)
VALUES
(1,'Informativa_Clienti','al trattamento di particolari categorie di dati personali (sensibili), per le finalità e nei limiti indicati dalla menzionata informativa','C',0,NULL,145,640,10,1),
(1,'Informativa_Clienti','al trattamento di particolari categorie di dati personali (sensibili), per le finalità e nei limiti indicati dalla menzionata informativa','C',0,'X',63,640,10,1),

(1,'Informativa_Clienti','al trattamento delle mie immagini o video, per le finalità e nei limiti indicati dalla menzionata informativa','C',1,NULL,145,607,10,1),
(1,'Informativa_Clienti','al trattamento delle mie immagini o video, per le finalità e nei limiti indicati dalla menzionata informativa','C',1,'X',63,607,10,1),

(1,'Informativa_Clienti','al trattamento dei miei dati personali per finalità di marketing e pubblicità nei limiti indicati dalla menzionata informativa','C',2,NULL,145,548,10,1),
(1,'Informativa_Clienti','al trattamento dei miei dati personali per finalità di marketing e pubblicità nei limiti indicati dalla menzionata informativa','C',2,'X',63,548,10,1),

(1,'Informativa_Clienti','NOME','T',NULL,NULL,300,275,500,1),			-- NOME 
(1,'Informativa_Clienti','COGNOME','T',NULL,NULL,340,275,200,1),			-- COGNOME
(1,'Informativa_Clienti','CITY','T',NULL,NULL,40,425,200,1),			-- CITTA'
(1,'Informativa_Clienti','COD_FIS','T',NULL,NULL,420,275,200,1),			-- COD_FISCALE'

(1,'Informativa_Clienti','DAY','T',NULL,NULL,180,425,50,1),				-- DATA	GIORNO
(1,'Informativa_Clienti','MONTH','T',NULL,NULL,210,425,50,1),			-- DATA	MESE	
(1,'Informativa_Clienti','YEAR','T',NULL,NULL,240,425,50,1),			-- DATA ANNO

(1,'Informativa_Clienti','FIRMA','F',NULL,NULL,300,380,140,1),			-- FIRMA

(1,'Informativa_Clienti','PERS1','T',NULL,NULL,40,425,200,1),			-- campo personale 1
(1,'Informativa_Clienti','PERS2','T',NULL,NULL,40,425,200,1),			-- campo personale 2
(1,'Informativa_Clienti','PERS3','T',NULL,NULL,40,425,200,1),			-- campo personale 3
(1,'Informativa_Clienti','PERS4','T',NULL,NULL,40,425,200,1),			-- campo personale 4
(1,'Informativa_Clienti','PERS5','T',NULL,NULL,40,425,200,1),			-- campo personale 5
(1,'Informativa_Clienti','PERS6','T',NULL,NULL,40,425,200,1);			-- campo personale 6
