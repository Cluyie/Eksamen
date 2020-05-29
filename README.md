# UCLDreamTeam
UCL 4. Semester Dreamteam Repository  
[Dokumentation](https://docs.google.com/document/d/1LsvcOnyi4bbdBS5vJB79S8oc9nSKq3CvjDTdlnTwY4k/edit?usp=sharing)

## Startup
MicroService indstillinger: [sharedSettings.json](sharedSettings.json)  
Disse indstillinger bruges i alle MicroServices. Lige nu er det kun "UseInMemoryDatabase" der bliver brugt til at vælge in memory database eller vores fælles database.

For at køre programmet i Visual Studio 2019 anbefaler vi at hente udvidelsen "SwitchStartupProject for VS 2019": https://www.vsixgallery.com/extension/399c17d5-6a98-44e4-938e-6d0f1f804076

Denne udviddelse bruger vores configuration til at skifte mellem de essentielle dele i vores projekt, dvs. MicroService, App, AdminPanel og en kombination af dem alle. App og AdminPanel kan ikke køre uden at have en MicroService backend, så det er vigtigt at have dem startet.

For at debug App'en, anbefaler vi derudover også at hente en Android emulator. Dette gøres ved at vælge projektet "XamarinFormsApp.Android" ved at højre klikke og trykke "Set as Startup Project". Herefter burde der være en knap til at åbne Android Device Manager, hvor man kan vælge en Android telefon. Vi har benyttet version 10.0 (API 29) til debugging.

## Iteration 1: 03/02 - 06/03
- App: @Alex Holm, @Christoffer Kragh Pedersen, @Ricardo Enrique Dollerup og @Simon Hansen 
- Web Service & Signal R: @Frederik Hegnet Petersen, @Henrik Langer Hallemberg Schou, @Tobias Lauritzen og @Sebastian Søberg
- Database: @Caspar, @Jacob Skov Madsen, @Simone og @Steffen Juhl 
- Dokumentation: @Anders Østergaard Laursen, @Andreas G Hede og @Jonas 
- Mock-ups: @Frederik Knudsen og @Oleksii 


## Iteration 2: 06/03 - 27/03
- App: @Andreas G Hede, @Henrik Langer Hallemberg Schou,@Oleksii @Tobias Lauritzen og @Steffen Juhl 
- API: @Alex Holm, @Caspar @Frederik Knudsen, @Jacob Skov Madsen og @Jonas 
- Database: @Anders, @Christoffer Kragh Pedersen, @Ricardo Enrique Dollerup og @Simon Hansen 
- Website og Design (Mockups): @Anders Østergaard Laursen, @Christian Jørgensen, @Frederik Hegnet Petersen, @Sebastian Søberg og @Simone 

## Iteration: 03/04 - 01/05

- App udvikling: Anders L, Jonas, Christian, Frederik, Hegnet og Sebastian
- Api: Steffen, Ricardo, Simon, Andreas, Simone, Oleksii
- Web: Anders J, Christoffer, Tobias, Alex, Henrik, Caspar og Skov

## Architecture diagram  
![alt text](https://github.com/lasserasch/UCLDreamTeam/blob/master/Architecture%20diagram.png "")
[Link til redigering](https://creately.com/diagram/k8diih3q1/Uj1fnQbfuYOHFMUdKdiTGqcOt8%3D)


## Pattern diagram  
![alt text](https://github.com/lasserasch/UCLDreamTeam/blob/master/UCLDreamTeam%20Patterns%20Diagram.jpg "")
