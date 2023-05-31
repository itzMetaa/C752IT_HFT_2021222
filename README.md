2023.05.31 - 21:29
final version pushed yey

Minden crud és non-crud implementálva aszinkron módon a wpf és js kliensen.
Inmemory database miatt nem cascaded a törlés, ez megoldható ha a #connection regionben átkommentezzük az sqlserverre a connection típusát.

Startup projectek: Endpoint, JSClient, WPFClient



OUTDATED:
Megvannak a pdf-ben említett kritériák: legalább 5 non crud, több tablet érintő lekérdezések, listázás, és mindegyik elérhető a menüből, a test-ek lefutnak, 25 commit minimum.
InMemory databasenél nem működik a cascade delete nem tudom miért, sqlserver-el teszteltem újra és működött rendesen.
Nincs lekezelve a bekérés, viszont az interfacet kicsit polisholtam, hogy ne csak az id meg a játék nevét írja ki.
Startup projectek: Client és Endpoint
