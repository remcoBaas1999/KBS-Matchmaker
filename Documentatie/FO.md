Functioneel ontwerp

Toelichting op functionele documentatie

**Friendr**

ICT.SE.KBS.V19

<img src="media/image1.png" width="600" height="379" />

Versie: 0.2.2

Versiebeheer
============

| **Versie** | **Aanpassing**                                                                              | **Door**        | **Datum**  |
|------------|---------------------------------------------------------------------------------------------|-----------------|------------|
| 0.1.0      | Document aangemaakt                                                                         | Marc Boerdijk   | 12/11/2019 |
| 0.1.1      | Domeinmodel toegevoegd met tekst                                                            | Remco de Baas   | 20/11/2019 |
| 0.1.2      | Activity diagrammen toegevoegd en uitgelegd                                                 | Remco de Baas   | 20/11/2019 |
| 0.1.3      | Inleiding, toelichting activity diagrammen, toelichting use case diagram                    | Patrick de Jong | 20/11/2019 |
| 0.1.4      | Inleiding aanvullen, toelichting activity diagrammen                                        | Siem Bosgra     | 20/11/2019 |
| 0.1.5      | Bijwerken use case diagram, inleiding aanvullen                                             | Siem Bosgra     | 21/11/2019 |
| 0.1.6      | Paragrafen gemaakt voor de uitleg van het domeinmodel.                                      | Remco de Baas   | 21/11/2019 |
| 0.1.7      | Activity diagrams Profiel bekijken en applicatie opstarten aangepast en opnieuw toegevoegd. | Remco de Baas   | 21/11/2019 |
| 0.1.8      | Activity diagram: Deelnemen aan activiteit aangepast/vernieuwd                              | Patrick de Jong | 21/11/2019 |
| 0.1.9      | Kleine aanpassingen aan activity diagrammen en de toelichtingen                             | Siem Bosgra     | 21/11/2019 |
| 0.2.0      | Schermontwerpen toegevoegd                                                                  | Guus Apeldoorn  | 21/11/2019 |
| 0.2.1      | Toelichting schermontwerpen                                                                 | Patrick de Jong | 21/11/2019 |
| 0.2.2      | Activity diagram tag filters toegevoegd                                                     | Siem Bosgra     | 21/11/2019 |

Inleiding
=========

In dit document worden specifieke beschrijvingen gegeven bij de gemaakte ontwerpen. De ontwerpen verduidelijken een proces of structuur die relevant is aan het project. Dit soort producten zijn bruikbaar voor de realisatiefase. De ontwerpen die worden beschreven in dit document, zijn het use case diagram, domeinmodel, schermontwerpen en activity diagrammen.

Ieder ontwerp is in afbeeldingsvorm ingevoegd in het document. De ontwerpen worden vervolgens toegelicht: hoe ze opgebouwd zijn en hoe de keuzes gemaakt zijn bij het ontwerpen. Deze uitleg verschilt per ontwerp sterk in lengte, omdat de ontwerpen ook in complexiteit zeer verschillen.

Dit document is bedoeld om de werking van het product duidelijk te maken. Het moet ervoor zorgen dat iedereen die het leest een goed beeld kan vormen van de applicatie. De ontwerpen moeten dus makkelijk te lezen en begrijpen zijn, maar toch in voldoende detail de werking van de applicatie tonen.

Use case diagram
================

In sprint 1 is een use case diagram gemaakt. Deze bevat alle aangevraagde functies die bepaald zijn door de Product Owner.

De gebruiker moet het volgende kunnen:

-   Een account aanmaken

-   Inloggen

-   Hobby's toevoegen

-   Activiteiten aanmaken

-   Aan activiteiten deelnemen

-   Foto's uploaden

-   Profielen bekijken

-   Gematched worden met andere gebruikers

-   Meldingen ontvangen

Een admin (oftewel administrator) moet het volgende kunnen:

-   Profielen beheren

-   Activiteiten beheren

<img src="media/image2.jpg" width="466" height="516" />

Domeinmodel
===========

<img src="media/image3.jpg" alt="A screenshot of a cell phone Description automatically generated" width="601" height="275" />

Figuur 1 Domeinmodel

De Accountklasse is de superklasse van de klassen Admin en User. Account heeft de basisinformatie die beide klasse nodig hebben om ons systeem te gebruiken. De User klasse heeft naast de informatie uit account ook zijn eigen eigenschappen. Deze informatie is uniek voor de User, omdat de informatie nodig is voor de andere klasse om juist te functioneren. De user klasse hangt vast aan vijf andere klassen.
Te beginnen met de hobby klasse. De klasse Hobby vormt een brug tussen Activity en User klasse. Een user kan ervoor kiezen om geen hobby’s op te geven. Als er toch en hobby toegevoegd is aan de user kan de Activity klasse controleren in welke activiteiten een gebruiker geïnteresseerd is. De User klasse is naast Hobby ook verbonden met de Activity Klasse. Omdat het voor iedereen toegankelijk moet zijn om activiteiten aan te maken of deel te nemen.

De klasse ActivityComments zorgt ervoor dat een gebruiker de mogelijkheid heeft om een reactie te plaatsen bij een activiteit. Als er een comment is zal die gekoppeld zijn aan zowel de gebruiker en de desbetreffende activiteit.

De gebruiker kan naast het meedoen aan activiteiten en reacties plaatsen ook chatten met anderen. Een chat gaat altijd over een gesprek tussen twee gebruikers. Om met een match te spreken moet die als eerstgevonden worden. Dit gebeurt in de Match klasse. De match klasse kijkt op basis van het profiel of twee mensen misschien vrienden kunnen worden.

Als er een match gevonden is wordt dit gestuurd naar de notificatie klasse. De notificatie klasse geeft melding terug aan de gebruiker dat hij/zij een match gevonden heeft. Voor Chat en Activity is de werking ongeveer gelijk. De meldingen voor de chat zijn gebaseerd op berichten die nieuw zijn binnengekomen. Voor de activiteiten zijn er verschillende factoren waardoor een melding gemaakt kan worden. Een melding kan gegenereerd zijn op basis van de datum en tijd die dichterbij komen, een aanpassing in de gegevens zoals een adres wat veranderd of bij het verwijderen van een activiteit.

Activity diagrammen
===================

Opstarten applicatie
--------------------

<img src="media/image4.png" width="601" height="306" />

Figuur 2 Activity diagram opstarten applicatie

Het opstarten van applicatie begint met de gebruiker die de applicatie activeert door er op te klikken. Als de applicatie een error tegenkomt voor het initialiseren dan zijn wij terug bij af en kan de gebruiker het nog een keer proberen. Als er geen errors zijn zal de applicatie zonder problemen opstarten en het scherm met de inhoud openen.

Aanmelden/registreren
---------------------

<img src="media/image5.png" width="601" height="293" />

Figuur 3 Activity diagram aanmelden/registreren

Vanuit het inlogscherm kan er gekozen worden of de gebruiker wil inloggen of zich wil aanmelden. Bij het aanmelden, het aanmaken van een nieuw account, worden om NAW-gegevens gevraagd en een wachtwoord. Bij de verificatie wordt gekeken of beide wachtwoordvelden ingevuld zijn en gelijk aan elkaar zijn. Zijn er gegevens niet correct ingevuld dat begint het aanmeldproces opnieuw. In het geval dat alle gegevens kloppen wordt het account aangemaakt en opgeslagen in de database.

De gebruiker heeft nu een account en hij/zij kan nu inloggen. Als de combinatie van het e-mailadres en het wachtwoord correct is wordt er ingelogd. Bij een onjuiste combinatie wordt de gebruiker gevraagd om het inloggen nogmaals te proberen.

<img src="media/image6.png" width="576" height="409" />

<img src="media/image7.png" width="576" height="409" />

Aanpassen account
-----------------

<img src="media/image8.jpg" width="601" height="247" />

Figuur 4 Activity diagram aanpassen account

Op het scherm waar de gebruiker de instellingen van het account kan aanpassen zijn veel keuzes aanwezig. Deze keuzes vallen onder vier categorieën: aanpassen wachtwoord, aanpassen profielweergave, aanpassen aanbevelingen en aanpassen meldingen.

Bij het aanpassen van het wachtwoord moet de gebruiker zijn oude wachtwoord invullen en twee keer het nieuwe wachtwoord. Op deze manier wordt er afgedwongen om het nieuwe wachtwoord goed in te vullen zodat de gebruiker niet per ongeluk een ander wachtwoord intypt. Het systeem verandert als laatste stap het wachtwoord van de gebruiker.<img src="media/image9.png" width="576" height="409" />

Het aanpassen van de profielweergave heeft meerdere opties. Het is mogelijk om het tonen van je interesses uit en aan te zetten. Dit geldt ook voor het tonen van de deelnames van de gebruiker aan activiteiten.

De aanbevelingen kunnen ook aangepast worden. De afstand (in km) tussen de gebruiker en andere gebruikers is een van deze instellingen. Zo kan de gebruiker kiezen of hij matches wil met gebruikers die dichtbij of ver weg wonen. Het aangeven van het leeftijdsverschil is ook mogelijk. Dit zorgt ervoor dat de gebruiker zijn eigen doelgroep makkelijker kan vinden in het geval dat hij/zij dit belangrijk vindt. Als derde optie kan je je interesses globaliseren, dit zorgt ervoor dat interesses die onder dezelfde categorie bevinden ook tegenkomt op de matchmaker. Een voorbeeld is een gebruiker die beroepsschilder is en wordt ‘gematched’ met een beeldhouwer omdat deze beiden onder de globale termen creatief en kunst vallen.

Voor het geval dat de gebruiker zich ergert aan meldingen kan hij deze ook uit zetten. Dit is mogelijk voor een nieuw chatbericht, een nieuwe match, een nieuwe comment op de comment van een gebruiker op een activiteit en bij een aanpassing van een activiteit.

Uiteindelijk zal de gebruiker op opslaan moeten klikken om de gegevens op te slaan. De applicatie zal de gegevens opslaan in de database.

<img src="media/image10.png" width="576" height="663" />

Profiel bekijken
----------------

<img src="media/image11.png" width="601" height="195" />

Figuur 5 Activity diagram profiel bekijken

Om bij een profiel te komen en deze te bekijken begint het met het selecteren van een profiel die je wilt zien. Om een profiel te zien het gewenste profiel in jouw lijst met matches staan. Als er een profiel geselecteerd is gaat het systeem op zoek. Het systeem kijkt of een gebruiker geblokkeerd is om te bepalen wat er op het volgende scherm getoond moet worden. Als een profiel niet geblokkeerd is dan wordt deze netjes getoond met alle informatie die er voor dat profiel te vinden is. Is dit niet zo dan wordt een special pagina geladen voor profielen die geblokkeerd zijn. Hierop is geen informatie te vinden, omdat dit niet toegankelijk is voor alle andere gebruikers die en profielen die een match hebben met de geblokkeerde.

<img src="media/image12.png" width="576" height="507" />

Hobby toevoegen
---------------

<img src="media/image13.png" width="601" height="190" />

Figuur 6 Activity diagram hobby toevoegen

Gebruikers kunnen vanuit hun persoonlijke profielpagina hobby’s toevoegen. Eerst klikt de gebruiker op de knop om een nieuwe hobby toe te voegen: de ‘Add new interest’-knop. Vervolgens kan met behulp van de zoekfunctie worden gezocht naar een hobby die de gebruiker wil toevoegen aan zijn of haar profiel. Als de klant een leuke hobby gevonden heeft, kan deze worden toegevoegd met de toevoegknop. Het systeem zorgt er vervolgens voor dat de hobby wordt toegevoegd aan het profiel van de gebruiker. Als de gebruiker nog meer hobby’s wil toevoegen, kan hij of zij opnieuw een hobby zoeken om toe te voegen. Zo niet, is het proces beëindigd. <img src="media/image14.png" width="576" height="409" />

Foto’s uploaden
---------------

<img src="media/image15.png" width="601" height="249" />

Figuur 7 Activity diagram foto's uploaden

Om foto’s te uploaden wordt Windows verkenner geopend. Als de verkenner geopend is kunnen een of meerdere foto’s geüpload geselecteerd worden. Zijn de foto’s gekozen en verzonden zal Windows verkenner sluiten.
Het systeem zal dan controleren of het gaat om afbeeldingen van het juiste type om te voorkomen dat er geen andere bestanden naar binnen kunnen komen. Zitten er wel andere bestanden tussen dan zal dit aangegeven worden door een melding. De melding geeft aan dat er verkeerde bestandstypen geüpload zijn. Dan zal het proces nog een keer uitgevoerd worden. Zijn de bestanden wel van de bestandstypen dan worden deze toegevoegd. Gaat dit fout dan zal er een melding gegeven worden die uitlegt dat er iets fout is gegaan tijdens het uploaden van de afbeelding of afbeeldingen. Als de afbeeldingen wel juist toegevoegd zijn zal er dit keer een melding verschijnen die aangeeft dat het met succes afgerond is.

<img src="media/image16.png" width="288" height="204" /><img src="media/image17.png" width="288" height="204" />

Deelnemen aan activiteit
------------------------

<img src="media/image18.png" width="480" height="287" />

Figuur 8 Activity diagram Deelnemen aan activiteit

De gebruiker ziet een activiteit waar hij/zij graag aan wil deelnemen. Op de pagina van de activiteit staat een knop met “Join Event". Bij het indrukken van deze knop gaat het systeem de gebruiker ophalen die op deze knop geklikt heeft. Vervolgens voegt het systeem deze toe aan de lijst van de deelnemers voor deze activiteit. Het proces is dan succesvol verlopen en de matchmakerapplicatie zal aangeven dat de desbetreffende persoon deelneemt aan de activiteit. De pagina ziet er voor de gebruiker anders uit, de “Join Event"-knop verandert in de “Leave event"-knop zodat de gebruiker zich ook weer af kan melden voor de activiteit.

<img src="media/image19.png" width="288" height="204" /><img src="media/image20.png" width="288" height="204" />

Activiteit taggen
-----------------

<img src="media/image21.png" width="601" height="156" />

Figuur 9 Activity diagram Activiteit taggen

De gebruiker kan een lijst zien met activiteiten die door gebruiker zelf zijn aangemaakt. Vanuit deze lijst kan de gebruiker een activiteit selecteren. Als de gebruiker die activiteit niet wil taggen, gaat de gebruiker terug naar de activiteitenlijst. Zo wel, klikt de gebruiker op de tag-knop en kan hij of zij de naam van de tag invullen. Het systeem voegt dan de tag aan de activiteit toe. De gebruiker kan terug keren naar de activiteitenlijst om nog meer activiteiten te taggen. Als dit niet nodig is, eindigt het proces.

<img src="media/image22.png" width="576" height="409" />

Melding ontvangen
-----------------

<img src="media/image23.png" width="601" height="319" />

Figuur 10 Activity diagram Melding ontvangen

Er kunnen events plaatsvinden die het versturen van een melding naar een gebruiker triggeren. De drie events waarbij dit gebeurt, zijn wanneer er een match gemaakt wordt tussen twee gebruikers, wanneer een activiteit ge-updatet wordt en wanneer een comment onder een activiteit geplaatst wordt (en de gebruiker is aangemeld voor die activiteit). In die drie gevallen maakt het systeem een melding aan, die vervolgens wordt ontvangen door de gebruiker.

Adminactiviteiten
-----------------

<img src="media/image24.png" width="601" height="278" />

Figuur 11 Activity diagram adminactiviteiten

Iemand die een administratoraccount heeft wordt naar de startpagina geleid voor de admins. Hier heeft hij de keuze om naar gebruikers en activiteiten te bekijken. De admin zal dan opties krijgen wat hij wil doen. Zo kan hij ervoor kiezen om terug te gaan naar het startscherm of een van de andere acties te kiezen. Als er een gebruiker geselecteerd is dan worden er twee opties gegeven: verwijderen en blokkeren. Als een van deze twee opties gekozen is dan worden die door het systeem uitgevoerd. Om er zeker van te zijn of het gewenste resultaat behaald is wordt een controlecheck uitgevoerd. Als er niks gebeurd is zal er een foutmelding verschijnen. Is het resultaat behaald dan wordt er een melding gemaakt die aangeeft dat het correct uitgevoerd is. Naast het verwijderen en blokkeren van een gebruiker is het ook mogelijk om een activiteit te verwijderen. Dit werkt volgens het zelfde principe als bij een gebruiker, maar dan voor het verwijderen van een activiteit. Als je klaar bent kan je door met het bekijken van ander gebruikers en activiteiten. Als de admin vindt dat hij klaar is kan hij uitloggen en de applicatie sluiten.

<img src="media/image25.png" width="576" height="409" /><img src="media/image26.png" width="576" height="409" />

Profiel aanpassen
-----------------

<img src="media/image27.jpg" width="601" height="530" />

Figuur 12 Activity diagram Profiel aanpassen

Er zijn verschillende manieren waarom een gebruiker aanpassingen kan doen aan zijn of haar profiel. Een van deze aanpassingen is al omschreven in een ander activity diagram: het toevoegen van een hobby. De andere drie aanpassingen worden in dit activity diagram beschreven. Er wordt vanuit gegaan dat de gebruiker zich al bevindt op de eigen profielpagina.

Het wijzigen van de naam gebeurt als volgt. De gebruiker klikt op de naam bovenaan de profielpagina. Dan kan deze gewijzigd worden. Als de gebruiker klikt op het vinkje wordt de naam door het systeem aangepast in de database. Als de gebruiker op het kruisje klikt wordt de naam weer teruggezet naar wat het was. De gebruiker stopt dan met het bewerken van de naam.

Het wijzigen van de beschrijving gebeurt op een vergelijkbare manier. Eerst klikt de gebruiker op de beschrijving op de profielpagina, waarna de gebruiker deze kan wijzigen. Klikt de gebruiker op het vinkje, dan verandert het systeem de beschrijving in de database. Als de gebruiker op het kruisje klikt, wordt de oude beschrijving weer teruggezet en stopt de gebruiker met het bewerken van de beschrijving.

De laatste aanpassing is het bewerken van de foto’s op de profielpagina van een gebruiker. Deze aanpassing heeft drie opties: het toevoegen van een foto, het vervangen (of aanpassen van een foto) en het verwijderen van een foto.

Bij het toevoegen van een foto klikt de gebruiker op ‘Add new picture’. Er verschijnt een schermpje en de gebruiker kan een foto uploaden. Als de gebruiker de foto wil uploaden op het profiel, klikt hij of zij op ‘Done’ en wordt de foto door het systeem in de database toegevoegd aan het profiel van de gebruiker. De gebruiker kan ook op ‘Cancel’ klikken, dan wordt het schermpje weer afgesloten.

De gebruiker kan ook een foto aanpassen, door op het aanpas-icoon bij de foto te klikken. Vervolgens verschijnt hetzelfde schermpje als bij het toevoegen van een nieuwe foto. De gebruiker kan een foto uploaden en op ‘Done’ klikken, dan wordt de oude foto door het systeem vervangen door de nieuwe foto. Ook hier kan de gebruiker op ‘Cancel’ klikken om het schermpje af te sluiten.

Tot slot kan de gebruiker een foto verwijderen. Dit kan door op het verwijder-icoon bij de foto te klikken. De gebruiker krijgt de vraag of de foto echt verwijderd moet worden of niet. Als de gebruiker klikt op het vinkje, wordt de foto door het systeem in de database verwijderd van het profiel van de gebruiker. Klikt de gebruiker op het kruisje, dan wordt het verwijderen van de foto geannuleerd.

De gebruiker doet aanpassingen aan zijn of haar profiel, totdat de gebruiker geen aanpassingen meer wil doen. Dan eindigt het proces.

<img src="media/image28.png" width="288" height="204" /><img src="media/image29.png" width="288" height="204" />

Schalen van de applicatie
-------------------------

<img src="media/image30.png" width="601" height="291" />

Figuur 13 Activity diagram Schalen van de applicatie

Bij het schalen van de applicatie kan de gebruiker kiezen uit een Windowed, Fullscreen of een Minimalized scherm. Maximaal een van deze drie keuzes kan op hetzelfde moment aan staan. Daarvoor moet er bij het kiezen van een ander schermafmeting de andere afmetingen uitgezet worden. Daarna zal het scherm succesvol geschaald zijn.

Activiteit aanpassen
--------------------

<img src="media/image31.png" width="288" height="204" /><img src="media/image32.png" width="288" height="204" />

<img src="media/image33.png" width="288" height="204" /><img src="media/image17.png" width="288" height="204" />

Bekijken activiteit
-------------------

<img src="media/image34.png" width="576" height="655" />

Dit is de activiteitenpagina. Bovenaan staat de titel van het evenement inclusief de locatie en tijd. De tags die erbij passen zijn aangegeven door de gebruiker die het evenement heeft aangemaakt. Daaronder volgt een korte beschrijving van het evenement. De mensen die deelnemen aan het event en hun eventuele comments staan weer daaronder. De gebruiker kan deze comments leuk vinden en een hartje geven.

Chatten
-------

<img src="media/image35.png" width="576" height="409" />

Dit is het scherm van een geopende chat. Rechts staan de chats van de gebruiker zelf en links de berichten van de andere gebruiker. Het verstuurde bericht staat samen met de tijd van dat moment.

Home screen
-----------

<img src="media/image36.png" width="576" height="409" />

Dit is het hoofdscherm van de applicatie. Hierop staan de aanbevolen matches en activiteiten. Door te klikken op een afbeelding van een gebruiker wordt de gebruiker doorgestuurd naar het profiel. Hier kan de gebruiker een ander bekijken en op basis daarvan besluiten of hij/zij een match wil vormen.
Bij de aanbevolen activiteiten kan de gebruiker klikken op de afbeelding om op de pagina van de desbetreffende activiteit te komen. Hier kan de gebruiker aangeven of hij/zij wil deelnemen. Op de hoofdpagina is echter al een knop rechtsboven van de afbeelding. Wanneer de gebruiker deze aan vinkt zal de gebruiker **gelijk** deelnemen aan de activiteit.

Matches bekijken
----------------

<img src="media/image37.png" width="576" height="409" />

Op de pagina van de matches kan de gebruiker zijn gemaakte matches zien. Chatberichten staan boven het scherm, hierdoor ziet de gebruiker gelijk of er een nieuw bericht is. Onder de chats van de gebruiker staan andere gemaakte matches waar nog geen chats mee verstuurd zijn.

Activiteiten bekijken
---------------------

<img src="media/image38.png" width="601" height="216" />

Bovenstaand activity diagram gaat over het filteren van activiteiten met behulp van tags. De gebruiker kan zoeken op tags en deze vervolgens toevoegen aan het filter. Als de gebruiker tevreden is kan hij of zij klikken op ‘Add filters’. Zo niet, kan de gebruiker de tagselectie aanpassen of op ‘Cancel drukken’. Het systeem zorgt uiteindelijk dat het filter wordt toegepast op de zoekresultaten.

<img src="media/image39.png" width="576" height="585" />

Op de pagina kan je aangeraden activiteiten zien, ze geven aan welke activiteiten aangeraden worden gebaseerd op de interesses van het account. Beneden kan je zien dat welke activiteiten in de regio van het account zijn. Het is mogelijk om een andere locatie of datum zijn dan die dag.

<img src="media/image40.png" width="288" height="204" /><img src="media/image41.png" width="288" height="204" />
