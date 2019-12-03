**Friendr**

ICT.SE.KBS.V19

Versie: 0.1.5

Versiebeheer
============

| **Versie** | **Aanpassing**                                | **Door**        | **Datum**  |
|------------|-----------------------------------------------|-----------------|------------|
| 0.1.0      | Document aangemaakt en basis opzet            | Marc Boerdijk   | 12/11/2019 |
| 0.1.1      | Database informatie                           | Guus Apeldoorn  | 19/11/2019 |
| 0.1.2      | Versiebeheer toegevoegd en document opgemaakt | Siem Bosgra     | 21/11/2019 |
| 0.1.3      | Uitleg klassendiagram, inleiding en testing   | Marc Boerdijk   | 21/11/2019 |
| 0.1.4      | ERD toegevoegd met toelichting                | Folkert Attema  | 21/11/2019 |
| 0.1.5      | Spellingscontrole                             | Patrick de Jong | 21/11/2019 |

Inleiding
=========

Voor het matchmakerproject is het nodig om een technisch ontwerp te schrijven om daarin te beschrijven hoe de applicatie technisch in elkaar zit. Het document beschrijft de databasestructuur, de opzet van de applicatie en de visuele ontwerpen van de applicatie. Dit zal dienen als een naslagdocument voor het project zodat het mogelijk is te zien wat het plan is met de applicatie. Door in dit document grondig de applicatie te beschrijven is het ook mogelijk om potentiele nieuwe team leden up-to-date te brengen.

Database
========

NoSQL
-----

Op grond van de object-georiënteerde basis van de gebruikte programmeertaal C\#, is het gebruik van een object-based datanotatie het overwegen waard. Door objecten op te slaan als zijnde een object in plaats van als zijnde een regel in een tabel, zorgen we ervoor dat de data makkelijk te vertalen is naar programmacode. Hierdoor wordt de code makkelijker te schrijven, wat o.a. verbeterde kwaliteit met zich mee brengt.

Aangezien de meeste server-client programma’s JSON gebruiken om data uit te wisselen tussen de server en de client, is een logische keuze om met behulp van JSON ook onze data op te slaan. Zo elimineren we meerdere vertaalslagen tussen de relationele database en de programmacode.

Accounts
========

De gebruikers kunnen hun profiel binnen de applicatie aanpassen en een wachtwoordverandering aangeven. Niet al hun gegevens zijn zichtbaar voor andere gebruikers. Het is bijvoorbeeld niet mogelijk om de email van een andere gebruiker te zien op zijn profiel.

Met een account kan de gebruiker ook aan geven dat hij geïnteresseerd is in een activiteit en daarvan op de hoogte worden gehouden.

Een gebruiker kan ook een aantal interesses en hobby’s selecteren, deze worden weer gegeven op het profiel.

Een account kan ook matchen met een ander account, dit moet ook worden vastgelegd in de database. Als accounts gematched zijn dan krijgen de gebruikers de mogelijkheid om met de gebruiker van het andere account te chatten. Accounts worden gematched gebaseerd op hun locatie en gedeelde interesses.

Zonder een account mag het niet mogelijk zijn om de applicatie te gebruiken buiten het inlogscherm om.

Activiteiten
============

Een activiteit kan worden aangemaakt door een gebruiker met een account, er kunnen hiervoor updates worden geschreven of geüpload die publiek zichtbaar zijn. Als een activiteit wordt geüpdatet dan krijgen alle mensen die hebben aangegeven in de activiteit geïnteresseerd te zijn een notificatie van de update.

Administrator
=============

Een administrator (Admin) is een type van account waarbij de admin in de beheer omgeving van de applicatie komt als hij ingelogd in plaats van de standaard gebruikers omgeving. De admin zal hier toegang hebben tot de gegevens van de gebruikers en kan accounts aanpassen. Een admin kan ook de gegevens van activiteiten aanpassen of de activiteiten verwijderen.

Of een account een administrator of niet is wordt aangegeven in de gegevens van een account.

Beveiliging
===========

Om te zorgen dat de input van gebruikers van de applicatie niet wordt gebruikt om aanpassingen te maken in de database die niet mogen, moeten de inputs worden gevalideerd. De validatie zal plaats vinden nadat tekst is in ingevoerd, er moet met behulp van reguliere expressies worden gecontroleerd dat er geen onverwachte leestekens in voorkomen. Er moet ook worden gecontroleerd of de input van het goede type is, er mag bijvoorbeeld geen string worden teruggegeven waar een integer wordt verwacht.

Authenticatie van de accounts die gebruikt worden is ook erg belangrijk. Er moet worden gezorgd dat er streng wordt gecontroleerd in de applicatie om te zorgen dat reguliere gebruikers niet in administratoronderdelen komen of dat administrators er niet bij kunnen.

Wachtwoorden van gebruikers moeten ook worden opgeslagen in de database nadat ze gehashed worden en niet in gewone tekst. Hiervoor heeft C\# een methode die kan worden versterkt met een salt.

Testing
=======

Voor het testen van de applicatie zullen we gebruik maken van twee soorten testen: unit testen en functioneel testen.

Voor het unit testing zullen verscheidene testen worden opgezet en uitgevoerd door gebruik te maken van Visual studio. Door te unit testen kunnen we verzekeren dat de applicatie intern en extern correct is.

De test cases voor het functioneel testen zijn van tevoren opgesteld om aan het einde van elke sprint te worden uitgevoerd wanneer van toepassing.

ERD
===

Hieronder is het ERD te vinden met daaronder de toelichting per tabel.

<img src="media/image2.png" width="480" height="463" />

Users

In de Userstabel is alle informatie van een gebruiker terug te vinden.

Elke gebruiker heeft een uniek e-mailadres. Omdat er een uniek e-mail per gebruiker is, is het e-mailveld de primary key geworden.

Bij het aanmaken van een account moet er een username en een password worden meegegeven om na het aanmelden in te kunnen loggen. Daarom zijn deze velden not null geworden.

Alle andere velden mogen null zijn. Deze velden bevatten locatiegegevens die vanwege gewenste anonimiteit weggelaten mogen worden.

Pictures
--------

In de Picturestabel krijgt elke picture een uniek ID. Dit zal fungeren als de primary key.

Een picture is van een user en daarom bevat de tabel een foreign key die verwijst naar de unieke identifier van een gebruiker: het e-mailadres.

Er wordt in de database naar een bestandslocatie verwezen. Daarom is er een pictureURLveld en niet een pictureveld die de afbeelding als blob opslaat. Het converteren van de blob kost tijd die bij een simpele verwijzing bespaard kan worden.

Een picture kan ook een profielfoto zijn. Dit is de foto die iedere gebruiker als icoon heeft. Om dit in de database op te slaan is ervoor gekozen om een boolean (TINYINT) te gebruiken voor dit veld. Om in de toekomst andere functionaliteit toe te laten is ervoor gekozen om het veld niet not null te maken. Zo zou er dus een derde functie van een foto kunnen zijn naast profielfoto (1, true) en fotoalbumfoto (0, false).

Matches
-------

In de Matchestabel worden de matches opgeslagen, maar ook de afwijzingen.

Elke match heeft een identificeerbaar matchID. Dit is tevens de primary key.

Bij een match zijn twee gebruikers betrokken. Deze zijn terug te vinden als userOne en userTwo.

Een match vindt plaats op een bepaalde datum. Hiervoor is een datumveld aangemaakt. Deze wordt pas gevuld op het moment dat beide personen geaccepteerd hebben of dat hij door een is afgewezen.

Een match kan zowel geaccepteerd als gewijzigd worden. Hiervoor is een veld accepted aangemaakt. Dit veld wordt gevuld met null als er een heeft geaccepteerd, met true als beide personen geaccepteerd hebben of met false als er een van beiden geweigerd heeft.

Hobbies
-------

In de Hobbiestabel worden de hobbies opgeslagen.

Elke hobby heeft een uniek hobbyID. Dit is de primary key.

Elke hobby heeft ook een hobbyName. Dit is de naam van de hobby. Dit veld mag niet null zijn omdat er anders een lege hobby ingevuld kan worden in de database. In verband met mogelijke uitbereidingen in de vorm van subgroepen is ervoor gekozen om er geen unique constraint op te zetten.

HobbyLines
----------

In de HobbyLinestabel worden de Userstabel en de Hobbiestabel aan elkaar verbonden. Verder heeft deze tabel (nog) geen functionaliteit. Deze tabel bestaat dus uit een dubbele primary key bestaande uit twee foreign keys: userEmail en hobbyID.

Chats
-----

In de Chatstabel worden de openstaande chats opgeslagen.

Elke chat heeft een ID en twee gebruikers. Er is voor een ID gekozen omdat er dan minder elementen in de Messagestabel opgeslagen hoeven te worden.

De beide profielen moeten ook opgeslagen worden in de tabel dus er zijn twee velden, één voor elke gebruiker.

Messages
--------

In de Messagestabel worden alle chatberichten opgeslagen.

Een chatbericht heeft een dubbele primary key. Hiervoor is gekozen om de limitatie van een int tegen te gaan. Een int kan maar een gelimiteerd aantal waarden aannemen. Gezien er veel chatberichten gestuurd kunnen worden, is het handig om een dubbele key te nemen om zo meer combinaties mogelijk te maken. Om te schetsen hoe snel een int vol zou kunnen raken wordt de volgende situatie geschetst: bij een actieve gebruikersbasis van 10.000 waarvan elke gebruiker per dag gemiddeld 100 berichten stuurt zal de int in net iets minder dan zes jaar vol zitten (2147483647/10000/100/365=5,8835168411). Bij een grotere gebruikersbasis van een miljoen zal dit al na 21 dagen zijn (2147483647/1000000/100=21.47483647).

Elk bericht heeft een sender. Er is voor sender gekozen omdat dat een logischere input geeft in de database gezien receiver/ontvanger afgeleid kan worden uit de chat.

Elk bericht bevat een bericht. Voor de tekst meegegeven in het bericht is het veld message aanwezig.

Voor het bewaren en weergeven van berichten is een timestamp essentieel. Zo is ook te achterhalen wanneer een bericht is verstuurd. Om dit relatief nauwkeurig gerealiseerd te krijgen is ervoor datetime gekozen.

Events
------

In de Eventstabel worden alle activiteiten opgeslagen.

Een activiteit heeft een uniek ID. Dit ID is de primary key

Een activiteit heeft een naam en een omschrijving. Deze velden zijn allebei verplicht, omdat het anders niet te zien is waar het over gaat.

Een organisator is ook verplicht. Dit is de persoon die de activiteit heeft aangemaakt. Dit veld mag ook niet leeg zijn. Dit is een foreign key naar de usertabel (email)

De activiteitdatum (en -tijd) is een veld dat null mag zijn. Zo kan er van tevoren niet bekend zijn wanneer iets precies plaats gaat vinden. In die gevallen moet een datumveld leeg kunnen blijven om later wel gevuld te worden.

EventParticipants
-----------------

De EventParticipantstabel bevat wie er naar welke activiteit gaat. Dit is puur een tussentabel zonder enige extra informatie. De key bestaat uit twee foreign keys die Events (eventID) linkt met Users (email).

EventComments
-------------

De EventCommentstabel bevat de berichten (comments) die geplaatst worden onder activiteiten.

Elk bericht heeft zijn eigen ID. Samen met het eventID, die tevens een foreign key is, vormen ze samen de primary key. Er is hier gekozen voor een dubbele primary key om, net als bij de Chat- en Messagetabel, het totaalaantal berichten te vergroten.

Elk bericht is geplaatst door iemand. Daarom is er dus een relatie met de Userstabel nodig. Daarom staat er een veld met userEmail. Dit is een foreign key.

Ook heeft een bericht een timestamp om te bepalen wanneer iets geplaatst is. In de applicatie zal dit bruikbaar zijn voor het sorteren op chronologische volgorde.

Er is een parentIDveld. Dit veld is nodig om “cascading comments” mogelijk te maken. Het houdt in dat het mogelijk is om berichten onder berichten te plaatsen. Dit veld bevat mogelijk een EventCommentID van het bovenliggende bericht. Sommige berichten hebben geen bovenliggend bericht, daarom moet het veld ook null kunnen zijn.

Klassendiagram
==============

<img src="media/image3.png" width="601" height="379" />

Een account van beide de reguliere gebruiker en de administrator erven de Abstract klasse AAcount en bouwen daarop verder. Een account kan een activity maken, verwijderen of aanpassen. Matches worden gevonden door gebruik te maken van de Matches klassen. De notifications klasse wordt gebruikt wanneer een activity wordt geüpdatet of er een match is gevonden.
