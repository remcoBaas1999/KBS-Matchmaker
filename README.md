# Setup manual Friendr

ICT.SE.KBS.V19

---

## Setup applicatie

Om de applicatie te starten hoeft de gebruiker enkel het bijgeleverde bestand in de Windows Verkenner te vinden en er twee keer op te klikken.

## Gebruik applicatie

### Inloggen

Wanneer de gebruiker de applicatie opent, komt deze op de inlogpagina terecht. Hier kan de gebruiker zijn/haar emailadres en wachtwoord invullen. Als de gebruiker dan op de *login*-knop drukt en de gegevens correct zijn, wordt hij doorgestuurd naar de startpagina.

### Account aanmaken

De gebruiker kan ook vanaf het inlogscherm ervoor kiezen om een nieuw account te registreren door op de *Create account*-knop te drukken. Hij wordt dan naar de registratiepagina doorverwezen waar de gebruiker de persoonlijke informatie kan invullen die nodig is voor het registreren van een account.

## Setup server

*Benodigdheden: 1 Debian-based webserver met een internetverbinding*

De server kan worden aangezet door Microsoft's dotnet packages te installeren, de source te clonen van GitHub (https://github.com/remcoBaas1999/KBS-Matchmaker.git) en het commando ```dotnet run``` uit te voeren in de subdirectory ```MatchmakerAPI/``` van de source code.
