DELETE FROM FLAG WHERE KEY_TEMPL = 1;    
INSERT INTO FLAG (KEY_TEMPL,NAME_DOC,DFLAG,TYP_FLAG,ID_FLAG,X_FLAG,X_CORD,Y_CORD,WIDTH,N_PAGE)
VALUES
(1,'Informativa_Clienti Massa Vecchia','al trattamento delle mie immagini o video, per le finalità e nei limiti indicati dalla menzionata informativa','C',0,NULL,145,290,10,2),
(1,'Informativa_Clienti Massa Vecchia','al trattamento delle mie immagini o video, per le finalità e nei limiti indicati dalla menzionata informativa','C',0,'X',63,290,10,2),

(1,'Informativa_Clienti Massa Vecchia','al trattamento dei miei dati personali per finalità di marketing e pubblicità nei limiti indicati dalla menzionata informativa','C',1,'X',63,350,10,2),
(1,'Informativa_Clienti Massa Vecchia','al trattamento dei miei dati personali per finalità di marketing e pubblicità nei limiti indicati dalla menzionata informativa','C',1,NULL,145,350,10,2),

 					     
(1,'Informativa_Clienti Massa Vecchia','NOME','T',NULL,NULL,260,80,280,2),			-- NOME 
(1,'Informativa_Clienti Massa Vecchia','COGNOME','T',NULL,NULL,320,80,200,2),		-- COGNOME
(1,'Informativa_Clienti Massa Vecchia','COD_FIS','T',NULL,NULL,410,80,200,2),		-- COD_FISCALE'

					     
(1,'Informativa_Clienti Massa Vecchia','DAY','T',NULL,NULL,130,170,50,2),			-- DATA	GIORNO
(1,'Informativa_Clienti Massa Vecchia','MONTH','T',NULL,NULL,155,170,50,2),			-- DATA	MESE	
(1,'Informativa_Clienti Massa Vecchia','YEAR','T',NULL,NULL,185,170,50,2),			-- DATA ANNO
 
					     
(1,'Informativa_Clienti Massa Vecchia','FIRMA','F',NULL,NULL,300,140,140,2)		-- FIRMA
					     
						 