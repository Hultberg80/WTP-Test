# WTP – CRM-system för ärendehantering

WTP (Web Ticket Platform) är ett CRM-system som hanterar kundärenden genom formulär, 
en chattfunktion mellan kund och personal samt ett rollbaserat inloggningssystem 
för kundtjänst och administratörer.

## 🚀 Kom igång

### Backend

Körs via ASP.NET Core (C#).

```bash
cd WTP-main/server
dotnet run
```
Servern startar som standard på http://localhost:5000

Frontend byggs till wwwroot/ och servas automatiskt via 
UseStaticFiles() och MapFallbackToFile("index.html") i Program.cs.


### Frontend (Vite + React)

Frontend ligger i WTP-main/client och byggs som en statisk app.

```
cd WTP-main/client
npm install
npm run build
````

Vite är konfigurerad att bygga till backendens wwwroot:

````
// vite.config.js
build: {
  outDir: '../server/wwwroot',
  emptyOutDir: true
}
````

### Tester
API-tester (Postman + Newman)
API-tester finns i testing/postman.
Kör via Newman:

````
cd END2ENDTester
 dotnet test
````
END2ENDTester använder Playwright med xUnit och är 
konfigurerade för att köras headless i CI-miljö. 

END2ENDTester inkluderar:

  #Inloggning
  
  #Formulär
  
  #Chatt
  
  #Adminvyer (skapa/ta bort användare)


🤖 CI/CD via GitHub Actions

Allt testas automatiskt vid varje push till main genom 
.github/workflows/dotnet.yml

Flödet inkluderar:

  #Installation av beroenden

  #Start av backend och PostgreSQL

  #Bygg och test av frontend

  #API-tester via Postman

  #GUI-tester via Playwright

  #(Valbar) Deploy via SSH
