DELETE FROM FLAG WHERE KEY_TEMPL = 1;    
INSERT INTO FLAG (KEY_TEMPL,NAME_DOC,DFLAG,TYP_FLAG,ID_FLAG,X_FLAG,X_CORD,Y_CORD,WIDTH,N_PAGE)
VALUES
(1,'Privacy Villa CasaGrande','la struttura ricettiva al trattamento dei miei dati personali per i servizi di alloggio, uso del Centro Benessere e servizi accessori da lei richiesti.','C',0,NULL,180,510,10,1),
(1,'Privacy Villa CasaGrande','la struttura ricettiva al trattamento dei miei dati personali per i servizi di alloggio, uso del Centro Benessere e servizi accessori da lei richiesti.','C',0,'X',95,510,10,1),

(1,'Privacy Villa CasaGrande','la struttura ricettiva al trattamento eventuale dei miei dati sensibili per i servizi di alloggio, uso del Centro Benessere e servizi accessori da lei richiesti','C',1,NULL,180,450,10,1),
(1,'Privacy Villa CasaGrande','la struttura ricettiva al trattamento eventuale dei miei dati sensibili per i servizi di alloggio, uso del Centro Benessere e servizi accessori da lei richiesti','C',1,'X',95,450,10,1),

(1,'Privacy Villa CasaGrande','la struttura ricettiva alla comunicazione esterna di dati relativi al mio soggiorno al fine di consentire il ricevimento di messaggi e telefonate a me indirizzati','C',2,NULL,180,380,10,1),
(1,'Privacy Villa CasaGrande','la struttura ricettiva alla comunicazione esterna di dati relativi al mio soggiorno al fine di consentire il ricevimento di messaggi e telefonate a me indirizzati','C',2,'X',95,380,10,1),

(1,'Privacy Villa CasaGrande','la struttura ricettiva alla conservazione delle mie generalità al fine di accelerare le procedure di registrazione in caso di miei successivi soggiorni','C',3,NULL,180,310,10,1),
(1,'Privacy Villa CasaGrande','la struttura ricettiva alla conservazione delle mie generalità al fine di accelerare le procedure di registrazione in caso di miei successivi soggiorni','C',3,'X',95,310,10,1),

(1,'Privacy Villa CasaGrande','la struttura ricettiva ad inviare al mio domicilio o al mio indirizzo di posta elettronica, periodica documentazione sulle tariffe e sulle offerte praticate','C',4,NULL,180,250,10,1),
(1,'Privacy Villa CasaGrande','la struttura ricettiva ad inviare al mio domicilio o al mio indirizzo di posta elettronica, periodica documentazione sulle tariffe e sulle offerte praticate','C',4,'X',95,250,10,1),


(1,'Privacy Villa CasaGrande','NOME','T',NULL,NULL,150,578,500,1),				-- NOME 
(1,'Privacy Villa CasaGrande','COGNOME','T',NULL,NULL,200,578,200,1),				-- COGNOME

(1,'Privacy Villa CasaGrande','PERS1','T',NULL,NULL,480,470,200,1),				-- campo personale 1 (TARGA)
(1,'Privacy Villa CasaGrande','CELL','T',NULL,NULL,290,470,200,1),				-- Cellulare
 
(1,'Privacy Villa CasaGrande','MAIL','T',NULL,NULL,300,220,250,1),				-- MAIL

(1,'Privacy Villa CasaGrande','DAY','T',NULL,NULL,100,180,50,1),				-- DATA	GIORNO
(1,'Privacy Villa CasaGrande','MONTH','T',NULL,NULL,150,180,50,1),				-- DATA	MESE	
(1,'Privacy Villa CasaGrande','YEAR','T',NULL,NULL,200,180,50,1),				-- DATA ANNO
	
(1,'Privacy Villa CasaGrande','FIRMA','F',NULL,NULL,100,130,140,1);				-- FIRMA

